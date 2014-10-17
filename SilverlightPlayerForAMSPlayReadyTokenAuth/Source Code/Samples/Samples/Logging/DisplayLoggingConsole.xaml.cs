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

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.Logging
{
    [SampleAttribute(GroupNames.Logging, "Display the Logging Console")]
    public partial class DisplayLoggingConsole : UserControl, ISupportCodeDisplay
    {
        public DisplayLoggingConsole()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (player == null) return;
            string state = (sender as RadioButton).Content.ToString();
            var visibility = (FeatureVisibility)Enum.Parse(typeof(FeatureVisibility), state, true);
            player.LoggingConsoleVisibility = visibility;
        }

        #region ISupportCodeDisplay Members

        public string CSharpCode
        {
            get
            {
                return @"public partial class DisplayLoggingConsole : UserControl
{
    public DisplayLoggingConsole()
    {
        InitializeComponent();
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (player == null) return;
        string state = (sender as RadioButton).Content.ToString();
        var visibility = (FeatureVisibility)Enum.Parse(typeof(FeatureVisibility), state, true);
        player.LoggingConsoleVisibility = visibility;
    }
}";
            }
        }

        public string XamlCode
        {
            get { return @"<Grid x:Name=""LayoutRoot"" Background=""White"">
    <Grid.RowDefinitions>
        <RowDefinition Height=""auto""/>
        <RowDefinition Height=""*""/>
    </Grid.RowDefinitions>
    <StackPanel Orientation=""Horizontal"">
        <TextBlock Text=""LoggingConsoleVisibility State: "" VerticalAlignment=""Center"" Margin=""5""/>
        <RadioButton IsChecked=""True"" Margin=""5"" GroupName=""DisplayLogging"" Content=""Hidden"" VerticalAlignment=""Center"" Checked=""RadioButton_Checked""/>
        <RadioButton Margin=""5"" GroupName=""DisplayLogging"" Content=""Visible"" VerticalAlignment=""Center"" Checked=""RadioButton_Checked""/>
        <RadioButton Margin=""5"" GroupName=""DisplayLogging"" Content=""Disabled"" VerticalAlignment=""Center"" Checked=""RadioButton_Checked""/>
    </StackPanel>
    <smf:SMFPlayer Grid.Row=""1"" x:Name=""player"" LogLevel=""All"" LoggingConsoleVisibility=""Hidden"">
        <smf:SMFPlayer.Playlist>
            <smfm:PlaylistItem DeliveryMethod=""AdaptiveStreaming"" MediaSource=""http://ecn.channel9.msdn.com/o9/content/smf/smoothcontent/elephantsdream/Elephants_Dream_1024-h264-st-aac.ism/manifest""/>
        </smf:SMFPlayer.Playlist>
    </smf:SMFPlayer>
</Grid>
"; }
        }

        #endregion
    }
}
