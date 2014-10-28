using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Utilities.Converters
{
    public class TimeSpanTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof (string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof (TimeSpan);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            TimeSpan result;
            return value != null && TimeSpan.TryParse(value.ToString(), out result)
                       ? result
                       : (TimeSpan?) null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            return value != null ? value.ToString() : null;
        }
    }
}