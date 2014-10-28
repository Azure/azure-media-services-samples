using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    public class MediaTrack : IMediaTrack
    {
        private const string NameAttribute = "name";
        private const string LanguageAttribute = "language";
        private const string HeightAttribute = "height";
        private const string WidthAttribute = "width";
        private const string MaxHeightAttribute = "maxheight";
        private const string MaxWidthAttribute = "maxwidth";

        private readonly TrackInfo _trackInfo;

        public MediaTrack(TrackInfo trackInfo)
        {
            if (trackInfo == null) throw new ArgumentNullException("trackInfo");
            _trackInfo = trackInfo;
        }

        internal TrackInfo TrackInfo
        {
            get { return _trackInfo; }
        }

        #region IMediaTrack Members
        /// <summary>
        /// Gets the name of this IMediaTrack.
        /// </summary>
        public string Name
        {
            get { return _trackInfo.Attributes.GetEntryIgnoreCase(NameAttribute); }
        }

        /// <summary>
        /// Gets whether this IMediaTrack is allowed.
        /// </summary>
        public bool Allowed
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the index of this IMediaTrack within it's parent stream.
        /// </summary>
        public int Index
        {
            get { return (int) _trackInfo.Index; }
        }

        /// <summary>
        /// Gets the language of this IMediaTrack.
        /// </summary>
        public string Language
        {
            get { return _trackInfo.Attributes.GetEntryIgnoreCase(LanguageAttribute); }
        }

        /// <summary>
        /// Gets the bitrate of this IMediaTrack.
        /// </summary>
        public long Bitrate
        {
            get { return (long) _trackInfo.Bitrate; }
        }

        /// <summary>
        /// Gets the attributes that are a part of this IMediaTrack.
        /// </summary>
        public IDictionary<string, string> CustomAttributes
        {
            get { return _trackInfo.CustomAttributes; }
        }

        /// <summary>
        /// Gets the attributes that are a part of this IMediaTrack.
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get { return _trackInfo.Attributes; }
        }

        /// <summary>
        /// Gets the resolution of this IMediaTrack.
        /// </summary>
        public Size Resolution
        {
            get
            {
                string heightStr = _trackInfo.Attributes.GetEntryIgnoreCase(MaxHeightAttribute) ?? _trackInfo.Attributes.GetEntryIgnoreCase(HeightAttribute);
                string widthStr = _trackInfo.Attributes.GetEntryIgnoreCase(MaxWidthAttribute) ?? _trackInfo.Attributes.GetEntryIgnoreCase(WidthAttribute);
                double height, width;
                return double.TryParse(heightStr, out height)
                       && double.TryParse(widthStr, out width)
                           ? new Size(width, height)
                           : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the stream that contains this IMediaTrack.
        /// </summary>
        public IMediaStream ParentStream
        {
            get { return new MediaStream(_trackInfo.Stream); }
        }

        #endregion

        public override bool Equals(object obj)
        {
            var mediaTrack = obj as MediaTrack;
            return mediaTrack != null && mediaTrack.TrackInfo == TrackInfo;
        }

        public override int GetHashCode()
        {
            return _trackInfo.GetHashCode();
        }
    }
}