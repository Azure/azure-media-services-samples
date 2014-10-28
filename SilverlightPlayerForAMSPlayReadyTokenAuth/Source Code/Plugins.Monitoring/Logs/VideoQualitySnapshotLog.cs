
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// A snapshot of the video quality. Used to display real time quality information. When sending to the server, you should use VideoQualityLog instead to get aggregated values and thereby have chunky over chatty communication with the server.
    /// </summary>
    public class VideoQualitySnapshotLog : VideoQualityBaseLog
    {
        public VideoQualitySnapshotLog()
            : base(VideoLogTypes.VideoQualitySnapshot)
        {
        }
    }
}
