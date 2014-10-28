using System;
using System.ComponentModel.Composition;

namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Specifies that a type, property, field, or method provides an export as a plug-in.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportPluginAttribute : ExportAttribute
    {
        public ExportPluginAttribute()
        {
        }

        public ExportPluginAttribute(string contractName)
            : base(contractName)
        {
        }

        public ExportPluginAttribute(Type contractType)
            : base(contractType)
        {
        }

        public ExportPluginAttribute(string contractName, Type contractType)
            : base(contractName, contractType)
        {
        }

        /// <summary>
        /// Gets or sets the official name of this plug-in.
        /// </summary>
        public string PluginName { get; set; }

        /// <summary>
        /// Gets or sets a description of what the plug-in does.
        /// </summary>
        public string PluginDescription { get; set; }

        /// <summary>
        /// Gets or sets the version of this plug-in.
        /// </summary>
        public string PluginVersion { get; set; }
    }
}