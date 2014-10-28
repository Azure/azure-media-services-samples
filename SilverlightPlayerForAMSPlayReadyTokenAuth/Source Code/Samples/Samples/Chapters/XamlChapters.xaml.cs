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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Chapters
{
    [SampleAttribute(GroupNames.Chapters, "Specfying XAML Chapters")]
    public partial class XamlChapters : UserControl, ISupportCodeDisplay
    {
        public XamlChapters()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get {
                return
        @"    public partial class XamlChapters : UserControl
    {
        public XamlChapters()
        {
            InitializeComponent();
        }
    }";
            }
        }

        public string XamlCode
        {
            get { return @"<smf:SMFPlayer>
    <smf:SMFPlayer.Playlist>
        <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
            <smfm:PlaylistItem.Chapters>
                <smfm:Chapter Begin=""0:0:0"" Title=""Proog Oversees"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s1_proog_t.jpg"" Description=""The main character, Proog, looks over his invented world.""/>
                <smfm:Chapter Begin=""0:0:27"" Title=""Emo in Danger"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s2_emo_t.jpg"" Description=""Emo seems oblivious to the imminent danger and Proog jumps in to help.""/>
                <smfm:Chapter Begin=""0:01:30"" Title=""On the Run"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s3_both_t.jpg"" Description=""Proog and Emo hurry to try to get to a place of safety.""/>
                <smfm:Chapter Begin=""0:02:31"" Title=""The Phone"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s3_telephone_t.jpg"" Description=""Nothing is as it seems, even the phone.""/>
            </smfm:PlaylistItem.Chapters>
        </smfm:PlaylistItem>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
