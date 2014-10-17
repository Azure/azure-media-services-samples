using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Microsoft.Phone.Shell;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Test.Phone.ViewModels
{
    
    public class RootViewModel : ViewModelBase
    {
        private ObservableCollection<PlaylistItem> _playlist;
        public ObservableCollection<PlaylistItem> Playlist
        {
            get { return _playlist; }
            set
            {
                if (_playlist != value)
                {
                    _playlist = value;
                    NotifyPropertyChanged("Playlist");
                }
            }
        }

        private PlaylistItem _selectedPlaylistItem;
        public PlaylistItem SelectedPlaylistItem
        {
            get { return _selectedPlaylistItem; }
            set
            {
                if (_selectedPlaylistItem != value)
                {
                    _selectedPlaylistItem = value;
                    NotifyPropertyChanged("SelectedPropertyChanged");
                }
            }
        }

        
        public RootViewModel()
        {
            _playlist = new ObservableCollection<PlaylistItem>();
        }



        private const string SelectedPlaylistItemStateKey = "SelectedPlaylistItemIndex";

        public void Deactivate()
        {
            PhoneApplicationService.Current.State[SelectedPlaylistItemStateKey] = Playlist != null && SelectedPlaylistItem != null
                ? (int?)Playlist.IndexOf(SelectedPlaylistItem)
                : null;
        }

        public void Activate()
        {
            if (PhoneApplicationService.Current.State.ContainsKey(SelectedPlaylistItemStateKey))
            {
                var selectedPlaylistItemIndex = PhoneApplicationService.Current.State[SelectedPlaylistItemStateKey] as int?;

                if (selectedPlaylistItemIndex.HasValue)
                {
                    SelectedPlaylistItem = Playlist[selectedPlaylistItemIndex.Value];
                }
            }
        }
    }
}
