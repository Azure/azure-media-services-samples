using System;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    /// <summary>
    /// EventArgs used to send information about when an individual VAST ad has completed running.
    /// Note: There may be multiple VAST ads per AdSequencingTrigger. Each is run in succession.
    /// </summary>
    public class AdCompletedEventArgs : EventArgs
    {
        readonly Ad ad;
        readonly bool success;
        public AdCompletedEventArgs(Ad Ad, bool Success)
        {
            ad = Ad;
            success = Success;
        }

        /// <summary>
        /// The Ad associated with this operation
        /// </summary>
        public Ad Ad { get { return ad; } }

        /// <summary>
        /// Flag indicating success vs. failure. Success criteria is based on the FailureStrategy property.
        /// </summary>
        public bool Success { get { return success; } }
    }
}
