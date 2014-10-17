using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Javascript
{
    [Sample(GroupNames.Javascript, "Setup Javascript Playlist")]
    public partial class JavascriptPlaylist : UserControl, ISupportHtmlDisplay
    {
        public JavascriptPlaylist()
        {
            InitializeComponent();
        }

        public string HtmlCode
        {
            get
            {
                return
                    @"        var slCtl = null;
        function pluginLoaded(sender, args) {  
            slCtl = sender.getHost().Content;
        }

        function createNewPlaylistItem() {
            var NewPlaylistItem = slCtl.Player.CreatePlayListItem('http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest', '', 'Title', 'Description');
            NewPlaylistItem.DeliveryMethod = 'AdaptiveStreaming';
            var NewPlaylist = slCtl.Player.CreatePlaylist();
            NewPlaylist.AddPlaylistItem(NewPlaylistItem);
            slCtl.Player.SetPlaylist(NewPlaylist);
        }";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri currentUri = new Uri(Application.Current.Host.Source, @"../Samples/JavascriptPlaylistHtmlResources/player.html");
            HtmlPage.Window.Invoke("open", new object[] { currentUri.AbsolutePath, "login", "resizable=1,width=646,height=436" });
        }
    }
}
