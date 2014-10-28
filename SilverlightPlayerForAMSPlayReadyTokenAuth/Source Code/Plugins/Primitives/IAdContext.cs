using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Contains information about a scheduled advertisement
    /// </summary>
    public interface IAdContext
    {
        /// <summary>
        /// The source of the ad content.
        /// </summary>
        Uri AdSource { get; }

        /// <summary>
        /// The URL where the user should be directed when they click the ad.
        /// </summary>
        Uri ClickThrough { get; }

        /// <summary>
        /// Indicates if the ad content is adaptive media.
        /// </summary>
        bool IsAdaptiveMedia { get; }

        /// <summary>
        /// The state of the ad.
        /// </summary>
        MediaPluginState CurrentAdState { get; }

        object Data { get; }

        /// <summary>
        /// Gets whether this ad has quartile events.
        /// </summary>
        bool? HasQuartileEvents { get; }

        /// <summary>
        /// Gets the natural duration of the ad content.
        /// </summary>
        TimeSpan? NaturalDuration { get; }

        /// <summary>
        /// Gets the playback duration of the ad content.
        /// </summary>
        TimeSpan? PlaybackDuration { get; }
    }
}