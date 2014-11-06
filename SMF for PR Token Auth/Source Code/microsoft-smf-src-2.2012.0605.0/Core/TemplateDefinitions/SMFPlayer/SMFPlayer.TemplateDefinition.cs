using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Core.Data;
#if !WINDOWS_PHONE
using Microsoft.SilverlightMediaFramework.Core.Javascript;
#endif
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Converters;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Controls;
using System.Collections.Specialized;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Core
{
    //Visual States
    [TemplateVisualState(Name = SMFPlayerVisualStates.CommonStates.Normal, GroupName = SMFPlayerVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.CommonStates.MouseOver, GroupName = SMFPlayerVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.AudioStreamMetadataAvailableStates.AudioStreamMetadataAvailable, GroupName = SMFPlayerVisualStates.GroupNames.AudioStreamMetadataAvailableStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.AudioStreamMetadataAvailableStates.AudioStreamMetadataNotAvailable, GroupName = SMFPlayerVisualStates.GroupNames.AudioStreamMetadataAvailableStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.MediaLiveStates.MediaNotLive, GroupName = SMFPlayerVisualStates.GroupNames.MediaLiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.MediaLiveStates.MediaLive, GroupName = SMFPlayerVisualStates.GroupNames.MediaLiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PositionLiveStates.PositionLive, GroupName = SMFPlayerVisualStates.GroupNames.PositionLiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PositionLiveStates.PositionNotLive, GroupName = SMFPlayerVisualStates.GroupNames.PositionLiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.FullScreenStates.FullScreen, GroupName = SMFPlayerVisualStates.GroupNames.FullScreenStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.FullScreenStates.NotFullScreen, GroupName = SMFPlayerVisualStates.GroupNames.FullScreenStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.MediaAdaptiveStates.MediaAdaptive, GroupName = SMFPlayerVisualStates.GroupNames.MediaAdaptiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.MediaAdaptiveStates.MediaNotAdaptive, GroupName = SMFPlayerVisualStates.GroupNames.MediaAdaptiveStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.ChapterStates.ChaptersHidden, GroupName = SMFPlayerVisualStates.GroupNames.ChapterStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.ChapterStates.ChaptersVisible, GroupName = SMFPlayerVisualStates.GroupNames.ChapterStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleVisible, GroupName = SMFPlayerVisualStates.GroupNames.LoggingConsoleStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleHidden, GroupName = SMFPlayerVisualStates.GroupNames.LoggingConsoleStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleDisabled, GroupName = SMFPlayerVisualStates.GroupNames.LoggingConsoleStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaylistStates.PlaylistHidden, GroupName = SMFPlayerVisualStates.GroupNames.PlaylistStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaylistStates.PlaylistVisible, GroupName = SMFPlayerVisualStates.GroupNames.PlaylistStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaylistStates.PlaylistDisabled, GroupName = SMFPlayerVisualStates.GroupNames.PlaylistStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PosterStates.PosterHidden, GroupName = SMFPlayerVisualStates.GroupNames.PosterStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PosterStates.PosterVisible, GroupName = SMFPlayerVisualStates.GroupNames.PosterStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.CaptionsStates.CaptionsHidden, GroupName = SMFPlayerVisualStates.GroupNames.CaptionsStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.CaptionsStates.CaptionsVisible, GroupName = SMFPlayerVisualStates.GroupNames.CaptionsStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.CaptionsStates.CaptionsDisabled, GroupName = SMFPlayerVisualStates.GroupNames.CaptionsStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.AcquiringLicense, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Buffering, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Closed, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Individualizing, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Opening, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Paused, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Playing, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.Stopped, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayStates.ClipPlaying, GroupName = SMFPlayerVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaySpeedStates.FastForwarding, GroupName = SMFPlayerVisualStates.GroupNames.PlaySpeedStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaySpeedStates.NormalPlayback, GroupName = SMFPlayerVisualStates.GroupNames.PlaySpeedStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaySpeedStates.Rewinding, GroupName = SMFPlayerVisualStates.GroupNames.PlaySpeedStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlaySpeedStates.SlowMotion, GroupName = SMFPlayerVisualStates.GroupNames.PlaySpeedStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.SlowMotionEnabledStates.SlowMotionEnabled, GroupName = SMFPlayerVisualStates.GroupNames.SlowMotionEnabledStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.SlowMotionEnabledStates.SlowMotionDisabled, GroupName = SMFPlayerVisualStates.GroupNames.SlowMotionEnabledStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.VersionInformationStates.VersionInformationVisible, GroupName = SMFPlayerVisualStates.GroupNames.VersionInformationStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.VersionInformationStates.VersionInformationHidden, GroupName = SMFPlayerVisualStates.GroupNames.VersionInformationStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.VersionInformationStates.VersionInformationDisabled, GroupName = SMFPlayerVisualStates.GroupNames.VersionInformationStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.RetryingStates.Retrying, GroupName = SMFPlayerVisualStates.GroupNames.RetryingStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.RetryingStates.NotRetrying, GroupName = SMFPlayerVisualStates.GroupNames.RetryingStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.RetryingStates.RetryFailed, GroupName = SMFPlayerVisualStates.GroupNames.RetryingStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphVisible, GroupName = SMFPlayerVisualStates.GroupNames.PlayerGraphStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphHidden, GroupName = SMFPlayerVisualStates.GroupNames.PlayerGraphStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphDisabled, GroupName = SMFPlayerVisualStates.GroupNames.PlayerGraphStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.ControlStripStates.ControlStripVisible, GroupName = SMFPlayerVisualStates.GroupNames.ControlStripStates)]
    [TemplateVisualState(Name = SMFPlayerVisualStates.ControlStripStates.ControlStripNotVisible, GroupName = SMFPlayerVisualStates.GroupNames.ControlStripStates)]

    //Template Parts
    [TemplatePart(Name = SMFPlayerTemplateParts.AudioStreamSelectionElement, Type = typeof(Selector))]
    [TemplatePart(Name = SMFPlayerTemplateParts.BitrateGraphElement, Type = typeof(BitrateGraphControl))]
    [TemplatePart(Name = SMFPlayerTemplateParts.CaptionsPresenterElement, Type = typeof(CaptionsPresenter))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ChapterSelectionElement, Type = typeof(Selector))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ChaptersToggleElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.CaptionToggleElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ControlStripToggleElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.FastForwardElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.FramerateGraphElement, Type = typeof(FramerateGraphControl))]
    [TemplatePart(Name = SMFPlayerTemplateParts.FullScreenToggleElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.HidePlaylistElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.HideChaptersElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.GraphToggleElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.LoggingDisplayElement, Type = typeof(TextBox))]
    [TemplatePart(Name = SMFPlayerTemplateParts.MaxBitrateLimiterElement, Type = typeof(MaxBitrateLimiterControl))]
    [TemplatePart(Name = SMFPlayerTemplateParts.NextChapterElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.NextPlaylistItemElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PlayElement, Type = typeof(PlayElement))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PlayerRoot, Type = typeof(Grid))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PlaylistSelectionElement, Type = typeof(Selector))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PlaylistToggleElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PreviousChapterElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.PreviousPlaylistItemElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ReplayElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.RewindElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.SeekToLiveElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ShowPlaylistElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.ShowChaptersElement, Type = typeof(ButtonBase))]
    [TemplatePart(Name = SMFPlayerTemplateParts.SlowMotionElement, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SMFPlayerTemplateParts.TimelineMarkerSelectionElement, Type = typeof(Selector))]
    [TemplatePart(Name = SMFPlayerTemplateParts.TimelineElement, Type = typeof(Timeline))]
    [TemplatePart(Name = SMFPlayerTemplateParts.MediaPresenterElement, Type = typeof(ContentControl))]
    [TemplatePart(Name = SMFPlayerTemplateParts.VideoAreaElement, Type = typeof(Panel))]
    [TemplatePart(Name = SMFPlayerTemplateParts.VersionInformationElement, Type = typeof(ContentControl))]
    [TemplatePart(Name = SMFPlayerTemplateParts.VolumeElement, Type = typeof(VolumeControl))]
    public partial class SMFPlayer
    {
        protected Selector AudioStreamSelectionElement { get; private set; }
        protected BitrateGraphControl BitrateGraphElement { get; set; }
        protected ToggleButton CaptionDisplayToggleElement { get; private set; }
        protected CaptionsPresenter CaptionsPresenterElement { get; set; }
        protected Selector ChapterSelectionElement { get; private set; }
        protected ToggleButton ChaptersDisplayToggleElement { get; private set; }
        protected ButtonBase ControlStripToggleElement { get; private set; }
        protected ButtonBase FastForwardElement { get; private set; }
        protected FramerateGraphControl FramerateGraphElement { get; set; }
        protected ToggleButton FullScreenToggleElement { get; private set; }
        protected ButtonBase HideChaptersElement { get; private set; }
        protected ButtonBase HidePlaylistElement { get; private set; }
        protected ButtonBase NextChapterElement { get; private set; }
        protected ButtonBase NextPlaylistItemElement { get; private set; }
        protected PlayElement PlayElement { get; private set; }
        protected Selector PlaylistSelectionElement { get; private set; }
        protected ToggleButton PlaylistDisplayToggleElement { get; private set; }
        protected ButtonBase PreviousChapterElement { get; private set; }
        protected ButtonBase PreviousPlaylistItemElement { get; private set; }
        protected ButtonBase ReplayElement { get; private set; }
        protected ButtonBase RewindElement { get; private set; }
        protected ButtonBase SeekToLiveElement { get; private set; }
        protected ButtonBase ShowChaptersElement { get; private set; }
        protected ButtonBase ShowPlaylistElement { get; private set; }
        protected ToggleButton SlowMotionElement { get; private set; }
        protected ContentControl MediaPresenterElement { get; private set; }
        protected Panel VideoAreaElement { get; private set; }
        protected Selector TimelineMarkerSelectionElement { get; private set; }
        protected Timeline TimelineElement { get; private set; }
        protected ContentControl VersionInformationElement { get; private set; }
        protected VolumeControl VolumeElement { get; private set; }
        protected TextBox LoggingDisplayElement { get; private set; }
        protected ToggleButton GraphToggleElement { get; private set; }
        protected MaxBitrateLimiterControl MaxBitrateLimiterElement { get; set; }
        protected Grid PlayerRoot { get; set; }
        protected Button ButtonRetryElement { get; set; }

        private void SetDefaultVisualStates()
        {
            this.GoToVisualState(SMFPlayerVisualStates.FullScreenStates.NotFullScreen);
            this.GoToVisualState(SMFPlayerVisualStates.PlaySpeedStates.NormalPlayback);
            this.GoToVisualState(SMFPlayerVisualStates.PlayStates.Closed);
            this.GoToVisualState(SMFPlayerVisualStates.PosterStates.PosterHidden);

            OnIsMediaAdaptiveChanged();
            OnAvailableAudioStreamsChanged();
            OnCaptionsVisibilityChanged();
            OnIsControlStripVisibleChanged(true);
            OnChaptersVisibilityChanged();
            OnIsMediaLiveChanged();
            OnLoggingConsoleVisibilityChanged();
            OnPlayerGraphVisibilityChanged();
            OnVersionInformationVisibilityChanged();
            OnPlaylistVisibilityChanged();
            OnRetryStatePropertyChanged();
            OnIsSlowMotionEnabledChanged();
        }

        private static bool IsCaptionStream(IMediaStream mediaStream)
        {
            return mediaStream.Type == StreamType.Text
                   &&
                   AllowedCaptionStreamSubTypes.Any(
                       i => string.Equals(i, mediaStream.SubType, StringComparison.CurrentCultureIgnoreCase));
        }

        #region Template Children

        /// <summary>
        /// Builds the visual tree when a new template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UninitializeTemplateChildren();
            GetTemplateChildren();
            InitializeTemplateChildren();
            SetDefaultVisualStates();
            InitializeProperties();
            _isTemplateApplied = true;

            PlayerId = PlayerIdDataClient.PlayerId;

            if (!IsInDesignMode)
            {
                IList<string> logWriterIds = LogWriters.ToDelimitedList();
                LoadLoggingPlugins(logWriterIds);
                LoadUIPlugins();
                LoadGenericPlugins();
				LoadS3DPlugins();
                LoadAdPayloadHandlerPlugins();
                LoadHeuristicsPlugin();

                bool playlistItemAssigned = CurrentPlaylistItem != null;
                Playlist.IfNotNull(i => OnPlaylistChanged());

                if (playlistItemAssigned)
                {
                    OnCurrentPlaylistItemChanged(null);
                }
            }
        }

        private void InitializeProperties()
        {
            AdMarkers = AdMarkers ?? new MediaMarkerCollection<AdMarker>();
            TimelineMarkers = TimelineMarkers ?? new MediaMarkerCollection<TimelineMediaMarker>();
            Chapters = Chapters ?? new MediaMarkerCollection<Chapter>();
            Captions = Captions ?? new MediaMarkerCollection<CaptionRegion>();
            VisibleCaptions = VisibleCaptions ?? new MediaMarkerCollection<CaptionRegion>();
        }

        private void InitializeTemplateChildren()
        {
            _videoDoubleClickMonitor.Element = MediaPresenterElement;
            AudioStreamSelectionElement.IfNotNull(i => i.SelectionChanged += AudioStreamSelectionElement_SelectionChanged);
            CaptionDisplayToggleElement.IfNotNull(i => i.Checked += CaptionDisplayToggleElement_CheckedChanged);
            CaptionDisplayToggleElement.IfNotNull(i => i.Unchecked += CaptionDisplayToggleElement_CheckedChanged);
            ChapterSelectionElement.IfNotNull(i => i.SelectionChanged += ChapterSelectionElement_SelectionChanged);
            ChaptersDisplayToggleElement.IfNotNull(i => i.Checked += ChaptersDisplayToggleElement_CheckedChanged);
            ChaptersDisplayToggleElement.IfNotNull(i => i.Unchecked += ChaptersDisplayToggleElement_CheckedChanged);
            ControlStripToggleElement.IfNotNull(i => i.Click += ControlStripToggleElement_Click);
            FullScreenToggleElement.IfNotNull(i => i.Checked += FullScreenToggleElement_CheckedChanged);
            FullScreenToggleElement.IfNotNull(i => i.Unchecked += FullScreenToggleElement_CheckedChanged);
            GraphToggleElement.IfNotNull(i => i.Checked += GraphToggleElement_CheckedChanged);
            GraphToggleElement.IfNotNull(i => i.Unchecked += GraphToggleElement_CheckedChanged);
            HideChaptersElement.IfNotNull(i => i.Click += HideChaptersElement_Click);
            HidePlaylistElement.IfNotNull(i => i.Click += HidePlaylistElement_Click);
            MaxBitrateLimiterElement.IfNotNull(i => i.RecommendMaximumBitrate += MaxBitrateLimiterElement_RecommendMaximumBitrate);
            MediaPresenterElement.IfNotNull(i => i.SizeChanged += MediaPresenterElement_SizeChanged);
            NextChapterElement.IfNotNull(i => i.Click += NextChapterElement_Click);
            NextPlaylistItemElement.IfNotNull(i => i.Click += NextPlaylistItemElement_Click);
            PlayElement.IfNotNull(i => i.Click += PlayElement_Click);
            PlaylistDisplayToggleElement.IfNotNull(i => i.Checked += PlaylistDisplayToggleElement_CheckedChanged);
            PlaylistDisplayToggleElement.IfNotNull(i => i.Unchecked += PlaylistDisplayToggleElement_CheckedChanged);
            PlaylistSelectionElement.IfNotNull(i => i.SelectionChanged += PlaylistSelectionElement_SelectionChanged);
            PreviousChapterElement.IfNotNull(i => i.Click += PreviousChapterElement_Click);
            PreviousPlaylistItemElement.IfNotNull(i => i.Click += PreviousPlaylistItemElement_Click);
            ReplayElement.IfNotNull(i => i.Click += ReplayElement_Click);
            SeekToLiveElement.IfNotNull(i => i.Click += SeekToLiveElement_Click);
            ShowChaptersElement.IfNotNull(i => i.Click += ShowChaptersElement_Click);
            ShowPlaylistElement.IfNotNull(i => i.Click += ShowPlaylistElement_Click);
            SlowMotionElement.IfNotNull(i => i.Checked += SlowMotionElement_CheckedChanged);
            SlowMotionElement.IfNotNull(i => i.Unchecked += SlowMotionElement_CheckedChanged);
            TimelineMarkerSelectionElement.IfNotNull(i => i.SelectionChanged += TimelineMarkerSelectionElement_SelectionChanged);
            VolumeElement.IfNotNull(i => i.VolumeLevelChanged += VolumeElement_VolumeLevelChanged);
            VolumeElement.IfNotNull(i => i.Muted += VolumeElement_Muted);
            VolumeElement.IfNotNull(i => i.UnMuted += VolumeElement_UnMuted);
            FastForwardElement.IfNotNull(i => i.Click += FastForwardElement_Click);
            FastForwardElement.IfNotNull(i => i.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(FastForwardElement_MouseLeftButtonDown), true));
            FastForwardElement.IfNotNull(i => i.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(FastForwardElement_MouseLeftButtonUp), true));
            RewindElement.IfNotNull(i => i.Click += RewindElement_Click);
            RewindElement.IfNotNull(i => i.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(RewindElement_MouseLeftButtonDown), true));
            RewindElement.IfNotNull(i => i.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(RewindElement_MouseLeftButtonUp), true));
            TimelineElement.IfNotNull(i => i.ScrubbingStarted += TimelineElement_ScrubbingStarted);
            TimelineElement.IfNotNull(i => i.Scrubbing += TimelineElement_Scrubbing);
            TimelineElement.IfNotNull(i => i.ScrubbingCompleted += TimelineElement_ScrubbingCompleted);
            TimelineElement.IfNotNull(i => i.MarkerSelected += TimelineElement_MarkerSelected);
            ButtonRetryElement.IfNotNull(i => i.Click += ButtonRetryElement_Click);
        }

        private void UninitializeTemplateChildren()
        {
            _videoDoubleClickMonitor.Element = null;
            AudioStreamSelectionElement.IfNotNull(i => i.SelectionChanged -= AudioStreamSelectionElement_SelectionChanged);
            CaptionDisplayToggleElement.IfNotNull(i => i.Checked -= CaptionDisplayToggleElement_CheckedChanged);
            CaptionDisplayToggleElement.IfNotNull(i => i.Unchecked -= CaptionDisplayToggleElement_CheckedChanged);
            ChapterSelectionElement.IfNotNull(i => i.SelectionChanged -= ChapterSelectionElement_SelectionChanged);
            ChaptersDisplayToggleElement.IfNotNull(i => i.Checked -= ChaptersDisplayToggleElement_CheckedChanged);
            ChaptersDisplayToggleElement.IfNotNull(i => i.Unchecked -= ChaptersDisplayToggleElement_CheckedChanged);
            ControlStripToggleElement.IfNotNull(i => i.Click -= ControlStripToggleElement_Click);
            FullScreenToggleElement.IfNotNull(i => i.Checked -= FullScreenToggleElement_CheckedChanged);
            FullScreenToggleElement.IfNotNull(i => i.Unchecked -= FullScreenToggleElement_CheckedChanged);
            GraphToggleElement.IfNotNull(i => i.Checked -= GraphToggleElement_CheckedChanged);
            GraphToggleElement.IfNotNull(i => i.Unchecked -= GraphToggleElement_CheckedChanged);
            HideChaptersElement.IfNotNull(i => i.Click -= HideChaptersElement_Click);
            HidePlaylistElement.IfNotNull(i => i.Click -= HidePlaylistElement_Click);
            MaxBitrateLimiterElement.IfNotNull(i => i.RecommendMaximumBitrate -= MaxBitrateLimiterElement_RecommendMaximumBitrate);
            MediaPresenterElement.IfNotNull(i => i.SizeChanged -= MediaPresenterElement_SizeChanged);
            NextChapterElement.IfNotNull(i => i.Click -= NextChapterElement_Click);
            NextPlaylistItemElement.IfNotNull(i => i.Click -= NextPlaylistItemElement_Click);
            PlayElement.IfNotNull(i => i.Click -= PlayElement_Click);
            PlaylistDisplayToggleElement.IfNotNull(i => i.Checked -= PlaylistDisplayToggleElement_CheckedChanged);
            PlaylistDisplayToggleElement.IfNotNull(i => i.Unchecked -= PlaylistDisplayToggleElement_CheckedChanged);
            PlaylistSelectionElement.IfNotNull(i => i.SelectionChanged -= PlaylistSelectionElement_SelectionChanged);
            PreviousChapterElement.IfNotNull(i => i.Click -= PreviousChapterElement_Click);
            PreviousPlaylistItemElement.IfNotNull(i => i.Click -= PreviousPlaylistItemElement_Click);
            ReplayElement.IfNotNull(i => i.Click -= ReplayElement_Click);
            SeekToLiveElement.IfNotNull(i => i.Click -= SeekToLiveElement_Click);
            ShowChaptersElement.IfNotNull(i => i.Click -= ShowChaptersElement_Click);
            ShowPlaylistElement.IfNotNull(i => i.Click -= ShowPlaylistElement_Click);
            SlowMotionElement.IfNotNull(i => i.Checked -= SlowMotionElement_CheckedChanged);
            SlowMotionElement.IfNotNull(i => i.Unchecked -= SlowMotionElement_CheckedChanged);
            TimelineMarkerSelectionElement.IfNotNull(i => i.SelectionChanged -= TimelineMarkerSelectionElement_SelectionChanged);
            VolumeElement.IfNotNull(i => i.VolumeLevelChanged -= VolumeElement_VolumeLevelChanged);
            VolumeElement.IfNotNull(i => i.Muted -= VolumeElement_Muted);
            VolumeElement.IfNotNull(i => i.UnMuted -= VolumeElement_UnMuted);
            FastForwardElement.IfNotNull(i => i.Click -= FastForwardElement_Click);
            FastForwardElement.IfNotNull(i => i.RemoveHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(FastForwardElement_MouseLeftButtonDown)));
            FastForwardElement.IfNotNull(i => i.RemoveHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(FastForwardElement_MouseLeftButtonUp)));
            RewindElement.IfNotNull(i => i.Click -= RewindElement_Click);
            RewindElement.IfNotNull(i => i.RemoveHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(RewindElement_MouseLeftButtonDown)));
            RewindElement.IfNotNull(i => i.RemoveHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(RewindElement_MouseLeftButtonUp)));
            TimelineElement.IfNotNull(i => i.ScrubbingStarted -= TimelineElement_ScrubbingStarted);
            TimelineElement.IfNotNull(i => i.Scrubbing -= TimelineElement_Scrubbing);
            TimelineElement.IfNotNull(i => i.ScrubbingCompleted -= TimelineElement_ScrubbingCompleted);
            TimelineElement.IfNotNull(i => i.MarkerSelected -= TimelineElement_MarkerSelected);
            ButtonRetryElement.IfNotNull(i => i.Click -= ButtonRetryElement_Click);
        }

        private void GetTemplateChildren()
        {
            AudioStreamSelectionElement = GetTemplateChild(SMFPlayerTemplateParts.AudioStreamSelectionElement) as Selector;
            BitrateGraphElement = GetTemplateChild(SMFPlayerTemplateParts.BitrateGraphElement) as BitrateGraphControl;
            CaptionDisplayToggleElement = GetTemplateChild(SMFPlayerTemplateParts.CaptionToggleElement) as ToggleButton;
            CaptionsPresenterElement = GetTemplateChild(SMFPlayerTemplateParts.CaptionsPresenterElement) as CaptionsPresenter;
            ChapterSelectionElement = GetTemplateChild(SMFPlayerTemplateParts.ChapterSelectionElement) as Selector;
            ChaptersDisplayToggleElement = GetTemplateChild(SMFPlayerTemplateParts.ChaptersToggleElement) as ToggleButton;
            ControlStripToggleElement = GetTemplateChild(SMFPlayerTemplateParts.ControlStripToggleElement) as ButtonBase;
            FastForwardElement = GetTemplateChild(SMFPlayerTemplateParts.FastForwardElement) as ButtonBase;
            FramerateGraphElement = GetTemplateChild(SMFPlayerTemplateParts.FramerateGraphElement) as FramerateGraphControl;
            FullScreenToggleElement = GetTemplateChild(SMFPlayerTemplateParts.FullScreenToggleElement) as ToggleButton;
            GraphToggleElement = GetTemplateChild(SMFPlayerTemplateParts.GraphToggleElement) as ToggleButton;
            HideChaptersElement = GetTemplateChild(SMFPlayerTemplateParts.HideChaptersElement) as ButtonBase;
            HidePlaylistElement = GetTemplateChild(SMFPlayerTemplateParts.HidePlaylistElement) as ButtonBase;
            LoggingDisplayElement = GetTemplateChild(SMFPlayerTemplateParts.LoggingDisplayElement) as TextBox;
            MaxBitrateLimiterElement = GetTemplateChild(SMFPlayerTemplateParts.MaxBitrateLimiterElement) as MaxBitrateLimiterControl;
            NextChapterElement = GetTemplateChild(SMFPlayerTemplateParts.NextChapterElement) as ButtonBase;
            NextPlaylistItemElement = GetTemplateChild(SMFPlayerTemplateParts.NextPlaylistItemElement) as ButtonBase;
            PlayElement = GetTemplateChild(SMFPlayerTemplateParts.PlayElement) as PlayElement;
            PlaylistSelectionElement = GetTemplateChild(SMFPlayerTemplateParts.PlaylistSelectionElement) as Selector;
            PlaylistDisplayToggleElement = GetTemplateChild(SMFPlayerTemplateParts.PlaylistToggleElement) as ToggleButton;
            PlayerRoot = GetTemplateChild(SMFPlayerTemplateParts.PlayerRoot) as Grid;
            PreviousChapterElement = GetTemplateChild(SMFPlayerTemplateParts.PreviousChapterElement) as ButtonBase;
            PreviousPlaylistItemElement = GetTemplateChild(SMFPlayerTemplateParts.PreviousPlaylistItemElement) as ButtonBase;
            ReplayElement = GetTemplateChild(SMFPlayerTemplateParts.ReplayElement) as ButtonBase;
            RewindElement = GetTemplateChild(SMFPlayerTemplateParts.RewindElement) as ButtonBase;
            SeekToLiveElement = GetTemplateChild(SMFPlayerTemplateParts.SeekToLiveElement) as ButtonBase;
            ShowChaptersElement = GetTemplateChild(SMFPlayerTemplateParts.ShowChaptersElement) as ButtonBase;
            ShowPlaylistElement = GetTemplateChild(SMFPlayerTemplateParts.ShowPlaylistElement) as ButtonBase;
            SlowMotionElement = GetTemplateChild(SMFPlayerTemplateParts.SlowMotionElement) as ToggleButton;
            TimelineElement = GetTemplateChild(SMFPlayerTemplateParts.TimelineElement) as Timeline;
            TimelineMarkerSelectionElement = GetTemplateChild(SMFPlayerTemplateParts.TimelineMarkerSelectionElement) as Selector;
            MediaPresenterElement = GetTemplateChild(SMFPlayerTemplateParts.MediaPresenterElement) as ContentControl;
            VideoAreaElement = GetTemplateChild(SMFPlayerTemplateParts.VideoAreaElement) as Panel;
            VersionInformationElement = GetTemplateChild(SMFPlayerTemplateParts.VersionInformationElement) as ContentControl;
            VolumeElement = GetTemplateChild(SMFPlayerTemplateParts.VolumeElement) as VolumeControl;
            ButtonRetryElement = GetTemplateChild(SMFPlayerTemplateParts.ButtonRetryElement) as Button;

            // Add all VideoArea children to Containers so they can be used to host ads.
            VideoAreaElement.IfNotNull(v => v.Children.OfType<FrameworkElement>().ForEach(i => Containers.Add(i)));
        }

        #endregion

        #region Dependency Property Definitions

        #region GlobalConfigMetadata
        /// <summary>
        /// GlobalConfigMetadata DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty GlobalConfigMetadataProperty =
            DependencyProperty.Register("GlobalConfigMetadata", typeof(MetadataCollection), typeof(SMFPlayer), null);

        /// <summary>
        /// Gets or sets the bitrate value for high definition video.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public MetadataCollection GlobalConfigMetadata
        {
            get { return (MetadataCollection)GetValue(GlobalConfigMetadataProperty); }
            set { SetValue(GlobalConfigMetadataProperty, value); }
        }

        #endregion

