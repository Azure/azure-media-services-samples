using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.SilverlightMediaFramework.Utilities.Converters
{
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var myTimeSpan = (TimeSpan) value;
                return myTimeSpan.TotalSeconds;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                TimeSpan myTimeSpan = TimeSpan.FromSeconds((double) value);
                return myTimeSpan;
            }
            return TimeSpan.Zero;
        }

        #endregion
    }
}