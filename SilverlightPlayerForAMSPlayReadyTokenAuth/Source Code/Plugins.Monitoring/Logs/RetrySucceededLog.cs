
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The video has failed to play, retries were issued by SMF and succeeded. This will always follow one or more MediaFailedLogs.
    /// </summary>
    public class RetrySucceededLog : VideoEventLog
    {
        public RetrySucceededLog()
            : base(VideoLogTypes.RetrySucceeded)
        {
        }

        /// <summary>
        /// The edge server address serving the stream. Note: This will always be set to 255.255.255.255 if you haven't configured the EdgeServerRules in the diagnostic config.
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
    }
}
