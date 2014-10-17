using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using Microsoft.Web.Media.Diagnostics;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Static class responsible for polling trace entries from SSME, reporting system analytics, and distilling the information into SmoothStreamingEvent objects.
    /// </summary>
    public static class TraceMonitor
    {
        /// <summary>
        /// Reports the actual trace logs as they are processed.
        /// </summary>
        public static event Action<IEnumerable<TraceEntry>> ReportTraceLog;

        /// <summary>
        /// Reports system information the is periodically retrieved.
        /// </summary>
        public static event Action<SmoothStreamingEvent> ReportSystemEvent;

        /// <summary>
        /// Reports relevant trace information that has been distilled into a SmoothStreamingEvent object.
        /// </summary>
        public static event Action<SmoothStreamingEvent> ReportTraceEvent;

        /// <summary>
        /// Provides a callback on the UI thread that occurs at the regular polling interval.
        /// </summary>
        public static event Action Pulse;

        /// <summary>
        /// Provides a callback on the background thread that occurs at the regular polling interval.
        /// </summary>
        public static event Action PulseBackground;

        const string DefaultTracingConfigResourceName = "/Microsoft.SilverlightMediaFramework.Diagnostics;component/TracingConfig.xml";

        static Analytics analytics;
        static DispatcherTimer timer;
        static TraceDataClient traceClient;
        static BackgroundWorker worker;
        static bool IsInitialized = false;
        static TracingConfig config;
        public static TracingConfig Config
        {
            get
            {
                return config;
            }
        }

        /// <summary>
        /// Required to initialize the TraceMonitor. Once this is called you can call .Start()
        /// </summary>
        /// <param name="Config">Required information about how the trace monitor should operate.</param>
        public static void Init(TracingConfig Config)
        {
            if (!IsInitialized)
            {
                if (System.Windows.Deployment.Current.CheckAccess())
                {
                    IsInitialized = true;
                    config = Config;

                    Tracing.Initialize();
                    if (Config.TracingConfigFile == null)
                    {
                        using (XmlReader reader = XmlReader.Create(DefaultTracingConfigResourceName))
                            Tracing.ReadTraceConfig(reader);
                    }
                    else
                    {
                        using (XmlReader reader = XmlReader.Create(Config.TracingConfigFile))
                            Tracing.ReadTraceConfig(reader);
                    }

                    traceClient = new TraceDataClient();
                    traceClient.EventCreated += traceClient_EventCreated;
                    timer = new DispatcherTimer();
                    timer.Interval = Config.PollingInterval;
                    worker = new BackgroundWorker();
                    worker.DoWork += worker_DoWork;
                }
                else
                    throw new UnauthorizedAccessException("The TraceMonitor cannot be initialized on background thread. Call from the UI thread.");
            }
        }

        static void traceClient_EventCreated(object sender, SimpleEventArgs<SmoothStreamingEvent> e)
        {
            OnReportTraceEvent(e.Result);
        }

        static void InitializeAnalytics()
        {
            try
            {
                if (config.RecordCpuLoad && analytics == null)
                {
                    analytics = new Analytics();
                }
            }
            catch { /* ignore */ }
        }

        static void RecordCpuUsage()
        {
            try
            {
                InitializeAnalytics();
                if (config.RecordCpuLoad)
                {
                    float totalLoad = analytics.AverageProcessorLoad;
                    // workaround for Silverlight 3 known issue where CPU returns 0 sometimes
                    if (totalLoad > 0)
                    {
                        float processLoad = analytics.AverageProcessLoad;
                        SmoothStreamingEvent processLoadItem = new SmoothStreamingEvent()
                        {
                            Value = processLoad,
                            EventType = EventType.ProcessCpuLoad
                        };
                        OnReportSystemEvent(processLoadItem);

                        SmoothStreamingEvent systemLoaditem = new SmoothStreamingEvent()
                        {
                            Value = totalLoad,
                            EventType = EventType.SystemCpuLoad
                        };
                        OnReportSystemEvent(systemLoaditem);
                    }
                }
            }
            catch
            {
                // ignore this exception, it is a known issue in Silverlight 3.
                // http://blogs.msdn.com/silverlight_sdk/archive/2009/06/05/silverlight-3-bugs.aspx
            }
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            RecordCpuUsage();
            if (Pulse != null)
                Pulse();
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        // used to keep track of how many observers we have so we can turn off the timer when everyone stops listening
        static int observerCounter;
        static readonly object observerCounterLock = new object();

        /// <summary>
        /// Used to startup the trace monitor.
        /// This method is thread-safe.
        /// </summary>
        public static void Start()
        {
            if (!IsInitialized)
                throw new InvalidOperationException("Init must be called before the trace monitor can be started.");

            lock (observerCounterLock)
            {
                if (observerCounter == 0)
                {
                    InitializeAnalytics();
                    timer.Tick += timer_Tick;
                    timer.Start();
                }
                observerCounter++;
            }
        }

        /// <summary>
        /// Used to stop the trace monitor. A counter is used to keep track of when the trace monitor is no longer being used.
        /// This method is thread-safe.
        /// </summary>
        public static void Stop()
        {
            lock (observerCounterLock)
            {
                observerCounter--;
                if (observerCounter == 0)
                {
                    timer.Tick -= timer_Tick;
                    timer.Stop();
                    if (analytics != null) analytics = null;
                }
            }
        }

        static readonly object traceLock = new object();
        /// <summary>
        /// Used to peek at the raw trace entries without actually removing them.
        /// This method is thread-safe
        /// </summary>
        /// <param name="BeforePeek">Optional thread-safe action to run before the peek occurs.</param>
        /// <returns>All SSME tracelogs currently in the queue.</returns>
        public static TraceEntry[] PeekTraceEntries(Action BeforePeek)
        {
            lock (traceLock)
            {
                if (BeforePeek != null) BeforePeek();
                return Tracing.GetTraceEntries(false);
            }
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            TraceEntry[] entries;
            lock (traceLock)
            {
                entries = Tracing.GetTraceEntries(true) ?? new TraceEntry[] { };
            }
            // The TraceClient will raise an event to tell us about it. Event is will fire synchronously.
            traceClient.ParseTraceEntries(entries);

            if (PulseBackground != null)
                PulseBackground();

            if (ReportTraceLog != null)
                ReportTraceLog(entries);
        }

        static void OnReportSystemEvent(SmoothStreamingEvent e)
        {
            if (ReportSystemEvent != null)
                ReportSystemEvent(e);
        }

        static void OnReportTraceEvent(SmoothStreamingEvent e)
        {
            if (ReportTraceEvent != null)
                ReportTraceEvent(e);
        }

        static int currentStreamID = 1;
        static readonly object currentStreamIDLock = new object();
        /// <summary>
        /// Creates a new stream ID by incrementing a counter starting at 1.
        /// This method is thread-safe
        /// </summary>
        /// <returns>The new stream ID.</returns>
        public static int GenerateStreamIdentifier()
        {
            int ret = 0;
            lock (currentStreamIDLock)
            {
                ret = currentStreamID;
                currentStreamID++;
            }
            return ret;
        }
    }
}
