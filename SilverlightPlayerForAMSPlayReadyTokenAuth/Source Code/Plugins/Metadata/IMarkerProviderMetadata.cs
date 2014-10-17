namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Is used internally for querying and filtering available plugins.
    /// </summary>
    public interface IMarkerProviderMetadata : IPluginMetadata
    {
        /// <summary>
        /// Gets whether this plugin supports polling a marker data source.
        /// </summary>
        bool SupportsPolling { get; }

        /// <summary>
        /// Gets the marker data source format supported by this plugin.
        /// </summary>
        string SupportedFormat { get; }
    }
}