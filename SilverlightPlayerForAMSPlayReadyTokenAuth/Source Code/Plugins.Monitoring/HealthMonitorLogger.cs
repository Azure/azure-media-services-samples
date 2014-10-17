using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Diagnostics;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;
using Microsoft.Web.Media.Diagnostics;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring
{
    /// <summary>
    /// Responsible for capturing information coming from SilverlightMediaFramework.Diagnostics, turning it into Log objects, and passing it onto the Logging component.
    /// </summary>
    internal class HealthMonitorLogger
    {
        HealthMonitor healthMonitor;
        VideoLoadLog streamLoadedLog;

        /// <summary>
        /// Contains optional/additional information that should be attached to all logs before they are sent to the logging framework.
        /// </summary>
        public Dictionary<string, string> AdditionalLogData { get; internal set; }

#if VIDEOID
        /// <summary>
        /// Contains the VideoId that will be associated with the video
        /// </summary>
        public string VideoId { get; set; }
#endif

        public HealthMonitorLogger(LoggingConfig loggingConfig)
        {
            AdditionalLogData = new Dictionary<string, string>();
            LoggingService.Current.Configuration = loggingConfig;
        }

        void healthMonitor_LatencyAlert(object sender, SimpleEventArgs<SmoothStreamingEvent> e)
        {
            //data1 = bitrate; data2 = chunkId * 2000000; data3 = bytes; value = download time

            try
            {
                LatencyAlertLog log;

                if (e.Result.EventType == EventType.VideoChunkDownload)
                {
                    log = new VideoLatencyAlertLog();
                }
                else
                {
                    log = new AudioLatencyAlertLog();
                }

                PopulateSimpleVideoLog(log);

                log.MediaElementId = e.Result.MediaElementId;
                log.BitRate = Convert.ToInt32(e.Result.Data1);
                log.ChunkId = e.Result.Data2;
                log.DurationSeconds = Convert.ToInt64(e.Result.Value);

                SendLog(log);
            }
            catch (Exception ex)
            {
                LoggingService.Current.BroadcastException(ex);
            }
        }

        void healthMonitor_ReportSampledData(object sender, SimpleEventArgs<SampleData> e)
        {
            try
            {
                if (e.Result is QualityData)
                {
                    QualityData data = (QualityData)e.Result;
                    if (data.IsSnapshot)
                        ReportSnapshotData(data);
                    else
                        ReportQualityData(data);
                }
                else if (e.Result is DownloadErrorAggregatedData)
                    ReportDownloadErrorData((DownloadErrorAggregatedData)e.Result);
            }
            catch (Exception ex)
            {
                LoggingService.Current.BroadcastException(ex);
            }
        }

        void ReportDownloadErrorData(DownloadErrorAggregatedData data)
        {
            ChunkDownloadErrorLog chunkDownloadErrorLog = new ChunkDownloadErrorLog();
            PopulateVideoEventLog(chunkDownloadErrorLog);
            chunkDownloadErrorLog.StreamType = data.StreamType;
            chunkDownloadErrorLog.ChunkId = data.ChunkId;
            chunkDownloadErrorLog.StartTime = data.StartTime;
            chunkDownloadErrorLog.Count = data.Count;
            chunkDownloadErrorLog.EdgeIP = data.EdgeIP;
            chunkDownloadErrorLog.MediaElementId = data.MediaElementId;
            chunkDownloadErrorLog.SamplingWindowSeconds = data.SampleSizeMilliseconds / 1000;
            chunkDownloadErrorLog.IsLive = data.IsLive;
            chunkDownloadErrorLog.TotalElapsedTime = healthMonitor.VideoSessionTotalTime.TotalSeconds;
            SendLog(chunkDownloadErrorLog);
        }

        void ReportQualityData(QualityData data)
        {
            VideoQualityLog qualityLog = new VideoQualityLog();
            PopulateQualityLog(data, qualityLog);
            SendLog(qualityLog);
        }

        void healthMonitor_ReportSnapshotData(object sender, SimpleEventArgs<SampleData> e)
        {
            try
            {
                if (e.Result is QualityData)
                    ReportSnapshotData((QualityData)e.Result);
            }
            catch (Exception ex)
            {
                LoggingService.Current.BroadcastException(ex);
            }
        }

        void ReportSnapshotData(QualityData data)
        {
            VideoQualitySnapshotLog qualityLog = new VideoQualitySnapshotLog();
            PopulateQualityLog(data, qualityLog);
            SendLog(qualityLog);
        }

        private void PopulateQualityLog(QualityData data, VideoQualityBaseLog qualityLog)
        {
            PopulateVideoEventLog(qualityLog);
            qualityLog.VideoUrl = data.CurrentStreamUrl;
            qualityLog.EdgeIP = healthMonitor.EdgeServer;
            qualityLog.ClientIP = healthMonitor.ClientIP;
            qualityLog.MediaElementId = data.MediaElementId;
            qualityLog.SamplingFrequencySeconds = data.SampleSizeMilliseconds / 1000;
            qualityLog.IsLive = data.IsLive;
            qualityLog.TotalElapsedTime = healthMonitor.VideoSessionTotalTime.TotalSeconds;
            if (data.Bitrate.HasValue)
                qualityLog.BitRate = Convert.ToInt32(data.Bitrate.Value);
            if (data.BitrateChangeCount.HasValue)
                qualityLog.BitRateChangeCount = data.BitrateChangeCount.Value;
            if (data.BitrateMax.HasValue)
                qualityLog.MaxBitRate = data.BitrateMax.Value;
            if (data.BitrateMaxMilliseconds.HasValue)
                qualityLog.MaxBitRateMilliseconds = Convert.ToInt32(data.BitrateMaxMilliseconds.Value);
            if (data.BufferingMilliseconds.HasValue)
                qualityLog.BufferingMilliseconds = Convert.ToInt32(data.BufferingMilliseconds.Value);
            if (data.ProcessCpuLoad.HasValue)
                qualityLog.ProcessCPULoad = Convert.ToInt16(data.ProcessCpuLoad.Value);
            if (data.SystemCpuLoad.HasValue)
                qualityLog.SystemCPULoad = Convert.ToInt16(data.SystemCpuLoad.Value);
            if (data.DroppedFrames.HasValue)
                qualityLog.DroppedFrames = Convert.ToInt16(data.DroppedFrames.Value);
            if (data.RenderedFrames.HasValue)
                qualityLog.RenderedFrames = Convert.ToInt16(data.RenderedFrames.Value);
            if (data.HttpErrorCount.HasValue)
                qualityLog.HttpErrorCount = data.HttpErrorCount.Value;
            if (data.AudioBufferSize.HasValue)
                qualityLog.AudioBufferSize = Convert.ToInt32(data.AudioBufferSize.Value);
            if (data.VideoBufferSize.HasValue)
                qualityLog.VideoBufferSize = Convert.ToInt32(data.VideoBufferSize.Value);
            if (data.PerceivedBandwidth.HasValue)
                qualityLog.PerceivedBandwidth = Convert.ToInt64(data.PerceivedBandwidth.Value);
            if (data.DvrOperationCount.HasValue)
                qualityLog.DVRUseCount = Convert.ToInt32(data.DvrOperationCount.Value);
            if (data.FullScreenChangeCount.HasValue)
                qualityLog.FullScreenChangeCount = data.FullScreenChangeCount.Value;
            if (data.VideoDownloadLatencyMilliseconds.HasValue)
            {
                if (!Double.IsInfinity(data.VideoDownloadLatencyMilliseconds.Value))
                {
                    qualityLog.VideoDownloadLatencyMilliseconds = Convert.ToInt32(data.VideoDownloadLatencyMilliseconds.Value);
                }
            }
            if (data.AudioDownloadLatencyMilliseconds.HasValue)
            {
                if (!Double.IsInfinity(data.AudioDownloadLatencyMilliseconds.Value))
                {
                    qualityLog.AudioDownloadLatencyMilliseconds = Convert.ToInt32(data.AudioDownloadLatencyMilliseconds.Value);
                }
            }

            //qualityLog.Data[VideoLogAttributes.CDNBlocked] = healthMonitor.AnonymousProxy;
        }

        void healthMonitor_ReportTraceLogs(object sender, SimpleEventArgs<IEnumerable<TraceEntry>> e)
        {
            foreach (var trace in e.Result)
            {
                TraceLog tracelog = new TraceLog();
                tracelog.ClassName = trace.ClassName;
                tracelog.Date = trace.Date.ToUniversalTime();
                tracelog.MediaElementId = trace.MediaElementId;
                tracelog.MethodName = trace.MethodName;
                tracelog.Text = trace.Text;
                tracelog.ThreadId = trace.ThreadId;
                tracelog.TraceArea = trace.TraceArea.ToString();
                tracelog.TraceLevel = trace.TraceLevel.ToString();
                SendLog(tracelog);
            }
        }

        // we need to queue logs until the edge server has been returned
        readonly Queue<Log> pendingLogs = new Queue<Log>();
        void healthMonitor_EdgeServerChanged(object sender, EventArgs e)
        {
            FlushLogs();
        }

        void FlushLogs()
        {
            while (true)
            {
                Log pendingLog;
                lock (pendingLogs)
                {
                    if (pendingLogs.Count == 0) break;
                    pendingLog = pendingLogs.Dequeue();
                }
                if (pendingLog.Data.ContainsKey(VideoLogAttributes.EdgeIP))
                    pendingLog.Data[VideoLogAttributes.EdgeIP] = healthMonitor.EdgeServer;
                LoggingService.Current.Log(pendingLog);
            }
        }

        void SendLog(Log log)
        {
            if (healthMonitor.IsEdgeServerComplete)
                LoggingService.Current.Log(log);
            else
            {
                lock (pendingLogs)
                {
                    pendingLogs.Enqueue(log);
                }
            }
        }

        void healthMonitor_EventCreated(object sender, SimpleEventArgs<SmoothStreamingEvent> e)
        {
            try
            {
                switch (e.Result.EventType)
                {
                    case EventType.StreamStarted:
                        // we keep this object around for reference
                        var streamStartedLog = new VideoStartLog();
                        PopulateVideoEventLog(streamStartedLog);
                        if (streamLoadedLog != null)
                        {
                            streamStartedLog.EdgeIP = streamLoadedLog.EdgeIP;
                            streamStartedLog.ClientIP = streamLoadedLog.ClientIP;
                            streamStartedLog.VideoUrl = streamLoadedLog.VideoUrl;
                            streamStartedLog.MaxBitRate = streamLoadedLog.MaxBitRate;
                        }
                        streamStartedLog.MediaElementId = e.Result.MediaElementId;
                        streamStartedLog.IsLive = e.Result.IsLive;
                        SendLog(streamStartedLog);
                        break;
                    case EventType.StreamLoaded:
                        // we keep this object around for reference
                        streamLoadedLog = new VideoLoadLog();
                        PopulateVideoEventLog(streamLoadedLog);
                        streamLoadedLog.EdgeIP = healthMonitor.EdgeServer;
                        streamLoadedLog.ClientIP = healthMonitor.ClientIP;
                        streamLoadedLog.VideoUrl = e.Result.Data1;
                        streamLoadedLog.MediaElementId = e.Result.MediaElementId;
                        streamLoadedLog.IsLive = e.Result.IsLive;
                        {
                            double maxBitRate = 0;
                            if (double.TryParse(e.Result.Data2, out maxBitRate))
                                streamLoadedLog.MaxBitRate = maxBitRate;
                        }
                        //streamStartedLog.CDNBlocked = healthMonitor.AnonymousProxy;
                        SendLog(streamLoadedLog);
                        break;
                    case EventType.StreamEnded:
                        VideoStopLog streamEndedLog = new VideoStopLog();
                        PopulateVideoEventLog(streamEndedLog);
                        streamEndedLog.MediaElementId = e.Result.MediaElementId;
                        streamEndedLog.IsLive = e.Result.IsLive;
                        //streamEndedLog.VideoUrl = e.Result.Data1;
                        SendLog(streamEndedLog);
                        break;
                    case EventType.ClipStarted:
                        VideoClipStartedLog clipStartLog = new VideoClipStartedLog();
                        PopulateVideoEventLog(clipStartLog);
                        //clipStartLog.VideoUrl = e.Result.Data1;
                        clipStartLog.MediaElementId = e.Result.MediaElementId;
                        clipStartLog.IsLive = e.Result.IsLive;
                        SendLog(clipStartLog);
                        break;
                    case EventType.ClipEnded:
                        VideoClipEndedLog clipEndLog = new VideoClipEndedLog();
                        PopulateVideoEventLog(clipEndLog);
                        clipEndLog.MediaElementId = e.Result.MediaElementId;
                        clipEndLog.IsLive = e.Result.IsLive;
                        //clipEndLog.VideoUrl = e.Result.Data1;
                        SendLog(clipEndLog);
                        break;
                    case EventType.MediaFailed:
                        MediaFailedLog mediaFailed = new MediaFailedLog();
                        PopulateVideoEventLog(mediaFailed);
                        mediaFailed.Reason = e.Result.Data1;
                        mediaFailed.EdgeIP = healthMonitor.EdgeServer;
                        mediaFailed.IsLive = e.Result.IsLive;
                        mediaFailed.MediaElementId = e.Result.MediaElementId;

                        //mediaFailed.VideoUrl = e.Result.Data2;
                        //mediaFailed.VideoTimelineMarker = Convert.ToInt64(e.Result.Data3);
                        SendLog(mediaFailed);
                        break;
                    case EventType.FullScreenChanged:
                        Log fullScreenLog;
                        if (Convert.ToBoolean(Convert.ToInt32(e.Result.Value)))
                            fullScreenLog = CreateVideoLog(VideoLogTypes.FullScreenEntered);
                        else
                            fullScreenLog = CreateVideoLog(VideoLogTypes.FullScreenExit);
                        SendLog(fullScreenLog);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggingService.Current.BroadcastException(ex);
            }
        }

        /// <summary>
        /// Attaches the diagnostic component so it can be monitored
        /// </summary>
        public void AttachMonitor(HealthMonitor monitor)
        {
            // bind to healthMonitor
            healthMonitor = monitor;
            healthMonitor.EdgeServerCompleted += healthMonitor_EdgeServerChanged;
            healthMonitor.EventCreated += healthMonitor_EventCreated;
            healthMonitor.ReportSampledData += healthMonitor_ReportSampledData;
            healthMonitor.LatencyAlert += healthMonitor_LatencyAlert;
            healthMonitor.ReportTraceLogs += healthMonitor_ReportTraceLogs;
            // guarantee that we're not using an old instance anymore
            streamLoadedLog = null;
        }

        /// <summary>
        /// Detaches the diagnostic from being monitored
        /// </summary>
        public void DetachMonitor()
        {
            healthMonitor.EdgeServerCompleted -= healthMonitor_EdgeServerChanged;
            healthMonitor.EventCreated -= healthMonitor_EventCreated;
            healthMonitor.ReportSampledData -= healthMonitor_ReportSampledData;
            healthMonitor.LatencyAlert -= healthMonitor_LatencyAlert;
            healthMonitor.ReportTraceLogs -= healthMonitor_ReportTraceLogs;
            healthMonitor = null;
        }

        /// <summary>
        /// Allows you to programmatically create a custom video log. The log will be populated with certain basic video information.
        /// You still need to pass the log on to LoggingService.Current.Log(log);
        /// </summary>
        /// <param name="logType">A string value representing the type of your custom log.</param>
        /// <returns>The newly created log object.</returns>
        public SimpleVideoEventLog CreateVideoLog(string logType)
        {
            SimpleVideoEventLog log = new SimpleVideoEventLog(logType);
            PopulateSimpleVideoLog(log);
            return log;
        }

        /// <summary>
        /// Populates a video log with the basic video information
        /// </summary>
        public void PopulateSimpleVideoLog(SimpleVideoEventLog log)
        {
#if VIDEOID
            log.VideoId = VideoId;
#endif

            // add extra data that should be included with all video logs
            foreach (var item in AdditionalLogData)
                log.Data.Add(item.Key, item.Value);

            if (streamLoadedLog != null)
                log.RelatedLogId = streamLoadedLog.LogId;
        }

        /// <summary>
        /// Populates a video log with the basic video information. This is a superset of PopulateSimpleVideoLog
        /// </summary>
        public void PopulateVideoEventLog(VideoEventLog log)
        {
            PopulateSimpleVideoLog(log);
            if (streamLoadedLog != null && streamLoadedLog != log)
                log.RelatedLogId = streamLoadedLog.LogId;
            log.VideoSessionId = healthMonitor.VideoSessionId;
            log.VideoSessionDuration = healthMonitor.VideoSessionRunningTime;
        }
    }
}
