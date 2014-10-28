using System;
using System.Windows.Data;

namespace Microsoft.HealthMonitor.Converters
{
    public class TimespanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ts = (TimeSpan)value;
            return ts.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var seconds = (double)value;
            return TimeSpan.FromSeconds(seconds);
        }
    }
}
