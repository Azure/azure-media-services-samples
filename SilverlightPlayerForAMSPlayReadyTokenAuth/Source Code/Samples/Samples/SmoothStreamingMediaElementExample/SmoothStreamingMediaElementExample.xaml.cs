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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.SmoothStreamingMediaElementExample
{
    [SampleAttribute(GroupNames.SmoothStreamingMediaElement, "Access SmoothStreamingMediaElement")]
    public partial class SmoothStreamingMediaElementExample : UserControl, ISupportCodeDisplay
    {

        public SmoothStreamingMediaElementExample()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get
            {
                return
                    @"public class CustomPlayer : SMFPlayer
                            {
                                protected override void OnMediaOpened()
                                {
                                    base.OnMediaOpened();

                                    SmoothStreamingMediaElement ssme = ActiveMediaPlugin.VisualElement as SmoothStreamingMediaElement;

                                    if (ssme != null)
                                    {
                                        Debug.WriteLine('Accessed the SmoothStreamingMediaElement');
                                    }
                                }
                            }";
            }
        }

        public string XamlCode
        {
            get
            {
                return
                    @"<SmoothStreamingMediaElementExample:CustomPlayer>
            <SmoothStreamingMediaElementExample:CustomPlayer.Playlist>
                <Media:PlaylistItem DeliveryMethod='AdaptiveStreaming' MediaSource='http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/bigbuckbunny/big buck bunny.ism/manifest'/>
            </SmoothStreamingMediaElementExample:CustomPlayer.Playlist>
        </SmoothStreamingMediaElementExample:CustomPlayer>";
            }
        }
    }
}
