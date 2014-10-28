using System;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// Provides a way for the VastAdModels to load creatives
    /// </summary>
    internal class VastCreativeFactory
    {
        public VastCreativeFactory(AdHandler VastAdHandler)
        {
            vastAdHandler = VastAdHandler;
        }

        readonly AdHandler vastAdHandler;
        public AdHandler VastAdHandler { get { return vastAdHandler; } }

        public ActiveCreative GetCompanionCreative(VastAdPod AdPod, VASTADInLine InlineAd, Companion_type Companion)
        {
            ICreativeSource PayloadSource = new CompanionSource(InlineAd, Companion);
            IAdTarget target = VastAdHandler.FindTarget(AdPod.AdUnit.Source, PayloadSource);
            if (target == null)
            {
                return null;
            }
            else
            {
                return GetCompanionCreative(PayloadSource, target);
            }
        }

        public ActiveCreative GetCompanionCreative(ICreativeSource adSource, IAdTarget adTarget)
        {
            return vastAdHandler.GetCreative(adSource, adTarget);
        }

        public ActiveCreative GetNonLinearCreative(VASTADInLine InlineAd, VASTADInLineCreativeNonLinearAds nonLinears, NonLinear_type nl, IAdSource AdSource)
        {
            var PayloadSource = new NonLinearSource(InlineAd, nonLinears, nl);
            //get the target. In this case because it is a nonlinear ad, it will aways be the player's main adcontainer
            IAdTarget target = VastAdHandler.FindTarget(AdSource, PayloadSource);
            if (target == null)
            {
                return null;
            }
            else
            {
                return GetNonLinearCreative(PayloadSource, target);
            }
        }

        public ActiveCreative GetNonLinearCreative(ICreativeSource adSource, IAdTarget adTarget)
        {
            return vastAdHandler.GetCreative(adSource, adTarget);
        }

        public ActiveCreative GetLinearCreative(VASTADInLine inline, VASTADInLineCreative Creative, VASTADInLineCreativeLinear linear, IAdSource AdSource)
        {
            //get the target. In this case because it is a linear ad, it will aways be the player's main adcontainer
            IAdTarget target = VastAdHandler.FindTarget(AdSource, new LinearSource(inline, null, linear, new VASTADWrapper[] { }));
            if (target == null)
            {
                return null;
            }
            else
            {
                return GetLinearCreative(linear, target, inline);
            }
        }

        public ActiveCreative GetLinearCreative(VASTADInLineCreativeLinear linear, IAdTarget adTarget, VASTADInLine ad, params VASTADWrapper[] wrappers)
        {
            IVpaid adPlayer;
            ICreativeSource adSource;
            var vPaidFactories = VastAdHandler.GetVpaidFactories().ToList();
            do
            {
                // get a list of all eligible media files
                var mediaAdSources = linear.MediaFiles.ToDictionary(m => m, m => new LinearSource(ad, m, linear, wrappers));

                var rankedMedia =
                    (from mediaAdSource in mediaAdSources
                     let vPaidFactoryAndPriority = vPaidFactories.ToDictionary(f => f, f => f.CheckSupport(mediaAdSource.Value, adTarget))
                     where vPaidFactoryAndPriority.Values.Any(v => v > PriorityCriteriaEnum.NotSupported)
                     let BestVpaidFactoryAndPriority = vPaidFactoryAndPriority.OrderByDescending(kvp => kvp.Value).First()
                     let rank = BestVpaidFactoryAndPriority.Value
                     orderby rank descending
                     select new
                     {
                         MediaFile = mediaAdSource.Key,
                         AdSource = mediaAdSource.Value,
                         VpaidFactory = BestVpaidFactoryAndPriority.Key,
                         Rank = rank,
                     }).ToList();

                if (rankedMedia.Any())
                {
                    // get all media with the best rankings
                    var topRank = rankedMedia.First().Rank;
                    var bestMedia = rankedMedia.Where(m => m.Rank == topRank);

                    // favor adaptive media
                    var adaptiveMedia = bestMedia.Where(m => m.MediaFile.delivery == VASTADInLineCreativeLinearMediaFileDelivery.streaming);
                    if (adaptiveMedia.Any())
                        bestMedia = adaptiveMedia;

                    double targetBitrateKbps = (double)VastAdHandler.AdHost.PlaybackBitrate / 1024;
                    if (targetBitrateKbps == 0.0)
                    {
                        // progressive videos won't have a bitrate. Therefore, target based on the one in the middle
                        targetBitrateKbps = rankedMedia.Where(m => m.MediaFile.bitrate.ToInt64().HasValue).Average(m => m.MediaFile.bitrate.ToInt64().Value);
                    }

                    // get the media with the closest bitrate
                    var bitrateMedia = bestMedia
                        .Where(m => m.MediaFile.bitrate.ToInt64().HasValue)
                        .GroupBy(m => Math.Abs(m.MediaFile.bitrate.ToInt64().Value))
                        .OrderBy(m => m.Key <= MaxBitrateKbps ? 0 : m.Key - MaxBitrateKbps)
                        .ThenBy(m => Math.Abs(m.Key - targetBitrateKbps))
                        .FirstOrDefault();
                    if (bitrateMedia != null && bitrateMedia.Any())
                        bestMedia = bitrateMedia;

                    //// get the media with the closest bitrate
                    //var selectedMedia = bestMedia
                    //    .Where(m => !string.IsNullOrEmpty(m.MediaFile.bitrate))
                    //    .OrderBy(m => Math.Abs(long.Parse(m.MediaFile.bitrate) - VastAdHandler.AdHost.PlaybackBitrate))
                    //    .FirstOrDefault();

                    // get the media with the closest size
                    var sizedMedia =
                        from m in bestMedia
                        where m.MediaFile.height.ToInt64().HasValue && m.MediaFile.width.ToInt64().HasValue
                        let x = VastAdHandler.AdHost.VideoArea.ActualHeight - m.MediaFile.height.ToInt64().Value
                        let y = VastAdHandler.AdHost.VideoArea.ActualWidth - m.MediaFile.width.ToInt64().Value
                        let delta = x + y
                        orderby delta
                        select new { Media = m, DeltaX = x, DeltaY = y };

                    // try to get the one with the closest size but both dimensions are within the current size
                    var selectedMedia = sizedMedia.Where(sm => sm.DeltaX >= 0 && sm.DeltaY >= 0).Select(sm => sm.Media).FirstOrDefault();
                    if (selectedMedia == null) // couldn't find one, instead get one with the closest size where only one dimension is over the current size
                        selectedMedia = sizedMedia.Where(sm => sm.DeltaX >= 0 || sm.DeltaY >= 0).Select(sm => sm.Media).FirstOrDefault();
                    if (selectedMedia == null) // couldn't find one, instead get one with the closest size
                        selectedMedia = sizedMedia.Select(sm => sm.Media).LastOrDefault();

                    // see if there were any bitrates, if not grab which ever one was first in the VAST file
                    if (selectedMedia == null)
                        selectedMedia = bestMedia.First();

                    //finally, get the ad player
                    adSource = selectedMedia.AdSource;
                    adPlayer = selectedMedia.VpaidFactory.GetVpaidPlayer(adSource, adTarget);

                    if (adPlayer == null)
                    {
                        //Log.Output(OutputType.Error, "Error - cannot find player to support video ad content. ");
                        // this should never happen and is the result of a bad VPaid plugin.
                        throw new Exception("VpaidFactory agreed to accept AdSource and then returned null during call to GetVPaidPlugin.");
                    }
                    // handshake with the ad player to make sure the version of VAST is OK
                    if (adPlayer == null || !vastAdHandler.VpaidController.Handshake(adPlayer))
                    {
                        // the version is no good, remove the factory from the list and try again.
                        vPaidFactories.Remove(selectedMedia.VpaidFactory);
                        if (!vPaidFactories.Any())
                        {
                            return null;
                        }
                        adPlayer = null;
                    }
                }
                else
                {
                    return null;
                }
            } while (adPlayer == null);

            //put video in target
            if (!adTarget.AddChild(adPlayer))
            {
                return null;
            }

            return new ActiveCreative(adPlayer, adSource, adTarget);
        }


#if LIMITBITRATE
        private const int DefaultMaxBitrateKbps = 1500;
#else
        private const int DefaultMaxBitrateKbps = int.MaxValue;
#endif

        public const string Key_MaxBitrateKbps = "Microsoft.Advertising.MaxBitrateKbps";
        private int? maxBitrateKbps;
        /// <summary>
        /// Indicates whtat the max bitrate for an ad can be. If there are no ads with bitrates below this theshold, the setting will be ignored.
        /// </summary>
        public int MaxBitrateKbps
        {
            get
            {
                if (!maxBitrateKbps.HasValue)
                {
                    maxBitrateKbps = GetMaxBitrateKbps();
                }
                return maxBitrateKbps.Value;
            }
            set
            {
                maxBitrateKbps = value;
            }
        }

        private int GetMaxBitrateKbps()
        {
            if (VastAdHandler.AdHost is IPlayer)
            {
                var player = (IPlayer)VastAdHandler.AdHost;
                if (player.GlobalConfigMetadata != null && player.GlobalConfigMetadata.ContainsKey(Key_MaxBitrateKbps))
                {
                    var maxBitrateKbpsObject = player.GlobalConfigMetadata[Key_MaxBitrateKbps];
                    if (maxBitrateKbpsObject is int)
                    {
                        return (int)maxBitrateKbpsObject;
                    }
                    else if (maxBitrateKbpsObject is string)
                    {
                        int maxBitrateKbpsResult;
                        if (int.TryParse(maxBitrateKbpsObject as string, out maxBitrateKbpsResult))
                        {
                            return maxBitrateKbpsResult;
                        }
                    }
                }
            }
            return DefaultMaxBitrateKbps;
        }

    }
}
