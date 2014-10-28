using System;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    /// <summary>
    /// Supports the ability to balance the resource consumption of multiple IAdaptiveMediaPlugin's running in the same application.
    /// </summary>
    [ExportMultiHeuristicsPlugin(PluginName = PluginName, PluginDescription = PluginDescription,
        PluginVersion = PluginVersion, SupportsSync = false)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportMetadata("SupportsBalancedMode", "True")]
    public class BalancedHeuristicsPlugin : IHeuristicsPlugin
    {
        private const string PluginName = "MultiHeuristicsPlugin";

        private const string PluginDescription =
            "Supports the ability to balance the resource consumption of multiple IAdaptiveMediaPlugin's running in the same application.";

        private const string PluginVersion = "2.2012.0605.0";
        private readonly BalancedHeuristicsManager _heuristicsManager;

        public BalancedHeuristicsPlugin()
        {
            _heuristicsManager = new BalancedHeuristicsManager();
        }

        #region IHeuristicsPlugin Members
        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
#pragma warning disable 67
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public void Load()
        {
            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public void Unload()
        {
            IsLoaded = false;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        /// <summary>
        /// Registers the specified media plugin so that it can be managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to register.</param>
        /// <param name="priority">The relative priority of this media plugin</param>
        /// <param name="enableSync">Indicates if synchronization of this media plugin with others should be enabled.</param>
        public void RegisterPlugin(IMediaPlugin mediaPlugin, int priority, bool enableSync)
        {
            var adaptivePlugin = mediaPlugin as IAdaptiveMediaPlugin;
            adaptivePlugin.IfNotNull(i => _heuristicsManager.RegisterPlugin(i, priority));
        }

        /// <summary>
        /// Unregisters the specified media plugin so that it is no longer managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to unregister.</param>
        public void UnregisterPlugin(IMediaPlugin mediaPlugin)
        {
            var adaptivePlugin = mediaPlugin as IAdaptiveMediaPlugin;
            adaptivePlugin.IfNotNull(_heuristicsManager.UnregisterPlugin);
        }

        /// <summary>
        /// Determines if this plugin supports management of the specified media plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin.</param>
        /// <returns>True if this media plugin is supported.</returns>
        public bool SupportsPlugin(IMediaPlugin mediaPlugin)
        {
            return mediaPlugin is IAdaptiveMediaPlugin;
        }

        #endregion
    }
}