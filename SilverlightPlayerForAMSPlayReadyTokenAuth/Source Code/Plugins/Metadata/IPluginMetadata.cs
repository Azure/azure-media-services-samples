namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Is used internally for querying and filtering available plugins.
    /// </summary>
    public interface IPluginMetadata
    {
        /// <summary>
        /// Gets the official name of this plug-in.
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Gets a description of what the plug-in does.
        /// </summary>
        string PluginDescription { get; }

        /// <summary>
        /// Gets the version of this plug-in.
        /// </summary>
        string PluginVersion { get; }
    }
}