using System;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    /// <summary>
    /// A script accessible chapter.
    /// </summary>
    public class ScriptChapter
    {
        public ScriptChapter()
        {
            ThumbSource = null;
            Title = String.Empty;
            Description = String.Empty;
        }

        public ScriptChapter(Chapter item)
        {
            ThumbSource = item.ThumbSource.ToString();
            Title = item.Title;
            Description = item.Description;
            Content = item.Content;
            Id = item.Id;
            Begin = item.Begin.TotalSeconds;
            End = item.End.TotalSeconds;
        }

        /// <summary>
        /// Gets or sets the uri source of a thumbnail image for this chapter.
        /// </summary>
        [ScriptableMember]
        public string ThumbSource { get; set; }

        /// <summary>
        /// Gets or sets the title of this chapter.
        /// </summary>
        [ScriptableMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a description of this chapter.
        /// </summary>
        [ScriptableMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content of this chapter.
        /// </summary>
        [ScriptableMember]
        public object Content { get; set; }

        /// <summary>
        /// Gets or sets the id of this chapter.
        /// </summary>
        [ScriptableMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the begin time of this chapter in total seconds.
        /// </summary>
        [ScriptableMember]
        public double Begin { get; set; }

        /// <summary>
        /// Gets or sets the end time of this chapter in total seconds.
        /// </summary>
        [ScriptableMember]
        public double End { get; set; }
    }
}