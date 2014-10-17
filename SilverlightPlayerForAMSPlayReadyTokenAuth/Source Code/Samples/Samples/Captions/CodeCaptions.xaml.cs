using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Captions
{
    [SampleAttribute(GroupNames.Captions, "Creating Captions In Code")]
    public partial class CodeCaptions : UserControl, ISupportCodeDisplay
    {
        private const string CodeCaptionsCSharpCode = @"public CodeCaptions()
        {
            InitializeComponent();
            CreatePlaylist();
        }

        private void CreatePlaylist()
        {
            var playlistItem = new Core.Media.PlaylistItem();
            playlistItem.MediaSource = new Uri(""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"");
            playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;

            var region = new CaptionRegion();
            playlistItem.Captions.Add(region);

            var caption1 = new CaptionElement
            {
                Begin = TimeSpan.Zero,
                End = TimeSpan.FromSeconds(10),
                Content = ""This marker should display from 00:00:00 to 00:00:10 and the text should be red.""
            };
            caption1.Style.Color = new SolidColorBrush(Colors.Red);
            caption1.Style.FontSize.Unit = LengthUnit.Pixel;
            caption1.Style.FontSize.Value = 15;
            region.Children.Add(caption1);

            var caption2 = new CaptionElement
            {
                Begin = TimeSpan.FromSeconds(20),
                End = TimeSpan.FromSeconds(30),
                Content = ""This marker should display from 00:00:20 to 00:00:30 and the text should be yellow.""
            };
            caption2.Style.Color = new SolidColorBrush(Colors.Yellow);
            caption2.Style.FontSize.Unit = LengthUnit.Pixel;
            caption2.Style.FontSize.Value = 15;
            region.Children.Add(caption2);

            player.Playlist.Add(playlistItem);
        }";
        private const string CodeCaptionsXamlCode = @"<smf:SMFPlayer x:Name=""player""/>";

        public CodeCaptions()
        {
            InitializeComponent();
            CreatePlaylist();
        }

        private void CreatePlaylist()
        {
            var playlistItem = new Core.Media.PlaylistItem();
            playlistItem.MediaSource = new Uri("http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest");
            playlistItem.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;

            var region = new CaptionRegion();
            playlistItem.Captions.Add(region);

            var caption1 = new CaptionElement
            {
                Begin = TimeSpan.Zero,
                End = TimeSpan.FromSeconds(10),
                Content = "This marker should display from 00:00:00 to 00:00:10 and the text should be red."
            };
            caption1.Style.Color = Colors.Red;
            caption1.Style.FontSize.Unit = LengthUnit.Pixel;
            caption1.Style.FontSize.Value = 15;
            region.Children.Add(caption1);

            var caption2 = new CaptionElement
            {
                Begin = TimeSpan.FromSeconds(20),
                End = TimeSpan.FromSeconds(30),
                Content = "This marker should display from 00:00:20 to 00:00:30 and the text should be yellow."
            };
            caption2.Style.Color = Colors.Yellow;
            caption2.Style.FontSize.Unit = LengthUnit.Pixel;
            caption2.Style.FontSize.Value = 15;
            region.Children.Add(caption2);

            player.Playlist.Add(playlistItem);
        }

        public string CSharpCode
        {
            get { return CodeCaptionsCSharpCode; }
        }

        public string XamlCode
        {
            get { return CodeCaptionsXamlCode; }
        }
    }
}
