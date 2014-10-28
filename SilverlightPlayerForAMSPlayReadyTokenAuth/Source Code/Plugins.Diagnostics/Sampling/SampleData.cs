
namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Defines the base aggregation information that comes from an aggregation agent
    /// </summary>
    public abstract class SampleData
    {
        /// <summary>
        /// The sample size in milliseconds
        /// </summary>
        public int SampleSizeMilliseconds { get; set; }

        /// <summary>
        /// The media element ID (also cooresponds to the SSME.Name property)
        /// </summary>
        public string MediaElementId { get; set; }

        /// <summary>
        /// The Url of the current stream
        /// </summary>
        public string CurrentStreamUrl { get; set; }

        /// <summary>
        /// A flag indicating whether or not the video position is currently at the live position
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// A generated ID used to track the stream. This does not coorespond to anything in the manifest
        /// </summary>
        public int? CurrentStreamId { get; set; }

        /// <summary>
        /// A generated ID used to track the clip. This does not coorespond to anything in the manifest
        /// </summary>
        public int? CurrentClipId { get; set; }

        /// <summary>
        /// The edge server used to host the streaming video. Default is 255.255.255.255 and will only be updated if edge server detection rules are specified in the diagnostic config
        /// </summary>
        public string EdgeIP { get; set; }
    }
}
