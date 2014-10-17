using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Samples.Framework;
using Microsoft.SilverlightMediaFramework.Samples.Samples.PlaylistItem;

namespace Microsoft.SilverlightMediaFramework.Samples
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            vm = new MainPageViewModel();
            this.DataContext = vm;
            trv.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(trv_SelectedItemChanged);
            this.sourceCodeExpander.Collapsed +=
                (s, args) => LayoutRoot.RowDefinitions[2].Height = new GridLength(0.05, GridUnitType.Star);
            this.sourceCodeExpander.Expanded +=
                (s, args) => LayoutRoot.RowDefinitions[2].Height = new GridLength(0.45, GridUnitType.Star);

            trv.LayoutUpdated += new EventHandler(trv_LayoutUpdated);
        }


        void trv_LayoutUpdated(object sender, EventArgs e)
        {
            trv.LayoutUpdated -= new EventHandler(trv_LayoutUpdated);
            SelectPathIfMentionedInQueryString();
        }

        private Queue<string> _queueOfIndividualnodesToExpandAndSelectTheLast = null;
        private void SelectPathIfMentionedInQueryString()
        {
            if (HtmlPage.IsEnabled 
            && HtmlPage.Document.QueryString.ContainsKey("path"))
            {
                string expandThisPath = HtmlPage.Document.QueryString["path"];
                string[] listOfIndividualNodesToExpand = expandThisPath.Split('|');
                _queueOfIndividualnodesToExpandAndSelectTheLast = new Queue<string>(listOfIndividualNodesToExpand);
                ExpandNextItemInQueueAndSelectIfLastItem(trv.Items, trv.ItemContainerGenerator);
            } 
        }

        private void ExpandNextItemInQueueAndSelectIfLastItem(ItemCollection items, ItemContainerGenerator itemContainerGenerator)
        {
            var sampleName = _queueOfIndividualnodesToExpandAndSelectTheLast.Dequeue();
            var sample = items.FirstOrDefault(s => (
                        ((s is Sample) && ((Sample)s).Name == sampleName)
                        || ((s is Group) && ((Group)s).Name == sampleName))
                        );
            if (sample == null)
                return; 
            var container = (TreeViewItem)itemContainerGenerator.ContainerFromItem(sample);

                if (_queueOfIndividualnodesToExpandAndSelectTheLast.Count == 0)
                {
                    container.IsSelected = true;
                }
                else
                {
                    ExpandNextItemInQueueAndSelectIfLastItem(container.Items, container.ItemContainerGenerator);
                }
        }

        MainPageViewModel vm { get; set; }

        void trv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Sample)
                LoadSampleWithName((Sample)e.NewValue);
        }

        private void LoadSampleWithName(Sample SelectedSample)
        {
            vm.ActiveUI = SelectedSample.ActiveUI();

            var codeDisplay = vm.ActiveUI as ISupportCodeDisplay;
            if (codeDisplay != null)
            {
                vm.XamlCode = codeDisplay.XamlCode;
                vm.CSharpCode = codeDisplay.CSharpCode;
            }
            else
            {
                vm.XamlCode = string.Empty;
                vm.CSharpCode = string.Empty;
            }

            var blendInstructionsDisplay = vm.ActiveUI as ISupportBlendInstructions;
            if (blendInstructionsDisplay != null)
            {
                vm.BlendInstructions = blendInstructionsDisplay.BlendInstructions;
            }
            else
            {
                vm.BlendInstructions = string.Empty;
            }

            var htmlDisplay = vm.ActiveUI as ISupportHtmlDisplay;
            if (htmlDisplay != null)
            {
                vm.HtmlCode = htmlDisplay.HtmlCode;
            }
            else
            {
                vm.HtmlCode = string.Empty;
            }
            
            Hack_FixTabControlNotSelectingTheOnlyvisibleTabItem();
        }

        private void Hack_FixTabControlNotSelectingTheOnlyvisibleTabItem()
        {
            if (vm.BlendInstructionsVisiblity == Visibility.Visible)
                blendTabItem.IsSelected = true;
            else if (vm.HtmlVisibility == Visibility.Visible)
                htmlTabItem.IsSelected = true;
            else if (vm.XamlVisibility == Visibility.Visible)
                xamlTabItem.IsSelected = true;
            else if (vm.CSharpVisiblity == Visibility.Visible)
                csharpTabItem.IsSelected = true;
        }
    }

}
