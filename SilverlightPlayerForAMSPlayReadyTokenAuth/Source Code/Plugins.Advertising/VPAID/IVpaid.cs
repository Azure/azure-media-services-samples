using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
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

    public abstract class VpaidMessageEventArgs : EventArgs
    {
        public abstract string Message { get; }
    }

    public abstract class ClickThroughEventArgs : EventArgs
    {
        public abstract string Url { get; }
        public abstract string Id { get; }
        public abstract bool PlayerHandles { get; }
    }
    #endregion
}
