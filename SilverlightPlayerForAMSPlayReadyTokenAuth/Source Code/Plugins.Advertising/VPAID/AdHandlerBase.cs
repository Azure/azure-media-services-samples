using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Base class for a plugin that handles ads
    /// </summary>
    public abstract class AdHandlerBase : IAdPayloadHandlerPlugin
    {
        public VpaidController VpaidController { get; set; }

        protected IAdHost adHost;
        protected string PluginLogName;

        private Action MediaOpenedAction;
        private bool IsMediaOpened = false;
        private bool isPaused = false;
        private bool isBlocked = false;
        private bool isBlocking = false;

        /// <summary>
        /// Raised when an individual creative fails.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativeFailed;

        /// <summary>
        /// Raised when an individual creative succeeds.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativeSucceeded;

        /// <summary>
        /// Raised when an individual creative is canceled.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativeCanceled;

        /// <summary>
        /// Raised when an individual creative is started.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativeStarted;

        /// <summary>
        /// Raised when the progress changes on an individual creative.
        /// </summary>
        public event EventHandler<CreativeProgressEventArgs> CreativeProgressChanged;

        /// <summary>
        /// Raised when an individual creative is paused.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativePaused;

        /// <summary>
        /// Raised when an individual creative is resumed from a pause.
        /// </summary>
        public event EventHandler<CreativeEventArgs> CreativeResumed;

        public AdHandlerBase()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Represents the player. SMFPlayer implements this but it could really be anything that implements IAdHost.
        /// </summary>
        public IAdHost AdHost
        {
            get { return adHost; }
            set
            {
                if (adHost != null)
                {
                    adHost.VolumeChanged -= adHost_VolumeChanged;
                    adHost.StateChanged -= adHost_StateChanged;
#if !WINDOWS_PHONE && !FULLSCREEN
                    adHost.FullScreenChanged -= adHost_FullScreenChanged;
#endif
                    Player.ContentChanged -= Player_ContentChanged;
                }

                adHost = value;

                if (adHost != null)
                {
                    adHost.VolumeChanged += adHost_VolumeChanged;
                    adHost.StateChanged += adHost_StateChanged;
#if !WINDOWS_PHONE && !FULLSCREEN
                    adHost.FullScreenChanged += adHost_FullScreenChanged;
#endif
                    Player.ContentChanged += Player_ContentChanged;
                }
            }
        }

        /// <summary>
        /// Indicates whether new ads passed to the plugin should be ignored.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// The timeout for initializing/loading ads. Null indicates no timeout.
        /// </summary>
        public TimeSpan? InitTimeout { get { return VpaidController.AdInitTimeout; } set { VpaidController.AdInitTimeout = value; } }

        /// <summary>
        /// The timeout for starting an ad. Null indicates no timeout.
        /// </summary>
        public TimeSpan? StartTimeout { get { return VpaidController.AdStartTimeout; } set { VpaidController.AdStartTimeout = value; } }

        /// <summary>
        /// The timeout for stopping an ad. Null indicates no timeout.
        /// </summary>
        public TimeSpan? StopTimeout { get { return VpaidController.AdStopTimeout; } set { VpaidController.AdStopTimeout = value; } }

        /// <summary>
        /// The failure strategy to use when handling an AdSequencingTrigger.
        /// </summary>
        public FailurePolicyEnum FailurePolicy { get; set; }

        /// <summary>
        /// Indicates that non required creatives (companion ads) should be allowed to continue running after the ad has completed.
        /// </summary>
        public bool CloseCompanionsOnComplete { get; set; }

        /// <summary>
        /// Raised when an AdSequencingTrigger fails. The criteria are based on the FailureStrategy property.
        /// </summary>
        public event EventHandler<HandleCompletedEventArgs> HandleCompleted;

        protected virtual void OnHandleCompleted(HandleCompletedEventArgs eventArgs)
        {
            if (HandleCompleted != null) HandleCompleted(this, eventArgs);
        }

        protected abstract bool CanHandleAd(IAdSource source);

        /// <summary>
        /// This is where it all begins. Calling this method will load the source if the plugin format matches and ads will starting playing in succession.
        /// </summary>
        /// <param name="source">The source for the ad (contains info about the source and targets)</param>
        /// <returns>Returns an object representing the exectuing ad. This object can then be used to deactivate the ad.</returns>
        public IAdPayload Handle(IAdSource source)
        {
            if (!IsEnabled) return null;

            if (!CanHandleAd(source)) return null;

            //assume we need to pull down source docs, block the player from starting a new video until this is done. If a video is already playing, this will not interfere.
            IAsyncAdPayload result = CreatePayload(source);

            ProcessPayload(result, source);

            return result;
        }

        void Player_ContentChanged(object sender, EventArgs e)
        {
            IsMediaOpened = false;
            MediaOpenedAction = null;
            isPaused = false;
            isBlocked = false;
            isBlocking = false;
        }

        void adHost_StateChanged(object sender, EventArgs e)
        {
            if (!IsMediaOpened)
            {
                switch (adHost.PlayState)
                {
                    case MediaPluginState.ClipPlaying:
                    case MediaPluginState.Paused:
                    case MediaPluginState.Playing:
                        OnMediaOpened();
                        break;
                }
            }
        }

        void OnMediaOpened()
        {
            // we can't process a payload until this event fires
            IsMediaOpened = true;
            if (MediaOpenedAction != null)
            {
                MediaOpenedAction();
                MediaOpenedAction = null;
            }
        }

        private readonly object isOpeningBlocker = new object();
        void ProcessPayload(IAsyncAdPayload payload, IAdSource source)
        {
            switch (payload.State)
            {
                case StateEnum.Loading:
                    EventHandler payload_StateChanged = null;
                    payload_StateChanged = (object sender, EventArgs e) =>
                    {
                        payload.StateChanged -= payload_StateChanged;
                        ProcessPayload(payload, source);
                    };
                    payload.StateChanged += payload_StateChanged;
                    break;
                case StateEnum.Ready:
                    ExecuteAd(payload, source);
                    break;
                case StateEnum.Failed:
                    ExecuteAdFailed(source);
                    break;
            }
        }

        private void ExecuteAd(IAsyncAdPayload payload, IAdSource source)
        {
            if (!ExecutePayload(payload, source))
            {
                ExecuteAdFailed(source);
            }
        }

        protected void ExecuteAdFailed(IAdSource source)
        {
            OnHandleCompleted(new HandleCompletedEventArgs(source, false));
        }

        protected abstract IAsyncAdPayload CreatePayload(IAdSource Source);
        protected abstract bool ExecutePayload(IAsyncAdPayload PackagePayload, IAdSource Source);

        #region vPaidController EventHandlers
        protected virtual void vPaidController_AdFailed(object sender, ActiveCreativeEventArgs e)
        {
            AdFailed(e);
        }

        protected virtual void AdFailed(ActiveCreativeEventArgs e)
        {
            VpaidController.RemoveAd(e.ActiveCreative);

            if (CreativeFailed != null)
                CreativeFailed(this, new CreativeEventArgs(e.ActiveCreative.Source));
        }

        protected virtual void vPaidController_AdStopped(object sender, ActiveCreativeEventArgs e)
        {
            AdStopped(e);
        }

        protected virtual void AdStopped(ActiveCreativeEventArgs e)
        {
            VpaidController.RemoveAd(e.ActiveCreative);

            if (CreativeSucceeded != null)
                CreativeSucceeded(this, new CreativeEventArgs(e.ActiveCreative.Source));
        }

        protected virtual void vPaidController_AdLoaded(object sender, ActiveCreativeEventArgs e)
        {
            VpaidController.SetVolume(e.ActiveCreative, adHost.Volume);
            VpaidController.StartAd(e.ActiveCreative, e.UserState);
        }

        protected virtual void vPaidController_AdCompleted(object sender, ActiveCreativeEventArgs e)
        {
            if (e.ActiveCreative.Player.AdLinear)
            {
                // a linear ad just completed, now is a good time to garbage collect
                GC.Collect();
            }
            RefreshAdMode();
        }

        protected virtual void VpaidController_AdStarted(object sender, ActiveCreativeEventArgs e)
        {
            if (CreativeStarted != null)
                CreativeStarted(this, new CreativeEventArgs(e.ActiveCreative.Source));
        }

        protected virtual void VpaidController_AdProgressChanged(object sender, ActiveCreativeEventArgs e)
        {
            // bubble up progress changes
            if (CreativeProgressChanged != null)
                CreativeProgressChanged(this, new CreativeProgressEventArgs(e.ActiveCreative.Source, e.ActiveCreative.Player.AdRemainingTime));
        }

        void VpaidController_AdResumed(object sender, ActiveCreativeEventArgs e)
        {
            // bubble up progress changes
            if (CreativeResumed != null)
                CreativeResumed(this, new CreativeEventArgs(e.ActiveCreative.Source));
        }

        void VpaidController_AdPaused(object sender, ActiveCreativeEventArgs e)
        {
            // bubble up progress changes
            if (CreativePaused != null)
                CreativePaused(this, new CreativeEventArgs(e.ActiveCreative.Source));
        }

        protected virtual void vPaidController_AdIsNotLinear(object sender, ActiveCreativeEventArgs e)
        {
            RefreshAdMode();
        }

        protected virtual void vPaidController_AdIsLinear(object sender, ActiveCreativeEventArgs e)
        {
            RefreshAdMode();
        }

        protected void PlayCreative(ActiveCreative creative, object userState)
        {
            VpaidController.LoadAd(creative, (int)(adHost.PlaybackBitrate / 1024), userState);
            RefreshAdMode();
        }

        protected void CancelCreative(ActiveCreative creative)
        {
            VpaidController.RemoveAd(creative);

            if (CreativeCanceled != null)
                CreativeCanceled(this, new CreativeEventArgs(creative.Source));
        }

        protected void BlockPlayer()
        {
            if (!isBlocked)
            {
                adHost.AdMode = true;

                if (IsPauseRequired()) // not using scheduleclip
                {
                    if (!isPaused)
                    {
                        isPaused = true;
                        PausePlayback();
                    }
                }
                else // using scheduleclip
                {
                    ResumePlayback();
                }
                isBlocked = true;
            }

            // a linear ad is about to start, now is a good time to garbage collect
            GC.Collect();
        }

        protected virtual bool IsPauseRequired()
        {
            var linearPlayers = VpaidController.ActivePlayers.Where(p => p.AdLinear).ToList();
            return (linearPlayers.Any() && !linearPlayers.Any(p => (p is IVpaidLinearBehavior && ((IVpaidLinearBehavior)p).Nonlinear)));
        }

        protected void ReleasePlayer()
        {
            if (isBlocked)
            {
                if (isPaused)
                {
                    ResumePlayback();
                    isPaused = false;
                }

                // release the play block (this will start the player again if a play operation was pending)
                adHost.AdMode = false;
                isBlocked = false;
            }
        }

        protected void ResumePlayback()
        {
            if (IsMediaOpened)
            {
                if (isBlocking)
                {
                    adHost.ReleasePlayBlock(this);
                    isBlocking = false;
                }
                else
                {
                    Player.ActiveMediaPlugin.Play();
                }
                Player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Visible);
            }
            else
            {
                MediaOpenedAction = ResumePlayback;
            }
        }

        protected void PausePlayback()
        {
            Player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Collapsed);

            if (((Microsoft.SilverlightMediaFramework.Core.SMFPlayer)adHost).IsPlayBlocked)
            {
                // if we're already blocked, stick with it.
                AdHost.AddPlayBlock(this);
                isBlocking = true;
            }
            else
            {
                Player.ActiveMediaPlugin.Pause();
            }
        }

        protected IPlayer Player
        {
            get { return (IPlayer)adHost; }
        }

        protected virtual void RefreshAdMode()
        {
            if (VpaidController.ActivePlayers.Any(p => p.AdLinear))    // if any creative sets contain a linear ad, we need to block playback
            {
                BlockPlayer();
            }
            else
            {
                ReleasePlayer();
            }
        }

        private void vPaidController_TrackingFailed(object sender, VpaidController.TrackingFailureEventArgs e)
        {
            SendLogEntry(LogEntryTypes.TrackingFailed, LogLevel.Error, string.Format(VpaidResources.TrackingFailed, e.TrackingEvent, e.Exception.Message));
        }

        private void vPaidController_Log(object sender, ActiveCreativeLogEventArgs e)
        {
            SendLogEntry(LogEntryTypes.VPaidLog, LogLevel.Debug, e.Message);
        }

        #endregion

        #region AdHost EventHandlers

