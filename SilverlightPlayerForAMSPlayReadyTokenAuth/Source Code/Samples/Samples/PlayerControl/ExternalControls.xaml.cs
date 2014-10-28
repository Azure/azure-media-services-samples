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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.PlayerControl
{
    [Sample(GroupNames.Player, "Player External Controls")]
    public partial class ExternalControls : UserControl, ISupportCodeDisplay
    {
        public ExternalControls()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cmd = (sender as Button).Content.ToString();
            switch (cmd)
            {
                case "Play":
                    player.Play();
                    break;
                case "Pause":
                    player.Pause();
                    break;
                case "Stop":
                    player.Stop();
                    break;
                case "Replay":
                    player.Replay();
                    break;
                case "Start FF":
                    player.StartFastForward();
                    break;
                case "Stop FF":
                    player.StopFastForward();
                    break;
                case "Start Rew":
                    player.StartRewind();
                    break;
                case "Stop Rew":
                    player.StopRewind();
                    break;
                case "Start SloMo":
                    player.StartSlowMotion();
                    break;
                case "Stop SloMo":
                    player.StopSlowMotion();
                    break;
                case "Next Chapter":
                    player.GoToNextChapter();
                    break;
                case "Prev Chapter":
                    player.GoToPreviousChapter();
                    break;
                case "Next Playlistitem":
                    player.GoToNextPlaylistItem();
                    break;
                case "Prev PlaylistItem":
                    player.GoToPreviousPlaylistItem();
                    break;
            }
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            int item = int.Parse(itemText.Text);
            player.GoToPlaylistItem(item);
        }

        public string CSharpCode
        {
            get
            {
                return @"public partial class ExternalControls : UserControl
{
    public ExternalControls()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string cmd = (sender as Button).Content.ToString();
        switch (cmd)
        {
            case ""Play"":
                player.Play();
                break;
            case ""Pause"":
                player.Pause();
                break;
            case ""Stop"":
                player.Stop();
                break;
            case ""Replay"":
                player.Replay();
                break;
            case ""Start FF"":
                player.StartFastForward();
                break;
            case ""Stop FF"":
                player.StopFastForward();
                break;
            case ""Start Rew"":
                player.StartRewind();
                break;
            case ""Stop Rew"":
                player.StopRewind();
                break;
            case ""Start SloMo"":
                player.StartSlowMotion();
                break;
            case ""Stop SloMo"":
                player.StopSlowMotion();
                break;
            case ""Next Chapter"":
                player.GoToNextChapter();
                break;
            case ""Prev Chapter"":
                player.GoToPreviousChapter();
                break;
            case ""Next Playlistitem"":
                player.GoToNextPlaylistItem();
                break;
            case ""Prev PlaylistItem"":
                player.GoToPreviousPlaylistItem();
                break;
        }
    }

    private void goButton_Click(object sender, RoutedEventArgs e)
    {
        int item = int.Parse(itemText.Text);
        player.GoToPlaylistItem(item);
    }
}
";
            }
        }

        public string XamlCode
        {
            get
            {
                return @"<Grid x:Name=""LayoutRoot"" Background=""White"">
    <Grid.RowDefinitions>
        <RowDefinition Height=""auto""/>
        <RowDefinition Height=""*""/>
    </Grid.RowDefinitions>
    <StackPanel>
        <StackPanel Orientation=""Horizontal"">
            <Button Content=""Play"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Stop"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Pause"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Replay"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Start FF"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Stop FF"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Start Rew"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Stop Rew"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Start SloMo"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Stop SloMo"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
        </StackPanel>
        <StackPanel Orientation=""Horizontal"">
            <Button Content=""Next Chapter"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Prev Chapter"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Next Playlistitem"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
            <Button Content=""Prev Playlistitem"" Click=""Button_Click"" Margin=""2"" Padding=""5""/>
        </StackPanel>
        <StackPanel Orientation=""Horizontal"">
            <TextBlock Text=""Go to Playlistitem:"" VerticalAlignment=""Center""/>
            <TextBox x:Name=""itemText"" Text=""0"" Margin=""3"" Width=""30""/>
            <Button x:Name=""goButton"" Content=""Go"" Click=""goButton_Click"" Margin=""2"" Padding=""5""/>
        </StackPanel>
    </StackPanel>
    <smf:SMFPlayer x:Name=""player"" Grid.Row=""1"">
        <smf:SMFPlayer.Playlist>
            <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest"" ThumbSource=""../../Assets/ElephentsDream.jpg"">
                <smfm:PlaylistItem.Chapters>
                    <smfm:Chapter Begin=""0:0:0"" Title=""Proog Oversees"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s1_proog_t.jpg"" Description=""The main character, Proog, looks over his invented world.""/>
                    <smfm:Chapter Begin=""0:0:27"" Title=""Emo in Danger"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s2_emo_t.jpg"" Description=""Emo seems oblivious to the imminent danger and Proog jumps in to help.""/>
                    <smfm:Chapter Begin=""0:01:30"" Title=""On the Run"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s3_both_t.jpg"" Description=""Proog and Emo hurry to try to get to a place of safety.""/>
                    <smfm:Chapter Begin=""0:02:31"" Title=""The Phone"" ThumbSource=""http://orange.blender.org/wp-content/themes/orange/images/media/gallery/s3_telephone_t.jpg"" Description=""Nothing is as it seems, even the phone.""/>
                </smfm:PlaylistItem.Chapters>
            </smfm:PlaylistItem>
            <smfm:PlaylistItem Title=""Progressive Download Video"" DeliveryMethod=""ProgressiveDownload"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/progressivecontent/wildlife.wmv"" ThumbSource=""../../Assets/ElephentsDream.jpg"" />
            <smfm:PlaylistItem Title=""Streaming Video"" DeliveryMethod=""Streaming""  MediaSource=""http://demo.com/StreamingSource"" ThumbSource=""../../Assets/ElephentsDream.jpg""/>
        </smf:SMFPlayer.Playlist>
    </smf:SMFPlayer>
</Grid>";
            }
        }
    }
}
