using System.Net;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// A video has loaded and is ready to be played
    /// </summary>
    public class VideoLoadLog : VideoEventLog
    {
        public VideoLoadLog()
            : base(VideoLogTypes.VideoLoaded)
        {
        }

        /// <summary>
        /// The maximum bitrate for the current stream
        /// </summary>
        public double? MaxBitRate
        {
            get
            {
                return GetValue<double>(VideoLogAttributes.MaxBitRate);
            }
            set
            {
                SetValue<double>(VideoLogAttributes.MaxBitRate, value);
            }
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
        /// The client IP. This will be IPAddress.None (255.255.255.255) if no EdgeServerRules are provided in the diagnostic config.
        /// </summary>
        public string ClientIP
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.ClientIP);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.ClientIP, value.ToString());
            }
        }

        /// <summary>
        /// The Url of the current stream.
        /// </summary>
        public string VideoUrl
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.VideoUrl);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.VideoUrl, value);
            }
        }
    }
}
