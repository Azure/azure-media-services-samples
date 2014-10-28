using System;
using System.Windows.Data;

namespace Microsoft.HealthMonitor.Converters
{
    public class RoundingConverter : IValueConverter
    {
        public int Digits { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var v = (double)value;
            return Math.Round(v, Digits);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
