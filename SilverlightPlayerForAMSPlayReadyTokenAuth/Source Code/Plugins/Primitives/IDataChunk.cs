using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Represents a chunk of data that can be downloaded from a stream.
    /// </summary>
    public interface IDataChunk
    {
        /// <summary>
        /// Gets the unique timestamp of this IDataChunk.
        /// </summary>
        TimeSpan Timestamp { get; }

        /// <summary>
        /// Gets the duration of this IDataChunk.
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets a list of attributes about this IDataChunk.
        /// </summary>
        IDictionary<string, string> Attributes { get; }
    }
}
