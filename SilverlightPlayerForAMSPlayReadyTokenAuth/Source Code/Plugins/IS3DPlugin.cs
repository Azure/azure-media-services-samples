using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;
using System;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
	/// <summary>
	/// A Stereoscopic 3D plugin that will be loaded by the Player and given a reference to the ActiveMediaPlugin
	/// </summary>
	public interface IS3DPlugin : IGenericPlugin
	{
		/// <summary>
		/// Gets the 3D formats supported by this plugin.
		/// </summary>
		S3D_Formats SupportedS3DFormats { get; }

		/// <summary>
		/// Allows the MediaPresenterElement to be passed to the 3D plugin
		/// </summary>
		ContentControl MediaPresenterElement { set; }

		/// <summary>
		/// Indicates whether the 3D Plugin is enabled.
		/// Allows a consuming application to instruct the 3D plugin to activate or deactivate.
		/// Useful when a consuming application will handle multuple 3D plugins.
		/// </summary>
		bool Is3DPluginEnabled { get; set; }

		/// <summary>
		/// An event indicating that the S3D Properties have been successfully
		/// validated for this 3D plugin for the current Playlist Item
		/// </summary>
		event EventHandler<S3DPropertiesEventArgs<S3DProperties>> S3DPropertiesValid;

		/// <summary>
		/// An event indicating that the S3D Properties are invalid for this 3D plugin
		/// for the current Playlist Item.
		/// </summary>
		event EventHandler<S3DPropertiesEventArgs<S3DProperties>> S3DPropertiesInvalid;
	}
}