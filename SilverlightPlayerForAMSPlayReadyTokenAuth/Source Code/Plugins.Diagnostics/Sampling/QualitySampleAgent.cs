using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// A class derived from QualitySampleAgent responsible for returning quality snapshots. (indicated by QualityData.IsSnapshot = true)
    /// </summary>
    internal class QualitySnapshotAgent : QualitySampleAgent
    {
        public QualitySnapshotAgent(TimeSpan SlidingWindowInterval, QualityConfig QualityConfig)
            : base(true, SlidingWindowInterval)
        {
            if (QualityConfig == null)
                LoadDefaultQualityConfig();
            else
                LoadQualityConfig(QualityConfig);
        }

        protected override QualityData GetAggregatedData()
        {
            var result = base.GetAggregatedData();
            result.IsSnapshot = true;
            return result;
        }
    }

    /// <summary>
    /// A class derived from QualitySampleAgent responsible for returning quality aggregations as opposed to snapshots. (indicated by QualityData.IsSnapshot = false)
    /// </summary>
    internal class QualityAggregationAgent : QualitySampleAgent
    {
        public QualityAggregationAgent(TimeSpan SlidingWindowInterval, QualityConfig QualityConfig)
            : base(false, SlidingWindowInterval)
        {
            if (QualityConfig == null)
                LoadDefaultQualityConfig();
            else
                LoadQualityConfig(QualityConfig);
        }
    }

    /// <summary>
    /// Responsible for aggregating all of the sampled quality data
    /// </summary>
    internal class QualitySampleAgent : SampleAgentBase
    {
        Dictionary<EventType, SampleEventAggregator> aggregators = new Dictionary<EventType, SampleEventAggregator>();
        public QualityConfig QualityConfig { get; set; }

        public QualitySampleAgent(bool ReportDataWhenDisabled, TimeSpan SlidingWindowInterval) : base(ReportDataWhenDisabled, SlidingWindowInterval) { }

        public void LoadDefaultQualityConfig()
        {
            AddAggregator(EventType.HttpError);
            AddAggregator(EventType.ProcessCpuLoad);
            AddAggregator(EventType.SystemCpuLoad);
            AddAggregator(EventType.DroppedFramesPerSecond);
            AddAggregator(EventType.RenderedFramesPerSecond);
            AddAggregator(EventType.PerceivedBandwidth);
            AddAggregator(EventType.BitrateChanged);
            AddAggregator(EventType.VideoBufferSize);
            AddAggregator(EventType.VideoChunkDownload);
            AddAggregator(EventType.AudioChunkDownload);
            AddAggregator(EventType.AudioBufferSize);
            AddAggregator(EventType.BufferingStateChanged);
            AddAggregator(EventType.DvrOperation);
            AddAggregator(EventType.SetPlaybackRate);
            AddAggregator(EventType.FullScreenChanged);
        }

        public void LoadQualityConfig(QualityConfig QualityConfig)
        {
            this.QualityConfig = QualityConfig;
            bool emptyConfig = (QualityConfig == null);

            if (emptyConfig || QualityConfig.HttpErrorCount)
                AddAggregator(EventType.HttpError);
            if (emptyConfig || QualityConfig.ProcessCpuLoad)
                AddAggregator(EventType.ProcessCpuLoad);
            if (emptyConfig || QualityConfig.SystemCpuLoad)
                AddAggregator(EventType.SystemCpuLoad);
            if (emptyConfig || QualityConfig.DroppedFrames)
                AddAggregator(EventType.DroppedFramesPerSecond);
            if (emptyConfig || QualityConfig.RenderedFrames)
                AddAggregator(EventType.RenderedFramesPerSecond);
            if (emptyConfig || QualityConfig.PerceivedBandwidth)
                AddAggregator(EventType.PerceivedBandwidth);
            if (emptyConfig || QualityConfig.Bitrate || QualityConfig.BitrateChangeCount || QualityConfig.BitrateMax || QualityConfig.BitrateMaxDuration)
                AddAggregator(EventType.BitrateChanged);
            if (emptyConfig || QualityConfig.VideoBufferSize)
                AddAggregator(EventType.VideoBufferSize);
            if (emptyConfig || QualityConfig.VideoDownloadLatency)
                AddAggregator(EventType.VideoChunkDownload);
            if (emptyConfig || QualityConfig.AudioDownloadLatency)
                AddAggregator(EventType.AudioChunkDownload);
            if (emptyConfig || QualityConfig.AudioBufferSize)
                AddAggregator(EventType.AudioBufferSize);
            if (emptyConfig || QualityConfig.Buffering)
                AddAggregator(EventType.BufferingStateChanged);
            if (emptyConfig || QualityConfig.DvrOperationCount)
                AddAggregator(EventType.DvrOperation);
            if (emptyConfig || QualityConfig.FullScreenChangeCount)
                AddAggregator(EventType.FullScreenChanged);
        }

        void AddAggregator(EventType type)
        {
            SampleEventAggregator agg = new SampleEventAggregator();
            agg.SlidingWindowTicks = slidingWindowInterval;
            aggregators.Add(type, agg);
        }

        public override TimeSpan SlidingWindowInterval
        {
            get { return base.SlidingWindowInterval; }
            set
            {
                base.SlidingWindowInterval = value;
                foreach (var agg in aggregators)
                    agg.Value.SlidingWindowTicks = slidingWindowInterval;
            }
        }

        public override void ProcessEvent(SmoothStreamingEvent entry)
        {
            base.ProcessEvent(entry);

            if (entry is MarkerEvent)
            {
                foreach (SampleEventAggregator agg in aggregators.Values)
                    agg.Enqueue(entry);
            }
            else
            {
                if (aggregators.ContainsKey(entry.EventType))
                    aggregators[entry.EventType].Enqueue(entry);
            }
        }

        protected virtual QualityData GetAggregatedData()
        {
            bool emptyConfig = (QualityConfig == null);
            QualityData qualityData = new QualityData();
            if (emptyConfig || QualityConfig.Bitrate || QualityConfig.BitrateChangeCount || QualityConfig.BitrateMax || QualityConfig.BitrateMaxDuration)
            {
                var agg = aggregators[EventType.BitrateChanged];
                if (emptyConfig || QualityConfig.Bitrate)
                    qualityData.Bitrate = agg.GetAverageOverTime();
                if (emptyConfig || QualityConfig.BitrateChangeCount)
                    qualityData.BitrateChangeCount = agg.GetCount();

                if (emptyConfig || QualityConfig.BitrateMax || QualityConfig.BitrateMaxDuration)
                {
                    double maxBitrate = agg.GetMax();
                    if (emptyConfig || QualityConfig.BitrateMax)
                        qualityData.BitrateMax = Convert.ToInt32(maxBitrate);
                    if (emptyConfig || QualityConfig.BitrateMaxDuration)
                        qualityData.BitrateMaxMilliseconds = agg.GetTotalTime(v => v == maxBitrate).TotalMilliseconds;
                }
            }
            if (emptyConfig || QualityConfig.Buffering)
                qualityData.BufferingMilliseconds = aggregators[EventType.BufferingStateChanged].GetTotalTime(v => v > 0).TotalMilliseconds;
            if (emptyConfig || QualityConfig.ProcessCpuLoad)
                qualityData.ProcessCpuLoad = aggregators[EventType.ProcessCpuLoad].GetAverage();
            if (emptyConfig || QualityConfig.SystemCpuLoad)
                qualityData.SystemCpuLoad = aggregators[EventType.SystemCpuLoad].GetAverage();
            if (emptyConfig || QualityConfig.DroppedFrames)
                qualityData.DroppedFrames = aggregators[EventType.DroppedFramesPerSecond].GetAverage();
            if (emptyConfig || QualityConfig.RenderedFrames)
                qualityData.RenderedFrames = aggregators[EventType.RenderedFramesPerSecond].GetAverage();
            if (emptyConfig || QualityConfig.PerceivedBandwidth)
                qualityData.PerceivedBandwidth = Convert.ToInt64(aggregators[EventType.PerceivedBandwidth].GetAverageOverTime());
            if (emptyConfig || QualityConfig.VideoBufferSize)
                qualityData.VideoBufferSize = aggregators[EventType.VideoBufferSize].GetAverage();
            if (emptyConfig || QualityConfig.AudioBufferSize)
                qualityData.AudioBufferSize = aggregators[EventType.AudioBufferSize].GetAverage();
            if (emptyConfig || QualityConfig.VideoDownloadLatency)
                qualityData.VideoDownloadLatencyMilliseconds = aggregators[EventType.VideoChunkDownload].GetAverage();
            if (emptyConfig || QualityConfig.AudioDownloadLatency)
                qualityData.AudioDownloadLatencyMilliseconds = aggregators[EventType.AudioChunkDownload].GetAverage();
            if (emptyConfig || QualityConfig.DvrOperationCount)
                qualityData.DvrOperationCount = aggregators[EventType.DvrOperation].GetCount();
            if (emptyConfig || QualityConfig.FullScreenChangeCount)
                qualityData.FullScreenChangeCount = aggregators[EventType.FullScreenChanged].GetCount();
            if (emptyConfig || QualityConfig.HttpErrorCount)
                qualityData.HttpErrorCount = aggregators[EventType.HttpError].GetCount();

            if (base.reportDataWhenDisabled)
                qualityData.SampleSizeMilliseconds = Convert.ToInt32(SlidingWindowInterval.TotalMilliseconds);
            else
                qualityData.SampleSizeMilliseconds = Convert.ToInt32(base.sampleSizeAggregator.GetWindowSize().TotalMilliseconds);

            return qualityData;
        }

        public override IEnumerable<SampleData> GetSamples(long currentTicks)
        {
            foreach (SampleEventAggregator agg in aggregators.Values)
                agg.Update(currentTicks);

            return base.GetSamples(currentTicks);
        }

        protected override IEnumerable<SampleData> GetSampleData()
        {
            return new SampleData[] { GetAggregatedData() };
        }
    }
}
