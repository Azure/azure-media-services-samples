using System;
using System.Diagnostics;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Resources;

namespace Microsoft.SilverlightMediaFramework.Plugins.LogWriters
{
    /// <summary>
    /// Represents the default logging plug-in which writes logging information to the Debug Window.
    /// </summary>
    [ExportLogWriter(PluginName = PluginName,
        PluginDescription = PluginDescription,
        PluginVersion = PluginVersion,
        LogWriterId = LogWriterId)]
    public class DebugLogWriter : ILogWriter
    {
        private const string LogWriterId = "Debug";
        private const string PluginName = "DebugLogWriter";
        private const string PluginDescription = "Writes logging output to the debug window.";
        private const string PluginVersion = "2.2012.0605.0";

        #region ILogWriter Members
        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;
        
        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
#pragma warning disable 67
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
#pragma warning disable 67
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Occurs when writing to this LogWriter fails.
        /// </summary>
        public event Action<ILogWriter, LogEntry, Exception> LogWriteFailed;

        /// <summary>
        /// Occurs after a successful write operation to this LogWriter.
        /// </summary>
        public event Action<ILogWriter, LogEntry> LogWriteSuccessful;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
#pragma warning disable 67
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Writes a specified LogEntry to the LogWriter.
        /// </summary>
        /// <param name="logEntry">The LogEntry to write.</param>
        public void WriteLog(LogEntry logEntry)
        {
            try
            {
                string message = string.Format(PluginsResources.DebugLogWriterOutputFormat, logEntry.Severity,
                                               logEntry.SenderName, logEntry.Message, logEntry.Timestamp);
                Debug.WriteLine(message);

                if (LogWriteSuccessful != null)
                {
                    LogWriteSuccessful(this, logEntry);
                }
            }
            catch (Exception err)
            {
                if (LogWriteFailed != null)
                {
                    LogWriteFailed(this, logEntry, err);
                }
            }
        }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public void Load()
        {
            IsLoaded = true;
            if (PluginLoaded != null)
            {
                PluginLoaded(this);
            }
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public void Unload()
        {
            IsLoaded = false;
            if (PluginUnloaded != null)
            {
                PluginUnloaded(this);
            }
        }

        #endregion
    }
}