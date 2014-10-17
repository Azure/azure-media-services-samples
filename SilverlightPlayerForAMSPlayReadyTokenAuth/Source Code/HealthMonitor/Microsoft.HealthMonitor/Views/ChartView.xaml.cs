using System.Windows;
using System.Windows.Controls;
using Microsoft.HealthMonitor.ViewModels;

namespace Microsoft.HealthMonitor.Views
{
	public partial class ChartView : UserControl
	{
		public ChartView()
		{
			InitializeComponent();
		}

		private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			(DataContext as SuperChartViewModel).ChartSizeChanged(e.NewSize);
		}
	}
}
