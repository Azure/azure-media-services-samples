using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Represents all the information necessary to render a VPAID ad.
    /// This will be used to find an appropriate VPAID plugin and then the VPAID plugin will use it to play the ad.
    /// </summary>
    public interface ICreativeSource
    {
        /// <summary>
        /// The MIME type of the ad
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// The payload of the creative. This is usually a URL depending on the MediaSourceType but can also contain HTML.
        /// </summary>
        string MediaSource { get; }

        /// <summary>
        /// Additional information associated with the creative to help VPAID play the ad.
        /// </summary>
        string ExtraInfo { get; }

        /// <summary>
        /// The URL that should be launched if the ad is clicked. This is different from the URL used to track clicks.
        /// </summary>
        string ClickUrl { get; }

        /// <summary>
        /// Called when a particular tracking event occurs
        /// </summary>
        /// <param name="TrackingEvent">The tracking event that occured</param>
        void Track(TrackingEventEnum TrackingEvent);

        /// <summary>
        /// The duration of the ad. Companion ads do not have durations. And durations for NonLinear ads are optional.
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Indicates what the MediaSource contains.
        /// </summary>
        MediaSourceEnum MediaSourceType { get; }

        /// <summary>
        /// Provides the alternate text for the target
        /// </summary>
        string AltText { get; }

        /// <summary>
        /// Indicates whether or not the source is streaming vs. progressive
        /// </summary>
        bool IsStreaming { get; }

        /// <summary>
        /// The expanded dimensions of the creative.
        /// </summary>
        Size ExpandedDimensions { get; }

        /// <summary>
        /// The dimensions of the creative.
        /// </summary>
        Size Dimensions { get; }

        /// <summary>
        /// Indicates whether or not the creative can scale in size.
        /// </summary>
        bool IsScalable { get; }

        /// <summary>
        /// Indicates whether or not the aspect ratio should be maintained.
        /// </summary>
        bool MaintainAspectRatio { get; }

        /// <summary>
        /// Indicates how the creative is intended to be used.
        /// </summary>
        CreativeSourceType Type { get; }

        /// <summary>
        /// The ID of the Ad Creative
        /// </summary>
        string Id { get; }
    }

    /// <summary>
    /// The possible values for how a creative is intended to be used.
    /// </summary>
    public enum CreativeSourceType
    {
        Linear,
        NonLinear,
        Companion
    }

    /// <summary>
    /// The various kinds of media sources.
    /// </summary>
    public enum MediaSourceEnum
    {
        Static,
        HTML,
        IFrame
    }

    /// <summary>
    /// The supported tracking event keys
    /// </summary>
    public enum TrackingEventEnum
    {
        impression,
        click,
        error,
        creativeView,
        start,
        midpoint,
        firstQuartile,
        thirdQuartile,
        complete,
        mute,
        unmute,
        pause,
        rewind,
        resume,
        fullscreen,
        expand,
        collapse,
        acceptInvitation,
        close,
    }
}
