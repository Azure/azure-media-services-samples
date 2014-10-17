
namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    /// <summary>
    /// Provides the choices for how to handle failures.
    /// </summary>
    public enum FailurePolicyEnum
    {
        /// <summary>
        /// No matter what fails, ignore it. e.g. If a companion ad fails, still play the associated linear ad.
        /// </summary>
        Ignore,
        /// <summary>
        /// If there is any failure at any level, cancel out of the entire AdSpot.
        /// </summary>
        Abort
    }
}
