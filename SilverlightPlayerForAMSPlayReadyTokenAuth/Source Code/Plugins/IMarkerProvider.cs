using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Represents an object that can get MediaMarkers from some source.
    /// </summary>
    /// <remarks>
    /// <para>An IMarkerProvider object knows how to get MediaMarkers.
    /// The provided default implementation gets Markers using the W3C Timed Text Authoring Format (TTAF) 1.0 specification.
    /// </para>
    /// <para>
    /// In general, an IMarkerProvider is an object that can return an IEnumerable collection of MediaMarker objects from somewhere.
    /// You can define an IMarkerProvider implementation that returns an IEnumerable collection of MediaMarker objects from any source you want 
    /// an application to use (for example, from an XML file on the network or a Web service).
    /// </para>
    /// <para>
    /// The PollingInterval is an optional property (nullable) that can be used to have the provider check for markers at a specific interval 
    /// of time. This allows you to define a marker provider that can add or remove markers dynamically, such as during a live event.
    /// </para>
    /// <para>
    /// An IMarkerProvider has a collection of new markers added and markers removed that are detected after the retrieval of markers. 
    /// The Id property of a marker is used to determine if it is new or if an existing marker has been removed.
    /// </para>
    /// </remarks>
    public interface IMarkerProvider : IPlugin
    {
        /// <summary>
        /// Gets or sets the location for getting markers.
        /// </summary>
        Uri Source { get; set; }

        /// <summary>
        /// Gets or sets the timing interval for polling (optional).
        /// </summary>
        TimeSpan? PollingInterval { get; set; }

        /// <summary>
        /// Occurs when new markers are found.
        /// </summary>
        event Action<IMarkerProvider, IEnumerable<MediaMarker>> NewMarkers;

        /// <summary>
        /// Occurs when markers have been removed.
        /// </summary>
        event Action<IMarkerProvider, IEnumerable<MediaMarker>> MarkersRemoved;

        /// <summary>
        /// Occurs when retrieving markers fails.
        /// </summary>
        event Action<IMarkerProvider, Exception> RetrieveMarkersFailed;

        /// <summary>
        /// Begins retrieving markers from the Source location.
        /// </summary>
        void BeginRetrievingMarkers();

        /// <summary>
        /// Stops retrieving markers.
        /// </summary>
        void StopRetrievingMarkers();
    }
}