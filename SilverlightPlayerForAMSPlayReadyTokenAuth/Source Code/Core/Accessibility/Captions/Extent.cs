using Microsoft.SilverlightMediaFramework.Utilities;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents the extent property of a caption.
    /// </summary>
    public class Extent : ObservableObject
    {
        private static Extent _empty;
        private static Extent _auto;
        private Length _height;
        private Length _width;

        public bool IsAuto
        {
            get
            {
                return Width.Unit == Auto.Width.Unit && Width.Value == Auto.Width.Value
                    && Height.Unit == Auto.Height.Unit && Height.Value == Auto.Height.Value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public Length Width
        {
            get { return _width; }

            set
            {
                if (_width != value)
                {
                    _width = value;
                    NotifyPropertyChanged("Width");
                }
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public Length Height
        {
            get { return _height; }

            set
            {
                if (_height != value)
                {
                    _height = value;
                    NotifyPropertyChanged("Height");
                }
            }
        }

        public Size ToPixelSize(Size? containerSize = null)
        {
            containerSize = containerSize ?? Size.Empty;
            
            return new Size
            {
                Height = Height.ToPixelLength(containerSize.Value.Height),
                Width = Width.ToPixelLength(containerSize.Value.Width)
            };
        }

        /// <summary>
        /// Gets or sets the empty value.
        /// </summary>
        public static Extent Empty
        {
            get
            {
                _empty = _empty ?? new Extent
                                       {
                                           Height = new Length(),
                                           Width = new Length()
                                       };

                return _empty;
            }
        }

        /// <summary>
        /// Gets or sets the auto value.
        /// </summary>
        public static Extent Auto
        {
            get
            {
                _auto = _auto ?? new Extent
                                {
                                    Height = new Length { Unit = LengthUnit.Percent, Value = 1 },
                                    Width = new Length { Unit = LengthUnit.Percent, Value = 1 }
                                };
                
                return _auto;
            }
        }
    }
}