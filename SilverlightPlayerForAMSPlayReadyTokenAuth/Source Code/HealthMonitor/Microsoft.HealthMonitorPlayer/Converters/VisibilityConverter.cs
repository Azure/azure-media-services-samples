using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.HealthMonitorPlayer.Converters
{
	public class VisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			VisibilityConverterMode mode = GetMode(parameter);

			switch (mode)
			{
				case VisibilityConverterMode.VisibleIfTrue:
					return (bool)value ? Visibility.Visible : Visibility.Collapsed;

				case VisibilityConverterMode.VisibleIfNotTrue:
					return !(bool)value ? Visibility.Visible : Visibility.Collapsed;

				case VisibilityConverterMode.TrueIfVisible:
					return (Visibility)value == Visibility.Visible;

				case VisibilityConverterMode.TrueIfNotVisible:
					return (Visibility)value != Visibility.Visible;

				default:
					string message = string.Format("Invalid mode: {0}", mode);
					throw new Exception(message);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			VisibilityConverterMode mode = GetMode(parameter);

			switch (mode)
			{
				case VisibilityConverterMode.VisibleIfTrue:
					return (Visibility)value == Visibility.Visible;

				case VisibilityConverterMode.VisibleIfNotTrue:
					return (Visibility)value != Visibility.Visible;

				case VisibilityConverterMode.TrueIfVisible:
					return (bool)value ? Visibility.Visible : Visibility.Collapsed;

				case VisibilityConverterMode.TrueIfNotVisible:
					return !(bool)value ? Visibility.Visible : Visibility.Collapsed;

				default:
					string message = string.Format("Invalid mode: {0}", mode);
					throw new Exception(message);
			}
		}

		private static VisibilityConverterMode GetMode(object parameter)
		{
			if (parameter == null)
			{
				return VisibilityConverterMode.VisibleIfTrue;
			}
			else if (parameter is VisibilityConverterMode)
			{
				return (VisibilityConverterMode)parameter;
			}
			else
			{
				return (VisibilityConverterMode)Enum.Parse(typeof(VisibilityConverterMode), (string)parameter, false);
			}
		}
	}

	public enum VisibilityConverterMode
	{
		VisibleIfTrue,
		VisibleIfNotTrue,
		TrueIfVisible,
		TrueIfNotVisible
	}
}
