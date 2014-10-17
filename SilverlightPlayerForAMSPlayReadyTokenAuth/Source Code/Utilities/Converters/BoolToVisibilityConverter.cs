using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.SilverlightMediaFramework.Utilities.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value is bool && ((bool) value)
                       ? Visibility.Visible
                       : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value is Visibility && ((Visibility) value) == Visibility.Visible;
        }

        #endregion
    }
}