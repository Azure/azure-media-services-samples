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
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Anaglyph3D;


namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Anaglyph3D
{
	[Sample(GroupNames.Stereoscopic3D, "Anaglyph 3D JavaScript Playlist")]
	public partial class Anaglyph3DJavascriptPlaylist : UserControl, ISupportHtmlDisplay
	{
		public Anaglyph3DJavascriptPlaylist()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Uri currentUri = new Uri(Application.Current.Host.Source, @"../Samples/S3DHtmlResources/anaglyph3Djsplayer.html");
			HtmlPage.Window.Invoke("open", new object[] { currentUri.AbsolutePath, "login", "resizable=1,width=646,height=436" });
		}

		public string HtmlCode
		{
			get
			{
				return
					@"                var slCtl = null;
        function pluginLoaded(sender, args) {
            slCtl = sender.getHost().Content;
        }

        function createNewPlaylistItem() {
            var NewPlaylistItemAdaptive = slCtl.Player.CreatePlaylistItem('http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_SxS_LeftFirst.ism/manifest', '', 'Smooth Streaming Anaglyph Half Color 3D', 'Description');
            NewPlaylistItemAdaptive.DeliveryMethod = 'AdaptiveStreaming';
            NewPlaylistItemAdaptive.ScriptS3DProperties.S3DFormat = \""SideBySide\"";
            NewPlaylistItemAdaptive.ScriptS3DProperties.S3DEyePriority = \""LeftFirst\"";
            NewPlaylistItemAdaptive.ScriptS3DProperties.S3DLeftEyePAR = \""2.0\"";
            NewPlaylistItemAdaptive.ScriptS3DProperties.S3DRightEyePAR = \""2.0\"";

            var NewPlaylist = slCtl.Player.CreatePlaylist();
            NewPlaylist.AddPlaylistItem(NewPlaylistItemAdaptive);
            NewPlaylist.AddPlaylistItem(NewPlaylistItemProgressive);
            slCtl.Player.SetPlaylist(NewPlaylist);
        }";
			}
		}
	}
}
