using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public class NavigationBehavior : TriggerAction<FrameworkElement>
    {
        [Category("NavigationBehavior Properties"),
        Description("The URI that should be navigated to when this behavior executes.")]
        public Uri NavigationUri
        {
            get { return (Uri)GetValue(NavigationUriProperty); }
            set { SetValue(NavigationUriProperty, value); }
        }

        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.Register("NavigationUri",
                                        typeof(Uri),
                                        typeof(NavigationBehavior),
                                        new PropertyMetadata(NavigationUriPropertyChanged));

        private static void NavigationUriPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var navigationBehavior = (NavigationBehavior)d;

            if (navigationBehavior.NavigationUri != null && navigationBehavior.NavigationUri.OriginalString.Trim().First() != '/')
            {
                Uri newNavigationUri;
                if (Uri.TryCreate("/" + navigationBehavior.NavigationUri.OriginalString, UriKind.Relative, out newNavigationUri))
                {
                    navigationBehavior.NavigationUri = newNavigationUri;
                }
            }
        }

        protected override void Invoke(object parameter)
        {
            var page = base.AssociatedObject.GetVisualParent<Page>();

            if (page != null && NavigationUri != null)
            {
                page.NavigationService.Navigate(NavigationUri);
            }
        }
    }
}
