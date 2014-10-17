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
    [SampleAttribute(GroupNames.Captions, "Using in-stream DFXP captions")]
    public partial class InStreamCaptions : UserControl, ISupportCodeDisplay
    {
        private const string InStreamCaptionsCSharpCode = "";
        private const string InStreamCaptionsXamlCode = @"<smf:SMFPlayer CaptionsVisibility=""Visible"">
            <smf:SMFPlayer.Playlist>
                <smf_media:PlaylistItem SelectedCaptionStreamName=""textstream_eng"" DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest""/>
            </smf:SMFPlayer.Playlist>
        </smf:SMFPlayer>";

        public InStreamCaptions()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get { return InStreamCaptionsCSharpCode; }
        }

        public string XamlCode
        {
            get { return InStreamCaptionsXamlCode; }
        }
    }
}
