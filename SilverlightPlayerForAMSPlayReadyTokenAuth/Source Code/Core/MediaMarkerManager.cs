using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core
{
    internal class MediaMarkerManager<TMediaMarker> where TMediaMarker : MediaMarker
    {
        private const long LargestNormalStepSizeMillis = 1000;
        private readonly object _syncObject = new object();

        protected TimeSpan? PreviousPosition { get; private set; }
        protected IList<TMediaMarker> PreviouslyActiveMarkers { get; private set; }
        public TimeSpan? SeekingSearchWindow { get; set; }
        public TimeSpan LargestNormalStepSize { get; set; }

        /// <summary>
        /// Occurs when a marker start time is reached.
        /// </summary>
        public event Action<MediaMarkerManager<TMediaMarker>, TMediaMarker, bool> MarkerReached;

        /// <summary>
        /// Occurs when a marker end time has been reached.
        /// </summary>
        public event Action<MediaMarkerManager<TMediaMarker>, TMediaMarker> MarkerLeft;
        
        public MediaMarkerManager()
        {
            PreviouslyActiveMarkers = new List<TMediaMarker>();
            LargestNormalStepSize = TimeSpan.FromMilliseconds(LargestNormalStepSizeMillis);
        }

        public void Reset()
        {
            PreviousPosition = null;
            PreviouslyActiveMarkers.Clear();
        }

        /// <summary>
        /// Checks for markers whose position has been reached or left.
        /// </summary>
        public void CheckMarkerPositions(TimeSpan mediaPosition, MediaMarkerCollection<TMediaMarker> markers, bool seeking = false, bool ignoreSearchWindow = false)
        {
            lock (_syncObject)
            {
                // clean up any previously active markers that are no longer in the markers list
                PreviouslyActiveMarkers.ToList()
                                       .Where(i => !markers.Contains(i))
                                       .ForEach(OnMarkerLeft)
                                       .ForEach(i => PreviouslyActiveMarkers.Remove(i));

                //Safeguard against large unusual position changes
                if (!seeking && PreviousPosition.HasValue)
                {
                    var rangeStart = PreviousPosition.Value < mediaPosition ? PreviousPosition.Value : mediaPosition;
                    var rangeEnd = PreviousPosition.Value > mediaPosition ? PreviousPosition.Value : mediaPosition;

					seeking = rangeEnd.Subtract(rangeStart) > LargestNormalStepSize;
                }

                IList<TMediaMarker> activeMarkers;
                var searchAfter = SeekingSearchWindow.HasValue && !ignoreSearchWindow
                                        ? mediaPosition.Subtract(SeekingSearchWindow.Value)
                                        : (TimeSpan?)null;

                if (!seeking && PreviousPosition.HasValue)
                {
                    var rangeStart = PreviousPosition.Value < mediaPosition ? PreviousPosition.Value : mediaPosition;
                    var rangeEnd = PreviousPosition.Value > mediaPosition ? PreviousPosition.Value : mediaPosition;
                    activeMarkers = markers.WhereActiveInRange(rangeStart, rangeEnd, searchAfter).ToList();
                }
                else
                {
                    activeMarkers = markers.WhereActiveAtPosition(mediaPosition, searchAfter).ToList();
                }


                PreviouslyActiveMarkers.Where(i => !i.IsActiveAtPosition(mediaPosition))
                                       .ForEach(OnMarkerLeft)
                                       .ForEach(i => activeMarkers.Remove(i));

                activeMarkers.Where(i => !PreviousPosition.HasValue || !PreviouslyActiveMarkers.Contains(i))
                             .ForEach(i => OnMarkerReached(i, seeking));

                PreviouslyActiveMarkers = PreviouslyActiveMarkers.Where(i => i.IsActiveAtPosition(mediaPosition) && !activeMarkers.Contains(i))
                                                                 .Concat(activeMarkers)
                                                                 .ToList();
                PreviousPosition = mediaPosition;
            }
        }

        public void CheckMarkerPosition(TimeSpan mediaPosition, TMediaMarker marker, bool seeking = false)
        {
            lock (_syncObject)
            {
                bool isActive = false;

                if (!seeking && PreviousPosition.HasValue)
                {
                    var rangeStart = PreviousPosition.Value < mediaPosition ? PreviousPosition.Value : mediaPosition;
                    var rangeEnd = PreviousPosition.Value > mediaPosition ? PreviousPosition.Value : mediaPosition;
                    isActive = marker.IsActiveInRange(rangeStart, rangeEnd);
                }
                else
                {
                    isActive = marker.IsActiveAtPosition(mediaPosition);
                }

                if (isActive && (!PreviousPosition.HasValue || !PreviouslyActiveMarkers.Contains(marker)))
                {
                    OnMarkerReached(marker, seeking);
                    PreviouslyActiveMarkers.Add(marker);
                }
            }
        }

        protected virtual void OnMarkerReached(TMediaMarker mediaMarker, bool skippedInto)
        {
            MarkerReached.IfNotNull(i => i(this, mediaMarker, skippedInto));
        }

        protected virtual void OnMarkerLeft(TMediaMarker mediaMarker)
        {
            MarkerLeft.IfNotNull(i => i(this, mediaMarker));
        }

    }
}
