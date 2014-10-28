
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// A video clip has ended.
    /// </summary>
    public class VideoClipEndedLog : VideoEventLog
    {
        public VideoClipEndedLog()
            : base(VideoLogTypes.VideoClipEnded)
        {
        }
    }
}
