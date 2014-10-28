using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Microsoft.HealthMonitor.Data;
using Microsoft.HealthMonitor.Helpers;
using Microsoft.HealthMonitor.Models;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;
using Ionic.Zip;

namespace Microsoft.HealthMonitor.ViewModels
{
    public class MainViewModel : ObservableObject, IDisposable
    {
        public ObservableCollection<TraceLog> TraceLogs { get; private set; }
        public ObservableCollection<Log> Logs { get; private set; }
        public SuperChartViewModel ChartViewModel { get; private set; }

        LocalConnectionDataClient client;

        public MainViewModel()
        {
            TraceLogs = new ObservableCollection<TraceLog>();
            Logs = new ObservableCollection<Log>();
            ChartViewModel = new SuperChartViewModel();
            ChartViewModel.Charts = new ObservableCollection<ChartViewModel>();
            ChartViewModel.ChartLines = new List<ChartLineViewModel>();

            AddCharts(ChartViewModel.Charts);

            SavedSources = new ObservableCollection<string>();
            foreach (string source in SavedSourcesDataClient.Load().Distinct())
                SavedSources.Add(source);

            SavedPlayers = new ObservableCollection<string>();
            foreach (string player in SavedPlayersDataClient.Load().Distinct())
                SavedPlayers.Add(player);

            connectExternalCommand = new DelegateCommand<string>(ConnectExternal);
            playInternalCommand = new DelegateCommand<string>(SetStreamingSource);
            saveTraceCommand = new DelegateCommand(SaveTrace);
            saveLogsCommand = new DelegateCommand(SaveLogs);

            LoggingService.Current.LogReceived += embeddedLogger_LogReceived;

            Instruction = "Enter a stream URL above or connect to an external player.";
        }

        void ResetChartViewModel()
        {
            foreach (var vm in ChartViewModel.Charts)
                vm.Clear();
        }

