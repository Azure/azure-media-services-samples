using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Plugins.Resources;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Represents the base class for markers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Markers signify a time location in a media file.
    /// Each marker has a Begin and End time. 
    /// </para>
    /// <para>
    /// There are two specific types of marker objects:
    /// <list type="bullet">
    /// <item><see cref="T:Microsoft.SilverlightMediaFramework.Media.CaptionMediaMarker">CaptionMediaMarker</see> objects represent caption text that can be displayed at a specific time in the media.</item>
    /// /// <item><see cref="T:Microsoft.SilverlightMediaFramework.Media.TimelineMediaMarker">TimelineMediaMarker</see> objects can be displayed on the timeline</item>
    /// </list>
    /// </para>
    /// </remarks>
    [ScriptableType]
    public partial class MediaMarker : INotifyPropertyChanged
    {
        private TimeSpan _begin;
        private object _content;
        private TimeSpan _end;
        private string _id;
        private string _type;

        public event Action<MediaMarker> PositionChanged;

        public MediaMarker()
        {
            _id = Guid.NewGuid().ToString();
        }

        protected MediaMarker(MediaMarker mediaMarker)
        {
            Begin = mediaMarker.Begin;
            Content = mediaMarker.Content;
            End = mediaMarker.End;
            Id = mediaMarker.Id;
            Type = mediaMarker.Type;
        }

        public void NotifyPositionChanged()
        {
            if (PositionChanged != null)
            {
                PositionChanged(this);
            }
        }

        /// <summary>
        /// Gets or sets a unique identifier for the marker.
        /// </summary>
        /// <remarks>
        /// The Id is used to determine which markers are new each time polling occurs.
        /// </remarks>
        [ScriptableMember]
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != null)
                {
                    if (_id != value)
                    {
                        _id = value;
                        NotifyPropertyChanged("Id");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Id");
                }
            }
        }

        /// <summary>
        /// Gets or sets the begin position in the media stream of this marker item.
        /// </summary>
        [ScriptableMember]
        public TimeSpan Begin
        {
            get { return _begin; }

            set
            {
                _begin = value;
                NotifyPropertyChanged("Begin");
                NotifyPropertyChanged("BeginText");
                NotifyPropertyChanged("Duration");
                NotifyPropertyChanged("DurationText");
            }
        }

        /// <summary>
        /// Gets or sets the end position in the media stream of this marker item.
        /// </summary>
        [ScriptableMember]
        public TimeSpan End
        {
            get { return _end; }

            set
            {
                _end = value;
                NotifyPropertyChanged("End");
                NotifyPropertyChanged("EndText");
                NotifyPropertyChanged("Duration");
                NotifyPropertyChanged("DurationText");
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this marker.
        /// </summary>
        [ScriptableMember]
        public virtual object Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        /// <summary>
        /// Gets the duration of this marker (calculated from start time to end time).
        /// </summary>
        /// <remarks>
        /// The property value is calculated (it cannot be set) and is provided as a convenience.
        /// </remarks>
        [ScriptableMember]
        public TimeSpan Duration
        {
            get { return Begin < End ? End.Subtract(Begin) : TimeSpan.Zero; }
        }

        /// <summary>
        /// Gets or sets the type of marker (such as caption or display).
        /// </summary>
        [ScriptableMember]
        public string Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// Gets a text representation of the marker begin time.
        /// </summary>
        public string BeginText
        {
            get { return Format(Begin); }
        }

        /// <summary>
        /// Gets a text representation of the marker duration time.
        /// </summary>
        public string DurationText
        {
            get { return Format(Duration); }
        }

        /// <summary>
        /// Gets a text representation of the marker end time.
        /// </summary>
        public string EndText
        {
            get { return Format(End); }
        }

        public bool IsActiveAtPosition(TimeSpan position)
        {
            return IsActiveAtPosition(this, position);
        }

        public bool IsActiveInRange(TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            return IsActiveInRange(this, rangeStart, rangeEnd);
        }

        public bool IsContainedByRange(TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            return IsContainedByRange(this, rangeStart, rangeEnd);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets a value indicating whether two markers are references to the same marker.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if the markers compared are the same marker; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var mediaMarker = obj as MediaMarker;
            return base.Equals(obj) 
                    || (mediaMarker != null && mediaMarker.Id != null && mediaMarker.Id == Id && mediaMarker.Begin == Begin && mediaMarker.End == End);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static string Format(TimeSpan timeSpan)
        {
            return string.Format(CultureInfo.InvariantCulture,
                                 PluginsResources.TimeSpanReadableFormat,
                                 timeSpan.Hours,
                                 timeSpan.Minutes,
                                 timeSpan.Seconds);
        }

        public static bool IsActiveAtPosition(MediaMarker mediaMarker, TimeSpan position)
        {
            return mediaMarker.Begin <= position && position < mediaMarker.End;
        }

        public static bool IsContainedByRange(MediaMarker mediaMarker, TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            return mediaMarker.Begin > rangeStart
                    && mediaMarker.Begin < rangeEnd
                    && mediaMarker.End > rangeStart
                    && mediaMarker.End < rangeEnd;
        }

        public static bool IsActiveInRange(MediaMarker mediaMarker, TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            return mediaMarker.Begin <= rangeEnd && mediaMarker.End > rangeStart;
        }
        

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            VerifyProperty(propertyName);

            PropertyChangedEventHandler propertyChangedHandler = PropertyChanged;

            if (propertyChangedHandler != null)
            {
                if (Application.Current != null && Application.Current.RootVisual != null)
                {
                    Dispatcher currentDispatcher = Application.Current.RootVisual.Dispatcher;
                    if (currentDispatcher.CheckAccess() == false)
                    {
                        currentDispatcher.BeginInvoke(
                            () => propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName)));
                    }
                    else
                    {
                        propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
                    }
                }
            }
        }

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);

            if (property == null)
            {
                string message = string.Format("Invalid property: {0}", propertyName);
                throw new Exception(message);
            }
        }
    }
}