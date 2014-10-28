using System;
using System.ComponentModel;
using System.Windows.Browser;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D
{
	// (Note: Plural enum names here due to Flags attribute):
	// http://msdn.microsoft.com/en-us/library/bb264486(v=vs.80).aspx
	
	/// <summary>
	/// Indicates the 3D format.
	/// 
	/// DiscreteTrack represents a Left or Right stream and is only valid when S3D_Content 
	/// is set to DiscreteLeft or DiscreteRight. 
	/// 
	/// SideBySide represents a frame-compatible formatted stream which contains Left and 
	/// Right images stacked horizontally within the frame. 
	/// 
	/// TopAndBottom represents a frame-compatible formatted stream which contains Left and 
	/// Right images stacked vertically within the frame. 
	///
	/// FrameSequential represents a stream where Left and Right frames alternate in 
	/// chronological order. 
	/// 
	/// FieldAlternative represents an interlaced stream where Left and Right images 
	/// are coded as interlaced fields.
	/// 
	/// SideBySide, TopAndBottom, FrameSequential and FieldAlternative are only valid 
	/// when S3D_Content is set to Pair.
	/// </summary>
	[Flags]
	public enum S3D_Formats
	{
		DiscreteTrack = 0, 
		SideBySide = 0x2,
		TopAndBottom = 0x4,
		FrameSequential = 0x8, 
		FieldAlternative = 0x16
	}

	/// <summary>
	/// None describes a plain 2D source. 
	/// 
	/// Pair describes a typical 3D source which contains both L and R images 
	/// in the same stream. 
	/// 
	/// DiscreteLeft/DiscreteRight describes a single stream which contains only the 
	/// Left or Right images of a stereo pair, but is different than None in 
	/// that it implies that another discrete stream is available which together 
	/// with this one forms a complete 3D pair.
	/// </summary>
	[Flags]
	public enum S3D_Contents
	{
		None = 0,
		Pair = 0x2,
		DiscreteLeft = 0x4,
		DiscreteRight = 0x8
	}

	/// <summary>
	/// LeftFirst indicates left eye images are delivered first in an image pair.
	/// 
	/// RightFirst indicates right eye images are delivered first in an image pair. 
	/// 
	/// “First” means left half in SideBySide; top half in TopAndBottom, even frames in 
	/// FrameSequential; and even lines in FieldAlternative.
	/// </summary>
	[Flags]
	public enum S3D_EyePriorities
	{
		LeftFirst = 0,
		RightFirst = 0x2
	}

	/// <summary>
	/// None indicates no special subsampling algorithm was used to scale the left and right images. 
	/// The other subsampling modes are only valid when S3D_Format is set to SideBySide or TopAndBottom.
	/// </summary>
	[Flags]
	public enum S3D_SubsamplingModes
	{
		None = 0,
		HorizontalSubsampling = 0x2,
		VerticalSubsampling = 0x4,
		QuincuxSubsampling = 0x8
	}

	/// <summary>
	/// None indicates no special subsampling algorithm was used to scale the left and right images. 
	/// The other subsampling orders are only valid when S3D_SubsamplingMode 
	/// is set to values not equal to None.
	/// </summary>
	[Flags]
	public enum S3D_SubsamplingOrders 
	{
		None = 0,
		OddLeft_OddRight = 0x2,
		OddLeft_EvenRight = 0x4,
		EvenLeft_OddRight = 0x8,
		EvenLeft_EvenRight = 0x16
	}

	[ScriptableType]
	public class S3DProperties : DependencyObject
	{
		private S3D_Formats _s3DFormat;
		private S3D_Contents _s3DContent;
		private S3D_EyePriorities _s3DEyePriority;
		private S3D_SubsamplingModes _s3DSubsamplingModes;
		private S3D_SubsamplingOrders _s3DSubsamplingOrders;
		private double _s3DLeftEyePAR = 1.0;
		private double _s3DRightEyePAR = 1.0;

		public S3DProperties()
		{

		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D format of the media.
		/// </summary>
		public S3D_Formats S3DFormat
		{
			get { return _s3DFormat; }
			set { _s3DFormat = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D content type of the media.
		/// </summary>
		public S3D_Contents S3DContent
		{
			get { return _s3DContent; }
			set { _s3DContent = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D eye priority of the media.
		/// </summary>
		public S3D_EyePriorities S3DEyePriority
		{
			get { return _s3DEyePriority; }
			set { _s3DEyePriority = value;}			
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D subsampling mode of the media.
		/// </summary>
		public S3D_SubsamplingModes S3DSubsamplingModes
		{
			get { return _s3DSubsamplingModes; }
			set { _s3DSubsamplingModes = value;}		
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D subsampling order the media.
		/// </summary>
		public S3D_SubsamplingOrders S3DSubsamplingOrders
		{
			get { return _s3DSubsamplingOrders; }
			set { _s3DSubsamplingOrders = value;}	
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D left eye pixel aspect ratio the media.
		/// </summary>
		public double S3DLeftEyePAR
		{
			get { return _s3DLeftEyePAR; }
			set { _s3DLeftEyePAR = value;}	
		}

		/// <summary>
		/// Gets or sets a value indicating the stereoscopic 3D right eye pixel aspect ratio the media.
		/// </summary>
		public double S3DRightEyePAR
		{
			get { return _s3DRightEyePAR; }
			set { _s3DRightEyePAR = value;}	
		}
	}
}
