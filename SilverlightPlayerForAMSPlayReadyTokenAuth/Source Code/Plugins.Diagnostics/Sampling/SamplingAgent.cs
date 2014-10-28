using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// The gateway for getting sampled data (aggregated and snapshots).
    /// Relies on individual sampling agents (ISampleAgent) to do the real work.
    /// </summary>
    internal class SamplingAgent : IDisposable
    {
        long lastAggregateUpdateTime = long.MinValue;
        string currentStreamUrl = "";
        TimeSpan runningDuration; // used to store the acculmulated duration of the current video session. This will not contain the latest chunk of time which can be found by adding (now - lastStartTime)
        DateTime lastStartTime; // timestamp of the most recent time that the video started or resumed.
        DateTime originalStartTime;

        public SamplingAgent()
        {
            originalStartTime = DateTime.Now;
        }

        bool isEnabled = true;
        /// <summary>
        /// Used to disable or enable the agent. This affects how aggregated data is calculated as well as the sample size.
        /// </summary>
        protected bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;

                var marker = isEnabled ? new MarkerEventStart() as MarkerEvent : new MarkerEventStop() as MarkerEvent;

                foreach (var agent in agents)
                    agent.ProcessEvent(marker);

                if (isEnabled)
                    lastStartTime = DateTime.Now;
                else
                    runningDuration = runningDuration.Add(DateTime.Now - lastStartTime);
            }
        }

        /// <summary>
        /// Returns the total duration that the video has been playing (minus the paused and stopped periods of time).
        /// </summary>
        public TimeSpan GetRunningDuration()
        {
            if (isEnabled)
                return runningDuration.Add(DateTime.Now - lastStartTime);
            else
                return runningDuration;
        }

        /// <summary>
        /// Returns the total duration since the start of the video (doesn't exclude paused or stopped time).
        /// </summary>
        public TimeSpan GetTotalDuration()
        {
            return DateTime.Now - originalStartTime;
        }

        /// <summary>
        /// Add a new entry to the proper aggregator so that it can be tracked and evaluated.
        /// </summary>
        /// <param name="entry"></param>
        public void ProcessEvent(SmoothStreamingEvent entry)
        {
            if (entry != null)
            {
                if (entry.EventType == EventType.None)
                {
                    throw (new ArgumentException("Smooth Streaming Events must have an event type"));
                }
                else if (entry.EventType == EventType.ClipStarted ||
                    entry.EventType == EventType.ClipEnded ||
                    entry.EventType == EventType.StreamLoaded ||
                    entry.EventType == EventType.StreamStarted ||
                    entry.EventType == EventType.StreamEnded ||
                    entry.EventType == EventType.Paused ||
                    entry.EventType == EventType.Playing)
                {
                    if (entry.Ticks > lastAggregateUpdateTime)
                    {
                        lastAggregateUpdateTime = entry.Ticks;
                    }
                    if (entry.EventType == EventType.StreamLoaded)
                    {
                        currentStreamUrl = entry.Data1;
                    }
                    else if (entry.EventType == EventType.StreamStarted)
                    {
                        IsEnabled = true;
                    }
                    else if (entry.EventType == EventType.Playing)
                    {
                        IsEnabled = true;
                    }
                    else if (entry.EventType == EventType.StreamEnded || entry.EventType == EventType.Paused)
                    {
                        IsEnabled = false;
                    }
                }

                // pass the events to the sub agents
                foreach (var agent in agents)
                    agent.ProcessEvent(entry);
            }
        }

        IList<ISampleAgent> agents = new List<ISampleAgent>();
        /// <summary>
        /// Returns the list of sample agents
        /// </summary>
        public IList<ISampleAgent> Agents
        {
            get { return agents; }
        }

        /// <summary>
        /// Creates objects that contain aggregated data values.
        /// </summary>
        /// <returns>An enumerable of SampleData objects</returns>
        public IEnumerable<SampleData> GetSampleData()
        {
            long currentTicks = DateTime.Now.Ticks;

            foreach (ISampleAgent agent in agents)
            {
                foreach (var sample in agent.GetSamples(currentTicks))
                {
                    sample.CurrentStreamUrl = currentStreamUrl;
                    yield return sample;
                }
            }
        }

        public void Dispose()
        {
            foreach (var agent in agents.OfType<IDisposable>())
                agent.Dispose();
        }
    }

}
