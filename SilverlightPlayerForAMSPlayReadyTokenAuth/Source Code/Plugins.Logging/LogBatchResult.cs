using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// The result from a successful async batch log operation.
    /// </summary>
    public class LogBatchResult
    {
        /// <summary>
        /// Indicates that the main log agent should continue to run. This is essentiall a kill switch.
        /// </summary>
        public bool? IsEnabled;

        /// <summary>
        /// The new polling interval. Overrides the one specified in the config.
        /// </summary>
        public TimeSpan? QueuePollingInterval;

        /// <summary>
        /// The server time. Used to calibrate the timestamp sent on the logs.
        /// </summary>
        public DateTimeOffset? ServerTime;
    }
}
