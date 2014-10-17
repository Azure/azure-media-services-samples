namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Identifies a class as a marker provider and specifies its capabilities.
    /// </summary>
    public class ExportMarkerProviderAttribute : ExportPluginAttribute
    {
        public ExportMarkerProviderAttribute()
            : base(typeof (IMarkerProvider))
        {
        }

        /// <summary>
        /// Gets whether this plugin supports polling a marker data source.
        /// </summary>
        public bool SupportsPolling { get; set; }

        /// <summary>
        /// Gets the marker data source format supported by this plugin.
        /// </summary>
        public string SupportedFormat { get; set; }
    }
}