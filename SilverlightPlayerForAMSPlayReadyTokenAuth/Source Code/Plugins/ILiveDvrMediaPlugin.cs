using System;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Represents a media plug-in with live DVR capabilities.
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// This interface keeps track of the current live position, which may be different than the current position 
    /// if the user has scrubbed back to an earlier point in the video using the timeline.
    /// 
    /// <para>
    /// You implement this interface for a plug-in that supports live media streams.
    /// </para>
    /// </remarks>
    public interface ILiveDvrMediaPlugin : IMediaPlugin
    {
        /// <summary>
        /// Gets a value indicating whether the current media is live.
        /// </summary>
        bool IsSourceLive { get; }

        /// <summary>
        /// Gets a value indicating whether playback is currently at the live position.
        /// </summary>
        /// <remarks>
        /// This property is only used on a live media stream with DVR capabilities, such as an adaptive stream (where the client can scrub to a 
        /// point on the timeline previous to the live position).
        /// </remarks>
        bool IsLivePosition { get; }

        /// <summary>
        /// Gets the live playback position.
        /// </summary>
        /// <remarks>
        /// This property is only used on a live media stream with DVR capabilities, such as an adaptive stream (where the client can scrub to a 
        /// point on the timeline previous to the live position).
        /// </remarks>
        TimeSpan LivePosition { get; }

        /// <summary>
        /// Gets/Sets the position within a live media where playback should start.
        /// </summary>
        LivePlaybackStartPosition LivePlaybackStartPosition { get; set; }

        /// <summary>
        /// Occurs when a live event has completed.
        /// </summary>
        event Action<ILiveDvrMediaPlugin> LiveEventCompleted;
    }
}