//#define LATENCYTEST

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// Plugin that handles VAST ads
    /// </summary>
    [ExportAdPayloadHandlerPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion, SupportedFormat = PayloadFormat)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AdHandler : AdHandlerBase
    {
        #region IPlugin Members
        private const string PluginName = "VastAdHandler";
        private const string PluginDescription = "A handler for ads in VAST format - manages layout and interaction with the player";
        private const string PluginVersion = "2.2012.0605.0";
        #endregion

        public const string PayloadFormat = "vast";

        private VastCreativeFactory adModelFactory;
        private VastAdPod activeAdPod;
        private readonly List<VastCreativeSet> activeCreativeSets = new List<VastCreativeSet>();
        private readonly Dictionary<string, VAST> availableAds = new Dictionary<string, VAST>();
        // A dictionary of VAST documents currently being downloaded.
        private readonly Dictionary<string, WebClient> downloadingAds = new Dictionary<string, WebClient>();

        public AdHandler()
            : base()
        {
            PluginLogName = PluginName;
        }

        /// <summary>
        /// A dictionary of VAST documents keyed on altReference and Uri.
        /// All downloaded VAST documents are cached here.
        /// New ads with the same key will always be retreived from here instead of re-downloaded.
        /// </summary>
        public Dictionary<string, VAST> AvailableAds { get { return availableAds; } }

        /// <summary>
        /// Raised when an individual VAST ad fails. The criteria are based on the FailureStrategy property.
        /// </summary>
        public event EventHandler<AdCompletedEventArgs> VastAdCompleted;

        protected override bool CanHandleAd(IAdSource source)
        {
            if (activeAdPod != null)
            {
                if (activeAdPod.CanShutdown)
                {
                    // shut down the current trigger and let the new one in.
                    activeAdPod.Cancel();
                }
                else
                {
                    // there is already a loading ad. ignore the new trigger.
                    return false;
                }
            }
            return true;
        }

        protected override IAsyncAdPayload CreatePayload(IAdSource source)
        {
            // create the payload result. It will only contain vast based ads
            VastAdUnit result = new VastAdUnit(source);

            // create a model to hold the ad and tell it to load itself
            activeAdPod = new VastAdPod(result, adModelFactory);
            activeAdPod.FailurePolicy = FailurePolicy;
            activeAdPod.CloseCompanionsOnComplete = CloseCompanionsOnComplete;
            result.AdPod = activeAdPod; // remember the active adspot

            return result;
        }

        private readonly object loadBlocker = new object();
        protected override bool ExecutePayload(IAsyncAdPayload Payload, IAdSource Source)
        {
            var result = Payload as VastAdUnit;
            var adPod = result.AdPod;

            bool needsRelease;
            Player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Collapsed);
            if (((Microsoft.SilverlightMediaFramework.Core.SMFPlayer)adHost).IsPlayBlocked)
            {
                // if we're already blocked, stick with it.
                AdHost.AddPlayBlock(loadBlocker);
                needsRelease = true;
            }
            else
            {
                Player.ActiveMediaPlugin.Pause();
                needsRelease = false;
            }

            adPod.LoadAsync(success =>
            {
                Player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Visible);
                if (success)
                {
                    // now that it is loaded, watch for each vastAd and CreativeSet to begin and complete running
                    foreach (var vastAd in adPod.Ads)
                    {
                        vastAd.RunCompleted += vastAd_RunCompleted;
                        foreach (var creativeSet in vastAd.CreativeSets)
                        {
                            creativeSet.RunStarting += creativeSet_RunStarting;
                            creativeSet.RunStarted += creativeSet_RunStarted;
                            creativeSet.RunCompleted += creativeSet_RunCompleted;
                        }
                    }

                    // pass on that we are now running this ad. Note: It still could fail to run.
                    result.OnStart();
                    // actually run the ad
                    adPod.RunCompleted += new Action<AdPod, bool>(adPod_RunCompleted);
                    adPod.ReleasePlayer += new Action<AdPod>(adPod_ReleasePlayer);
                    adPod.RunAsync();
                }
                else
                {
                    // clear out the current running AdSpot. This permits other ads to be handled.
                    activeCreativeSets.Clear();
                    activeAdPod = null;

                    // notify upstream
                    result.OnFail();
                    result.Deactivate();

                    base.ExecuteAdFailed(Source);

                    if (!needsRelease)
                    {
                        Player.ActiveMediaPlugin.Play();
                    }
                }

                if (needsRelease)
                {
                    adHost.ReleasePlayBlock(loadBlocker);
                }

            });
            return true;
        }
                
        void adPod_ReleasePlayer(AdPod adPod)
        {
            adPod.ReleasePlayer -= adPod_ReleasePlayer;
            // release the play block (this will start the player again if a play operation was pending)
            ReleasePlayer();
        }

        void adPod_RunCompleted(AdPod adPod, bool success)
        {
            adPod.RunCompleted -= adPod_RunCompleted;

            VastAdPod vastAdPod = adPod as VastAdPod;
            // unhook all the event handlers we added for the individual parts of the ad operation
            foreach (var vastAd in vastAdPod.Ads)
            {
                vastAd.RunCompleted -= vastAd_RunCompleted;
                foreach (var creativeSet in vastAd.CreativeSets)
                {
                    creativeSet.RunStarting -= creativeSet_RunStarting;
                    creativeSet.RunStarted -= creativeSet_RunStarted;
                    creativeSet.RunCompleted -= creativeSet_RunCompleted;
                }
            }

            // clear out the current running AdSpot. This permits other ads to be handled.
            activeAdPod = null;

            // notify upstream
            if (!success) vastAdPod.AdUnit.OnFail();
            vastAdPod.AdUnit.Deactivate();

            OnHandleCompleted(new HandleCompletedEventArgs(vastAdPod.AdUnit.Source, success));
        }

        #region vPaidController EventHandlers

        protected override void vPaidController_AdCompleted(object sender, ActiveCreativeEventArgs e)
        {
            // do nothing, we control our own blocking
        }

        protected override void AdFailed(ActiveCreativeEventArgs e)
        {
            base.AdFailed(e);

            VastCreative creative = e.UserState as VastCreative;
            creative.Failed();
        }

        protected override void AdStopped(ActiveCreativeEventArgs e)
        {
            base.AdStopped(e);

            VastCreative creative = e.UserState as VastCreative;
            creative.Succeeded();
        }

        //protected override void RefreshAdMode()
        //{
        //    if (activeCreativeSets.Any(cs => cs.ContainsLinear) || VpaidController.ActivePlayers.Any(p => p.AdLinear))    // if any creative sets contain a linear ad, we need to block playback
        //    {
        //        BlockPlayer();
        //    }
        //    else
        //    {
        //        ReleasePlayer();
        //    }
        //}

        //protected override bool IsPauseRequired()
        //{
        //    var linearPlayers = VpaidController.ActivePlayers.Where(p => p.AdLinear).ToList();
        //    return (linearPlayers.Any() && !linearPlayers.Any(p => (p is IVpaidLinearBehavior && ((IVpaidLinearBehavior)p).Nonlinear)));
        //}

        #endregion

        #region AdModel EventHandlers
        private void creativeSet_RunStarting(CreativeSet sender)
        {
            var vastCreativeSet = sender as VastCreativeSet;
            activeCreativeSets.Add(vastCreativeSet);    // remember the active creative set.
            //RefreshAdMode();

            foreach (var creative in vastCreativeSet.Creatives)
            {
                creative.RunCompleted += creative_RunCompleted;
            }
        }

        private void creativeSet_RunStarted(CreativeSet sender)
        {
            var vastCreativeSet = sender as VastCreativeSet;
            // unhook all creatives
            foreach (var creative in vastCreativeSet.Creatives)
            {
                creative.RunCompleted -= creative_RunCompleted;
            }
            // re-hook the ones that are actually running
            foreach (var creative in vastCreativeSet.RunningCreatives)
            {
                creative.RunCompleted += creative_RunCompleted;
            }
        }

        private void creative_RunCompleted(Creative creative, bool success)
        {
            var vastAdCreative = (VastCreative)creative;
            if (success)
                SendLogEntry(LogEntryTypes.CreativeSucceeded, LogLevel.Information, string.Format(VastAdHandlerResources.CreativeSucceeded, vastAdCreative.Id));
            else
                SendLogEntry(LogEntryTypes.CreativeFailed, LogLevel.Error, string.Format(VastAdHandlerResources.CreativeFailed, vastAdCreative.Id));
        }

        private void creativeSet_RunCompleted(CreativeSet sender, bool success)
        {
            var vastCreativeSet = sender as VastCreativeSet;
            // cleanup
            foreach (var creative in vastCreativeSet.Creatives)
            {
                creative.RunCompleted -= creative_RunCompleted;
            }
            activeCreativeSets.Remove(vastCreativeSet);
        }

        private void vastAd_RunCompleted(Ad vastAd, bool success)
        {
            if (VastAdCompleted != null) VastAdCompleted(this, new AdCompletedEventArgs(vastAd as VastAd, success));
        }
        #endregion

        #region AdHost EventHandlers
        private void adHost_StateChanged(object sender, EventArgs e)
        {
            // do nothing
        }

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

        #region VAST download/population operations
        /// <summary>
        /// Populates an AdUnit with the VAST info, downloads the VAST doc if necessary
        /// </summary>
        /// <param name="ad">The VastAdUnit to be populated</param>
        /// <param name="Completed">Fired when the operation is complete, includes a status param</param>
        internal void LoadAdUnitAsync(VastAdUnit ad, Action<bool> Completed)
        {
            if (!string.IsNullOrEmpty(ad.Source.Uri))
            {
                var key = GetAdSourceKey(ad.Source);
                //look to see if we have a source already
                if (AvailableAds.ContainsKey(key))
                {
                    VAST vast = AvailableAds[key];
                    ad.Vast = vast;

                    if (Completed != null) Completed(true);
                }
                else if (downloadingAds.ContainsKey(key))
                {
                    // piggy back on the currently downloading doc
                    var wc = downloadingAds[key];
                    wc.DownloadStringCompleted += (s, e) =>
                    {
                        OnSourceDownloadCompleted(e, ad, Completed);
                    };
                }
                else
                {
                    var wc = DownloadSource(ad, Completed);
                    downloadingAds.Add(key, wc);    // add the webclient to a dictionary
                }
            }
            else
            {
                if (Completed != null) Completed(false);
            }
        }

        private WebClient DownloadSource(VastAdUnit ad, Action<bool> Completed)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnSourceDownloadCompleted);
            Uri uri = ConversionHelper.GetAbsoluteUri(System.Windows.Application.Current.Host.Source, ad.Source.Uri);
            wc.DownloadStringAsync(uri, new Tuple<VastAdUnit, Action<bool>>(ad, Completed));
            return wc;
        }

        private void OnSourceDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var userState = (Tuple<VastAdUnit, Action<bool>>)e.UserState;
            var ad = userState.Item1;
            var Completed = userState.Item2;
            var key = GetAdSourceKey(ad.Source);
            downloadingAds.Remove(key);

            OnSourceDownloadCompleted(e, ad, Completed);
        }

        private void OnSourceDownloadCompleted(DownloadStringCompletedEventArgs e, VastAdUnit ad, Action<bool> Completed)
        {
#if LATENCYTEST
            var latencySimulatorTimer = new DispatcherTimer();
            latencySimulatorTimer.Interval = TimeSpan.FromSeconds(3);
            latencySimulatorTimer.Start();
            latencySimulatorTimer.Tick += (ts, te) => {
#endif
            if (e.Error == null)
            {
                Exception ex;
                VAST vast;

                if (VAST.Deserialize(e.Result, out vast, out ex) && vast != null)
                {
                    var key = GetAdSourceKey(ad.Source);
                    if (!AvailableAds.ContainsKey(key))
                    {
                        AvailableAds.Add(key, vast);
                    }
                    ad.Vast = vast;

                    if (Completed != null) Completed(true);
                }
                if (ex != null)
                {
                    if (Completed != null) Completed(false);
                }
            }
            else
            {
                //Log.Output(OutputType.Error, "Unknown error handling VAST doc from source url: " + t.Source.uri);
                if (Completed != null) Completed(false);
            }
#if LATENCYTEST
            latencySimulatorTimer.Stop();
        };
#endif
        }

        private void DownloadWrappedAd(IAdSource Source, VASTAD ad, Action<VASTAD> OnComplete, Action<VASTAD, Exception> OnError)
        {
            var wrapperAd = (VASTADWrapper)ad.Item;
            var url = wrapperAd.VASTAdTagURI;

            //look to see if we have a source already
            var key = GetAdSourceKey(url, Source.AltReference);
            if (AvailableAds.ContainsKey(key))
            {
                VAST vast = AvailableAds[key];
                UnWrapAd(Source, ad, OnComplete, OnError, vast);
            }
            else
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += OnWrappedAdDownloadCompleted;
                Uri uri = new Uri(url, UriKind.Absolute);
                wc.DownloadStringAsync(uri, new Tuple<IAdSource, VASTAD, Action<VASTAD>, Action<VASTAD, Exception>>(Source, ad, OnComplete, OnError));
            }
        }

        private void OnWrappedAdDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var wc = sender as WebClient;
            wc.DownloadStringCompleted -= OnWrappedAdDownloadCompleted;

            var args = (Tuple<IAdSource, VASTAD, Action<VASTAD>, Action<VASTAD, Exception>>)e.UserState;
            var source = args.Item1;
            var ad = args.Item2;
            var OnComplete = args.Item3;
            var OnError = args.Item4;
            if (e.Error == null)
            {
                Exception ex;
                VAST vast;

                if (VAST.Deserialize(e.Result, out vast, out ex) && vast != null)
                {
                    var wrapperAd = (VASTADWrapper)ad.Item;
                    var key = GetAdSourceKey(wrapperAd.VASTAdTagURI, source.AltReference);
                    AvailableAds.Add(key, vast);

                    UnWrapAd(source, ad, OnComplete, OnError, vast);
                }
                if (ex != null)
                {
                    OnError(ad, new Exception("Failed to deserialize VAST doc.", ex));
                }
            }
            else
            {
                OnError(ad, new Exception("Unknown error downloading wrapped VAST doc", e.Error));
            }
        }

        private void UnWrapAd(IAdSource Source, VASTAD ad, Action<VASTAD> OnComplete, Action<VASTAD, Exception> OnError, VAST vast)
        {
            // there has to be a one to one relationship between wrapped ads and what their linear counterpart. However, a wrapped ad can point to an entire VAST doc which can contain many ads. Therefore, always just take the first one.
            var newAd = vast.Ad.FirstOrDefault(a => a.Item != null);
            if (newAd == null)
            {
                OnError(ad, new Exception("Wrapped ad was empty"));
            }
            else if (newAd.Item is VASTADWrapper)
            {
                DownloadWrappedAd(Source, newAd, OnComplete, OnError);
            }
            else
            {
                // plug the ad back into the parent
                OnComplete(newAd);
            }
        }

        /// <summary>
        /// Processes any wrapper ads in the adpod by downloading and merging them.
        /// All adpods should go through this process before being run.
        /// </summary>
        /// <param name="adPod">The adpod to process.</param>
        /// <param name="Completed">A callback to notify when the operation is complete.</param>
        internal void UnWrapAdPod(VastAdPod adPod, Action Completed)
        {
            var wrapperAds = adPod.AdUnit.Vast.Ad.Where(a => a.Item is VASTADWrapper).ToList();
            if (wrapperAds.Any())
            {
                int loadingWrapperAds = 0;
                foreach (var ad in wrapperAds)
                {
                    loadingWrapperAds++;
                    DownloadWrappedAd(adPod.AdUnit.Source, ad,
                        a =>
                        {
                            MergeWrappedAd((VASTADWrapper)ad.Item, (VASTADInLine)a.Item);
                            ad.Item = a.Item;   // now that the essential wrapper info is applied, set the wrapper to the inline.
                            loadingWrapperAds--;
                            if (loadingWrapperAds == 0) Completed();
                        },
                        (a, e) =>
                        {
                            //todo: log error
                            ad.Item = null;
                            loadingWrapperAds--;
                            if (loadingWrapperAds == 0) Completed();
                        });
                }
            }
            else
            {
                Completed();
            }
        }

        /// <summary>
        /// Merges the tracking info for the two ads
        /// </summary>
        private static void MergeWrappedAd(VASTADWrapper wrapperAd, VASTADInLine inlineAd)
        {
            // carry over impressions
            {
                var newItems = inlineAd.Impression.ToList();
                newItems.AddRange(wrapperAd.Impression.Select(i => new Impression_type() { Value = i }));
                inlineAd.Impression = newItems.ToArray();
            }
            // carry over tracking events
            foreach (var creative in inlineAd.Creatives)
            {
                var creativeItem = creative.Item;
                VASTADWrapperCreative creativeWrapper = FindMatchingCreative(inlineAd, creative, wrapperAd);
                if (creativeWrapper != null)
                {
                    if (creativeItem is VASTADInLineCreativeLinear)
                    {
                        var linearWrapper = (VASTADWrapperCreativeLinear)creativeWrapper.Item;
                        if (linearWrapper != null && linearWrapper.TrackingEvents != null)
                        {
                            var linear = (VASTADInLineCreativeLinear)creativeItem;
                            List<TrackingEvents_typeTracking> newItems;
                            if (linear.TrackingEvents == null)
                                newItems = new List<TrackingEvents_typeTracking>();
                            else
                                newItems = linear.TrackingEvents.ToList();
                            newItems.AddRange(linearWrapper.TrackingEvents);

                            linear.TrackingEvents = newItems.ToArray();
                        }
                    }
                    else if (creativeItem is VASTADInLineCreativeNonLinearAds)
                    {
                        var nonlinearWrapper = (VASTADWrapperCreativeNonLinearAds)creativeWrapper.Item;
                        if (nonlinearWrapper != null && nonlinearWrapper.TrackingEvents != null)
                        {
                            var nonLinear = (VASTADInLineCreativeNonLinearAds)creativeItem;
                            List<TrackingEvents_typeTracking> newItems;
                            if (nonLinear.TrackingEvents == null)
                                newItems = new List<TrackingEvents_typeTracking>();
                            else
                                newItems = nonLinear.TrackingEvents.ToList();
                            newItems.AddRange(nonlinearWrapper.TrackingEvents);

                            nonLinear.TrackingEvents = newItems.ToArray();
                        }
                    }
                    else if (creativeItem is VASTADInLineCreativeCompanionAds)
                    {
                        var companions = (VASTADInLineCreativeCompanionAds)creativeItem;
                        foreach (var companion in companions.Companion)
                        {
                            Companion_type companionWrapper = FindMatchingCompanion(companions, companion, (VASTADWrapperCreativeCompanionAds)creativeWrapper.Item);
                            if (companionWrapper != null && companionWrapper.TrackingEvents != null)
                            {
                                List<TrackingEvents_typeTracking> newItems;
                                if (companion.TrackingEvents == null)
                                    newItems = new List<TrackingEvents_typeTracking>();
                                else
                                    newItems = companion.TrackingEvents.ToList();
                                newItems.AddRange(companionWrapper.TrackingEvents);

                                companion.TrackingEvents = newItems.ToArray();
                            }
                        }
                    }
                }
            }
        }

        private static VASTADWrapperCreative FindMatchingCreative(VASTADInLine ad, VASTADInLineCreative creative, VASTADWrapper wrapperAd)
        {
            Type type = creative.Item.GetType();
            var appropriateCreatives = ad.Creatives.Where(c => c.Item.GetType() == type);
            Type matchingType;
            if (type == typeof(VASTADInLineCreativeLinear))
                matchingType = typeof(VASTADWrapperCreativeLinear);
            else if (type == typeof(VASTADInLineCreativeNonLinearAds))
                matchingType = typeof(VASTADWrapperCreativeNonLinearAds);
            else if (type == typeof(VASTADInLineCreativeCompanionAds))
                matchingType = typeof(VASTADWrapperCreativeCompanionAds);
            else
                return null;

            var appropriateWrapperCreatives = wrapperAd.Creatives.Where(c => c.Item.GetType() == matchingType);

            int index = appropriateCreatives.ToList().IndexOf(creative);
            if (appropriateWrapperCreatives.Count() > index)
                return appropriateWrapperCreatives.ElementAt(index);
            else
                return null;
        }

        private static Companion_type FindMatchingCompanion(VASTADInLineCreativeCompanionAds companions, Companion_type companion, VASTADWrapperCreativeCompanionAds wrapperCompanions)
        {
            int index = companions.Companion.ToList().IndexOf(companion);
            if (wrapperCompanions.Companion.Length > index)
                return wrapperCompanions.Companion[index];
            else
                return null;
        }
        #endregion

        #region IGenericPlugin

        public override void Load()
        {
            adModelFactory = new VastCreativeFactory(this);
            base.Load();
        }

        public override void Unload()
        {
            adModelFactory = null;
            base.Unload();
        }

        #endregion

        internal void PlayCreative(VastCreative creative)
        {
            PlayCreative(creative.ActiveCreative, creative);
        }

        internal void CancelCreative(VastCreative creative)
        {
            base.CancelCreative(creative.ActiveCreative);
        }

        /// <summary>
        /// Creates a key for the AvailableAds dictionary from the AdSequencingSource.
        /// </summary>
        /// <param name="Source">The ad sequencing source</param>
        /// <returns>A string key to be used in the AvailableAds dictionary</returns>
        public string GetAdSourceKey(IAdSource Source)
        {
            return GetAdSourceKey(Source.Uri, Source.AltReference);
        }

        /// <summary>
        /// Creates a key for the AvailableAds dictionary from a key (altReference) and the Uri of the document.
        /// This is the recommended way to key ads from the VAST spec
        /// </summary>
        /// <param name="uri">The Uri of the VAST document</param>
        /// <param name="altReference">Optional. An additional piece of data to add to your key</param>
        /// <returns>A string key to be used in the AvailableAds dictionary</returns>
        public string GetAdSourceKey(string uri, string altReference = null)
        {
            if (string.IsNullOrEmpty(altReference))
            {
                return uri;
            }
            else
            {
                return altReference + uri;
            }
        }

    }
}
