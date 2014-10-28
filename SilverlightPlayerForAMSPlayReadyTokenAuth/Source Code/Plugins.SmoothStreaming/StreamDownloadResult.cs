using System;
using System.IO;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// The result of a stream download request.
    /// </summary>
    public class StreamDownloadResult : IStreamDownloadResult
    {
        #region IStreamDownloadResult Members
        /// <summary>
        /// Gets a stream that contains the downloaded data.
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// Gets the type of stream this data was downloaded from.
        /// </summary>
        public StreamType Type { get; set; }

        /// <summary>
        /// Gets the data chunk that was downloaded.
        /// </summary>
        public IDataChunk DataChunk { get; set; }

        #endregion
    }
}