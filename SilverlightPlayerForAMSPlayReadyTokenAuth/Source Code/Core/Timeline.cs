using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents a control that shows a visual indicator of the duration of the current media and current position.
    /// </summary>
    /// <remarks>
    /// The Timeline has a collection of Markers that are loaded into the Timeline when a new media Playlist item is loaded. 
    /// The Timeline keeps track of the current position, start position, and end position. 
    /// These values are set each time a new media Playlist item is loaded.
    /// </remarks>
    public partial class Timeline : Slider
    {
        private bool _mouseCaptured;

        public Timeline()
        {
            DefaultStyleKey = typeof (Timeline);
            AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Timeline_MouseLeftButtonDown), true);
        }

        /// <summary>
        /// Gets or sets whether the timeline is scrubbing.
        /// </summary>
        public bool IsScrubbing
        {
            get
            {
                return _mouseCaptured
                       || (_horizontalThumb != null && _horizontalThumb.IsDragging);
            }
        }

        /// <summary>
        /// Occurs when a marker has been clicked on.
        /// </summary>
        public event EventHandler<CustomEventArgs<MediaMarker>> MarkerSelected;

        /// <summary>
        /// Occurs when the user begins scrubbing.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> ScrubbingStarted;

        /// <summary>
        /// Occurs when the user scrubs.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> Scrubbing;

        /// <summary>
        /// Occurs when the user completes scrubbing.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> ScrubbingCompleted;

        // if the mouse is _mouseCaptured, click and dragging on left or right side of _horizontalThumb

        internal void UpdateTimeline()
        {
            IsEnabled = EndPosition > StartPosition;

            if (!IsScrubbing)
            {
                SetSliderPosition(PlaybackPosition);
            }

            if (IsLive)
            {
                UpdateLiveAvailability();
            }
        }

        private void SetSliderPosition(TimeSpan position)
        {
            TimeSpan newPosition = GetInRangeValue(position);
            double newValue = ConvertPositionToValue(newPosition);
            SetSliderValue(newValue);
        }

        private void Timeline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var sourceElement = e.OriginalSource as FrameworkElement;
            MediaMarker marker = sourceElement != null
                                     ? sourceElement.DataContext as MediaMarker
                                     : null;

            if (marker != null && MarkerSelected != null)
            {
                MarkerSelected(this, new CustomEventArgs<MediaMarker>(marker));
            }
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            if (IsScrubbing && Scrubbing != null)
            {
                TimeSpan newPosition = ConvertValueToPosition(newValue);
                Scrubbing(this, new CustomEventArgs<TimeSpan>(newPosition));
            }
        }

        private void SetSliderValue(double newValue)
        {
            Dispatcher.BeginInvoke(() => Value = newValue);
        }

        private void UpdateLiveAvailability()
        {
            double range = Maximum - Minimum;

            // convert the available unit to a pixel width

            if (_horizontalAvailableBar != null)
            {
                if (_horizontalPanel != null && _horizontalPanel.ActualWidth > 0
                    && IsLive && LivePosition.HasValue && range > 0)
                {
                    // calculate the pixel width of the available bar, need to take into
                    // account the _horizontalThumb width, otherwise the _horizontalThumb position is calculated differently
                    // then the available bar (the _horizontalThumb position takes into account the _horizontalThumb width)
                    double availableRange = ConvertPositionToValue(LivePosition.Value);
                    double pixelValue = (availableRange/range)*
                                        (_horizontalPanel.ActualWidth - _horizontalThumb.ActualWidth);

                    // want the width of the available bar to be aligned with the right of the _horizontalThumb, this
                    // allows the live indictor to be correctly positioned to the right of the _horizontalThumb
                    pixelValue += _horizontalThumb.ActualWidth;

                    // make sure within range, Available can be negative when live stream first starts
                    pixelValue = Math.Min(_horizontalPanel.ActualWidth, pixelValue);
                    pixelValue = Math.Max(0, pixelValue);

                    _horizontalAvailableBar.Width = pixelValue;
                }
                else
                {
                    _horizontalAvailableBar.Width = 0;
                }
            }
        }


        private TimeSpan GetInRangeValue(TimeSpan position)
        {
            TimeSpan maxPosition = IsLive && LivePosition.HasValue
                                       ? LivePosition.Value
                                       : EndPosition;

            return position > maxPosition
                       ? maxPosition
                       : position < StartPosition
                             ? StartPosition
                             : position;
        }

        private TimeSpan ConvertValueToPosition(double value)
        {
            double totalSeconds = EndPosition.Subtract(StartPosition).TotalSeconds;
            double timelineProportion = Minimum != Maximum
                                            ? value/(Maximum - Minimum)
                                            : 0;
            double relativeSeconds = totalSeconds*timelineProportion;

            return StartPosition.Add(TimeSpan.FromSeconds(relativeSeconds));
        }

        private double ConvertPositionToValue(TimeSpan position)
        {
            TimeSpan relativePosition = position.Subtract(StartPosition);
            double totalSeconds = EndPosition.Subtract(StartPosition).TotalSeconds;
            double timelineProportion = totalSeconds != 0
                                            ? relativePosition.TotalSeconds/totalSeconds
                                            : 0;
            double relativeValue = (Maximum - Minimum)*timelineProportion;

            return Minimum + relativeValue;
        }

        protected TimeSpan? GetHorizontalPanelMousePosition(MouseEventArgs e)
        {
            TimeSpan? result = null;

            // take into account the scrubber _horizontalThumb size
            double thumbWidth = (_horizontalThumb == null) ? 0 : _horizontalThumb.ActualWidth;
            double panelWidth = _horizontalPanel.ActualWidth - thumbWidth;

            if (panelWidth > 0)
            {
                double range = Maximum - Minimum;

                // calculate the new newValue based on mouse position
                Point mousePosition = e.GetPosition(_horizontalPanel);
                double value = (mousePosition.X*range)/panelWidth;

                // right now, the _horizontalThumb will be left-justified to the cursor, take
                // into account the size of the _horizontalThumb and center it under the cursor
                value -= ((thumbWidth / 2) * range) / _horizontalPanel.ActualWidth;

                // offset from the min newValue
                value += Minimum;

                //PlaybackPosition = ConvertValueToPosition(value);
                result = ConvertValueToPosition(value);
            }

            return result;
        }
    }
}