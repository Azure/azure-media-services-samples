using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using System;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Defines a plugin capable of handling advertisements
    /// </summary>
    public interface IAdPayloadHandlerPlugin : IPlugin, IPlayerConsumer
    {
        /// <summary>
        /// Accepts the ad and plays it ASAP
        /// </summary>
        /// <param name="Source">The source of the ad</param>
        /// <returns>A handle to the playing ad. Used to deactivate it from an outside source</returns>
        IAdPayload Handle(IAdSource Source);

        /// <summary>
        /// Raised when an AdSequencingTrigger fails. The criteria are based on the FailureStrategy property.
        /// </summary>
        event EventHandler<HandleCompletedEventArgs> HandleCompleted;
        
        /// <summary>
        /// The failure strategy to use when handling an AdSequencingTrigger. Indicates what happens when a subset of an ad fails (such as when a companion ad fails).
        /// </summary>
        FailurePolicyEnum FailurePolicy { get; set; }

        /// <summary>
        /// Indicates that companion ads should be allowed to continue running after the ad has completed.
        /// </summary>
        bool CloseCompanionsOnComplete { get; set; }

        /// <summary>
        /// The timeout for initializing/loading ads. Null indicates no timeout.
        /// </summary>
        TimeSpan? InitTimeout {get; set;}
        
        /// <summary>
        /// The timeout for starting an ad. Null indicates no timeout.
        /// </summary>
        TimeSpan? StartTimeout {get; set;}

        /// <summary>
        /// The timeout for stopping an ad. Null indicates no timeout.
        /// </summary>
        TimeSpan? StopTimeout { get; set; }
    }
}
