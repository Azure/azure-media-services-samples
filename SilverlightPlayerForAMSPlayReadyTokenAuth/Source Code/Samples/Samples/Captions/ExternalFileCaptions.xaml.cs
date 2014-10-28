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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Markers
{
    [SampleAttribute(GroupNames.Captions, "External Captions File")]
    public partial class CaptionMarkers : UserControl, ISupportCodeDisplay
    {
        public CaptionMarkers()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get
            {
                return @"public partial class CaptionMarkers : UserControl
{
    public CaptionMarkers()
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
                return @"<smf:SMFPlayer CaptionsVisibility=""Visible"">
    <smf:SMFPlayer.Playlist>
        <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
            <smfm:PlaylistItem.MarkerResource>
                <smfm:MarkerResource
                            Format=""TTAF1-DFXP""
                            Source=""samplecaptions.xml""
                            />
            </smfm:PlaylistItem.MarkerResource>
        </smfm:PlaylistItem>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
