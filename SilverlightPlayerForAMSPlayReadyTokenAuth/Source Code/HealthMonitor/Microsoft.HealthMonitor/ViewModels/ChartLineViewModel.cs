using System.Windows;
using Microsoft.HealthMonitor.Models;

namespace Microsoft.HealthMonitor.ViewModels
{
	public class ChartLineViewModel: ObservableObject
	{
		Point p1;
		Point p2;
		
		public Point P1
		{
			get
			{
				return p1;
			}
			set
			{
				if (p1 != value)
				{
					p1 = value;
					OnPropertyChanged(() =>P1);
				}
			}
		}

		public Point P2
		{
			get
			{
				return p2;
			}
			set
			{
				if (p2 != value)
				{
					p2 = value;
					OnPropertyChanged(() =>P2);
				}
			}
		}
	}
}
