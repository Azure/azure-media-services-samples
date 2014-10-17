using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    public class PrioritizedHeuristicsManager
    {
        #region Private Member Variables

        private readonly Analytics _analytics;
        private readonly DispatcherTimer _clearReEnableTimer;
        private readonly List<SSMEStateInfo> _ssmeStateInfoList;
        private readonly DispatcherTimer _timer;
        private int _intervalDelta;
        private int _maxReEnableAttempts = 2;
        private double _maximumRenderedFpsForMinimizedDetection = 2.0f;
        private int _monitorIntervalInMilliseconds = 10000;
        private bool _recommendPermanentlyDisableAllSecondary;

        #endregion Private Member Variables

        #region Constructors

        /// <summary>
        /// Constructor that takes an initial capacity 
        /// </summary>
        public PrioritizedHeuristicsManager()
        {
            _ssmeStateInfoList = new List<SSMEStateInfo>();
            _timer = new DispatcherTimer();
            // poll every second so we can average FPS
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Dispatcher_Tick;

            _clearReEnableTimer = new DispatcherTimer();
            _clearReEnableTimer.Interval = TimeSpan.FromMinutes(2);
            _clearReEnableTimer.Tick += _clearReEnableTimer_Tick;

            // instance our Analytics class
            // we must do this in a try / catch block because
            // this can throw on some machines because of 
            // a windows bug
            try
            {
                _analytics = new Analytics();
            }
            catch (Exception) //TODO: what is the specific exception to catch here?
            {
                // do nothing
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// AddSmoothStreamingMediaElement
        /// </summary>
        /// <param name="smoothStreamingMediaElement">the SmoothStreamingMediaElement</param>
        public void AddSmoothStreamingMediaElement(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            AddSmoothStreamingMediaElement(smoothStreamingMediaElement, 0, 0.0);
        }

        /// <summary>
        /// AddSmoothStreamingMediaElement
        /// </summary>
        /// <param name="smoothStreamingMediaElement">the SmoothStreamingMediaElement</param>
        /// <param name="minimumPlaybackBitrate">
        ///     The minimum bitrate (in bps) this SmoothStreamingMediaElement
        ///     must be capable of playing before the next SmoothStreamingMediaElement
        ///     should be enabled
        /// </param>
        /// <param name="minimumRenderedFramesPerSecond">
        ///     The minimum RenderedFramesPerSecond this SmoothStreamingMediaElement
        ///     must be achieving at the minimum bitrate before the next 
        ///     SmoothStreamingMediaElement should be enabled
        /// </param>
        public void AddSmoothStreamingMediaElement(SmoothStreamingMediaElement smoothStreamingMediaElement,
                                                   ulong minimumPlaybackBitrate,
                                                   double minimumRenderedFramesPerSecond)
        {
            if (ContainsSmoothStreamingMediaElement(smoothStreamingMediaElement))
            {
                throw new ArgumentException("SmoothStreamingMediaElement has already been added",
                                            "smoothStreamingMediaElement");
            }
            if (smoothStreamingMediaElement == null)
            {
                throw new ArgumentNullException("smoothStreamingMediaElement");
            }
            if (minimumRenderedFramesPerSecond < 0.0)
            {
                throw new ArgumentOutOfRangeException("minimumRenderedFramesPerSecond");
            }

            //finally, add to our collection
            var ssmeStateInfo = new SSMEStateInfo();
            ssmeStateInfo.SmoothStreamingMediaElement = smoothStreamingMediaElement;
            ssmeStateInfo.MinimumPlaybackBitrate = minimumPlaybackBitrate;
            ssmeStateInfo.MinimumRenderedFramesPerSecond = minimumRenderedFramesPerSecond;

            // TODOL: workaround to get the initial download and playback bitrates,
            // this can be removed when the SSME control exposes the properties
            SmoothStreamingMediaElement coreSmoothStreamingMediaElement = smoothStreamingMediaElement;
            if (coreSmoothStreamingMediaElement != null)
            {
                ssmeStateInfo.LastDownloadTrackBitrate = coreSmoothStreamingMediaElement.VideoDownloadTrack != null
                                                             ? coreSmoothStreamingMediaElement.VideoDownloadTrack.
                                                                   Bitrate
                                                             : 0;
                ssmeStateInfo.LastPlaybackTrackBitrate = coreSmoothStreamingMediaElement.VideoPlaybackTrack != null
                                                             ? coreSmoothStreamingMediaElement.VideoPlaybackTrack.
                                                                   Bitrate
                                                             : 0;
            }

            _ssmeStateInfoList.Add(ssmeStateInfo);

            HookEvents(smoothStreamingMediaElement);
        }

        /// <summary>
        /// Removes a SmoothStreamingMediaElement
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public void RemoveSmoothStreamingMediaElement(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);

            UnhookEvents(ssmeStateInfo.SmoothStreamingMediaElement);
            _ssmeStateInfoList.Remove(ssmeStateInfo);
        }

        /// <summary>
        /// Clears the MultiSSMEHeuristicsManager of all it's
        /// SmoothStreamingMediaElement references and stops monitoring
        /// </summary>
        public void Clear()
        {
            StopMonitoring();

            for (int i = 0; i < _ssmeStateInfoList.Count; i++)
            {
                UnhookEvents(_ssmeStateInfoList[i].SmoothStreamingMediaElement);
            }
            _ssmeStateInfoList.Clear();
        }

        /// <summary>
        /// Resets all of the ReEnableAttempt counts
        /// and sets the enabled state of the secondary SSMEs back 
        /// to disabled.  Call this method after you seek or play an add
        /// </summary>
        public void ResetRecommendions()
        {
            StopMonitoring();

            // skip the first SSME
            for (int i = 1; i < _ssmeStateInfoList.Count; i++)
            {
                _ssmeStateInfoList[i].ResetReEnableCount();
                _ssmeStateInfoList[i].RecommendedEnable = false;
            }
            _recommendPermanentlyDisableAllSecondary = false;
        }

        /// <summary>
        /// SetMinimumPlaybackBitrate
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        /// <param name="minimumPlaybackBitrate">
        ///     The minimum bitrate (in bps) this SmoothStreamingMediaElement
        ///     must be capable of playing before the next SmoothStreamingMediaElement
        ///     should be enabled
        /// </param>
        public void SetMinimumPlaybackBitrate(SmoothStreamingMediaElement smoothStreamingMediaElement,
                                              ulong minimumPlaybackBitrate)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            ssmeStateInfo.MinimumPlaybackBitrate = minimumPlaybackBitrate;
        }

        /// <summary>
        /// GetMinimumPlaybackBitrate - gets the bitrate set during the call to SetMinimumPlaybackBitrate
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public ulong GetMinimumPlaybackBitrate(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            return ssmeStateInfo.MinimumPlaybackBitrate;
        }

        /// <summary>
        /// SetMinimumRenderedFramesPerSecond specifies the minimum RenderedFramesPerSecond
        /// each SmoothStreamingMediaElement should sustain before the next  
        /// SmoothStreamingMediaElement is enabled
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        /// <param name="minimumRenderedFramesPerSecond">minimumRenderedFramesPerSecond</param>
        public void SetMinimumRenderedFramesPerSecond(SmoothStreamingMediaElement smoothStreamingMediaElement,
                                                      double minimumRenderedFramesPerSecond)
        {
            if (minimumRenderedFramesPerSecond < 0.0)
            {
                throw new ArgumentOutOfRangeException("minimumRenderedFramesPerSecond");
            }
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            ssmeStateInfo.MinimumRenderedFramesPerSecond = minimumRenderedFramesPerSecond;
        }

        /// <summary>
        /// GetMinimumRenderedFramesPerSecond returns the value set in SetMinimumRenderedFramesPerSecond
        /// or in the AddSmoothStreamingMediaElement method
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public double GetMinimumRenderedFramesPerSecond(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            return ssmeStateInfo.MinimumRenderedFramesPerSecond;
        }

        /// <summary>
        /// Returns the last observed RenderedFramesPerSecond
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public double GetAverageRenderedFramesPerSecond(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            return ssmeStateInfo.AverageRenderedFramesPerSecond;
        }

        /// <summary>
        /// Returns the last observed PlaybackTrack.Bitrate
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public ulong GetLastPlaybackBitrate(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            return ssmeStateInfo.LastPlaybackTrackBitrate;
        }

        /// <summary>
        /// Returns the last observed DownloadTrack.Bitrate
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        public ulong GetLastDownloadBitrate(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo =
                GetSSMEStateInfoThrowIfNotFound(smoothStreamingMediaElement);
            return ssmeStateInfo.LastDownloadTrackBitrate;
        }

        /// <summary>
        /// Causes the MultiSSMEHeuristicsManager to start monitoring
        /// </summary>
        public void StartMonitoring()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
                _clearReEnableTimer.Start();
            }
        }

        /// <summary>
        /// Causes the MultiSSMEHeuristicsManager to stop monitoring
        /// </summary>
        public void StopMonitoring()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                _clearReEnableTimer.Stop();
            }
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Indexer that returns the SmoothStreamingMediaElement by index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns></returns>
        public SmoothStreamingMediaElement this[int index]
        {
            get
            {
                if (index < 0 || index >= _ssmeStateInfoList.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return _ssmeStateInfoList[index].SmoothStreamingMediaElement;
            }
        }

        /// <summary>
        /// The interval at which we poll SmoothStreamingMediaElement data
        /// and raise the RecommendationChanged event
        /// 
        /// Default value is every 3 seconds
        /// </summary>
        public int MonitorIntervalInMilliseconds
        {
            get { return _monitorIntervalInMilliseconds; }
            set
            {
                if (value < 1000)
                {
                    throw new ArgumentException("Only values of 1000 or greater are valid");
                }
                _monitorIntervalInMilliseconds = value;
            }
        }

        /// <summary>
        /// The number of times the recommendation to reenable
        /// should be raised.  
        /// 
        /// Default value is 2
        /// </summary>
        public int MaxReenableAttempts
        {
            get { return _maxReEnableAttempts; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _maxReEnableAttempts = value;
            }
        }

        /// <summary>
        /// SmoothStreamingMediaElement.RenderedFramesPerSecond will drop to 
        /// zero when the browser is minimized - even when playback is still
        /// working correctly.  This property specifies a maximum threshold
        /// that SmoothStreamingMediaElement.RenderedFramesPerSecond to trigger
        /// this browser minimized detection
        /// 
        /// Default value is 2.0
        /// </summary>
        public double MaximumRenderedFPSForMinimizedDetection
        {
            get { return _maximumRenderedFpsForMinimizedDetection; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Value must be greater than 0.0");
                }
                _maximumRenderedFpsForMinimizedDetection = 0;
            }
        }

        /// <summary>
        /// The count of SmoothStreamingMediaElement's being tracked
        /// </summary>
        public int Count
        {
            get { return _ssmeStateInfoList.Count; }
        }

        /// <summary>
        /// True if the MultiSSMEHeuristicsManager is monitoring
        /// To enable monitoring, call the StartMonitoring method
        /// </summary>
        public bool IsMonitoring
        {
            get { return _timer.IsEnabled; }
        }

        /// <summary>
        /// RecommendDisableAllSecondary - true if we've recommended
        /// to permanantly disable all secondary SmoothStreamingMediaElements
        /// </summary>
        public bool RecommendPermanentlyDisableAllSecondary
        {
            get { return _recommendPermanentlyDisableAllSecondary; }
        }

        #endregion Public Properties

        #region Public Events

        /// <summary>
        /// Event is raised anytime a recommendation changes about enabling or disabling a 
        /// SmoothStreamingMediaElement
        /// </summary>
        public event EventHandler<RecommendationChangedEventArgs> RecommendationChanged;

        /// <summary>
        /// RaiseRecommendationChanged
        /// </summary>
        private void RaiseRecommendationChanged(IList<SmoothStreamingMediaElement> enable,
                                                IList<SmoothStreamingMediaElement> disable)
        {
            EventHandler<RecommendationChangedEventArgs> handler = RecommendationChanged;
            if (handler != null)
            {
                var args =
                    new RecommendationChangedEventArgs(enable,
                                                       disable,
                                                       _recommendPermanentlyDisableAllSecondary);
                handler(this, args);
            }
        }

        #endregion Public Events

        #region Private Methods and Properties

        /// <summary>
        /// Main heuristics logic
        /// </summary>
        private void Dispatcher_Tick(object sender, EventArgs e)
        {
            _intervalDelta += 1000;

            if (_intervalDelta < _monitorIntervalInMilliseconds)
            {
                // just update the FPS averages
                for (int i = 0; i < _ssmeStateInfoList.Count; i++)
                {
                    UpdateAverageFramesPerSecond(_ssmeStateInfoList[i]);
                }
            }
            else
            {
                _intervalDelta = 0;
                MatchPlaySpeeds();

                var enable =
                    new List<SmoothStreamingMediaElement>();

                var disable =
                    new List<SmoothStreamingMediaElement>();

                // Simple algoritm for determining which SSMEs to enable
                // or disable
                // 1) SSME 0 is always enabled
                // 2) If SSME 0 can't meet the minimum bitrate, disable 
                //    all of the other SSME's
                // 3) If SSME 0 can meet the minimum bitrate, enable
                //    the next disabled SSME 
                // 4) When enabling the next disabled SSME, check
                //    all of the intermediate SSMEs and make sure they
                //    are keeping up and disable them if they are not
                bool disableRest = false;
                bool enableNext = false;
                for (int i = 0; i < _ssmeStateInfoList.Count; i++)
                {
                    SSMEStateInfo ssmeStateInfo = _ssmeStateInfoList[i];

                    if (disableRest)
                    {
                        ssmeStateInfo.RecommendedEnable = false;
                        disable.Add(ssmeStateInfo.SmoothStreamingMediaElement);
                        continue;
                    }

                    // if we're not disabling the rest, see how the 
                    // current SSME is doing
                    ulong minBitrateRequested = ssmeStateInfo.MinimumPlaybackBitrate;
                    double minRenderedFPS = ssmeStateInfo.MinimumRenderedFramesPerSecond;
                    ulong playbackBitrate = ssmeStateInfo.LastPlaybackTrackBitrate;
                    ulong downloadBitrate = ssmeStateInfo.LastDownloadTrackBitrate;
                    UpdateAverageFramesPerSecond(ssmeStateInfo);

                    double renderedFPS = ssmeStateInfo.AverageRenderedFramesPerSecond;


                    // this next section can be replaced with a call to 
                    // Math.Min(downloadTrack.Bitrate, playbackTrack.Bitrate)
                    // but I've expanded it for clarity (and to allow 
                    // special tweaks for things like ads, etc)
                    ulong effectiveBitrate = 0;
                    if (downloadBitrate < playbackBitrate)
                    {
                        // we're downloading a lower bitrate than 
                        // we're playing - so we're scaling down
                        // which means we can't handle this bitrate
                        effectiveBitrate = downloadBitrate;
                    }
                    else if (downloadBitrate > playbackBitrate)
                    {
                        // we're downloading a higher bitrate, so we can 
                        // handle playing this bitrate.  We're still not 
                        // sure if we can handle playing this bitrate, so 
                        // use the bitrate we know we can handle
                        effectiveBitrate = playbackBitrate;
                    }
                    else
                    {
                        effectiveBitrate = playbackBitrate;
                    }

                    if (effectiveBitrate == 0)
                    {
                        // we can get into this state if downloadBitrate
                        // has never been updated
                        effectiveBitrate = Math.Max(playbackBitrate, downloadBitrate);
                    }

                    if (enableNext)
                    {
                        // The previous SSME was playing or downloading at or above  
                        // the minBitrate - enable the next disabled SSME
                        // but check enabled SSME's along the way and make sure
                        // they can still handle playback
                        if (ssmeStateInfo.RecommendedEnable)
                        {
                            // we previously recommended enabling this
                            // SSME - let's check in and make sure 
                            // it should still be enabled
                            if (effectiveBitrate < minBitrateRequested ||
                                (renderedFPS < minRenderedFPS && !GetIsMinimized(renderedFPS)))
                            {
                                // this enabled SSME is not keeping up
                                ssmeStateInfo.RecommendedEnable = false;
                                disable.Add(ssmeStateInfo.SmoothStreamingMediaElement);
                                disableRest = true;
                            }
                            else
                            {
                                // continue recommeding this SSME be enabled
                                enable.Add(ssmeStateInfo.SmoothStreamingMediaElement);
                            }
                        }
                        else
                        {
                            // we were asked to enable the next disabled
                            // SSME, and along our way to find it we have 
                            // validated the all of the other enabled SSME's
                            // should still be enabled.  It's time to turn on 
                            // a disabled SSME (if it hasn't been re-enabled 
                            // too many times)
                            ssmeStateInfo.ReEnableCount++;
                            if (ssmeStateInfo.ReEnableCount > MaxReenableAttempts)
                            {
                                // changed our mind
                                ssmeStateInfo.RecommendedEnable = false;
                                disable.Add(ssmeStateInfo.SmoothStreamingMediaElement);

                                // since we disable SSME's in a cascading fashion
                                // if index 1 is being turned off, we're disabling all
                                // of the secondary SSMEs - set our flag
                                if (i == 1)
                                {
                                    _recommendPermanentlyDisableAllSecondary = true;
                                }
                            }
                            else
                            {
                                // go ahead and turn this SSME back on
                                ssmeStateInfo.RecommendedEnable = true;
                                enable.Add(ssmeStateInfo.SmoothStreamingMediaElement);
                            }

                            // either way - the rest of you can just chill out
                            disableRest = true;
                        }
                    }

                    if (i == 0)
                    {
                        // this is our first pass through the heuristics loop and it 
                        // is a special one because we treat the primary SSME special.
                        // It is never disabled and if it can't keep up, it can
                        // disable all of the secondary SSMEs
                        enable.Add(ssmeStateInfo.SmoothStreamingMediaElement);
                        ssmeStateInfo.RecommendedEnable = true;

                        bool browserIsMinimized = GetIsMinimized(renderedFPS);

                        // if we're minimized we need to disable our secondary SSMEs
                        if (effectiveBitrate < minBitrateRequested ||
                            renderedFPS < minRenderedFPS ||
                            browserIsMinimized)
                        {
                            // we're not keeping up (or are minimized), disable the other SSMEs
                            disableRest = true;

                            if (browserIsMinimized)
                            {
                                // if the browser is minimized we're disabling the secondary
                                // SSME's to prevent a slideshow, but we don't want
                                // this disabling to count against the ReEnable counts
                                for (int j = 1; j < _ssmeStateInfoList.Count; j++)
                                {
                                    _ssmeStateInfoList[j].DecrementReEnableCount();
                                }
                            }
                        }

                        else
                        {
                            // we're keeping up - evaluating enabling the next SSME
                            enableNext = true;
                        }
                    }
                }

                //raise our event
                RaiseRecommendationChanged(enable, disable);
            }
        }

        private void MatchPlaySpeeds()
        {
            if (Count > 0)
            {
                var master = this[0];

                if (master.PlaybackRate.HasValue)
                {
                    for (int i = 1; i < Count; i++)
                    {
                        this[i].SetPlaybackRate(master.PlaybackRate.Value);
                    }
                }
            }

        }

        // clear reenable count every tick.  
        private void _clearReEnableTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _ssmeStateInfoList.Count; i++)
            {
                SSMEStateInfo ssmeStateInfo = _ssmeStateInfoList[i];

                if (ssmeStateInfo.ReEnableCount >= _maxReEnableAttempts)
                {
                    ssmeStateInfo.ReEnableCount = -1;
                }
            }
        }

        /// <summary>
        /// Updates the AverageRenderedFramesPerSecond for the SSME
        /// </summary>
        /// <param name="ssmeStateInfo"></param>
        private void UpdateAverageFramesPerSecond(SSMEStateInfo ssmeStateInfo)
        {
            double averageFPS = ssmeStateInfo.AverageRenderedFramesPerSecond;
            averageFPS = ((averageFPS + ssmeStateInfo.SmoothStreamingMediaElement.RenderedFramesPerSecond)/2);
            ssmeStateInfo.AverageRenderedFramesPerSecond = averageFPS;
        }

        /// <summary>
        /// Hooks events on our referenced SSME's
        /// </summary>
        private void HookEvents(SmoothStreamingMediaElement ssme)
        {
            ssme.PlaybackTrackChanged += SSME_PlaybackTrackChanged;
            ssme.DownloadTrackChanged += SSME_DownloadTrackChanged;
        }

        /// <summary>
        /// Unhooks events on our referenced SSME's
        /// </summary>
        private void UnhookEvents(SmoothStreamingMediaElement ssme)
        {
            ssme.PlaybackTrackChanged -= SSME_PlaybackTrackChanged;
            ssme.DownloadTrackChanged -= SSME_DownloadTrackChanged;
        }

        /// <summary>
        /// Handles DownloadTrackChanged for our SSMEs
        /// </summary>
        private void SSME_DownloadTrackChanged(object sender, TrackChangedEventArgs e)
        {
            var ssme = sender as SmoothStreamingMediaElement;
            Debug.Assert(ssme != null);

            if (ssme != null)
            {
                // work around a bug in the SSME where it raises events on a non-UI thread
                // TODO: this code can be removed once that bug is fixed
                if (!ssme.Dispatcher.CheckAccess())
                {
                    ssme.Dispatcher.BeginInvoke(() => SSME_DownloadTrackChanged(sender, e));
                    return;
                }

                if (e.StreamType == MediaStreamType.Video)
                {
                    SSMEStateInfo ssmeStateInfo = GetSSMEStateInfoBySSME(ssme);

                    ulong lastDownloadBitrate = e.NewTrack != null ? e.NewTrack.Bitrate : 0;
                    ssmeStateInfo.LastDownloadTrackBitrate = lastDownloadBitrate;
                }
            }
        }

        /// <summary>
        /// Handles PlaybackTrackChanged for our SSMEs
        /// </summary>
        private void SSME_PlaybackTrackChanged(object sender, TrackChangedEventArgs e)
        {
            var ssme =
                sender as SmoothStreamingMediaElement;
            Debug.Assert(ssme != null);

            if (ssme != null)
            {
                // work around a bug in the SSME where it raises events on a non-UI thread
                // TODO: this code can be removed once that bug is fixed
                if (!ssme.Dispatcher.CheckAccess())
                {
                    ssme.Dispatcher.BeginInvoke(() => SSME_PlaybackTrackChanged(sender, e));
                    return;
                }

                if (e.StreamType == MediaStreamType.Video)
                {
                    SSMEStateInfo ssmeStateInfo = GetSSMEStateInfoBySSME(ssme);

                    ulong lastPlaybackBitrate = e.NewTrack != null ? e.NewTrack.Bitrate : 0;
                    ssmeStateInfo.LastPlaybackTrackBitrate = lastPlaybackBitrate;
                }
            }
        }

        /// <summary>
        /// GetSSMEStateInfoThrowIfNotFound
        /// </summary>
        /// <param name="smoothStreamingMediaElement">smoothStreamingMediaElement</param>
        private SSMEStateInfo GetSSMEStateInfoThrowIfNotFound(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            SSMEStateInfo ssmeStateInfo = GetSSMEStateInfoBySSME(smoothStreamingMediaElement);
            if (ssmeStateInfo == null)
            {
                throw new ArgumentException(
                    "smoothStreamingMediaElement is not known to this MultiSmoothStreamingMediaElementHeuristicsManager",
                    "smoothStreamingMediaElement");
            }
            return ssmeStateInfo;
        }

        /// <summary>
        /// Simple helper to see if we have an id 
        /// </summary>
        private bool ContainsSmoothStreamingMediaElement(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            for (int i = 0; i < _ssmeStateInfoList.Count; i++)
            {
                if (ReferenceEquals(_ssmeStateInfoList[i].SmoothStreamingMediaElement,
                                    smoothStreamingMediaElement))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Simple helper to see if we have an SSME 
        /// </summary>
        private SSMEStateInfo GetSSMEStateInfoBySSME(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            for (int i = 0; i < _ssmeStateInfoList.Count; i++)
            {
                if (ReferenceEquals(_ssmeStateInfoList[i].SmoothStreamingMediaElement,
                                    smoothStreamingMediaElement))
                {
                    return _ssmeStateInfoList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Private helper to indicate if the browser is minimized or
        /// occluded.  This helps us determine if SSME.RenderedFramesPerSecond
        /// can be trusted (as it goes to 0 when minimized)
        /// 
        /// TODO: this need to be tried on a variety of OS's in a variety 
        /// of conditions to validate this
        /// </summary>
        private bool GetIsMinimized(double renderedFPS)
        {
            bool isMinimized = false;

            float processLoad = 0;
            float processPercentageOfMachine = 0;
            if (_analytics != null)
            {
                processLoad = _analytics.AverageProcessLoad;
                float machineLoad =
                    _analytics.AverageProcessorLoad;

                processPercentageOfMachine = processLoad/machineLoad;

                if (renderedFPS < _maximumRenderedFpsForMinimizedDetection)
                {
                    // our FPS is suspect... check to see if the process 
                    // load is high, if it's not, we're likely minimized
                    if (processLoad < 30.0 && processPercentageOfMachine > 0.3)
                    {
                        isMinimized = true;
                    }
                }
            }
            return isMinimized;
        }

        #endregion Private Methods and Properties
    }
}