using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public class RateMonitor
    {
        private const long TimeWindowMillis = 1000;

        private readonly EventCollection _events = new EventCollection();

        public double CurrentRate
        {
            get
            {
                int eventCount;
                var rangeEnd = DateTime.Now;
                var rangeStart = rangeEnd.Subtract(TimeSpan.FromMilliseconds(TimeWindowMillis));
                var rangeDuration = rangeEnd.Subtract(rangeStart);

                lock (_events)
                {
                    eventCount = _events.WhereActiveInRange(rangeStart, rangeEnd).Count();
                }

                return eventCount/rangeDuration.TotalSeconds;
            }
        }

        public TimeSpan RecommendDelay(double preferredRate)
        {
            TimeSpan recommendedDelay = TimeSpan.Zero;

            if (CurrentRate > preferredRate)
            {
                var timeWindow = TimeSpan.FromMilliseconds(TimeWindowMillis);
                var rangeEnd = DateTime.Now;
                var rangeStart = rangeEnd.Subtract(timeWindow);

                var firstEvent = _events.WhereActiveInRange(rangeEnd, rangeStart).FirstOrDefault();
                if (firstEvent != default(DateTime))
                {
                    recommendedDelay = timeWindow.Subtract(DateTime.Now.Subtract(firstEvent));
                }
            }

            return recommendedDelay;
        }

        public void UpdateRate()
        {
            var eventTime = DateTime.Now;
            lock (_events)
            {
                _events.Add(eventTime, new DateTimeComparable(eventTime));
                TrimEventsList();
            }
        }

        private void TrimEventsList()
        {
            var rangeEnd = DateTime.Now;
            var rangeStart = rangeEnd.Subtract(TimeSpan.FromMilliseconds(TimeWindowMillis));

            var validEvents = _events.WhereActiveInRange(rangeStart, rangeEnd).ToList();
            _events.Clear();
            validEvents.ForEach(i => _events.Add(i, new DateTimeComparable(i)));
        }

        private class DateTimeComparable : IComparable<DateTime>
        {
            private readonly DateTime _current;

            public DateTimeComparable(DateTime current)
            {
                _current = current;
            }

            public int CompareTo(DateTime other)
            {
                return _current.CompareTo(other);
            }
        }

        private class EventCollection : OrderedScriptableObservableCollection<DateTime>
        {
            public IEnumerable<DateTime> WhereActiveInRange(DateTime rangeStart, DateTime rangeEnd)
            {
                var comparer = new DateTimeComparable(rangeStart);
                var startIndex = SearchForInsertIndex(comparer);

                return this.Skip(startIndex)
                    .TakeWhile(i => i <= rangeEnd)
                    .ToList();
            }
        }
    }
}