#if !WINDOWS_PHONE && !FULLSCREEN
        #region AllowSpaceBarToggle
        /// <summary>
        /// AllowSpaceBarToggle DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AllowSpaceBarToggleProperty =
            DependencyProperty.Register("AllowSpaceBarToggle", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the whether space and escape keys should toggle play/pause when not in fullscreen mode.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Int64TypeConverter))]
        public bool AllowSpaceBarToggle
        {
            get { return (bool)GetValue(AllowSpaceBarToggleProperty); }
            set { SetValue(AllowSpaceBarToggleProperty, value); }
        }
        #endregion
#else // WINDOWS_PHONE
        private bool AllowSpaceBarToggle 
        {
            get { return false; }
        }
#endif

#if !WINDOWS_PHONE && !FULLSCREEN
        #region AllowDoubleClickToggle
        /// <summary>
        /// AllowSpaceBarToggle DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AllowDoubleClickToggleProperty =
            DependencyProperty.Register("AllowDoubleClickToggle", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the whether space and escape keys should toggle play/pause when not in fullscreen mode.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Int64TypeConverter))]
        public bool AllowDoubleClickToggle
        {
            get { return (bool)GetValue(AllowDoubleClickToggleProperty); }
            set { SetValue(AllowDoubleClickToggleProperty, value); }
        }
        #endregion
