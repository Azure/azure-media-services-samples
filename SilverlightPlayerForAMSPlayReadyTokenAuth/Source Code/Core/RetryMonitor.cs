using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Provides functionality to handle retrying logic when media fails to play. 
    /// </summary>
    /// <remarks>
    /// Retrying begins after a MediaFailed event occurs.
    /// The RetryMonitor sets the Player IsRetrying property.
    /// An application can check the <c>Player.IsRetrying</c> property or databind to that property to change the appearance of the player in retrying mode. 
    /// </remarks>
    internal class RetryMonitor : IDisposable
    {
        private bool _isMediaSourceAdaptive;
        private TimeSpan _lastPosition;
        private IMediaPlugin _mediaPlugin;
        private Uri _mediaSource;
        private DispatcherTimer _giveupTimer;
        private DispatcherTimer _successTimer;
        private DispatcherTimer _waitTimer;
        private bool exitOnFail;

        public RetryMonitor()
        {
            _giveupTimer = new DispatcherTimer();
            _giveupTimer.Tick += GiveupTimer_Tick;

            _successTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            _successTimer.Tick += successTimer_Tick;

            _waitTimer = new DispatcherTimer();
            _waitTimer.Tick += WaitTimer_Tick;
        }

        /// <summary>
        /// Gets a value indicating whether the Player is currently in retry mode.
        /// </summary>
        public bool IsRetrying { get; private set; }

        internal TimeSpan LastPosition
        {
            get { return _lastPosition; }
            set
            {
                if (!IsRetrying) _lastPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets a media plugin.
        /// </summary>
        /// <remarks>
        /// If a connection is re-established, the IsRetrying property is set to false.
        /// </remarks>
        public IMediaPlugin MediaPlugin
        {
            get { return _mediaPlugin; }
            set
            {
                CancelRetrying();
                _mediaPlugin = value;
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Releases the resources used by the RetryMonitor.
        /// </summary>
        public void Dispose()
        {
            _successTimer.Tick -= successTimer_Tick;
            if (_successTimer.IsEnabled)
            {
                _successTimer.Stop();
            }

            _waitTimer.Tick -= WaitTimer_Tick;
            if (_waitTimer.IsEnabled)
            {
                _waitTimer.Stop();
            }

            if (_giveupTimer != null)
            {
                _giveupTimer.Tick -= GiveupTimer_Tick;
                _giveupTimer.Stop();
                UnregisterMediaPlugin();
                _mediaPlugin = null;
                _mediaSource = null;
            }
        }

        #endregion

        public event Action<RetryMonitor> RetryStarted;
        public event Action<RetryMonitor> RetrySuccessful;
        public event Action<RetryMonitor> RetryFailed;
        public event Action<RetryMonitor> RetryCancelled;
        public event Action<RetryMonitor, Exception> RetryAttemptFailed;

        /// <summary>
        /// Begins retrying to re-establish connectivity to the media source.
        /// </summary>
        /// <param name="progressiveMediaSource">Location of the media file (for progressive download only)</param>
        /// <param name="isMediaSourceAdaptive">Indicates whether media is adaptive (the default is false).</param>
        /// <remarks>
        /// 
        /// </remarks>
        public void BeginRetrying(Uri progressiveMediaSource = null, bool isMediaSourceAdaptive = false)
        {
            BeginRetrying(TimeSpan.MaxValue, TimeSpan.Zero, progressiveMediaSource, isMediaSourceAdaptive);
        }

        public void BeginRetrying(TimeSpan timeout, TimeSpan waitInterval, Uri mediaSource = null, bool isMediaSourceAdaptive = false)
        {
            if (_mediaPlugin == null)
            {
                throw new InvalidOperationException(
                    SilverlightMediaFrameworkResources.RetryMonitorMediaPluginCannotBeNullMessage);
            }

            CancelRetrying();
            IsRetrying = true;
            exitOnFail = false;
            RegisterMediaPlugin();
            _mediaSource = mediaSource;

            _isMediaSourceAdaptive = isMediaSourceAdaptive;
            _waitTimer.Interval = waitInterval;
            SetMediaSource();
            _giveupTimer.Interval = timeout;
            _giveupTimer.Start();
            RetryStarted.IfNotNull(i => i(this));
        }

        /// <summary>
        /// Cancels retrying to re-establish a connection.
        /// </summary>
        public void CancelRetrying()
        {
            if (IsRetrying)
            {
                StopRetrying();
                RetryCancelled.IfNotNull(i => i(this));
            }
        }

        private void MediaPlugin_MediaFailed(IMediaPlugin mediaPlugin, Exception error)
        {
            if (_successTimer.IsEnabled)
            {
                _successTimer.Stop();
            }

            if (!exitOnFail)
            {
                RetryAttemptFailed.IfNotNull(i => i(this, error));
                // start up a timer to try again
                _waitTimer.Start();
            }
            else
            {
                exitOnFail = false;
                RetryFailed.IfNotNull(i => i(this));
                Deployment.Current.Dispatcher.BeginInvoke(StopRetrying);
            }
        }

        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            _waitTimer.IfNotNull(i => i.Stop());

            if (IsRetrying)
            {
                SetMediaSource();
            }
            else
            {
                StopRetrying();
                RetryFailed.IfNotNull(i => i(this));
            }
        }

        private void MediaPlugin_MediaEnded(IMediaPlugin mediaPlugin)
        {
            StopRetrying();
            RetrySuccessful.IfNotNull(i => i(this));
        }

        private void MediaPlugin_MediaOpened(IMediaPlugin mediaPlugin)
        {
            // restore state of player
            var livePlugin = _mediaPlugin as ILiveDvrMediaPlugin;
            if (livePlugin != null && livePlugin.IsSourceLive && LastPosition == TimeSpan.Zero)
            {
                // go to live if a live feed and don't have a last position,
                // most likely this is the result of the first load attempt
                livePlugin.Position = livePlugin.LivePosition;
                //livePlugin.SeekToLive();
            }
            else
            {
                // restore position		
                _mediaPlugin.Position = LastPosition;
            }

            //Per IIS CP team, we set auto play = true so that the video will start playing when it 
            //recovers from the retry.  Now that it has recovered, we will set it back to false.
            _mediaPlugin.AutoPlay = false;

            if (!_successTimer.IsEnabled)
            {
                _successTimer.Start();
            }
        }

        void successTimer_Tick(object sender, EventArgs e)
        {
            if (_mediaPlugin.Position > LastPosition)
            {
                StopRetrying();
                RetrySuccessful.IfNotNull(i => i(this));
            }
        }

        private void StopRetrying()
        {
            if (_giveupTimer.IsEnabled)
            {
                _giveupTimer.Stop();
            }
            if (_successTimer.IsEnabled)
            {
                _successTimer.Stop();
            }

            UnregisterMediaPlugin();
            IsRetrying = false;
        }

        private void SetMediaSource()
        {
            var newMediaSource = new Uri(_mediaSource.AbsoluteUri);

            if (!_isMediaSourceAdaptive)
            {
                _mediaPlugin.Source = newMediaSource;
            }
            else
            {
                var adaptiveMediaPlugin = _mediaPlugin as IAdaptiveMediaPlugin;
                if (adaptiveMediaPlugin != null)
                {
                    adaptiveMediaPlugin.AdaptiveSource = null;
                    adaptiveMediaPlugin.AutoPlay = true;
                    adaptiveMediaPlugin.AdaptiveSource = newMediaSource;
                }
            }
        }

        private void RegisterMediaPlugin()
        {
            if (_mediaPlugin != null)
            {
                _mediaPlugin.MediaOpened += MediaPlugin_MediaOpened;
                _mediaPlugin.MediaEnded += MediaPlugin_MediaEnded;
                _mediaPlugin.MediaFailed += MediaPlugin_MediaFailed;
            }
        }

        private void UnregisterMediaPlugin()
        {
            if (_mediaPlugin != null)
            {
                _mediaPlugin.MediaOpened -= MediaPlugin_MediaOpened;
                _mediaPlugin.MediaEnded -= MediaPlugin_MediaEnded;
                _mediaPlugin.MediaFailed -= MediaPlugin_MediaFailed;
            }
        }

        private void GiveupTimer_Tick(object sender, EventArgs e)
        {
            _giveupTimer.Stop();

            if (_waitTimer.IsEnabled)
            {
                _waitTimer.Stop();
                RetryFailed.IfNotNull(i => i(this));
                StopRetrying();
            }
            else
            {
                exitOnFail = true;
            }
        }
    }
}