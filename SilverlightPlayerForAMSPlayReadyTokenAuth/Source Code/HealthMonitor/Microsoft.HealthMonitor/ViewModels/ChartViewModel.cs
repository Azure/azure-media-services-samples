using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Microsoft.HealthMonitor.Models;

namespace Microsoft.HealthMonitor.ViewModels
{
	public class ChartViewModel : ObservableObject
	{
		ObservableCollection<double> data;
		SolidColorBrush lineColor;
        bool isOver;
		double maxValue;
		string qualityAttribute;
		string title;
		int currentValue;
		bool visible;

		public double Width { get; set; }
		public double Height { get; set; }
		public int MaxEntries {get; set;}
		public bool ValidIfStreamIdle { get; set; }

		public int CurrentValue
		{
			get
			{
				return currentValue;
			}
			set
			{
				if (currentValue != value)
				{
					currentValue = value;
					OnPropertyChanged(() =>CurrentValue);
				}
			}
		}

		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				if (visible != value)
				{
					visible = value;
					OnPropertyChanged(() =>Visible);
				}
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				if (title != value)
				{
					title = value;
					OnPropertyChanged(() =>Title);
				}
			}
		}

		public string QualityAttribute
		{
			get
			{
				return qualityAttribute;
			}
			set
			{
				if (qualityAttribute != value)
				{
					qualityAttribute = value;
					OnPropertyChanged(() =>QualityAttribute);
				}
			}
		}

		public double MaxValue
		{
			get
			{
				return maxValue;
			}
			set
			{
				if (maxValue != value)
				{
					maxValue = value;
					OnPropertyChanged(() =>MaxValue);
					OnPropertyChanged(() =>Points);
				}
			}
		}

		public SolidColorBrush LineColor
		{
			get
			{
				return lineColor;
			}
			set
			{
				if (lineColor != value)
				{
					lineColor = value;
					OnPropertyChanged(() =>LineColor);
				}
			}
		}

        public bool IsOver
        {
            get
            {
                return isOver;
            }
            set
            {
                if (isOver != value)
                {
                    isOver = value;
                    OnPropertyChanged(() => IsOver);
                }
            }
        }

		public ChartViewModel()
		{
			Data = new ObservableCollection<double>();
		}

		public ObservableCollection<double> Data
		{
			get
			{
				return data;
			}
			set
			{
				if (data != value)
				{
					data = value;
					OnPropertyChanged(() =>Data);
					OnPropertyChanged(() =>Points);
				}
			}
		}

        public void Clear() 
        {
            IsOver = false;
            CurrentValue = 0;
            Data.Clear();
            OnPropertyChanged(() => Data);
            OnPropertyChanged(() => Points);
        }

		public void AddDataPoint(double value)
		{
            CurrentValue = (int)value;
            IsOver = (value > maxValue);

            Data.Add(Math.Min(value, maxValue));
			while (Data.Count > MaxEntries)
			{
				Data.RemoveAt(0);
			}
			OnPropertyChanged(() =>Data);
			OnPropertyChanged(() =>Points);
		}

		public void SetSize(Size size)
		{
			Width = size.Width;
			Height = size.Height;
			OnPropertyChanged(() =>Points);
		}

		public PointCollection Points
		{
			get
			{
				PointCollection points = new PointCollection();
				double dx = (Width / (MaxEntries - 1));
				double x = Width - (Data.Count - 1) * dx;
				double y = 0;
				foreach (double d in Data)
				{
					y = Height - ((Height * (d / MaxValue))) * .95 - 2;
					y = Math.Min(Math.Max(y, 0), Height);
					points.Add(new Point(x, y));
					x+=dx;
				}
				return points;
			}
		}
	}
}
