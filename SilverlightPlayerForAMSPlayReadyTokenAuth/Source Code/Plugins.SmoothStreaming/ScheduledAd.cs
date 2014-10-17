using System;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    internal class ScheduledAd
    {
#if !WINDOWS_PHONE
        public bool IsLinearClip { get; set; }
#endif
        public ClipInformation ClipInformation { get; set; }
        public ClipContext ClipContext { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? StartOffset { get; set; }
        public bool PauseTimeline { get; set; }
        public ScheduledAd AppendToAd { get; set; }
        public object Data { get; set; }

        public bool IsScheduled
        {
            get { return ClipContext != null; }
        }
    }
}