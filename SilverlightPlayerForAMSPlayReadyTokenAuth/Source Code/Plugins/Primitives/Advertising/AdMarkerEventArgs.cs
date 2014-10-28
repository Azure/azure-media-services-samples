using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    public class AdMarkerEventArgs : EventArgs
    {
        readonly AdMarker adMarker;
        public AdMarkerEventArgs(AdMarker AdMarker)
        {
            adMarker = AdMarker;
        }

        public AdMarker AdMarker { get { return adMarker; } }
    }
}
