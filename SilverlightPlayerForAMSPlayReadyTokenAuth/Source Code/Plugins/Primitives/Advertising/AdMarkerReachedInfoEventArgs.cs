using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    public class AdMarkerReachedInfoEventArgs : EventArgs
    {
        public AdMarker Marker { get; set; }
        public bool SeekedInto { get; set; }
    }
}
