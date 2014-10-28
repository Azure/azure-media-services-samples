using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;

namespace Microsoft.SilverlightMediaFramework.Samples.LocalDiagnostics
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            LoggingService.Current.LogReceived += logger_LogReceived;
        }

        void logger_LogReceived(object sender, LogReceivedEventArgs e)
        {
            if (Dispatcher.CheckAccess())
                DoSomethingWithLog(e.Log);
            else
                Dispatcher.BeginInvoke(() => DoSomethingWithLog(e.Log));
        }

        void DoSomethingWithLog(Log log)
        {
            if (log is VideoQualitySnapshotLog)
            {
                VideoQualitySnapshotLog qualityLog = (VideoQualitySnapshotLog)log;
                double kbps = qualityLog.PerceivedBandwidth.GetValueOrDefault(0) / 1024;
                Bandwidth.Text = kbps.ToString() + " Kbps";
            }
        }
    }
}
