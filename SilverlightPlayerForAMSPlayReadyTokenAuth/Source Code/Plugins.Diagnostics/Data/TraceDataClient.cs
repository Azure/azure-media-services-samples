using System;
using System.Text.RegularExpressions;
using Microsoft.Web.Media.Diagnostics;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Responsible for parsing the SSME TraceEntries and turning them into SmoothStreamingEvent objects
    /// </summary>
    internal class TraceDataClient
    {
        public event EventHandler<SimpleEventArgs<SmoothStreamingEvent>> EventCreated;

        static SmoothStreamingEvent ConvertTraceToEvent(TraceEntry entry, EventType eventType, string regexString)
        {
            Regex regex = new Regex(regexString);
            if (regex.IsMatch(entry.Text))
            {
                SmoothStreamingEvent ssEvent = new SmoothStreamingEvent(entry);
                ssEvent.EventType = eventType;
                ssEvent.Message = entry.Text;
                ssEvent.MediaElementId = entry.MediaElementId;
                var matches = regex.Matches(entry.Text);
                var valueCapture = matches[0].Groups["v"].Captures;
                if (valueCapture.Count > 0)
                {
                    double value;
                    if (double.TryParse(valueCapture[0].Value, NumberStyles.None, CultureInfo.CurrentCulture, out value))
                        ssEvent.Value = value;
                    else
                        ssEvent.Value = 0;
                }
                var data1Capture = matches[0].Groups["d1"].Captures;
                if (data1Capture.Count > 0)
                {
                    ssEvent.Data1 = data1Capture[0].Value;
                }
                var data2Capture = matches[0].Groups["d2"].Captures;
                if (data2Capture.Count > 0)
                {
                    ssEvent.Data2 = data2Capture[0].Value;
                }
                var data3Capture = matches[0].Groups["d3"].Captures;
                if (data3Capture.Count > 0)
                {
                    ssEvent.Data3 = data3Capture[0].Value;
                }
                return ssEvent;
            }
            else
            {
                return null;
            }
        }

        void Submit(SmoothStreamingEvent entry)
        {
            if (entry != null)
            {
                if (EventCreated != null) EventCreated(this, new SimpleEventArgs<SmoothStreamingEvent>(entry));
            }
        }

        void ProcessBufferingEngineMessage(TraceEntry entry)
        {
            if (entry.MethodName == "AddChunkToCache")
            {
                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.VideoBufferSize, "Added (?<d2>.*) chunk with duration (?<d1>.*) to cache. Size including active chunk (?<v>.*) ms");
                if (ssEvent != null)
                {
                    if (ssEvent.Data2 == "Audio")
                    {
                        ssEvent.EventType = EventType.AudioBufferSize;
                    }
                    Submit(ssEvent);
                }
            }
            else if (entry.MethodName == "RaiseBufferChangedEvent")
            {
                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.BufferingStateChanged, "Rebuffer State: (?<d1>.), (?<d2>.*)");
                if (ssEvent != null)
                {
                    switch (ssEvent.Data1)
                    {
                        case "a":
                            ssEvent.Value = 0;
                            ssEvent.Data1 = "NotRebuffering";
                            break;
                        case "b":
                            ssEvent.Value = 1;
                            ssEvent.Data1 = "RebufferingDueToSeek";
                            break;
                        case "c":
                            ssEvent.Value = 1;
                            ssEvent.Data1 = "RebufferingDueToStartPlayback";
                            break;
                        case "d":
                            ssEvent.Value = 1;
                            ssEvent.Data1 = "RebufferingDueToUnderflow";
                            break;
                    }
                    Submit(ssEvent);
                }
            }
        }

        void SetPlaybackRate(long ticks, int rate)
        {
            SmoothStreamingEvent ssEvent = new SmoothStreamingEvent();
            ssEvent.Ticks = ticks;
            ssEvent.Value = rate;
            ssEvent.EventType = EventType.SetPlaybackRate;
            Submit(ssEvent);
        }

        /// <summary>
        /// Processes trace entries from the SSME and raises the EventCreated event for each relevant one
        /// </summary>
        /// <param name="entries">An array of SSME trace events to process</param>
        public void ParseTraceEntries(TraceEntry[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                TraceEntry entry = entries[i];

                if (entry.TraceLevel == TraceLevel.Warning || entry.TraceLevel == TraceLevel.Fatal
                    || entry.TraceLevel == TraceLevel.Error || entry.TraceLevel == TraceLevel.Shutdown)
                {
                    if (entry.TraceArea == TraceArea.BufferingEngine)
                    {
                        if (entry.MethodName == "HandleDownloadError")
                        {
#if SSME_1_5_1287_0
                            SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.DownloadError, "Download error for (?<d1>.*) chunk id (?<d2>.*) startTime (?<d3>.*) physicalStartTime (?<d4>.*) timeout = .*");
#else
                            SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.DownloadError, "Download error for (?<d1>.*) chunk id (?<d2>.*) startTime (?<d3>.*) timeout = .*");
#endif
                            if (ssEvent != null) Submit(ssEvent);
                        }
                    }
                    else
                    {
                        SmoothStreamingEvent ssEvent = new SmoothStreamingEvent(entry);
                        ssEvent.Message = entry.Text;
                        ssEvent.Value = (int)entry.TraceLevel;
                        ssEvent.Data1 = entry.TraceLevel.ToString();
                        ssEvent.Data2 = entry.ClassName + "." + entry.MethodName;
                        ssEvent.EventType = EventType.ErrorMessage;
                        Submit(ssEvent);
                    }
                }
                else
                {
                    switch (entry.TraceArea)
                    {
                        case TraceArea.HttpWebResponse:
                            if (!entry.Text.Contains("200 OK"))
                            {
                                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.HttpError, "HTTP Response:  (?<v>.*) .*, NetworkingStackType: (?<d1>.*)");
                                Submit(ssEvent);
                            }
                            break;
                        case TraceArea.Heuristics:
                            if (entry.MethodName == "GetPerceivedBandwidth")
                            {
                                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.PerceivedBandwidth, "NetworkHeuristicsModule - Perceived bandwidth using .* sliding windows and .* method is (?<v>.*) bytes/sec \\[(?<d1>.*)\\]");
                                if (ssEvent != null) Submit(ssEvent);
                            }
                            break;
                        case TraceArea.BufferingEngine:
                            ProcessBufferingEngineMessage(entry);
                            break;
                        case TraceArea.MediaElement:
                            if (entry.MethodName == "SetPlaybackRate")
                            {
                                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.SetPlaybackRate, "SetPlaybackRate: (?<v>.*)");
                                if (ssEvent != null)
                                {
                                    Submit(ssEvent);
                                    SmoothStreamingEvent dvrEvent = new SmoothStreamingEvent(entry);
                                    dvrEvent.EventType = EventType.DvrOperation;
                                    dvrEvent.Message = ssEvent.Message;
                                    dvrEvent.Data1 = DvrOperationType.SetPlaybackRate.ToString();
                                    dvrEvent.Value = ssEvent.Value;
                                    Submit(dvrEvent);
                                }
                            }
                            else if (entry.MethodName == "Position_set")
                            {
                                SmoothStreamingEvent ssEvent = ConvertTraceToEvent(entry, EventType.SetPosition, "Requested position (?<v>.*) \\(SS\\)");
                                if (ssEvent != null)
                                {
                                    Submit(ssEvent);
                                    SmoothStreamingEvent dvrEvent = new SmoothStreamingEvent(entry);
                                    dvrEvent.EventType = EventType.DvrOperation;
                                    dvrEvent.Message = ssEvent.Message;
                                    dvrEvent.Data1 = DvrOperationType.SetPosition.ToString();
                                    dvrEvent.Value = ssEvent.Value;
                                    Submit(dvrEvent);
                                }
                            }
                            else if (entry.MethodName == "Pause" && entry.Text == "Pause")
                            {
                                SmoothStreamingEvent ssEvent = new SmoothStreamingEvent(entry);
                                ssEvent.EventType = EventType.SetPlaybackState;
                                ssEvent.Data1 = entry.Text;
                                Submit(ssEvent);
                                SetPlaybackRate(ssEvent.Ticks, 0);
                            }
                            else if (entry.MethodName == "Play" && entry.Text == "Play")
                            {
                                SmoothStreamingEvent ssEvent = new SmoothStreamingEvent(entry);
                                ssEvent.EventType = EventType.SetPlaybackState;
                                ssEvent.Data1 = entry.Text;
                                Submit(ssEvent);
                                SetPlaybackRate(ssEvent.Ticks, 1);
                            }
                            break;
                        case TraceArea.Test:
                            try
                            {
                                // this trace message sometimes had bad data in it
                                string[] s = entry.Text.Split(' ');
                                if (s.Length == 17 && (s[5] == "V" || s[5] == "A"))
                                {
                                    SmoothStreamingEvent ssEvent = new SmoothStreamingEvent(entry);
#if SILVERLIGHT3
                                    ssEvent.Value = TimeSpan.Parse(s[2]).TotalMilliseconds;
#else
                                    ssEvent.Value = TimeSpan.Parse(s[2], CultureInfo.CurrentCulture).TotalMilliseconds;
#endif
                                    ssEvent.Data1 = s[10];
                                    ssEvent.Data2 = s[9];
                                    ssEvent.Data3 = s[7];
                                    if (s[6] == "200")
                                    {
                                        if (s[5] == "V")
                                        {
                                            ssEvent.EventType = EventType.VideoChunkDownload;
                                        }
                                        else
                                        {
                                            ssEvent.EventType = EventType.AudioChunkDownload;
                                        }
                                    }
                                    else
                                    {
                                        ssEvent.EventType = EventType.HttpError;
                                        ssEvent.Value = int.Parse(s[6], CultureInfo.CurrentCulture);
                                    }
                                    Submit(ssEvent);
                                }
                            }
                            catch
                            {
                                // ignore this exception, must have been a bad trace message
                            }
                            break;
                    }
                }
            }
        }
    }
}
