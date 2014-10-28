namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{    
    /// <summary>
    /// An video latecy alert has occured. The latency threshold is defined in the diagnostic config and is 2 KBps by default.
    /// </summary>
    public class VideoLatencyAlertLog : LatencyAlertLog
    {
        public VideoLatencyAlertLog()
            : base(VideoLogTypes.VideoLatencyAlert) { }
    }
}
