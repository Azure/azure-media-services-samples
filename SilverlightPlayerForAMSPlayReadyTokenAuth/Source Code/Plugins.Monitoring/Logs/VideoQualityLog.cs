
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// Aggregated video quality metrics over a sampling window.
    /// </summary>
    public class VideoQualityLog : VideoQualityBaseLog
    {
        public VideoQualityLog()
            : base(VideoLogTypes.VideoQuality)
        {
        }

    }
}
