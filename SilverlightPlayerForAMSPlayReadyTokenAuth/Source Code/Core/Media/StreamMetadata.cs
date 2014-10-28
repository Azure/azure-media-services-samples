using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents information about an audio stream.
    /// </summary>
    [ScriptableType]
    public class StreamMetadata : DependencyObject, INotifyPropertyChanged
    {
        private const string NameAttribute = "name";
        private const string LanguageAttribute = "language";

        private IDictionary<string, string> _attributes;
        private string _id;

        /// <summary>
        /// Gets or sets the id of the stream.
        /// </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        [ScriptableMember]
        public string Name
        {
            get { return _attributes.GetEntryIgnoreCase(NameAttribute); }
        }

        /// <summary>
        /// 
        /// </summary>
        [ScriptableMember]
        public string Language
        {
            get { return _attributes.GetEntryIgnoreCase(LanguageAttribute); }
        }

        /// <summary>
        /// Gets or sets a description for the audio stream.
        /// </summary>
        [ScriptableMember]
        public IDictionary<string, string> Attributes
        {
            get { return _attributes; }
            set
            {
                if (_attributes != value)
                {
                    _attributes = value;
                    NotifyPropertyChanged("Attributes");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var audioStreamMetadata = obj as StreamMetadata;
            return audioStreamMetadata != null
                   && audioStreamMetadata.Id == Id;
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