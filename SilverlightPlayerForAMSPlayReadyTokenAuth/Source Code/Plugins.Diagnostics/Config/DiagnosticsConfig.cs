using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// This is the main configuration object used by the diagnostic component.
    /// An instance of this object is required for the HealthMonitor class to instantiate.
    /// </summary>
    public class DiagnosticsConfig
    {
        /// <summary>
        /// Stores the information necessary for configuring the trace monitor.
        /// The trace monitor is the module that polls information from SSME at a regular interval on a background thread as well as retrieves system information.
        /// If using PIP, you should set this null for the instance of the HealthMonitor that is tracking the PIP
        /// </summary>
        public TracingConfig TracingConfig { get; set; }
        internal bool InitTraceMonitor { get { return TracingConfig != null; } }

        /// <summary>
        /// Responsible for indicating which quality data should be tracked and aggregated. Used to filter data that will not be used at the earliest possible moment to improve performance.
        /// Set to null to track all quality data.
        /// </summary>
        public QualityConfig QualityConfig { get; set; }

        /// <summary>
        /// Indicates that quality snapshots should be taken and passed on via the HealthMonitor.ReportSnapshotData event.
        /// Quality snapshots are only valuable for display of real time diag info and quality aggregated data should be used instead for logging to the server.
        /// Quality snapshots are taken at the same interval as the TraceConfig.PollingInterval so performance can be improved by setting this to false.
        /// </summary>
        public bool TrackQualitySnapshot { get; set; }

        /// <summary>
        /// Indicates that the raw tracelogs should be passed on through the HealthMonitor.ReportTraceLogs event.
        /// TraceLogs are generated very quickly and can easily generate thousands of them in a matter of minutes. You should therefore set this to false if you do not intend to use them.
        /// </summary>
        public bool RecordTraceLogs { get; set; }

        /// <summary>
        /// Indicates that specifics should be tracked about download errors and reported via the HealthMonitor.ReportAggregatedData event.
        /// </summary>
        public bool TrackDownloadErrors { get; set; }

        /// <summary>
        /// Indicates that quality data should be sampled, aggregated and reported via the HealthMonitor.ReportAggregatedData event.
        /// </summary>
        public bool TrackQuality { get; set; }

        /// <summary>
        /// Indicates the latency in KBps that needs to be reached before a LatencyAlert is generated.
        /// </summary>
        public double LatencyAlertThreshold { get; set; }

        /// <summary>
        /// Defines the rules used to retrieve info about the edge server and client IP.
        /// </summary>
        public IEnumerable<EdgeServerRules> EdgeServerRuleCollection { get; set; }

        /// <summary>
        /// Indicates the interval that quality data should be generated. Default is every 30 seconds.
        /// </summary>
        public TimeSpan AggregationInterval { get; set; }

        /// <summary>
        /// Indicates the interval that quality snapshot data should be generated. Default is every seconds.
        /// </summary>
        public TimeSpan SnapshotInterval { get; set; }

        public DiagnosticsConfig()
        {
            SetDefaultSettings();
        }

        void SetDefaultSettings()
        {
            TracingConfig = new TracingConfig();
            RecordTraceLogs = true;
            TrackQuality = true;
            TrackDownloadErrors = true;
            AggregationInterval = TimeSpan.FromSeconds(30);
            SnapshotInterval = TimeSpan.FromSeconds(1);
            LatencyAlertThreshold = 2;
            RecordTraceLogs = true;
            TrackQualitySnapshot = true;
            QualityConfig = null;
            EdgeServerRuleCollection = null;
        }

        /// <summary>
        /// Creates an instance of the main diagnostic config object from a relative Uri
        /// </summary>
        public static DiagnosticsConfig Load(Uri configUri)
        {
            if (configUri == null)
                throw new ArgumentNullException("configUri");

            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(configUri.OriginalString, xmlSettings))
                return Load(reader);
        }

        /// <summary>
        /// Creates an instance of the main diagnostic config object from an XmlReader
        /// </summary>
        public static DiagnosticsConfig Load(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var result = new DiagnosticsConfig();
            List<EdgeServerRules> edgeServerRuleCollection = null;

            reader.GoToElement();
            reader.ReadStartElement();
            if (!reader.IsEmptyElement)
            {
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "TraceMonitor":
                            result.TracingConfig = TracingConfig.Load(reader);
                            break;

                        case "EdgeServerRules":
                            if (!reader.IsEmptyElement)
                            {
                                if (edgeServerRuleCollection == null)
                                {
                                    edgeServerRuleCollection = new List<EdgeServerRules>();
                                    result.EdgeServerRuleCollection = edgeServerRuleCollection;
                                }
                                edgeServerRuleCollection.Add(EdgeServerRules.Load(reader));
                            }
                            else
                                reader.Skip();
                            break;

                        case "Diagnostics":
                            if (!reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                                while (reader.GoToSibling())
                                {
                                    switch (reader.LocalName)
                                    {
                                        case "TrackQuality":
                                            result.TrackQuality = Convert.ToBoolean(reader.ReadElementContentAsInt());
                                            break;
                                        case "TrackDownloadErrors":
                                            result.TrackDownloadErrors = Convert.ToBoolean(reader.ReadElementContentAsInt());
                                            break;
                                        case "AggregationIntervalMilliseconds":
                                            result.AggregationInterval = TimeSpan.FromMilliseconds(reader.ReadElementContentAsInt());
                                            break;
                                        case "TrackQualitySnapshot":
                                            result.TrackQualitySnapshot = Convert.ToBoolean(reader.ReadElementContentAsInt());
                                            break;
                                        case "SnapshotIntervalMilliseconds":
                                            result.SnapshotInterval = TimeSpan.FromMilliseconds(reader.ReadElementContentAsInt());
                                            break;
                                        case "LatencyAlertThreshold":
                                            result.LatencyAlertThreshold = reader.ReadElementContentAsFloat();
                                            break;
                                        case "RecordTraceLogs":
                                            result.RecordTraceLogs = Convert.ToBoolean(reader.ReadElementContentAsInt());
                                            break;
                                        case "QualityTracking":
                                            result.QualityConfig = QualityConfig.Load(reader);
                                            break;
                                        default:
                                            reader.Skip();
                                            break;
                                    }
                                }
                                reader.ReadEndElement();
                            }
                            else
                                reader.Skip();
                            break;

                        default:
                            reader.Skip();
                            break;
                    }
                }
                reader.ReadEndElement();
            }
            else
                reader.Skip();

            return result;
        }


    }

}