#else // WINDOWS_PHONE
        private bool AllowDoubleClickToggle 
        {
            get { return false; }
        }
#endif

        #region HighDefinitionBitrate
        /// <summary>
        /// HighDefinitionBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty HighDefinitionBitrateProperty =
            DependencyProperty.Register("HighDefinitionBitrate", typeof(long), typeof(SMFPlayer), null);

        /// <summary>
        /// Gets or sets the bitrate value for high definition video.
        /// </summary>
        [Category("Bitrate Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Int64TypeConverter))]
        public long HighDefinitionBitrate
        {
            get { return (long)GetValue(HighDefinitionBitrateProperty); }
            set { SetValue(HighDefinitionBitrateProperty, value); }
        }

        #endregion

#if !WINDOWS_PHONE && !FULLSCREEN
        #region AllowFullScreenPinning
        /// <summary>
        /// AllowFullScreenPinning DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AllowFullScreenPinningProperty =
            DependencyProperty.Register("AllowFullScreenPinning", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether fullscreen pinning is allowed. This is a feature that will allow the user with multiple monitors to pin the video to one monitor while they work on the other.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool AllowFullScreenPinning
        {
            get { return (bool)GetValue(AllowFullScreenPinningProperty); }
            set { SetValue(AllowFullScreenPinningProperty, value); }
        }
        #endregion
#endif

        #region PositionLiveBuffer
        /// <summary>
        /// PositionLiveBuffer DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PositionLiveBufferProperty =
            DependencyProperty.Register("PositionLiveBuffer", typeof(TimeSpan), typeof(SMFPlayer), new PropertyMetadata(TimeSpan.FromMilliseconds(DefaultPositionLiveBufferMillis)));

        /// <summary>
        /// Gets or sets a value indicating what the tollerance is for determining whether or not the current position is live. IsPositionLive is affected by this property.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan PositionLiveBuffer
        {
            get { return (TimeSpan)GetValue(PositionLiveBufferProperty); }
            set { SetValue(PositionLiveBufferProperty, value); }
        }
        #endregion

        #region EstimatedLiveDuration
        /// <summary>
        /// EstimatedLiveDuration DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EstimatedLiveDurationProperty =
            DependencyProperty.Register("EstimatedLiveDuration", typeof(TimeSpan), typeof(SMFPlayer), new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets a value indicating what the estimated live duration is for live streams. This property affects the EndPosition property and causes the EstimatedLiveDurationExceeded event to fire when the position exceeds this position.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan EstimatedLiveDuration
        {
            get { return (TimeSpan)GetValue(EstimatedLiveDurationProperty); }
            set { SetValue(EstimatedLiveDurationProperty, value); }
        }
        #endregion

        #region OffsetStartPosition
        /// <summary>
        /// OffsetStartPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsStartPositionOffsetProperty =
            DependencyProperty.Register("IsStartPositionOffset", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the StartPosition is offset. If so, positions are then calculated relative to the StartPosition.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool IsStartPositionOffset
        {
            get { return (bool)GetValue(IsStartPositionOffsetProperty); }
            set { SetValue(IsStartPositionOffsetProperty, value); }
        }
        #endregion

        #region AvailableVideoBitrates
        /// <summary>
        /// AvailableVideoBitrates DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AvailableVideoBitratesProperty =
            DependencyProperty.Register("AvailableVideoBitrates", typeof(IEnumerable<long>), typeof(SMFPlayer),
                                        new PropertyMetadata(Enumerable.Empty<long>()));

        /// <summary>
        /// Gets a value indicating which video bitrates are available for the current playlistitem.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public IEnumerable<long> AvailableVideoBitrates
        {
            get { return (IEnumerable<long>)GetValue(AvailableVideoBitratesProperty); }
            protected set { SetValue(AvailableVideoBitratesProperty, value); }
        }
        #endregion

        #region PlayerGraphVisibility
        /// <summary>
        /// PlayerGraphVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlayerGraphVisibilityProperty =
            DependencyProperty.Register("PlayerGraphVisibility", typeof(FeatureVisibility), typeof(SMFPlayer), new PropertyMetadata(FeatureVisibility.Disabled, OnPlayerGraphVisibilityPropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the player graph feature should be displayed.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility PlayerGraphVisibility
        {
            get { return (FeatureVisibility)GetValue(PlayerGraphVisibilityProperty); }
            set { SetValue(PlayerGraphVisibilityProperty, value); }
        }

        private static void OnPlayerGraphVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlayerGraphVisibilityChanged());
        }

        private void OnPlayerGraphVisibilityChanged()
        {
            var state = PlayerGraphVisibility == FeatureVisibility.Visible
                            ? SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphVisible
                            : PlayerGraphVisibility == FeatureVisibility.Hidden
                                  ? SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphHidden
                                  : SMFPlayerVisualStates.PlayerGraphStates.PlayerGraphDisabled;

            if (PlayerGraphVisibility == FeatureVisibility.Visible)
            {
                BitrateGraphElement.IfNotNull(i => i.StartRecording());
                FramerateGraphElement.IfNotNull(i => i.StartRecording());
            }

            this.GoToVisualState(state);
        }

        #endregion

        #region AutoLoad
        /// <summary>
        /// AutoLoad DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AutoLoadProperty =
            DependencyProperty.Register("AutoLoad", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether a PlaylistItem should automatically be loaded. Setting this to False requires you to manually set CurrentPlaylistItem. Note: Loading a PlaylistItem will open the video and start buffering. However, AutoPlay is the property that controls whether or not playback should begin.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool AutoLoad
        {
            get { return (bool)GetValue(AutoLoadProperty); }
            set { SetValue(AutoLoadProperty, value); }
        }

        #endregion

        #region AutoPlay
        /// <summary>
        /// AutoPlay DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AutoPlayProperty =
            DependencyProperty.Register("AutoPlay", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether a playlist should start playing when loaded.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        #endregion

        #region IsControlStripVisible
        /// <summary>
        /// IsControlStripVisible DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsControlStripVisibleProperty =
            DependencyProperty.Register("IsControlStripVisible", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(true, OnIsControlStripVisiblePropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the control strip should be visible or not.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool IsControlStripVisible
        {
            get { return (bool)GetValue(IsControlStripVisibleProperty); }
            set { SetValue(IsControlStripVisibleProperty, value); }
        }

        private static void OnIsControlStripVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsControlStripVisibleChanged());
        }

        private void OnIsControlStripVisibleChanged(bool OnStartup = false)
        {
            var state = IsControlStripVisible
                            ? SMFPlayerVisualStates.ControlStripStates.ControlStripVisible
                            : SMFPlayerVisualStates.ControlStripStates.ControlStripNotVisible;
            this.GoToVisualState(state, !OnStartup);
        }

        #endregion

        #region AvailableAudioStreams
        /// <summary>
        /// AvailableAudioStreams DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AvailableAudioStreamsProperty =
            DependencyProperty.Register("AvailableAudioStreams", typeof(IEnumerable<StreamMetadata>),
                                        typeof(SMFPlayer),
                                        new PropertyMetadata(Enumerable.Empty<StreamMetadata>(),
                                                             OnAvailableAudioStreamsPropertyChanged));

        /// <summary>
        /// Gets the available audio streams for the media.
        /// </summary>
        [ScriptableMember]
        public IEnumerable<StreamMetadata> AvailableAudioStreams
        {
            get { return (IEnumerable<StreamMetadata>)GetValue(AvailableAudioStreamsProperty); }
            private set { SetValue(AvailableAudioStreamsProperty, value); }
        }

        private static void OnAvailableAudioStreamsPropertyChanged(DependencyObject d,
                                                                   DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnAvailableAudioStreamsChanged());
        }

        protected virtual void OnAvailableAudioStreamsChanged()
        {
            string state = AvailableAudioStreams != null && AvailableAudioStreams.Count() > 1
                               ? SMFPlayerVisualStates.AudioStreamMetadataAvailableStates.AudioStreamMetadataAvailable
                               : SMFPlayerVisualStates.AudioStreamMetadataAvailableStates.
                                     AudioStreamMetadataNotAvailable;
            this.GoToVisualState(state);

            AvailableAudioStreamsChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        #endregion

        #region AvailableCaptionStreams
        /// <summary>
        /// AvailableCaptionStreams DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AvailableCaptionStreamsProperty =
            DependencyProperty.Register("AvailableCaptionStreams", typeof(IEnumerable<StreamMetadata>),
                                        typeof(SMFPlayer), new PropertyMetadata(Enumerable.Empty<StreamMetadata>()));

        /// <summary>
        /// Gets the buffering progress for the currently playing Progressive media.
        /// </summary>
        [ScriptableMember]
        public IEnumerable<StreamMetadata> AvailableCaptionStreams
        {
            get { return (IEnumerable<StreamMetadata>)GetValue(AvailableCaptionStreamsProperty); }
            private set { SetValue(AvailableCaptionStreamsProperty, value); }
        }

        #endregion

        #region BufferingProgress
        /// <summary>
        /// BufferingProgress DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty BufferingProgressProperty =
            DependencyProperty.Register("BufferingProgress", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata((double)0, OnBufferingProgressPropertyChanged));

        /// <summary>
        /// Gets the buffering progress for the currently playing Progressive media.
        /// </summary>
        [ScriptableMember]
        public double BufferingProgress
        {
            get { return (double)GetValue(BufferingProgressProperty); }
            private set { SetValue(BufferingProgressProperty, value); }
        }

        private static void OnBufferingProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnBufferingProgressChanged());
        }

        private void OnBufferingProgressChanged()
        {
            BufferingProgressChanged.IfNotNull(i => i(this, new CustomEventArgs<double>(BufferingProgress)));
            if (!IsInDesignMode)
            {
                string logMessage = string.Format(
                    SilverlightMediaFrameworkResources.BufferingProgressChangedLogMessage, BufferingProgress);
                SendLogEntry(message: logMessage, type: KnownLogEntryTypes.BufferingProgressChanged,
                    extendedProperties: new Dictionary<string, object> { { "BufferingProgress", BufferingProgress } });
            }
        }

        #endregion

        #region BufferingTime
        /// <summary>
        /// BufferingTime DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty BufferingTimeProperty =
            DependencyProperty.Register("BufferingTime", typeof(TimeSpan?), typeof(SMFPlayer),
                                        new PropertyMetadata(new Nullable<TimeSpan>(), OnBufferingTimePropertyChanged));

        /// <summary>
        /// Gets or sets the buffering time in seconds for the media plugin.
        /// </summary>
        [Category("Player Configuration")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? BufferingTime
        {
            get { return (TimeSpan?)GetValue(BufferingTimeProperty); }
            set { SetValue(BufferingTimeProperty, value); }
        }

        private static void OnBufferingTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnBufferingTimeChanged());
        }

        private void OnBufferingTimeChanged()
        {
            if (BufferingTime.HasValue)
            {
                ActiveMediaPlugin.IfNotNull(i => i.BufferingTime = BufferingTime.Value);
            }
        }

        #endregion

        #region ChunkDownloadStrategy
        public static readonly DependencyProperty ChunkDownloadStrategyProperty =
            DependencyProperty.Register("ChunkDownloadStrategy", typeof(ChunkDownloadStrategy), typeof(SMFPlayer),
                                        new PropertyMetadata(ChunkDownloadStrategy.AsNeeded,
                                                             OnLoggingConsoleVisibilityPropertyChanged));

        [Category("Plug In Configuration")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public ChunkDownloadStrategy ChunkDownloadStrategy
        {
            get { return (ChunkDownloadStrategy)GetValue(ChunkDownloadStrategyProperty); }
            set { SetValue(ChunkDownloadStrategyProperty, value); }
        }

        private static void OnChunkDownloadStrategyPropertyChanged(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnChunkDownloadStrategyChanged());
        }

        private void OnChunkDownloadStrategyChanged()
        {
            if (CurrentPlaylistItem == null || CurrentPlaylistItem.ChunkDownloadStrategy == ChunkDownloadStrategy.Unspecified)
            {
                SetChunkDownloadStrategy(this.ChunkDownloadStrategy);
            }
        }

        #endregion

        #region ContinuousPlay
        /// <summary>
        /// ContinuousPlay DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ContinuousPlayProperty =
            DependencyProperty.Register("ContinuousPlay", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether the next PlaylistItem should automatically start when another PlaylistItem has finished.
        /// </summary>
        [ScriptableMember]
        public bool ContinuousPlay
        {
            get { return (bool)GetValue(ContinuousPlayProperty); }
            set { SetValue(ContinuousPlayProperty, value); }
        }

        #endregion

        #region DownloadProgress
        /// <summary>
        /// DownloadProgress DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DownloadProgressProperty =
            DependencyProperty.Register("DownloadProgress", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata((double)0, OnDownloadProgressPropertyChanged));

        /// <summary>
        /// Gets the buffering progress for the currently playing progressive media.
        /// </summary>
        [ScriptableMember]
        public double DownloadProgress
        {
            get { return (double)GetValue(DownloadProgressProperty); }
            private set { SetValue(DownloadProgressProperty, value); }
        }

        private static void OnDownloadProgressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnDownloadProgressChanged((double)e.NewValue));
        }

        private void OnDownloadProgressChanged(double value)
        {
            DownloadProgressChanged.IfNotNull(i => i(this, new CustomEventArgs<double>(value)));

            if (!IsInDesignMode)
            {
                string logMessage = string.Format(SilverlightMediaFrameworkResources.DownloadProgressChangedLogMessage,
                                                  DownloadProgress);
                SendLogEntry(message: logMessage, type: KnownLogEntryTypes.DownloadProgressChanged,
                    extendedProperties: new Dictionary<string, object> { { "DownloadProgress", DownloadProgress } });
            }
        }

        #endregion

        #region DownloadProgressOffset
        /// <summary>
        /// DownloadProgressOffset DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DownloadProgressOffsetProperty =
            DependencyProperty.Register("DownloadProgressOffset", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata((double)0));

        /// <summary>
        /// Gets the buffering progress for the currently playing progressive media.
        /// </summary>
        [ScriptableMember]
        public double DownloadProgressOffset
        {
            get { return (double)GetValue(DownloadProgressOffsetProperty); }
            private set { SetValue(DownloadProgressOffsetProperty, value); }
        }

        #endregion

        #region EnableCachedComposition
        /// <summary>
        /// EnableCachedComposition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EnableCachedCompositionProperty =
            DependencyProperty.Register("EnableCachedComposition", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether GPU accelerated cached composition should be used in the player.
        /// </summary>
        [ScriptableMember]
        public bool EnableCachedComposition
        {
            get { return (bool)GetValue(EnableCachedCompositionProperty); }
            set { SetValue(EnableCachedCompositionProperty, value); }
        }

        #endregion

        #region EndPosition
        /// <summary>
        /// EndPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register("EndPosition", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets the duration of the current playlist item.
        /// </summary>
        [ScriptableMember]
        public TimeSpan EndPosition
        {
            get { return (TimeSpan)GetValue(EndPositionProperty); }
            private set { SetValue(EndPositionProperty, value); }
        }

        #endregion

        #region FastForwardButtonBehavior
        /// <summary>
        /// FastForwardButtonBehavior DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty FastForwardButtonBehaviorProperty =
            DependencyProperty.Register("FastForwardButtonBehavior", typeof(ButtonInputType), typeof(SMFPlayer),
                                        new PropertyMetadata(ButtonInputType.ButtonHold));

        /// <summary>
        /// Gets or sets the behavior of the FastForward button when a user presses and holds the mouse down on the button.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public ButtonInputType FastForwardButtonBehavior
        {
            get { return (ButtonInputType)GetValue(FastForwardButtonBehaviorProperty); }
            set { SetValue(FastForwardButtonBehaviorProperty, value); }
        }

        #endregion

        #region HeuristicsPriority
        /// <summary>
        /// HeuristicsPriority DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty HeuristicsPriorityProperty =
            DependencyProperty.Register("HeuristicsPriority", typeof(int?), typeof(SMFPlayer), null);

        /// <summary>
        /// Configures the value used to prioritize multiple Player heuristics.
        /// </summary>
        [Category("Media Heuristics")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public int? HeuristicsPriority
        {
            get { return (int?)GetValue(HeuristicsPriorityProperty); }
            set { SetValue(HeuristicsPriorityProperty, value); }
        }

        #endregion

        #region EnableSync
        /// <summary>
        /// EnableSync DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty EnableSyncProperty =
            DependencyProperty.Register("EnableSync", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(false));

        /// <summary>
        /// Configures the value used to prioritize multiple Player heuristics.
        /// </summary>
        [Category("Media Heuristics")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool EnableSync
        {
            get { return (bool)GetValue(EnableSyncProperty); }
            set { SetValue(EnableSyncProperty, value); }
        }

        #endregion

        #region CaptionsVisibility
        /// <summary>
        /// CaptionsVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty CaptionsVisibilityProperty =
            DependencyProperty.Register("CaptionsVisibility", typeof(FeatureVisibility), typeof(SMFPlayer),
                                        new PropertyMetadata(FeatureVisibility.Hidden,
                                                             OnCaptionsVisibilityPropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating the visibility of captions.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility CaptionsVisibility
        {
            get { return (FeatureVisibility)GetValue(CaptionsVisibilityProperty); }
            set { SetValue(CaptionsVisibilityProperty, value); }
        }

        private static void OnCaptionsVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnCaptionsVisibilityChanged());
        }

        private void OnCaptionsVisibilityChanged()
        {
            _captionManager.IfNotNull(i => i.Reset());
            string state = CaptionsVisibility == FeatureVisibility.Visible
                               ? SMFPlayerVisualStates.CaptionsStates.CaptionsVisible
                               : CaptionsVisibility == FeatureVisibility.Disabled
                                     ? SMFPlayerVisualStates.CaptionsStates.CaptionsDisabled
                                     : SMFPlayerVisualStates.CaptionsStates.CaptionsHidden;
            this.GoToVisualState(state);

            CaptionsVisibilityChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        #endregion

        #region ChaptersVisibility
        /// <summary>
        /// ChaptersVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ChaptersVisibilityProperty =
            DependencyProperty.Register("ChaptersVisibility", typeof(FeatureVisibility), typeof(SMFPlayer),
                                        new PropertyMetadata(FeatureVisibility.Hidden,
                                                             OnChaptersVisibilityPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the Chapters control is visible.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility ChaptersVisibility
        {
            get { return (FeatureVisibility)GetValue(ChaptersVisibilityProperty); }
            set { SetValue(ChaptersVisibilityProperty, value); }
        }

        private static void OnChaptersVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnChaptersVisibilityChanged());
        }

        private void OnChaptersVisibilityChanged()
        {
            string state = ChaptersVisibility == FeatureVisibility.Visible
                               ? SMFPlayerVisualStates.ChapterStates.ChaptersVisible
                               : ChaptersVisibility == FeatureVisibility.Disabled
                                     ? SMFPlayerVisualStates.ChapterStates.ChaptersDisabled
                                     : SMFPlayerVisualStates.ChapterStates.ChaptersHidden;
            this.GoToVisualState(state);
        }

        #endregion

        #region LoggingConsoleVisibility
        /// <summary>
        /// LoggingConsoleVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LoggingConsoleVisibilityProperty =
            DependencyProperty.Register("LoggingConsoleVisibility", typeof(FeatureVisibility), typeof(SMFPlayer),
                                        new PropertyMetadata(FeatureVisibility.Disabled,
                                                             OnLoggingConsoleVisibilityPropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the Player is displaying the Log Window.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility LoggingConsoleVisibility
        {
            get { return (FeatureVisibility)GetValue(LoggingConsoleVisibilityProperty); }
            set { SetValue(LoggingConsoleVisibilityProperty, value); }
        }

        private static void OnLoggingConsoleVisibilityPropertyChanged(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnLoggingConsoleVisibilityChanged());
        }

        private void OnLoggingConsoleVisibilityChanged()
        {
            string state = LoggingConsoleVisibility == FeatureVisibility.Visible
                               ? SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleVisible
                               : LoggingConsoleVisibility == FeatureVisibility.Disabled
                                     ? SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleDisabled
                                     : SMFPlayerVisualStates.LoggingConsoleStates.LoggingConsoleHidden;
            this.GoToVisualState(state);
        }

        #endregion

#if !WINDOWS_PHONE
        /// <summary>
        /// Gets a value indicating whether the media is being decoded by the GPU.
        /// </summary>
        [ScriptableMember]
        public bool IsDecodingOnGPU
        {
            get { return ActiveMediaPlugin.IsDecodingOnGPU; }
        }
#endif

        #region IsFullScreen
#if !WINDOWS_PHONE
        /// <summary>
        /// IsFullScreen DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsFullScreenProperty =
            DependencyProperty.Register("IsFullScreen", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsFullScreenPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the Player is in FullScreen mode.
        /// </summary>
        [ScriptableMember]
        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        private static void OnIsFullScreenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsFullScreenChanged());
        }

        private void OnIsFullScreenChanged()
        {
            Application.Current.Host.Content.FullScreenOptions = AllowFullScreenPinning
                                                                     ? FullScreenOptions.StaysFullScreenWhenUnfocused
                                                                     : FullScreenOptions.None;
            Application.Current.Host.Content.IsFullScreen = IsFullScreen;

            string state = IsFullScreen
                               ? SMFPlayerVisualStates.FullScreenStates.FullScreen
                               : SMFPlayerVisualStates.FullScreenStates.NotFullScreen;
            this.GoToVisualState(state);

            ConfigureVideoSize(IsFullScreen);
            FullScreenChanged.IfNotNull(i => i(this, EventArgs.Empty));
            SendLogEntry(KnownLogEntryTypes.FullScreenChanged,
                extendedProperties: new Dictionary<string, object> { { "IsFullScreen", IsFullScreen } });
        }
#else // WINDOWS_PHONE
        private bool IsFullScreen
        {
            get { return false; }
            set { }
        }
#endif
        #endregion

        #region IsMediaAdaptive
        /// <summary>
        /// IsMediaAdaptive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsMediaAdaptiveProperty =
            DependencyProperty.Register("IsMediaAdaptive", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsMediaAdaptivePropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the current playing media is adaptive media.
        /// </summary>
        [ScriptableMember]
        public bool IsMediaAdaptive
        {
            get { return (bool)GetValue(IsMediaAdaptiveProperty); }
            private set { SetValue(IsMediaAdaptiveProperty, value); }
        }

        private static void OnIsMediaAdaptivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsMediaAdaptiveChanged());
        }

        private void OnIsMediaAdaptiveChanged()
        {
            string state = IsMediaAdaptive
                               ? SMFPlayerVisualStates.MediaAdaptiveStates.MediaAdaptive
                               : SMFPlayerVisualStates.MediaAdaptiveStates.MediaNotAdaptive;
            this.GoToVisualState(state);
        }

        #endregion

        #region IsMediaLive
        /// <summary>
        /// IsMediaLive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsMediaLiveProperty =
            DependencyProperty.Register("IsMediaLive", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsMediaLivePropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the current media is live.
        /// </summary>
        [ScriptableMember]
        public bool IsMediaLive
        {
            get { return (bool)GetValue(IsMediaLiveProperty); }
            private set { SetValue(IsMediaLiveProperty, value); }
        }

        private static void OnIsMediaLivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsMediaLiveChanged());
        }

        private void OnIsMediaLiveChanged()
        {
            string state = IsMediaLive
                               ? SMFPlayerVisualStates.MediaLiveStates.MediaLive
                               : SMFPlayerVisualStates.MediaLiveStates.MediaNotLive;
            this.GoToVisualState(state);
        }

        #endregion

        #region PlaylistVisibility
        /// <summary>
        /// PlaylistVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaylistVisibilityProperty =
            DependencyProperty.Register("PlaylistVisibility", typeof(FeatureVisibility), typeof(SMFPlayer),
                                        new PropertyMetadata(FeatureVisibility.Hidden,
                                                             OnPlaylistVisibilityPropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the Playlist is visible.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility PlaylistVisibility
        {
            get { return (FeatureVisibility)GetValue(PlaylistVisibilityProperty); }
            set { SetValue(PlaylistVisibilityProperty, value); }
        }

        private static void OnPlaylistVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlaylistVisibilityChanged());
        }

        private void OnPlaylistVisibilityChanged()
        {
            string state = PlaylistVisibility == FeatureVisibility.Visible
                               ? SMFPlayerVisualStates.PlaylistStates.PlaylistVisible
                               : PlaylistVisibility == FeatureVisibility.Disabled
                                     ? SMFPlayerVisualStates.PlaylistStates.PlaylistDisabled
                                     : SMFPlayerVisualStates.PlaylistStates.PlaylistHidden;
            this.GoToVisualState(state);
        }

        #endregion

        #region IsPositionLive
        /// <summary>
        /// IsPositionLive DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsPositionLiveProperty =
            DependencyProperty.Register("IsPositionLive", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsPositionLivePropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the current playback position for live media is the live position.
        /// </summary>
        [ScriptableMember]
        public bool IsPositionLive
        {
            get { return (bool)GetValue(IsPositionLiveProperty); }
            private set { SetValue(IsPositionLiveProperty, value); }
        }

        private static void OnIsPositionLivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsPositionLiveChanged());
        }

        protected virtual void OnIsPositionLiveChanged()
        {
            string state = IsPositionLive
                               ? SMFPlayerVisualStates.PositionLiveStates.PositionLive
                               : SMFPlayerVisualStates.PositionLiveStates.PositionNotLive;
            this.GoToVisualState(state);
        }

        #endregion

        #region IsPosterVisible
        /// <summary>
        /// IsPosterVisible DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsPosterVisibleProperty =
            DependencyProperty.Register("IsPosterVisible", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsPosterVisiblePropertyChanged));

        /// <summary>
        /// Gets a value indicating whether a Poster is displayed.
        /// </summary>
        [ScriptableMember]
        public bool IsPosterVisible
        {
            get { return (bool)GetValue(IsPosterVisibleProperty); }
            private set { SetValue(IsPosterVisibleProperty, value); }
        }

        private static void OnIsPosterVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsPosterVisibleChanged());
        }

        private void OnIsPosterVisibleChanged()
        {
            string state = IsPosterVisible
                               ? SMFPlayerVisualStates.PosterStates.PosterVisible
                               : SMFPlayerVisualStates.PosterStates.PosterHidden;
            this.GoToVisualState(state);
        }

        #endregion

        #region IsAdvertising
        /// <summary>
        /// IsAdvertisingProperty DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsAdvertisingProperty =
            DependencyProperty.Register("IsAdvertising", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsAdvertisingPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether an ad is displayed.
        /// </summary>
        [ScriptableMember]
        public bool IsAdvertising
        {
            get { return (bool)GetValue(IsAdvertisingProperty); }
            set { SetValue(IsAdvertisingProperty, value); }
        }

        private static void OnIsAdvertisingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsAdvertisingChanged((bool)e.NewValue));
        }

        private void OnIsAdvertisingChanged(bool value)
        {
            this.GoToVisualState(value ? 
                SMFPlayerVisualStates.AdStates.AdVisible:
                SMFPlayerVisualStates.AdStates.AdHidden);
        }

        #endregion

        #region RetryState
        /// <summary>
        /// IsRetrying DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty RetryStateProperty =
            DependencyProperty.Register("RetryState", typeof(RetryStateEnum), typeof(SMFPlayer),
                                        new PropertyMetadata(RetryStateEnum.NotRetrying, OnRetryStatePropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the Player is in retrying mode.
        /// </summary>
        [ScriptableMember]
        public RetryStateEnum RetryState
        {
            get { return (RetryStateEnum)GetValue(RetryStateProperty); }
            private set { SetValue(RetryStateProperty, value); }
        }

        private static void OnRetryStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnRetryStatePropertyChanged());
        }

        private void OnRetryStatePropertyChanged()
        {
            this.GoToVisualState(RetryState.ToString());
            RetryStateChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        /// <summary>
        /// Indicates that the RetryState property has changed.
        /// </summary>
        public event EventHandler RetryStateChanged;

        public enum RetryStateEnum
        {
            NotRetrying,
            Retrying,
            RetryFailed
        }

        #endregion

        #region IsScrubbing
        /// <summary>
        /// IsScrubbing DependencyProperty definition.
        /// </summary>
        public static DependencyProperty IsScrubbingProperty =
            DependencyProperty.Register("IsScrubbing", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsScrubbingPropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating whether the timeline is currently seeking to a new position.
        /// </summary>
        /// <remarks>
        /// When IsScrubbing is set to true the PlaybackPosition is not updated based on the current position of the underlying media element. 
        /// This allows PlaybackPosition to be set via two-way data binding. 
        /// When IsScrubbing is false, PlaybackPosition is updated using a timer interval to reflect the current media position.
        /// </remarks>
        [ScriptableMember]
        public bool IsScrubbing
        {
            get { return (bool)GetValue(IsScrubbingProperty); }
            private set { SetValue(IsScrubbingProperty, value); }
        }

        private static void OnIsScrubbingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsScrubbingChanged());
        }

        private void OnIsScrubbingChanged()
        {
            if (!IsInDesignMode && !IsScrubbing)
            {
                //SeekToPosition(PlaybackPosition);
            }
        }

        #endregion

        #region IsSlowMotionEnabled
        /// <summary>
        /// IsSlowMotionEnabled DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsSlowMotionEnabledProperty =
            DependencyProperty.Register("IsSlowMotionEnabled", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsSlowMotionEnabledPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the Player is in SlowMotion mode.
        /// </summary>
        [ScriptableMember]
        public bool IsSlowMotionEnabled
        {
            get { return (bool)GetValue(IsSlowMotionEnabledProperty); }
            private set { SetValue(IsSlowMotionEnabledProperty, value); }
        }

        private static void OnIsSlowMotionEnabledPropertyChanged(DependencyObject d,
                                                                 DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsSlowMotionEnabledChanged());
        }

        private void OnIsSlowMotionEnabledChanged()
        {
            string state = IsSlowMotionEnabled
                               ? SMFPlayerVisualStates.SlowMotionEnabledStates.SlowMotionEnabled
                               : SMFPlayerVisualStates.SlowMotionEnabledStates.SlowMotionDisabled;
            this.GoToVisualState(state);
        }

        #endregion

        #region LivePosition
        /// <summary>
        /// LivePosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LivePositionProperty =
            DependencyProperty.Register("LivePosition", typeof(TimeSpan?), typeof(SMFPlayer),
                                        new PropertyMetadata(null, OnLivePositionPropertyChanged));

        /// <summary>
        /// Gets the position of live playback.
        /// </summary>
        /// <remarks>
        /// This is an optional property. The value is null if the media is not live media.
        /// </remarks>
        [ScriptableMember]
        public TimeSpan? LivePosition
        {
            get { return (TimeSpan?)GetValue(LivePositionProperty); }
            private set { SetValue(LivePositionProperty, value); }
        }

        private static void OnLivePositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnLivePositionChanged());
        }

        private void OnLivePositionChanged()
        {
            LivePositionText = LivePosition.HasValue
                                   ? LivePosition.Value.Format()
                                   : string.Empty;
        }

        #endregion

        #region LivePositionText
        /// <summary>
        /// LivePositionText DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LivePositionTextProperty =
            DependencyProperty.Register("LivePositionText", typeof(string), typeof(SMFPlayer),
                                        new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets the current live position as text.
        /// </summary>
        [ScriptableMember]
        public string LivePositionText
        {
            get { return (string)GetValue(LivePositionTextProperty); }
            private set { SetValue(LivePositionTextProperty, value); }
        }

        #endregion

        #region LogLevel
        /// <summary>
        /// LogLevel DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LogLevelProperty =
            DependencyProperty.Register("LogLevel", typeof(LogLevel), typeof(SMFPlayer),
                                        new PropertyMetadata(LogLevel.None));

        /// <summary>
        /// Gets or sets the level for logging.
        /// </summary>
        /// <remarks>
        /// <para>The LogLevel allows you to filter log messages that are written to the LogWriters.
        /// For example, you might set the LogLevel to LogLevel.Information in development, 
        /// but use LogLevel.Errors in production to limit the number of log statements being generated in the Player
        /// and plug-ins for performance reasons.
        /// </para>
        /// <para>
        /// The LogLevel also determines the severity of log messages displayed in the Log Window when Ctrl-Alt-L is pressed when the Player is displayed.
        /// </para>
        /// </remarks>
        [Category("Player Logging")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public LogLevel LogLevel
        {
            get { return (LogLevel)GetValue(LogLevelProperty); }
            set { SetValue(LogLevelProperty, value); }
        }

        #endregion

        #region LogWriters
        /// <summary>
        /// LogWriters DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LogWritersProperty =
            DependencyProperty.Register("LogWriters", typeof(string), typeof(SMFPlayer),
                                        new PropertyMetadata(OnLogWritersPropertyChanged));

        /// <summary>
        /// Gets  or sets the current set of LogWriters to write logging information to.
        /// </summary>
        /// <remarks>
        /// The value of this property is a comma-separated list of LogWriters.
        /// The LogLevel determines the severity of log messages in order for them to be sent to the LogWriters.
        /// </remarks>
        [Category("Player Logging")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public string LogWriters
        {
            get { return (string)GetValue(LogWritersProperty); }
            set { SetValue(LogWritersProperty, value); }
        }

        private static void OnLogWritersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnLogWritersChanged());
        }

        private void OnLogWritersChanged()
        {
            if (!IsInDesignMode)
            {
                IList<string> logWriterIds = LogWriters.ToDelimitedList();
                LoadLoggingPlugins(logWriterIds);
            }
        }

        #endregion

        #region TimelineMarkers
        /// <summary>
        /// TimelineMarkers DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty TimelineMarkersProperty =
            DependencyProperty.Register("TimelineMarkers", typeof(MediaMarkerCollection<TimelineMediaMarker>),
                                        typeof(SMFPlayer), new PropertyMetadata(OnTimelineMarkersPropertyChanged));

        /// <summary>
        /// Gets the collection of markers for the current Playlist item.
        /// </summary>
        //[ScriptableMember]
        public MediaMarkerCollection<TimelineMediaMarker> TimelineMarkers
        {
            get { return (MediaMarkerCollection<TimelineMediaMarker>)GetValue(TimelineMarkersProperty); }
            set { SetValue(TimelineMarkersProperty, value); }
        }

        private static void OnTimelineMarkersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var player = d as SMFPlayer;
            if (player != null)
            {
                if (args.NewValue != null)
                {
                    var oldTimelineMarkers = args.OldValue as MediaMarkerCollection<TimelineMediaMarker>;
                    var newTimelineMarkers = args.NewValue as MediaMarkerCollection<TimelineMediaMarker>;
                    player.OnTimelineMarkersChanged(oldTimelineMarkers, newTimelineMarkers);
                }
                else
                {
                    var oldValue = args.OldValue as MediaMarkerCollection<TimelineMediaMarker>;
                    player.TimelineMarkers = oldValue ?? new MediaMarkerCollection<TimelineMediaMarker>();
                }
            }
        }

        private void OnTimelineMarkersChanged(MediaMarkerCollection<TimelineMediaMarker> oldTimelineMarkers, MediaMarkerCollection<TimelineMediaMarker> newTimelineMarkers)
        {
            oldTimelineMarkers.IfNotNull(i => i.CollectionChanged -= TimelineMarkers_CollectionChanged);
            newTimelineMarkers.IfNotNull(i => i.CollectionChanged += TimelineMarkers_CollectionChanged);

            TimelineMarkersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void TimelineMarkers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TimelineMarkersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void UpdateTimeline()
        {
            if (TimelineElement != null)
            {
                TimelineElement.PlaybackPosition = PlaybackPosition;
                TimelineElement.UpdateTimeline();
            }
        }

        #endregion

        #region AdMarkers
        /// <summary>
        /// AdMarkers DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AdMarkersProperty =
            DependencyProperty.Register("AdMarkers", typeof(MediaMarkerCollection<AdMarker>),
                                        typeof(SMFPlayer), new PropertyMetadata(OnAdMarkersPropertyChanged));

        /// <summary>
        /// Gets the collection of ad markers for the current Playlist item.
        /// </summary>
        //[ScriptableMember]
        public MediaMarkerCollection<AdMarker> AdMarkers
        {
            get { return (MediaMarkerCollection<AdMarker>)GetValue(AdMarkersProperty); }
            set { SetValue(AdMarkersProperty, value); }
        }

        private static void OnAdMarkersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var player = d as SMFPlayer;
            if (player != null)
            {
                if (args.NewValue != null)
                {
                    var oldAdMarkers = args.OldValue as MediaMarkerCollection<AdMarker>;
                    var newAdMarkers = args.NewValue as MediaMarkerCollection<AdMarker>;
                    player.OnAdMarkersChanged(oldAdMarkers, newAdMarkers);
                }
                else
                {
                    var oldValue = args.OldValue as MediaMarkerCollection<AdMarker>;
                    player.AdMarkers = oldValue ?? new MediaMarkerCollection<AdMarker>();
                }
            }
        }

        private void OnAdMarkersChanged(MediaMarkerCollection<AdMarker> oldAdMarkers, MediaMarkerCollection<AdMarker> newAdMarkers)
        {
            oldAdMarkers.IfNotNull(i => i.CollectionChanged -= AdMarkers_CollectionChanged);
            newAdMarkers.IfNotNull(i => i.CollectionChanged += AdMarkers_CollectionChanged);

            AdMarkersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void AdMarkers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AdMarkersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        #endregion

        #region Chapters
        /// <summary>
        /// Chapters DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ChaptersProperty =
            DependencyProperty.Register("Chapters", typeof(MediaMarkerCollection<Chapter>), typeof(SMFPlayer),
                                        new PropertyMetadata(OnChaptersPropertyChanged));

        /// <summary>
        /// Gets the current caption markers.
        /// </summary>
        [ScriptableMember]
        public MediaMarkerCollection<Chapter> Chapters
        {
            get { return (MediaMarkerCollection<Chapter>)GetValue(ChaptersProperty); }
            set { SetValue(ChaptersProperty, value); }
        }

        private static void OnChaptersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var player = d as SMFPlayer;
            if (player != null)
            {
                if (args.NewValue != null)
                {
                    var oldChapters = args.OldValue as MediaMarkerCollection<Chapter>;
                    var newChapters = args.NewValue as MediaMarkerCollection<Chapter>;
                    player.OnChaptersChanged(oldChapters, newChapters);
                }
                else
                {
                    var oldValue = args.OldValue as MediaMarkerCollection<Chapter>;
                    player.Chapters = oldValue ?? new MediaMarkerCollection<Chapter>();
                }
            }
        }

        private void OnChaptersChanged(MediaMarkerCollection<Chapter> oldChapters, MediaMarkerCollection<Chapter> newChapters)
        {
            oldChapters.IfNotNull(i => i.CollectionChanged -= Chapters_CollectionChanged);
            newChapters.IfNotNull(i => i.CollectionChanged += Chapters_CollectionChanged);

            ChaptersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void Chapters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ChaptersChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void UpdateChapterSelection(Chapter chapterItem)
        {
            ChapterSelectionElement.IfNotNull(i => i.SelectedItem = chapterItem);
        }

        #endregion

        #region Captions
        /// <summary>
        /// Captions DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty CaptionsProperty =
            DependencyProperty.Register("Captions", typeof(MediaMarkerCollection<CaptionRegion>),
                                        typeof(SMFPlayer), new PropertyMetadata(OnCaptionsPropertyChanged));

        /// <summary>
        /// Gets the current caption markers.
        /// </summary>
        [ScriptableMember]
        public MediaMarkerCollection<CaptionRegion> Captions
        {
            get { return (MediaMarkerCollection<CaptionRegion>)GetValue(CaptionsProperty); }
            set { SetValue(CaptionsProperty, value); }
        }

        private static void OnCaptionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var player = d as SMFPlayer;
            var oldValue = args.OldValue as MediaMarkerCollection<CaptionRegion>;
            var newValue = args.NewValue as MediaMarkerCollection<CaptionRegion>;

            if (player != null)
            {
                if (newValue != null)
                {
                    player.OnCaptionsChanged(oldValue, newValue);
                }
                else
                {
                    player.Captions = oldValue ?? new MediaMarkerCollection<CaptionRegion>();
                }
            }
        }

        private void OnCaptionsChanged(MediaMarkerCollection<CaptionRegion> oldValue, MediaMarkerCollection<CaptionRegion> newValue)
        {
            if (newValue != null)
            {
                newValue.CollectionChanged += Captions_CollectionChanged;
                newValue.MarkerPositionChanged += CaptionPositionChanged;
            }

            if (oldValue != null)
            {
                oldValue.CollectionChanged -= Captions_CollectionChanged;
                oldValue.MarkerPositionChanged -= CaptionPositionChanged;
            }
        }

        private void Captions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ActiveMediaPlugin != null && _captionManager != null)
            {
                if (e.NewItems != null)
                {
                    e.NewItems.Cast<CaptionRegion>().ForEach(i => _captionManager.CheckMarkerPosition(RelativeMediaPluginPosition, i));
                }

                if (e.OldItems != null)
                {
                    e.OldItems.Cast<CaptionRegion>().ForEach(i => _captionManager.CheckMarkerPosition(RelativeMediaPluginPosition, i));
                }
            }
        }

        private void CaptionPositionChanged(CaptionRegion captionRegion)
        {
            if (ActiveMediaPlugin != null && _captionManager != null)
            {
                _captionManager.CheckMarkerPosition(RelativeMediaPluginPosition, captionRegion);
            }
        }

        #endregion

        #region VisibleCaptions
        /// <summary>
        /// VisibleCaptions DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VisibleCaptionsProperty =
            DependencyProperty.Register("VisibleCaptions", typeof(MediaMarkerCollection<CaptionRegion>),
                                        typeof(SMFPlayer), null);

        /// <summary>
        /// Gets the current caption markers.
        /// </summary>
        [ScriptableMember]
        public MediaMarkerCollection<CaptionRegion> VisibleCaptions
        {
            get { return (MediaMarkerCollection<CaptionRegion>)GetValue(VisibleCaptionsProperty); }
            private set { SetValue(VisibleCaptionsProperty, value); }
        }

        #endregion

        #region MaximumPlaybackBitrate
        /// <summary>
        /// MaximumPlaybackBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MaximumPlaybackBitrateProperty =
            DependencyProperty.Register("MaximumPlaybackBitrate", typeof(long), typeof(SMFPlayer),
                                        new PropertyMetadata((long)0, OnMaximumPlaybackBitratePropertyChanged));

        /// <summary>
        /// Gets the maximum bitrate for the currently playing adaptive media.
        /// </summary>
        /// <remarks>
        /// This value is only meaningful for adaptive media.
        /// </remarks>
        [ScriptableMember]
        public long MaximumPlaybackBitrate
        {
            get { return (long)GetValue(MaximumPlaybackBitrateProperty); }
            private set { SetValue(MaximumPlaybackBitrateProperty, value); }
        }

        private static void OnMaximumPlaybackBitratePropertyChanged(DependencyObject d,
                                                                    DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnMaximumPlaybackBitrateChanged());
        }

        private void OnMaximumPlaybackBitrateChanged()
        {
            if (!IsInDesignMode)
            {
                string logMessage = string.Format(SilverlightMediaFrameworkResources.MaximumBitrateChangedLogMessage,
                                                  MaximumPlaybackBitrate);
                SendLogEntry(message: logMessage, type: KnownLogEntryTypes.MaximumPlaybackBitrateChanged,
                    extendedProperties: new Dictionary<string, object> { { "Bitrate", MaximumPlaybackBitrate } });
            }
        }

        #endregion

        #region MediaTitle
        /// <summary>
        /// MediaTitle DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MediaTitleProperty =
            DependencyProperty.Register("MediaTitle", typeof(string), typeof(SMFPlayer), null);

        /// <summary>
        /// Gets or sets the title for the media.
        /// </summary>
        [ScriptableMember]
        public object MediaTitleContent
        {
            get { return GetValue(MediaTitleProperty); }
            set { SetValue(MediaTitleProperty, value); }
        }

        #endregion

        #region PlaybackBitrate
        /// <summary>
        /// PlaybackBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaybackBitrateProperty =
            DependencyProperty.Register("PlaybackBitrate", typeof(long), typeof(SMFPlayer),
                                        new PropertyMetadata((long)0));

        /// <summary>
        /// Gets the actual bitrate for the currently playing adaptive media.
        /// </summary>
        [ScriptableMember]
        public long PlaybackBitrate
        {
            get { return (long)GetValue(PlaybackBitrateProperty); }
            private set { SetValue(PlaybackBitrateProperty, value); }
        }

        #endregion

        #region DownloadBitrate
        /// <summary>
        /// DownloadBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DownloadBitrateProperty =
            DependencyProperty.Register("DownloadBitrate", typeof(long), typeof(SMFPlayer),
                                        new PropertyMetadata((long)0));

        /// <summary>
        /// Gets the actual bitrate for the currently playing adaptive media.
        /// </summary>
        [ScriptableMember]
        public long DownloadBitrate
        {
            get { return (long)GetValue(DownloadBitrateProperty); }
            private set { SetValue(DownloadBitrateProperty, value); }
        }

        #endregion

        #region DroppedFramesPerSecond
        /// <summary>
        /// DroppedFramesPerSecond DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DroppedFramesPerSecondProperty =
            DependencyProperty.Register("DroppedFramesPerSecond", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata((double)0));

        /// <summary>
        /// Gets the actual bitrate for the currently playing adaptive media.
        /// </summary>
        [ScriptableMember]
        public double DroppedFramesPerSecond
        {
            get { return (double)GetValue(DroppedFramesPerSecondProperty); }
            private set { SetValue(DroppedFramesPerSecondProperty, value); }
        }

        #endregion

        #region RenderedFramesPerSecond
        /// <summary>
        /// RenderedFramesPerSecond DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty RenderedFramesPerSecondProperty =
            DependencyProperty.Register("RenderedFramesPerSecond", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata((double)0));

        /// <summary>
        /// Gets the actual bitrate for the currently playing adaptive media.
        /// </summary>
        [ScriptableMember]
        public double RenderedFramesPerSecond
        {
            get { return (double)GetValue(RenderedFramesPerSecondProperty); }
            private set { SetValue(RenderedFramesPerSecondProperty, value); }
        }

        #endregion

        #region PlaybackPosition
        /// <summary>
        /// PlaybackPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaybackPositionProperty =
            DependencyProperty.Register("PlaybackPosition", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.Zero, OnPlaybackPositionPropertyChanged));

        /// <summary>
        /// Gets the current playback position.
        /// </summary>
        [ScriptableMember]
        public TimeSpan PlaybackPosition
        {
            get { return (TimeSpan)GetValue(PlaybackPositionProperty); }
            private set { SetValue(PlaybackPositionProperty, value); }
        }

        private static void OnPlaybackPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlaybackPositionChanged());
        }

        protected virtual void OnPlaybackPositionChanged()
        {
            if (!IsInDesignMode && !IsScrubbing && PlayState == MediaPluginState.Playing)
            {
                _retryMonitor.LastPosition = CalculateAbsoluteMediaPosition(PlaybackPosition);
            }

			PlaybackPositionChanged.IfNotNull(i => i(this, new CustomEventArgs<TimeSpan>(PlaybackPosition)));
        }

        #endregion

        #region PlaybackPositionOverride
        /// <summary>
        /// PlaybackPositionOverride DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaybackPositionDatabindingProperty =
            DependencyProperty.Register("PlaybackPositionDatabinding", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.Zero, OnPlaybackPositionDatabindingPropertyChanged));

        /// <summary>
        /// Gets or sets a time position that the playback position can be set to.
        /// </summary>
        /// <remarks>
        /// This property can be used to force media playback to seek to a specific time value in the current media.
        /// </remarks>
        [ScriptableMember]
        public TimeSpan PlaybackPositionDatabinding
        {
            get { return (TimeSpan)GetValue(PlaybackPositionDatabindingProperty); }
            set { SetValue(PlaybackPositionDatabindingProperty, value); }
        }

        private static void OnPlaybackPositionDatabindingPropertyChanged(DependencyObject d,
                                                                         DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlaybackPositionDatabindingChanged());
        }

        private void OnPlaybackPositionDatabindingChanged()
        {
            PlaybackPosition = PlaybackPositionDatabinding;
            if (!IsInDesignMode)
            {
                SeekToPosition(PlaybackPositionDatabinding);
            }
        }

        #endregion

        #region PlayerId
        /// <summary>
        /// PlayerId DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlayerIdProperty =
            DependencyProperty.Register("PlayerId", typeof(string), typeof(SMFPlayer),
                                        new PropertyMetadata(String.Empty));

        /// <summary>
        /// Gets a persisted ID for the player. ID is stored in isolated storage.
        /// </summary>
        public string PlayerId
        {
            get { return (string)GetValue(PlayerIdProperty); }
            private set { SetValue(PlayerIdProperty, value); }
        }

        #endregion

        #region Playlist
        /// <summary>
        /// Playlist DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaylistProperty =
            DependencyProperty.Register("Playlist", typeof(ObservableCollection<PlaylistItem>), typeof(SMFPlayer),
                                        new PropertyMetadata(new PropertyChangedCallback(OnPlaylistPropertyChanged)));

        /// <summary>
        /// Gets a list of media items to play.
        /// </summary>
        [Category("Common Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public ObservableCollection<PlaylistItem> Playlist
        {
            get { return (ObservableCollection<PlaylistItem>)GetValue(PlaylistProperty); }
            set { SetValue(PlaylistProperty, value); }
        }

        private static void OnPlaylistPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;

            if (player != null)
            {
                player.OnPlaylistChanged(e.OldValue as ObservableCollection<PlaylistItem>, e.NewValue as ObservableCollection<PlaylistItem>);

                if (player._isTemplateApplied)
                {
                    player.OnPlaylistChanged();
                }
            }
        }

        private void OnPlaylistChanged(ObservableCollection<PlaylistItem> oldPlaylist, ObservableCollection<PlaylistItem> newPlaylist)
        {
            oldPlaylist.IfNotNull(i => i.CollectionChanged -= Playlist_CollectionChanged);
            newPlaylist.IfNotNull(i => i.CollectionChanged += Playlist_CollectionChanged);
        }

        private void Playlist_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CurrentPlaylistItem != null && Playlist != null && !Playlist.Contains(CurrentPlaylistItem))
            {
                Stop();
                CurrentPlaylistItem = null;
            }
        }

        #endregion

        #region CurrentPlaylistItem
        /// <summary>
        /// CurrentPlaylistItem DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty CurrentPlaylistItemProperty =
            DependencyProperty.Register("CurrentPlaylistItem", typeof(PlaylistItem), typeof(SMFPlayer),
                                        new PropertyMetadata(OnCurrentPlaylistItemPropertyChanged));

        /// <summary>
        /// Gets the current PlaylistItem.
        /// </summary>
        [ScriptableMember]
        public PlaylistItem CurrentPlaylistItem
        {
            get { return (PlaylistItem)GetValue(CurrentPlaylistItemProperty); }
            set { SetValue(CurrentPlaylistItemProperty, value); }
        }

        private static void OnCurrentPlaylistItemPropertyChanged(DependencyObject d,
                                                                 DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            if (player != null && player._isTemplateApplied)
            {
                player.IfNotNull(i => i.OnCurrentPlaylistItemChanged(e.OldValue as PlaylistItem));
            }
        }

        private void UpdateCurrentPlaylistItemSelection() 
        {
            PlaylistSelectionElement.IfNotNull(i => i.SelectedItem = CurrentPlaylistItem);
        }

        #endregion

        #region PlaySpeedState
        /// <summary>
        /// PlaySpeedState DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaySpeedStateProperty =
            DependencyProperty.Register("PlaySpeedState", typeof(PlaySpeedState), typeof(SMFPlayer),
                                        new PropertyMetadata(PlaySpeedState.NormalPlayback, OnPlaySpeedStatePropertyChanged));

        /// <summary>
        /// Gets a value indicating the play speed of the media.
        /// </summary>
        [ScriptableMember]
        public PlaySpeedState PlaySpeedState
        {
            get { return (PlaySpeedState)GetValue(PlaySpeedStateProperty); }
            private set { SetValue(PlaySpeedStateProperty, value); }
        }

        private static void OnPlaySpeedStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlaySpeedStateChanged());
        }

        private void OnPlaySpeedStateChanged()
        {
            if (!IsInDesignMode)
            {
                string logMessage = string.Format(SilverlightMediaFrameworkResources.PlaySpeedChangedLogMessage,
                                                  PlaySpeedState);
                SendLogEntry(message: logMessage, type: KnownLogEntryTypes.PlaySpeedStateChanged,
                    extendedProperties: new Dictionary<string, object> { { "PlaySpeedState", PlaySpeedState } });
            }

            if (SlowMotionElement != null
                && SlowMotionElement.IsChecked.HasValue
                && SlowMotionElement.IsChecked.Value
                && PlaySpeedState != PlaySpeedState.SlowMotion)
            {
                SlowMotionElement.IsChecked = false;
            }

            this.GoToVisualState(PlaySpeedState.ToString());
            PlaySpeedStateChanged.IfNotNull(i => i(this, new CustomEventArgs<PlaySpeedState>(PlaySpeedState)));
        }

        #endregion

        #region PlayState
        /// <summary>
        /// PlayState DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlayStateProperty =
            DependencyProperty.Register("PlayState", typeof(MediaPluginState), typeof(SMFPlayer),
                                        new PropertyMetadata(MediaPluginState.Stopped, OnPlayStatePropertyChanged));

        /// <summary>
        /// Gets the current play state of the Player.
        /// </summary>
        [ScriptableMember]
        public MediaPluginState PlayState
        {
            get { return (MediaPluginState)GetValue(PlayStateProperty); }
            private set { SetValue(PlayStateProperty, value); }
        }

        private static void OnPlayStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnPlayStateChanged());
        }

        private void OnPlayStateChanged()
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.PlayStateChangedLogMessage, PlayState);
            SendLogEntry(message: logMessage, type: KnownLogEntryTypes.PlayStateChanged,
                extendedProperties: new Dictionary<string, object> { { "PlayState", PlayState } });

            this.GoToVisualState(PlayState.ToString());

            if (PlayState == MediaPluginState.Playing || PlayState == MediaPluginState.ClipPlaying)
            {
                IsPosterVisible = false;
                if (!_positionTimer.IsEnabled) _positionTimer.Start();
            }
            else if (PlayState != MediaPluginState.Paused)
            {
                if (_positionTimer.IsEnabled) _positionTimer.Stop();
            }
            
            PlayStateChanged.IfNotNull(i => i(this, new CustomEventArgs<MediaPluginState>(PlayState)));
        }

        #endregion

        #region ReplayOffset
        /// <summary>
        /// ReplayOffset DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ReplayOffsetProperty =
            DependencyProperty.Register("ReplayOffset", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.FromSeconds(5)));

        /// <summary>
        /// Gets or sets the amount of time to offset the current play position back for a replay.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan ReplayOffset
        {
            get { return (TimeSpan)GetValue(ReplayOffsetProperty); }
            set { SetValue(ReplayOffsetProperty, value); }
        }

        #endregion

        #region RetryInterval
        /// <summary>
        /// RetryInterval DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty RetryIntervalProperty =
            DependencyProperty.Register("RetryInterval", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.FromSeconds(10)));

        /// <summary>
        /// Gets or sets the number of times to retry opening failed media
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan RetryInterval
        {
            get { return (TimeSpan)GetValue(RetryIntervalProperty); }
            set { SetValue(RetryIntervalProperty, value); }
        }

        #endregion

        #region RetryDuration
        /// <summary>
        /// RetryDuration DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty RetryDurationProperty =
            DependencyProperty.Register("RetryDuration", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets the number of times to retry opening failed media
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan RetryDuration
        {
            get { return (TimeSpan)GetValue(RetryDurationProperty); }
            set { SetValue(RetryDurationProperty, value); }
        }

        #endregion

        #region RewindButtonBehavior
        /// <summary>
        /// RewindButtonBehavior DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty RewindButtonBehaviorProperty =
            DependencyProperty.Register("RewindButtonBehavior", typeof(ButtonInputType), typeof(SMFPlayer),
                                        new PropertyMetadata(ButtonInputType.ButtonHold));

        /// <summary>
        /// Gets or sets the behavior of the Rewind button when a user presses and holds the mouse down on the button. 
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public ButtonInputType RewindButtonBehavior
        {
            get { return (ButtonInputType)GetValue(RewindButtonBehaviorProperty); }
            set { SetValue(RewindButtonBehaviorProperty, value); }
        }

        #endregion

        #region SeekWhileScrubbing
        /// <summary>
        /// SeekWhileScrubbing DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty SeekWhileScrubbingProperty =
            DependencyProperty.Register("SeekWhileScrubbing", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(true));


        /// <summary>
        /// Gets or sets a value indicating whether the Player should seek to a new video position while scrubbing or wait until scrubbing is completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if the Player should seek to a new video position while scrubbing; otherwise, <c>false</c>.
        /// </value>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool SeekWhileScrubbing
        {
            get { return (bool)GetValue(SeekWhileScrubbingProperty); }
            set { SetValue(SeekWhileScrubbingProperty, value); }
        }

        #endregion

        #region ScriptableName
#if !WINDOWS_PHONE
        /// <summary>
        /// ScriptableName DependencyProperty definition.
        /// </summary>
        public static DependencyProperty ScriptableNameProperty =
            DependencyProperty.Register("ScriptableName", typeof(string), typeof(SMFPlayer),
                                        new PropertyMetadata(String.Empty, OnScriptableNamePropertyChanged));

        /// <summary>
        /// Set the object name used for the Javascript interface to the Player. String.Empty is the default and indicates
        /// no scripting.
        /// </summary>
        /// 
        [Category("Player Configuration")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public string ScriptableName
        {
            get { return (string)GetValue(ScriptableNameProperty); }
            set { SetValue(ScriptableNameProperty, value); }
        }

        private static void OnScriptableNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnScriptableNameChanged());
        }

        private void OnScriptableNameChanged()
        {
            if (!IsInDesignMode && !ScriptableName.IsNullOrWhiteSpace())
            {
                JavascriptBridge.Initialize(this, ScriptableName);
            }
        }
