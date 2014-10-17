using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Converters;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Contains information about a advertisement to be scheduled for play within a media.
    /// </summary>
    public class Advertisement : DependencyObject, INotifyPropertyChanged
    {
        private Uri _adSource;
        private Uri _clickThrough;
        private DeliveryMethods _deliveryMethod;

        private TimeSpan? _duration;
        private bool _pauseTimeline = true;
        private bool _isLinearClip = false;
        private TimeSpan? _startTime;

        public Advertisement()
        {
        }

        private Advertisement(Advertisement advertisement)
        {
            IsLinearClip = advertisement._isLinearClip;
            AdSource = advertisement.AdSource;
            ClickThrough = advertisement.ClickThrough;
            DeliveryMethod = advertisement.DeliveryMethod;
            Duration = advertisement.Duration;
            PauseTimeline = advertisement.PauseTimeline;
            StartTime = advertisement.StartTime;
        }

        /// <summary>
        /// Gets or sets the source of the advertisement content.
        /// </summary>
        [XmlIgnore]
        public Uri AdSource
        {
            get { return _adSource; }
            set
            {
                if (_adSource != value)
                {
                    _adSource = value;
                    NotifyPropertyChanged("AdSource");
                }
            }
        }

        /// <summary>
        /// Text representation of AdSource.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AdSourceText
        {
            get
            {
                return AdSource != null
                           ? AdSource.ToString()
                           : string.Empty;
            }

            set
            {
                Uri result;
                AdSource = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)
                                  ? result
                                  : null;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the advertisement to be played.
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
        /// Gets or sets the uri to direct a user to when they click on the advertisement.
        /// </summary>
        [XmlIgnore]
        public Uri ClickThrough
        {
            get { return _clickThrough; }
            set
            {
                if (_clickThrough != value)
                {
                    _clickThrough = value;
                    NotifyPropertyChanged("ClickThrough");
                }
            }
        }

        /// <summary>
        /// Text representation of ClickThrough.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ClickThroughText
        {
            get
            {
                return ClickThrough != null
                           ? ClickThrough.ToString()
                           : string.Empty;
            }

            set
            {
                Uri result;
                ClickThrough = Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result)
                                  ? result
                                  : null;
            }
        }

        /// <summary>
        /// Gets or sets the delivery method to be used to play the advertisement content.
        /// </summary>
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
        /// Gets or sets whether the ad should be scheduled as a linear clip (for live scenarios).
        /// </summary>
        public bool IsLinearClip
        {
            get { return _isLinearClip; }
            set
            {
                if (_isLinearClip != value)
                {
                    _isLinearClip = value;
                    NotifyPropertyChanged("IsLinearClip");
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the timeline of the media should be paused when the advertisment is playing.
        /// </summary>
        public bool PauseTimeline
        {
            get { return _pauseTimeline; }
            set
            {
                if (_pauseTimeline != value)
                {
                    _pauseTimeline = value;
                    NotifyPropertyChanged("PauseTimeline");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position within the media where this advertisment should begin playing.
        /// </summary>
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    NotifyPropertyChanged("StartTime");
                }
            }
        }

        public Advertisement Clone()
        {
            return new Advertisement(this);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

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