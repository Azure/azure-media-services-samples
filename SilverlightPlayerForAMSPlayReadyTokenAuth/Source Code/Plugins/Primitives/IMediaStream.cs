using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// A stream that is part of an adaptive media manifest.
    /// </summary>
    public interface IMediaStream
    {
        /// <summary>
        /// Gets the id of the IMediaStream.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the name of the IMediaStream.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the language of the IMediaStream.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Gets the type of the IMediaStream.
        /// </summary>
        StreamType Type { get; }

        /// <summary>
        /// Gets the subtype of the IMediaStream.
        /// </summary>
        string SubType { get; }

        /// <summary>
        /// Gets whether this IMediaStream is enabled.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets whether this IMediaStream is a sparse stream.
        /// </summary>
        bool IsSparseStream { get; }

        /// <summary>
        /// Gets the FourCC property of this IMeidaStream.
        /// </summary>
        string FourCC { get; }

        /// <summary>
        /// Gets the Duration of this IMediaStream.
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Gets the TimeScale of this IMediaStream.
        /// </summary>
        TimeSpan? TimeScale { get; }

        /// <summary>
        /// Gets the tracks available in this IMediaStream.
        /// </summary>
        IEnumerable<IMediaTrack> AvailableTracks { get; }

        /// <summary>
        /// Gets the tracks that are currently selected in this IMediaStream.
        /// </summary>
        IEnumerable<IMediaTrack> SelectedTracks { get; }

        /// <summary>
        /// Gets the child streams that are part of this IMediaStream.
        /// </summary>
        IEnumerable<IMediaStream> ChildStreams { get; }

        /// <summary>
        /// Gets the data chunks that can be downloaded from this IMediaStream.
        /// </summary>
        IEnumerable<IDataChunk> DataChunks { get; }

        /// <summary>
        /// Gets the attributes that are part of this IMediaStream.
        /// </summary>
        IDictionary<string, string> Attributes { get; }

        /// <summary>
        /// Sets the tracks that are selected in this IMediaStream.
        /// </summary>
        /// <param name="tracks">All of the tracks that should be selected when this operation completes.</param>
        void SetSelectedTracks(IEnumerable<IMediaTrack> tracks);

        /// <summary>
        /// Sets the tracks that are allowed to be used in this IMediaStream.
        /// </summary>
        /// <param name="tracks">The list of tracks that are allowed to be used.</param>
        void SetRestrictedTracks(IEnumerable<IMediaTrack> tracks);

    }
}