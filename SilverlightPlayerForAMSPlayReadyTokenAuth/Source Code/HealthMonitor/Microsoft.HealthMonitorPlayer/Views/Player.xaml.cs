using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace Microsoft.HealthMonitorPlayer.Views
{
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty VideoUrlProperty = DependencyProperty.Register("VideoUrlProperty", typeof(string), typeof(Player), new PropertyMetadata((d, e) => ((Player)d).OnVideoUrlChanged((string)e.NewValue)));
        public string VideoUrl
        {
            get { return (string)GetValue(VideoUrlProperty); }
            set { SetValue(VideoUrlProperty, value); }
        }

        void OnVideoUrlChanged(string url) {
            if (url == null)
                StopVideo();
            else
                PlayVideo(url);
        }

        public void PlayVideo(string Url)
        {
            var playlist = new ObservableCollection<Microsoft.SilverlightMediaFramework.Core.Media.PlaylistItem>();
            playlist.Add(new Microsoft.SilverlightMediaFramework.Core.Media.PlaylistItem()
            {
                MediaSource = new Uri(Url, UriKind.Absolute),
                DeliveryMethod = Microsoft.SilverlightMediaFramework.Plugins.Primitives.DeliveryMethods.AdaptiveStreaming
            });
            playerSmooth.Playlist = playlist;
        }

        public void StopVideo()
        {
            playerSmooth.Playlist.Clear();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "SettingsVisible", true);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "SettingsHidden", true);
        }
    }
}
