namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// A plugin that implements heuristics for managing multiple IMediaPlugins running in the same application
    /// </summary>
    public interface IHeuristicsPlugin : IPlugin
    {
        /// <summary>
        /// Determines if this plugin supports management of the specified media plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin.</param>
        /// <returns>True if this media plugin is supported.</returns>
        bool SupportsPlugin(IMediaPlugin mediaPlugin);

        /// <summary>
        /// Registers the specified media plugin so that it can be managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to register.</param>
        /// <param name="priority">The relative priority of this media plugin</param>
        /// <param name="enableSync">Indicates if synchronization of this media plugin with others should be enabled.</param>
        void RegisterPlugin(IMediaPlugin mediaPlugin, int priority, bool enableSync);

        /// <summary>
        /// Unregisters the specified media plugin so that it is no longer managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to unregister.</param>
        void UnregisterPlugin(IMediaPlugin mediaPlugin);
    }
}