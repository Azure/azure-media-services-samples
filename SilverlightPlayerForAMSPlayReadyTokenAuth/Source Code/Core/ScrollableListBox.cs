using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// A specialized ListBox that supports scrolling.
    /// </summary>
    [TemplateVisualState(Name = ScrollableListBoxVisualStates.CanScrollLeftStates.CanScrollLeft, GroupName = ScrollableListBoxVisualStates.GroupNames.CanScrollLeftStates)]
    [TemplateVisualState(Name = ScrollableListBoxVisualStates.CanScrollLeftStates.CannotScrollLeft, GroupName = ScrollableListBoxVisualStates.GroupNames.CanScrollLeftStates)]
    [TemplateVisualState(Name = ScrollableListBoxVisualStates.CanScrollRightStates.CanScrollRight, GroupName = ScrollableListBoxVisualStates.GroupNames.CanScrollRightStates)]
    [TemplateVisualState(Name = ScrollableListBoxVisualStates.CanScrollRightStates.CannotScrollRight, GroupName = ScrollableListBoxVisualStates.GroupNames.CanScrollRightStates)]
    [TemplatePart(Name = ScrollableListBoxVisualElements.ForwardButtonElement, Type = typeof (RepeatButton))]
    [TemplatePart(Name = ScrollableListBoxVisualElements.BackButtonElement, Type = typeof (RepeatButton))]
    public class ScrollableListBox : ListBox
    {
        /// <summary>
        /// ScrollIncrement DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty ScrollIncrementProperty =
            DependencyProperty.Register("ScrollIncrement", typeof (double), typeof (ScrollableListBox), null);

        /// <summary>
        /// IncrementDelay DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IncrementDelayProperty =
            DependencyProperty.Register("IncrementDelay", typeof (TimeSpan), typeof (ScrollableListBox), null);

        private RepeatButton _backButtonElement;
        private RepeatButton _fowardButtonElement;
        private ScrollViewer _scrollViewer;

        public ScrollableListBox()
        {
            DefaultStyleKey = typeof (ScrollableListBox);
            Loaded += ScrollableListBox_Loaded;
            SizeChanged += ScrollableListBox_SizeChanged;
        }

        /// <summary>
        /// Gets or sets the size of the increment used when scrolling.
        /// </summary>
        public double ScrollIncrement
        {
            get { return (double) GetValue(ScrollIncrementProperty); }
            set { SetValue(ScrollIncrementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the speed of scrolling when the user is holding down the scroll button.
        /// </summary>
        public TimeSpan IncrementDelay
        {
            get { return (TimeSpan) GetValue(IncrementDelayProperty); }
            set { SetValue(IncrementDelayProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _scrollViewer = GetTemplateChild(ScrollableListBoxVisualElements.ScrollViewer) as ScrollViewer;
            _fowardButtonElement =
                GetTemplateChild(ScrollableListBoxVisualElements.ForwardButtonElement) as RepeatButton;
            _backButtonElement = GetTemplateChild(ScrollableListBoxVisualElements.BackButtonElement) as RepeatButton;

            if (_fowardButtonElement != null)
            {
                if (IncrementDelay > TimeSpan.Zero)
                {
                    _fowardButtonElement.Interval = (int) IncrementDelay.TotalMilliseconds;
                }

                _fowardButtonElement.Click += FowardButtonElement_Click;
            }

            if (_backButtonElement != null)
            {
                if (IncrementDelay > TimeSpan.Zero)
                {
                    _backButtonElement.Interval = (int) IncrementDelay.TotalMilliseconds;
                }

                _backButtonElement.Click += BackButtonElement_Click;
            }
        }

        private void ScrollableListBox_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollViewer.IfNotNull(i => UpdateVisualStates(i.HorizontalOffset));
        }

        private void ScrollableListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _scrollViewer.IfNotNull(i => UpdateVisualStates(i.HorizontalOffset));
        }

        private void UpdateVisualStates(double newPosition)
        {
            if (_scrollViewer == null || newPosition == 0)
            {
                this.GoToVisualState(ScrollableListBoxVisualStates.CanScrollLeftStates.CannotScrollLeft);
            }
            else
            {
                this.GoToVisualState(ScrollableListBoxVisualStates.CanScrollLeftStates.CanScrollLeft);
            }

            if (_scrollViewer == null || newPosition == _scrollViewer.ScrollableWidth)
            {
                this.GoToVisualState(ScrollableListBoxVisualStates.CanScrollRightStates.CannotScrollRight);
            }
            else
            {
                this.GoToVisualState(ScrollableListBoxVisualStates.CanScrollRightStates.CanScrollRight);
            }
        }

        private void BackButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (_scrollViewer != null)
            {
                double nextStep = _scrollViewer.HorizontalOffset - ScrollIncrement;
                nextStep = Math.Max(0, nextStep);
                _scrollViewer.ScrollToHorizontalOffset(nextStep);
                UpdateVisualStates(nextStep);
            }
        }

        private void FowardButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (_scrollViewer != null)
            {
                double nextStep = _scrollViewer.HorizontalOffset + ScrollIncrement;
                nextStep = Math.Min(_scrollViewer.ScrollableWidth, nextStep);
                _scrollViewer.ScrollToHorizontalOffset(nextStep);
                UpdateVisualStates(nextStep);
            }
        }
    }

    internal static class ScrollableListBoxVisualElements
    {
        public const string ScrollViewer = "ScrollViewer";
        public const string BackButtonElement = "BackButtonElement";
        public const string ForwardButtonElement = "ForwardButtonElement";
    }

    internal static class ScrollableListBoxVisualStates
    {
        #region Nested type: CanScrollLeftStates

        public static class CanScrollLeftStates
        {
            public const string CanScrollLeft = "CanScrollLeft";
            public const string CannotScrollLeft = "CannotScrollLeft";
        }

        #endregion

        #region Nested type: CanScrollRightStates

        public static class CanScrollRightStates
        {
            public const string CanScrollRight = "CanScrollRight";
            public const string CannotScrollRight = "CannotScrollRight";
        }

        #endregion

        #region Nested type: GroupNames

        public static class GroupNames
        {
            public const string CanScrollLeftStates = "CanScrollLeftStates";
            public const string CanScrollRightStates = "CanScrollRightStates";
        }

        #endregion
    }
}