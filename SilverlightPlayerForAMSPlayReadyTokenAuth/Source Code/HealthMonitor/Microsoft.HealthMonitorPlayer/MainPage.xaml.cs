using System;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Core.Javascript;

namespace Microsoft.HealthMonitorPlayer
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Bridge.Instance.PlayVideo += Instance_PlayVideo;
            Bridge.Instance.StopVideo += Instance_StopVideo;
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= MainPage_Loaded;
            Bridge.Instance.PluginReady();
        }

        void Instance_PlayVideo(object sender, ScriptEventArgs<string> e)
        {
            Player.PlayVideo(e.Result);
        }

        void Instance_StopVideo(object sender, EventArgs e)
        {
            Player.StopVideo();
        }
    }
}
