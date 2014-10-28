using System.Net;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The base log for video quality info. This is used for both snapshots and aggregated data.
    /// </summary>
    public abstract class VideoQualityBaseLog : VideoEventLog
    {
        public VideoQualityBaseLog(string logType)
            : base(logType)
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
        /// The bitrate being played.
        /// </summary>
        public int? BitRate
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.BitRate);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.BitRate, value);
            }
        }
        
        /// <summary>
        /// The total buffering in milliseconds.
        /// </summary>
        public int? BufferingMilliseconds
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.BufferingMilliseconds);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.BufferingMilliseconds, value);
            }
        }

        /// <summary>
        /// The dropped frames per second.
        /// </summary>
        public short? DroppedFrames
        {
            get
            {
                return GetValue<short>(VideoLogAttributes.DroppedFrames);
            }
            set
            {
                SetValue<short>(VideoLogAttributes.DroppedFrames, value);
            }
        }

        /// <summary>
        /// The rendered frames per second.
        /// </summary>
        public short? RenderedFrames
        {
            get
            {
                return GetValue<short>(VideoLogAttributes.RenderedFrames);
            }
            set
            {
                SetValue<short>(VideoLogAttributes.RenderedFrames, value);
            }
        }

        /// <summary>
        /// The CPU load for the current process (e.g. 15 = 15%).
        /// </summary>
        public short? ProcessCPULoad
        {
            get
            {
                return GetValue<short>(VideoLogAttributes.ProcessCPULoad);
            }
            set
            {
                SetValue<short>(VideoLogAttributes.ProcessCPULoad, value);
            }
        }

        /// <summary>
        /// The CPU load for the entire system (e.g. 15 = 15%).
        /// </summary>
        public short? SystemCPULoad
        {
            get
            {
                return GetValue<short>(VideoLogAttributes.SystemCPULoad);
            }
            set
            {
                SetValue<short>(VideoLogAttributes.SystemCPULoad, value);
            }
        }

        /// <summary>
        /// The latency time for video chunks in milliseconds.
        /// </summary>
        public int? VideoDownloadLatencyMilliseconds
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.VideoDownloadLatencyMilliseconds);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.VideoDownloadLatencyMilliseconds, value);
            }
        }

        /// <summary>
        /// The latency time for audio chunks in milliseconds.
        /// </summary>
        public int? AudioDownloadLatencyMilliseconds
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.AudioDownloadLatencyMilliseconds);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.AudioDownloadLatencyMilliseconds, value);
            }
        }

        /// <summary>
        /// The time since the video was started. Does not exclude paused time.
        /// </summary>
        public double? TotalElapsedTime
        {
            get
            {
                return GetValue<double>(VideoLogAttributes.TotalElapsedTime);
            }
            set
            {
                SetValue<double>(VideoLogAttributes.TotalElapsedTime, value);
            }
        }

        /// <summary>
        /// The size of the audio buffer in bytes.
        /// </summary>
        public int? AudioBufferSize
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.AudioBufferSize);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.AudioBufferSize, value);
            }
        }

        /// <summary>
        /// The size of the video buffer in bytes.
        /// </summary>
        public int? VideoBufferSize
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.VideoBufferSize);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.VideoBufferSize, value);
            }
        }

        /// <summary>
        /// The perceived bandwidth in bytes per second.
        /// </summary>
        public long? PerceivedBandwidth
        {
            get
            {
                return GetValue<long>(VideoLogAttributes.PerceivedBandwidth);
            }
            set
            {
                SetValue<long>(VideoLogAttributes.PerceivedBandwidth, value);
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

        /// <summary>
        /// The total times that the bitrate changed within the current sampling period
        /// </summary>
        public int? BitRateChangeCount
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.BitRateChangeCount);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.BitRateChangeCount, value);
            }
        }

        /// <summary>
        /// The max bitrate achieved in the current sampling period
        /// </summary>
        public int? MaxBitRate
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.MaxBitRate);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.MaxBitRate, value);
            }
        }

        /// <summary>
        /// The total amount of time that the max bitrate was achieved within the current sampling period
        /// </summary>
        public int? MaxBitRateMilliseconds
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.MaxBitRateMilliseconds);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.MaxBitRateMilliseconds, value);
            }
        }

        /// <summary>
        /// The total number of HTTP errors
        /// </summary>
        public int? HttpErrorCount
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.HttpErrorCount);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.HttpErrorCount, value);
            }
        }

        /// <summary>
        /// The size of the sampling window in seconds
        /// </summary>
        public double? SamplingFrequencySeconds
        {
            get
            {
                return GetValue<double>(VideoLogAttributes.SamplingWindowSeconds);
            }
            set
            {
                SetValue<double>(VideoLogAttributes.SamplingWindowSeconds, value);
            }
        }

        /// <summary>
        /// The total times that a DVR operation occured
        /// </summary>
        public int? DVRUseCount
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.DVRUseCount);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.DVRUseCount, value);
            }
        }

        /// <summary>
        /// The total times that the video came in or out of full screen mode
        /// </summary>
        public int? FullScreenChangeCount
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.FullScreenChangeCount);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.FullScreenChangeCount, value);
            }
        }

    }
}
