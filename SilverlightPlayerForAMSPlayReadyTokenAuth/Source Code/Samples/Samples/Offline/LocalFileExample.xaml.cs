using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Offline
{
    [SampleAttribute(GroupNames.Offline, "Open Local Media Files")]
    public partial class LocalFileExample : UserControl, ISupportCodeDisplay
    {
        private const string LocalFileExampleCSharpCode = @"    var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.Value)
            {
                var playlistItem = new Core.Media.PlaylistItem();

                playlistItem.StreamSource = openFileDialog.File.OpenRead();
                player.Playlist.Add(playlistItem);
                player.Play();
            }";
        private const string LocalFileExampleXamlCode = @"<smf:SMFPlayer x:Name='player' Grid.Row='0'/>
        <Button x:Name='btnOpenFile' Click='btnOpenFile_Click' Content='Open' Width='75' Height='35' Grid.Row='1'/>";

        public LocalFileExample()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.Value)
            {
                var playlistItem = new Core.Media.PlaylistItem();

                playlistItem.StreamSource = openFileDialog.File.OpenRead();
                player.Playlist.Add(playlistItem);
                player.Play();
            }
        }


        public string CSharpCode
        {
            get { return LocalFileExampleCSharpCode; }
        }

        public string XamlCode
        {
            get { return LocalFileExampleXamlCode; }
        }
    }
}
