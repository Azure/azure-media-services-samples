using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core
{
    public class ExternalPackageDownloadProgressInfo : EventArgs
    {
        public Uri XapLocation { get; set; }
        public DownloadProgressChangedEventArgs DownloadProgress { get; set; }
    }

    public class TimelineMarkerReachedInfo : EventArgs
    {
        public TimelineMediaMarker Marker { get; set; }
        public bool SeekedInto { get; set; }
    }

    public class LogWriteErrorOccurredInfo : EventArgs
    {
        public LogEntry LogEntry { get; set; }
        public Exception Error { get; set; }
    }

    public class AdvertisementProgressChangedInfo : AdvertisementStateChangedInfo
    {
        public AdProgress AdProgress { get; set; }
    }

    public class AdvertisementStateChangedInfo : EventArgs
    {
        public IAdContext AdContext { get; set; }
    }

    
}
