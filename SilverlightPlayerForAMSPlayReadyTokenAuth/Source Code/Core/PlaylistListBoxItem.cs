using System.Windows;
using System.Windows.Controls.Primitives;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// A specialized ListBoxItem used for displaying a playlist item.
    /// </summary>
    [TemplateVisualState(Name = PlaylistListBoxItemVisualStates.PlayingStates.IsPlaying, GroupName = PlaylistListBoxItemVisualStates.GroupNames.PlayingStates)]
    [TemplateVisualState(Name = PlaylistListBoxItemVisualStates.PlayingStates.IsNotPlaying, GroupName = PlaylistListBoxItemVisualStates.GroupNames.PlayingStates)]
    [TemplateVisualState(Name = PlaylistListBoxItemVisualStates.ChapterSelectionStates.ChapterSelectionAllowed, GroupName = PlaylistListBoxItemVisualStates.GroupNames.HasChaptersStates)]
    [TemplateVisualState(Name = PlaylistListBoxItemVisualStates.ChapterSelectionStates.ChapterSelectionNotAllowed, GroupName = PlaylistListBoxItemVisualStates.GroupNames.HasChaptersStates)]
    [TemplatePart(Name = PlaylistListBoxItemVisualElements.ChaptersButtonElement, Type = typeof (ButtonBase))]
    public class PlaylistListBoxItem : PositionAwareListBoxItem
    {
        private bool _isPlaying;

        public PlaylistListBoxItem()
        {
            DefaultStyleKey = typeof (PlaylistListBoxItem);
        }

        /// <summary>
        /// Gets or sets whether the playlist item being displayed is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    string isPlayingState = _isPlaying
                                                ? PlaylistListBoxItemVisualStates.PlayingStates.IsPlaying
                                                : PlaylistListBoxItemVisualStates.PlayingStates.IsNotPlaying;
                    this.GoToVisualState(isPlayingState);
                }
            }
        }

        private bool AllowChapterSelection
        {
            get
            {
                var player = this.GetVisualParent<SMFPlayer>();
                return player != null
                       && player.ChaptersVisibility != FeatureVisibility.Disabled;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var chaptersButton = GetTemplateChild(PlaylistListBoxItemVisualElements.ChaptersButtonElement) as ButtonBase;
            chaptersButton.IfNotNull(i => i.Click += ChaptersButton_Click);

            UpdateChaptersState();
            var player = this.GetVisualParent<SMFPlayer>();
            var playlistItem = DataContext as PlaylistItem;
            IsPlaying = player != null && player.CurrentPlaylistItem == playlistItem;
            if (player != null)
            {
                IsPlaying = player.CurrentPlaylistItem == playlistItem;
            }
        }

        internal void UpdateChaptersState()
        {
            var playlistItem = DataContext as PlaylistItem;

            string hasChaptersState = AllowChapterSelection
                                      && playlistItem != null
                                      && playlistItem.Chapters != null
                                      && playlistItem.Chapters.Count > 0
                                          ? PlaylistListBoxItemVisualStates.ChapterSelectionStates.
                                                ChapterSelectionAllowed
                                          : PlaylistListBoxItemVisualStates.ChapterSelectionStates.
                                                ChapterSelectionNotAllowed;

            this.GoToVisualState(hasChaptersState);
        }

        private void ChaptersButton_Click(object sender, RoutedEventArgs e)
        {
            var player = this.GetVisualParent<SMFPlayer>();

            if (AllowChapterSelection && player != null && player.ChaptersVisibility != FeatureVisibility.Disabled)
            {
                player.ChaptersVisibility = FeatureVisibility.Visible;
            }
        }
    }

    internal static class PlaylistListBoxItemVisualElements
    {
        public const string ChaptersButtonElement = "ChaptersButtonElement";
    }

    internal static class PlaylistListBoxItemVisualStates
    {
        #region Nested type: ChapterSelectionStates

        internal static class ChapterSelectionStates
        {
            public const string ChapterSelectionAllowed = "ChapterSelectionAllowed";
            public const string ChapterSelectionNotAllowed = "ChapterSelectionNotAllowed";
        }

        #endregion

        #region Nested type: GroupNames

        internal static class GroupNames
        {
            public const string PlayingStates = "PlayingStates";
            public const string HasChaptersStates = "HasChaptersStates";
        }

        #endregion

        #region Nested type: PlayingStates

        internal static class PlayingStates
        {
            public const string IsPlaying = "IsPlaying";
            public const string IsNotPlaying = "IsNotPlaying";
        }

        #endregion
    }
}