#endif
        #endregion

        #region SelectedAudioStream
        /// <summary>
        /// SelectedAudioStream DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty SelectedAudioStreamProperty =
            DependencyProperty.Register("SelectedAudioStream", typeof(StreamMetadata), typeof(SMFPlayer),
                                        new PropertyMetadata(OnSelectedAudioStreamPropertyChanged));

        /// <summary>
        /// Gets the selected AudioStream.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public StreamMetadata SelectedAudioStream
        {
            get { return (StreamMetadata)GetValue(SelectedAudioStreamProperty); }
            set { SetValue(SelectedAudioStreamProperty, value); }
        }

        private static void OnSelectedAudioStreamPropertyChanged(DependencyObject d,
                                                                 DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnSelectedAudioStreamChanged());
        }

        private void OnSelectedAudioStreamChanged()
        {
            SelectAudioStream();
            AudioStreamSelectionElement.IfNotNull(i => i.SelectedItem = SelectedAudioStream);
        }

        #endregion

        #region SelectedCaptionStream
        /// <summary>
        /// SelectedCaptionStream DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty SelectedCaptionStreamProperty =
            DependencyProperty.Register("SelectedCaptionStream", typeof(StreamMetadata), typeof(SMFPlayer),
                                        new PropertyMetadata(OnSelectedCaptionStreamPropertyChanged));

        /// <summary>
        /// Gets the selected AudioStream.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public StreamMetadata SelectedCaptionStream
        {
            get { return (StreamMetadata)GetValue(SelectedCaptionStreamProperty); }
            set { SetValue(SelectedCaptionStreamProperty, value); }
        }

        private static void OnSelectedCaptionStreamPropertyChanged(DependencyObject d,
                                                                   DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnSelectedCaptionStreamChanged());
        }

        private void OnSelectedCaptionStreamChanged()
        {
            SelectCaptionStream();
        }

        #endregion

        #region StartMuted
        /// <summary>
        /// StartMuted DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartMutedProperty =
            DependencyProperty.Register("StartMuted", typeof(bool), typeof(SMFPlayer), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether the player should start in muted state.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool StartMuted
        {
            get { return (bool)GetValue(StartMutedProperty); }
            set { SetValue(StartMutedProperty, value); }
        }

        #endregion

        #region StartPosition
        /// <summary>
        /// StartPosition DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof(TimeSpan), typeof(SMFPlayer),
                                        new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets the start position of the current playlist item.
        /// </summary>
        [ScriptableMember]
        public TimeSpan StartPosition
        {
            get { return (TimeSpan)GetValue(StartPositionProperty); }
            private set { SetValue(StartPositionProperty, value); }
        }

        #endregion

        #region VersionInformationVisibility
        /// <summary>
        /// VersionInformationVisibility DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VersionInformationVisibilityProperty =
            DependencyProperty.Register("VersionInformationVisibility", typeof(FeatureVisibility), typeof(SMFPlayer),
                                        new PropertyMetadata(FeatureVisibility.Disabled,
                                                             OnVersionInformationVisibilityPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether to show version information.
        /// </summary>
        /// <remarks>
        /// The Version Information window shows the versions of all SMF assemblies on the client computer.
        /// The Version Information window is displayed on the client computer when Ctrl-Alt-V is pressed.
        /// </remarks>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FeatureVisibility VersionInformationVisibility
        {
            get { return (FeatureVisibility)GetValue(VersionInformationVisibilityProperty); }
            set { SetValue(VersionInformationVisibilityProperty, value); }
        }

        private static void OnVersionInformationVisibilityPropertyChanged(DependencyObject d,
                                                                          DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnVersionInformationVisibilityChanged());
        }

        private void OnVersionInformationVisibilityChanged()
        {
            if (!IsInDesignMode && VersionInformationElement != null)
            {
                if (VersionInformationVisibility == FeatureVisibility.Visible)
                {
                    var versionInformation = new VersionInformation
                    {
                        PlayerId = PlayerId,
                        AssemblyNames = PluginsManager.Assemblies.Select(i => i.FullName).ToList(),
#if !WINDOWS_PHONE
                        PluginMetadata = GetPluginMetadata().OrderBy(i => i.PluginName).ToList()
#endif
                    };

                    VersionInformationElement.Content = versionInformation;
                }
                else
                {
                    VersionInformationElement.Content = null;
                }
            }

            string state = VersionInformationVisibility == FeatureVisibility.Visible
                               ? SMFPlayerVisualStates.VersionInformationStates.VersionInformationVisible
                               : VersionInformationVisibility == FeatureVisibility.Disabled
                                     ? SMFPlayerVisualStates.VersionInformationStates.VersionInformationDisabled
                                     : SMFPlayerVisualStates.VersionInformationStates.VersionInformationHidden;
            this.GoToVisualState(state);
        }

        private IEnumerable<IPluginMetadata> GetPluginMetadata()
        {
            var metadata = new List<IPluginMetadata>();
            PluginsManager.GenericPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

			PluginsManager.S3DPlugins
				.Select(i => i.Metadata)
				.ForEach(metadata.Add);

            PluginsManager.AdPayloadHandlerPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            PluginsManager.LogWriterPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            PluginsManager.MarkerProviderPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            PluginsManager.MediaPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            PluginsManager.HeuristicsPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            PluginsManager.PresentationPlugins
                .Select(i => i.Metadata)
                .ForEach(metadata.Add);

            return metadata;
        }

        #endregion

        #region VideoHeight
        /// <summary>
        /// VideoHeight DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VideoHeightProperty =
            DependencyProperty.Register("VideoHeight", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata(double.NaN));

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        [ScriptableMember]
        public double VideoHeight
        {
            get { return (double)GetValue(VideoHeightProperty); }
            private set { SetValue(VideoHeightProperty, value); }
        }

        #endregion

        #region VideoWidth
        /// <summary>
        /// VideoWidth DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VideoWidthProperty =
            DependencyProperty.Register("VideoWidth", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata(double.NaN));

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        [ScriptableMember]
        public double VideoWidth
        {
            get { return (double)GetValue(VideoWidthProperty); }
            private set { SetValue(VideoWidthProperty, value); }
        }

        #endregion

        #region VolumeLevel
        /// <summary>
        /// VolumeLevel DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VolumeLevelProperty =
            DependencyProperty.Register("VolumeLevel", typeof(double), typeof(SMFPlayer),
                                        new PropertyMetadata(DefaultVolume, OnVolumeLevelPropertyChanged));

        /// <summary>
        /// Gets the current volume level.
        /// </summary>
        /// <remarks>
        /// The value is between 0 and 1.0.
        /// </remarks>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public double VolumeLevel
        {
            get { return (double)GetValue(VolumeLevelProperty); }
            set { SetValue(VolumeLevelProperty, value); }
        }

        private static void OnVolumeLevelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnVolumeLevelChanged());
        }

        private void OnVolumeLevelChanged()
        {
            ActiveMediaPlugin.IfNotNull(i => i.Volume = VolumeLevel);
            VolumeElement.IfNotNull(i => i.VolumeLevel = VolumeLevel);
            VolumeLevelChanged.IfNotNull(i => i(this, new CustomEventArgs<double>(VolumeLevel)));
            SendLogEntry(KnownLogEntryTypes.VolumeLevelChanged,
                extendedProperties: new Dictionary<string, object> { { "VolumeLevel", VolumeLevel } });
        }

        private void UpdateMute()
        {
            VolumeElement.IfNotNull(i => i.IsMuted = IsMuted || StartMuted);
        }

        #endregion

        #region IsMuted
        /// <summary>
        /// IsMuted DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsMutedProperty =
            DependencyProperty.Register("IsMuted", typeof(bool), typeof(SMFPlayer),
                                        new PropertyMetadata(false, OnIsMutedPropertyChanged));

        /// <summary>
        /// Gets or sets whether the current media is muted.
        /// </summary>
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [ScriptableMember]
        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        private static void OnIsMutedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as SMFPlayer;
            player.IfNotNull(i => i.OnIsMutedChanged());
        }

        private void OnIsMutedChanged()
        {
            ActiveMediaPlugin.IfNotNull(i => i.IsMuted = IsMuted);
            IsMutedChanged.IfNotNull(i => i(this, new CustomEventArgs<bool>(IsMuted)));
            SendLogEntry(KnownLogEntryTypes.IsMutedChanged,
                extendedProperties: new Dictionary<string, object> { { "IsMuted", IsMuted } });
        }

        #endregion

        #region HeuristicsPluginRequiredMetadata
        /// <summary>
        /// HeuristicsPluginRequiredMetadata DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty HeuristicsPluginRequiredMetadataProperty =
            DependencyProperty.Register("HeuristicsPluginRequiredMetadata", typeof(MetadataCollection),
                                        typeof(SMFPlayer), null);

        /// <summary>
        /// Gets or sets the metadata used to selected a heuristics plugin.
        /// </summary>
        [Category("Media Heuristics")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [ScriptableMember]
        public MetadataCollection HeuristicsPluginRequiredMetadata
        {
            get { return (MetadataCollection)GetValue(HeuristicsPluginRequiredMetadataProperty); }
            set { SetValue(HeuristicsPluginRequiredMetadataProperty, value); }
        }

        #endregion

        #endregion

        #region Control Event Handlers



        private void ControlStripToggleElement_Click(object sender, RoutedEventArgs e)
        {
            IsControlStripVisible = !IsControlStripVisible;
        }
        
        private void MediaPresenterElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnSizeChanged();
            ConfigureCaptionPresenterSize();
        }

        partial void OnSizeChanged();

        private void MaxBitrateLimiterElement_RecommendMaximumBitrate(object sender, CustomEventArgs<long> args)
        {
            if (args.Value > 0)
            {
                ActiveAdaptiveMediaPlugin.IfNotNull(i => i.SetVideoBitrateRange(0, args.Value, false));
            }
        }

        private void GraphToggleElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (PlayerGraphVisibility != FeatureVisibility.Disabled)
            {
                PlayerGraphVisibility = GraphToggleElement.IsChecked.HasValue && GraphToggleElement.IsChecked.Value
                                            ? FeatureVisibility.Visible
                                            : FeatureVisibility.Hidden;
            }
        }

        private void ShowChaptersElement_Click(object sender, RoutedEventArgs e)
        {
            if (ChaptersVisibility != FeatureVisibility.Disabled)
            {
                ChaptersVisibility = FeatureVisibility.Visible;
            }
        }

        private void HideChaptersElement_Click(object sender, RoutedEventArgs e)
        {
            if (ChaptersVisibility != FeatureVisibility.Disabled)
            {
                ChaptersVisibility = FeatureVisibility.Hidden;
            }
        }

        private void ShowPlaylistElement_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistVisibility != FeatureVisibility.Disabled)
            {
                PlaylistVisibility = FeatureVisibility.Visible;
            }
        }

        private void HidePlaylistElement_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistVisibility != FeatureVisibility.Disabled)
            {
                PlaylistVisibility = FeatureVisibility.Hidden;
            }
        }

        private void AudioStreamSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAudioStream = AudioStreamSelectionElement.SelectedItem as StreamMetadata;
        }

        private void TimelineElement_MarkerSelected(object sender, CustomEventArgs<MediaMarker> args)
        {
            var displayMediaMarker = args.Value as TimelineMediaMarker;
            if (displayMediaMarker != null && displayMediaMarker.AllowSeek)
            {
                SeekToPosition(args.Value.Begin);
            }
        }

        private void TimelineElement_ScrubbingStarted(object sender, CustomEventArgs<TimeSpan> args)
        {
            OnScrubbingStarted(args.Value);
        }

        private void TimelineElement_Scrubbing(object sender, CustomEventArgs<TimeSpan> args)
        {
            OnScrubbing(args.Value);
        }

        private void TimelineElement_ScrubbingCompleted(object sender, CustomEventArgs<TimeSpan> args)
        {
            OnScrubbingCompleted(args.Value);
        }

        private void RewindElement_Click(object sender, RoutedEventArgs e)
        {
            if (RewindButtonBehavior == ButtonInputType.ClickToggle)
            {
                if (PlaySpeedState != PlaySpeedState.Rewinding)
                {
                    StartRewind();
                }
                else if (PlaySpeedManager.CanIncrementRewind)
                {
                    PlaySpeedManager.IncrementRewind();
                }
                else
                {
                    StopRewind();
                }
            }
        }

        private void FastForwardElement_Click(object sender, RoutedEventArgs e)
        {
            if (FastForwardButtonBehavior == ButtonInputType.ClickToggle)
            {
                if (PlaySpeedState != PlaySpeedState.FastForwarding)
                {
                    StartFastForward();
                }
                else if (PlaySpeedManager.CanIncrementFastForward)
                {
                    PlaySpeedManager.IncrementFastForward();
                }
                else
                {
                    StopFastForward();
                }
            }
        }

        private void RewindElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RewindButtonBehavior == ButtonInputType.ButtonHold)
            {
                StartRewind();
            }
        }

        private void RewindElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (RewindButtonBehavior == ButtonInputType.ButtonHold)
            {
                StopRewind();
            }
        }

        private void FastForwardElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FastForwardButtonBehavior == ButtonInputType.ButtonHold)
            {
                StartFastForward();
            }
        }

        private void FastForwardElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (FastForwardButtonBehavior == ButtonInputType.ButtonHold)
            {
                StopFastForward();
            }
        }

        private void CaptionDisplayToggleElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (CaptionsVisibility != FeatureVisibility.Disabled)
            {
                CaptionsVisibility = CaptionDisplayToggleElement.IsChecked.HasValue
                                     && CaptionDisplayToggleElement.IsChecked.Value
                                         ? FeatureVisibility.Visible
                                         : FeatureVisibility.Hidden;
            }
        }

        private void VolumeElement_UnMuted(object sender, EventArgs args)
        {
            IsMuted = false;
        }

        private void VolumeElement_Muted(object sender, EventArgs args)
        {
            IsMuted = true;
        }

        private void VolumeElement_VolumeLevelChanged(object sender, CustomEventArgs<double> args)
        {
            VolumeLevel = args.Value;
        }

        private void SlowMotionElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (SlowMotionElement.IsChecked.HasValue && SlowMotionElement.IsChecked.Value)
            {
                StartSlowMotion();
            }
            else
            {
                StopSlowMotion();
            }
        }

        private void ReplayElement_Click(object sender, RoutedEventArgs e)
        {
            Replay();
        }

        private void PreviousPlaylistItemElement_Click(object sender, RoutedEventArgs e)
        {
            GoToPreviousPlaylistItem();
        }

        private void PreviousChapterElement_Click(object sender, RoutedEventArgs e)
        {
            GoToPreviousChapter();
        }

        private void PlaylistSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var playlistItem = PlaylistSelectionElement.SelectedItem as PlaylistItem;
            GoToPlaylistItem(playlistItem);
        }

        private void PlaylistDisplayToggleElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (PlaylistVisibility != FeatureVisibility.Disabled)
            {
                PlaylistVisibility = PlaylistDisplayToggleElement.IsChecked.HasValue
                                     && PlaylistDisplayToggleElement.IsChecked.Value
                                         ? FeatureVisibility.Visible
                                         : FeatureVisibility.Hidden;
            }
        }

        private void PlayElement_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveMediaPlugin != null)
            {
                if (ActiveMediaPlugin.CurrentState == MediaPluginState.Playing)
                {
                    if (ActiveMediaPlugin.CanPause)
                    {
                        Pause();
                    }
                }
                else
                {
                    Play();
                }
            }
        }

        private void NextPlaylistItemElement_Click(object sender, RoutedEventArgs e)
        {
            GoToNextPlaylistItem();
        }

        private void NextChapterElement_Click(object sender, RoutedEventArgs e)
        {
            GoToNextChapter();
        }

        private void SeekToLiveElement_Click(object sender, RoutedEventArgs e)
        {
            SeekToLive();
        }

        private void FullScreenToggleElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            IsFullScreen = FullScreenToggleElement.IsChecked.HasValue && FullScreenToggleElement.IsChecked.Value;
        }

        private void ChaptersDisplayToggleElement_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (ChaptersVisibility != FeatureVisibility.Disabled)
            {
                ChaptersVisibility = ChaptersDisplayToggleElement.IsChecked.HasValue
                                     && ChaptersDisplayToggleElement.IsChecked.Value
                                         ? FeatureVisibility.Visible
                                         : FeatureVisibility.Hidden;
            }
        }

        private void TimelineMarkerSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var timelineMarker = TimelineMarkerSelectionElement.SelectedItem as TimelineMediaMarker;
            timelineMarker.IfNotNull(i => SeekToPosition(i.Begin));
        }

        private void ChapterSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chapterItem = ChapterSelectionElement.SelectedItem as Chapter;
            chapterItem.IfNotNull(GoToChapterItem);
        }

        private void ButtonRetryElement_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveMediaPlugin != null)
            {
                RetryState = RetryStateEnum.NotRetrying;
                Retry();
            }
        }
        
        private void Retry()
        {
            var isMediaSourceAdaptive = CurrentPlaylistItem.DeliveryMethod == DeliveryMethods.AdaptiveStreaming;
            var mediaSource = new Uri(CurrentPlaylistItem.MediaSource.AbsoluteUri);

            if (!isMediaSourceAdaptive)
            {
                ActiveMediaPlugin.Source = mediaSource;
            }
            else
            {
                var adaptiveMediaPlugin = ActiveMediaPlugin as Microsoft.SilverlightMediaFramework.Plugins.IAdaptiveMediaPlugin;
                if (adaptiveMediaPlugin != null)
                {
                    adaptiveMediaPlugin.AdaptiveSource = null;
                    adaptiveMediaPlugin.AutoPlay = true;
                    adaptiveMediaPlugin.AdaptiveSource = mediaSource;
                }
            }
        }

        #endregion

        partial void OnPlaylistItemChanged()
        {
            PlaylistItemChanged.IfNotNull(i => i(this, new CustomEventArgs<PlaylistItem>(CurrentPlaylistItem)));
        }
    }
}
