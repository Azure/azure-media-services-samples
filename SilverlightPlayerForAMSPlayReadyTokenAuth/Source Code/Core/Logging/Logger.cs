using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Logging
{
    /// <summary>
    /// Handles loading and unloading LogWriters.
    /// </summary>
    public class Logger : IDisposable
    {
        private readonly IList<ILogWriter> _logWriters;

        public Logger()
        {
            _logWriters = new List<ILogWriter>();
        }

        /// <summary>
        /// Represents the current LogWriters collection.
        /// </summary>
        public IEnumerable<ILogWriter> LogWriters
        {
            get { return _logWriters.ToList(); }
        }

        #region IDisposable Members

        /// <summary>
        /// Releases the resources used by the Logger.
        /// </summary>
        public void Dispose()
        {
            _logWriters.ToList().ForEach(UnregisterLogWriter);
        }

        #endregion

        /// <summary>
        /// Occurs when an error occurs on attempting to write to a LogWriter.
        /// </summary>
        public event Action<ILogWriter, LogEntry, Exception> LogWriteErrorOccurred;

        /// <summary>
        /// Occurs when a log entry writes successfully to a LogWriter.
        /// </summary>
        public event Action<ILogWriter, LogEntry> LogWriteSuccessful;


        /// <summary>
        /// Loads a logWriter to the current LogWriters collection.
        /// </summary>
        /// <param name="logWriter">The logWriter to load.</param>
        public void RegisterLogWriter(ILogWriter logWriter)
        {
            if (!_logWriters.Contains(logWriter))
            {
                logWriter.PluginLoaded += LogWriter_PluginLoaded;
                logWriter.PluginLoadFailed += LogWriter_PluginLoadFailed;
                logWriter.LogWriteSuccessful += LogWriter_LogWriteSuccessful;
                logWriter.LogWriteFailed += LogWriter_LogWriteErrorOccurred;

                if (!logWriter.IsLoaded)
                {
                    logWriter.Load();
                }
                else
                {
                    _logWriters.Add(logWriter);
                }
            }
        }

        /// <summary>
        /// Removes a logWriter from the current LogWriters collection.
        /// </summary>
        /// <param name="logWriter">The logWriter to remove.</param>
        public void UnregisterLogWriter(ILogWriter logWriter)
        {
            if (_logWriters.Contains(logWriter))
            {
                _logWriters.Remove(logWriter);
                logWriter.PluginLoaded -= LogWriter_PluginLoaded;
                logWriter.PluginLoadFailed -= LogWriter_PluginLoadFailed;
                logWriter.LogWriteSuccessful -= LogWriter_LogWriteSuccessful;
                logWriter.LogWriteFailed -= LogWriter_LogWriteErrorOccurred;

                if (logWriter.IsLoaded)
                {
                    logWriter.Unload();
                }
            }
        }

        void LogWriter_PluginLoadFailed(IPlugin plugin, Exception ex)
        {
            var logWriter = plugin as ILogWriter;
            logWriter.PluginLoaded -= LogWriter_PluginLoaded;
            logWriter.PluginLoadFailed -= LogWriter_PluginLoadFailed;
            logWriter.LogWriteSuccessful -= LogWriter_LogWriteSuccessful;
            logWriter.LogWriteFailed -= LogWriter_LogWriteErrorOccurred;
        }

        private void LogWriter_PluginLoaded(IPlugin logWriter)
        {
            if (logWriter is ILogWriter && logWriter.IsLoaded && !_logWriters.Contains(logWriter as ILogWriter))
            {
                _logWriters.Add(logWriter as ILogWriter);
            }
        }

        private void LogWriter_LogWriteSuccessful(ILogWriter logWriter, LogEntry logEntry)
        {
            LogWriteSuccessful.IfNotNull(i => i(logWriter, logEntry));
        }

        private void LogWriter_LogWriteErrorOccurred(ILogWriter logWriter, LogEntry logEntry, Exception err)
        {
            LogWriteErrorOccurred.IfNotNull(i => i(logWriter, logEntry, err));
        }

        /// <summary>
        /// Writes a log entry to each of the LogWriters.
        /// </summary>
        /// <param name="logEntry">The log entry to write.</param>
        public void SendLogEntry(LogEntry logEntry)
        {
            if (LogWriters != null)
            {
                LogWriters.Where(i => i.IsLoaded)
                    .ForEach(i => WriteLog(i, logEntry));
            }
        }

        private void WriteLog(ILogWriter logWriter, LogEntry logEntry)
        {
            try
            {
                logWriter.WriteLog(logEntry);
            }
            catch (Exception err)
            {
                LogWriteErrorOccurred.IfNotNull(i => i(logWriter, logEntry, err));
            }
        }
    }
}