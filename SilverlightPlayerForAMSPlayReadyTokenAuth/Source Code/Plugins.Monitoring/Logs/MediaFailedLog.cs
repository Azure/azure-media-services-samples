
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The video has failed to play. SMF automatically retries (resulting in the possibility of multiple MediaFailureLog logs followed by a RetryFailedLog or RetrySucceededLog.
    /// </summary>
    public class MediaFailedLog : VideoEventLog
    {
        public MediaFailedLog()
            : base(VideoLogTypes.MediaFailed)
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

        /// <summary>
        /// The text based reason for the failure.
        /// </summary>
        public string Reason
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.Reason);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.Reason, value.Length > 2048 ? value.Substring(0, 2048) : value);
            }
        }



    }
}
