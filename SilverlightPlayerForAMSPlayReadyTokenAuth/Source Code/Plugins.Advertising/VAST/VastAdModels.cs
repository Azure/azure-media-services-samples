using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// Represents a single VAST document, which can contain many Ads.
    /// </summary>
    public class VastAdPod : AdPod
    {
        /// <summary>
        /// A reference to a factory that can help create and populate these models at the appropriate times.
        /// </summary>
        internal VastCreativeFactory AdModelFactory { get; private set; }

        /// <summary>
        /// The VAST AdUnit associated with this model. This object provides information about the source used to load this AdPod.
        /// </summary>
        public VastAdUnit AdUnit { get; private set; }

        internal VastAdPod(VastAdUnit AdUnit, VastCreativeFactory AdModelFactory)
            : base()
        {
            this.AdUnit = AdUnit;
            this.AdModelFactory = AdModelFactory;
        }

        /// <summary>
        /// Starts loading the AdPod. This calls out to the AdModelFactory to do much of the actual downloading and processing of wrapper ads.
        /// </summary>
        /// <param name="Completed">Callback to notify when we are finished. Passes a success param.</param>
        public void LoadAsync(Action<bool> Completed)
        {
            AdModelFactory.VastAdHandler.LoadAdUnitAsync(AdUnit, success =>
            {
                if (success)
                {
                    // if ad is a wrapper, load it
                    AdModelFactory.VastAdHandler.UnWrapAdPod(this, () =>
                    {
                        // now that we're done loading wrappers, add the Ads. Note: VastAd can only hold inline ads
                        foreach (VASTAD item in AdUnit.Vast.Ad)
                        {
                            base.Ads.Add(new VastAd(item, this));
                        }
                        Completed(true);
                    });
                }
                else
                {
                    Completed(false);
                }
            });
        }
    }

    /// <summary>
    /// Represents a single VAST ad. Cooresponds to the Ad node of the VAST document.
    /// </summary>
    public class VastAd : Ad
    {
        /// <summary>
        /// A reference to the parent AdPod.
        /// </summary>
        internal VastAdPod Parent { get; private set; }

        /// <summary>
        /// The VAST Ad model that came directly from the Ad node of the VAST document.
        /// </summary>
        public VASTAD Ad { get; private set; }

        internal VastAd(VASTAD Ad, VastAdPod Parent)
            : base()
        {
            this.Ad = Ad;
            this.Parent = Parent;
            this.FailureStrategy = Parent.FailurePolicy;
            this.CloseCompanionsOnComplete = Parent.CloseCompanionsOnComplete;

            if (Ad.Item is VASTADInLine)
            {
                var inlineAd = (VASTADInLine)Ad.Item;
                foreach (var item in (inlineAd.Creatives.GroupBy(c => c.sequence.ToInt()).OrderBy(i => i.Key)))
                {
                    base.CreativeSets.Add(new VastCreativeSet(item, this));
                }
            }
            else
            {
                throw new ArgumentException("Ad can only contain Inline ads. Wrapper ads need to be processed beforehand");
            }
        }
    }

    /// <summary>
    /// Represents a group of creatives intended to play at the same time.
    /// </summary>
    public class VastCreativeSet : CreativeSet
    {
        /// <summary>
        /// A reference to the parent AdPod.
        /// </summary>
        internal VastAd Parent { get; private set; }

        /// <summary>
        /// A collection of inline creatives with the same sequence number. These come directly from the deserialized VAST document.
        /// </summary>
        public IEnumerable<VASTADInLineCreative> InlineCreatives { get; private set; }

        internal VastCreativeSet(IEnumerable<VASTADInLineCreative> InlineCreatives, VastAd Parent)
            : base()
        {
            this.InlineCreatives = InlineCreatives;
            this.Parent = Parent;
            this.FailureStrategy = Parent.FailureStrategy;
            foreach (var item in InlineCreatives)
            {
                object creativeItem = item.Item;
                if (creativeItem is VASTADInLineCreativeLinear)
                {
                    var linear = creativeItem as VASTADInLineCreativeLinear;
                    base.Creatives.Add(new LinearVastCreative(linear, item, this));
                }
                else if (creativeItem is VASTADInLineCreativeNonLinearAds)
                {
                    var nonLinears = creativeItem as VASTADInLineCreativeNonLinearAds;
                    if (nonLinears.NonLinear != null)
                    {
                        foreach (NonLinear_type nl in nonLinears.NonLinear)
                        {
                            base.Creatives.Add(new NonLinearVastCreative(nl, item, this));
                        }
                    }
                }
                else if (creativeItem is VASTADInLineCreativeCompanionAds)
                {
                    var companions = creativeItem as VASTADInLineCreativeCompanionAds;
                    if (companions.Companion != null)
                    {
                        foreach (Companion_type comp in companions.Companion)
                        {
                            base.Creatives.Add(new CompanionVastCreative(comp, item, this));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Indicates whether or not the creative set contains a linear creative.
        /// </summary>
        public bool ContainsLinear
        {
            get
            {
                return base.Creatives.OfType<LinearVastCreative>().Any();
            }
        }

        /// <summary>
        /// Filters a list of creatives based on target dependencies.
        /// If one creative uses a target that has a dependency on another target, only permit the creative if the dependency target was used.
        /// </summary>
        /// <param name="Creatives">The list of creatives to filter.</param>
        /// <returns>The filtered list of creatives.</returns>
        protected override IEnumerable<Creative> GetAllowedCreatives(IEnumerable<Creative> Creatives)
        {
            var usedTargets = Creatives.OfType<VastCreative>().Select(c => c.ActiveCreative.Target.TargetSource).ToList();
            foreach (var Creative in Creatives)
            {
                // only run creatives that have successfull dependency targets
                if (Creative is VastCreative)
                {
                    var vastCreative = Creative as VastCreative;
                    IAdTarget target = vastCreative.ActiveCreative.Target;

                    bool dependencyFailure = false;
                    foreach (var targetSource in target.TargetDependencies)
                    {
                        if (!usedTargets.Contains(targetSource))
                        {
                            dependencyFailure = true;
                            break;
                        }
                    }
                    if (!dependencyFailure)
                    {
                        yield return Creative;
                    }
                }
                else
                {
                    yield return Creative;
                }
            }
        }
    }

    /// <summary>
    /// A creative for a companion ad
    /// </summary>
    public class CompanionVastCreative : VastCreative
    {
        /// <summary>
        /// The model that comes from the VAST document representing the specific companion creative.
        /// </summary>
        public Companion_type Companion { get; private set; }

        internal CompanionVastCreative(Companion_type Companion, VASTADInLineCreative Creative, VastCreativeSet Parent)
            : base(Creative, Parent)
        {
            this.Companion = Companion;
        }

        public override string Id
        {
            get { return Companion.id; }
        }

        /// <summary>
        /// Always returns false. Companions never control the lifespan.
        /// </summary>
        public override bool ControlsLifespan
        {
            get { return false; }
        }

        /// <summary>
        /// Loads the ActiveCreative.
        /// </summary>
        /// <returns>Indicates success.</returns>
        public override bool Load()
        {
            ActiveCreative = AdModelFactory.GetCompanionCreative(Parent.Parent.Parent, Parent.Parent.Ad.Item as VASTADInLine, Companion);
            return (ActiveCreative != null);
        }
    }

    /// <summary>
    /// A creative for a linear ad
    /// </summary>
    public class LinearVastCreative : VastCreative
    {
        /// <summary>
        /// The model that comes from the VAST document representing the specific linear creative.
        /// </summary>
        public VASTADInLineCreativeLinear Linear { get; private set; }

        internal LinearVastCreative(VASTADInLineCreativeLinear Linear, VASTADInLineCreative Creative, VastCreativeSet Parent)
            : base(Creative, Parent)
        {
            this.Linear = Linear;
        }

        private string id;
        public override string Id { get { return id; } }

        /// <summary>
        /// Always returns true. Linear ads are expected to have durations and control the lifespan.
        /// </summary>
        public override bool ControlsLifespan
        {
            get { return true; }
        }

        /// <summary>
        /// Loads the ActiveCreative.
        /// </summary>
        /// <returns>Indicates success.</returns>
        public override bool Load()
        {
            ActiveCreative = AdModelFactory.GetLinearCreative(Parent.Parent.Ad.Item as VASTADInLine, Creative, Linear, Parent.Parent.Parent.AdUnit.Source);
            if (ActiveCreative != null)
            {
                id = ActiveCreative.Source.Id;
            }
            return (ActiveCreative != null);
        }
    }

    /// <summary>
    /// A creative for a nonlinear ad
    /// </summary>
    public class NonLinearVastCreative : VastCreative
    {
        /// <summary>
        /// The model that comes from the VAST document representing the specific non linear creative.
        /// </summary>
        public NonLinear_type NonLinear { get; private set; }

        internal NonLinearVastCreative(NonLinear_type NonLinear, VASTADInLineCreative Creative, VastCreativeSet Parent)
            : base(Creative, Parent)
        {
            this.NonLinear = NonLinear;
        }

        public override string Id
        {
            get { return NonLinear.id; }
        }

        /// <summary>
        /// Always returns true. Nonlinear ads are expected to control the lifespan.
        /// </summary>
        public override bool ControlsLifespan
        {
            get { return true; }
        }

        /// <summary>
        /// Loads the ActiveCreative.
        /// </summary>
        /// <returns>Indicates success.</returns>
        public override bool Load()
        {
            ActiveCreative = AdModelFactory.GetNonLinearCreative(Parent.Parent.Ad.Item as VASTADInLine, Creative.Item as VASTADInLineCreativeNonLinearAds, NonLinear, Parent.Parent.Parent.AdUnit.Source);
            return (ActiveCreative != null);
        }
    }

    /// <summary>
    /// The base class for a creative
    /// </summary>
    public abstract class VastCreative : Creative
    {
        /// <summary>
        /// The deserialized model from the VAST document.
        /// </summary>
        protected VASTADInLineCreative Creative;

        /// <summary>
        /// An object that represents a running creative.
        /// </summary>
        public ActiveCreative ActiveCreative { get; protected set; }

        /// <summary>
        /// The creative set that creative belongs to.
        /// </summary>
        public VastCreativeSet Parent { get; private set; }

        internal VastCreative(VASTADInLineCreative Creative, VastCreativeSet Parent)
        {
            this.Parent = Parent;
            this.Creative = Creative;
        }

        /// <summary>
        /// Actually runs the creative.
        /// </summary>
        public override void RunAsync()
        {
            AdModelFactory.VastAdHandler.PlayCreative(this);
        }

        /// <summary>
        /// Cancels the running creative.
        /// </summary>
        public override void Cancel()
        {
            AdModelFactory.VastAdHandler.CancelCreative(this);
            Failed();
        }

        /// <summary>
        /// Shortcut to the AdModelFactory.
        /// Should be protected AND internal but no way in C# to indicate this. 'protected internal' notation does OR.
        /// </summary>
        internal VastCreativeFactory AdModelFactory
        {
            get
            {
                return Parent.Parent.Parent.AdModelFactory;
            }
        }
    }
}