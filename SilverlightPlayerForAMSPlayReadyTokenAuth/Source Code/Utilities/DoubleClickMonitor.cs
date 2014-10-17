using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public class DoubleClickMonitor : IDisposable
    {
        private UIElement _element;
        private DispatcherTimer _timer;

        public DoubleClickMonitor()
        {
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 200)
            };
            _timer.Tick += (s, e) => _timer.Stop();
        }

        public UIElement Element
        {
            get { return _element; }
            set
            {
                if (_element != null)
                {
                    _element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
                }

                _element = value;

                if (_element != null)
                {
                    _element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }

            if (_element != null)
            {
                _element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
                _element = null;
            }
        }

        #endregion

        public event Action<DoubleClickMonitor, UIElement> ElementDoubleClicked;

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                ElementDoubleClicked.IfNotNull(i => i(this, _element));
            }
            else
            {
                _timer.Start();
            }
        }
    }
}