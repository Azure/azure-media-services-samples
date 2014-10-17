namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Exports a plugin that implements the IAdPayloadHandlerPlugin interface
    /// </summary>
    /// <remarks>
    /// This attribute should be added to classes that implement IAdPayloadHandlerPlugin.
    /// </remarks>
    public class ExportAdPayloadHandlerPluginAttribute : ExportPluginAttribute
    {
        public ExportAdPayloadHandlerPluginAttribute()
            : base(typeof(IAdPayloadHandlerPlugin))
        {
        }
            
        /// <summary>
        /// Indicates the supported format of the ad source
        /// </summary>
        public string SupportedFormat { get; set; }
    }
}