using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents a marker that can be displayed on the Timeline.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The position of a TimelineMediaMarker represents its specific time location in a media clip.
    /// </para>
    /// <para>
    /// A display marker can have associated text that pops up when the user hovers over that marker in the timeline. 
    /// A display marker can be clickable so if a user clicks on the marker, the current play position 
    /// of the media jumps to that location and begins playing there.
    /// </para>
    /// </remarks>
    [ScriptableType]
    public class TimelineMediaMarker : MediaMarker
    {
        private bool _allowSeek = true;

        public TimelineMediaMarker()
        {
            Type = "timeline";
        }

        private TimelineMediaMarker(TimelineMediaMarker timelineMediaMarker)
            : base(timelineMediaMarker)
        {
            AllowSeek = timelineMediaMarker.AllowSeek;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a user clicking this marker causes playback to jump to the beginning position of this marker.
        /// </summary>
        public bool AllowSeek
        {
            get { return _allowSeek; }
            set
            {
                if (_allowSeek != value)
                {
                    _allowSeek = value;
                    NotifyPropertyChanged("AllowSeek");
                }
            }
        }

        public TimelineMediaMarker Clone()
        {
            return new TimelineMediaMarker(this);
        }
    }
}