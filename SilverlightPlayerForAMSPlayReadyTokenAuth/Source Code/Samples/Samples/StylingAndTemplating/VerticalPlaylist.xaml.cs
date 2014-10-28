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
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.StylingAndTemplating
{
    [Sample(GroupNames.StylingAndTemplating, "Playlist on Top")]
    public partial class VerticalPlaylist : UserControl, ISupportBlendInstructions, ISupportCodeDisplay
    {
        public VerticalPlaylist()
        {
            InitializeComponent();
        }

        public string BlendInstructions
        {
            get
            {
                return @"1. In Expression Blend, right click a Player control and select ""Edit Template"" --> ""Edit Copy"". 
2. Select the ""playlist"" visual element and change the Grid.Row from it's original value of ""1"" to ""0"".  ";
            }
        }

        public string CSharpCode
        {
            get { return @""; }
        }

        public string XamlCode
        {
            get
            {
                return @"<Grid Grid.Row=""0"" x:Name=""playlist"" Height=""0"" VerticalAlignment=""Top"" RenderTransformOrigin=""0.5,0.5"" Visibility=""Collapsed"" >

<smf:SMFPlayer Style=""{StaticResource PlayerStyle1}"" PlaylistVisibility=""Visible"">
    <smf:SMFPlayer.Playlist>
        <smfm:PlaylistItem Title=""Video #1"" DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
        <smfm:PlaylistItem Title=""Video #2"" DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
        <smfm:PlaylistItem Title=""Video #3"" DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>

";
            }
        }
    }
}
