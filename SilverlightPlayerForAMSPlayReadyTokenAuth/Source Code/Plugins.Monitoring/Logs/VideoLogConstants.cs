
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// Constants used for Log.Type
    /// </summary>
    public class VideoLogTypes
    {
        public const string VideoStarted = "VideoStarted";
        public const string VideoLoaded = "VideoLoaded";
        public const string VideoStop = "VideoStop";
        public const string VideoPause = "VideoPause";
        public const string VideoVolumeUp = "VideoVolumeUp";
        public const string VideoVolumeDown = "VideoVolumeDown";
        public const string VideoVolumeMute = "VideoVolumeMute";
        public const string VideoSSME = "VideoSSME";
        public const string MediaFailed = "MediaFailed";
        public const string RetryFailed = "RetryFailed";
        public const string RetrySucceeded = "RetrySucceeded";
        public const string VideoSlowForward = "VideoSlowForward";
        public const string VideoFastForward = "VideoFastForward";
        public const string VideoRewind = "VideoRewind";
        public const string VideoReplay = "VideoReplay";
        public const string VideoJumpToLive = "VideoJumpToLive";
        public const string VideoPlay = "VideoPlay";
        public const string VideoScrubStart = "VideoScrubStart";
        public const string VideoVolumeChange = "VideoVolumeChange";
        public const string VideoSmoothStreamingWarning = "VideoSmoothStreamingWarning";
        public const string VideoDuration = "VideoDuration";
        public const string VideoBitRateChange = "VideoBitRateChange";
        public const string VideoVideoSession = "VideoVideoSession";
        public const string VideoOpening = "VideoOpening";
        public const string VideoQuality = "VideoQuality";
        public const string VideoQualitySnapshot = "VideoQualitySnapshot";
        public const string VideoFrozen = "VideoFrozen";
        public const string VideoClipStarted = "VideoClipStarted";
        public const string VideoClipEnded = "VideoClipEnded";
        public const string ChunkLatency = "ChunkLatency";
        public const string AudioLatencyAlert = "AudioChunkLatency";
        public const string VideoLatencyAlert = "VideoChunkLatency";
        public const string VideoHttpError = "VideoHttpError";
        public const string FullScreenEntered = "FullScreenEntered";
        public const string FullScreenExit = "FullScreenExit";
        public const string AudioTrackSelect = "AudioTrackSelect";
        public const string ClosedCaptionOn = "ClosedCaptionOn";
        public const string ClosedCaptionOff = "ClosedCaptionOff";
        public const string ChunkDownloadError = "ChunkDownloadError";
        public const string Trace = "Trace";
    }

    /// <summary>
    /// Constants used as the keys in a log's underlying dictionary of values.
    /// </summary>
    public class VideoLogAttributes
    {
        public const string BitRate = "BitRate";
        public const string ChunkId = "ChunkId";
        public const string StreamType = "StreamType";
        public const string StartTime = "StartTime";
        public const string TotalElapsedTime = "TotalElapsedTime";
        public const string MediaElementId = "MediaElementId";
        public const string FailureDescription = "FailureDescription";
        public const string EdgeIP = "EdgeIP";
        public const string ClientIP = "ClientIP";
        public const string Reason = "Reason";
        public const string VideoTimelineMarker = "VideoTimelineMarker";
        public const string VideoSessionId = "VideoSessionId";
        public const string VideoSessionDuration = "VideoSessionDuration";
        public const string VideoUrl = "VideoUrl";
        public const string CDNBlocked = "CDNBlocked";
        public const string GPUDeviceId = "GPUDeviceId";
        public const string GPUVendorId = "GPUVendorId";
        public const string GPUVersion = "GPUVersion";
        public const string BitRateChangeCount = "BitRateChangeCount";
        public const string MaxBitRate = "MaxBitRate";
        public const string MaxBitRateMilliseconds = "MaxBitRateMilliseconds";
        public const string BufferingMilliseconds = "BufferingMilliseconds";
        public const string DroppedFrames = "DroppedFrames";
        public const string RenderedFrames = "RenderedFrames";
        public const string HttpErrorCount = "HttpErrorCount";
        public const string ProcessCPULoad = "ProcessCPULoad";
        public const string SystemCPULoad = "SystemCPULoad";
        public const string VideoDownloadLatencyMilliseconds = "VideoDownloadLatencyMilliseconds";
        public const string AudioDownloadLatencyMilliseconds = "AudioDownloadLatencyMilliseconds";
        public const string SamplingWindowSeconds = "SamplingWindowSeconds";
        public const string AudioBufferSize = "AudioBufferSize";
        public const string VideoBufferSize = "VideoBufferSize";
        public const string PerceivedBandwidth = "PerceivedBandwidth";
        public const string DVRUseCount = "DVRUseCount";
        public const string FullScreenChangeCount = "FullScreenChangeCount";
        public const string QualityWindowDuration = "QualityWindowDuration";
        public const string Language = "Language";
        public const string VideoPosition = "VideoPosition";
        public const string IsLive = "IsLive";
        public const string DownloadErrorCount = "DownloadErrorCount";
        public const string ProcessCPU = "ProcessCPU";
        public const string ProcessorCPU = "ProcessorCPU";
        public const string DurationSeconds = "DurationSeconds";
#if VIDEOID
        public const string VideoId = "VideoId";
#endif

        public const string TraceClassName = "ClassName";
        public const string TraceDate = "Date";
        public const string TraceMethodName = "MethodName";
        public const string TraceText = "Text";
        public const string TraceThreadId = "ThreadId";
        public const string TraceArea = "TraceArea";
        public const string TraceLevel = "TraceLevel";
    }
}
