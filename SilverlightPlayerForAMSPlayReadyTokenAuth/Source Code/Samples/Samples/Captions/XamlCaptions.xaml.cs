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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Captions
{
    [SampleAttribute(GroupNames.Captions, "Creating Captions In Xaml")]
    public partial class XamlCaptions : UserControl, ISupportCodeDisplay
    {
        private const string XamlCaptionsCSharpCode = "";
        private const string XamlCaptionsXamlCode = @"<smf:SMFPlayer CaptionsVisibility=""Visible"">
            <smf:SMFPlayer.Playlist>
                <smf_media:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
                    <smf_media:PlaylistItem.Captions>
                        <smf_acc:CaptionRegion>
                            <smf_acc:CaptionRegion.Children>
                                <smf_acc:CaptionElement Begin=""00:00:00"" End=""00:00:10"" Content=""This marker should display from 00:00:00 to 00:00:10 and the text should be red."">
                                    <smf_acc:CaptionElement.Style>
                                        <smf_acc:TimedTextStyle Color=""Red"">
                                            <smf_acc:TimedTextStyle.FontSize>
                                                <smf_acc:Length Unit=""Pixel"" Value=""15""/>
                                            </smf_acc:TimedTextStyle.FontSize>
                                        </smf_acc:TimedTextStyle>
                                    </smf_acc:CaptionElement.Style>
                                </smf_acc:CaptionElement>
                                <smf_acc:CaptionElement Begin=""00:00:20"" End=""00:00:30"" Content=""This marker should display from 00:00:20 to 00:00:30 and the text should be yellow."">
                                    <smf_acc:CaptionElement.Style>
                                        <smf_acc:TimedTextStyle Color=""Yellow"">
                                            <smf_acc:TimedTextStyle.FontSize>
                                                <smf_acc:Length Unit=""Pixel"" Value=""15""/>
                                            </smf_acc:TimedTextStyle.FontSize>
                                        </smf_acc:TimedTextStyle>
                                    </smf_acc:CaptionElement.Style>
                                </smf_acc:CaptionElement>
                            </smf_acc:CaptionRegion.Children>
                        </smf_acc:CaptionRegion>
                    </smf_media:PlaylistItem.Captions>
                </smf_media:PlaylistItem>
            </smf:SMFPlayer.Playlist>
        </smf:SMFPlayer>";

        public XamlCaptions()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get { return XamlCaptionsCSharpCode; }
        }

        public string XamlCode
        {
            get { return XamlCaptionsXamlCode; }
        }
    }
}
