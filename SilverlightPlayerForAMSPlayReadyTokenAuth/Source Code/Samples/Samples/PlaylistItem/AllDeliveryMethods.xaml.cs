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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.PlaylistItem
{
    [SampleAttribute(GroupNames.PlaylistItem, "Multiple Items Playlist")]
    public partial class AllDeliveryMethods : UserControl, ISupportCodeDisplay
    {
        public AllDeliveryMethods()
        {
            InitializeComponent();
        }


        public string CSharpCode
        {
            get
            {
                return @"    public partial class AllDeliveryMethods : UserControl
    {
        public AllDeliveryMethods()
        {
            InitializeComponent();
        }
    }";
            }
        }

        public string XamlCode
        {
            get
            {
                return @"<smf:SMFPlayer>
    <smf:SMFPlayer.Playlist>
        <smfm:PlaylistItem Title=""Progressive Download Video"" DeliveryMethod=""ProgressiveDownload"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/progressivecontent/wildlife.wmv"" ThumbSource=""../../Assets/ElephentsDream.jpg"" />
        <smfm:PlaylistItem Title=""Smooth Streaming Video"" DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
        <smfm:PlaylistItem Title=""Streaming Video"" DeliveryMethod=""Streaming""  MediaSource=""http://demo.com/StreamingSource"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
