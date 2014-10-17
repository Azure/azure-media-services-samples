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
using Microsoft.SilverlightMediaFramework.Plugins.Anaglyph3D;
using System.Windows.Browser;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Anaglyph3D
{
	[Sample(GroupNames.Stereoscopic3D, "Anaglyph 3D HTML Playlist")]
	public partial class Anaglyph3DHTMLPlaylist : UserControl, ISupportHtmlDisplay
	{
		public Anaglyph3DHTMLPlaylist()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Uri currentUri = new Uri(Application.Current.Host.Source, @"../Samples/S3DHtmlResources/anaglyph3Dhtmlplaylist.html");
			HtmlPage.Window.Invoke("open", new object[] { currentUri.AbsolutePath, "login", "resizable=1,width=646,height=436" });
		}

		public string HtmlCode
		{
			get
			{
				return
					@"         
          <param name=\""initparams\"" value=\""PlayerSettings = 
                        <Playlist>
                            <AutoLoad>true</AutoLoad>
                            <AutoPlay>true</AutoPlay>
                            <DisplayTimeCode>false</DisplayTimeCode>
                            <EnableCachedComposition>true</EnableCachedComposition>
                            <EnableCaptions>true</EnableCaptions>
                            <EnableOffline>true</EnableOffline>
                            <EnablePopOut>true</EnablePopOut>
                            <StartMuted>false</StartMuted>
                            <StretchMode>None</StretchMode>
                            <Items>
								<PlaylistItem>
									<AudioCodec>WmaProfessional</AudioCodec>
									<Description>Anaglyph%203D%20Smooth%20Streaming</Description>
									<FileSize>23775864</FileSize>
									<FrameRate>29.9700898503294</FrameRate>
									<Height>476</Height>
									<IsAdaptiveStreaming>true</IsAdaptiveStreaming>
                                    <DeliveryMethod>AdaptiveStreaming</DeliveryMethod>
									<MediaSource>http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_SxS_LeftFirst.ism/manifest</MediaSource>
									<Title>Anaglyph 3D Smooth Streaming</Title>
									<VideoCodec>VC1</VideoCodec>
									<Width>848</Width>
                                    <S3DProperties>
                                        <S3DFormat>SideBySide</S3DFormat>
                                        <S3DEyePriority>LeftFirst</S3DEyePriority>
                                        <S3DLeftEyePAR>2.0</S3DLeftEyePAR>
                                        <S3DRightEyePAR>2.0</S3DRightEyePAR>
                                    </S3DProperties>
								</PlaylistItem>
                            </Items>
                        </Playlist>\""/>  ";
			}
		}
	}
}
