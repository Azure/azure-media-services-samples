using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// Represents one or Ads to be played in sequence.
    /// </summary>
    public class AdPod
    {
        /// <summary>
        /// The list of Ads to play in sequential order.
        /// </summary>
        public List<Ad> Ads { get; private set; }

        /// <summary>
        /// Indicates that the RunAsync operation has completed. Includes a success param.
        /// </summary>
        public event Action<AdPod, bool> RunCompleted;

        /// <summary>
        /// Indicates that the ad pod is no longer holding up the show. There may still be active companion ads but the player should treat this as if the ad is complete.
        /// </summary>
        public event Action<AdPod> ReleasePlayer;

        /// <summary>
        /// The failure policy for the AdPod.
        /// </summary>
        public FailurePolicyEnum FailurePolicy { get; set; }

        /// <summary>
        /// Indicates that non required creatives (companion ads) should be allowed to continue running after the ad has completed.
        /// </summary>
        public bool CloseCompanionsOnComplete { get; set; }

        /// <summary>
        /// Creates a new ad pod
        /// </summary>
        public AdPod()
        {
            FailurePolicy = FailurePolicyEnum.Ignore;
            Ads = new List<Ad>();
        }

        private Ad runningAd;
        private Queue<Ad> pendingAds;
        private bool IsLoading = true;

        /// <summary>
        /// Starts the AdPod. Calls RunCompleted when finished.
        /// </summary>
        public virtual bool RunAsync()
        {
            IsLoading = false;
            pendingAds = new Queue<Ad>(Ads.Where(a => a != null));
            if (pendingAds.Any())
            {
                RunNextInQueue();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RunNextInQueue()
        {
            if (pendingAds.Any())
            {
                if (runningAd != null && runningAd.RunningCreativeSet != null)
                {
                    // we had a soft cancel, which means that there are still running creativesets
                    runningAd.Cancel();
                }
                runningAd = pendingAds.Dequeue();
                runningAd.RunCompleted += ad_RunCompleted;
                runningAd.RunAsync();
            }
            else
            {
                if (runningAd == null || runningAd.RunningCreativeSet == null)
                {
                    // there are no actively running creative sets, we're done!
                    Cleanup();
                    OnCompleted(true);
                }
                else
                {
                    // there are still some creatives running but they are companion ads and can be trumped as soon as another ad comes along
                    if (ReleasePlayer != null) ReleasePlayer(this);
                }
            }
        }

        private void ad_RunCompleted(Ad ad, bool success)
        {
            ad.RunCompleted -= ad_RunCompleted;
            OnRunCompleted(success);
        }

        protected void OnRunCompleted(bool success)
        {
            if (!success && (FailurePolicy != FailurePolicyEnum.Ignore || !pendingAds.Any()))
            {
                Cleanup();
                OnCompleted(success);
            }
            else
            {
                RunNextInQueue();
            }
        }

        /// <summary>
        /// Cleans up the AdPod by canceling all running Ads if any exist.
        /// </summary>
        protected virtual void Cleanup()
        {
            // clean up
            pendingAds = null;

            if (runningAd != null)
            {
                runningAd.RunCompleted -= ad_RunCompleted;
                runningAd.Cancel();
                runningAd = null;
            }
        }

        /// <summary>
        /// Cancels the AdPod and any of it's running Ads.
        /// </summary>
        public void Cancel()
        {
            Cleanup();
            OnCompleted(CanShutdown);   // normally this would be a failure, but if there are only companion ads running then we can consider it a success.
        }

        private void OnCompleted(bool success)
        {
            if (ReleasePlayer != null) ReleasePlayer(this);
            if (RunCompleted != null) RunCompleted(this, success);
        }

        /// <summary>
        /// Indicates that the ad can be replaced with another.
        /// </summary>
        /// <returns>true if the ad can be shut down without prematurely turning off the ad</returns>
        public bool CanShutdown
        {
            get
            {
                if (IsLoading)
                {
                    return false;
                }
                else
                {
                    return runningAd == null || (!pendingAds.Any() && runningAd.CanShutdown);
                }
            }
        }
    }

    /// <summary>
    /// Represents a single Ad, which contains many creatives (which can be grouped into CreativeSets).
    /// </summary>
    public class Ad
    {
        /// <summary>
        /// Indicates that non required creatives (companion ads) should be allowed to continue running after the ad has completed.
        /// </summary>
        public bool CloseCompanionsOnComplete { get; set; }

        /// <summary>
        /// Contains a list of Creatives grouped by sequence. These are to be played in sequential order.
        /// </summary>
        public List<CreativeSet> CreativeSets { get; private set; }

        /// <summary>
        /// Indicates that the RunAsync operation has completed. Includes a success param.
        /// </summary>
        public event Action<Ad, bool> RunCompleted;

        /// <summary>
        /// The failure strategy for the Ad.
        /// </summary>
        public FailurePolicyEnum FailureStrategy { get; set; }

        /// <summary>
        /// Createsa  new ad object
        /// </summary>
        public Ad()
        {
            CreativeSets = new List<CreativeSet>();
        }

        private Queue<CreativeSet> pendingCreativeSets;
        private CreativeSet runningCreativeSet;

        /// <summary>
        /// Returns the currently running creative set
        /// </summary>
        public CreativeSet RunningCreativeSet
        {
            get { return runningCreativeSet; }
        }

        /// <summary>
        /// Starts the Ad. Calls RunCompleted when finished.
        /// </summary>
        public void RunAsync()
        {
            pendingCreativeSets = new Queue<CreativeSet>(CreativeSets);
            RunNextInQueue();
        }

        private void RunNextInQueue()
        {
            if (pendingCreativeSets.Any())
            {
                IEnumerable<Creative> creativesToMerge;
                if (runningCreativeSet != null && runningCreativeSet.RunningCreatives != null)
                {
                    creativesToMerge = runningCreativeSet.RunningCreatives;
                    runningCreativeSet.RunningCreatives = null;
                }
                else
                {
                    creativesToMerge = Enumerable.Empty<Creative>();
                }
                runningCreativeSet = pendingCreativeSets.Dequeue();
                runningCreativeSet.RunCompleted += creativeSet_RunCompleted;
                runningCreativeSet.RunAsync(creativesToMerge);
            }
            else
            {
                if (!CloseCompanionsOnComplete && runningCreativeSet != null && runningCreativeSet.RunningCreatives != null)
                {
                    // don't cleanup, let everything continue to run
                    OnCompleted(true);
                }
                else
                {
                    Cleanup();
                    OnCompleted(true);
                }
            }
        }

        private void creativeSet_RunCompleted(CreativeSet creative, bool success)
        {
            creative.RunCompleted -= creativeSet_RunCompleted;
            if (!success && (FailureStrategy != FailurePolicyEnum.Ignore || !pendingCreativeSets.Any()))
            {
                Cleanup();
                OnCompleted(success);
            }
            else
            {
                RunNextInQueue();
            }
        }

        /// <summary>
        /// Cancels all running CreativeSets and cleans up.
        /// </summary>
        protected virtual void Cleanup()
        {
            // clean up
            pendingCreativeSets = null;

            if (runningCreativeSet != null)
            {
                runningCreativeSet.RunCompleted -= creativeSet_RunCompleted;
                runningCreativeSet.Cancel();
                runningCreativeSet = null;
            }
        }

        /// <summary>
        /// Cancel the ad
        /// </summary>
        public void Cancel()
        {
            Cleanup();
            OnCompleted(false);
        }

        private void OnCompleted(bool success)
        {
            if (RunCompleted != null) RunCompleted(this, success);
        }

        /// <summary>
        /// Indicates that the ad can be replaced with another.
        /// </summary>
        /// <returns>true if the ad can be shut down without prematurely turning off the ad</returns>
        public bool CanShutdown
        {
            get
            {
                return runningCreativeSet == null || (!pendingCreativeSets.Any() && runningCreativeSet.CanShutdown);
            }
        }
    }

    /// <summary>
    /// Represents a set of creatives to be played at the same time.
    /// </summary>
    public class CreativeSet
    {
        /// <summary>
        /// The list of creatives for this specific group.
        /// </summary>
        public List<Creative> Creatives { get; private set; }

        /// <summary>
        /// Indicates the creatives are starting.
        /// </summary>
        public event Action<CreativeSet> RunStarting;

        /// <summary>
        /// Indicates the creatives have successfully started.
        /// </summary>
        public event Action<CreativeSet> RunStarted;

        /// <summary>
        /// Indicates the creatives have all finished.
        /// </summary>
        public event Action<CreativeSet, bool> RunCompleted;

        /// <summary>
        /// Provides the failure strategy for the CreativeSet.
        /// </summary>
        public FailurePolicyEnum FailureStrategy { get; set; }

        public CreativeSet()
        {
            Creatives = new List<Creative>();
        }

        private List<Creative> requiredCreatives;
        private List<Creative> runningCreatives;

        /// <summary>
        /// Returns the collection of actively running creatives
        /// </summary>
        public ReadOnlyCollection<Creative> RunningCreatives
        {
            get
            {
                return runningCreatives == null ? null : runningCreatives.AsReadOnly();
            }
            internal set
            {
                if (value == null)
                    runningCreatives = null;
                else
                    runningCreatives = value.ToList();
            }
        }

        /// <summary>
        /// Starts the creative set (which will launch all creatives simultaneously).
        /// </summary>
        public void RunAsync(IEnumerable<Creative> mergeCreatives)
        {
            if (RunStarting != null) RunStarting(this);

            List<Creative> loadedCreatives = new List<Creative>();
            foreach (var creative in Creatives)
            {
                if (creative.Load())
                {
                    loadedCreatives.Add(creative);
                }
                else
                {
                    creative.Failed();
                    if (FailureStrategy != FailurePolicyEnum.Ignore)
                    {
                        OnCompleted(false);
                        return;
                    }
                }
            }

            // allow the list of creatives to be filtered. This is used for assessing dependency creatives
            var allowedCreatives = GetAllowedCreatives(loadedCreatives);
            // report back about all the creatives that were filtered
            foreach (var creative in Creatives.Where(c => !allowedCreatives.Contains(c)))
            {
                creative.Failed();
            }
            // make sure at least one creative was successfully loaded
            if (!allowedCreatives.Any())
            {
                OnCompleted(false);
            }
            else
            {
                requiredCreatives = new List<Creative>();
                runningCreatives = new List<Creative>(mergeCreatives);
                foreach (var creative in allowedCreatives)
                {
                    runningCreatives.Add(creative);
                    creative.RunCompleted += creative_RunCompleted;
                    creative.RunAsync();
                    if (creative.ControlsLifespan) requiredCreatives.Add(creative);
                }

                foreach (var mergedCreative in mergeCreatives)
                {
                    mergedCreative.RunCompleted += creative_RunCompleted;
                }

                if (!requiredCreatives.Any())
                {
                    // we're done with the required creatives.
                    SoftCleanup();
                    OnCompleted(true);
                }
                else
                {
                    // we're good!
                    if (RunStarted != null) RunStarted(this);
                }
            }
        }

        /// <summary>
        /// Filters the list of creatives. This allows a subclass to do it's own filtering.
        /// </summary>
        /// <param name="Creatives">The creatives to be filtered.</param>
        /// <returns>The new list of creatives.</returns>
        protected virtual IEnumerable<Creative> GetAllowedCreatives(IEnumerable<Creative> Creatives)
        {
            return Creatives;
        }

        private void creative_RunCompleted(Creative creative, bool success)
        {
            runningCreatives.Remove(creative);
            creative.RunCompleted -= creative_RunCompleted;
            if (!success && FailureStrategy != FailurePolicyEnum.Ignore)
            {
                Cleanup();
                OnCompleted(success);
            }
            else
            {
                if (requiredCreatives.Contains(creative))
                {
                    requiredCreatives.Remove(creative);
                }

                if (!runningCreatives.Any())
                {
                    // we're done
                    Cleanup();
                    OnCompleted(success);
                }
                else if (!requiredCreatives.Any())
                {
                    // we're done with the required creatives.
                    SoftCleanup();
                    OnCompleted(success);
                }
            }
        }

        /// <summary>
        /// Indicates that the ad can be replaced with another. Returns true if the ad can be shut down without prematurely turning off the ad.
        /// </summary>
        public bool CanShutdown
        {
            get
            {
                return requiredCreatives == null || !requiredCreatives.Any();
            }
        }

        /// <summary>
        /// Cancels all required creatives and cleans up event handlers for running creatives
        /// </summary>
        protected virtual void SoftCleanup()
        {
            // clean up
            requiredCreatives = null;
            if (runningCreatives != null)
            {
                foreach (var creative in runningCreatives)
                {
                    creative.RunCompleted -= creative_RunCompleted;
                }
            }
        }

        /// <summary>
        /// Cancels all running creatives and cleans up the creative set.
        /// </summary>
        protected virtual void Cleanup()
        {
            // clean up
            requiredCreatives = null;
            if (runningCreatives != null)
            {
                foreach (var creative in runningCreatives)
                {
                    creative.RunCompleted -= creative_RunCompleted;
                    creative.Cancel();
                }
            }
            runningCreatives = null;
        }

        /// <summary>
        /// Cancels all running creatives and raises the RunCompleted event.
        /// </summary>
        public void Cancel()
        {
            Cleanup();
            OnCompleted(false);
        }

        private void OnCompleted(bool success)
        {
            if (RunCompleted != null) RunCompleted(this, success);
        }
    }

    /// <summary>
    /// Describes the required properties for an individual creative
    /// </summary>
    public abstract class Creative
    {
        /// <summary>
        /// The ID of the creative
        /// </summary>
        public abstract string Id { get; }
        /// <summary>
        /// Indicates whether or not the creative is required to finish on it's own before the current CreativeSet is considered complete and next CreativeSet will begin.
        /// </summary>
        public abstract bool ControlsLifespan { get; }
        /// <summary>
        /// Loads the current creative set.
        /// </summary>
        /// <returns>Returns false if there was a problem such as finding a suitable VPaid plugin.</returns>
        public abstract bool Load();
        /// <summary>
        /// Runs the creative. Fires RunCompleted when done.
        /// </summary>
        public abstract void RunAsync();
        /// <summary>
        /// Forces the current creative to cancel.
        /// </summary>
        public abstract void Cancel();
        /// <summary>
        /// Raised when the creative has completed running.
        /// </summary>
        public event Action<Creative, bool> RunCompleted;
        /// <summary>
        /// Called by the VastAdHandler to tell us that we failed.
        /// </summary>
        public virtual void Failed()
        {
            OnRunCompleted(false);
        }
        /// <summary>
        /// Called by the VastAdHandler to tell us that we succeeded.
        /// </summary>
        public virtual void Succeeded()
        {
            OnRunCompleted(true);
        }

        private void OnRunCompleted(bool success)
        {
            if (RunCompleted != null)
                RunCompleted(this, success);
        }
    }
}
