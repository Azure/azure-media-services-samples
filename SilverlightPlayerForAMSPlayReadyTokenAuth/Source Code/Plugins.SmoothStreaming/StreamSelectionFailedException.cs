using System;
using Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming.Resources;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// Indicates that selection of a stream has failed.
    /// </summary>
    public class StreamSelectionFailedException : Exception
    {
        public StreamSelectionFailedException()
            : base(SmoothStreamingResources.StreamSelectionFailedMessage)
        {
        }

        public StreamSelectionFailedException(Exception innerException)
            : base(SmoothStreamingResources.StreamSelectionFailedMessage, innerException)
        {
        }
    }
}