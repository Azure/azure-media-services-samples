using System;
using System.ComponentModel;
using System.Windows;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Utilities.Converters;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents a resource for getting markers.
    /// </summary>
    /// <remarks>
    /// The Source property indicates where to get the markers. This can be a location of an XML file, a web service, or any other mechanism located at a Uri that can return a collection of markers.
    /// The default Format for markers is the W3C Timed Text Authoring Format 1.0 (TTAF1-DFXP).
    /// </remarks>
    public class MarkerResource : DependencyObject
    {
        private const string DefaultMarkerResourceFormat = "TTAF1-DFXP";

        /// <summary>
        /// 
        /// </summary>
        public MarkerResource()
        {
            Format = DefaultMarkerResourceFormat;
        }

        private MarkerResource(MarkerResource markerResource)
        {
            Source = markerResource.Source;
            Format = markerResource.Format;
            PollingInterval = markerResource.PollingInterval;
        }

        /// <summary>
        /// Gets or sets the location of the Markers data.
        /// </summary>
        [XmlIgnore]
        public Uri Source { get; set; }

        /// <summary>
        /// Text representation of Source.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SourceText
        {
            get
            {
                return Source != null
                           ? Source.ToString()
                           : string.Empty;
            }

            set
            {
                Uri result;
                Source = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)
                                  ? result
                                  : null;
            }
        }

        /// <summary>
        /// Gets or sets the time interval to poll the Markers data.
        /// </summary>
        /// <remarks>
        /// This is a nullable type. A null value indicates no polling will be done.
        /// </remarks>
        [TypeConverter(typeof (TimeSpanTypeConverter))]
        public TimeSpan? PollingInterval { get; set; }

        /// <summary>
        /// Gets or sets the format of the Markers data.
        /// </summary>
        /// <remarks>
        /// The default Format is "TTAF1-DFXP" which represents markers using the W3C Timed Text Authoring Format 1.0 standard.
        /// </remarks>
        public string Format { get; set; }

        public MarkerResource Clone()
        {
            return new MarkerResource(this);
        }
    }
}