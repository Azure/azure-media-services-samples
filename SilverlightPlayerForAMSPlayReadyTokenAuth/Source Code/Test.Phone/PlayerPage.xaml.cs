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
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Test.Phone
{
    public partial class PlayerPage : PhoneApplicationPage
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            player.Dispose();
            base.OnNavigatedFrom(e);
        }

        private bool _isDetailsVisible;
        public bool IsDetailsVisible
        {
            get { return _isDetailsVisible; }
            set
            {
                if (_isDetailsVisible != value)
                {
                    _isDetailsVisible = value;

                    ConfigureSupportedOrientations();
                    var state = _isDetailsVisible
                                    ? PlayerPageVisualStates.DetailsVisibilityStates.DetailsVisible
                                    : PlayerPageVisualStates.DetailsVisibilityStates.DetailsNotVisible;
                    this.GoToVisualState(state);
                }
            }
        }

        private void ConfigureSupportedOrientations()
        {
            SupportedOrientations = IsDetailsVisible
                                        ? SupportedPageOrientation.PortraitOrLandscape
                                        : SupportedPageOrientation.Landscape;
        }

        private void DetailsDisplayElement_Click(object sender, RoutedEventArgs e)
        {
            IsDetailsVisible = !IsDetailsVisible;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (IsDetailsVisible)
            {
                IsDetailsVisible = false;
                e.Cancel = true;
            }

            base.OnBackKeyPress(e);
        }

        private void ChapterSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chapter = (Chapter)ChapterSelectionElement.SelectedItem;

            if (chapter != null)
            {
                IsDetailsVisible = false;
                ChapterSelectionElement.SelectedItem = null;
                player.GoToChapterItem(chapter);
            }
        }

        private void TimelineMarkerSelectionElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var timelineMarker = (TimelineMediaMarker) TimelineMarkerSelectionElement.SelectedItem;

            if (timelineMarker != null)
            {
                IsDetailsVisible = false;
                TimelineMarkerSelectionElement.SelectedItem = null;
                player.SeekToPosition(timelineMarker.Begin);
            }
        }
    }

    internal static class PlayerPageVisualStates
    {
        internal static class GroupNames
        {
            public const string DetailsVisibilityStates = "DetailsVisibilityStates";
        }

        internal static class DetailsVisibilityStates
        {
            public const string DetailsVisible = "DetailsVisible";
            public const string DetailsNotVisible = "DetailsNotVisible";
        }
    }
}