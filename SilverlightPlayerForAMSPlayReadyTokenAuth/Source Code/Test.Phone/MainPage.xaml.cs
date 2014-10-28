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
using Microsoft.Phone.Controls;
using Microsoft.SilverlightMediaFramework.Core;

namespace Microsoft.SilverlightMediaFramework.Test.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += (s, e) => PlaylistItemSelector.SelectedItem = null;
            PlaylistItemSelector.SelectionChanged += PlaylistItemSelector_SelectionChanged;
        }

        private void PlaylistItemSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistItemSelector.SelectedItem != null)
            {
                NavigationService.Navigate(new Uri("/MediaDetailPage.xaml", UriKind.Relative));
            }
        }
    }
}