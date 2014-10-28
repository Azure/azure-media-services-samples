using Microsoft.SilverlightMediaFramework.Utilities;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents the padding value to be applied to a caption property.
    /// </summary>
    public class Padding : ObservableObject
    {
        private static Padding _empty;
        private Length _bottom;
        private Length _left;
        private Length _right;
        private Length _top;

        /// <summary>
        /// Gets or sets the left padding space.
        /// </summary>
        public Length Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    NotifyPropertyChanged("Left");
                }
            }
        }

        /// <summary>
        /// Gets or sets the right padding space.
        /// </summary>
        public Length Right
        {
            get { return _right; }
            set
            {
                if (_right != value)
                {
                    _right = value;
                    NotifyPropertyChanged("Right");
                }
            }
        }

        /// <summary>
        /// Gets or sets the top padding space.
        /// </summary>
        public Length Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    NotifyPropertyChanged("Top");
                }
            }
        }

        /// <summary>
        /// Gets or sets the bottom padding space.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom; }
            set
            {
                if (_bottom != value)
                {
                    _bottom = value;
                    NotifyPropertyChanged("Bottom");
                }
            }
        }

        /// <summary>
        /// Gets an instance of an empty padding.
        /// </summary>
        public static Padding Empty
        {
            get
            {
                _empty = _empty ?? new Padding
                                    {
                                        Bottom = new Length(),
                                        Left = new Length(),
                                        Right = new Length(),
                                        Top = new Length()
                                    };

                return _empty;
            }
        }

        public Thickness ToThickness(Size? containerSize = null)
        {
            containerSize = containerSize ?? Size.Empty;

            return new Thickness
            {
                Left = Left.ToPixelLength(containerSize.Value.Width),
                Right = Right.ToPixelLength(containerSize.Value.Width),
                Top = Top.ToPixelLength(containerSize.Value.Height),
                Bottom = Bottom.ToPixelLength(containerSize.Value.Height)
            };
        }
    }
}