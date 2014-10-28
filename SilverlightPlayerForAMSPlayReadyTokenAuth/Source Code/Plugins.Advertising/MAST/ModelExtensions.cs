using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    public partial class Trigger
    {
        /// <summary>
        /// Implements the Duration property for a MAST trigger.
        /// This will always return null since MAST triggers don't support durations. Instead they are removed based on end conditions instead
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                return null;
            }
        }
    }
}
