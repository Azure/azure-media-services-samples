using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents the collection of marker items.
    /// </summary>
    /// <remarks>
    /// The actual layout of the markers on the timeline is done by the MarkerItemsPanel.
    /// 
    /// </remarks>
    public class MarkerItemsControl : ItemsControl
    {
        /// <summary>
        /// StartPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof (TimeSpan), typeof (MarkerItemsControl),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// EndPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register("EndPosition", typeof (TimeSpan), typeof (MarkerItemsControl),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// LivePosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LivePositionProperty =
            DependencyProperty.Register("LivePosition", typeof (TimeSpan?), typeof (MarkerItemsControl),
                                        new PropertyMetadata(null));

        /// <summary>
        /// ThumbWidth DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ThumbWidthProperty =
            DependencyProperty.Register("ThumbWidth", typeof (double), typeof (MarkerItemsControl),
                                        new PropertyMetadata((double) 0));

        /// <summary>
        /// IsLive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsLiveProperty =
            DependencyProperty.Register("IsLive", typeof (bool), typeof (MarkerItemsControl),
                                        new PropertyMetadata(false));

        public MarkerItemsControl()
        {
            DefaultStyleKey = typeof (MarkerItemsControl);

            Loaded += MarkerItemsControl_Loaded;
        }

        /// <summary>
        /// Gets or sets the start position of the markers.
        /// </summary>
        public TimeSpan StartPosition
        {
            get { return (TimeSpan) GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the end position of the markers.
        /// </summary>
        public TimeSpan EndPosition
        {
            get { return (TimeSpan) GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the live position.
        /// </summary>
        public TimeSpan? LivePosition
        {
            get { return (TimeSpan?) GetValue(LivePositionProperty); }
            set { SetValue(LivePositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the thumb in the timeline.
        /// </summary>
        public double ThumbWidth
        {
            get { return (double) GetValue(ThumbWidthProperty); }
            set { SetValue(ThumbWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a live stream.
        /// </summary>
        public bool IsLive
        {
            get { return (bool) GetValue(IsLiveProperty); }
            set { SetValue(IsLiveProperty, value); }
        }

        private void MarkerItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            var markerItemsPanel = this.GetVisualChild<MarkerItemsPanel>();
            markerItemsPanel.IfNotNull(InitializeBindings);
        }

        private void InitializeBindings(MarkerItemsPanel markerItemsPanel)
        {
            var startPositionBinding = new Binding("StartPosition")
            {
                Source =  this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.StartPositionProperty, startPositionBinding);

            var endPositionBinding = new Binding("EndPosition")
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.EndPositionProperty, endPositionBinding);

            var livePositionBinding = new Binding("LivePosition")
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.LivePositionProperty, livePositionBinding);

            var isLiveBinding = new Binding("IsLive")
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.IsLiveProperty, isLiveBinding);

            var markersBinding = new Binding("ItemsSource")
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.MarkersProperty, markersBinding);

            var thumbWidthBinding = new Binding("ThumbWidth")
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            markerItemsPanel.SetBinding(MarkerItemsPanel.ThumbWidthProperty, thumbWidthBinding);
        }
    }
}