        static void AddCharts(IList<ChartViewModel> Charts)
        {
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Green),
                MaxEntries = 90,
                MaxValue = 100,
                QualityAttribute = VideoLogAttributes.ProcessCPULoad,
                ValidIfStreamIdle = true,
                Title = "CPU Load",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Red),
                MaxEntries = 90,
                MaxValue = 3500000,
                QualityAttribute = VideoLogAttributes.BitRate,
                Title = "Bitrate",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Brown),
                MaxEntries = 90,
                MaxValue = 30,
                QualityAttribute = VideoLogAttributes.RenderedFrames,
                Title = "FPS",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Orange),
                MaxEntries = 90,
                MaxValue = 35000,
                QualityAttribute = VideoLogAttributes.AudioBufferSize,
                Title = "Audio Buffer",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Purple),
                MaxEntries = 90,
                MaxValue = 35000,
                QualityAttribute = VideoLogAttributes.VideoBufferSize,
                Title = "Video Buffer",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Blue),
                MaxEntries = 90,
                MaxValue = 5000,
                QualityAttribute = VideoLogAttributes.BufferingMilliseconds,
                Title = "Buffering (ms)",
                Visible = true
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Magenta),
                MaxEntries = 90,
                MaxValue = 10 * 1024,
                QualityAttribute = VideoLogAttributes.PerceivedBandwidth,
                Title = "Perceived Bandwidth (KBps)",
                Visible = false
            });
            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Black),
                MaxEntries = 90,
                MaxValue = 2000,
                QualityAttribute = VideoLogAttributes.VideoDownloadLatencyMilliseconds,
                Title = "Video Latency (ms)",
                Visible = false
            });

            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.DarkGray),
                MaxEntries = 90,
                MaxValue = 2000,
                QualityAttribute = VideoLogAttributes.AudioDownloadLatencyMilliseconds,
                Title = "Audio Latency (ms)",
                Visible = false
            });

            Charts.Add(new ChartViewModel()
            {
                LineColor = new SolidColorBrush(Colors.Yellow),
                MaxEntries = 90,
                MaxValue = 10,
                QualityAttribute = VideoLogAttributes.HttpErrorCount,
                Title = "Download Errors",
                Visible = false
            });
        }

        ObservableCollection<string> savedPlayers;
        public ObservableCollection<string> SavedPlayers
        {
            get { return savedPlayers; }
            set
            {
                savedPlayers = value;
                OnPropertyChanged(() => SavedPlayers);
            }
        }

        ObservableCollection<string> savedSources;
        public ObservableCollection<string> SavedSources
        {
            get { return savedSources; }
            set
            {
                if (savedSources != value)
                {
                    savedSources = value;
                    OnPropertyChanged(() => SavedSources);
                }
            }
        }

        string instruction;
        public string Instruction
        {
            get { return instruction; }
            set
            {
                instruction = value;
                OnPropertyChanged(() => Instruction);
            }
        }

        string clientIP;
        public string ClientIP
        {
            get
            {
                return clientIP;
            }
            set
            {
                if (value != clientIP)
                {
                    clientIP = value;
                    OnPropertyChanged(() => ClientIP);
                }
            }
        }

        string edgeServerIP;
        public string EdgeServerIP
        {
            get
            {
                return edgeServerIP;
            }
            set
            {
                if (value != edgeServerIP)
                {
                    edgeServerIP = value;
                    OnPropertyChanged(() => EdgeServerIP);
                }
            }
        }

        QualityDataViewModel qualityData = new QualityDataViewModel(new VideoQualityLog());
        public QualityDataViewModel QualityData
        {
            get
            {
                return qualityData;
            }
            set
            {
                if (qualityData != value)
                {
                    qualityData = value;
                    OnPropertyChanged(() => QualityData);
                }
            }
        }

        bool connected;
        public bool Connected
        {
            get
            {
                return connected;
            }
            set
            {
                if (connected != value)
                {
                    connected = value;
                    OnPropertyChanged(() => Connected);
                }
            }
        }

        string smoothStreamingSource;
        public string SmoothStreamingSource
        {
            get
            {
                return smoothStreamingSource;
            }
            set
            {
                if (smoothStreamingSource != value)
                {
                    smoothStreamingSource = value;
                    OnPropertyChanged(() => SmoothStreamingSource);
                }
            }
        }

        string smoothStreamingUrl;
        public string SmoothStreamingUrl
        {
            get
            {
                return smoothStreamingUrl;
            }
            set
            {
                if (smoothStreamingUrl != value)
                {
                    smoothStreamingUrl = value;
                    OnPropertyChanged(() => SmoothStreamingUrl);
                }
            }
        }

        public string LocalChannelName
        {
            get
            {
                if (HtmlPage.Document.GetElementById("defaultChannelName") != null)
                    return HtmlPage.Document.GetElementById("defaultChannelName").GetAttribute("value");
                else
                    return null;
            }
        }

        private void ConnectExternal(string channelName)
        {
            if (!SavedPlayers.Contains(channelName) && !string.IsNullOrEmpty(channelName))
            {
                SavedPlayers.Add(channelName);
                SavedPlayersDataClient.Save(SavedPlayers);
            }

            OpenChannel(channelName);

            Instruction = "Connecting to external player...";
            StopVideo();
        }

        private void OpenChannel(string channelName)
        {
            if (client != null)
            {
                client.LogReceived -= externalLogger_LogReceived;
                client.Connected -= client_Connected;
                client.Dispose();
            }
            client = new LocalConnectionDataClient(channelName);
            client.LogReceived += externalLogger_LogReceived;
            client.Connected += client_Connected;
        }

        void externalLogger_LogReceived(object sender, LogReceivedEventArgs e)
        {
            ProcessEvent(e.Log);
        }

        void embeddedLogger_LogReceived(object sender, LogReceivedEventArgs e)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
                ProcessEvent(e.Log);
            else
                Deployment.Current.Dispatcher.BeginInvoke(() => ProcessEvent(e.Log));
        }

        private bool isPlayerEmbedded;
        public bool IsPlayerEmbedded
        {
            get { return isPlayerEmbedded; }
            set
            {
                if (isPlayerEmbedded != value)
                {
                    if (!isPlayerEmbedded && client != null)
                    {
                        client.Dispose();
                        client = null;
                    }

                    isPlayerEmbedded = value;
                    base.OnPropertyChanged(() => IsPlayerEmbedded);
                }
            }
        }

        public void SetStreamingSource(string source)
        {
            Logs.Clear();
            TraceLogs.Clear();
            ResetChartViewModel();

            SmoothStreamingSource = source;
            IsPlayerEmbedded = true;

            if (!SavedSources.Contains(source))
            {
                SavedSources.Add(source);
                SavedSourcesDataClient.Save(SavedSources);
            }
        }

        public void StopVideo()
        {
            Logs.Clear();
            TraceLogs.Clear();
            ResetChartViewModel();
            IsPlayerEmbedded = false;
            SmoothStreamingSource = null;
        }

        void client_Connected(object sender, EventArgs e)
        {
            Connected = true;
            if (!IsPlayerEmbedded)
                Instruction = "Connected to external player";
        }

        void ProcessEvent(Log log)
        {
            if (log.Type != VideoLogTypes.Trace && log.Type != VideoLogTypes.VideoQualitySnapshot)
            {
                // weed these out, there are way too many of them. This is consistent with what a logagent that sends to the server would do too.
                Logs.Add(log);
            }

            IEnumerable<ChartViewModel> charts = ChartViewModel.Charts;
            switch (log.Type)
            {
                case VideoLogTypes.VideoQuality:
                    QualityData = new QualityDataViewModel(log.CastLog<VideoQualityLog>());
                    break;
                case VideoLogTypes.VideoQualitySnapshot:
                    VideoQualitySnapshotLog qualityLog = log.CastLog<VideoQualitySnapshotLog>();

                    foreach (ChartViewModel chartVM in charts)
                    {
                        if (log.Data.ContainsKey(chartVM.QualityAttribute))
                        {
                            if (chartVM.QualityAttribute == VideoLogAttributes.PerceivedBandwidth)
                                chartVM.AddDataPoint(qualityLog.PerceivedBandwidth.GetValueOrDefault(0) / 1024);
                            else
                                chartVM.AddDataPoint(Convert.ToDouble(log.Data[chartVM.QualityAttribute]));
                        }
                    }

                    ClientIP = qualityLog.ClientIP.ToString();
                    EdgeServerIP = qualityLog.EdgeIP;
                    SmoothStreamingUrl = qualityLog.VideoUrl;
                    ProcessCPU = qualityLog.ProcessCPULoad.GetValueOrDefault(0);
                    SystemCPU = qualityLog.SystemCPULoad.GetValueOrDefault(0);

                    break;
                case VideoLogTypes.VideoStarted:
                case VideoLogTypes.VideoLoaded:
                    VideoLoadLog loadLog = log.CastLog<VideoLoadLog>();

                    var bitrateChartData = charts.First(vm => vm.QualityAttribute == VideoLogAttributes.BitRate);
                    if (loadLog.MaxBitRate.HasValue)
                        bitrateChartData.MaxValue = loadLog.MaxBitRate.Value;
                    ClientIP = loadLog.ClientIP.ToString();
                    EdgeServerIP = loadLog.EdgeIP;
                    SmoothStreamingUrl = loadLog.VideoUrl;
                    break;
                case VideoLogTypes.Trace:
                    TraceLogs.Add(log.CastLog<TraceLog>());
                    break;
            }
        }

        static ItemViewModel GetItemViewModel(Log log)
        {
            return new ItemViewModel(log.Type) { Items = GetItemViewModelChildren(log.Data) };
        }

        static List<object> GetItemViewModelChildren(IDictionary<string, string> nvps)
        {
            return nvps.Where(nvp => nvp.Value != null).Cast<object>().ToList();
        }

        int processCPU;
        public int ProcessCPU
        {
            get
            {
                return processCPU;
            }

            set
            {
                if (processCPU != value)
                {
                    processCPU = value;
                    OnPropertyChanged(() => ProcessCPU);
                }
            }
        }

        double systemCPU;
        public double SystemCPU
        {
            get
            {
                return systemCPU;
            }

            set
            {
                if (systemCPU != value)
                {
                    systemCPU = value;
                    OnPropertyChanged(() => SystemCPU);
                }
            }
        }

        public void SaveTrace()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Zip files (*.zip)|*.zip";
            if (dlg.ShowDialog().GetValueOrDefault(false))
            {
                using (Stream fileStream = dlg.OpenFile())
                {
                    using (MemoryStream traceStream = new MemoryStream())
                    {
                        WriteTraceLogs(traceStream);
                        traceStream.Seek(0, SeekOrigin.Begin);
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddEntry("trace.txt", traceStream);
                            zip.Save(fileStream);
                        }
                    }
                }
            }
        }

        public void SaveLogs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Zip files (*.zip)|*.zip";
            if (dlg.ShowDialog().GetValueOrDefault(false))
            {
                using (Stream fileStream = dlg.OpenFile())
                {
                    using (MemoryStream logStream = new MemoryStream())
                    {
                        WriteLogs(logStream);
                        logStream.Seek(0, SeekOrigin.Begin);
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddEntry("logs.txt", logStream);
                            zip.Save(fileStream);
                        }
                    }
                }
            }
        }

        void WriteTraceLogs(Stream outputStream)
        {
            var writer = new StreamWriter(outputStream);
            foreach (TraceLog item in TraceLogs)
            {
                writer.WriteLine(item.ToString());
            }
            writer.Flush();
        }

        void WriteLogs(Stream outputStream)
        {
            var writer = XmlWriter.Create(outputStream);
            writer.WriteStartElement("Logs");
            foreach (Log log in Logs)
            {
                log.Serialize(writer);
            }
            writer.WriteEndElement();
            writer.Flush();
        }

        DelegateCommand<string> playInternalCommand;
        public ICommand PlayInternalCommand
        {
            get
            {
                return playInternalCommand;
            }
        }

        DelegateCommand<string> connectExternalCommand;
        public ICommand ConnectExternalCommand
        {
            get
            {
                return connectExternalCommand;
            }
        }

        DelegateCommand saveTraceCommand;
        public ICommand SaveTraceCommand
        {
            get
            {
                return saveTraceCommand;
            }
        }

        DelegateCommand saveLogsCommand;
        public ICommand SaveLogsCommand
        {
            get
            {
                return saveLogsCommand;
            }
        }

        public void Dispose()
        {
            if (client != null)
                client.Dispose();
        }
    }
}
