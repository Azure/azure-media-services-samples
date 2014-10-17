using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Enum used to indicates how the cooresponding VPAID plugin would play the ad creative.
    /// This is a bitwise enum and helps provide the priority that the given VPAID player should be given.
    /// </summary>
    [Flags]
    public enum PriorityCriteriaEnum
    {
        /// <summary>
        /// Indicates that the creative would be dynamic as opposed to static (e.g. videos are dynamic, images are static).
        /// </summary>
        Dynamic = 128,
        /// <summary>
        /// Indicates that the creative would be played natively in Silverlight.
        /// </summary>
        Native = 64,
        /// <summary>
        /// Indicates that the creative would be interactive.
        /// </summary>
        Interactive = 32,
        /// <summary>
        /// Indicates that the creative contains adaptive streaming content.
        /// </summary>
        Adaptive = 16,
        /// <summary>
        /// Indicates that the creative contains progressive content as opposed to adaptive.
        /// </summary>
        Progressive = 8,
        /// <summary>
        /// Indicates that the creative would be static such as an image.
        /// </summary>
        Static = 4,
        /// <summary>
        /// Indicates that the creative would be played inside the player by the ActiveMediaPlugin. This is preferrable to being played in a separate container.
        /// </summary>
        InPlayer = 2,
        /// <summary>
        /// Indicates that the creative would be played dynamically.
        /// </summary>
        Trump = 1,
        /// <summary>
        /// Back door to allow VPAID plugin developers to override the default implementations.
        /// </summary>
        NotSupported = 0
    }

    /// <summary>
    /// The interface used when implementing a VPAID plugin.
    /// Classes that implement this interface can participate in the list of VPAID plugins.
    /// </summary>
    public interface IVpaidFactory
    {
        /// <summary>
        /// Allows the factory to indicate what level of support the given ad would have if executed by its cooresponding VPAID player.
        /// This is used to help prioritize which plugin to use in the event that multiple VPAID plugins are capable of handling the same ad.
        /// </summary>
        /// <param name="AdSource">Provides information about the ad creative that is to be played.</param>
        /// <param name="AdTarget">Provides information about the target where the VPAID player is to be placed.</param>
        /// <returns>A bitwise enum used to help determine priority in the event that multiple VPAID plugins are capable of handling the same ad creative.</returns>
        PriorityCriteriaEnum CheckSupport(ICreativeSource AdSource, IAdTarget AdTarget);

        /// <summary>
        /// Returns a VPAID player to handle the ad.
        /// </summary>
        /// <param name="AdSource">Provides information about the ad creative that is to be played.</param>
        /// <param name="AdTarget">Provides information about the target where the VPAID player is to be placed.</param>
        /// <returns>The VPAID player that will be expected to play the ad creative creative.</returns>
        IVpaid GetVpaidPlayer(ICreativeSource AdSource, IAdTarget AdTarget);
    }
}
