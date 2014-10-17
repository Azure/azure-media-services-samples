using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.PlaylistItem
{
    [SampleAttribute(GroupNames.PlaylistItem, "Smooth Streaming Player")]
    public partial class SmoothStreaming : UserControl, ISupportCodeDisplay
    {
        public SmoothStreaming()
        {
            InitializeComponent();
        }


        public string CSharpCode
        {
            get
            {
                return
                    @"    public partial class SmoothStreaming : UserControl
    {
        public SmoothStreaming()
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
    	<smfm:Playlist>
    		<smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest""/>
    	</smfm:Playlist>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
