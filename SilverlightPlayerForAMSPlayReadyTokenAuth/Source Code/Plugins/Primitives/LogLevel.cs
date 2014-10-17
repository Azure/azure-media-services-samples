using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Indicates the severity level for a log entry.
    /// </summary>
    /// <remarks>
    /// The LogLevel is used by plug-ins to indicate the severity of a log entry.
    /// The Player has a LogLevel property that indicates what level of severity is required for a log entry to be logged (written to a LogWriter).
    /// For example, if a plug-in sends a log entry of <c>LogLevel.Information</c> and the <c>Player.LogLevel</c> property is set to <c>LogLevel.Error</c>, 
    /// the log entry will not be written to a LogWriter.
    /// </remarks>
    [Flags]
    public enum LogLevel
    {
        None = 0,
        Debug = 0x1,
        Information = 0x2,
        Statistics = 0x4,
        Warning = 0x8,
        Error = 0x10,
        All = 0x3E
    }
}