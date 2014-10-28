using System;
using System.ComponentModel.Composition;

namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Specifies that a type, property, field, or method provides an export as a logging plug-in.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportLogWriterAttribute : ExportPluginAttribute
    {
        public ExportLogWriterAttribute()
            : base(typeof (ILogWriter))
        {
        }

        /// <summary>
        /// The Id of this LogWriter, used by the Player to load specific LogWriters.
        /// </summary>
        public string LogWriterId { get; set; }
    }
}