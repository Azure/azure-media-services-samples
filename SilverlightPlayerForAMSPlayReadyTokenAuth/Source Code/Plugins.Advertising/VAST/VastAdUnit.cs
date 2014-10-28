using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// The individual ad unit for a single VAST document.
    /// </summary>
    public class VastAdUnit : AdUnitBase
    {
        /// <summary>
        /// Creates a new VastAdUnit to serve as a handle to the running ad
        /// </summary>
        /// <param name="Source">The source associated with this ad</param>
        internal VastAdUnit(IAdSource Source)
            : base(Source)
        { }

        /// <summary>
        /// The VAST document that the source refered to. This will be null until populated.
        /// </summary>
        public VAST Vast { get; internal set; }

        /// <summary>
        /// Returns the VAST ad pod associated with this ad unit
        /// </summary>
        public new VastAdPod AdPod
        {
            get { return base.AdPod as VastAdPod; }
            set { base.AdPod = value; }
        }
    }
}