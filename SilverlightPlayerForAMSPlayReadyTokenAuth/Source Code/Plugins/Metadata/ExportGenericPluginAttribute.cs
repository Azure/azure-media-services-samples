namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Exports a plugin that implements the IGenericPlugin interface
    /// </summary>
    /// <remarks>
    /// This attribute should be added to classes that implement IGenericPlugin.
    /// </remarks>
    public class ExportGenericPluginAttribute : ExportPluginAttribute
    {
        public ExportGenericPluginAttribute()
            : base(typeof (IGenericPlugin))
        {
        }
    }
}