#if !WINDOWS_PHONE && !FULLSCREEN
        private void adHost_FullScreenChanged(object sender, EventArgs e)
        {
            if (adHost.IsFullScreen)
            {
                VpaidController.OnFullscreen();
            }
        }
#endif

        private void adHost_VolumeChanged(object sender, EventArgs e)
        {
            VpaidController.SetVolume(adHost.Volume);
        }
        #endregion

        #region IGenericPlugin

        public event Action<IPlugin, Primitives.LogEntry> LogReady;

        public event Action<IPlugin> PluginLoaded;

        public event Action<IPlugin> PluginUnloaded;

        public event Action<IPlugin, Exception> PluginLoadFailed;

        public event Action<IPlugin, Exception> PluginUnloadFailed;

        bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
        }

        public virtual void Load()
        {
            try
            {
                VpaidController = new VpaidController();

                VpaidController.AdIsLinear += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdIsLinear);
                VpaidController.AdIsNotLinear += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdIsNotLinear);
                VpaidController.Log += new EventHandler<ActiveCreativeLogEventArgs>(vPaidController_Log);
                VpaidController.TrackingFailed += new EventHandler<VpaidController.TrackingFailureEventArgs>(vPaidController_TrackingFailed);
                VpaidController.AdLoaded += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdLoaded);
                VpaidController.AdStopped += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdStopped);
                VpaidController.AdStarted += new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdStarted);
                // Note: all 3 events can use the same handler
                VpaidController.AdFailed += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);
                VpaidController.AdLoadFailed += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);
                VpaidController.AdStartFailed += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);

                VpaidController.AdRemoved += new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdCompleted);
                VpaidController.AdProgressChanged += new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdProgressChanged);
                VpaidController.AdPaused += new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdPaused);
                VpaidController.AdResumed += new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdResumed);

                isLoaded = true;
                PluginLoaded.IfNotNull(i => i(this));
            }
            catch (Exception e)
            {
                PluginLoadFailed.IfNotNull(i => i(this, e));
            }
        }

        public virtual void Unload()
        {
            try
            {
                foreach (var creative in VpaidController.ActiveCreatives.ToList())
                {
                    CancelCreative(creative);
                }

                VpaidController.AdIsLinear -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdIsLinear);
                VpaidController.AdIsNotLinear -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdIsNotLinear);
                VpaidController.Log -= new EventHandler<ActiveCreativeLogEventArgs>(vPaidController_Log);
                VpaidController.TrackingFailed -= new EventHandler<VpaidController.TrackingFailureEventArgs>(vPaidController_TrackingFailed);
                VpaidController.AdLoaded -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdLoaded);
                VpaidController.AdStopped -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdStopped);
                VpaidController.AdStarted -= new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdStarted);
                // Note: all 3 events can use the same handler
                VpaidController.AdFailed -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);
                VpaidController.AdLoadFailed -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);
                VpaidController.AdStartFailed -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdFailed);

                VpaidController.AdRemoved -= new EventHandler<ActiveCreativeEventArgs>(vPaidController_AdCompleted);
                VpaidController.AdProgressChanged -= new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdProgressChanged);
                VpaidController.AdPaused -= new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdPaused);
                VpaidController.AdResumed -= new EventHandler<ActiveCreativeEventArgs>(VpaidController_AdResumed);

                VpaidController = null;
                AdHost = null;

                isLoaded = false;
                PluginUnloaded.IfNotNull(i => i(this));
            }
            catch (Exception e)
            {
                PluginUnloadFailed.IfNotNull(i => i(this, e));
            }
        }

        IEnumerable<IGenericPlugin> plugins;
        public void SetPlugins(IEnumerable<IGenericPlugin> Plugins)
        {
            plugins = Plugins;
        }

        public void SetPlayer(FrameworkElement player)
        {
            AdHost = player as IAdHost;
        }

        #endregion

        public virtual IEnumerable<IVpaidFactory> GetVpaidFactories()
        {
            return AdHost.Plugins.OfType<IVpaidFactory>();
        }

        internal protected void SendLogEntry(string type,
                                  LogLevel severity = LogLevel.Information,
                                  string message = null,
                                  DateTime? timeStamp = null,
                                  IEnumerable<KeyValuePair<string, object>> extendedProperties = null)
        {
            if (LogReady != null)
            {
                var logEntry = new LogEntry
                {
                    Type = type,
                    Severity = severity,
                    Message = message,
                    SenderName = PluginLogName,
                    Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);
                LogReady(this, logEntry);
            }

        }

        internal protected ActiveCreative GetCreative(ICreativeSource adSource, IAdTarget adTarget)
        {
            IVpaid adPlayer;
            var vPaidFactories = GetVpaidFactories().ToList();
            do
            {
                var rankedVpaidFactories =
                from factory in vPaidFactories
                let rank = factory.CheckSupport(adSource, adTarget)
                where rank > PriorityCriteriaEnum.NotSupported
                orderby rank descending
                select factory;

                var playerFactory = rankedVpaidFactories.FirstOrDefault();
                if (playerFactory == null) return null;

                adPlayer = playerFactory.GetVpaidPlayer(adSource, adTarget);

                // handshake with the ad player to make sure the version of VAST is OK
                if (adPlayer == null || !VpaidController.Handshake(adPlayer))
                {
                    // the version is no good, remove the factory from the list and try again.
                    vPaidFactories.Remove(playerFactory);
                    if (!vPaidFactories.Any())
                    {
                        return null;
                    }
                    adPlayer = null;
                }
            } while (adPlayer == null);

            //put companion in target
            if (!adTarget.AddChild(adPlayer))
            {
                return null;
            }

            return new ActiveCreative(adPlayer, adSource, adTarget);
        }


        #region Target operations

        protected class TargetSearchResult
        {
            public TargetSearchResult(IAdSequencingTarget AdSequencingTarget, List<IAdSequencingTarget> DependencyTargets)
            {
                this.AdSequencingTarget = AdSequencingTarget;
                this.DependencyTargets = DependencyTargets;
            }
            public IAdSequencingTarget AdSequencingTarget { get; private set; }
            public List<IAdSequencingTarget> DependencyTargets { get; private set; }
        }

        /// <summary>
        /// Returns all targets that meet a certain condition in order of likelihood.
        /// </summary>
        /// <param name="Targets">The list of targets to test</param>
        /// <param name="Predicate">The condition to test with</param>
        /// <returns>All matching targets in order of priority, along with their dependencies</returns>
        protected IEnumerable<TargetSearchResult> GetAdTargets(IEnumerable<IAdSequencingTarget> Targets, Predicate<IAdSequencingTarget> Predicate)
        {
            var TargetsToSearch = new Queue<TargetSearchResult>(Targets.Select(t => new TargetSearchResult(t, new List<IAdSequencingTarget>())));

            while (TargetsToSearch.Any())
            {
                var Target = TargetsToSearch.Dequeue();
                if (Predicate(Target.AdSequencingTarget))
                {
                    yield return Target;
                }
                if (Target.AdSequencingTarget.Targets != null)
                {
                    foreach (var child in Target.AdSequencingTarget.Targets)
                    {
                        var dependencies = new List<IAdSequencingTarget>(Target.DependencyTargets);
                        dependencies.Add(Target.AdSequencingTarget);
                        TargetsToSearch.Enqueue(new TargetSearchResult(child, dependencies));
                    }
                }
            };
        }

        /// <summary>
        /// Returns all matching targets in order of priority for a given creative source
        /// </summary>
        /// <param name="AdSource">The sequencing source that contains the targets to search</param>
        /// <param name="CreativeSource">The creative that needs to find a target</param>
        /// <returns>All matching targets in order of priority, along with their dependencies</returns>
        protected virtual IEnumerable<TargetSearchResult> GetAdTargets(IAdSource AdSource, ICreativeSource CreativeSource)
        {
            if (AdSource.Targets != null)
            {
                // first, try  to find a target where the type matches the creative's ID
                if (!string.IsNullOrEmpty(CreativeSource.Id))
                {
                    foreach (var item in GetAdTargets(AdSource.Targets, t => t.Type == CreativeSource.Id))
                    {
                        yield return item;
                    }
                }

                // next, try to find a target where the type matches the creative's type, give priority to the parents on only look at the children if a target was not found
                foreach (var item in GetAdTargets(AdSource.Targets, t => t.Type.ToLower() == CreativeSource.Type.ToString().ToLower()))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Finds the Silverlight FrameworkElement associated with the target.
        /// </summary>
        /// <param name="Target">The target for a given ad creative</param>
        /// <returns>The FrameworkElement cooresponding to the target</returns>
        protected virtual FrameworkElement GetAdContainer(IAdSequencingTarget Target)
        {
            if (Target.Region == TargetRegions.VideoArea)
            {
                return adHost.VideoArea;
            }
            else
            {
                return adHost.Containers.FirstOrDefault(c => c.Name == Target.Region);
            }
        }

#if !WINDOWS_PHONE && !OOB
        /// <summary>
        /// Finds the HTMLElement associated with the target
        /// </summary>
        /// <param name="Target">The target for a given ad creative</param>
        /// <returns>The HTMLElement cooresponding to the target</returns>
        protected virtual System.Windows.Browser.HtmlElement GetHtmlAdContainer(IAdSequencingTarget Target)
        {
            return System.Windows.Browser.HtmlPage.Document.GetElementById(Target.Region);
        }
#endif

        /// <summary>
        /// Finds and creates a target from an ad sequencing source and a creative source.
        /// </summary>
        /// <param name="AdSource">Provides required target info</param>
        /// <param name="CreativeSource">Provides info about the actual creative that is to be placed.</param>
        /// <returns>And object representing the selected target of the creative</returns>
        public virtual IAdTarget FindTarget(IAdSource AdSource, ICreativeSource CreativeSource)
        {
            var target = GetAdTargets(AdSource, CreativeSource).FirstOrDefault();
            if (target == null && CreativeSource.Type != CreativeSourceType.Companion)
            {
                // assume VideoArea for linear and nonlinear ads
                target = new TargetSearchResult((IAdSequencingTarget)new VideoAreaTarget(CreativeSource), new List<IAdSequencingTarget>());
            }

            if (target != null)
            {
                var slResult = GetAdContainer(target.AdSequencingTarget);
#if !WINDOWS_PHONE && !OOB
                if (slResult == null && !Application.Current.IsRunningOutOfBrowser)
                {
                    var htmlResult = GetHtmlAdContainer(target.AdSequencingTarget);
                    if (htmlResult != null)
                    {
                        return new HtmlElementAdTarget(htmlResult, target.AdSequencingTarget, target.DependencyTargets);
                    }
                }
                else
#endif
                {
                    return CreateAdTarget(slResult, target.AdSequencingTarget, target.DependencyTargets);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a target object to store info about a chosen target to place a creative in.
        /// </summary>
        /// <param name="Element">The FrameworkElement that the ad creative will be place in.</param>
        /// <param name="TargetSource">The sequencing target that was selected from the ad source</param>
        /// <param name="TargetDependencies">The other dependency targets. These will be looked at later to make sure all dependency targets have been used before playing the ad.</param>
        /// <returns></returns>
        protected virtual IAdTarget CreateAdTarget(FrameworkElement Element, IAdSequencingTarget TargetSource, IList<IAdSequencingTarget> TargetDependencies)
        {
            if (Element is Panel)
                return new PanelAdTarget(Element as Panel, TargetSource, TargetDependencies);
            else if (Element is ContentControl)
                return new ControlAdTarget(Element as ContentControl, TargetSource, TargetDependencies);
            else if (Element is Border)
                return new BorderAdTarget(Element as Border, TargetSource, TargetDependencies);
            else
                return null;
        }
        #endregion


        /// <summary>
        /// Serves as an ad target for the VideoArea. The VideoArea is the default target for linear and nonlinear ads so it won't always be provided in the list of targets, in which case, this class will do the job.
        /// </summary>
        public class VideoAreaTarget : IAdSequencingTarget
        {
            internal VideoAreaTarget(ICreativeSource CreativeSource)
            {
                Region = TargetRegions.VideoArea;
                Type = CreativeSource.Type.ToString();
            }

            public string Region { get; set; }
            public string Type { get; set; }
            public IEnumerable<IAdSequencingTarget> Targets
            {
                get { return Enumerable.Empty<IAdSequencingTarget>(); }
            }
        }
    }
}
