
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// A video clip has started.
    /// </summary>
    public class VideoClipStartedLog : VideoEventLog
    {
        public VideoClipStartedLog()
            : base(VideoLogTypes.VideoClipStarted)
        {
        }
    }
}
