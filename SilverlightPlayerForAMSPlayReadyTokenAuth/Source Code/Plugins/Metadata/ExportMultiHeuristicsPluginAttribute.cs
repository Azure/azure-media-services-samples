namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Exports a plugin that implements the IHeuristicsPlugins interface
    /// </summary>
    public class ExportMultiHeuristicsPluginAttribute : ExportPluginAttribute
    {
        public ExportMultiHeuristicsPluginAttribute()
            : base(typeof (IHeuristicsPlugin))
        {
        }

        /// <summary>
        /// Gets wether this plugin supports synchronization.
        /// </summary>
        public bool SupportsSync { get; set; }
    }
}