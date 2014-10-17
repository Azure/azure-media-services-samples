using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Microsoft.SilverlightMediaFramework.Utilities.Offline
{
    public class OfflineImageSourceAttachedProperty
    {
        //1. Declare the attached property as static, readonly field in your class.   
        public static readonly DependencyProperty OfflineImageSourceProperty = DependencyProperty.RegisterAttached(
            "OfflineImageSource",
            typeof (Uri),
            typeof(OfflineImageSourceAttachedProperty),
            new PropertyMetadata(OfflineImageSourcePropertyChanged));

        public void SetOfflineImageSource(DependencyObject d, Uri offlineImageSource)
        {
            d.SetValue(OfflineImageSourceProperty, offlineImageSource);
        }

        public Uri GetOfflineImageSource(DependencyObject d)
        {
            return (Uri) d.GetValue(OfflineImageSourceProperty);
        }


        private static void OfflineImageSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var image = sender as Image;

            if (image != null)
            {
                var offlineImageSource = image.GetValue(OfflineImageSourceProperty) as Uri;

                if (offlineImageSource != null && offlineImageSource.Scheme == "is")
                {
                    IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForApplication();
                    string filename = offlineImageSource.GetIsolatedStorageFilename();
                    var file = userStore.OpenFile(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var source = new BitmapImage();
                    source.SetSource(file);
                    image.Source = source;
                }
                else
                {
                    image.Source = new BitmapImage(offlineImageSource);
                }
            }
        }

        
    }
}
