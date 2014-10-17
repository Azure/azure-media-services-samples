using System;
using System.Collections.Generic;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    /// <summary>
    /// A script accessible playlist item.
    /// </summary>
    [ScriptableType]
    public class ScriptPlaylistItem
    {
        private readonly List<ScriptChapter> _chapters = new List<ScriptChapter>();
        private readonly List<ScriptMediaMarker> _markers = new List<ScriptMediaMarker>();

        public ScriptPlaylistItem(PlaylistItem item)
        {
            foreach (var marker in item.TimelineMarkers)
            {
                _markers.Add(new ScriptMediaMarker(marker));
            }
            FileSize = item.FileSize;
            FrameRate = item.FrameRate;
            JumpToLive = item.JumpToLive;
            if (MediaSource != null)
            {
                MediaSource = item.MediaSource.ToString();
            }
            if (ThumbSource != null)
            {
                ThumbSource = item.ThumbSource.ToString();
            }
            Description = item.Description;
            Title = item.Title;
            VideoStretchMode = item.VideoStretchMode.ToString();
            VideoWidth = item.VideoWidth;
            VideoHeight = item.VideoHeight;
            if (item.S3DProperties == null)
            {
                ScriptS3DProperties = new ScriptS3DProperties();
            }
            else
            {
                ScriptS3DProperties = new ScriptS3DProperties(item.S3DProperties);
            }
			CustomMetadata = item.CustomMetadata;
        }

        public ScriptPlaylistItem()
        {
            FileSize = 0;
            FrameRate = 30;
            JumpToLive = false;
            MediaSource = null;
            ThumbSource = null;
            Description = string.Empty;
            Title = String.Empty;
            VideoStretchMode = "Uniform";
            VideoWidth = double.NaN;
            VideoHeight = double.NaN;
			ScriptS3DProperties = new ScriptS3DProperties();
			CustomMetadata = new MetadataCollection();
        }

        [ScriptableMember]
        public string BearerToken { get; set; }

        /// <summary>
        /// Gets the number of markers within this playlist item.
        /// </summary>
        public int MarkerCount
        {
            get { return _markers.Count; }
        }

        /// <summary>
        /// Gets the number of chapters within this playlist item.
        /// </summary>
        public int ChapterCount
        {
            get { return _chapters.Count; }
        }

        /// <summary>
        /// Gets or sets the file size of the playlist item.
        /// </summary>
        [ScriptableMember]
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the frame rate of the playlist item.
        /// </summary>
        [ScriptableMember]
        public double FrameRate { get; set; }

        /// <summary>
        /// Gets or sets whether the player should jump to the live position within the specified media when this playlist item begins playing.  Only respected for live media.
        /// </summary>
        [ScriptableMember]
        public bool JumpToLive { get; set; }

        /// <summary>
        /// Gets or sets the source of the media to play when this playlist item is loaded.
        /// </summary>
        [ScriptableMember]
        public string MediaSource { get; set; }

        /// <summary>
        /// Gets or sets a thumbnail image for this playlist item.
        /// </summary>
        [ScriptableMember]
        public string ThumbSource { get; set; }

        /// <summary>
        /// Gets or sets a description of this playlist item.
        /// </summary>
        [ScriptableMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of this playlist item.
        /// </summary>
        [ScriptableMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets how video should be stretched within the display window.
        /// </summary>
        [ScriptableMember]
        public string VideoStretchMode { get; set; }

        /// <summary>
        /// Gets or sets the specific width of the video window used to display this media.
        /// </summary>
        [ScriptableMember]
        public double VideoWidth { get; set; }

        /// <summary>
        /// Gets or sets the specific height of the video window used to display this media.
        /// </summary>
        [ScriptableMember]
        public double VideoHeight { get; set; }

        /// <summary>
        /// Gets or sets the delivery method to be used for the media specified in this playlist item.
        /// </summary>
        [ScriptableMember]
        public string DeliveryMethod { get; set; }

		/// <summary>
		/// Gets or sets the stereoscopic 3D properties specified in this playlist item.
		/// </summary>
		[ScriptableMember]
		public ScriptS3DProperties ScriptS3DProperties { get; set; }

		/// <summary>
		/// Gets or sets the custom metadata specified in this playlist item.
		/// </summary>
		[ScriptableMember]
		public MetadataCollection CustomMetadata { get; set; }

        /// <summary>
        /// Adds a marker to this playlist item.
        /// </summary>
        /// <param name="marker">The marker to add to this playlist item.</param>
        [ScriptableMember]
        public void AddMarker(ScriptMediaMarker marker)
        {
            _markers.Add(marker);
        }

        /// <summary>
        /// Gets the marker at the specified index within this playlist item.
        /// </summary>
        /// <param name="index">The index of the marker.</param>
        /// <returns>The marker at the specified index within this playlist item.</returns>
        [ScriptableMember]
        public ScriptMediaMarker GetMarker(int index)
        {
            ScriptMediaMarker marker = _markers[index];
            return marker;
        }

        /// <summary>
        /// Adds a chapter to this playlist item.
        /// </summary>
        /// <param name="chapter">The chapter to add to this playlist item.</param>
        [ScriptableMember]
        public void AddChapter(ScriptChapter chapter)
        {
            _chapters.Add(chapter);
        }

        /// <summary>
        /// Gets the chapter at the specified index within this playlist item.
        /// </summary>
        /// <param name="index">The index of the chapter.</param>
        /// <returns>The chapter at the specified index within this playlist item.</returns>
        [ScriptableMember]
        public ScriptChapter GetChapter(int index)
        {
            ScriptChapter chapter = _chapters[index];
            return chapter;
        }
    }
}