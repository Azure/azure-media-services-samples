namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Exports a plugin that can act as an AdaptiveCacheProvider.
    /// </summary>
    public class ExportAdaptiveCacheProviderAttribute : ExportPluginAttribute
    {
        public const string AdaptiveCacheProviderContractName =
            "Microsoft.SilverlightMediaFramework.Plugins.AdaptiveCacheProvider";

        public ExportAdaptiveCacheProviderAttribute()
            : base(AdaptiveCacheProviderContractName)
        {
        }
    }
}