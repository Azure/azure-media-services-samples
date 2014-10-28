using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Tracks a data sample (SmoothStreamingEvent objects)
    /// Capable of calculating aggregated values for the data
    /// Contains logic for excluding non-tracked periods of time within the sampled data. For example, if the video was paused half way through the sample, that time will not be factored into weighted averages.
    /// </summary>
    internal class SampleEventAggregator
    {
        double total;
        int count;
        long? startTicks;
        Queue<SmoothStreamingEvent> queue = new Queue<SmoothStreamingEvent>();
        SmoothStreamingEvent previousEntry;
        SmoothStreamingEvent previousEntryOrMarker;  // this could be a marker event
        SmoothStreamingEvent currentEntry;
        event EventHandler<SimpleEventArgs<SmoothStreamingEvent>> ItemRemoved;

        public SampleEventAggregator()
        {
            MaxEntries = -1;
        }

        /// <summary>
        /// The total number of ticks that should be represented in the queue
        /// </summary>
        public long SlidingWindowTicks { get; set; }

        /// <summary>
        /// The maximum number of entries that can be contained in the queue
        /// </summary>
        public int MaxEntries { get; private set; }

        void OnItemRemoved(SmoothStreamingEvent entry)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new SimpleEventArgs<SmoothStreamingEvent>(entry));
            }
        }

        /// <summary>
        /// The raw average value of the items in the queue (total/count)
        /// </summary>
        public double GetAverage()
        {
            if (count == 0)
            {
                if (currentEntry == null)
                    return 0;
                else // currentEntry will still contain the last entry even if it's not in the queue
                    return currentEntry.Value;
            }
            else
            {
                return total / count;
            }
        }

        /// <summary>
        /// The max value of the items in the queue
        /// </summary>
        public double GetMax()
        {
            if (count == 0)
            {
                if (currentEntry == null)
                    return 0;
                else // currentEntry will still contain the last entry even if it's not in the queue
                    return currentEntry.Value;
            }
            else
            {
                return queue.Max(item => item.Value);
            }
        }

        /// <summary>
        /// The min value of the items in the queue
        /// </summary>
        public double GetMin()
        {
            if (count == 0)
            {
                if (currentEntry == null)
                    return 0;
                else // currentEntry will still contain the last entry even if it's not in the queue
                    return currentEntry.Value;
            }
            else
            {
                return queue.Min(item => item.Value);
            }
        }

        /// <summary>
        /// The total number of items in the queue
        /// </summary>
        public int GetCount()
        {
            return count;
        }

        /// <summary>
        /// The sum of items in the queue
        /// </summary>
        public double GetSum()
        {
            return total;
        }

        /// <summary>
        /// Gets a average of the event values weighted by their timespan.
        /// Weighted values are weighted by the timespan between itself and the next event.
        /// Time in between stop and start markers is not counted.
        /// </summary>
        public double GetAverageOverTime()
        {
            long now = DateTime.Now.Ticks;
            long lastTime = Math.Max(now - SlidingWindowTicks, startTicks.Value);
            double lastValue = previousEntry == null ? 0 : previousEntry.Value;
            SmoothStreamingEvent lastEntry = previousEntryOrMarker ?? new MarkerEventStart();
            long aggregatedTicks = 0;
            double aggregatedValue = 0;
            long elapsedTicks = 0;

            foreach (SmoothStreamingEvent item in queue)
            {
                if (!(item is MarkerEventStart && lastEntry is MarkerEventStop))
                {
                    elapsedTicks += item.Ticks - lastTime;
                    if (!(item is MarkerEvent))
                    {
                        aggregatedValue += lastValue * elapsedTicks;
                        aggregatedTicks += elapsedTicks;
                        elapsedTicks = 0;
                        lastValue = item.Value;
                    }
                }
                lastEntry = item;
                lastTime = item.Ticks;
            }

            if (!(lastEntry is MarkerEventStop))
            {
                elapsedTicks = now - lastTime;
                aggregatedValue += lastValue * elapsedTicks;
                aggregatedTicks += elapsedTicks;
            }

            if (aggregatedTicks == 0)
                return 0;
            else
                return aggregatedValue / aggregatedTicks;
        }

        /// <summary>
        /// Gets the total time that an event occurred by adding future timespans (the time between itself and the next event)
        /// </summary>
        /// <param name="condition">A predicate for determining which values to factor into the result</param>
        public TimeSpan GetTotalTime(Predicate<double> condition)
        {
            long now = DateTime.Now.Ticks;
            long lastTime = Math.Max(now - SlidingWindowTicks, startTicks.Value);
            double lastValue = previousEntry == null ? 0 : previousEntry.Value;
            SmoothStreamingEvent lastEntry = previousEntryOrMarker ?? new MarkerEventStart();
            long runningTotal = 0;
            foreach (SmoothStreamingEvent item in queue)
            {
                if (!(item is MarkerEventStart && lastEntry is MarkerEventStop))
                {
                    if (condition == null || condition(lastValue))
                    {
                        runningTotal += item.Ticks - lastTime;
                        if (!(item is MarkerEvent))
                        {
                            lastValue = item.Value;
                        }
                    }
                }
                lastEntry = item;
                lastTime = item.Ticks;
            }

            if (!(lastEntry is MarkerEventStop))
            {
                if (condition == null || condition(lastValue))
                    runningTotal += now - lastTime;
            }

            return TimeSpan.FromTicks(runningTotal);
        }

        /// <summary>
        /// Gets the total timespan of the sample window (excluding periods of time where the video was paused or stopped)
        /// </summary>
        public TimeSpan GetWindowSize()
        {
            long now = DateTime.Now.Ticks;
            long lastTime = Math.Max(now - SlidingWindowTicks, startTicks.Value);
            SmoothStreamingEvent lastEntry = previousEntry ?? new MarkerEventStart();
            long runningTotal = 0;
            foreach (SmoothStreamingEvent item in queue)
            {
                if (!(item is MarkerEventStart && lastEntry is MarkerEventStop))
                {
                    runningTotal += item.Ticks - lastTime;
                }
                lastEntry = item;
                lastTime = item.Ticks;
            }

            if (!(lastEntry is MarkerEventStop))
                runningTotal += now - lastTime;

            return TimeSpan.FromTicks(runningTotal);
        }

        public void Enqueue(SmoothStreamingEvent entry)
        {
            queue.Enqueue(entry);
            if (!(entry is MarkerEvent))
            {
                currentEntry = entry;
                count++;
                total += entry.Value;
                if (MaxEntries >= 0 && count > MaxEntries)
                {
                    SmoothStreamingEvent oldestEntry = DequeueAggregator();
                    OnItemRemoved(oldestEntry);
                }
            }
        }

        public SmoothStreamingEvent Peek()
        {
            if (queue.Count > 0)
                return queue.Peek();
            else
                return null;
        }

        public SmoothStreamingEvent DequeueAggregator()
        {
            SmoothStreamingEvent entry = queue.Dequeue();
            if (!(entry is MarkerEvent))
            {
                count--;
                total -= entry.Value;
                previousEntry = entry;
            }
            previousEntryOrMarker = entry;
            return entry;
        }


        /// <summary>
        /// Refreshes the queue by causing items that were created longer ago than the SlidingWindowTicks property to get dropped
        /// </summary>
        public void Update(long currentTicks)
        {
            if (SlidingWindowTicks < 0) return;

            if (!startTicks.HasValue)
                startTicks = currentTicks;
            long windowStart = currentTicks - SlidingWindowTicks;
            while (queue.Count > 0 && Peek().Ticks < windowStart)
            {
                SmoothStreamingEvent entry = DequeueAggregator();
                OnItemRemoved(entry);
            }
        }
    }
}
