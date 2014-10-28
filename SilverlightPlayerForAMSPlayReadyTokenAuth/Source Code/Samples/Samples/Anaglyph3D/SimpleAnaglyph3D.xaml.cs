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
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Samples.Framework;
using Microsoft.SilverlightMediaFramework.Plugins.Anaglyph3D;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Anaglyph3D
{
	/// <summary>
	/// A simple Anaglyph 3D implementation, allowing switching modes.
	/// </summary>
	[SampleAttribute(GroupNames.Stereoscopic3D, "Simple Anaglyph 3D")]
	public partial class SimpleAnaglyph3D : UserControl, ISupportCodeDisplay
	{
		public SimpleAnaglyph3D()
		{
			InitializeComponent();

			smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
		}

		void smfPlayer_PlaylistItemChanged(object sender, Core.CustomEventArgs<Core.Media.PlaylistItem> e)
		{
			ChooseAnaglyphMode();
		}

		private void AnaglyphModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ChooseAnaglyphMode();
		}

		private void ChooseAnaglyphMode()
		{
			if ((Anaglyph3DPlugin.Current != null) && (AnaglyphModeSelector != null))
			{
				switch (((ComboBoxItem)AnaglyphModeSelector.SelectedItem).Content.ToString())
				{
					case "None":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.None);
							break;
						}
					case "Half Color 3D":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphHalfColorRedCyan);
							break;
						}
					case "Greyscale 3D":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphGrayRedCyan);
							break;
						}
					case "Left Eye Only":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphLeftOnly);
							break;
						}
				}
			}
		}

		public string CSharpCode
		{
			get
			{
				return @"    public Anaglyph3DExample()
		{
			InitializeComponent();

			smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
		}

		void smfPlayer_PlaylistItemChanged(object sender, Core.CustomEventArgs<Core.Media.PlaylistItem> e)
		{
			ChooseAnaglyphMode();
		}

		private void AnaglyphModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ChooseAnaglyphMode();
		}

		private void ChooseAnaglyphMode()
		{
			if (Anaglyph3DPlugin.Current != null)
			{
				switch (((ComboBoxItem)AnaglyphModeSelector.SelectedItem).Content.ToString())
				{
					case \""None\"":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.None);
							break;
						}
					case \""Half Color 3D\"":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphHalfColorRedCyan);
							break;
						}
					case \""Greyscale 3D\"":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphGrayRedCyan);
							break;
						}
					case \""Left Eye Only\"":
						{
							Anaglyph3DPlugin.Current.SetActiveDisplayMechanism(Anaglyph3DPlugin.AnaglyphDisplayMechanism.AnaglyphLeftOnly);
							break;
						}
				}
			}
		}";
			}
		}

		public string XamlCode
		{
			get
			{
				return @"<Grid x:Name=\""LayoutRoot\"" Background=\""White\"" Margin=\""0\"">
        <Grid.RowDefinitions>
            <RowDefinition Height=\""Auto\""/>
            <RowDefinition Height=\""*\""/>
        </Grid.RowDefinitions>
        <StackPanel Grid.Row=\""0\"" Orientation=\""Horizontal\"">
            <TextBlock Text=\""Selected Anaglyph Mode:\"" FontWeight=\""Bold\""></TextBlock>
            <ComboBox x:Name=\""AnaglyphModeSelector\"" SelectionChanged=\""AnaglyphModeSelector_SelectionChanged\"" Width=\""150\"">
                <ComboBoxItem Content=\""Half Color 3D\"" IsSelected=\""True\""></ComboBoxItem>
                <ComboBoxItem Content=\""Greyscale 3D\""></ComboBoxItem>
                <ComboBoxItem Content=\""Left Eye Only\""></ComboBoxItem>
                <ComboBoxItem Content=\""None\""></ComboBoxItem>
            </ComboBox>
        </StackPanel>
        <smf:SMFPlayer x:Name=\""smfPlayer\"" Grid.Row=\""1\"" >
            <smf:SMFPlayer.Playlist>
                <smfm:PlaylistItem Title=\""Progessive Anaglyph SxS Left First 3D\"" DeliveryMethod=\""ProgressiveDownload\""  MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_720p30_SxS_LeftFirst.wmv\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""LeftFirst\"" S3DFormat=\""SideBySide\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Progessive Anaglyph SxS Right First 3D\"" DeliveryMethod=\""ProgressiveDownload\""  MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_720p30_SxS_RightFirst.wmv\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""RightFirst\"" S3DFormat=\""SideBySide\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Progessive Anaglyph TxB Left First 3D\"" DeliveryMethod=\""ProgressiveDownload\""  MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_720p30_TxB_LeftFirst.wmv\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""LeftFirst\"" S3DFormat=\""TopAndBottom\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Progessive Anaglyph TxB Right First 3D\"" DeliveryMethod=\""ProgressiveDownload\""  MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_720p30_TxB_RightFirst.wmv\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""RightFirst\"" S3DFormat=\""TopAndBottom\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Smooth Streaming SxS Left First 3D Multi Bitrate\"" MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_SxS_LeftFirst.ism/manifest\"" DeliveryMethod=\""AdaptiveStreaming\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""LeftFirst\"" S3DFormat=\""SideBySide\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Smooth Streaming SxS Right First 3D Multi Bitrate\"" MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_SxS_RightFirst.ism/manifest\"" DeliveryMethod=\""AdaptiveStreaming\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""RightFirst\"" S3DFormat=\""SideBySide\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Smooth Streaming TxB Left First 3D Multi Bitrate\"" MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_TxB_LeftFirst.ism/manifest\"" DeliveryMethod=\""AdaptiveStreaming\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""LeftFirst\"" S3DFormat=\""TopAndBottom\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                    <smfm:PlaylistItem.CustomMetadata>
                        <smfutilitymd:MetadataItem Key=\""ExampleProperty1\"" Value=\""ExampleValue1\""/>
                        <smfutilitymd:MetadataItem Key=\""ExampleProperty2\"" Value=\""ExampleValue2\""/>
                    </smfm:PlaylistItem.CustomMetadata>
                </smfm:PlaylistItem>
                <smfm:PlaylistItem Title=\""Smooth Streaming TxB Right First 3D Multi Bitrate\"" MediaSource=\""http://devplatem.vo.msecnd.net/3D/NVIDIA_3DV_PC_1080p30_TxB_RightFirst.ism/manifest\"" DeliveryMethod=\""AdaptiveStreaming\"">
                    <smfm:PlaylistItem.S3DProperties>
                        <plugins3d:S3DProperties S3DEyePriority=\""RightFirst\"" S3DFormat=\""TopAndBottom\"" S3DLeftEyePAR=\""2.0\"" S3DRightEyePAR=\""2.0\"" />
                    </smfm:PlaylistItem.S3DProperties>
                    <smfm:PlaylistItem.CustomMetadata>
                        <smfutilitymd:MetadataItem Key=\""ExampleProperty1\"" Value=\""ExampleValue1\""/>
                        <smfutilitymd:MetadataItem Key=\""ExampleProperty2\"" Value=\""ExampleValue2\""/>
                    </smfm:PlaylistItem.CustomMetadata>
                </smfm:PlaylistItem>
            </smf:SMFPlayer.Playlist>
        </smf:SMFPlayer>    
    </Grid>        ";
			}
		}		

	}
}
