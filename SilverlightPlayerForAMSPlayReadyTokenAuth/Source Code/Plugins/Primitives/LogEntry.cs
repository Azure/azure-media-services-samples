using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Represents information in a log entry written to the LogWriters.
    /// </summary>
    public class LogEntry
    {
        public LogEntry()
        {
            Id = Guid.NewGuid();
            Severity = LogLevel.Information;
            Message = string.Empty;
            SenderName = string.Empty;
            Timestamp = DateTime.Now;
            ExtendedProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the unique id of this LogEntry.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the severity of this LogEntry.
        /// </summary>
        public LogLevel Severity { get; set; }

        /// <summary>
        /// Gets the message of this LogEntry.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets the type of this LogEntry.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the name of the sender of this LogEntry.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets the timestamp indicating when this LogEntry was created.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets a dictionary containing additional data that is part of this LogEntry.
        /// </summary>
        public IDictionary<string, object> ExtendedProperties { get; private set; }
    }
}