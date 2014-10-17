using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    /// <summary>
    /// A script accessible media marker.
    /// </summary>
    [ScriptableType]
    public class ScriptMediaMarker
    {
        public ScriptMediaMarker()
        {
            Content = "";
            AllowSeek = true;
        }

        public ScriptMediaMarker(MediaMarker marker)
        {
            Content = marker.Content;
            Id = marker.Id;
            Begin = marker.Begin.TotalSeconds;
            End = marker.End.TotalSeconds;
            Type = marker.Type;
        }

        /// <summary>
        /// Gets or sets whether clicking on this marker will cause the player to see to it's position within the media.
        /// </summary>
        [ScriptableMember]
        public bool AllowSeek { get; set; }

        /// <summary>
        /// Gets or sets the content of this media marker.
        /// </summary>
        [ScriptableMember]
        public object Content { get; set; }

        /// <summary>
        /// Gets or sets the id of this media marker.
        /// </summary>
        [ScriptableMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the begin time of this media marker in seconds.
        /// </summary>
        [ScriptableMember]
        public double Begin { get; set; }

        /// <summary>
        /// Gets or sets the end time of this media marker in seconds.
        /// </summary>
        [ScriptableMember]
        public double End { get; set; }

        /// <summary>
        /// Gets or sets the type of this media marker.
        /// </summary>
        [ScriptableMember]
        public string Type { get; set; }
    }
}