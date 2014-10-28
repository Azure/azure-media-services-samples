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
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.PlayerControl
{
    [SampleAttribute(GroupNames.FullScreen, "Pause on Switch to Full Screen")]
    public partial class FullScreen : UserControl, ISupportCodeDisplay
    {
        public FullScreen()
        {
            InitializeComponent();
            player.FullScreenChanged += player_FullScreenChanged;
        }

        void player_FullScreenChanged(object sender, EventArgs args)
        {
            var player = (SMFPlayer) sender;
            if (player.IsFullScreen)
            {
                player.Pause();
            }
            else
            {
                player.Play();
            }
        }

        public string CSharpCode
        {
            get
            {
                return @"public partial class FullScreen : UserControl
{
    public FullScreen()
    {
        InitializeComponent();
        player.FullScreenChanged += new Action<Player>(player_FullScreenChanged);
    }

    void player_FullScreenChanged(Player obj)
    {
        if (obj.IsFullScreen)
        {
            obj.Pause();
        }
        else
        {
            obj.Play();
        }
    }
}";
            }
        }

        public string XamlCode
        {
            get
            {
                return @"<smf:SMFPlayer x:Name=""player"">
    <smf:SMFPlayer.Playlist>
        <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"">
        </smfm:PlaylistItem>
    </smf:SMFPlayer.Playlist>
</smf:SMFPlayer>";
            }
        }
    }
}
