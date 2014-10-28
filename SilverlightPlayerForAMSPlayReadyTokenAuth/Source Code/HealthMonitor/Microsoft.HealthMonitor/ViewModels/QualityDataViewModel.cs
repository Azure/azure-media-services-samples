using System;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;

namespace Microsoft.HealthMonitor.ViewModels
{
    public class QualityDataViewModel
    {
        VideoQualityLog qualityLog;

        public QualityDataViewModel(VideoQualityLog QualityLog)
        {
            this.qualityLog = QualityLog;
        }

        public int SampleSizeSeconds { get { return Convert.ToInt32(qualityLog.SamplingFrequencySeconds.GetValueOrDefault(0)); } }
        public string CurrentStreamUrl { get { return qualityLog.VideoUrl; } }
        //public int CurrentStreamId { get { return Convert.ToInt32(quality[VideoLogAttributes.MediaElementId]); } }
        //public int CurrentClipId { get { return Convert.ToInt32(quality[VideoLogAttributes.ClipId]); } }
        public short DroppedFrames { get { return qualityLog.DroppedFrames.GetValueOrDefault(0); } }
        public short RenderedFrames { get { return qualityLog.RenderedFrames.GetValueOrDefault(0); } }
        public short ProcessCPULoad { get { return qualityLog.ProcessCPULoad.GetValueOrDefault(0); } }
        public short SystemCPULoad { get { return qualityLog.SystemCPULoad.GetValueOrDefault(0); } }
        public int Bitrate { get { return qualityLog.BitRate.GetValueOrDefault(0); } }
        public int MaxBitrate { get { return qualityLog.MaxBitRate.GetValueOrDefault(0); } }
        public int MaxBitRateMilliseconds { get { return qualityLog.MaxBitRateMilliseconds.GetValueOrDefault(0); } }
        public long PerceivedBandwidth { get { return qualityLog.PerceivedBandwidth.GetValueOrDefault(0); } }
        public int VideoBufferSize { get { return qualityLog.VideoBufferSize.GetValueOrDefault(0); } }
        public int AudioBufferSize { get { return qualityLog.AudioBufferSize.GetValueOrDefault(0); } }
        public int BufferingMilliseconds { get { return qualityLog.BufferingMilliseconds.GetValueOrDefault(0); } }
        public int BitrateChangeCount { get { return qualityLog.BitRateChangeCount.GetValueOrDefault(0); } }
        public int VideoDownloadLatencyMilliseconds { get { return qualityLog.VideoDownloadLatencyMilliseconds.GetValueOrDefault(0); } }
        public int AudioDownloadLatencyMilliseconds { get { return qualityLog.AudioDownloadLatencyMilliseconds.GetValueOrDefault(0); } }
        public int DvrOperationCount { get { return qualityLog.DVRUseCount.GetValueOrDefault(0); } }
        public int FullScreenChangeCount { get { return qualityLog.FullScreenChangeCount.GetValueOrDefault(0); } }
        public int HttpErrorCount { get { return qualityLog.HttpErrorCount.GetValueOrDefault(0); } }
    }
}
