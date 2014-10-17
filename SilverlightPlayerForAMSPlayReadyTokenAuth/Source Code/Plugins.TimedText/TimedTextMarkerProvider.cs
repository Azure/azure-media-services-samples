using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.TimedText.Resources;
using Microsoft.SilverlightMediaFramework.Utilities.Data;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using System.Threading;
#if USEAFFINITY
using Microsoft.Xbox.Core;
#endif

namespace Microsoft.SilverlightMediaFramework.Plugins.TimedText
{
    /// <summary>
    /// Retrieves MediaMarkers using the W3C Timed Text Authoring Format (TTAF) 1.0 specification.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TimedTextMarkerProvider can read the TTAF format and return a collection of MediaMarker objects from it.
    /// A TimedTextMarkerProvider has a reference to a PollingRequest object which determines the polling interval to be used, if any, for checking the source for new markers (polling is optional).
    /// </para>
    /// <para>
    /// This class has a collection of new markers and a collection of markers removed since the last request to check for markers. This check is done using the Marker Id property.
    /// </para>
    /// </remarks>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMarkerProvider(PluginName = PluginName,
        PluginDescription = PluginDescription,
        PluginVersion = PluginVersion,
        SupportedFormat = SupportedFormat,
        SupportsPolling = true)]
    public class MarkerProvider : IMarkerProvider
    {
        private const string PluginName = "TimedTextMarkerProvider";

        private const string PluginDescription =
            "Provides a Marker Provider implementation capable of reading the W3C Timed Text Authoring Format 1.0 XML format.";

        private const string PluginVersion = "2.2012.0605.0";
        private const string SupportedFormat = "TTAF1-DFXP";
        private readonly IMarkerParser _markerParser;
        private readonly PollingRequest _request;
        private IDictionary<string, MediaMarker> _previousMarkers;

        public MarkerProvider()
            : this(new TimedTextMarkerParser())
        {
        }

        public MarkerProvider(IMarkerParser markerParser)
        {
            _markerParser = markerParser;
            _previousMarkers = new Dictionary<string, MediaMarker>();

            _request = new PollingRequest();
            _request.RequestProgressChanged += Request_RequestProgressChanged;
            _request.RequestCompleted += Request_RequestCompleted;
            _request.RequestFailed += Request_RequestFailed;

            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderInstantiated, message: TimedTextMediaPluginResources.TimedTextMediaPluginInstantiatedLogMessage);
        }

        #region IMarkerProvider Members
        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when retrieving markers fails.
        /// </summary>
        public event Action<IMarkerProvider, Exception> RetrieveMarkersFailed;

        /// <summary>
        /// Occurs when markers have been removed.
        /// </summary>
        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> MarkersRemoved;

        /// <summary>
        /// Occurs when new markers are found.
        /// </summary>
        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> NewMarkers;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets or sets the location where the markers are retrieved.
        /// </summary>
        public Uri Source
        {
            get { return _request.Source; }
            set { _request.Source = value; }
        }

        /// <summary>
        /// The time interval between checking for new and removed markers.
        /// </summary>
        public TimeSpan? PollingInterval
        {
            get
            {
                return _request.PollingInterval != TimeSpan.Zero
                           ? _request.PollingInterval
                           : (TimeSpan?) null;
            }
            set
            {
                _request.PollingInterval = value.HasValue
                                               ? value.Value
                                               : TimeSpan.Zero;
            }
        }


        /// <summary>
        /// Begins the polling for retrieving markers.
        /// </summary>
        public void BeginRetrievingMarkers()
        {
            _request.StartPolling();
        }

        /// <summary>
        /// Stops polling for markers.
        /// </summary>
        public void StopRetrievingMarkers()
        {
            _request.StopPolling();
        }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public void Load()
        {
            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderLoaded, message: TimedTextMediaPluginResources.TimedTextMediaPluginLoadedLogMessage);
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public void Unload()
        {
            IsLoaded = false;
            Source = null;
            StopRetrievingMarkers();
            _previousMarkers.Clear();
            PluginUnloaded.IfNotNull(i => i(this));
            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderUnloaded, message: TimedTextMediaPluginResources.TimedTextMediaPluginUnloadedLogMessage);
        }

        #endregion

        private void Request_RequestProgressChanged(PollingRequest request, double progress)
        {
            string message = string.Format(TimedTextMediaPluginResources.DownloadProgressChangedLogMessage, progress);
            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderDownloadProgressChanged, message: message);
        }

        private void Request_RequestFailed(PollingRequest request, Exception error)
        {
            string logMessage = string.Format(TimedTextMediaPluginResources.DownloadFailedLogMessage, error.Message);
            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderDownloadFailed, LogLevel.Warning, logMessage);
            RetrieveMarkersFailed.IfNotNull(i => i(this, error));
        }

        private void Request_RequestCompleted(PollingRequest request, string result)
        {
            try
            {
                SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderDownloadCompleted, message: TimedTextMediaPluginResources.DownloadCompletedLogMessage);

                if (result.IsNullOrWhiteSpace())
                    return;

                Thread thread = new Thread(ParseMarkers);
                thread.Start(result);
            }
            catch (Exception err)
            {
                LogParseError(err);
            }
        }

        void ParseMarkers(object data)
        {
#if USEAFFINITY
            // The "extension" method SetThreadProcessorAffinity was missing the "this" notation, 
            // so it's not really an extension hence this style of call.
            RuntimeExtensions.SetThreadProcessorAffinity(Thread.CurrentThread, 4);
#endif
            try
            {                
                string result = data as string;

                XDocument markerXml = XDocument.Parse(result);

                IEnumerable<MediaMarker> markers = _markerParser.ParseMarkerCollection(markerXml, TimeSpan.Zero, TimeSpan.MaxValue)
                                                                .Cast<MediaMarker>()
                                                                .ToList();

                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => ParseSucceeded(markers));
            }
            catch (Exception ex)
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => LogParseError(ex));
            }
        }

        void ParseSucceeded(IEnumerable<MediaMarker> markers)
        {
            try
            {
                if (markers == null)
                    return;

                var markersHash = markers.ToDictionary(i => i.Id, i => i);

                List<MediaMarker> newMarkers = markers.Where(i => !_previousMarkers.ContainsKey(i.Id)).ToList();
                List<MediaMarker> removedMarkers = _previousMarkers.Values.Where(i => !markersHash.ContainsKey(i.Id)).ToList();

                if (newMarkers.Count > 0 && NewMarkers != null)
                {
                    NewMarkers(this, newMarkers);
                }

                if (removedMarkers.Count > 0 && MarkersRemoved != null)
                {
                    MarkersRemoved(this, removedMarkers);
                }

                _previousMarkers = markersHash;
            }
            catch (Exception err)
            {
                LogParseError(err);
            }
        }

        void LogParseError(Exception err)
        {
            string logMessage = string.Format(TimedTextMediaPluginResources.ParseErrorLogMessage, err.Message);
            SendLogEntry(KnownLogEntryTypes.TimedTextMarkerProviderErrorOccurred, LogLevel.Warning, logMessage);
            RetrieveMarkersFailed.IfNotNull(i => i(this, err));
        }

        private void SendLogEntry(string type,
                                  LogLevel severity = LogLevel.Information,
                                  string message = null,
                                  DateTime? timeStamp = null,
                                  IEnumerable<KeyValuePair<string, object>> extendedProperties = null)
        {
            if (LogReady != null)
            {
                var logEntry = new LogEntry
                                   {
                                       Severity = severity,
                                       Message = message,
                                       SenderName = PluginName,
                                       Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                                   };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);
                LogReady(this, logEntry);
            }
        }
    }
}