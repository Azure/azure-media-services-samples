namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Exports a plugin that implement the IUIPlugin interface.
    /// </summary>
    public class ExportUIPluginAttribute : ExportPluginAttribute
    {
        public ExportUIPluginAttribute()
            : base(typeof (IUIPlugin))
        {
        }
    }
}