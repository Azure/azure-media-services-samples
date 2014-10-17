
namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    public interface IAdPayloadHandlerMetadata : IPluginMetadata
    {
        /// <summary>
        /// Indicates the supported format of the ad source
        /// </summary>
        string SupportedFormat { get; }
    }
}
