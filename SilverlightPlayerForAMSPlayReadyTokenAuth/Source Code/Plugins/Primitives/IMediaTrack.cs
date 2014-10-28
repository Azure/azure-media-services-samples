using System.Collections.Generic;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// A track that is part of an IMediaStream.
    /// </summary>
    public interface IMediaTrack
    {
        /// <summary>
        /// Gets the name of this IMediaTrack.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets whether this IMediaTrack is allowed.
        /// </summary>
        bool Allowed { get; }

        /// <summary>
        /// Gets the resolution of this IMediaTrack.
        /// </summary>
        Size Resolution { get; }

        /// <summary>
        /// Gets the index of this IMediaTrack within it's parent stream.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the language of this IMediaTrack.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Gets the stream that contains this IMediaTrack.
        /// </summary>
        IMediaStream ParentStream { get; }

        /// <summary>
        /// Gets the bitrate of this IMediaTrack.
        /// </summary>
        long Bitrate { get; }

        /// <summary>
        /// Gets the attributes that are a part of this IMediaTrack.
        /// </summary>
        IDictionary<string, string> CustomAttributes { get; }

        /// <summary>
        /// Gets the attributes that are a part of this IMediaTrack.
        /// </summary>
        IDictionary<string, string> Attributes { get; }
    }
}