using System.Collections.Generic;
using System;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Provides an interface for a smooth streaming sample agent
    /// </summary>
    internal interface ISampleAgent
    {
        /// <summary>
        /// Called to give the agent an opportunity to use the smooth streaming event
        /// </summary>
        /// <param name="entry">The smooth streaming event to process</param>
        void ProcessEvent(SmoothStreamingEvent entry);

        /// <summary>
        /// Called periodically to give the agent an opportunity to return data
        /// </summary>
        IEnumerable<SampleData> GetSamples(long currentTicks);
    }
}
