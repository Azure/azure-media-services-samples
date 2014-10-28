using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Microsoft.SilverlightMediaFramework.Utilities.Offline
{
    public class OfflineProperties
    {
        public static readonly DependencyProperty OfflineImageSourceProperty = DependencyProperty.RegisterAttached(
                "OfflineImageSource",
                typeof(Uri),
                typeof(OfflineImageSourceAttachedProperty),
                new PropertyMetadata(OfflineImageSourcePropertyChanged));

        public static void SetOfflineImageSource(DependencyObject d, Uri offlineImageSource)
        {
            d.SetValue(OfflineImageSourceProperty, offlineImageSource);
        }

        public static Uri GetOfflineImageSource(DependencyObject d)
        {
            return (Uri)d.GetValue(OfflineImageSourceProperty);
        }


        private static void OfflineImageSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var image = sender as Image;

            if (image != null)
            {
                ImageSource source;
                var offlineImageSource = image.GetValue(OfflineImageSourceProperty) as Uri;


                if (offlineImageSource != null)
                {
                    try
                    {
                        if (offlineImageSource.Scheme == "is")
                        {
                            IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForApplication();
                            string filename = offlineImageSource.GetIsolatedStorageFilename();
                            var file = userStore.OpenFile(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                            var bitmapImage = new BitmapImage();
                            bitmapImage.SetSource(file);
                            source = bitmapImage;
                        }
                        else
                        {
                            source = new BitmapImage(offlineImageSource);
                        }
                    }
                    catch (Exception)
                    {
                        source = new BitmapImage(offlineImageSource);
                    }

                    image.Source = source;
                }
            }
        }

    }
}
