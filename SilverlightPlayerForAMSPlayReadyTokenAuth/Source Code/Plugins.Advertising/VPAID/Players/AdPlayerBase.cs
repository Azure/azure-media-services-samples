using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A reusable abstract base class to help build VPAID ad players
    /// </summary>
    public abstract class AdPlayerBase : ClickableAdPlayerHost, IVpaid, IDisposable
    {
        /// <summary>
        /// Creates a new instance of AdPlayerBase.
        /// </summary>
        /// <param name="Dimensions">The dimensions of the ad to play</param>
        /// <param name="Scalable">Whether or not the ad is scalable</param>
        /// <param name="Linear">Whether or not the ad is linear</param>
        public AdPlayerBase(Size Dimensions, bool Scalable, bool Linear)
        {
            this.Dimensions = Dimensions;
            this.Scalable = Scalable;
            this.Linear = Linear;
        }

        /// <summary>
        /// Indicates the desired startup bitrate (based on the current bitrate of the video when the ad started).
        /// </summary>
        public int DesiredBitrate { get; private set; }

        /// <summary>
        /// Indicates whether or not the ad is linear
        /// </summary>
        public bool Linear { get; private set; }

        /// <summary>
        /// Returns the dimensions of the ad
        /// </summary>
        public Size Dimensions { get; private set; }

        /// <summary>
        /// Indicates that the ad is scalable.
        /// </summary>
        public bool Scalable { get; private set; }

        protected FrameworkElement ContentsElement;
        protected abstract FrameworkElement CreateContentsElement();
        public bool IsResumed { get; protected set; }
        public bool IsPaused { get; protected set; }

        #region VPAID Events
        protected virtual void OnAdLoaded() { if (AdLoaded != null) AdLoaded(this, EventArgs.Empty); }
        public event EventHandler AdLoaded;

        protected virtual void OnAdStarted() { if (AdStarted != null) AdStarted(this, EventArgs.Empty); }
        public event EventHandler AdStarted;

        protected virtual void OnAdStopped() { if (AdStopped != null) AdStopped(this, EventArgs.Empty); }
        public event EventHandler AdStopped;

        protected virtual void OnAdPaused() { if (AdPaused != null) AdPaused(this, EventArgs.Empty); }
        public event EventHandler AdPaused;

        protected virtual void OnAdResumed() { if (AdResumed != null) AdResumed(this, EventArgs.Empty); }
        public event EventHandler AdResumed;

        protected virtual void OnAdExpandedChanged() { if (AdExpandedChanged != null) AdExpandedChanged(this, EventArgs.Empty); }
        public event EventHandler AdExpandedChanged;

        protected virtual void OnAdLinearChanged() { if (AdLinearChanged != null) AdLinearChanged(this, EventArgs.Empty); }
        public event EventHandler AdLinearChanged;

        protected virtual void OnAdResizeRequest() { if (AdResizeRequest != null) AdResizeRequest(this, EventArgs.Empty); }
        public event EventHandler AdResizeRequest;

        protected virtual void OnAdResized() { if (AdResized != null) AdResized(this, EventArgs.Empty); }
        public event EventHandler AdResized;

        protected virtual void OnAdVolumeChanged() { if (AdVolumeChanged != null) AdVolumeChanged(this, EventArgs.Empty); }
        public event EventHandler AdVolumeChanged;

        protected virtual void OnAdVideoStart() { if (AdVideoStart != null) AdVideoStart(this, EventArgs.Empty); }
        public event EventHandler AdVideoStart;

        private bool AdVideoFirstQuartileFired;
        protected virtual void OnAdVideoFirstQuartile()
        {
            if (!AdVideoFirstQuartileFired && AdVideoFirstQuartile != null)
            {
                AdVideoFirstQuartile(this, EventArgs.Empty);
                AdVideoFirstQuartileFired = true;
            }
        }
        public event EventHandler AdVideoFirstQuartile;

        private bool AdVideoMidpointFired;
        protected virtual void OnAdVideoMidpoint()
        {
            if (!AdVideoMidpointFired && AdVideoMidpoint != null)
            {
                AdVideoMidpoint(this, EventArgs.Empty);
                AdVideoMidpointFired = true;
            }
        }
        public event EventHandler AdVideoMidpoint;

        private bool AdVideoThirdQuartileFired;
        protected virtual void OnAdVideoThirdQuartile()
        {
            if (!AdVideoThirdQuartileFired && AdVideoThirdQuartile != null)
            {
                AdVideoThirdQuartile(this, EventArgs.Empty);
                AdVideoThirdQuartileFired = true;
            }
        }
        public event EventHandler AdVideoThirdQuartile;

        private bool AdVideoCompleteFired;
        protected virtual void OnAdVideoComplete()
        {
            if (!AdVideoCompleteFired && AdVideoComplete != null)
            {
                AdVideoComplete(this, EventArgs.Empty);
                AdVideoCompleteFired = true;
            }
        }
        public event EventHandler AdVideoComplete;

        protected virtual void OnAdUserAcceptInvitation() { if (AdUserAcceptInvitation != null) AdUserAcceptInvitation(this, EventArgs.Empty); }
        public event EventHandler AdUserAcceptInvitation;

        protected virtual void OnAdRemainingTimeChange() { if (AdRemainingTimeChange != null) AdRemainingTimeChange(this, EventArgs.Empty); }
        public event EventHandler AdRemainingTimeChange;

        protected virtual void OnAdImpression() { if (AdImpression != null) AdImpression(this, EventArgs.Empty); }
        public event EventHandler AdImpression;

        protected virtual void OnAdUserClose() { if (AdUserClose != null) AdUserClose(this, EventArgs.Empty); }
        public event EventHandler AdUserClose;

        protected virtual void OnAdUserMinimize() { if (AdUserMinimize != null) AdUserMinimize(this, EventArgs.Empty); }
        public event EventHandler AdUserMinimize;

        protected virtual void OnAdClickThru(ClickThroughEventArgs e) { if (AdClickThru != null) AdClickThru(this, e); }
        public event EventHandler<ClickThroughEventArgs> AdClickThru;

        protected virtual void OnAdError(AdMessageEventArgs e) { if (AdError != null) AdError(this, e); }
        public event EventHandler<VpaidMessageEventArgs> AdError;

        protected virtual void OnAdLog(VpaidMessageEventArgs e) { if (AdLog != null) AdLog(this, e); }
        public event EventHandler<VpaidMessageEventArgs> AdLog;
        #endregion

        #region VPAID Properties / Methods

        public virtual string HandshakeVersion(string version)
        {
            return "1.1";
        }

        public virtual void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            Init(width, height, viewMode, desiredBitrate, creativeData, environmentVariables);
            OnAdLoaded();
        }

        protected virtual void Init(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            if (ContentsElement == null)
            {
                ContentsElement = CreateContentsElement();
                IsHitTestVisible = true;
                this.Cursor = Cursors.Hand;

                // make linear ads opaque
                if (Linear && ContentsElement != null)
                {
                    Background = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    Background = new SolidColorBrush(Colors.Transparent);
                }

                DesiredBitrate = desiredBitrate;
                Content = ContentsElement;
                ResizeAd(width, height, viewMode);
            }
        }

        public virtual void ResizeAd(double width, double height, string viewMode)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;

            if (ContentsElement != null)    // null is legit... and used in the case of html based ads
            {
                if (Scalable || Dimensions.IsEmpty)
                {
                    ContentsElement.Width = width;
                    ContentsElement.Height = height;
                }
                else
                {
                    ContentsElement.Width = Dimensions.Width;
                    ContentsElement.Height = Dimensions.Height;
                }
            }
        }

        bool isExpanded = false;
        public virtual bool AdExpanded
        {
            get { return isExpanded; }
        }

        public virtual TimeSpan AdRemainingTime
        {
            get
            {
                // -2 means unknown
                return TimeSpan.FromSeconds(-2);
            }
        }

        public bool AdLinear { get { return Linear; } }

        public virtual double AdVolume { get; set; }

        public virtual void StartAd()
        {
            Start();
            OnAdStarted();
            OnAdImpression();
        }

        protected void Start()
        {
            // nothing to do here
        }

        public virtual void StopAd()
        {
            Stop();
            OnAdStopped();
        }

        public virtual void Stop()
        {
            // nothing to do here
        }

        public virtual void ResumeAd()
        {
            Resume();
            OnAdResumed();
        }

        public virtual void Resume()
        {
            IsPaused = false;
            IsResumed = true;
        }

        public virtual void PauseAd()
        {
            Pause();
            OnAdPaused();
        }

        public virtual void Pause()
        {
            IsPaused = true;
        }

        public virtual void ExpandAd()
        {
            isExpanded = true;
            OnAdExpandedChanged();
        }

        public virtual void CollapseAd()
        {
            isExpanded = false;
            OnAdExpandedChanged();
        }

        #endregion

        public virtual void Dispose()
        {
            if (ContentsElement != null)
            {
                ContentsElement = null;
            }
        }
    }
}
