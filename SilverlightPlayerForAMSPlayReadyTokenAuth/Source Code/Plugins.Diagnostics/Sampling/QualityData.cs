using System;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
	/// <summary>
	/// Contains all the aggregated values for the sampled quality data
	/// </summary>
    public class QualityData : SampleData
    {
        /// <summary>
        /// Indicates whether or not the sampling is a snapshot or the full aggregation window
        /// </summary>
        public bool IsSnapshot { get; set; }

        /// <summary>
        /// Average dropped frames per second
        /// </summary>
        public double? DroppedFrames { get; set; }

        /// <summary>
        /// Average rendered frames per second
        /// </summary>
        public double? RenderedFrames { get; set; }

        /// <summary>
        /// The average CPU load for the current process (e.g. 15 = 15%)
        /// </summary>
        public double? ProcessCpuLoad { get; set; }

        /// <summary>
        /// The average CPU load for the entire system (e.g. 15 = 15%)
        /// </summary>
        public double? SystemCpuLoad { get; set; }

        /// <summary>
        /// The average bitrate currently being played
        /// </summary>
        public double? Bitrate { get; set; }

        /// <summary>
        /// The average perceived bandwidth in bytes per second
        /// </summary>
        public long? PerceivedBandwidth { get; set; }

        /// <summary>
        /// The average size of the video buffer in bytes
        /// </summary>
        public double? VideoBufferSize { get; set; }

        /// <summary>
        /// The average size of the audio buffer in bytes
        /// </summary>
        public double? AudioBufferSize { get; set; }

        /// <summary>
        /// The total duration in milliseconds that the player was buffering
        /// </summary>
        public double? BufferingMilliseconds { get; set; }

        /// <summary>
        /// The average latency time for video chunks in milliseconds
        /// </summary>
        public double? VideoDownloadLatencyMilliseconds { get; set; }

        /// <summary>
        /// The average latency time for audio chunks in milliseconds
        /// </summary>
        public double? AudioDownloadLatencyMilliseconds { get; set; }

        /// <summary>
        /// The max bitrate achieved in the current sampling period
        /// </summary>
		public int? BitrateMax { get; set; }

        /// <summary>
        /// The total amount of time that the max bitrate was achieved within the current sampling period
        /// </summary>
		public double? BitrateMaxMilliseconds { get; set; }

        /// <summary>
        /// The total times that the bitrate changed within the current sampling period
        /// </summary>
		public int? BitrateChangeCount { get; set; }

        /// <summary>
        /// The total times that a DVR operation occured
        /// </summary>
		public int? DvrOperationCount { get; set; }

        /// <summary>
        /// The total times that the video came in or out of full screen mode
        /// </summary>
		public int? FullScreenChangeCount { get; set; }

        /// <summary>
        /// The total number of HTTP errors
        /// </summary>
		public int? HttpErrorCount { get; set; }
	}
}
