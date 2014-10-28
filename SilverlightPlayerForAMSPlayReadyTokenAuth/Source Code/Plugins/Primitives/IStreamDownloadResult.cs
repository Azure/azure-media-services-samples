using System;
using System.IO;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// The result of a stream download request.
    /// </summary>
    public interface IStreamDownloadResult
    {
        /// <summary>
        /// Gets a stream that contains the downloaded data.
        /// </summary>
        Stream Stream { get; }

        /// <summary>
        /// Gets the type of stream this data was downloaded from.
        /// </summary>
        StreamType Type { get; }

        /// <summary>
        /// Gets the IDataChunk that was downloaded.
        /// </summary>
        IDataChunk DataChunk { get; }
    }
}