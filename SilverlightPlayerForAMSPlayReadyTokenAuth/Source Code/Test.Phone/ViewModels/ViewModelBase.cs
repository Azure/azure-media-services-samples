using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.SilverlightMediaFramework.Test.Phone.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && Application.Current != null && Application.Current.RootVisual != null)
            {
                Application.Current.RootVisual.Dispatcher.BeginInvoke(() =>
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}
