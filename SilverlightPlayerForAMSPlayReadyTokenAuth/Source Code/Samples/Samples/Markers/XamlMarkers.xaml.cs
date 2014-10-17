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
    [SampleAttribute(GroupNames.Markers, "Visual XAML Markers")]
    public partial class XamlMarkers : UserControl, ISupportCodeDisplay
    {
        public XamlMarkers()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get
            {
                return @"public partial class XamlMarkers : UserControl
{
    public XamlMarkers()
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
        <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
            <smfm:PlaylistItem.TimelineMarkers>
                <smfm:TimelineMediaMarker Begin=""0:0:10"" End=""0:0:10"" AllowSeek=""true"">
                    <smfm:TimelineMediaMarker.Content>
                        <StackPanel Orientation=""Horizontal"">
                            <TextBlock Text=""There should be an ellipse: "" FontSize=""16""/>
                            <Ellipse Fill=""Yellow"" Width=""20"" Height=""20"" Stroke=""Black"" StrokeThickness=""2"" Margin=""3""/>
                        </StackPanel>
                    </smfm:TimelineMediaMarker.Content>
                </smfm:TimelineMediaMarker>
                <smfm:TimelineMediaMarker Begin=""0:1:15"" End=""0:1:15"" AllowSeek=""true"">
                    <smfm:TimelineMediaMarker.Content>
                        <StackPanel Orientation=""Horizontal"">
                            <TextBlock Text=""Here's a button: "" FontSize=""16""/>
                            <Button Content=""Button"" Margin=""3""/>
                        </StackPanel>
                    </smfm:TimelineMediaMarker.Content>
                </smfm:TimelineMediaMarker>
            </smfm:PlaylistItem.TimelineMarkers>
        </smfm:PlaylistItem>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
