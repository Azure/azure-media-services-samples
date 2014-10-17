namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// An audio latecy alert has occured. The latency threshold is defined in the diagnostic config and is 2 KBps by default.
    /// </summary>
    public class AudioLatencyAlertLog : LatencyAlertLog
    {
        public AudioLatencyAlertLog()
            : base(VideoLogTypes.AudioLatencyAlert) { }
    }
}
