using System.Collections.Generic;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// The target or container of an ad. Controls placement of the ad.
    /// </summary>
    public interface IAdTarget
    {
        /// <summary>
        /// Adds a VPAID ad player to the container
        /// </summary>
        /// <param name="adPlayer">The VPAID ad player to add</param>
        /// <returns>Indicates success or failure to add the player</returns>
        bool AddChild(IVpaid adPlayer);
        /// <summary>
        /// Removes the VPAID ad player from the container.
        /// </summary>
        /// <param name="adPlayer">The VPAID ad player to add</param>
        void RemoveChild(IVpaid adPlayer);
        /// <summary>
        /// Provides the size of the container.
        /// </summary>
        Size Size { get; }
        /// <summary>
        /// The ad sequencing target associated with this target
        /// </summary>
        IAdSequencingTarget TargetSource { get; }
        /// <summary>
        /// The ad sequencing target dependencies that must be filled in order to place the ad.
        /// </summary>
        IEnumerable<IAdSequencingTarget> TargetDependencies { get; }
    }
}
