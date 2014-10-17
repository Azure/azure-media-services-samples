using System;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Data;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising
{
    /// <summary>
    /// Retrieves MediaMarkers (triggers) using the IAB MAST specification.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The MastMarkerProvider can read the MAST format and return a collection of MediaMarker objects from it.
    /// A MastMarkerProvider has a reference to a PollingRequest object which determines the polling interval to be used, if any, for checking the source for new markers (polling is optional).
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
    public class MastMarkerProvider : MastMarkerProviderBase
    {
        private const string PluginName = "MastMarkerProvider";
        private const string PluginDescription = "Provides a Marker Provider implementation capable of reading the IAB MAST XML format.";
        private const string PluginVersion = "2.2012.0605.0";
        private const string SupportedFormat = "IAB-MAST";

        private readonly PollingRequest _request;
        private bool IsRetrievingMarkers = false;

        public MastMarkerProvider() : base()
        {
            PluginLogName = PluginName;

            _request = new PollingRequest();
            _request.RequestProgressChanged += Request_RequestProgressChanged;
            _request.RequestCompleted += Request_RequestCompleted;
            _request.RequestCanceled += Request_RequestCanceled;
            _request.RequestFailed += Request_RequestFailed;
        }
        
        /// <summary>
        /// Gets or sets the location where the markers are retrieved.
        /// </summary>
        public override Uri Source
        {
            get { return _request.Source; }
            set { _request.Source = value; }
        }

        /// <summary>
        /// The time interval between checking for new and removed markers.
        /// </summary>
        public override TimeSpan? PollingInterval
        {
            get
            {
                return _request.PollingInterval != TimeSpan.Zero
                           ? _request.PollingInterval
                           : (TimeSpan?)null;
            }
            set
            {
                _request.PollingInterval = value.GetValueOrDefault(TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Begins the polling for retrieving markers.
        /// </summary>
        public override void BeginRetrievingMarkers()
        {
            if (!IsRetrievingMarkers)
            {
                _request.StartPolling();
                IsRetrievingMarkers = _request.IsDownloading;
                if (IsRetrievingMarkers)
                {
                    base.BeginRetrievingMarkers();
                }
            }
        }

        /// <summary>
        /// Stops polling for markers.
        /// </summary>
        public override void StopRetrievingMarkers()
        {
            if (IsRetrievingMarkers)
            {
                base.StopRetrievingMarkers();
                _request.StopPolling();
                IsRetrievingMarkers = false;
            }
        }

        private void Request_RequestProgressChanged(PollingRequest request, double progress)
        {
            string message = string.Format(MastMarkerProviderResources.DownloadProgressChangedLogMessage, progress);
            SendLogEntry(LogEntryTypes.DownloadProgressChanged, message: message);
        }

        private void Request_RequestFailed(PollingRequest request, Exception error)
        {
            base.LoadMastDoc(null);
            SendLogEntry(LogEntryTypes.DownloadFailed, LogLevel.Warning, string.Format(MastMarkerProviderResources.DownloadFailedLogMessage, error.Message));
            OnRetrieveMarkersFailed(error);
        }

        private void Request_RequestCanceled(PollingRequest obj)
        {
            base.LoadMastDoc(null);
        }

        private void Request_RequestCompleted(PollingRequest request, string result)
        {
            base.LoadMastDoc(result);
            SendLogEntry(LogEntryTypes.DownloadCompleted, message: MastMarkerProviderResources.DownloadCompletedLogMessage);
        }

        public override void Unload()
        {
            StopRetrievingMarkers();
            base.Unload();
        }
    }
}
