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
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Anaglyph3D
{
	/// <summary>
	/// This example shows how to load and unload a 3D plugin and switch it on and off 
	/// dynamically.  Keep in mind that when switching 3D plugins, the active
	/// media plugin must be re-loaded, and the playback position must be
	/// restored.
	/// </summary>
	[SampleAttribute(GroupNames.Stereoscopic3D, "Multi Mode Anaglyph 3D")]
	public partial class MultiModeAnaglyph3D : UserControl, ISupportCodeDisplay
	{
		private IMediaPlugin currentMediaPlugin;
		private TimeSpan restoreToPlaybackPosition = new TimeSpan(0);

		public MultiModeAnaglyph3D()
		{
			InitializeComponent();

			smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
			smfPlayer.MediaPluginRegistered += smfPlayer_MediaPluginRegistered;
			smfPlayer.MediaOpened += smfPlayer_MediaOpened;
		}		

		void Current_S3DPropertiesInvalid(object sender, S3DPropertiesEventArgs<Plugins.Primitives.S3D.S3DProperties> e)
		{
			//This playlist item is not supported for Anaglyph.  Nothing to do in this case,
			//but some consuming apps may wish to add error handling here.
		}

		void Current_S3DPropertiesValid(object sender, S3DPropertiesEventArgs<Plugins.Primitives.S3D.S3DProperties> e)
		{
			//This playlist item is supported for Anaglyph.  Nothing to do here, but some
			//consuming apps may want to add handling here.
		}

		void smfPlayer_MediaOpened(object sender, EventArgs e)
		{
			//If we have changed the 3D plugin, we'll need to restore the playback position
			//and re-set the anaglyph mode.
			if (restoreToPlaybackPosition.Ticks > 0)
			{
				smfPlayer.SeekToPosition(restoreToPlaybackPosition);
				restoreToPlaybackPosition = new TimeSpan(0);
				ChooseAnaglyphMode();
			}
		}

		void smfPlayer_PlaylistItemChanged(object sender, Core.CustomEventArgs<Core.Media.PlaylistItem> e)
		{
			ChooseAnaglyphMode();
		}

		void smfPlayer_MediaPluginRegistered(object sender, Core.CustomEventArgs<Plugins.IMediaPlugin> e)
		{
			//When the media plugin is registered, we'll enable and disable the appropriate
			//3D plugins, and record which media plugin is loaded.
			Choose3DPlugin();
			currentMediaPlugin = e.Value;

			//Subscribe to events indicating whether the 3D plugin's properties are valid
			if (Anaglyph3DPlugin.Current != null)
			{
				Anaglyph3DPlugin.Current.S3DPropertiesValid -= Current_S3DPropertiesValid;
				Anaglyph3DPlugin.Current.S3DPropertiesInvalid -= Current_S3DPropertiesInvalid;

				Anaglyph3DPlugin.Current.S3DPropertiesValid += Current_S3DPropertiesValid;
				Anaglyph3DPlugin.Current.S3DPropertiesInvalid += Current_S3DPropertiesInvalid;
			}
		}

		private void PluginSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//When selecting a 3D plugin, enable/disable the appropriate 3D plugins, store
			//the current playback position, and re-load the media plugin
			Choose3DPlugin();
			if (currentMediaPlugin != null)
			{
				restoreToPlaybackPosition = smfPlayer.PlaybackPosition;
				currentMediaPlugin.Unload();
				currentMediaPlugin.Load();
			}
		}

		private void AnaglyphModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ChooseAnaglyphMode();
		}

		private void StretchModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (StretchModeSelector != null)
			{
				switch (((ComboBoxItem)StretchModeSelector.SelectedItem).Content.ToString())
				{
					case "Uniform":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.StretchMode = Anaglyph3DPlugin.StretchModes.Uniform;
							break;
						}
					case "Fill":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.StretchMode = Anaglyph3DPlugin.StretchModes.Fill;
							break;
						}
				}
			}
		}

		private void Choose3DPlugin()
		{
			if (PluginSelector != null)
			{
				switch (((ComboBoxItem)PluginSelector.SelectedItem).Content.ToString())
				{
					case "None":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.Is3DPluginEnabled = false;

							AnaglyphModeSelector.IsEnabled = false;
							StretchModeSelector.IsEnabled = false;
							break;
						}
					case "Anaglyph":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.Is3DPluginEnabled = true;

							AnaglyphModeSelector.IsEnabled = true;
							StretchModeSelector.IsEnabled = true;
							break;
						}
				}
			}
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
				return @"    private IMediaPlugin currentMediaPlugin;
		private TimeSpan restoreToPlaybackPosition = new TimeSpan(0);

		public MultiModeAnaglyph3D()
		{
			InitializeComponent();

			smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
			smfPlayer.MediaPluginRegistered += smfPlayer_MediaPluginRegistered;
			smfPlayer.MediaOpened += smfPlayer_MediaOpened;
		}

		void smfPlayer_MediaOpened(object sender, EventArgs e)
		{
			//If we have changed the 3D plugin, we'll need to restore the playback position
			//and re-set the anaglyph mode.
			if (restoreToPlaybackPosition.Ticks > 0)
			{
				smfPlayer.SeekToPosition(restoreToPlaybackPosition);
				restoreToPlaybackPosition = new TimeSpan(0);
				ChooseAnaglyphMode();
			}
		}

		void smfPlayer_PlaylistItemChanged(object sender, Core.CustomEventArgs<Core.Media.PlaylistItem> e)
		{
			ChooseAnaglyphMode();
		}

		void smfPlayer_MediaPluginRegistered(object sender, Core.CustomEventArgs<Plugins.IMediaPlugin> e)
		{
			//When the media plugin is registered, we'll enable and disable the appropriate
			//3D plugins, and record which media plugin is loaded.
			Choose3DPlugin();
			currentMediaPlugin = e.Value;
		}

		private void PluginSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//When selecting a 3D plugin, enable/disable the appropriate 3D plugins, store
			//the current playback position, and re-load the media plugin
			Choose3DPlugin();
			if (currentMediaPlugin != null)
			{
				restoreToPlaybackPosition = smfPlayer.PlaybackPosition;
				currentMediaPlugin.Unload();
				currentMediaPlugin.Load();
			}
		}

		private void AnaglyphModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ChooseAnaglyphMode();
		}

		private void Choose3DPlugin()
		{
			if (PluginSelector != null)
			{
				switch (((ComboBoxItem)PluginSelector.SelectedItem).Content.ToString())
				{
					case \""None\"":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.Is3DPluginEnabled = false;
							break;
						}
					case \""Anaglyph\"":
						{
							if (Anaglyph3DPlugin.Current != null)
								Anaglyph3DPlugin.Current.Is3DPluginEnabled = true;
							break;
						}
				}
			}
		}

		private void ChooseAnaglyphMode()
		{
			if ((Anaglyph3DPlugin.Current != null) && (AnaglyphModeSelector != null))
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
            <TextBlock Text=\""Selected Plugin:\"" FontWeight=\""Bold\""></TextBlock>
            <ComboBox x:Name=\""PluginSelector\"" SelectionChanged=\""PluginSelector_SelectionChanged\"" Width=\""150\"" Margin=\""0,0,10,0\"">
                <ComboBoxItem Content=\""Anaglyph\"" IsSelected=\""True\""></ComboBoxItem>
                <ComboBoxItem Content=\""None\""></ComboBoxItem>
            </ComboBox>
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
