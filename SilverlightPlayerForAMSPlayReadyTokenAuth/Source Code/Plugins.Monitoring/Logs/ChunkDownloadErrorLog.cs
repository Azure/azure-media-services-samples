
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// Errors have occured when downloading a chunk
    /// </summary>
    public class ChunkDownloadErrorLog : VideoEventLog
    {
        public ChunkDownloadErrorLog()
            : base(VideoLogTypes.ChunkDownloadError)
        {
        }

        /// <summary>
        /// The number of errors that occured for a given chunk in the sampling window.
        /// </summary>
        public int? Count
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.DownloadErrorCount);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.DownloadErrorCount, value);
            }
        }

        /// <summary>
        /// The timestamp index of the chunk. This will coorespond to the index in the manifest. This and the StartTime provide the same information only this is an index and StartTime is the offset.
        /// </summary>
        public int? ChunkId
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.ChunkId);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.ChunkId, value);
            }
        }

        /// <summary>
        /// The stream type of the chunk. (e.g. audio or video)
        /// </summary>
        public string StreamType
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.StreamType);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.StreamType, value);
            }
        }

        /// <summary>
        /// The timestamp of the chunk. This will coorespond to the timestamp used in the chunk's URL. This and the ChunkId provide the same information only ChunkId is an index and StartTime is the offset.
        /// </summary>
        public long? StartTime
        {
            get
            {
                return GetValue<long>(VideoLogAttributes.StartTime);
            }
            set
            {
                SetValue<long>(VideoLogAttributes.StartTime, value);
            }
        }

        /// <summary>
        /// The edge server address serving the chunk. Note: This will always be set to 255.255.255.255 if you haven't configured the EdgeServerRules in the diagnostic config.
        /// </summary>
        public string EdgeIP
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.EdgeIP);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.EdgeIP, value);
            }
        }

        /// <summary>
        /// The time since the video was started. Does not exclude paused time.
        /// </summary>
        public double? TotalElapsedTime
        {
            get
            {
                return GetValue<double>(VideoLogAttributes.TotalElapsedTime);
            }
            set
            {
                SetValue<double>(VideoLogAttributes.TotalElapsedTime, value);
            }
        }

        /// <summary>
        /// The size of the sampling window in seconds
        /// </summary>
        public double? SamplingWindowSeconds
        {
            get
            {
                return GetValue<double>(VideoLogAttributes.SamplingWindowSeconds);
            }
            set
            {
                SetValue<double>(VideoLogAttributes.SamplingWindowSeconds, value);
            }
        }



    }
}
