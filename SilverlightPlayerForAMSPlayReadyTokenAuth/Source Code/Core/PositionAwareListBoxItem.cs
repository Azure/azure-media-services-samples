using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Specialized control that exposes Visual States indicating it's position within a ListBox.
    /// </summary>
    [TemplateVisualState(Name = PositionAwareListBoxItemVisualStates.FirstStates.IsFirst, GroupName = PositionAwareListBoxItemVisualStates.GroupNames.FirstStates)]
    [TemplateVisualState(Name = PositionAwareListBoxItemVisualStates.FirstStates.IsNotFirst, GroupName = PositionAwareListBoxItemVisualStates.GroupNames.FirstStates)]
    [TemplateVisualState(Name = PositionAwareListBoxItemVisualStates.LastStates.IsLast, GroupName = PositionAwareListBoxItemVisualStates.GroupNames.LastStates)]
    [TemplateVisualState(Name = PositionAwareListBoxItemVisualStates.LastStates.IsNotLast, GroupName = PositionAwareListBoxItemVisualStates.GroupNames.LastStates)]
    public class PositionAwareListBoxItem : Control
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var itemsControl = this.GetVisualParent<ItemsControl>();

            if (itemsControl != null && itemsControl.ItemsSource != null && itemsControl.Items.Contains(DataContext))
            {
                int index = itemsControl.Items.ToList().IndexOf(DataContext);
                SetPosition(index, itemsControl.Items.Count);
            }
        }

        internal void SetPosition(int position, int totalCount)
        {
            string firstState = position == 0
                                    ? PositionAwareListBoxItemVisualStates.FirstStates.IsFirst
                                    : PositionAwareListBoxItemVisualStates.FirstStates.IsNotFirst;
            this.GoToVisualState(firstState);

            string lastState = position == totalCount - 1
                                   ? PositionAwareListBoxItemVisualStates.LastStates.IsLast
                                   : PositionAwareListBoxItemVisualStates.LastStates.IsNotLast;
            this.GoToVisualState(lastState);
        }
    }
}

internal static class PositionAwareListBoxItemVisualStates
{
    #region Nested type: FirstStates

    internal static class FirstStates
    {
        public const string IsFirst = "IsFirst";
        public const string IsNotFirst = "IsNotFirst";
    }

    #endregion

    #region Nested type: GroupNames

    internal static class GroupNames
    {
        public const string FirstStates = "FirstStates";
        public const string LastStates = "LastStates";
    }

    #endregion

    #region Nested type: LastStates

    internal static class LastStates
    {
        public const string IsLast = "IsLast";
        public const string IsNotLast = "IsNotLast";
    }

    #endregion
}