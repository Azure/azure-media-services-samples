using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
	/// <summary>
	/// A script accessible stereoscopic 3D properties item.
	/// </summary>
	[ScriptableType]
	public class ScriptS3DProperties
	{
		public ScriptS3DProperties(S3DProperties item)
        {
			S3DFormat = item.S3DFormat.ToString();
			S3DContent = item.S3DContent.ToString();
			S3DEyePriority = item.S3DEyePriority.ToString();
			S3DSubsamplingModes = item.S3DSubsamplingModes.ToString();
			S3DSubsamplingOrders = item.S3DSubsamplingOrders.ToString();
			S3DLeftEyePAR = item.S3DLeftEyePAR;
			S3DRightEyePAR = item.S3DRightEyePAR;
        }

		public ScriptS3DProperties()
        {
			S3DFormat = string.Empty;
			S3DContent = string.Empty;
			S3DEyePriority = string.Empty;
			S3DSubsamplingModes = string.Empty;
			S3DSubsamplingOrders = string.Empty;
			S3DLeftEyePAR = 1.0;
			S3DRightEyePAR = 1.0;
        }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D format of the media.
		/// </summary>
		[ScriptableMember]
		public string S3DFormat { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D content type of the media.
		/// </summary>
		[ScriptableMember]
		public string S3DContent { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D eye priority of the media.
		/// </summary>
		[ScriptableMember]
		public string S3DEyePriority { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D subsampling mode of the media.
		/// </summary>
		[ScriptableMember]
		public string S3DSubsamplingModes  { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D subsampling order the media.
		/// </summary>
		[ScriptableMember]
		public string S3DSubsamplingOrders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D left eye pixel aspect ratio the media.
		/// </summary>
		[ScriptableMember]
		public double S3DLeftEyePAR  { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D right eye pixel aspect ratio the media.
		/// </summary>
		[ScriptableMember]
		public double S3DRightEyePAR { get; set; }

		/// <summary>
		/// Converts this ScriptS3DProperties to a proper S3DProperties object
		/// </summary>
		/// <returns></returns>
		public S3DProperties ConvertToS3DProperties()
		{
			S3DProperties s3DProperties = new S3DProperties();

			s3DProperties.S3DContent = Enum.IsDefined(typeof(S3D_Contents),S3DContent) ? (S3D_Contents)Enum.Parse(typeof(S3D_Contents), S3DContent, true) : S3D_Contents.None;
			s3DProperties.S3DEyePriority = Enum.IsDefined(typeof(S3D_EyePriorities),S3DEyePriority) ? (S3D_EyePriorities)Enum.Parse(typeof(S3D_EyePriorities), S3DEyePriority, true) : S3D_EyePriorities.LeftFirst;
			s3DProperties.S3DFormat = Enum.IsDefined(typeof(S3D_Formats), S3DFormat) ? (S3D_Formats)Enum.Parse(typeof(S3D_Formats), S3DFormat, true) : S3D_Formats.DiscreteTrack;
			s3DProperties.S3DLeftEyePAR = S3DLeftEyePAR;
			s3DProperties.S3DRightEyePAR = S3DRightEyePAR;
			s3DProperties.S3DSubsamplingModes = Enum.IsDefined(typeof(S3D_SubsamplingModes), S3DSubsamplingModes) ? (S3D_SubsamplingModes)Enum.Parse(typeof(S3D_SubsamplingModes), S3DSubsamplingModes, true) : S3D_SubsamplingModes.None;
			s3DProperties.S3DSubsamplingOrders = Enum.IsDefined(typeof(S3D_SubsamplingOrders), S3DSubsamplingOrders) ? (S3D_SubsamplingOrders)Enum.Parse(typeof(S3D_SubsamplingOrders), S3DSubsamplingOrders, true) : S3D_SubsamplingOrders.None;
			
			return s3DProperties;
		}
	}
}
