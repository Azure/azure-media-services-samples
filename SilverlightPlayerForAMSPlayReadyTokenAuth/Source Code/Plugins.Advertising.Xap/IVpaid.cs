using System;

namespace Microsoft.Advertising.VPAID
{
    /// <summary>
    /// VPAID 1.1 interface. For more info: http://www.iab.net/media/file/VPAIDFINAL51109.pdf
    /// Players capable of playing ads must implement this interface.
    /// </summary>
    public interface IVpaid
    {
        #region VPAID Methods
        string HandshakeVersion(string version);
        void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables);
        void StartAd();
        void StopAd();
        void ResizeAd(double width, double height, string viewMode);
        void PauseAd();
        void ResumeAd();
        void ExpandAd();
        void CollapseAd();
        #endregion
        #region VPAID Properties
        bool AdLinear { get; }
        bool AdExpanded { get; }
        TimeSpan AdRemainingTime { get; }
        double AdVolume { get; set; }
        #endregion
        #region VPAID Events
        event EventHandler AdLoaded;
        event EventHandler AdStarted;
        event EventHandler AdStopped;
        event EventHandler AdPaused;
        event EventHandler AdResumed;
        event EventHandler AdExpandedChanged;
        event EventHandler AdLinearChanged;
        event EventHandler AdVolumeChanged;
        event EventHandler AdVideoStart;
        event EventHandler AdVideoFirstQuartile;
        event EventHandler AdVideoMidpoint;
        event EventHandler AdVideoThirdQuartile;
        event EventHandler AdVideoComplete;
        event EventHandler AdUserAcceptInvitation;
        event EventHandler AdUserClose;
        event EventHandler AdUserMinimize;
        event EventHandler<ClickThroughEventArgs> AdClickThru;
        event EventHandler<VpaidMessageEventArgs> AdError;
        event EventHandler<VpaidMessageEventArgs> AdLog;
        event EventHandler AdRemainingTimeChange;
        event EventHandler AdImpression;
        #endregion
    }
    #region Event Argument classes

    public class VpaidMessageEventArgs : EventArgs
    {
        public VpaidMessageEventArgs(string Message)
        {
            message = Message;
        }

        readonly string message;
        /// <summary>
        /// The error info or log message to be logged.
        /// </summary>
        public string Message
        {
            get { return message; }
        }
    }

    public class ClickThroughEventArgs : EventArgs
    {
        public ClickThroughEventArgs(string Url, string Id, bool PlayerHandles)
        {
            url = Url;
            id = Id;
            playerHandles = PlayerHandles;
        }

        readonly string url;
        /// <summary>
        /// The url can be used for the ad to specify the click thru url.
        /// </summary>
        public string Url
        {
            get { return url; }
        }

        readonly string id;
        /// <summary>
        /// The ID of the Ad that was clicked. Can be used for tracking purposes.
        /// </summary>
        public string Id
        {
            get { return id; }
        }

        readonly bool playerHandles;
        /// <summary>
        /// If true, the player must handle the event by opening a new browser window to the url. false, the ad will handle the event.
        /// </summary>
        public bool PlayerHandles
        {
            get { return playerHandles; }
        }
    }
    
    #endregion
}
