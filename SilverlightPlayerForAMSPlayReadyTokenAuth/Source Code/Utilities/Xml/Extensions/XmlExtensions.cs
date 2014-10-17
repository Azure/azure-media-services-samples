using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace Microsoft.SilverlightMediaFramework.Utilities.Xml.Extensions
{
    public static class XmlExtensions
    {

        #region Extension Method: GetValue

        public static string GetValue(this XAttribute attribute, string defaultValue)
        {
            string value = attribute.GetValue();
            return value ?? defaultValue;
        }

        public static string GetValue(this XElement element, string defaultValue)
        {
            return (element != null) ? element.Value : null;
        }

        public static string GetValue(this XAttribute attribute)
        {
            return (attribute != null) ? attribute.Value : null;
        }

        public static string GetValue(this XElement element)
        {
            return (element != null) ? element.Value : null;
        }

        #endregion

        #region Extension Method: GetValueAsBoolean

        public static bool? GetValueAsBoolean(this XAttribute attribute)
        {
            bool b;

            return ((attribute != null) &&
                bool.TryParse(attribute.Value, out b)) ?
                b : (bool?)null;
        }

        public static bool? GetValueAsBoolean(this XElement element)
        {
            bool b;

            return ((element != null) &&
                bool.TryParse(element.Value, out b)) ?
                b : (bool?)null;
        }

        #endregion

        #region Extension Method: GetValueAsDouble

        public static double? GetValueAsDouble(this XAttribute attribute)
        {
            double d;

            return ((attribute != null) &&
                double.TryParse(attribute.Value, out d)) ?
                d : (double?)null;
        }

        public static double? GetValueAsDouble(this XElement element)
        {
            double d;

            return ((element != null) &&
                double.TryParse(element.Value, out d)) ?
                d : (double?)null;
        }

        #endregion

        #region Extension Method: GetValueAsDateTime

        public static DateTime? GetValueAsDateTime(this XElement element, IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            if (String.IsNullOrEmpty(element.Value) == false)
            {
                DateTime dateTime;
                if (DateTime.TryParse(element.Value, formatProvider, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
            }

            return null;
        }

        public static DateTime? GetValueAsDateTime(this XAttribute attribute, IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            if (String.IsNullOrEmpty(attribute.Value) == false)
            {
                DateTime dateTime;
                if (DateTime.TryParse(attribute.Value, formatProvider, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
            }

            return null;
        }

        #endregion

        #region Extension Method: GetValueAsInt32

        public static int? GetValueAsInt(this XAttribute attribute)
        {
            int i;

            return (attribute != null) &&
                int.TryParse(attribute.Value, out i) ?
                i : (int?)null;
        }

        public static int? GetValueAsInt(
            this XAttribute attribute,
            IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            int i;

            return (attribute != null) &&
                int.TryParse(attribute.Value,
                    NumberStyles.Integer,
                    formatProvider,
                    out i) ?
                i : (int?)null;
        }

        public static int? GetValueAsInt(this XElement element)
        {
            int i;

            return (element != null) &&
                int.TryParse(element.Value, out i) ?
                i : (int?)null;
        }

        public static int? GetValueAsInt(
            this XElement element,
            IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            int i;

            return (element != null) &&
                int.TryParse(
                    element.Value,
                    NumberStyles.Integer,
                    formatProvider,
                    out i) ?
                i : (int?)null;
        }

        #endregion

        #region Extension Method: GetValueAsInt64

        public static long? GetValueAsLong(this XAttribute attribute)
        {
            long result;

            return (attribute != null) &&
                long.TryParse(attribute.Value, out result) ?
                result : (long?)null;
        }

        public static long? GetValueAsLong(
            this XAttribute attribute,
            IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            long result;

            return (attribute != null) &&
                long.TryParse(attribute.Value,
                    NumberStyles.Integer,
                    formatProvider,
                    out result) ?
                result : (long?)null;
        }

        public static long? GetValueAsLong(this XElement element)
        {
            long result;

            return (element != null) &&
                long.TryParse(element.Value, out result) ?
                result : (long?)null;
        }

        public static long? GetValueAsLong(
            this XElement element,
            IFormatProvider formatProvider)
        {
            Debug.Assert(formatProvider != null, "formatProvider is null.");

            long result;

            return (element != null) &&
                long.TryParse(
                    element.Value,
                    NumberStyles.Integer,
                    formatProvider,
                    out result) ?
                result : (long?)null;
        }

        #endregion

        #region Extension Method: GetValueAsTimeSpan

        public static TimeSpan? GetValueAsTimeSpan(this XAttribute attribute)
        {
            TimeSpan result;
            return (TimeSpan.TryParse(attribute.GetValue(), out result)) ? result : (TimeSpan?)null;
        }

        public static TimeSpan? GetValueAsTimeSpan(this XElement element)
        {
            TimeSpan t;

            return (element != null) &&
                TimeSpan.TryParse(element.Value, out t) ?
                t : (TimeSpan?)null;
        }
        #endregion

        #region Extension Method: GetValueAsUri

        public static Uri GetValueAsUri(this XElement element)
        {
            Uri result;
            return (Uri.TryCreate(element.GetValue(), UriKind.RelativeOrAbsolute, out result)) ? result : null;
        }

        public static Uri GetValueAsUri(this XAttribute attribute)
        {
            Uri result;
            return (Uri.TryCreate(attribute.GetValue(), UriKind.RelativeOrAbsolute, out result)) ? result : null;
        }

        public static Uri GetValueAsUri(
            this XAttribute attribute,
            UriKind uriKind)
        {
            Uri u;
            return (attribute != null) &&
                Uri.TryCreate(attribute.Value, uriKind, out u) ?
                u : null;
        }

        #endregion
    }
}
