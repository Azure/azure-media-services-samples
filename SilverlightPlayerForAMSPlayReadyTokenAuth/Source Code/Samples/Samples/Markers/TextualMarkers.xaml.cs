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
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Markers
{
    [SampleAttribute(GroupNames.Markers, "Textual Markers")]
    public partial class TextualMarkers : UserControl, ISupportCodeDisplay
    {
        public TextualMarkers()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get { return @"public partial class TextualMarkers : UserControl
{
    public TextualMarkers()
    {
        InitializeComponent();
    }
        }"; }
        }

        public string XamlCode
        {
            get
            {
                return @"<Grid x:Name=""LayoutRoot"" Background=""White"">
    <smf:SMFPlayer CaptionsVisibility=""Visible"">
        <smf:SMFPlayer.Playlist>
            <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
                <smfm:PlaylistItem.TimelineMarkers>
                    <smfm:TimelineMediaMarker Begin=""0:0:10"" End=""0:0:10"" Content=""This is a text marker"" AllowSeek=""true""/>
                    <smfm:TimelineMediaMarker Begin=""0:1:15"" End=""0:1:20"" Content=""This is another text marker"" AllowSeek=""true""/>
                </smfm:PlaylistItem.TimelineMarkers>
            </smfm:PlaylistItem>
        </smf:SMFPlayer.Playlist>
    </smf:SMFPlayer>
</Grid>"; }
        }


    }
}
