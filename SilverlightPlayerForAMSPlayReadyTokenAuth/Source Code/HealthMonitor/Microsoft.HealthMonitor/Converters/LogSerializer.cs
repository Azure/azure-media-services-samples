using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Microsoft.HealthMonitor.Converters
{
    public class LogSerializer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return value;
            if (value is KeyValuePair<string, string>)
            {
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)value;
                return kvp.Key + " = " + kvp.Value;
            }
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
