using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Responsible for aggregating all of the http chunk download errors that occur and group them by stream type and chunk id (aka start time)
    /// </summary>
    internal class DownloadErrorSampleAgent : SampleAgentBase
    {
        Dictionary<string, SampleEventAggregator> aggregators = new Dictionary<string, SampleEventAggregator>();

        public DownloadErrorSampleAgent(DiagnosticsConfig Settings)
            : base(false, Settings.AggregationInterval)
        { }

        public override void ProcessEvent(SmoothStreamingEvent entry)
        {
            base.ProcessEvent(entry);

            if (entry.EventType == EventType.DownloadError)
            {
                string key = entry.Data1 + "." + entry.Data2;
                if (!aggregators.ContainsKey(key))
                    AddAggregator(key);

                SampleEventAggregator agg = aggregators[key];
                agg.Enqueue(entry);
            }
        }

        public override IEnumerable<SampleData> GetSamples(long currentTicks)
        {
            foreach (var keyValue in aggregators.ToArray())
            {
                var agg = keyValue.Value;
                agg.Update(currentTicks);
                if (agg.GetCount() == 0)
                    aggregators.Remove(keyValue.Key);
            }

            return base.GetSamples(currentTicks);
        }

        protected override IEnumerable<SampleData> GetSampleData()
        {
            foreach (var agg in aggregators.Values)
            {
                var sse = agg.Peek();
                yield return new DownloadErrorAggregatedData()
                {
                    ChunkId = int.Parse(sse.Data2),
                    StartTime = long.Parse(sse.Data3),
                    StreamType = sse.Data1,
                    Count = agg.GetCount()
                };
            }
        }

        SampleEventAggregator AddAggregator(string key)
        {
            SampleEventAggregator agg = new SampleEventAggregator();
            agg.SlidingWindowTicks = base.slidingWindowInterval;
            aggregators.Add(key, agg);
            return agg;
        }
    }
}
