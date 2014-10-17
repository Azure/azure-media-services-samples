using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            VerifyProperty(propertyName);

            PropertyChangedEventHandler propertyChangedHandler = PropertyChanged;

            if (propertyChangedHandler != null)
            {
                if (Application.Current != null && Application.Current.RootVisual != null)
                {
                    Dispatcher currentDispatcher = Application.Current.RootVisual.Dispatcher;
                    if (currentDispatcher.CheckAccess() == false)
                    {
                        currentDispatcher.BeginInvoke(
                            () => propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName)));
                    }
                    else
                    {
                        propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
                    }
                }
            }
        }

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);

            if (property == null)
            {
                string message = string.Format("Invalid property: {0}", propertyName);
                throw new Exception(message);
            }
        }

        #endregion
    }
}