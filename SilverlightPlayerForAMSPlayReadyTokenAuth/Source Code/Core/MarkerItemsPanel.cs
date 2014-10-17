using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Does the layout of the markers on the timeline.
    /// </summary>
    /// <remarks>
    /// Markers are arranged on the timeline relative to their Start positions. 
    /// </remarks>
    public class MarkerItemsPanel : Panel
    {
        /// <summary>
        /// ThumbWidth DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ThumbWidthProperty =
            DependencyProperty.Register("ThumbWidth", typeof (double),
                                        typeof (MarkerItemsPanel), null);

        /// <summary>
        /// StartPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof (TimeSpan), typeof (MarkerItemsPanel),
                                        new PropertyMetadata(TimeSpan.Zero, OnDependencyPropertyChanged));

        /// <summary>
        /// EndPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register("EndPosition", typeof (TimeSpan), typeof (MarkerItemsPanel),
                                        new PropertyMetadata(TimeSpan.Zero, OnDependencyPropertyChanged));

        /// <summary>
        /// LivePosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LivePositionProperty =
            DependencyProperty.Register("LivePosition", typeof (TimeSpan?), typeof (MarkerItemsPanel),
                                        new PropertyMetadata(null, OnDependencyPropertyChanged));

        /// <summary>
        /// IsLive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsLiveProperty =
            DependencyProperty.Register("IsLive", typeof (bool), typeof (MarkerItemsPanel),
                                        new PropertyMetadata(false, OnDependencyPropertyChanged));

        /// <summary>
        /// Markers DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MarkersProperty =
            DependencyProperty.Register("Markers", typeof (ObservableCollection<MediaMarker>),
                                        typeof (MarkerItemsPanel),
                                        new PropertyMetadata(OnMarkersPropertyChanged));

        /// <summary>
        /// DisplayMarkersOutsideLiveWindow DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DisplayMarkersOutsideLiveWindowProperty =
            DependencyProperty.Register("DisplayMarkersOutsideLiveWindow", typeof (bool), typeof (MarkerItemsPanel),
                                        new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the width of the thumb in the timeline (used to calculate the position of the markers).
        /// </summary>
        public double ThumbWidth
        {
            get { return (double) GetValue(ThumbWidthProperty); }
            set { SetValue(ThumbWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the start position of the timeline.
        /// </summary>
        public TimeSpan StartPosition
        {
            get { return (TimeSpan) GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the end position of the timeline.
        /// </summary>
        public TimeSpan EndPosition
        {
            get { return (TimeSpan) GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the live position of the timeline.
        /// </summary>
        public TimeSpan? LivePosition
        {
            get { return (TimeSpan?) GetValue(LivePositionProperty); }
            set { SetValue(LivePositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this item is a live stream.
        /// </summary>
        public bool IsLive
        {
            get { return (bool) GetValue(IsLiveProperty); }
            set { SetValue(IsLiveProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of markers that will be positioned on the timeline.
        /// </summary>
        public ObservableCollection<MediaMarker> Markers
        {
            get { return (ObservableCollection<MediaMarker>) GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display markers outside of the window playing live media.
        /// </summary>
        public bool DisplayMarkersOutsideLiveWindow
        {
            get { return (bool) GetValue(DisplayMarkersOutsideLiveWindowProperty); }
            set { SetValue(DisplayMarkersOutsideLiveWindowProperty, value); }
        }

        // dependency property notification
        private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkerItemsPanel;
            source.IfNotNull(i => i.InvalidateArrange());
        }

        // dependency property notification
        private static void OnMarkersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkerItemsPanel;
            source.IfNotNull(i => i.OnMarkersChanged(e.OldValue));
        }

        private void OnMarkersChanged(object oldValue)
        {
            var oldMarkers = oldValue as ObservableCollection<MediaMarker>;

            oldMarkers.IfNotNull(i => i.CollectionChanged -= Markers_CollectionChanged);
            Markers.IfNotNull(i => i.CollectionChanged += Markers_CollectionChanged);

            InvalidateArrange();
        }

        private void Markers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // update markers on timeline
            InvalidateArrange();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // this is the first pass in the layout, each control updates
            // its DesiredSize property, which is used later in ArrangeOverride
            Children.ForEach(i => i.Measure(availableSize));
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // use live position when in live, otherwise end position, 
            // this is the bounds where markers are visisble
            TimeSpan endPosition = IsLive && LivePosition.HasValue && !DisplayMarkersOutsideLiveWindow
                                       ? LivePosition.Value
                                       : EndPosition;

            if (endPosition > StartPosition)
            {
                // go through each marker control and layout on the timeline
                foreach (UIElement childControl in Children)
                {
                    var contentPresenter = childControl as ContentPresenter;
                    MediaMarker mediaMarker = contentPresenter != null
                                                  ? contentPresenter.Content as MediaMarker
                                                  : null;

                    if (mediaMarker != null)
                    {
                        // make sure the marker is within the timeline range
                        if (mediaMarker.Begin < StartPosition || mediaMarker.Begin > endPosition)
                        {
                            // don't display the marker
                            childControl.Arrange(new Rect(0, 0, 0, 0));
                        }
                        else
                        {
                            // convert the absolute time to a relative timeline time
                            TimeSpan time = mediaMarker.Begin - StartPosition;

                            // calculate the top position, center the marker vertically
                            double top = (finalSize.Height - childControl.DesiredSize.Height)/2;

                            // calculate the left position, first get the pixel position within the timeline
                            double left = (time.TotalSeconds*(finalSize.Width - ThumbWidth))/
                                          (endPosition.TotalSeconds - StartPosition.TotalSeconds);

                            // next adjust the position so the center of the control is at the time on the timeline,
                            // note that the marker control can overhang the left or right side of the timeline
                            left += ((ThumbWidth - childControl.DesiredSize.Width)/2);

                            // display the marker
                            childControl.Arrange(new Rect(left, top, childControl.DesiredSize.Width,
                                                          childControl.DesiredSize.Height));
                        }
                    }
                }
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}