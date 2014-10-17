namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents an animation that will be applied to a caption
    /// </summary>
    public class TimedTextAnimation : TimedTextElement
    {
        /// <summary>
        /// The property that will be animated.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Will merge this CaptionAnimationElement's Style with the specified Style.
        /// </summary>
        /// <param name="s">The style to merge with</param>
        public void MergeStyle(TimedTextStyle s)
        {
            switch (PropertyName)
            {
                case "backgroundColor":
                    s.BackgroundColor = Style.BackgroundColor;
                    break;
                case "color":
                    s.Color = Style.Color;
                    break;
                case "displayAlign":
                    s.DisplayAlign = Style.DisplayAlign;
                    break;
                case "display":
                    s.Display = Style.Display;
                    break;
                case "extent":
                    s.Extent = Style.Extent;
                    break;
                case "fontFamily":
                    s.FontFamily = Style.FontFamily;
                    break;
                case "fontSize":
                    s.FontSize = Style.FontSize;
                    break;
                case "fontStyle":
                    s.FontStyle = Style.FontStyle;
                    break;
                case "fontWeight":
                    s.FontWeight = Style.FontWeight;
                    break;
                case "lineHeight":
                    s.LineHeight = Style.LineHeight;
                    break;
                case "opacity":
                    s.Opacity = Style.Opacity;
                    break;
                case "origin":
                    s.Origin = Style.Origin;
                    break;
                case "overflow":
                    s.Overflow = Style.Overflow;
                    break;
                case "padding":
                    s.Padding = Style.Padding;
                    break;
                case "showBackground":
                    s.ShowBackground = Style.ShowBackground;
                    break;
                case "textAlign":
                    s.TextAlign = Style.TextAlign;
                    break;
                case "visibility":
                    s.Visibility = Style.Visibility;
                    break;
                case "wrapOption":
                    s.WrapOption = Style.WrapOption;
                    break;
                case "zIndex":
                    s.ZIndex = Style.ZIndex;
                    break;
            }
        }
    }
}