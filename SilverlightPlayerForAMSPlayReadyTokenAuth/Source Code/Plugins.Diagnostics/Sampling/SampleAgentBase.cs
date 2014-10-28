using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Responsible for aggregating all of the sampled quality data
    /// </summary>
    internal abstract class SampleAgentBase : ISampleAgent
    {
        /// <summary>
        /// Flag indicating whether or not the video is currently playing.
        /// </summary>
        protected bool isPlayingVideo;

        /// <summary>
        /// Indicates whether or not data should be returned while the video is not playing.
        /// </summary>
        protected bool reportDataWhenDisabled;

        /// <summary>
        /// The time of the last update.
        /// </summary>
        protected long? LastUpdateTime;

        /// <summary>
        /// An aggregator purely used to keep track of video pause and resume events.
        /// </summary>
        protected SampleEventAggregator sampleSizeAggregator;

        public SampleAgentBase(bool ReportDataWhenDisabled, TimeSpan SlidingWindowInterval)
        {
            reportDataWhenDisabled = ReportDataWhenDisabled;
            slidingWindowInterval = SlidingWindowInterval.Ticks;
            sampleSizeAggregator = new SampleEventAggregator() { SlidingWindowTicks = slidingWindowInterval };
        }

        protected long slidingWindowInterval;
        /// <summary>
        /// Interval at which aggregation occurs
        /// </summary>
        public virtual TimeSpan SlidingWindowInterval
        {
            get { return TimeSpan.FromTicks(slidingWindowInterval); }
            set
            {
                slidingWindowInterval = value.Ticks;
                sampleSizeAggregator.SlidingWindowTicks = slidingWindowInterval;
            }
        }

        /// <summary>
        /// Accepts new entries for aggregating
        /// </summary>
        /// <param name="entry">The new smoothstreamingevent to process</param>
        public virtual void ProcessEvent(SmoothStreamingEvent entry)
        {
            if (entry is MarkerEvent)
            {
                isPlayingVideo = !(entry is MarkerEventStop);
                sampleSizeAggregator.Enqueue(entry);
            }
        }

        /// <summary>
        /// Returns all available samples. Only returns samples every SlidingWindowInterval.
        /// </summary>
        /// <param name="currentTicks">The current time</param>
        /// <returns>An enumerable of SampleData results. Usually, this will only return none or one item.</returns>
        public virtual IEnumerable<SampleData> GetSamples(long currentTicks)
        {
            sampleSizeAggregator.Update(currentTicks);

            if (!LastUpdateTime.HasValue)
                LastUpdateTime = currentTicks;

            if ((reportDataWhenDisabled || isPlayingVideo) && currentTicks - LastUpdateTime.Value > slidingWindowInterval)
            {
                LastUpdateTime = currentTicks;
                return GetSampleData();
            }
            else
                return Enumerable.Empty<SampleData>();
        }

        /// <summary>
        /// Required abstract method for returning sample data. This is only called when the sliding window interval has elapsed.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<SampleData> GetSampleData();
    }
}
