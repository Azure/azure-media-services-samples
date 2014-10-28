using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Checks the status of ad markers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class tracks which ad markers have been reached, which ad markers have been left 
    /// (when media playing has passed the end position of an ad marker), 
    /// and which ad markers were skipped (due to the user seeking to a new position in the timeline, for example).
    /// This class raises the MarkerReached, MarkerLeft, and MarkerSkipped events respectively when these conditions occur.
    /// </para>
    /// </remarks>
    internal class AdMarkerManager : SkippableMarkerManager<AdMarker>
    {
        public AdMarkerManager(SMFPlayer Player)
        {
            player = Player;
            JumpToLiveAfterAd = true;
            PlayAdsOnSeek = true;
            PlayAdsOnFwdRwd = true;
        }

        private SMFPlayer player;
        //private ScheduledAd ActiveAd = null;

        /// <summary>
        /// Indicates that if the media source is a live streaming source, the player should jump to the current live position if we were close to the live position to begin with.
        /// </summary>
        public bool JumpToLiveAfterAd { get; set; }

        /// <summary>
        /// Indicates that ads should still be triggered when seeked into
        /// </summary>
        public bool PlayAdsOnSeek { get; set; }

        /// <summary>
        /// Indicates that ads should still be triggered when fast-forwarding or rewinding into.
        /// </summary>
        public bool PlayAdsOnFwdRwd { get; set; }

        /// <summary>
        /// Exposes the list of AdPayloadHandler plugins used to handle ads.
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IAdPayloadHandlerPlugin, IAdPayloadHandlerMetadata>> PayloadHandlers { get; internal set; }

        protected override void OnMarkerLeft(AdMarker mediaMarker)
        {
            if (mediaMarker.ScheduledAd != null)
            {
                var content = mediaMarker.ScheduledAd;
                content.Deactivate();   // this will fire an event that its handler can track 
            }

            base.OnMarkerLeft(mediaMarker);
        }

        protected override void OnMarkerReached(AdMarker mediaMarker, bool skippedInto)
        {
            var ad = mediaMarker.ScheduledAd;

            if (!PlayAdsOnSeek && skippedInto)
            {
                // don't handle if seeked into
                player.SendLogEntry(KnownLogEntryTypes.AdSkippedFromSeek, Plugins.Primitives.LogLevel.Information, string.Format(SilverlightMediaFrameworkResources.AdSkippedFromSeek, ad.Trigger.Id), this.GetType().Name);
                ad.Deactivate();
                return;
            }

            if (!PlayAdsOnFwdRwd)
            {
                // don't handle if rewind or fast forward
                if (player.ActiveMediaPlugin != null && player.ActiveMediaPlugin.PlaybackRate != 1.0)
                {
                    // Not supporting rewind or fast forward
                    player.SendLogEntry(KnownLogEntryTypes.AdSkippedFromFwdRwd, Plugins.Primitives.LogLevel.Information, string.Format(SilverlightMediaFrameworkResources.AdSkippedFromFwdRwd, ad.Trigger.Id), this.GetType().Name);
                    ad.Deactivate();
                    return;
                }
            }

            if (PayloadHandlers != null)
            {
                if (ad != null)
                {
                    var unit = TriggerAd(ad.Trigger);
                    if (unit != null)
                    {
                        //if it returns us a payload, it handled it. 
                        ad.Payload = unit;
                        unit.ActiveSourceChanged += new EventHandler(unit_ActiveSourceChanged);
                        unit.Deactivated += new EventHandler(unit_Deactivated);
                        //ActiveAd = ad;
                    }
                    else
                    {
                        player.SendLogEntry(KnownLogEntryTypes.AdNotHandled, Plugins.Primitives.LogLevel.Error, string.Format(SilverlightMediaFrameworkResources.AdNotHandled, ad.Trigger.Id), this.GetType().Name);
                        // no one handled it, deactivate it ASAP.
                        ad.Deactivate();
                    }
                }
            }

            base.OnMarkerReached(mediaMarker, skippedInto);
        }

        private AdSequencingPayload TriggerAd(IAdSequencingTrigger Trigger)
        {
            var activeAdPod = new AdSequencingPayload(Trigger);
            activeAdPod.WasAtLivePosition = player.IsMediaLive && player.IsPositionLive;
            activeAdPod.ActiveSource = ProcessNextAdInQueue(activeAdPod.AdQueue);
            if (activeAdPod.ActiveSource != null)
            {
                return activeAdPod;
            }
            else
            {
                return null;
            }
        }

        private void unit_ActiveSourceChanged(object sender, EventArgs e)
        {
            AdSequencingPayload activeAdPod = sender as AdSequencingPayload;
            if (activeAdPod.ActiveSource == null)
            {
                ProcessNextAdInQueue(activeAdPod.AdQueue);
            }
        }

        private AdSequencingSourcePayload ProcessNextAdInQueue(Queue<IAdSequencingSource> adQueue)
        {
            while (adQueue.Count > 0)
            {
                var Source = adQueue.Dequeue();
                var result = TriggerAdSequencingSource(Source);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private AdSequencingSourcePayload TriggerAdSequencingSource(IAdSequencingSource Source)
        {
            IAdPayload payload = null;
#if !WINDOWS_PHONE && !SILVERLIGHT3
            var plugins = PayloadHandlers.Where(i => Source.Format.ToLower() == i.Metadata.SupportedFormat.ToLower());
#else
            var plugins = PayloadHandlers.Where(i => i.LooseMetadata.ContainsKey("SupportedFormat") && (((string)i.LooseMetadata["SupportedFormat"]).ToLower() == Source.Format.ToLower()));
#endif
            var handlers = plugins.Select(i => i.Value);

            foreach (var payloadHandler in handlers)
            {
                payload = payloadHandler.Handle(Source);
                if (payload != null) break;
            }

            if (payload != null)
            {
                var result = new AdSequencingSourcePayload(payload);
                foreach (var child in Source.Sources)
                {
                    var childResult = TriggerAdSequencingSource(child);
                    if (childResult != null)
                    {
                        result.AddChild(childResult);
                    }
                }
                return result;
            }
            else
            {
                // pass this info upstream
                player.OnAdFailed(Source);
                return null;
            }
        }

        /// <summary>
        /// Fires when the ad has completed running (regardless of the reason).
        /// </summary>
        private void unit_Deactivated(object sender, EventArgs e)
        {
            var unit = sender as AdSequencingPayload;
            unit.Deactivated -= new EventHandler(unit_Deactivated);
            unit.ActiveSourceChanged -= new EventHandler(unit_ActiveSourceChanged);

            //ActiveAd = null;

            // the ad has completed, make sure at live position if watching live stream
            // and close to the live position... jump to live if the current position
            // after ad plays is within LivePositionBufferDuration of the actual
            // live position, this is already calculated in the IsPositionLive property
            // We also have a setting that turns this on/off
            if (JumpToLiveAfterAd && player.IsMediaLive && unit.WasAtLivePosition)
            {
                player.SeekToLive();
            }
        }
    }

}