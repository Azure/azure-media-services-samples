using System.ComponentModel;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.HealthMonitor.ViewModels;

namespace Microsoft.HealthMonitor.Views
{
    public partial class MainView : UserControl
    {
        public MainViewModel VM
        {
            get
            {
                return LayoutRoot.DataContext as MainViewModel;
            }
        }

        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        public MainView(string DefaultStreamUrl, bool AutoPlay) : this()
        {
            if (!string.IsNullOrEmpty(DefaultStreamUrl))
            {
                TextStreamUrl.Text = DefaultStreamUrl;
                if (AutoPlay)
                {
                    VM.PlayInternalCommand.Execute(DefaultStreamUrl);
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextPlayerId.Text == "[Leave empty to use default ID]")
                TextPlayerId.Text = "";
        }

        private void TextPlayerId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextPlayerId.Text == "")
                TextPlayerId.Text = "[Leave empty to use default ID]";
        }

        private void TextStreamUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonPlay.Command.Execute(ButtonPlay.CommandParameter);
            }
        }

        private void TextPlayerId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonConnect.Command.Execute(ButtonConnect.CommandParameter);
            }
        }

        private void TabControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // hack: this helps with performance when switching tabs. Without it, the datagrid is given infinite room and will draw every item once before it shows and after is hides. (i.e. no virtualization).
            TraceGrid.MaxHeight = e.NewSize.Height;
        }

        //private void TextStreamUrl_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    TextStreamUrl.SelectAll();
        //}
    }
}
