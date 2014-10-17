using System;
using System.Windows.Threading;
using Microsoft.Logging.LocalConnection;
using Microsoft.SilverlightMediaFramework.Logging;

namespace Microsoft.HealthMonitor.Data
{
    public class LocalConnectionDataClient : IDisposable
    {
        DispatcherTimer timer = new DispatcherTimer();
        LocalConnectionMessageService svc;
        public event EventHandler<LogReceivedEventArgs> LogReceived;
        public event EventHandler<EventArgs> Connected;
        public LocalConnectionDataClient(string channelName)
        {
            svc = new LocalConnectionMessageService(false, channelName);
            svc.MessageReceived += svc_MessageReceived;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public bool IsConnected
        {
            get
            {
                return svc.Connected;
            }
        }

        void svc_MessageReceived(object sender, LogReceivedEventArgs e)
        {
            Log log = e.Log;
            if (LogReceived != null)
            {
                LogReceived(this, e);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (!svc.Connected)
            {
                svc.Connect();
                if (svc.Connected)
                {
                    timer.Stop();
                    if (Connected != null)
                    {
                        Connected(this, null);
                    }
                }
            }
            else
            {
                timer.Stop();
                if (Connected != null)
                {
                    Connected(this, null);
                }
            }
        }

        public void Update()
        {
            if (!svc.Connected)
            {
                if (!timer.IsEnabled) timer.Start();
            }
        }

        public void Dispose()
        {
            if (timer.IsEnabled)
                timer.Stop();

            timer.Tick -= timer_Tick;
            svc.MessageReceived -= svc_MessageReceived;
            svc.Dispose();
        }
    }
}
