using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// A specialized ListBox used for displaying a playlist.
    /// </summary>
    public class PlaylistListBox : ScrollableListBox
    {
        private bool _allowChapterSelection = true;

        public PlaylistListBox()
        {
            DefaultStyleKey = typeof (PlaylistListBox);
            SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        /// Gets or sets whether to allow chapter selection.
        /// </summary>
        public bool AllowChapterSelection
        {
            get { return _allowChapterSelection; }
            set
            {
                if (_allowChapterSelection != value)
                {
                    _allowChapterSelection = value;
                    UpdatePlaylistItems();
                }
            }
        }


        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            Dispatcher.BeginInvoke(UpdatePlaylistItems);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(UpdateNowPlaying);
        }

        private void UpdateNowPlaying()
        {
            IList<PlaylistListBoxItem> playlistItemControls = this.GetVisualChildren<PlaylistListBoxItem>();

            foreach (PlaylistListBoxItem playlistItemControl in playlistItemControls)
            {
                var playlistItem = playlistItemControl.DataContext as PlaylistItem;
                playlistItemControl.IsPlaying = playlistItem != null && playlistItem == SelectedItem;
            }
        }

        private void UpdatePlaylistItems()
        {
            var playlist = DataContext as IList<PlaylistItem>;
            IList<PlaylistListBoxItem> playlistItemControls = this.GetVisualChildren<PlaylistListBoxItem>();

            if (playlist != null && playlistItemControls != null)
            {
                foreach (PlaylistListBoxItem playlistItemControl in playlistItemControls)
                {
                    playlistItemControl.UpdateChaptersState();

                    var playlistItem = playlistItemControl.DataContext as PlaylistItem;
                    if (playlistItem != null)
                    {
                        int index = playlist.IndexOf(playlistItem);
                        playlistItemControl.SetPosition(index, playlist.Count);
                    }
                }
            }
        }
    }
}