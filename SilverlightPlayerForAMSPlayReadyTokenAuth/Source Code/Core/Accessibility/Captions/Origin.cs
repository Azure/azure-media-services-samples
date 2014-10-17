using Microsoft.SilverlightMediaFramework.Utilities;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents a point of origination to be applied to a caption 
    /// </summary>
    public class Origin : ObservableObject
    {
        private static Origin _empty;
        private Length _left;


        private Length _right;

        /// <summary>
        /// Gets or sets the left margin of this origin.
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
        /// Gets or sets the top margin of this origin.
        /// </summary>
        public Length Top
        {
            get { return _right; }
            set
            {
                if (_right != value)
                {
                    _right = value;
                    NotifyPropertyChanged("Top");
                }
            }
        }

        public Thickness ToThickness(Size? containerSize = null)
        {
            containerSize = containerSize ?? Size.Empty;

            return new Thickness
            {
                Left = Left.ToPixelLength(containerSize.Value.Width),
                Top = Top.ToPixelLength(containerSize.Value.Height),
            };
        }

        /// <summary>
        /// Gets an instance of an empty origin.
        /// </summary>
        public static Origin Empty
        {
            get
            {
                _empty = _empty ?? new Origin
                                    {
                                        Left = new Length(),
                                        Top = new Length()
                                    };

                return _empty;
            }
        }
    }
}