
namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Contains information about a group of download errors. Info about the bitrate is not available.
    /// </summary>
    public class DownloadErrorAggregatedData : SampleData
    {
        /// <summary>
        /// The index of the time stamp of the chunk
        /// </summary>
        public int ChunkId { get; set; }

        /// <summary>
        /// The stream type (e.g. audio or video)
        /// </summary>
        public string StreamType { get; set; }

        /// <summary>
        /// The timestamp of the chunk. Matches the timestamp that is part of the url for the chunk itself.
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// The total number of download errors for the given chunk (across all bitrates) for a given sample.
        /// </summary>
        public int Count { get; set; }
    }
}
