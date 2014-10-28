using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using Microsoft.Web.Media.Diagnostics;
using Microsoft.Web.Media.SmoothStreaming;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// The primary class responsible for connecting to an instance of SSME and reporting diagnostic information.
    /// </summary>
    public class HealthMonitor : IDisposable
    {
        /// <summary>
        /// A generated ID used for the life of the HealthMonitor
        /// </summary>
        public Guid VideoSessionId { get; private set; }

        /// <summary>
        /// The Configuration data used to drive the HealthMonitor
        /// </summary>
        public DiagnosticsConfig Configuration { get; private set; }

        /// <summary>
        /// Indicates that the Latency has passed the threshold supplied in the config
        /// </summary>
        public event EventHandler<SimpleEventArgs<SmoothStreamingEvent>> LatencyAlert;

        /// <summary>
        /// Provides all simple diagnostic events.
        /// </summary>
        public event EventHandler<SimpleEventArgs<SmoothStreamingEvent>> EventCreated;

        /// <summary>
        /// Provides aggregated data as it is available.
        /// </summary>
        public event EventHandler<SimpleEventArgs<SampleData>> ReportSampledData;

        /// <summary>
        /// Provides the low level SSME trace logs. This is normally used only for diagnostic purposes.
        /// </summary>
        public event EventHandler<SimpleEventArgs<IEnumerable<TraceEntry>>> ReportTraceLogs;

        /// <summary>
        /// The client IP. This will be IPAddress.None (255.255.255.255) if no EdgeServerRules are provided in the config.
        /// </summary>
        public string ClientIP { get; private set; }

        /// <summary>
        /// The edge server used to host the current manifest. This will be 255.255.255.255 if no EdgeServerRules are provided in the config.
        /// </summary>
        public string EdgeServer { get; private set; }

        /// <summary>
        /// Indicates that the edge server and client IP are now ready for consumption... or no attempt is being made to detect them.
        /// If EdgeServerRules are supplied in the config, a video will start playback first and only after a subsequent async request to get this info completes, will the event fire.
        /// If no EdgeServerRules are provided in the config, this event will fire immediately when the manifest is loaded.
        /// </summary>
        public event EventHandler EdgeServerCompleted;

        /// <summary>
        /// A flag indicating whether or not the edge server detection process has completed (or is no attempt was made to detect it).
        /// If EdgeServerRules are supplied in the config, a video will start playback first and only after a subsequent async request to get this info completes, will this flag be set.
        /// If no EdgeServerRules are provided in the config, this flag will be set immediately when the manifest is loaded.
        /// </summary>
        public bool IsEdgeServerComplete { get; private set; }

        Queue<SmoothStreamingEvent> realtimeEventQueue = new Queue<SmoothStreamingEvent>();
        SmoothStreamingMediaElement mediaElement = null;
        SamplingAgent agent;
        int CurrentStreamId = 0;
        int CurrentClipId = 0;
        ulong PlaybackBitrate;
        double perceivedKBps = 0;
        string mediaElementId;
        bool startedPlay;

        public HealthMonitor(DiagnosticsConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            ClientIP = EdgeServerDataClient.IpNA;
            EdgeServer = EdgeServerDataClient.IpNA;
            VideoSessionId = Guid.NewGuid();

            Configuration = config;

            agent = new SamplingAgent();
            if (config.TrackQuality)
                agent.Agents.Add(new QualityAggregationAgent(config.AggregationInterval, config.QualityConfig));
            if (config.TrackQualitySnapshot)
                agent.Agents.Add(new QualitySnapshotAgent(config.SnapshotInterval, config.QualityConfig));
            if (config.TrackDownloadErrors)
                agent.Agents.Add(new DownloadErrorSampleAgent(Configuration));

            if (Configuration.InitTraceMonitor)
            {
                TraceMonitor.Init(Configuration.TracingConfig);
            }
        }

        /// <summary>
        /// Returns the total duration that the video has been playing (minus the paused and stopped periods of time).
        /// </summary>
        public TimeSpan VideoSessionRunningTime
        {
            get
            {
                return agent.GetRunningDuration();
            }
        }

        /// <summary>
        /// Returns the total duration since the start of the video (doesn't exclude paused or stopped time).
        /// </summary>
        public TimeSpan VideoSessionTotalTime
        {
            get
            {
                return agent.GetTotalDuration();
            }
        }

        /// <summary>
        /// Forces all events in the queue to get flushed.
        /// </summary>
        public void Flush()
        {
            ProcessQueuedEntries(null);
        }

        /// <summary>
        /// Forces a trace log to occur and then analyzes the MediaElementId used. This is only necessary if SSME.Name is not set.
        /// </summary>
        string GetMediaElementId()
        {
            TraceEntry[] trace = TraceMonitor.PeekTraceEntries(mediaElement.RequestLog);
            var log = trace.LastOrDefault(t => t.MethodName == "RequestLog");
            if (log != null)
                return log.MediaElementId;
            else
                return null;    // the only way that this could happen is if someone else called Tracing.GetTraceEntries(true) on another thread
        }

        /// <summary>
        /// The callback from the tracemonitor at it regular polling interval (always running on the UI thread)
        /// </summary>
        void TraceMonitor_Pulse()
        {
            RecordFrameData();
        }

        /// <summary>
        /// The callback from the tracemonitor at it regular polling interval (always running on a background thread)
        /// </summary>
        void TraceMonitor_PulseBackground()
        {
            ProcessQueuedEntries(null);

            foreach (var sample in agent.GetSampleData())
            {
                sample.MediaElementId = mediaElementId;
                sample.IsLive = mediaElement.IsLive;
                sample.CurrentClipId = CurrentClipId;
                sample.CurrentStreamId = CurrentStreamId;
                sample.EdgeIP = EdgeServer;
                ReportSampledData(this, new SimpleEventArgs<SampleData>(sample));
            }
        }

        void TraceMonitor_ReportSystemEvent(SmoothStreamingEvent traceEvent)
        {
            Enqueue(traceEvent);
        }

        void TraceMonitor_ReportTraceEvent(SmoothStreamingEvent traceEvent)
        {
            if (mediaElementId == null || traceEvent.MediaElementId == null || traceEvent.MediaElementId == mediaElementId)
                Submit(traceEvent);
        }

        void TraceMonitor_ReportTraceLog(IEnumerable<TraceEntry> traceEntries)
        {
            // filter out all tracelogs that are not related to the SSME we are responsible for tracking
            IEnumerable<TraceEntry> relevantEntries;
            if (mediaElementId == null)
                relevantEntries = traceEntries;
            else
                relevantEntries = traceEntries.Where(traceEvent => (traceEvent.MediaElementId ?? mediaElementId) == mediaElementId);

            if (ReportTraceLogs != null)
                ReportTraceLogs(this, new SimpleEventArgs<IEnumerable<TraceEntry>>(relevantEntries));
        }

        void ProcessQueuedEntries(SmoothStreamingEvent entry)
        {
            lock (realtimeEventQueue)
            {
                while (realtimeEventQueue.Count > 0 && (entry == null || realtimeEventQueue.Peek().Ticks < entry.Ticks))
                {
                    SmoothStreamingEvent queuedEntry = realtimeEventQueue.Dequeue();
                    if (queuedEntry.EventType == EventType.ClipStarted)
                    {
                        CurrentClipId = queuedEntry.ClipId;
                        if (entry != null) entry.ClipId = queuedEntry.ClipId;
                    }
                    else if (queuedEntry.EventType == EventType.StreamLoaded)
                    {
                        CurrentStreamId = TraceMonitor.GenerateStreamIdentifier();
                        queuedEntry.StreamId = CurrentStreamId;
                        if (entry != null) entry.StreamId = CurrentStreamId;
                    }
                    else
                    {
                        queuedEntry.ClipId = CurrentClipId;
                        queuedEntry.StreamId = CurrentStreamId;
                    }
                    AddEvent(queuedEntry);
                }
            }
        }

        void AddEvent(SmoothStreamingEvent entry)
        {
            // check for a latency alert
            if (entry.EventType == EventType.PerceivedBandwidth)
            {
                perceivedKBps = entry.Value / (8192);
            }
            else if (entry.EventType == EventType.VideoChunkDownload || entry.EventType == EventType.AudioChunkDownload)
            {
                if (entry.Value > 75)
                {
                    double dataSize = double.Parse(entry.Data3, CultureInfo.CurrentCulture) / 1024;
                    double observedThroughput = dataSize / ((entry.Value - 75) / 1000);
                    if (observedThroughput < (perceivedKBps / Configuration.LatencyAlertThreshold))
                    {
                        if (LatencyAlert != null) LatencyAlert(this, new SimpleEventArgs<SmoothStreamingEvent>(entry));
                    }
                }
            }

            // give the agents an opportunity to care about this event
            agent.ProcessEvent(entry);
            if (EventCreated != null) EventCreated(this, new SimpleEventArgs<SmoothStreamingEvent>(entry));
        }

        void Submit(SmoothStreamingEvent entry)
        {
            StampEvent(entry);
            ProcessQueuedEntries(entry);
            AddEvent(entry);
        }

        void Enqueue(SmoothStreamingEvent entry)
        {
            StampEvent(entry);
            lock (realtimeEventQueue)
            {
                realtimeEventQueue.Enqueue(entry);
            }
        }

        void StampEvent(SmoothStreamingEvent entry)
        {
            entry.IsLive = mediaElement.IsLive;
            entry.MediaElementId = mediaElementId;
        }

#if !WINDOWS_PHONE && !FULLSCREEN
        void Content_FullScreenChanged(object sender, EventArgs e)
        {
            bool isFullScreen = Application.Current.Host.Content.IsFullScreen;
            SmoothStreamingEvent entry = new SmoothStreamingEvent();
            entry.EventType = EventType.FullScreenChanged;
            if (isFullScreen)
            {
                entry.Value = 1;
                entry.Data1 = "True";
            }
            else
            {
                entry.Value = 0;
                entry.Data1 = "False";
            }
            Enqueue(entry);
        }
#endif

        void RecordFrameData()
        {
            Enqueue(new SmoothStreamingEvent()
            {
                Value = mediaElement.DroppedFramesPerSecond,
                EventType = EventType.DroppedFramesPerSecond
            });
            Enqueue(new SmoothStreamingEvent()
            {
                Value = mediaElement.RenderedFramesPerSecond,
                EventType = EventType.RenderedFramesPerSecond
            });
        }

        void AttachEvents()
        {
            mediaElement.PlaybackTrackChanged += new EventHandler<TrackChangedEventArgs>(mediaElement_PlaybackTrackChanged);
            mediaElement.ClipProgressUpdate += new EventHandler<ClipPlaybackEventArgs>(mediaElement_ClipProgressUpdate);
#if !WINDOWS_PHONE && !FULLSCREEN
            Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);
#endif
            mediaElement.MediaEnded += new RoutedEventHandler(mediaElement_MediaEnded);
            mediaElement.SmoothStreamingErrorOccurred += new EventHandler<SmoothStreamingErrorEventArgs>(mediaElement_SmoothStreamingErrorOccurred);
            mediaElement.CurrentStateChanged += new RoutedEventHandler(mediaElement_CurrentStateChanged);
            if (mediaElement.ManifestInfo == null)
            {
                mediaElement.ManifestReady += new EventHandler<EventArgs>(mediaElement_ManifestReady);
            }
            else
            {
                mediaElement_ManifestReady(mediaElement, EventArgs.Empty);
            }

            TraceMonitor.ReportTraceEvent += TraceMonitor_ReportTraceEvent;
            TraceMonitor.ReportSystemEvent += TraceMonitor_ReportSystemEvent;
            TraceMonitor.Pulse += TraceMonitor_Pulse;
            TraceMonitor.PulseBackground += TraceMonitor_PulseBackground;
            if (Configuration.RecordTraceLogs)
                TraceMonitor.ReportTraceLog += TraceMonitor_ReportTraceLog;
        }

        void DetachEvents()
        {
            if (mediaElement != null)
            {
                mediaElement.PlaybackTrackChanged -= new EventHandler<TrackChangedEventArgs>(mediaElement_PlaybackTrackChanged);
#if !WINDOWS_PHONE && !FULLSCREEN
                Application.Current.Host.Content.FullScreenChanged -= new EventHandler(Content_FullScreenChanged);
#endif
                mediaElement.ClipProgressUpdate -= new EventHandler<ClipPlaybackEventArgs>(mediaElement_ClipProgressUpdate);
                mediaElement.MediaEnded -= new RoutedEventHandler(mediaElement_MediaEnded);
                mediaElement.ManifestReady -= new EventHandler<EventArgs>(mediaElement_ManifestReady);
                mediaElement.SmoothStreamingErrorOccurred -= new EventHandler<SmoothStreamingErrorEventArgs>(mediaElement_SmoothStreamingErrorOccurred);
                mediaElement.CurrentStateChanged -= new RoutedEventHandler(mediaElement_CurrentStateChanged);
            }

            TraceMonitor.ReportTraceEvent -= TraceMonitor_ReportTraceEvent;
            TraceMonitor.ReportSystemEvent -= TraceMonitor_ReportSystemEvent;
            TraceMonitor.Pulse -= TraceMonitor_Pulse;
            TraceMonitor.PulseBackground -= TraceMonitor_PulseBackground;
            if (Configuration.RecordTraceLogs)
                TraceMonitor.ReportTraceLog -= TraceMonitor_ReportTraceLog;
        }

        void mediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (mediaElement.CurrentState)
            {
                case SmoothStreamingMediaElementState.Playing:
                    if (!startedPlay)
                    {
                        // we only create a log the very first time it happens for this stream
                        Enqueue(new SmoothStreamingEvent() { EventType = EventType.StreamStarted });
                        startedPlay = true;
                    }
                    Enqueue(new SmoothStreamingEvent() { EventType = Diagnostics.EventType.Playing });
                    break;
                case SmoothStreamingMediaElementState.Paused:
                    Enqueue(new SmoothStreamingEvent() { EventType = Diagnostics.EventType.Paused });
                    break;
            }
        }

        void mediaElement_SmoothStreamingErrorOccurred(object sender, SmoothStreamingErrorEventArgs e)
        {
            SmoothStreamingEvent mediaFailedEvent = new SmoothStreamingEvent();
            mediaFailedEvent.EventType = EventType.MediaFailed;
            mediaFailedEvent.Data1 = e.ErrorMessage;
            mediaFailedEvent.Data2 = GetUrlWithoutQueryString(mediaElement.SmoothStreamingSource);
            mediaFailedEvent.Data3 = mediaElement.Position.TotalSeconds.ToString(CultureInfo.CurrentCulture);
            Enqueue(mediaFailedEvent);
        }

        void mediaElement_ManifestReady(object sender, EventArgs e)
        {
            startedPlay = false;    // flag used to tell us when the video actually starts playing
            SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
            ssEvent.EventType = EventType.StreamLoaded;
            ssEvent.Data1 = GetUrlWithoutQueryString(mediaElement.SmoothStreamingSource);
            SmoothStreamingSourceChanged(ssEvent.Data1);
            foreach (SegmentInfo segment in mediaElement.ManifestInfo.Segments)
            {
                foreach (StreamInfo stream in segment.AvailableStreams)
                {
                    if (stream.Type == MediaStreamType.Video)
                    {
                        ulong bitrate = 0;
                        foreach (TrackInfo track in stream.AvailableTracks)
                        {
                            if (track.Bitrate > bitrate)
                            {
                                bitrate = track.Bitrate;
                            }
                        }
                        ssEvent.Data2 = bitrate.ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
            Enqueue(ssEvent);
        }

        void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
            ssEvent.EventType = EventType.StreamEnded;
            ssEvent.Data1 = GetUrlWithoutQueryString(mediaElement.SmoothStreamingSource);
            Enqueue(ssEvent);
        }

        void mediaElement_ClipProgressUpdate(object sender, ClipPlaybackEventArgs e)
        {
            if (e.Progress == ClipProgress.Start)
            {
                SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
                ssEvent.Data1 = e.Context.ClipInformation.ClipUri.ToString();
                ssEvent.EventType = EventType.ClipStarted;
                ssEvent.ClipId = TraceMonitor.GenerateStreamIdentifier();
                Enqueue(ssEvent);
            }
            else if (e.Progress == ClipProgress.Complete)
            {
                SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
                ssEvent.EventType = EventType.ClipEnded;
                ssEvent.Data1 = e.Context.ClipInformation.ClipUri.ToString();
                Enqueue(ssEvent);
            }
        }

        void mediaElement_PlaybackTrackChanged(object sender, TrackChangedEventArgs e)
        {
            if (e.NewTrack != null)
            {
                if (e.StreamType == MediaStreamType.Video && PlaybackBitrate != e.NewTrack.Bitrate)
                {
                    SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
                    ssEvent.Value = e.NewTrack.Bitrate;
                    ssEvent.Data1 = PlaybackBitrate.ToString(CultureInfo.InvariantCulture);
                    ssEvent.EventType = EventType.BitrateChanged;
                    Enqueue(ssEvent);
                    // remember value
                    PlaybackBitrate = e.NewTrack.Bitrate;
                }
            }
        }

        static string GetUrlWithoutQueryString(Uri uri)
        {
            string ret = uri.ToString();
            ret = ret.Substring(0, ret.Length - uri.Query.Length);
            return ret;
        }

        /// <summary>
        /// Connects a SmoothStreamingMediaElement that needs to be monitored
        /// </summary>
        public void Attach(SmoothStreamingMediaElement smoothStreamingMediaElement)
        {
            if (smoothStreamingMediaElement == null)
                throw new ArgumentNullException("smoothStreamingMediaElement");

            mediaElement = smoothStreamingMediaElement;
            if (!string.IsNullOrEmpty(mediaElement.Name))   // this allows TraceMonitor events to be filtered so we only capture ones associated with this instance of the SSME
                mediaElementId = mediaElement.Name;
            else
                mediaElementId = GetMediaElementId();

            AttachEvents();
            TraceMonitor.Start();
        }

        /// <summary>
        /// Disconnects the SmoothStreamingMediaElement that was passed to the Attach method
        /// </summary>
        public void Detach()
        {
            TraceMonitor.Stop();
            DetachEvents();
            if (agent != null)
                agent.Dispose();
            mediaElement = null;
        }

        void SmoothStreamingSourceChanged(string url)
        {
            Uri currentStreamUri = new Uri(url, UriKind.Absolute);
            IsEdgeServerComplete = false;
            EdgeServer = EdgeServerDataClient.IpNA;  // clear the current edge server
            ClientIP = EdgeServerDataClient.IpNA;

            if (Configuration.EdgeServerRuleCollection != null)
            {
                EdgeServerRules addressRules = Configuration.EdgeServerRuleCollection.FirstOrDefault(ai => ai.Domain != null && currentStreamUri.Host.EndsWith(ai.Domain, StringComparison.OrdinalIgnoreCase));
                // fallback on the address rules without a domain
                if (addressRules == null)
                    addressRules = Configuration.EdgeServerRuleCollection.FirstOrDefault(ai => ai.Domain == null);
                try
                {
                    if (addressRules != null)
                    {
                        EdgeServerDataClient edgeServerDataClient = new EdgeServerDataClient();
                        edgeServerDataClient.GetEdgeServerCompleted += (s, e) =>
                        {
                            // warning: this can come back after we've shut down, checking for the mediaelement is a good way to ensure we're still supposed to be tracking
                            if (mediaElement != null)
                            {
                                if (e.Result != null)
                                {
                                    EdgeServer = e.Result.EdgeServer;
                                    ClientIP = e.Result.ClientIP;
                                }
                                FinishSettingEdgeServer();

                                SmoothStreamingEvent entry = new SmoothStreamingEvent();
                                entry.EventType = EventType.AddressInfo;
                                entry.Data1 = ClientIP.ToString();
                                entry.Data2 = EdgeServer;
                                Enqueue(entry);
                            }
                        };
                        edgeServerDataClient.GetEdgeServerAsync(addressRules, currentStreamUri);
                    }
                    else
                        FinishSettingEdgeServer();
                }
                catch
                {
                    FinishSettingEdgeServer();
                }
            }
            else
                FinishSettingEdgeServer();
        }

        void FinishSettingEdgeServer()
        {
            IsEdgeServerComplete = true;
            if (EdgeServerCompleted != null)
                EdgeServerCompleted(this, EventArgs.Empty);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Detach();
            }
        }

        #endregion
    }
}
