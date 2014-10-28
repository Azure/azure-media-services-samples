using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Identifies a class as a media plug-in and specifies its capabilities.
    /// </summary>
    public class ExportMediaPluginAttribute : ExportPluginAttribute
    {
        public ExportMediaPluginAttribute()
            : base(typeof (IMediaPlugin))
        {
        }

        /// <summary>
        /// Gets whether this plugin supports live DVR functionality implemented by the ILiveMediaPlugin.
        /// </summary>
        public bool SupportsLiveDvr { get; set; }

        /// <summary>
        /// Gets the supported delivery methods.
        /// </summary>
        public DeliveryMethods SupportedDeliveryMethods { get; set; }

        /// <summary>
        /// Gets the supported media type.
        /// </summary>
        public string[] SupportedMediaTypes { get; set; }
    }
}