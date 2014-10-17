
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The base class for a basic video event where we want to track whether or not the playback is live.
    /// </summary>
    public abstract class VideoEventLog : SimpleVideoEventLog
    {
        public VideoEventLog(string logType)
            : base(logType)
        {
        }

        public bool? IsLive
        {
            get
            {
                return GetValue<bool>(VideoLogAttributes.IsLive);
            }
            set
            {
                SetValue<bool>(VideoLogAttributes.IsLive, value);
            }
        }



    }
}
