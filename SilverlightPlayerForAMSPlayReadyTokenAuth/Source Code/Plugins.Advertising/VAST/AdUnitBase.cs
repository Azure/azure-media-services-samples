using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// The individual ad unit for a single VAST document.
    /// </summary>
    public abstract class AdUnitBase : IAsyncAdPayload
    {
        /// <summary>
        /// Creates a new VastAdUnit to serve as a handle to the running ad
        /// </summary>
        /// <param name="Source">The source associated with this ad</param>
        public AdUnitBase(IAdSource Source)
        {
            this.Source = Source;
        }

        /// <summary>
        /// The ad pod associated with this ad unit.
        /// </summary>
        public AdPod AdPod { get; set; }

        /// <summary>
        /// The source for the ad unit
        /// </summary>
        public IAdSource Source { get; private set; }

        /// <summary>
        /// Indicates that the payload has been deactivated from an outside source.
        /// </summary>
        public event EventHandler Deactivated;

        /// <summary>
        /// Forces the payload to deactivate.
        /// </summary>
        public void Deactivate()
        {
            if (Deactivated != null)
                Deactivated(this, EventArgs.Empty);
        }

        /// <summary>
        /// Indicates that the payload has been activated/handled.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Tells us that the payload has been activated.
        /// </summary>
        public void OnStart()
        {
            if (Started != null)
                Started(this, EventArgs.Empty);
        }

        /// <summary>
        /// Indicates that the payload has failed.
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// Tells us that the payload has failed.
        /// </summary>
        public void OnFail()
        {
            if (Failed != null)
                Failed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns the current state of the ad unit (loading, ready, failed).
        /// </summary>
        public StateEnum State
        {
            get { return StateEnum.Ready; }
        }

        /// <summary>
        /// Indicates the state has changed.
        /// </summary>
        public event EventHandler StateChanged;
    }
}