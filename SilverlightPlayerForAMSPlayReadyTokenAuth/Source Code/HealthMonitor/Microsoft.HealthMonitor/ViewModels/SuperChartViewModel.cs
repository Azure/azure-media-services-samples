using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.HealthMonitor.Models;

namespace Microsoft.HealthMonitor.ViewModels
{
    public class SuperChartViewModel : ObservableObject
    {
        ObservableCollection<ChartViewModel> charts;
        public ObservableCollection<ChartViewModel> Charts
        {
            get { return charts; }
            set
            {
                charts = value;
                OnPropertyChanged(() => Charts);
            }
        }

        List<ChartLineViewModel> chartLines;
        public List<ChartLineViewModel> ChartLines
        {
            get { return chartLines; }
            set
            {
                chartLines = value;
                OnPropertyChanged(() => ChartLines);
            }
        }

        public void ChartSizeChanged(Size size)
        {
            List<ChartLineViewModel> cl = new List<ChartLineViewModel>();
            double dy = ((size.Height * .95) - 2) / 5;
            double y = 2;
            for (int i = 0; i < 6; i++)
            {
                ChartLineViewModel cvm = new ChartLineViewModel()
                {
                    P1 = new Point(0, size.Height - y),
                    P2 = new Point(size.Width, size.Height - y)
                };
                cl.Add(cvm);
                y += dy;
            }
            ChartLines = cl;

            foreach (ChartViewModel chartVM in Charts)
                chartVM.SetSize(size);
        }
    }
}
