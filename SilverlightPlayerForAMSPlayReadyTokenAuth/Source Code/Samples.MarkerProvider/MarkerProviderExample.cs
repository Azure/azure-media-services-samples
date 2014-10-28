using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Samples.MarkerProvider
{
    [ExportMarkerProvider(PluginName = "MarkerProviderExample",
                          PluginDescription = "This is an example of a Marker Provider",
                          PluginVersion = "1.0",
                          SupportedFormat = "FakeItFormat")]
    public class MarkerProviderExample : IMarkerProvider
    {
        public event Action<IPlugin, LogEntry> LogReady;
        public event Action<IPlugin> PluginLoaded;
        public event Action<IPlugin> PluginUnloaded;
        public event Action<IPlugin, Exception> PluginLoadFailed;
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        private List<MediaMarker> _markers;

        public Uri Source { get; set; }
        public TimeSpan? PollingInterval { get; set; }
        public bool IsLoaded { get; private set; }

        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> NewMarkers;
        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> MarkersRemoved;
        public event Action<IMarkerProvider, Exception> RetrieveMarkersFailed;

        public void BeginRetrievingMarkers()
        {
            NewMarkers.IfNotNull(i => i(this, _markers));
        }

        public void StopRetrievingMarkers()
        {
            
        }


        public void Load()
        {
            InitializeMarkers();
            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
        }

        public void Unload()
        {
            IsLoaded = false;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        private void InitializeMarkers()
        {
            _markers = new List<MediaMarker>
            {
                new TimelineMediaMarker
                {
                        Begin = TimeSpan.FromSeconds(10),
                        End = TimeSpan.FromSeconds(10),
                        Content = "Marker 1"
                },
                new TimelineMediaMarker
                {
                        Begin = TimeSpan.FromSeconds(20),
                        End = TimeSpan.FromSeconds(20),
                        Content = "Marker 2"
                },
                new TimelineMediaMarker
                {
                        Begin = TimeSpan.FromSeconds(30),
                        End = TimeSpan.FromSeconds(30),
                        Content = "Marker 3"
                },
                new TimelineMediaMarker
                {
                        Begin = TimeSpan.FromSeconds(40),
                        End = TimeSpan.FromSeconds(40),
                        Content = "Marker 4"
                },
                new TimelineMediaMarker
                {
                        Begin = TimeSpan.FromSeconds(50),
                        End = TimeSpan.FromSeconds(50),
                        Content = "Marker 5"
                },
            };
        }
    }
}
