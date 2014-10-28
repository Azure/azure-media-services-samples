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
    [SampleAttribute(GroupNames.PlaylistItem, "Progressive Download Player")]
    public partial class ProgressiveDownload : UserControl, ISupportCodeDisplay
    {
        public ProgressiveDownload()
        {
            InitializeComponent();
        }


        public string CSharpCode
        {
            get
            {
                return @"    public partial class ProgressiveDownload : UserControl
    {
        public ProgressiveDownload()
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
             <smfm:PlaylistItem DeliveryMethod=""ProgressiveDownload"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/progressivecontent/wildlife.wmv""/>
        </smfm:Playlist>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
