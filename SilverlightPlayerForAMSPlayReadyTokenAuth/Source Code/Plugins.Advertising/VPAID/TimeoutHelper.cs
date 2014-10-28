using System;
using System.Windows.Threading;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Helper class that allows us to track a timeout for an action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TimeoutHelper<T> : IDisposable where T : EventArgs
    {
        DispatcherTimer timer;
        bool inProgress = false;

        public event EventHandler<TimerHandlerEventArgs<T>> Completed;

        public void Begin(Action Start, TimeSpan? Timeout)
        {
            if (Timeout.HasValue)
            {
                timer = new DispatcherTimer();
                timer.Tick += timer_Tick;
                timer.Interval = Timeout.Value;
                timer.Start();
            }

            inProgress = true;

            try
            {
                Start();
            }
            catch (Exception ex) 
            {
                Failed(ex);
            }
        }

        public void Complete(object sender, T e)
        {
            if (inProgress)
            {
                CleanupTimer();
                inProgress = false;
                if (Completed != null)
                    Completed(this, new TimerHandlerEventArgs<T>(e));
            }
        }

        public void Failed(Exception e)
        {
            if (inProgress)
            {
                CleanupTimer();
                inProgress = false;
                if (Completed != null)
                    Completed(this, new TimerHandlerEventArgs<T>(e));
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (inProgress)
            {
                CleanupTimer();
                inProgress = false;
                if (Completed != null)
                    Completed(this, new TimerHandlerEventArgs<T>());
            }
        }

        void CleanupTimer()
        {
            if (timer != null)
            {
                timer.Tick -= timer_Tick;
                timer.Stop();
                timer = null;
            }
        }

        public void Dispose()
        {
            if (inProgress)
            {
                CleanupTimer();
                inProgress = false;
            }
        }
    }

    internal class TimerHandlerEventArgs<T> : EventArgs where T : EventArgs
    {
        public TimerHandlerEventArgs(T Result)
        {
            result = Result;
        }

        public TimerHandlerEventArgs(Exception Error)
        {
            error = Error;
        }

        public TimerHandlerEventArgs()
        {
            error = new TimeoutException();
        }

        readonly T result;
        public T Result { get { return result; } }

        readonly Exception error;
        public Exception Error { get { return error; } }
    }
}
