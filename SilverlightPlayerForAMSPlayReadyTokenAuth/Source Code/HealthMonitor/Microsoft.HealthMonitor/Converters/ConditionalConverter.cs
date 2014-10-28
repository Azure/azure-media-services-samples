using System;
using System.Windows.Data;

namespace Microsoft.HealthMonitor.Converters
{
    public class ConditionalConverter : IValueConverter
    {
        public string Condition { get; set; }
        public string Substitution { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return value;
            string stringValue = value.ToString();
            if (stringValue == Condition)
                return Substitution;
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
