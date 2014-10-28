using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    [TemplatePart(Name = TimelineTemplateParts.HorizontalTemplate, Type = typeof (Panel))]
    [TemplatePart(Name = TimelineTemplateParts.HorizontalThumb, Type = typeof (Thumb))]
    [TemplatePart(Name = TimelineTemplateParts.HorizontalTrackLargeChangeIncreaseRepeatButton, Type = typeof (RepeatButton))]
    [TemplatePart(Name = TimelineTemplateParts.HorizontalTrackLargeChangeDecreaseRepeatButton, Type = typeof (RepeatButton))]
    [TemplatePart(Name = TimelineTemplateParts.HorizontalAvailableBar, Type = typeof (Border))]
    [TemplatePart(Name = TimelineTemplateParts.MarkersItemControl, Type = typeof (ItemsControl))]
    //[TemplatePart(Name = TimelineTemplateParts.MarkerElement, Type = typeof(MarkerControl))]
    [TemplateVisualState(Name = TimelineVisualStates.LiveStates.Live, GroupName = TimelineVisualStates.GroupNames.LiveStates)]
    [TemplateVisualState(Name = TimelineVisualStates.LiveStates.Vod,GroupName = TimelineVisualStates.GroupNames.LiveStates)]
    [TemplateVisualState(Name = TimelineVisualStates.ScrubbingStates.IsScrubbing, GroupName = TimelineVisualStates.GroupNames.ScrubbingStates)]
    [TemplateVisualState(Name = TimelineVisualStates.ScrubbingStates.IsNotScrubbing, GroupName = TimelineVisualStates.GroupNames.ScrubbingStates)]
    public partial class Timeline
    {
        // template controls
        private Border _horizontalAvailableBar;
        private Panel _horizontalPanel;
        private Thumb _horizontalThumb;
        private RepeatButton _largeChangeDecreaseButton;
        private RepeatButton _largeChangeIncreaseButton;

        #region Template Children

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UninitializeTemplateChildren();
            GetTemplateChildren();
            InitializeTemplateChildren();
        }

        private void GetTemplateChildren()
        {
            _horizontalPanel = GetTemplateChild(TimelineTemplateParts.HorizontalTemplate) as Panel;
            _horizontalThumb = GetTemplateChild(TimelineTemplateParts.HorizontalThumb) as Thumb;
            _horizontalAvailableBar = GetTemplateChild(TimelineTemplateParts.HorizontalAvailableBar) as Border;
            _largeChangeIncreaseButton =
                GetTemplateChild(TimelineTemplateParts.HorizontalTrackLargeChangeDecreaseRepeatButton) as RepeatButton;
            _largeChangeDecreaseButton =
                GetTemplateChild(TimelineTemplateParts.HorizontalTrackLargeChangeIncreaseRepeatButton) as RepeatButton;
        }

        private void InitializeTemplateChildren()
        {
            // workaround for mouse events, don't want the left / right areas to 
            // get mouse events since it prevents the position from being updated
            // by clicking in the left / right area and dragging
            _largeChangeIncreaseButton.IfNotNull(i => i.IsHitTestVisible = false);
            _largeChangeDecreaseButton.IfNotNull(i => i.IsHitTestVisible = false);

            if (_horizontalPanel != null)
            {
                _horizontalPanel.IsHitTestVisible = true;
                _horizontalPanel.MouseLeftButtonDown += HorizontalPanelMouseLeftButtonDown;
                _horizontalPanel.MouseLeftButtonUp += HorizontalPanelMouseLeftButtonUp;
                _horizontalPanel.MouseMove += HorizontalPanelMouseMove;
                _horizontalPanel.SizeChanged += HorizontalPanelSizeChanged;
            }

            if (_horizontalThumb != null)
            {
                _horizontalThumb.DragStarted += HorizontalThumbDragStarted;
                _horizontalThumb.DragDelta += HorizontalThumbDragDelta;
                _horizontalThumb.DragCompleted += HorizontalThumbDragCompleted;
            }
        }

        private void UninitializeTemplateChildren()
        {
            // main container
            if (_horizontalPanel != null)
            {
                _horizontalPanel.MouseLeftButtonDown -= HorizontalPanelMouseLeftButtonDown;
                _horizontalPanel.MouseLeftButtonUp -= HorizontalPanelMouseLeftButtonUp;
                _horizontalPanel.MouseMove -= HorizontalPanelMouseMove;
                _horizontalPanel.SizeChanged -= HorizontalPanelSizeChanged;
            }

            // _horizontalThumb
            if (_horizontalThumb != null)
            {
                _horizontalThumb.DragStarted -= HorizontalThumbDragStarted;
                _horizontalThumb.DragCompleted -= HorizontalThumbDragCompleted;
            }
        }

        #endregion

        #region Dependency Properties

        #region TimelinePosition
        /// <summary>
        /// PlaybackPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaybackPositionProperty =
            DependencyProperty.Register("PlaybackPosition", typeof (TimeSpan), typeof (Timeline),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets the playback position of the media.
        /// </summary>
        public TimeSpan PlaybackPosition
        {
            get { return (TimeSpan) GetValue(PlaybackPositionProperty); }
            set { SetValue(PlaybackPositionProperty, value); }
        }

        #endregion

        #region StartPosition
        /// <summary>
        /// StartPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof (TimeSpan), typeof (Timeline),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets the start position of the media.
        /// </summary>
        public TimeSpan StartPosition
        {
            get { return (TimeSpan) GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        #endregion

        #region EndPosition
        /// <summary>
        /// EndPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register("EndPosition", typeof (TimeSpan), typeof (Timeline),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets the end position of the media.
        /// </summary>
        public TimeSpan EndPosition
        {
            get { return (TimeSpan) GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        #endregion

        #region LivePosition
        /// <summary>
        /// LivePosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LivePositionProperty =
            DependencyProperty.Register("LivePosition", typeof (TimeSpan?), typeof (Timeline), null);

        /// <summary>
        /// Gets or sets the live position of the media.
        /// </summary>
        public TimeSpan? LivePosition
        {
            get { return (TimeSpan?) GetValue(LivePositionProperty); }
            set { SetValue(LivePositionProperty, value); }
        }

        #endregion

        #region IsLive
        /// <summary>
        /// IsLive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsLiveProperty =
            DependencyProperty.Register("IsLive", typeof (bool), typeof (Timeline),
                                        new PropertyMetadata(false, OnIsLivePropertyChanged));

        /// <summary>
        /// Gets or sets whether the current media is live.
        /// </summary>
        public bool IsLive
        {
            get { return (bool) GetValue(IsLiveProperty); }
            set { SetValue(IsLiveProperty, value); }
        }

        private static void OnIsLivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeline = d as Timeline;
            timeline.IfNotNull(i => i.OnIsLiveChanged());
        }

        private void OnIsLiveChanged()
        {
            string state = IsLive
                               ? TimelineVisualStates.LiveStates.Live
                               : TimelineVisualStates.LiveStates.Vod;
            this.GoToVisualState(state);
            UpdateLiveAvailability();
        }

        #endregion

        #region Markers
        /// <summary>
        /// TimelineMarkers DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty TimelineMarkersProperty =
            DependencyProperty.Register("TimelineMarkers", typeof (ObservableCollection<TimelineMediaMarker>),
                                        typeof (Timeline),
                                        new PropertyMetadata(new ObservableCollection<TimelineMediaMarker>(),
                                                             OnTimelineMarkersPropertyChanged));

        /// <summary>
        /// Chapters DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ChaptersProperty =
            DependencyProperty.Register("Chapters", typeof (ObservableCollection<Chapter>), typeof (Timeline),
                                        new PropertyMetadata(new ObservableCollection<Chapter>(),
                                                             OnChaptersPropertyChanged));

        /// <summary>
        /// Markers DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MarkersProperty =
            DependencyProperty.Register("Markers", typeof (ObservableCollection<MediaMarker>), typeof (Timeline),
                                        new PropertyMetadata(new ObservableCollection<MediaMarker>()));

        /// <summary>
        /// Gets or sets a collection of timeline markers to be displayed on the timeline.
        /// </summary>
        public ObservableCollection<TimelineMediaMarker> TimelineMarkers
        {
            get { return (ObservableCollection<TimelineMediaMarker>) GetValue(TimelineMarkersProperty); }
            set { SetValue(TimelineMarkersProperty, value); }
        }

        /// <summary>
        /// Gets or sets a collection of chapters to be displayed on the timeline.
        /// </summary>
        public ObservableCollection<Chapter> Chapters
        {
            get { return (ObservableCollection<Chapter>) GetValue(ChaptersProperty); }
            set { SetValue(ChaptersProperty, value); }
        }

        /// <summary>
        /// Gets or sets an aggregation of TimelineMarkers and Chapters used to display them all with a MarkersItemControl.
        /// </summary>
        public ObservableCollection<MediaMarker> Markers
        {
            get { return (ObservableCollection<MediaMarker>) GetValue(MarkersProperty); }
            set { SetValue(MarkersProperty, value); }
        }

        private static void OnTimelineMarkersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var timeline = d as Timeline;
            var oldTimelineMarkers = args.OldValue as ObservableCollection<TimelineMediaMarker>;
            var newTimelineMarkers = args.NewValue as ObservableCollection<TimelineMediaMarker>;

            if (timeline != null)
            {
                oldTimelineMarkers.IfNotNull(i => i.CollectionChanged -= (s, e) => timeline.UpdateMarkers());
                newTimelineMarkers.IfNotNull(i => i.CollectionChanged += (s, e) => timeline.UpdateMarkers());
                timeline.UpdateMarkers();
            }
        }

        private static void OnChaptersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var timeline = d as Timeline;
            var oldChapters = args.OldValue as ObservableCollection<Chapter>;
            var newChapters = args.NewValue as ObservableCollection<Chapter>;

            if (timeline != null)
            {
                oldChapters.IfNotNull(i => i.CollectionChanged -= (s, e) => timeline.UpdateMarkers());
                newChapters.IfNotNull(i => i.CollectionChanged += (s, e) => timeline.UpdateMarkers());
                timeline.UpdateMarkers();
            }
        }

        private void UpdateMarkers()
        {
            var markers = new List<MediaMarker>();
            if (TimelineMarkers != null)
            {
                TimelineMarkers.ForEach(markers.Add);
            }

            if (Chapters != null)
            {
                Chapters.ForEach(markers.Add);
            }

            Markers = markers.ToObservableCollection();
        }

        #endregion

        #endregion

        #region Event Handlers

        private void HorizontalPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // bar size changed, update available bar
            UpdateLiveAvailability();
        }

        private void HorizontalThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            UpdateScrubbingVisualState();
            if (ScrubbingStarted != null)
            {
                TimeSpan newPosition = ConvertValueToPosition(Value);
                ScrubbingStarted(this, new CustomEventArgs<TimeSpan>(newPosition));
            }
        }

        private void HorizontalThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            TimeSpan newPosition = ConvertValueToPosition(Value);
            newPosition = GetInRangeValue(newPosition);
            Value = ConvertPositionToValue(newPosition);
        }

        private void HorizontalThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateScrubbingVisualState();
            if (ScrubbingCompleted != null)
            {
                TimeSpan newPosition = ConvertValueToPosition(Value);
                ScrubbingCompleted(this, new CustomEventArgs<TimeSpan>(newPosition));

            }
        }

        private void HorizontalPanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _mouseCaptured = _horizontalPanel.CaptureMouse();
            UpdateScrubbingVisualState();
            if (_mouseCaptured && ScrubbingStarted != null)
            {
                TimeSpan? newPosition = GetHorizontalPanelMousePosition(e);
                if (newPosition.HasValue)
                {
                    SetSliderPosition(newPosition.Value);
                    ScrubbingStarted(this, new CustomEventArgs<TimeSpan>(newPosition.Value));
                }
            }
        }

        private void HorizontalPanelMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_mouseCaptured)
            {
                _horizontalPanel.ReleaseMouseCapture();
                _mouseCaptured = false;

                UpdateScrubbingVisualState();
                if (ScrubbingCompleted != null)
                {
                    TimeSpan? timelinePosition = GetHorizontalPanelMousePosition(e);

                    if (timelinePosition.HasValue)
                    {
                        var newPosition = GetInRangeValue(timelinePosition.Value);
    
                        SetSliderPosition(newPosition);
                        ScrubbingCompleted(this, new CustomEventArgs<TimeSpan>(newPosition));
                    }
                }
            }
        }

        private void HorizontalPanelMouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseCaptured && Scrubbing != null)
            {
                TimeSpan? timelinePosition = GetHorizontalPanelMousePosition(e);

                if (timelinePosition.HasValue)
                {
                    var newPosition = GetInRangeValue(timelinePosition.Value);
                    SetSliderPosition(newPosition);
                    Scrubbing(this, new CustomEventArgs<TimeSpan>(newPosition));
                }
            }
        }

        #endregion

        #region Misc
        private void UpdateScrubbingVisualState()
        {
            var state = IsScrubbing
                            ? TimelineVisualStates.ScrubbingStates.IsScrubbing
                            : TimelineVisualStates.ScrubbingStates.IsNotScrubbing;
            this.GoToVisualState(state);
        }
        #endregion
    }
}