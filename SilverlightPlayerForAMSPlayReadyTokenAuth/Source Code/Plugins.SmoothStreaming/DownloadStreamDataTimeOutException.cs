using System;
using Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming.Resources;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// Indicates that a timeout has occurred while downloading stream data.
    /// </summary>
    public class DownloadStreamDataTimeOutException : Exception
    {
        public DownloadStreamDataTimeOutException()
            : base(SmoothStreamingResources.DownloadStreamDataTimeOutExceptionMessage)
        {
        }

        public DownloadStreamDataTimeOutException(Exception innerException)
            : base(SmoothStreamingResources.DownloadStreamDataTimeOutExceptionMessage, innerException)
        {
        }
    }
}