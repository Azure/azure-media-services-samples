using System;
using System.ComponentModel;
using System.Windows.Browser;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents a chapter marker for the media item.
    /// </summary>
    [ScriptableType]
    public class Chapter : MediaMarker
    {
        private string _description;
        private Uri _thumbSource;
        private string _title = string.Empty;

        public Chapter()
        {
            Type = "chapter";
        }

        private Chapter(Chapter chapter)
            : base(chapter)
        {
            Description = chapter.Description;
            ThumbSource = chapter.ThumbSource;
            Title = chapter.Title;
        }

        public Chapter Clone()
        {
            return new Chapter(this);
        }

        /// <summary>
        /// Gets or sets the source of the thumbnail for this chapter item.
        /// </summary>
        [XmlIgnore]
        public Uri ThumbSource
        {
            get { return _thumbSource; }

            set
            {
                _thumbSource = value;
                NotifyPropertyChanged("ThumbSource");
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
        /// Gets or sets the title of this chapter item.
        /// </summary>
        public string Title
        {
            get { return _title; }

            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the description of this chapter item.
        /// </summary>
        public string Description
        {
            get { return _description; }

            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
    }
}