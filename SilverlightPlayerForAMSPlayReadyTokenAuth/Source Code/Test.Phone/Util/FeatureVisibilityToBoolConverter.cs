using System;
using System.Windows.Data;
using Microsoft.SilverlightMediaFramework.Core;

namespace Microsoft.SilverlightMediaFramework.Test.Phone.Util
{
    public class FeatureVisibilityToBoolConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool negate = false;

            if (parameter != null)
            {
                bool.TryParse(parameter.ToString(), out negate);
            }

            var result = (value is FeatureVisibility && ((FeatureVisibility) value) == FeatureVisibility.Visible);

            return !negate ? result : !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool negate = false;

            if (parameter != null)
            {
                bool.TryParse(parameter.ToString(), out negate);
            }

            var result = value != null && value is bool && (bool) value
                             ? FeatureVisibility.Visible
                             : FeatureVisibility.Hidden;

            return !negate ? result
                           : result == FeatureVisibility.Hidden
                                 ? FeatureVisibility.Visible
                                 : FeatureVisibility.Hidden;
        }

        #endregion
    }
}
