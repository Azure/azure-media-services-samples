using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents a visual control used for displaying Markers.
    /// </summary>
    [TemplateVisualState(Name = MarkerControlVisualStates.MarkerTypes.Chapter, GroupName = MarkerControlVisualStates.GroupNames.MarkerTypes)]
    [TemplateVisualState(Name = MarkerControlVisualStates.MarkerTypes.Timeline, GroupName = MarkerControlVisualStates.GroupNames.MarkerTypes)]
    public class MarkerControl : Control
    {
        public MarkerControl()
        {
            DefaultStyleKey = typeof (MarkerControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var marker = DataContext as MediaMarker;
            if (marker != null)
            {
                this.GoToVisualState(marker.Type);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }

    internal static class MarkerControlVisualStates
    {
        #region Nested type: GroupNames

        internal static class GroupNames
        {
            internal const string MarkerTypes = "MarkerTypes";
        }

        #endregion

        #region Nested type: MarkerTypes

        internal static class MarkerTypes
        {
            internal const string Chapter = "chapter";
            internal const string Timeline = "timeline";
        }

        #endregion
    }
}