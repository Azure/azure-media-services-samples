using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.HealthMonitor.Helpers;

namespace Microsoft.HealthMonitor.Controls
{
    public class SlidingTabs : Control
    {
        int selectedTab = 1;
        bool isLoaded = false;
        double collapsedWidth1;
        double collapsedWidth2;

        internal System.Windows.Controls.Grid LayoutRoot;
        internal System.Windows.VisualStateGroup TabStates;
        internal System.Windows.VisualState Select1;
        internal System.Windows.VisualState Select2;
        internal System.Windows.Controls.Grid Tab1;
        internal System.Windows.Controls.ContentPresenter Tab1_Selected;
        internal System.Windows.Controls.Grid Tab1_Unselected;
        internal System.Windows.Controls.Grid Tab2;
        internal System.Windows.Controls.ContentPresenter Tab2_Selected;
        internal System.Windows.Controls.Grid Tab2_Unselected;
        internal System.Windows.Controls.Button Button1;
        internal System.Windows.Controls.Button Button2;

        public SlidingTabs()
        {
            this.DefaultStyleKey = typeof(SlidingTabs);
            this.SizeChanged += this_SizeChanged;
        }

        public static readonly DependencyProperty Tab1ContentProperty = DependencyProperty.Register("Tab1Content", typeof(object), typeof(SlidingTabs), null);
        public object Tab1Content
        {
            get { return (object)GetValue(Tab1ContentProperty); }
            set { SetValue(Tab1ContentProperty, value); }
        }

        public static readonly DependencyProperty Tab2ContentProperty = DependencyProperty.Register("Tab2Content", typeof(object), typeof(SlidingTabs), null);
        public object Tab2Content
        {
            get { return (object)GetValue(Tab2ContentProperty); }
            set { SetValue(Tab2ContentProperty, value); }
        }

        public static readonly DependencyProperty Button1ContentProperty = DependencyProperty.Register("Button1Content", typeof(object), typeof(SlidingTabs), null);
        public object Button1Content
        {
            get { return (object)GetValue(Button1ContentProperty); }
            set { SetValue(Button1ContentProperty, value); }
        }

        public static readonly DependencyProperty Button2ContentProperty = DependencyProperty.Register("Button2Content", typeof(object), typeof(SlidingTabs), null);
        public object Button2Content
        {
            get { return (object)GetValue(Button2ContentProperty); }
            set { SetValue(Button2ContentProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.GetTemplateChild("LayoutRoot")));
            this.TabStates = ((System.Windows.VisualStateGroup)(this.GetTemplateChild("TabStates")));
            this.Select1 = ((System.Windows.VisualState)(this.GetTemplateChild("Select1")));
            this.Select2 = ((System.Windows.VisualState)(this.GetTemplateChild("Select2")));
            this.Tab1 = ((System.Windows.Controls.Grid)(this.GetTemplateChild("Tab1")));
            this.Tab1_Selected = ((System.Windows.Controls.ContentPresenter)(this.GetTemplateChild("Tab1_Selected")));
            this.Tab1_Unselected = ((System.Windows.Controls.Grid)(this.GetTemplateChild("Tab1_Unselected")));
            this.Tab2 = ((System.Windows.Controls.Grid)(this.GetTemplateChild("Tab2")));
            this.Tab2_Selected = ((System.Windows.Controls.ContentPresenter)(this.GetTemplateChild("Tab2_Selected")));
            this.Tab2_Unselected = ((System.Windows.Controls.Grid)(this.GetTemplateChild("Tab2_Unselected")));
            this.Button1 = ((System.Windows.Controls.Button)(this.GetTemplateChild("Button1")));
            this.Button2 = ((System.Windows.Controls.Button)(this.GetTemplateChild("Button2")));

            Button1.Click += Button1_Click;
            Button2.Click += Button2_Click;
        }

        public ICommand PlayInternal { get; set; }
        public ICommand ConnectExternal { get; set; }
        
        void this_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!isLoaded)
            {
                Tab1_Unselected.Measure(e.NewSize);
                collapsedWidth1 = Tab1_Unselected.DesiredSize.Width;

                Tab2_Unselected.Measure(e.NewSize);
                collapsedWidth2 = Tab2_Unselected.DesiredSize.Width;

                Tab1_Unselected.Visibility = Visibility.Collapsed;
                Tab2_Selected.Visibility = Visibility.Collapsed;

                isLoaded = true;
            }

            var expandedWidth1 = e.NewSize.Width - collapsedWidth2;
            var expandedWidth2 = e.NewSize.Width - collapsedWidth1;

            var select1Storyboard = VisualStateHelper.FindStoryboard(LayoutRoot, "TabStates", "Select1");
            var expand1 = (DoubleAnimation)select1Storyboard.Children.FirstOrDefault(an => Storyboard.GetTargetName(an) == "Tab1" && Storyboard.GetTargetProperty(an).Path == "Width");
            expand1.To = expandedWidth1;
            var collapse2 = (DoubleAnimation)select1Storyboard.Children.FirstOrDefault(an => Storyboard.GetTargetName(an) == "Tab2" && Storyboard.GetTargetProperty(an).Path == "Width");
            collapse2.To = collapsedWidth2;

            var select2Storyboard = VisualStateHelper.FindStoryboard(LayoutRoot, "TabStates", "Select2");
            var expand2 = (DoubleAnimation)select2Storyboard.Children.FirstOrDefault(an => Storyboard.GetTargetName(an) == "Tab2" && Storyboard.GetTargetProperty(an).Path == "Width");
            expand2.To = expandedWidth2;
            var collapse1 = (DoubleAnimation)select2Storyboard.Children.FirstOrDefault(an => Storyboard.GetTargetName(an) == "Tab1" && Storyboard.GetTargetProperty(an).Path == "Width");
            collapse1.To = collapsedWidth1;

            Tab1_Selected.Width = expandedWidth1;
            Tab2_Selected.Width = expandedWidth2;

            if (selectedTab == 1)
            {
                Tab1.Width = expandedWidth1;
            }
            else if (selectedTab == 2)
            {
                Tab2.Width = expandedWidth2;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Select1", true);
            selectedTab = 1;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Select2", true);
            selectedTab = 2;
        }
    }
}
