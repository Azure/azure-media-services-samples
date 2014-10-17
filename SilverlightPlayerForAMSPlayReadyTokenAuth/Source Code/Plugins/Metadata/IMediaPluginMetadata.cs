using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Is used internally for querying and filtering available plugins.
    /// </summary>
    public interface IMediaPluginMetadata : IPluginMetadata
    {
        /// <summary>
        /// Gets whether this plugin supports live DVR functionality implemented by the ILiveMediaPlugin.
        /// </summary>
        bool SupportsLiveDvr { get; }

        /// <summary>
        /// Gets the supported delivery methods.
        /// </summary>
        DeliveryMethods SupportedDeliveryMethods { get; }

        /// <summary>
        /// Gets the supported media type.
        /// </summary>
        string[] SupportedMediaTypes { get; }
    }
}