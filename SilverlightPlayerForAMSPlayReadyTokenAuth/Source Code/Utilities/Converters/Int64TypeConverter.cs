using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Utilities.Converters
{
    public class Int64TypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(long);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            long result;

            return value != null && long.TryParse(value.ToString(), out result)
                            ? result
                            : (long?) null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            return value != null ? value.ToString() : null;
        }
    }
}
