using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// A segment that is specified inside of an adaptive manifest.
    /// </summary>
    public interface ISegment
    {
        /// <summary>
        /// Gets the start position of this ISegment.
        /// </summary>
        TimeSpan StartPosition { get; }

        /// <summary>
        /// Gets the end position of this ISegment.
        /// </summary>
        TimeSpan EndPosition { get; }

        /// <summary>
        /// Gets the streams available in this ISegment.
        /// </summary>
        IEnumerable<IMediaStream> AvailableStreams { get; }

        /// <summary>
        /// Gets the streams that are currently selected within this ISegment.
        /// </summary>
        IEnumerable<IMediaStream> SelectedStreams { get; }

#if !WINDOWS_PHONE
        /// <summary>
        /// Sets the streans that are allowed to be used in this ISegment.
        /// </summary>
        /// <param name="streams">The list of streams that are allowed to be used.</param>
        void SetRestrictStreams(IEnumerable<IMediaStream> streams);
#endif
    }
}