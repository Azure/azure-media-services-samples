using System;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Defines the members a class must implement to be a LogWriter plug-in.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This plugin is useful for the recording of logging information.
    /// </para>
    /// <para>
    /// DebugLogWriter is the ILogWriter implementation that SMF provides. You can write logging plug-ins
    /// that write log entries to other locations, such as to an XML string that is sent by a Web service to a database.
    /// </para>
    /// <para> 
    /// In addition to implementing this interface, a class must have an ExportLogWriter attribute
    /// to be used as a Logging plug-in.
    /// </para>
    /// </remarks>
    public interface ILogWriter : IPlugin
    {
        /// <summary>
        /// Occurs when writing to this LogWriter fails.
        /// </summary>
        event Action<ILogWriter, LogEntry, Exception> LogWriteFailed;

        /// <summary>
        /// Occurs after a successful write operation to this LogWriter.
        /// </summary>
        event Action<ILogWriter, LogEntry> LogWriteSuccessful;

        /// <summary>
        /// Writes a specified LogEntry to the LogWriter.
        /// </summary>
        /// <param name="logEntry">The LogEntry to write.</param>
        void WriteLog(LogEntry logEntry);
    }
}