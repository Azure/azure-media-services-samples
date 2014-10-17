using System;
using System.Windows.Input;

namespace Microsoft.HealthMonitor.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private bool _isActive;

        private event EventHandler _isActiveChanged;

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public DelegateCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public event EventHandler IsActiveChanged
        {
            add { _isActiveChanged += value; }
            remove { _isActiveChanged -= value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value) return;
                _isActive = value;

                EventHandler handler = _isActiveChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public bool CanExecute()
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod();
        }

        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        public void RaiseCanExecuteChanged()
        {
            //OnCanExecuteChanged();
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _executeMethod;
        private readonly Func<T, bool> _canExecuteMethod;
        private bool _isActive;

        private event EventHandler _isActiveChanged;

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public DelegateCommand(Action<T> executeMethod)
        {
            _executeMethod = executeMethod;
        }

        public event EventHandler IsActiveChanged
        {
            add { _isActiveChanged += value; }
            remove { _isActiveChanged -= value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value) return;
                _isActive = value;

                EventHandler handler = _isActiveChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public bool CanExecute()
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod(default(T));
        }

        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod(default(T));
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod((T)parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            //OnCanExecuteChanged();
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
