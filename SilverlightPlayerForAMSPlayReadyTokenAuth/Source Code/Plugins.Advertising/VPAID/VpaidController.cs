using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
#if !WINDOWS_PHONE && !FULLSCREEN
using System.Windows.Browser;
#endif

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A controller that is capable of playing multiple VPAID ads simultaneously.
    /// </summary>
    public class VpaidController
    {
        /// <summary>
        /// The timeout for initializing/loading ads. AdLoadFailed will fire if the timeout is exceeded.
        /// </summary>
        public TimeSpan? AdInitTimeout { get; set; }

        /// <summary>
        /// The timeout for starting an ad. AdStartFailed will fire if the timeout is exceeded.
        /// </summary>
        public TimeSpan? AdStartTimeout { get; set; }

        /// <summary>
        /// The timeout for stopping an ad. AdStopFailed will fire if the timeout is exceeded.
        /// </summary>
        public TimeSpan? AdStopTimeout { get; set; }

        /// <summary>
        /// The ad has changed to linear mode.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdIsLinear;

        /// <summary>
        /// The ad has changed to nonlinear mode.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdIsNotLinear;

        /// <summary>
        /// The ad failed to load due to a timeout.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdLoadFailed;

        /// <summary>
        /// The ad failed to start due to a timeout.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdStartFailed;

        /// <summary>
        /// The ad failed to stop due to a timeout.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdStopFailed;

        /// <summary>
        /// The ad successfully to loaded.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdLoaded;

        /// <summary>
        /// The ad successfully to started.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdStarted;

        /// <summary>
        /// The ad successfully to stopped.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdStopped;

        /// <summary>
        /// The ad failed for some reason.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdFailed;

        /// <summary>
        /// The ad was removed and destroyed.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdRemoved;

        /// <summary>
        /// The ad was paused.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdPaused;

        /// <summary>
        /// The ad was resumed from a pause.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdResumed;

        /// <summary>
        /// The ad player is requesting to log something.
        /// </summary>
        public event EventHandler<ActiveCreativeLogEventArgs> Log;

        /// <summary>
        /// A tracking event failed usually due to a loss of internet or a bad tracking URL.
        /// </summary>
        public event EventHandler<TrackingFailureEventArgs> TrackingFailed;

        /// <summary>
        /// The progress of the ad has changed.
        /// </summary>
        public event EventHandler<ActiveCreativeEventArgs> AdProgressChanged;

        private readonly Dictionary<IVpaid, ActiveCreative> activeAds = new Dictionary<IVpaid, ActiveCreative>();
        private readonly Dictionary<ActiveCreative, object> userStates = new Dictionary<ActiveCreative, object>();

        private static Version HandlerVersion = new Version("1.1");

        public IEnumerable<IVpaid> ActivePlayers
        {
            get
            {
                return activeAds.Select(a => a.Key);
            }
        }

        public IEnumerable<ActiveCreative> ActiveCreatives
        {
            get
            {
                return activeAds.Select(a => a.Value);
            }
        }

        /// <summary>
        /// Passes on player volume to all active ads
        /// </summary>
        /// <param name="VolumeLevel"></param>
        public void SetVolume(double VolumeLevel)
        {
            uint vPaidVolume = (uint)(VolumeLevel * 100);
            foreach (IVpaid adPlayer in activeAds.Keys)
            {
                try
                {
                    adPlayer.AdVolume = vPaidVolume;
                }
                catch (Exception ex)
                {
                    OnLog(new ActiveCreativeLogEventArgs(activeAds[adPlayer], "VPAID.AdVolume.Set Exception: " + ex.Message));
                }
            }
        }

        /// <summary>
        /// Passes on player volume to a single ad
        /// </summary>
        /// <param name="VolumeLevel"></param>
        public void SetVolume(ActiveCreative activeCreative, double VolumeLevel)
        {
            uint vPaidVolume = (uint)(VolumeLevel * 100);
            try
            {
                activeCreative.Player.AdVolume = vPaidVolume;
            }
            catch (Exception ex)
            {
                OnLog(new ActiveCreativeLogEventArgs(activeCreative, "VPAID.AdVolume.Set Exception: " + ex.Message));
            }
        }

        /// <summary>
        /// Loads a creative. This causes VPAID.InitAd to execute.
        /// </summary>
        /// <param name="ad">The creative to load</param>
        /// <param name="bitrate">The current bitrate of the player. This is passed onto the VPAID player which can use it to initialize itself.</param>
        /// <param name="userState">A user state associated with this ad. This will be included with the various events associated with this ad.</param>
        public void LoadAd(ActiveCreative ad, int bitrate, object userState)
        {
            if (!activeAds.ContainsKey(ad.Player))
            {
                activeAds.Add(ad.Player, ad);
                HookupPlayer(ad.Player);
            }

            userStates.Add(ad, userState);

            Size adContainerSize = ad.Target.Size;

            TimeoutHelper<EventArgs> InitAdTimeoutHandler = new TimeoutHelper<EventArgs>();
            ad.Player.AdLoaded += InitAdTimeoutHandler.Complete;
            EventHandler<VpaidMessageEventArgs> failHandler = (s, e) => InitAdTimeoutHandler.Failed(new Exception(e.Message));
            ad.Player.AdError -= player_AdError;
            ad.Player.AdError += failHandler;

            InitAdTimeoutHandler.Completed += (s, e) =>
                {
                    ad.Player.AdLoaded -= InitAdTimeoutHandler.Complete;
                    ad.Player.AdError -= failHandler;
                    if (e.Error == null)
                    {
                        ad.Player.AdError += player_AdError;
                        player_AdLoaded(ad.Player, e.Result);
                    }
                    else
                        if (AdLoadFailed != null) AdLoadFailed(this, new ActiveCreativeEventArgs(ad, userState));
                };
            InitAdTimeoutHandler.Begin(() =>
            {
                try
                {
                    ad.Player.InitAd(
                        adContainerSize.Width,
                        adContainerSize.Height,
#if !WINDOWS_PHONE && !FULLSCREEN
                        Application.Current.Host.Content.IsFullScreen ? "fullscreen" : "normal",
#else
 "normal",
#endif
 bitrate,
                        ad.Source.MediaSource,
                        ad.Source.ExtraInfo);
                }
                catch (Exception ex)
                {
                    OnLog(new ActiveCreativeLogEventArgs(ad, "VPAID.AdInit Exception: " + ex.Message));
                    throw ex;
                }
            }, AdInitTimeout);
        }

        /// <summary>
        /// Tells the ad to start.
        /// </summary>
        /// <param name="ad">The ad creative that should start playing</param>
        /// <param name="userState">A user state associated with this ad. This will be included with the various events associated with this ad.</param>
        public void StartAd(ActiveCreative ad, object userState)
        {
            TimeoutHelper<EventArgs> timeoutHandler = new TimeoutHelper<EventArgs>();
            ad.Player.AdStarted += timeoutHandler.Complete;
            EventHandler<VpaidMessageEventArgs> failHandler = (s, e) => timeoutHandler.Failed(new Exception(e.Message));
            ad.Player.AdError -= player_AdError;
            ad.Player.AdError += failHandler;
            timeoutHandler.Completed += (s, e) =>
            {
                ad.Player.AdStarted -= timeoutHandler.Complete;
                ad.Player.AdError -= failHandler;
                if (e.Error == null)
                {
                    ad.Player.AdError += player_AdError;
                    player_AdStarted(ad.Player, e.Result);
                }
                else
                    if (AdStartFailed != null) AdStartFailed(this, new ActiveCreativeEventArgs(ad, userState));
            };
            timeoutHandler.Begin(() =>
            {
                try
                {
                    ad.Player.StartAd();
                }
                catch (Exception ex)
                {
                    OnLog(new ActiveCreativeLogEventArgs(ad, "VPAID.StartAd Exception: " + ex.Message));
                    throw ex;
                }
            }, AdStartTimeout);
        }

        /// <summary>
        /// Stops the ad.
        /// </summary>
        /// <param name="ad">The ad creative that should stop playing</param>
        /// <param name="userState">A user state associated with this ad. This will be included with the various events associated with this ad.</param>
        void StopAd(ActiveCreative ad, object userState)
        {
            TimeoutHelper<EventArgs> timeoutHandler = new TimeoutHelper<EventArgs>();
            ad.Player.AdStopped -= player_AdStopped;    // temporarily unhook this event. Instead we will use our timeout handler to help trap for it.
            ad.Player.AdStopped += timeoutHandler.Complete;
            EventHandler<VpaidMessageEventArgs> failHandler = (s, e) => timeoutHandler.Failed(new Exception(e.Message));
            ad.Player.AdError -= player_AdError;
            ad.Player.AdError += failHandler;
            timeoutHandler.Completed += (s, e) =>
            {
                ad.Player.AdStopped += player_AdStopped;    // hook up the event again
                ad.Player.AdStopped -= timeoutHandler.Complete;
                ad.Player.AdError -= failHandler;
                if (e.Error == null)
                {
                    ad.Player.AdError += player_AdError;
                    player_AdStopped(ad.Player, e.Result);
                }
                else
                    if (AdStopFailed != null) AdStopFailed(this, new ActiveCreativeEventArgs(ad, userState));
            };
            timeoutHandler.Begin(() =>
            {
                try
                {
                    ad.Player.StopAd();
                }
                catch (Exception ex)
                {
                    OnLog(new ActiveCreativeLogEventArgs(ad, "VPAID.StopAd Exception: " + ex.Message));
                    throw ex;
                }
            }, AdStopTimeout);
        }

        /// <summary>
        /// Removes the ad.
        /// </summary>
        /// <param name="ad">The ad creative that should be removed.</param>
        public void RemoveAd(ActiveCreative ad)
        {
            ad.Target.RemoveChild(ad.Player);
            UnhookPlayer(ad.Player);
            activeAds.Remove(ad.Player);

            if (ad.Player is IDisposable)
            {
                try
                {
                    ((IDisposable)ad.Player).Dispose();
                }
                catch (Exception ex)
                {
                    OnLog(new ActiveCreativeLogEventArgs(ad, "VPAID.Dispose Exception: " + ex.Message));
                }
            }

            var userState = userStates[ad];
            userStates.Remove(ad);

            if (AdRemoved != null)
                AdRemoved(this, new ActiveCreativeEventArgs(ad, userState));
        }

        private void HookupPlayer(IVpaid player)
        {
            player.AdLinearChanged += new EventHandler(player_AdLinearChanged);
            player.AdLog += new EventHandler<VpaidMessageEventArgs>(player_AdLog);
            player.AdUserAcceptInvitation += new EventHandler(player_AdUserAcceptInvitation);
            player.AdUserClose += new EventHandler(player_AdUserClose);
            player.AdExpandedChanged += new EventHandler(player_AdExpandedChanged);
            player.AdResumed += new EventHandler(player_AdResumed);
            player.AdPaused += new EventHandler(player_AdPaused);
            player.AdVolumeChanged += new EventHandler(player_AdVolumeChanged);
            player.AdClickThru += new EventHandler<ClickThroughEventArgs>(player_AdClickThru);
            player.AdError += new EventHandler<VpaidMessageEventArgs>(player_AdError);
            player.AdImpression += new EventHandler(player_AdImpression);
            player.AdVideoFirstQuartile += new EventHandler(player_AdVideoFirstQuartile);
            player.AdVideoMidpoint += new EventHandler(player_AdVideoMidpoint);
            player.AdVideoThirdQuartile += new EventHandler(player_AdVideoThirdQuartile);
            player.AdVideoComplete += new EventHandler(player_AdVideoComplete);
            player.AdRemainingTimeChange += new EventHandler(player_AdRemainingTimeChange);
            //we intercept these with the timeouthandler and invoke internally instead
            //player.AdLoaded += new EventHandler(player_AdLoaded);
            player.AdStopped += new EventHandler(player_AdStopped);
        }

        private void UnhookPlayer(IVpaid player)
        {
            player.AdLinearChanged -= new EventHandler(player_AdLinearChanged);
            player.AdLog -= new EventHandler<VpaidMessageEventArgs>(player_AdLog);
            player.AdUserAcceptInvitation -= new EventHandler(player_AdUserAcceptInvitation);
            player.AdUserClose -= new EventHandler(player_AdUserClose);
            player.AdExpandedChanged -= new EventHandler(player_AdExpandedChanged);
            player.AdResumed -= new EventHandler(player_AdResumed);
            player.AdPaused -= new EventHandler(player_AdPaused);
            player.AdVolumeChanged -= new EventHandler(player_AdVolumeChanged);
            player.AdClickThru -= new EventHandler<ClickThroughEventArgs>(player_AdClickThru);
            player.AdError -= new EventHandler<VpaidMessageEventArgs>(player_AdError);
            player.AdImpression -= new EventHandler(player_AdImpression);
            player.AdVideoFirstQuartile -= new EventHandler(player_AdVideoFirstQuartile);
            player.AdVideoMidpoint -= new EventHandler(player_AdVideoMidpoint);
            player.AdVideoThirdQuartile -= new EventHandler(player_AdVideoThirdQuartile);
            player.AdVideoComplete -= new EventHandler(player_AdVideoComplete);
            player.AdRemainingTimeChange -= new EventHandler(player_AdRemainingTimeChange);
            //player.AdLoaded -= new EventHandler(player_AdLoaded);
            player.AdStopped -= new EventHandler(player_AdStopped);
        }

        /// <summary>
        /// Handshakes with the current player to test version compatibility.
        /// </summary>
        /// <param name="adPlayer">The player to handshake with.</param>
        /// <returns>Flag indicating whether the player's version is supported.</returns>
        public bool Handshake(IVpaid adPlayer)
        {
            // handshake with the ad player to make sure the version of VAST is OK
            string strPlayerVersion = null;
            try
            {
                strPlayerVersion = adPlayer.HandshakeVersion(HandlerVersion.ToString());
            }
            catch (Exception ex)
            {
                OnLog(new ActiveCreativeLogEventArgs(null, "VPAID.HandshakeVersion Exception: " + ex.Message));
            }
            try
            {
                Version playerVersion = new Version(strPlayerVersion);
                return (playerVersion <= HandlerVersion);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Forces the rewind tracking event to occur for all active creatives
        /// </summary>
        public void OnRewind()
        {
            foreach (var adSource in activeAds.Select(a => a.Value.Source))
            {
                TrackEvent(adSource, TrackingEventEnum.rewind);
            }
        }

        /// <summary>
        /// Forces the fullscreen tracking event to occur for all active creatives
        /// </summary>
        public void OnFullscreen()
        {
            foreach (var adSource in activeAds.Select(a => a.Value.Source))
            {
                TrackEvent(adSource, TrackingEventEnum.fullscreen);
            }
        }

        #region Ad Player events

        void player_AdLog(object sender, VpaidMessageEventArgs e)
        {
            var activeAd = activeAds[sender as IVpaid];
            OnLog(new ActiveCreativeLogEventArgs(activeAd, e.Message));
        }

        void player_AdExpandedChanged(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var adSource = activeAds[vp].Source;

            if (vp.AdExpanded)
                TrackEvent(adSource, TrackingEventEnum.expand);
            else
                TrackEvent(adSource, TrackingEventEnum.collapse);
        }

        void player_AdLinearChanged(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var activeCreative = activeAds[vp];
            bool isLinear;
            try
            {
                isLinear = vp.AdLinear;
            }
            catch (Exception ex)
            {
                OnLog(new ActiveCreativeLogEventArgs(activeCreative, "VPAID.AdLinear Exception: " + ex.Message));
                return;
            }
            var args = new ActiveCreativeEventArgs(activeCreative, userStates[activeCreative]);
            if (isLinear)
            {
                if (AdIsLinear != null) AdIsLinear(this, args);
            }
            else
            {
                if (AdIsNotLinear != null) AdIsNotLinear(this, args);
            }
        }

        void player_AdUserClose(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.close);
        }

        void player_AdUserAcceptInvitation(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.acceptInvitation);
        }

        void player_AdResumed(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var adSource = activeAds[vp].Source;
            TrackEvent(adSource, TrackingEventEnum.resume);
            if (AdResumed != null)
            {
                var ad = activeAds[vp];
                var userState = userStates[ad];

                AdResumed(this, new ActiveCreativeEventArgs(ad, userState));
            }
        }

        void player_AdPaused(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var adSource = activeAds[vp].Source;
            TrackEvent(adSource, TrackingEventEnum.pause);
            if (AdPaused != null)
            {
                var ad = activeAds[vp];
                var userState = userStates[ad];

                AdPaused(this, new ActiveCreativeEventArgs(ad, userState));
            }
        }

        void player_AdVolumeChanged(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var activeCreative = activeAds[vp];
            var adSource = activeCreative.Source;
            double vol;
            try
            {
                vol = vp.AdVolume;
            }
            catch (Exception ex)
            {
                OnLog(new ActiveCreativeLogEventArgs(activeCreative, "VPAID.AdVolume.Get Exception: " + ex.Message));
                return;
            }

            if (vol == 0)
            {
                TrackEvent(adSource, TrackingEventEnum.mute);
            }
            else if (activeCreative.PreviousVolume == 0)
            {
                TrackEvent(adSource, TrackingEventEnum.unmute);
            }
            activeCreative.PreviousVolume = vol;
        }

        void player_AdImpression(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.impression);
            TrackEvent(adSource, TrackingEventEnum.start);
        }

        void player_AdVideoFirstQuartile(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.firstQuartile);
        }

        void player_AdVideoMidpoint(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.midpoint);
        }

        void player_AdVideoThirdQuartile(object sender, EventArgs e)
        {
            var adSource = activeAds[sender as IVpaid].Source;
            TrackEvent(adSource, TrackingEventEnum.thirdQuartile);
        }

        void player_AdVideoComplete(object sender, EventArgs e)
        {
            var vp = sender as IVpaid;
            var adSource = activeAds[vp].Source;
            TrackEvent(adSource, TrackingEventEnum.complete);

            var ad = activeAds[vp];
            var userState = userStates[ad];
            StopAd(ad, userState);
        }

        void player_AdRemainingTimeChange(object sender, EventArgs e)
        {
            if (AdProgressChanged != null)
            {
                var vp = sender as IVpaid;
                var adSource = activeAds[vp].Source;

                var ad = activeAds[vp];
                var userState = userStates[ad];

                AdProgressChanged(this, new ActiveCreativeEventArgs(ad, userState));
            }
        }

        private void TrackEvent(ICreativeSource adSource, TrackingEventEnum eventToTrack)
        {
            try
            {
                adSource.Track(eventToTrack);
            }
            catch (Exception ex)
            {
                if (TrackingFailed != null) TrackingFailed(this, new TrackingFailureEventArgs(eventToTrack.ToString(), ex));
            }
        }

        void player_AdClickThru(object sender, ClickThroughEventArgs e)
        {
#if !WINDOWS_PHONE && !OOB
            if (e.PlayerHandles && !string.IsNullOrEmpty(e.Url))
            {
                HtmlPage.Window.Navigate(new Uri(e.Url), "_blank");
            }
#endif
            var vp = sender as IVpaid;
            var adSource = activeAds[vp].Source;
            TrackEvent(adSource, TrackingEventEnum.click);
        }

        void player_AdError(object sender, VpaidMessageEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Message);

            IVpaid vp = sender as IVpaid;
            var ad = activeAds[vp];
            var userState = userStates[ad];

            var adSource = ad.Source;
            TrackEvent(adSource, TrackingEventEnum.error);

            if (AdFailed != null)
                AdFailed(this, new ActiveCreativeEventArgs(ad, userState));
        }

        void player_AdLoaded(object sender, EventArgs args)
        {
            IVpaid vp = sender as IVpaid;
            if (vp != null)
            {
                var ad = activeAds[vp];
                var userState = userStates[ad];
                if (AdLoaded != null)
                    AdLoaded(this, new ActiveCreativeEventArgs(ad, userState));

                TrackEvent(ad.Source, TrackingEventEnum.creativeView);
            }
        }

        void player_AdStarted(object sender, EventArgs e)
        {
            IVpaid vp = sender as IVpaid;
            var ad = activeAds[vp];
            var adSource = ad.Source;
            var userState = userStates[ad];
            if (AdStarted != null)
                AdStarted(this, new ActiveCreativeEventArgs(ad, userState));
            TrackEvent(adSource, TrackingEventEnum.start);
        }

        void player_AdStopped(object sender, EventArgs e)
        {
            IVpaid vp = sender as IVpaid;

            var ad = activeAds[vp];
            var userState = userStates[ad];
            if (AdStopped != null)
                AdStopped(this, new ActiveCreativeEventArgs(ad, userState));
        }
        #endregion

        /// <summary>
        /// Provides additional information about a tracking failure event.
        /// </summary>
        public class TrackingFailureEventArgs : EventArgs
        {
            public TrackingFailureEventArgs(string TrackingEvent, Exception Exception)
            {
                trackingEvent = TrackingEvent;
                exception = Exception;
            }

            readonly string trackingEvent;
            /// <summary>
            /// The tracking url that failed.
            /// </summary>
            public string TrackingEvent
            {
                get { return trackingEvent; }
            }

            readonly Exception exception;
            /// <summary>
            /// The exception that occurred when trying to track.
            /// </summary>
            public Exception Exception
            {
                get { return exception; }
            }
        }

        protected void OnLog(ActiveCreativeLogEventArgs args)
        {
            if (Log != null) Log(this, args);
        }

    }

}
