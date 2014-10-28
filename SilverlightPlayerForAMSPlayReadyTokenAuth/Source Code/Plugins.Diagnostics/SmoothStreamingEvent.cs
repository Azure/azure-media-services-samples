using System;
using System.Xml.Linq;
using Microsoft.Web.Media.Diagnostics;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    public enum EventType
    {
        None,
        HttpError,
        ProcessCpuLoad,
        SystemCpuLoad,
        DroppedFramesPerSecond,
        RenderedFramesPerSecond,
        PerceivedBandwidth,
        BitrateChanged,
        AudioBufferSize,
        VideoBufferSize,
        ErrorMessage,
        StreamLoaded,
        StreamStarted,
        StreamEnded,
        ClipStarted,
        ClipEnded,
        DvrOperation,
        SetPlaybackRate,
        SetPosition,
        SetPlaybackState,
        BufferingStateChanged,
        FullScreenChanged,
        AddressInfo,
        VideoChunkDownload,
        AudioChunkDownload,
        MediaFailed,
        DownloadError,
        Playing,
        Paused,
    }

    public enum DvrOperationType
    {
        None,
        SetPlaybackRate,
        SetPosition
    }

    /// <summary>
    /// This is the primary class responsible for storing relevant events that occur during playback.
    /// These events are sometimes important by themselves and at other times are expected to be aggregated.
    /// </summary>
    public class SmoothStreamingEvent
    {
        internal SmoothStreamingEvent()
        {
            Ticks = DateTime.Now.Ticks;
        }

        internal SmoothStreamingEvent(TraceEntry entry)
        {
            Ticks = entry.Date.Ticks;
        }

        /// <summary>
        /// The datetime (in ticks) that the event occurred
        /// </summary>
        public long Ticks { get; set; }

        /// <summary>
        /// The value of the event. The meaning depends on the event type.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Used to store additional data about the event. The meaning depends on the event type.
        /// </summary>
        public string Data1 { get; set; }

        /// <summary>
        /// Used to store additional data about the event. The meaning depends on the event type.
        /// </summary>
        public string Data2 { get; set; }

        /// <summary>
        /// Used to store additional data about the event. The meaning depends on the event type.
        /// </summary>
        public string Data3 { get; set; }

        /// <summary>
        /// Used to store text data about the event. The meaning depends on the event type.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The EventType
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// The MediaElementId that the event is associated with. This maps to SSME.Name
        /// </summary>
        public string MediaElementId { get; set; }

        /// <summary>
        /// Flag indicating that the event occured in live mode.
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// A generated Id for the stream that was playing when the event occurred.
        /// </summary>
        public int StreamId { get; set; }

        /// <summary>
        /// A generated Id for the clip that was playing when the event occurred.
        /// </summary>
        public int ClipId { get; set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}|{1}|{2}|{3}|{4}|{5}", new DateTime(Ticks).ToShortTimeString(), EventType.ToString(), Value.ToString(CultureInfo.CurrentCulture), Data1, Data2, Message);
        }
    }
}
