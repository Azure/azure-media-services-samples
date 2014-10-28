using System;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Defines the minimum that a plug-in must implement to be loaded by the SMF plug-in architecture.
    /// </summary>
    /// <remarks>
    /// The SMF plug-in architecture is based on the Managed Extensibility Framework (MEF). 
    /// </remarks>
    /// 
    [InheritedExport]
    public interface IPlugin
    {
        // Consumed internally for getting log messages and events to the server/database

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        bool IsLoaded { get; }

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
        event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        void Load();

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        void Unload();
    }
}