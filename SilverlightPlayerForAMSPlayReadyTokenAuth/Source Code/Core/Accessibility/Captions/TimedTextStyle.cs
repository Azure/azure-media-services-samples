using System.ComponentModel;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Media;
using System;
using System.Xml.Serialization;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents styling settings for a CaptionMediaMarker object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is used to apply text styling settings defined in the W3C Timed Text Markup Language to a CaptionMediaMarker object.
    /// For more detailed information, see http://www.w3.org/TR/ttaf1-dfxp/#styling
    /// </para>
    /// <para>
    /// Note that members have a <c>[ScriptableMember]</c> attribute allowing them to be accessed from your JavaScript code.
    /// </para>
    /// </remarks>
    [ScriptableType]
    public class TimedTextStyle
    {
        private const DisplayAlign DefaultDisplayAlign = DisplayAlign.Before;
        private const double DefaultOpacity = 1;
        private const Overflow DefaultOverflow = Overflow.Hidden;
        private const ShowBackground DefaultShowBackground = ShowBackground.Always;
        private const TextAlignment DefaultTextAlignment = TextAlignment.Center;
        private const Direction DefaultDirection = Direction.LeftToRight;
        private const Visibility DefaultVisibility = Visibility.Visible;
        private const TextWrapping DefaultWrapOption = TextWrapping.Wrap;
        private static readonly Color DefaultColor = Colors.White;
        private static readonly Color DefaultBackgroundColor = Colors.Transparent;
        private static readonly Extent DefaultExtent = Extent.Auto;
        private static readonly FontFamily DefaultFontFamily = new FontFamily("Portable User Interface");
        private static readonly FontStyle DefaultFontStyle = FontStyles.Normal;
        private static readonly FontWeight DefaultFontWeight = FontWeights.Normal;
        private static readonly Origin DefaultOrigin = Origin.Empty;
        private static readonly Padding DefaultPadding = Padding.Empty;
        private static readonly Color DefaultOutlineColor = Colors.Black;

        public TimedTextStyle()
        {
            BackgroundColor = DefaultBackgroundColor;
            Color = DefaultColor;
            DisplayAlign = DefaultDisplayAlign;
            Extent = DefaultExtent;
            FontFamily = DefaultFontFamily;
            FontSize = new Length
            {
                Unit = LengthUnit.Cell,
                Value = 1
            };
            FontStyle = DefaultFontStyle;
            FontWeight = DefaultFontWeight;
            Id = Guid.NewGuid().ToString();
            LineHeight = null;
            Opacity = DefaultOpacity;
            Origin = DefaultOrigin;
            Overflow = DefaultOverflow;
            Padding = DefaultPadding;
            ShowBackground = DefaultShowBackground;
            TextAlign = DefaultTextAlignment;
            Direction = DefaultDirection;
            Visibility = DefaultVisibility;
            WrapOption = DefaultWrapOption;
            OutlineColor = DefaultOutlineColor;
            OutlineWidth = new Length { Value = 0.0, Unit = LengthUnit.Pixel };
            OutlineBlur = new Length { Value = 0.0, Unit = LengthUnit.Pixel };
        }

        /// <summary>
        /// Gets or sets an identifier.
        /// </summary>
        /// <remarks>
        /// The Id is used when polling occurs to determine which of the caption markers are new.
        /// </remarks>
        [ScriptableMember]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the background color for the caption.
        /// </summary>
        [ScriptableMember]
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground text color for the caption.
        /// </summary>
        [ScriptableMember]
        public Color Color { get; set; }

        /// <summary>
        /// Set the outline color for the font
        /// </summary>
        [ScriptableMember]
        public Color OutlineColor { get; set; }

        /// <summary>
        /// Sets the thickness of the font outline (stroke)
        /// </summary>
        [ScriptableMember]
        public Length OutlineWidth { get; set; }

        /// <summary>
        /// Sets the thickess of a shadow around the font
        /// </summary>
        [ScriptableMember]
        public Length OutlineBlur { get; set; }

        /// <summary>
        /// Gets or sets the font family for the caption.
        /// </summary>
        [ScriptableMember]
        [XmlIgnore]
        public FontFamily FontFamily { get; set; }

        /// <summary>
        /// Text representation of FontFamily.  Really only here to support XML serialization.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FontFamilyText
        {
            get
            {
                return FontFamily != null
                           ? FontFamily.Source
                           : string.Empty;
            }

            set
            {
                FontFamily = !string.IsNullOrEmpty(value)
                                 ? new FontFamily(value)
                                 : null;
            }
        }

        /// <summary>
        /// Gets or sets the text size for the caption.
        /// </summary>
        /// <remarks>
        /// The only unit of measurement supported is pixels (px).
        /// </remarks>
        [ScriptableMember]
        public Length FontSize { get; set; }

        /// <summary>
        /// Gets or sets the text style for the caption.
        /// </summary>
        /// <remarks>
        /// Only Normal and Italic styles are supported.
        /// </remarks>
        [ScriptableMember]
        public FontStyle FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the font weight for the caption.
        /// </summary>
        [ScriptableMember]
        public FontWeight FontWeight { get; set; }

        /// <summary>
        /// Gets or sets the line height for the caption.
        /// </summary>
        /// <remarks>
        /// The only unit of measurement supported is pixels (px).
        /// </remarks>
        [ScriptableMember]
        public Length LineHeight { get; set; }

        /// <summary>
        /// Gets or sets the percent that the caption is transparent (as a value between 0 and 1.0).
        /// </summary>
        [ScriptableMember]
        public double Opacity { get; set; }

        /// <summary>
        /// Gets or sets the margin for the caption.
        /// </summary>
        [ScriptableMember]
        public Origin Origin { get; set; }

        /// <summary>
        /// Gets or sets the amount of padding used for the caption.
        /// </summary>
        [ScriptableMember]
        public Padding Padding { get; set; }

        /// <summary>
        /// Gets or sets the text alignment setting for the caption.
        /// </summary>
        /// <remarks>
        /// The only horizontal text alignments supported are left, center, and right.
        /// </remarks>
        [ScriptableMember]
        public TextAlignment TextAlign { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the caption.
        /// </summary>
        [ScriptableMember]
        public Visibility Visibility { get; set; }

        /// <summary>
        /// Gets or sets the direction (left to right or right to left) of the caption.
        /// </summary>
        [ScriptableMember]
        public Direction Direction { get; set; }

        /// <summary>
        /// Gets or sets the display of the caption, this differs from visibility because visibility still takes space.
        /// </summary>
        [ScriptableMember]
        public Visibility Display { get; set; }

        /// <summary>
        /// Gets or sets the text wrapping setting for the caption.
        /// </summary>
        [ScriptableMember]
        public TextWrapping WrapOption { get; set; }

        /// <summary>
        /// Gets or sets the display alignment for the caption.
        /// </summary>
        [ScriptableMember]
        public DisplayAlign DisplayAlign { get; set; }

        /// <summary>
        /// Gets or sets when the background should be displayed for the caption.
        /// </summary>
        [ScriptableMember]
        public ShowBackground ShowBackground { get; set; }

        /// <summary>
        /// Gets or sets the overflow for the caption.
        /// </summary>
        [ScriptableMember]
        public Overflow Overflow { get; set; }

        /// <summary>
        /// Gets or sets the extent for the caption.
        /// </summary>
        [ScriptableMember]
        public Extent Extent { get; set; }

        /// <summary>
        /// Gets or sets the zindex for the caption.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// Creates a memberwise close of this style.
        /// </summary>
        /// <returns></returns>
        public TimedTextStyle Clone()
        {
            return MemberwiseClone() as TimedTextStyle;
        }
    }
}