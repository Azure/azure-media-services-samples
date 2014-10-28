using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{    
    /// <summary>
    /// The payload for a handled AdSequencingSource
    /// </summary>
    public interface IAdPayload
    {
        /// <summary>
        /// Deactivates the payload
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Provides notification that the payload has been deactivated
        /// </summary>
        event EventHandler Deactivated;
    }
}
