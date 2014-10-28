using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A VPAID ad player that can play cips using an existing IMediaPlugin object retrieved from IPlayer.ActiveMediaPlugin using the ScheduleAd method
    /// </summary>
    public class AdClipLinearAdPlayer : AdPlayerBase, IVpaidLinearBehavior
    {
        internal AdClipLinearAdPlayer(ICreativeSource AdSource, IAdTarget AdTarget, IPlayer AdHost)
            : base(AdSource.Dimensions, AdSource.IsScalable, true)
        {
            this.AdSource = AdSource;
            this.AdTarget = AdTarget;
            this.ActiveMediaPlugin = AdHost.ActiveMediaPlugin;
            this.AdHost = AdHost;
            if (AdSource.ClickUrl != null)
            {
                this.NavigateUri = new Uri(AdSource.ClickUrl, UriKind.RelativeOrAbsolute);
            }
        }

        readonly IMediaPlugin ActiveMediaPlugin;
        readonly IAdTarget AdTarget;
        readonly ICreativeSource AdSource;
        readonly IPlayer AdHost;

        private IAdContext CurrentAdContext;
        private DateTime startTime;
        private bool isLoaded;
        private bool isStarted;

        protected override void OnClick()
        {
            OnAdClickThru(new AdClickThruEventArgs(AdSource.ClickUrl, AdSource.Id, false));
            base.OnClick();
        }

        public override TimeSpan AdRemainingTime
        {
            get
            {
                if (CurrentAdContext.NaturalDuration.HasValue)
                    return DateTime.Now.Subtract(startTime);
                else
                    return TimeSpan.Zero;
            }
        }

        protected override FrameworkElement CreateContentsElement()
        {
            return null;
        }

        public override void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            if (ActiveMediaPlugin.SupportsAdScheduling)
            {
                // warning: we need to dispatch or app will crash if we are running this synchronously after a previous clip failed.
                // looks like the SSME needs time to deal with the failure.
                // Update: add the error to the dispatcher instead
                var MediaSource = new Uri(creativeData);
                var mimeType = AdSource.MimeType.ToLower();
                var DeliveryMethod = mimeType == "video/x-ms-wmv" || mimeType == "video/mp4" ? DeliveryMethods.ProgressiveDownload : DeliveryMethods.AdaptiveStreaming;
                var isLive = (ActiveMediaPlugin is ILiveDvrMediaPlugin && ((ILiveDvrMediaPlugin)ActiveMediaPlugin).IsSourceLive);
                ActiveMediaPlugin.CurrentStateChanged += new Action<IMediaPlugin, MediaPluginState>(ActiveMediaPlugin_CurrentStateChanged);
                ActiveMediaPlugin.AdProgressUpdated += new Action<IAdaptiveMediaPlugin, IAdContext, AdProgress>(ActiveMediaPlugin_AdProgressUpdated);
                ActiveMediaPlugin.AdError += new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdError);
                ActiveMediaPlugin.AdStateChanged += new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdStateChanged);
                //ActiveMediaPlugin.AdClickThrough += new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdClickThrough);
                CurrentAdContext = AdHost.PlayLinearAd(MediaSource, DeliveryMethod, null, null, AdSource.ClickUrl == null ? null : new Uri(AdSource.ClickUrl, UriKind.RelativeOrAbsolute), AdSource.Duration, !isLive, null, null);
                base.Init(width, height, viewMode, desiredBitrate, creativeData, environmentVariables);
            }
            else
            {
                OnAdError(new AdMessageEventArgs("ActiveMediaPlugin does not support ad scheduling"));
            }
        }

        // dispatching errors so the ssme can catch up. see note in InitAd
        protected override void OnAdError(AdMessageEventArgs e)
        {
            Dispatcher.BeginInvoke(() => base.OnAdError(e));
        }

        void ActiveMediaPlugin_AdClickThrough(IAdaptiveMediaPlugin mediaPlugin, IAdContext adContext)
        {
            OnAdClickThru(new AdClickThruEventArgs(adContext.ClickThrough.OriginalString, AdSource.Id, true));
        }

        void ActiveMediaPlugin_AdProgressUpdated(IAdaptiveMediaPlugin mp, IAdContext adContext, AdProgress progress)
        {
            switch (progress)
            {
                case AdProgress.Start:
                    startTime = DateTime.Now;
                    OnAdVideoStart();
                    break;
                case AdProgress.FirstQuartile:
                    OnAdVideoFirstQuartile();
                    break;
                case AdProgress.Midpoint:
                    OnAdVideoMidpoint();
                    break;
                case AdProgress.ThirdQuartile:
                    OnAdVideoThirdQuartile();
                    break;
                case AdProgress.Complete:
                    OnAdVideoComplete();
                    break;
            }
        }

        void ActiveMediaPlugin_AdError(IAdaptiveMediaPlugin mp, IAdContext adContext)
        {
            OnAdError(new AdMessageEventArgs("An unknown error occured while playing the ad clip."));
        }

        void ActiveMediaPlugin_AdStateChanged(IAdaptiveMediaPlugin arg1, IAdContext arg2)
        {
            switch (CurrentAdContext.CurrentAdState)
            {
                case MediaPluginState.Paused:
                case MediaPluginState.Buffering:
                    if (!isLoaded)
                    {
                        OnAdLoaded();
                        isLoaded = true;
                    }
                    break;
                case MediaPluginState.Playing:
                    if (!isStarted)
                    {
                        OnAdImpression();
                        OnAdStarted();
                        isStarted = true;
                    }
                    break;
            }
        }

        void ActiveMediaPlugin_CurrentStateChanged(IMediaPlugin mp, MediaPluginState state)
        {
            switch (state)
            {
                case MediaPluginState.Paused:
                    if (CurrentAdContext.CurrentAdState == MediaPluginState.Closed)
                    {
                        OnAdStopped();
                    }
                    break;
            }
        }

        public override double AdVolume
        {
            get
            {
                return ActiveMediaPlugin.Volume * 100;
            }
            set
            {
                ActiveMediaPlugin.Volume = value / 100.0;
            }
        }

        public override void StartAd()
        {
            base.Start();
        }

        public override void ResumeAd()
        {
            ActiveMediaPlugin.Play();
            base.Resume();
        }

        public override void PauseAd()
        {
            ActiveMediaPlugin.Pause();
            base.Pause();
        }

        public override void Dispose()
        {
            if (ActiveMediaPlugin != null)
            {
                //ActiveMediaPlugin.AdClickThrough -= new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdClickThrough);
                ActiveMediaPlugin.CurrentStateChanged -= new Action<IMediaPlugin, MediaPluginState>(ActiveMediaPlugin_CurrentStateChanged);
                ActiveMediaPlugin.AdProgressUpdated -= new Action<IAdaptiveMediaPlugin, IAdContext, AdProgress>(ActiveMediaPlugin_AdProgressUpdated);
                ActiveMediaPlugin.AdError -= new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdError);
                ActiveMediaPlugin.AdStateChanged -= new Action<IAdaptiveMediaPlugin, IAdContext>(ActiveMediaPlugin_AdStateChanged);
            }

            base.Dispose();
        }

        public bool Nonlinear
        {
            get { return true; }
        }
    }
}
