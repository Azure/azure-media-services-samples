namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
	/// <summary>
	/// Exports a plugin that implements the IS3DPlugin interface
	/// </summary>
	/// <remarks>
	/// This attribute should be added to classes that implement IS3DPlugin.
	/// </remarks>
	public class ExportS3DPluginAttribute : ExportPluginAttribute
	{
		public ExportS3DPluginAttribute()
			: base(typeof(IS3DPlugin))
		{
		}
	}
}