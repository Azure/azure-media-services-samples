
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// A latecy alert has occured. The latency threshold is defined in the diagnostic config and is 2 KBps by default.
    /// </summary>
    public abstract class LatencyAlertLog : SimpleVideoEventLog
    {
        public LatencyAlertLog(string logType)
            : base(logType)
        {
        }

        /// <summary>
        /// The bitrate of the chunk being downloaded
        /// </summary>
        public int? BitRate
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.BitRate);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.BitRate, value);
            }
        }

        /// <summary>
        /// The ID of the chunk being downloaded
        /// </summary>
        public string ChunkId
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.ChunkId);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.ChunkId, value);
            }
        }

        /// <summary>
        /// The length of time it took to download the chunk
        /// </summary>
        public long? DurationSeconds
        {
            get
            {
                return GetValue<long>(VideoLogAttributes.DurationSeconds);
            }
            set
            {
                SetValue<long>(VideoLogAttributes.DurationSeconds, value);
            }
        }
        
    }
}
