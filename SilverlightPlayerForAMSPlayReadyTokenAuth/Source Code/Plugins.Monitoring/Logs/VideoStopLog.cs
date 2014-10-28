
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The video has finished playing. Pausing the video will not cause this.
    /// </summary>
    public class VideoStopLog : VideoEventLog
    {
        public VideoStopLog()
            : base(VideoLogTypes.VideoStop)
        {
        }
    }
}
