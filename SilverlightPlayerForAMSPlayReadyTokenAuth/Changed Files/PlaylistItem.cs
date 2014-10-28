using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Browser;
using System.Windows.Media;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Utilities.Converters;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using System.IO;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents a media item in a playlist.
    /// </summary>
    [ScriptableType]
    public partial class PlaylistItem : ObservableObject
    {
        public const double DefaultVideoHeight = double.NaN;
        public const double DefaultVideoWidth = double.NaN;
        public const Stretch DefaultVideoStretchMode = Stretch.Uniform;

        private List<CaptionRegion> _captions;
        private List<Chapter> _chapters;
        private ChunkDownloadStrategy _chunkDownloadStrategy;
        private DeliveryMethods _deliveryMethod;
        private string _mediaType;
		private S3DProperties _s3DProperties; 
        private string _description;
        private long _fileSize;
        private double _frameRate;
        private List<Advertisement> _interstitialAdvertisements;
        private bool _jumpToLive;
        private bool _liveDvrRequired;
        private List<MarkerResource> _markerResources;
        private MetadataCollection _mediaPluginRequiredMetadata;
		private MetadataCollection _customMetadata;
        private Uri _mediaSource;
        private Advertisement _postRollAdvertisement;
        private Advertisement _preRollAdvertisement;
        private string _selectedAudioStreamName;
        private string _selectedAudioStreamLanguage;
        private string _selectedCaptionStreamName;
        private string _selectedCaptionStreamLanguage;
        
        private Uri _thumbSource;
        private List<TimelineMediaMarker> _timelineMarkers;
        private string _title;
        private double _videoHeight;
        private Stretch _videoStretchMode;
        private double _videoWidth;
        private TimeSpan? _startPosition;
        private TimeSpan? _duration;
        private Stream _streamSource;
        private LicenseAcquirer _licenseAcquirer;

        /// <summary>
        /// parameterless constructor required for Edit in Blend.
        /// </summary>
        public PlaylistItem()
        {
            TimelineMarkers = new List<TimelineMediaMarker>();
            Chapters = new List<Chapter>();
            Captions = new List<CaptionRegion>();
            MarkerResources = new List<MarkerResource>();
            InterstitialAdvertisements = new List<Advertisement>();
            MediaPluginRequiredMetadata = new MetadataCollection();
			CustomMetadata = new MetadataCollection();
            ChunkDownloadStrategy = Plugins.Primitives.ChunkDownloadStrategy.Unspecified;
            Title = string.Empty;
            Description = string.Empty;
            ThumbSource = null;
            FileSize = 0;
            JumpToLive = true;
            VideoHeight = DefaultVideoHeight;
            VideoWidth = DefaultVideoWidth;
            VideoStretchMode = DefaultVideoStretchMode;
        }

        private PlaylistItem(PlaylistItem playlistItem)
        {
            CustomMetadata = new MetadataCollection();
            MediaPluginRequiredMetadata = new MetadataCollection();
            playlistItem.MediaPluginRequiredMetadata
                        .Select(i => i.Clone())
                        .ForEach(MediaPluginRequiredMetadata.Add);
			playlistItem.CustomMetadata
						.Select(i => i.Clone())
						.ForEach(CustomMetadata.Add);
            InterstitialAdvertisements = playlistItem.InterstitialAdvertisements
                                                     .Select(i => i.Clone())
                                                     .ToList();

            TimelineMarkers = playlistItem.TimelineMarkers
                                          .Select(i => i.Clone())
                                          .ToList();

            Chapters = playlistItem.Chapters
                                   .Select(i => i.Clone())
                                   .ToList();

            Captions = playlistItem.Captions.ToList();

            playlistItem.MarkerResources.IfNotNull(i => MarkerResources = i.Select(j => j.Clone()).ToList());
            playlistItem.PreRollAdvertisement.IfNotNull(i => PreRollAdvertisement = i.Clone());
            playlistItem.PostRollAdvertisement.IfNotNull(i => PostRollAdvertisement = i.Clone());

            DeliveryMethod = playlistItem.DeliveryMethod;
			S3DProperties = playlistItem.S3DProperties;
            FrameRate = playlistItem.FrameRate;
            LiveDvrRequired = playlistItem.LiveDvrRequired;
            MediaSource = playlistItem.MediaSource;
            SelectedAudioStreamLanguage = playlistItem.SelectedAudioStreamLanguage;
            SelectedAudioStreamName = playlistItem.SelectedAudioStreamName;
            SelectedCaptionStreamLanguage = playlistItem.SelectedCaptionStreamLanguage;
            SelectedCaptionStreamName = playlistItem.SelectedCaptionStreamName;
            StartPosition = playlistItem.StartPosition;
            StreamSource = playlistItem.StreamSource;
            Title = playlistItem.Title;
            Description = playlistItem.Description;
            ThumbSource = playlistItem.ThumbSource;
            FileSize = playlistItem.FileSize;
            JumpToLive = playlistItem.JumpToLive;
            VideoHeight = playlistItem.VideoHeight;
            VideoWidth = playlistItem.VideoWidth;
            VideoStretchMode = playlistItem.VideoStretchMode;
        }

        private string _bearerToken = null;
        [ScriptableMember]
        public string BearerToken
        {
            get { return _bearerToken; }
            set { _bearerToken = value; }
        }


        [XmlIgnore]
        public LicenseAcquirer LicenseAcquirer
        {
            get { return _licenseAcquirer; }
            set
            {
                if (_licenseAcquirer != value)
                {
                    _licenseAcquirer = value;
                    NotifyPropertyChanged("LicenseAcquirer");
                }
            }

        }

        [XmlIgnore]
        public Stream StreamSource
        {
            get { return _streamSource; }
            set
            {
                if (_streamSource != value)
                {
                    _streamSource = value;
                    NotifyPropertyChanged("StreamSource");
                }
            }
        }


        /// <summary>
        /// Indicates the playback position to jump to after the media has opened.
        /// </summary>
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? StartPosition
        {
            get { return _startPosition; }
            set
            {
                if (_startPosition != value)
                {
                    _startPosition = value;
                    NotifyPropertyChanged("StartPosition");
                }
            }
        }

        
        /// <summary>
        /// Indicates the duration of the media.
        /// </summary>
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    NotifyPropertyChanged("Duration");
                }
            }
        }


        /// <summary>
        /// Gets the total file size of the encoded video for this item.
        /// </summary>
        [Category("Media")]
        [ScriptableMember]
        [TypeConverter(typeof(Int64TypeConverter))]
        public long FileSize
        {
            get { return _fileSize; }
            set
            {
                if (_fileSize != value)
                {
                    _fileSize = value;
                    NotifyPropertyChanged("FileSize");
                }
            }
        }


        /// <summary>
        /// Gets or sets the frame rate in FPS as persisted.
        /// </summary>
        [Category("Media")]
        [ScriptableMember]
        public double FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (_frameRate != value)
                {
                    _frameRate = value;
                    NotifyPropertyChanged("FrameRate");
                }
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the player should jump to 
        /// the media live position when playback begins.
        /// </summary>
        [Category("Media")]
        [DefaultValue(false)]
        public bool JumpToLive
        {
            get { return _jumpToLive; }
            set
            {
                if (_jumpToLive != value)
                {
                    _jumpToLive = value;
                    NotifyPropertyChanged("JumpToLive");
                }
            }
        }

        /// <summary>
        /// Gets or sets the location of the media item.
        /// </summary>
        [Category("Common Properties")]
        [ScriptableMember]
        [XmlIgnore]
        public Uri MediaSource
        {
            get { return _mediaSource; }
            set
            {
                if (_mediaSource != value)
                {
                    _mediaSource = value;
                    NotifyPropertyChanged("MediaSource");
                }
            }
        }

        /// <summary>
        /// Text representation of MediaSource.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string MediaSourceText
        {
            get
            {
                return MediaSource != null
                           ? MediaSource.ToString()
                           : string.Empty;
            }

            set
            {
                Uri result;
                MediaSource = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)
                                  ? result
                                  : null;
            }
        }

        /// <summary>
        /// Gets or sets the source of the thumbnail for this item. 
        /// </summary>
        /// <remarks>
        /// Uses a string instead of a URI to facilitate binding and handling cases where 
        /// the thumbnail file is missing without generating a page error.
        /// </remarks>       
        [Category("Common Properties")]
        [ScriptableMember]
        [XmlIgnore]
        public Uri ThumbSource
        {
            get { return _thumbSource; }
            set
            {
                if (_thumbSource != value)
                {
                    _thumbSource = value;
                    NotifyPropertyChanged("ThumbSource");
                }
            }
        }

        /// <summary>
        /// Text representation of ThumbSource.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ThumbSourceText
        {
            get
            {
                return ThumbSource != null
                           ? ThumbSource.ToString()
                           : string.Empty;
            }

            set
            {
                Uri result;
                ThumbSource = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)
                                  ? result
                                  : null;
            }
        }

        /// <summary>
        /// Gets or sets the description of this playlist item.
        /// </summary>
        [Category("Common Properties")]
        [ScriptableMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// Gets or sets the title of the playlist item.
        /// </summary>
        [Category("Common Properties")]
        [ScriptableMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets or sets the stretch mode used to display this item.
        /// </summary>
        /// <remarks>
        /// The default is <c>Stretch.Uniform</c>
        /// </remarks>
        [Category("Media")]
        [ScriptableMember]
        public Stretch VideoStretchMode
        {
            get { return _videoStretchMode; }
            set
            {
                if (_videoStretchMode != value)
                {
                    _videoStretchMode = value;
                    NotifyPropertyChanged("VideoStretchMode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the encoded video for this item.
        /// </summary>
        [Category("Media")]
        [ScriptableMember]
        public double VideoWidth
        {
            get { return _videoWidth; }
            set
            {
                if (_videoWidth != value)
                {
                    _videoWidth = value;
                    NotifyPropertyChanged("VideoWidth");
                }
            }
        }


        /// <summary>
        /// Gets or sets the height of the encoded video for this item.
        /// </summary>
        [Category("Media")]
        [ScriptableMember]
        public double VideoHeight
        {
            get { return _videoHeight; }
            set
            {
                if (_videoHeight != value)
                {
                    _videoHeight = value;
                    NotifyPropertyChanged("VideoHeight");
                }
            }
        }


        [Category("Metadata")]
        [ScriptableMember]
        public MetadataCollection MediaPluginRequiredMetadata
        {
            get { return _mediaPluginRequiredMetadata; }
            set
            {
                if (_mediaPluginRequiredMetadata != value)
                {
                    _mediaPluginRequiredMetadata = value;
                    NotifyPropertyChanged("MediaPluginRequiredMetadata");
                }
            }
        }

		[Category("Metadata")]
		[ScriptableMember]
		public MetadataCollection CustomMetadata
		{
			get { return _customMetadata; }
			set
			{
				if (_customMetadata != value)
				{
					_customMetadata = value;
					NotifyPropertyChanged("CustomMetadata");
				}
			}
		}

        /// <summary>
        /// Gets the chapters for this item.
        /// </summary>
        [Category("Metadata")]
        [ScriptableMember]
        public List<Chapter> Chapters
        {
            get { return _chapters; }
            set
            {
                if (_chapters != value)
                {
                    _chapters = value;
                    NotifyPropertyChanged("Chapters");
                }
            }
        }

        /// <summary>
        /// Gets the marker resources for this item.
        /// </summary>
        [Category("Metadata")]
        [ScriptableMember]
        public List<MarkerResource> MarkerResources
        {
            get { return _markerResources; }
            set
            {
                if (_markerResources != value)
                {
                    _markerResources = value;
                    NotifyPropertyChanged("MarkerResources");
                }
            }
        }

        /// <summary>
        /// Gets or sets the markers for this item.
        /// </summary>
        [Category("Metadata")]
        [ScriptableMember]
        public List<TimelineMediaMarker> TimelineMarkers
        {
            get { return _timelineMarkers; }
            set
            {
                if (_timelineMarkers != value)
                {
                    _timelineMarkers = value;
                    NotifyPropertyChanged("TimelineMarkers");
                }
            }
        }

        [ScriptableMember]
        public List<CaptionRegion> Captions
        {
            get { return _captions; }
            set
            {
                if (_captions != value)
                {
                    _captions = value;
                    NotifyPropertyChanged("Captions");
                }
            }
        }

        [Category("Plug In Configuration")]
        [ScriptableMember]
        public ChunkDownloadStrategy ChunkDownloadStrategy
        {
            get { return _chunkDownloadStrategy; }
            set
            {
                if (_chunkDownloadStrategy != value)
                {
                    _chunkDownloadStrategy = value;
                    NotifyPropertyChanged("ChunkDownloadStrategy");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating the delivery method of the media (progressive, streaming, etc.).
        /// </summary>
        [Category("Common Properties")]
        [ScriptableMember]
        public DeliveryMethods DeliveryMethod
        {
            get { return _deliveryMethod; }
            set
            {
                if (_deliveryMethod != value)
                {
                    _deliveryMethod = value;
                    NotifyPropertyChanged("DeliveryMethod");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the media type. This is useful when using custom media plugins
        /// </summary>
        [Category("Common Properties")]
        [ScriptableMember]
        public string MediaType
        {
            get { return _mediaType; }
            set
            {
                if (_mediaType != value)
                {
                    _mediaType = value;
                    NotifyPropertyChanged("MediaType");
                }
            }
        }

		/// <summary>
		/// Container object for all the S3D Properties used for Stereoscopic 3D display.
		/// </summary>
		[Category("Plug In Configuration")]
		[ScriptableMember]
		public S3DProperties S3DProperties
		{
			get { return _s3DProperties; }
			set
			{
				if (_s3DProperties != value)
				{
					_s3DProperties = value;
					NotifyPropertyChanged("S3DProperties");
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether this playlist item is a live stream.
        /// </summary>
        [Category("Plug In Configuration")]
        [ScriptableMember]
        public bool LiveDvrRequired
        {
            get { return _liveDvrRequired; }
            set
            {
                if (_liveDvrRequired != value)
                {
                    _liveDvrRequired = value;
                    NotifyPropertyChanged("LiveDvrRequired");
                }
            }
        }

        [Category("Advertising")]
        [ScriptableMember]
        public Advertisement PreRollAdvertisement
        {
            get { return _preRollAdvertisement; }

            set
            {
                if (_preRollAdvertisement != value)
                {
                    _preRollAdvertisement = value;
                    NotifyPropertyChanged("PreRollAdvertisement");
                }
            }
        }

        [Category("Advertising")]
        [ScriptableMember]
        public Advertisement PostRollAdvertisement
        {
            get { return _postRollAdvertisement; }

            set
            {
                if (_postRollAdvertisement != value)
                {
                    _postRollAdvertisement = value;
                    NotifyPropertyChanged("PostRollAdvertisement");
                }
            }
        }

        [Category("Advertising")]
        [ScriptableMember]
        public List<Advertisement> InterstitialAdvertisements
        {
            get { return _interstitialAdvertisements; }
            set
            {
                if (_interstitialAdvertisements != value)
                {
                    _interstitialAdvertisements = value;
                    NotifyPropertyChanged("InterstitialAdvertisements");
                }
            }
        }

        [Category("Metadata")]
        [ScriptableMember]
        public string SelectedCaptionStreamName
        {
            get { return _selectedCaptionStreamName; }
            set
            {
                if (_selectedCaptionStreamName != value)
                {
                    _selectedCaptionStreamName = value;
                    NotifyPropertyChanged("SelectedCaptionStreamName");
                }
            }
        }

        [Category("Metadata")]
        [ScriptableMember]
        public string SelectedCaptionStreamLanguage
        {
            get { return _selectedCaptionStreamLanguage; }
            set
            {
                if (_selectedCaptionStreamLanguage != value)
                {
                    _selectedCaptionStreamLanguage = value;
                    NotifyPropertyChanged("SelectedCaptionStreamLanguage");
                }
            }
        }

        [Category("Metadata")]
        [ScriptableMember]
        public string SelectedAudioStreamName
        {
            get { return _selectedAudioStreamName; }
            set
            {
                if (_selectedAudioStreamName != value)
                {
                    _selectedAudioStreamName = value;
                    NotifyPropertyChanged("SelectedAudioStreamName");
                }
            }
        }

        [Category("Metadata")]
        [ScriptableMember]
        public string SelectedAudioStreamLanguage
        {
            get { return _selectedAudioStreamLanguage; }
            set
            {
                if (_selectedAudioStreamLanguage != value)
                {
                    _selectedAudioStreamLanguage = value;
                    NotifyPropertyChanged("SelectedAudioStreamLanguage");
                }
            }
        }

        public PlaylistItem Clone()
        {
            return new PlaylistItem(this);
        }
    }
}
