using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Checks the status of markers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class tracks which markers have been reached, which markers have been left 
    /// (when media playing has passed the end position of a marker), 
    /// and which markers were skipped (due to the user seeking to a new position in the timeline, for example).
    /// This class raises the MarkerReached, MarkerLeft, and MarkerSkipped events respectively when these conditions occur.
    /// </para>
    /// </remarks>
    internal class SkippableMarkerManager<T> : MediaMarkerManager<T> where T : MediaMarker
    {
        /// <summary>
        /// Occurs when a marker start time was passed during a seek for a new position.
        /// </summary>
        public event Action<SkippableMarkerManager<T>, T> MarkerSkipped;

        /// <summary>
        /// Checks for markers that have been skipped.
        /// </summary>
        public void CheckForSkippedMarkers(TimeSpan mediaPosition, MediaMarkerCollection<T> markers)
        {
            if (PreviousPosition.HasValue)
            {
                bool movingForward = mediaPosition > PreviousPosition.Value;
                TimeSpan rangeStart = movingForward ? PreviousPosition.Value : mediaPosition;
                TimeSpan rangeEnd = movingForward ? mediaPosition : PreviousPosition.Value;

                var skippedMarkers = markers.WhereContainedByRange(rangeStart, rangeEnd);
                skippedMarkers.ForEach(OnMarkerSkipped);
            }
        }

        protected virtual void OnMarkerSkipped(T mediaMarker)
        {
            MarkerSkipped.IfNotNull(i => i(this, mediaMarker));
        }
    }
}