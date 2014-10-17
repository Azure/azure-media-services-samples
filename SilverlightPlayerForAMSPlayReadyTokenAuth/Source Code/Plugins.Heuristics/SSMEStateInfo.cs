using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    internal class SSMEStateInfo
    {
        internal SSMEStateInfo()
        {
            ReEnableCount = -1;
            RecommendedEnable = false;
        }

        /// <summary>
        /// The SSME to track
        /// </summary>
        internal SmoothStreamingMediaElement SmoothStreamingMediaElement { get; set; }

        /// <summary>
        /// The minimum bitrate (in bps) this SmoothStreamingMediaElement
        /// must be capable of playing before the next SmoothStreamingMediaElement
        /// should be enabled
        /// </summary>
        internal ulong MinimumPlaybackBitrate { get; set; }

        /// <summary>
        /// The minimum number of RenderedFramesPerSecond this SSME should achieve
        /// at the MinimumPlaybackBitrate before enabling the next SSME
        /// </summary>
        internal double MinimumRenderedFramesPerSecond { get; set; }

        /// <summary>
        /// The number of times we're recommended to renable this SSME
        /// </summary>
        internal int ReEnableCount { get; set; }

        /// <summary>
        /// Tracks if we've recommended this SSME to be enabled
        /// </summary>
        internal bool RecommendedEnable { get; set; }

        /// <summary>
        /// LastRenderedFramesPerSecond
        /// </summary>
        internal double AverageRenderedFramesPerSecond { get; set; }

        /// <summary>
        /// LastDownloadTrackBitrate
        /// </summary>
        internal ulong LastDownloadTrackBitrate { get; set; }

        /// <summary>
        /// LastPlaybackTrackBitrate
        /// </summary>
        internal ulong LastPlaybackTrackBitrate { get; set; }

        /// <summary>
        /// DecrementReEnableCount
        /// </summary>
        internal void DecrementReEnableCount()
        {
            if (ReEnableCount > -1)
            {
                ReEnableCount--;
            }
        }

        /// <summary>
        /// ResetReEnableCount
        /// </summary>
        internal void ResetReEnableCount()
        {
            ReEnableCount = -1;
        }
    }
}