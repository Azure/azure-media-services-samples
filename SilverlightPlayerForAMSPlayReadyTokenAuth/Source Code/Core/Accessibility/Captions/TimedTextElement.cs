using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using System.Collections.Specialized;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// The base class for captioning elements.
    /// </summary>
    public class TimedTextElement : MediaMarker
    {
        private TimedTextStyle _currentStyle;
        private TimedTextStyle _style;

        public MediaMarkerCollection<TimedTextElement> Children { get; private set; }

        public TimedTextElement()
        {
            Type = "captionelement";
            Style = new TimedTextStyle();
            Animations = new MediaMarkerCollection<TimedTextAnimation>();
            Children = new MediaMarkerCollection<TimedTextElement>();
        }

        /// <summary>
        /// Gets or sets the Style to be applied to this element.
        /// </summary>
        public TimedTextStyle Style
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    CurrentStyle = Style;
                    NotifyPropertyChanged("Style");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current style of this element.
        /// </summary>
        public TimedTextStyle CurrentStyle
        {
            get { return _currentStyle; }

            protected set
            {
                if (_currentStyle != value)
                {
                    _currentStyle = value;
                    NotifyPropertyChanged("CurrentStyle");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of this caption element.
        /// </summary>
        public TimedTextElementType CaptionElementType { get; set; }

        /// <summary>
        /// Gets or sets the list of animations to be applied to this element.
        /// </summary>
        public MediaMarkerCollection<TimedTextAnimation> Animations { get; private set; }


        public void CalculateCurrentStyle(TimeSpan position)
        {
            var activeAnimations = Animations.WhereActiveAtPosition(position);
            if (activeAnimations.Any())
            {
                var animatedStyle = Style.Clone();
                activeAnimations.ForEach(i => i.MergeStyle(animatedStyle));
                CurrentStyle = animatedStyle;
            }

            Children.WhereActiveAtPosition(position)
                    .ForEach(i => i.CalculateCurrentStyle(position));
        }

        public bool HasAnimations
        {
            get
            {
                return Animations.Any() || Children.Any(i => i.HasAnimations);
            }
        }

    }
}