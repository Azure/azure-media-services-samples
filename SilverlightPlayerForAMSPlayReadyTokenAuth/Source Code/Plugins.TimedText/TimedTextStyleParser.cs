using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using TimedText;
using TimedText.Styling;
using Extent = Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions.Extent;
using Origin = Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions.Origin;
using Style = Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions.TimedTextStyle;
using TimedTextExtent = TimedText.Styling.Extent;
using TimedTextOrigin = TimedText.Styling.Origin;
using TimedTextStyling = TimedText.Styling;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.TimedText
{
    /// <summary>
    /// Parses style information about a timed text marker.
    /// </summary>
    /// <remarks>
    /// Style information determines the appearance of the marker text.
    /// This class uses the style information defined in XML and creates corresponding 
    /// Style objects.
    /// Styles are defined in the W3C TTAF 1.0 specification.
    /// </remarks>
    internal static class TimedTextStyleParser
    {
        private static readonly Style DefaultStyle = new Style();

        internal static bool IsValidAnimationPropertyName(string name)
        {
            bool ret = false;
            switch (name)
            {
                case "backgroundColor":
                    ret = true;
                    break;
                case "color":
                    ret = true;
                    break;
                case "displayAlign":
                    ret = true;
                    break;
                case "display":
                    ret = true;
                    break;
                case "extent":
                    ret = true;
                    break;
                case "fontFamily":
                    ret = true;
                    break;
                case "fontSize":
                    ret = true;
                    break;
                case "fontStyle":
                    ret = true;
                    break;
                case "fontWeight":
                    ret = true;
                    break;
                case "lineHeight":
                    ret = true;
                    break;
                case "opacity":
                    ret = true;
                    break;
                case "origin":
                    ret = true;
                    break;
                case "overflow":
                    ret = true;
                    break;
                case "padding":
                    ret = true;
                    break;
                case "showBackground":
                    ret = true;
                    break;
                case "textAlign":
                    ret = true;
                    break;
                case "visibility":
                    ret = true;
                    break;
                case "wrapOption":
                    ret = true;
                    break;
                case "zIndex":
                    ret = true;
                    break;
            }
            return ret;
        }

        static LengthUnit FromUnit(Unit unit)
        {
            switch (unit)
            {
                case Unit.Cell:
                    return LengthUnit.Cell;
                case Unit.Em:
                    return LengthUnit.Em;
                case Unit.Percent:
                    return LengthUnit.Percent;
                case Unit.Pixel:
                    return LengthUnit.Pixel;
                case Unit.PixelProportional:
                    return LengthUnit.PixelProportional;
                default:
                    throw new ArgumentException("Unexpected unit type");
            }
            return LengthUnit.Percent;
        }

        static Dictionary<string, NumberPair> numberPairXref = new Dictionary<string, NumberPair>();
        static NumberPair GetNumberPair(string value)
        {
            if (numberPairXref.ContainsKey(value))
            {
                return new NumberPair(numberPairXref[value]);
            }
            else
            {
                NumberPair pair = new NumberPair(value);
                numberPairXref.Add(value, new NumberPair(pair));
                return pair;
            }
        }

        internal static Style MapStyle(TimedTextElementBase styleElement, RegionElement region)
        {
            var style = new Style();
            if (styleElement.Id != null)
            {
                style.Id = styleElement.Id;
            }


            var backgroundColor =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.BackgroundColor.LocalName, region)
                as Color?;
            style.BackgroundColor = backgroundColor.GetValueOrDefault(DefaultStyle.BackgroundColor);

            var color =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Color.LocalName, region) as Color?;
            style.Color = color.GetValueOrDefault(DefaultStyle.Color);

            var extent =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Extent.LocalName, region) as
                TimedTextExtent;
            if (extent != null)
            {
                Length height = new Length { Value = extent.Height, Unit = FromUnit(extent.UnitMeasureVertical) };
                Length width = new Length { Value = extent.Width, Unit = FromUnit(extent.UnitMeasureHorizontal) };
                style.Extent = new Extent { Height = height, Width = width };
            }
            else
            {
                style.Extent = DefaultStyle.Extent;
            }

            var fontFamily =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.FontFamily.LocalName, region) as
                string;
            style.FontFamily = !fontFamily.IsNullOrWhiteSpace()
                                   ? new FontFamily(fontFamily)
                                   : DefaultStyle.FontFamily;

            object oFontSize = styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.FontSize.LocalName,
                                                             region);
            var fontSize = oFontSize as string;
            if (!fontSize.IsNullOrWhiteSpace())
            {
                var parsedFontSize = GetNumberPair(fontSize);
                style.FontSize = new Length
                {
                    Unit = FromUnit(parsedFontSize.UnitMeasureHorizontal),
                    Value = parsedFontSize.First
                };
            }

            var fontStyle =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.FontStyle.LocalName, region) as
                FontStyleAttributeValue?;
            style.FontStyle = fontStyle.HasValue &&
                              (fontStyle.Value == FontStyleAttributeValue.Italic ||
                               fontStyle.Value == FontStyleAttributeValue.Oblique ||
                               fontStyle.Value == FontStyleAttributeValue.ReverseOblique)
                                  ? FontStyles.Italic
                                  : DefaultStyle.FontStyle;

            var fontWeight =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.FontWeight.LocalName, region) as
                FontWeightAttributeValue?;
            style.FontWeight = fontWeight.HasValue && fontWeight.Value == FontWeightAttributeValue.Bold
                                   ? FontWeights.Bold
                                   : DefaultStyle.FontWeight;

            var lineHeight =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.LineHeight.LocalName, region) as
                LineHeight;
            style.LineHeight = lineHeight != null && !(lineHeight is NormalHeight)
                                   ? new Length
                                   {
                                       Unit = FromUnit(lineHeight.UnitMeasureVertical),
                                       Value = lineHeight.Height
                                   }
                                   : null;


            var textOutline = styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.TextOutline.LocalName, region) as TextOutline;
            style.OutlineBlur = new Length
            {
                Unit = FromUnit(textOutline.UnitMeasureBlur),
                Value = textOutline.Blur
            };
            style.OutlineWidth = new Length
            {
                Unit = FromUnit(textOutline.UnitMeasureWidth),
                Value = textOutline.Width
            };
            style.OutlineColor = textOutline.StrokeColor;

            var opacity =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Opacity.LocalName, region) as
                double?;
            style.Opacity = opacity.HasValue
                                ? opacity.Value
                                : DefaultStyle.Opacity;

            var origin =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Origin.LocalName, region) as
                TimedTextOrigin;
            style.Origin = origin != null
                               ? new Origin
                               {
                                   Left = new Length
                                   {
                                       Unit =
                                           FromUnit(origin.UnitMeasureHorizontal),
                                       Value = origin.X
                                   },
                                   Top = new Length
                                   {
                                       Unit = FromUnit(origin.UnitMeasureVertical),
                                       Value = origin.Y
                                   }
                               }
                               : DefaultStyle.Origin;

            var overflow =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Overflow.LocalName, region) as
                string;
            Overflow parsedOverflow;
            style.Overflow = overflow.EnumTryParse(true, out parsedOverflow)
                                 ? parsedOverflow
                                 : DefaultStyle.Overflow;

            var padding =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Padding.LocalName, region) as
                PaddingThickness;
            style.Padding = padding != null
                                ? new Padding
                                {
                                    Left = new Length
                                    {
                                        Unit = FromUnit(padding.WidthStartUnit),
                                        Value = padding.WidthStart
                                    },
                                    Right = new Length
                                    {
                                        Unit = FromUnit(padding.WidthEndUnit),
                                        Value = padding.WidthEnd
                                    },
                                    Top = new Length
                                    {
                                        Unit = FromUnit(padding.WidthBeforeUnit),
                                        Value = padding.WidthBefore
                                    },
                                    Bottom = new Length
                                    {
                                        Unit = FromUnit(padding.WidthAfterUnit),
                                        Value = padding.WidthAfter
                                    }
                                }
                                : DefaultStyle.Padding;

            var textAlign =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.TextAlign.LocalName, region) as
                string;
            TextAlignment parsedTextAlign;
            if (textAlign == "start") textAlign = "left";
            else if (textAlign == "end") textAlign = "right";
            style.TextAlign = textAlign.EnumTryParse(true, out parsedTextAlign)
                                  ? parsedTextAlign
                                  : DefaultStyle.TextAlign;

            var direction =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Direction.LocalName, region) as
                string;
            style.Direction = direction == "rtl" ? Direction.RightToLeft : Direction.LeftToRight;

            var displayAlign =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.DisplayAlign.LocalName, region) as
                string;
            DisplayAlign parsedDisplayAlign;
            style.DisplayAlign = displayAlign.EnumTryParse(true, out parsedDisplayAlign)
                                     ? parsedDisplayAlign
                                     : DefaultStyle.DisplayAlign;

            var visibility =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Visibility.LocalName, region) as
                string;
            style.Visibility = !visibility.IsNullOrWhiteSpace() &&
                               visibility.Equals("hidden", StringComparison.CurrentCultureIgnoreCase)
                                   ? Visibility.Collapsed
                                   : DefaultStyle.Visibility;

            var display =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.Display.LocalName, region) as
                string;
            style.Display = !display.IsNullOrWhiteSpace() &&
                            display.Equals("none", StringComparison.CurrentCultureIgnoreCase)
                                ? Visibility.Collapsed
                                : DefaultStyle.Display;

            var wrapOption =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.WrapOption.LocalName, region) as
                string;
            TextWrapping parsedWrapOption;
            style.WrapOption = wrapOption.EnumTryParse(true, out parsedWrapOption)
                                   ? parsedWrapOption
                                   : DefaultStyle.WrapOption;

            var showBackground =
                styleElement.GetComputedStyle(TimedTextVocabulary.Attributes.Styling.ShowBackground.LocalName, region)
                as string;
            ShowBackground parsedShowBackground;
            style.ShowBackground = showBackground.EnumTryParse(true, out parsedShowBackground)
                                       ? parsedShowBackground
                                       : DefaultStyle.ShowBackground;

            object zindex = styleElement.GetComputedStyle("zIndex", null);
            try
            {
                if (zindex is string == false)
                {
                    var tmp = (double)zindex;
                    style.ZIndex = (int)tmp;
                }
                else
                {
                    style.ZIndex = 0;
                }
            }
            catch
            {
                style.ZIndex = 0;
            }
            return style;
        }


        private static TResult ConvertEnum<TSource, TResult>(TSource source)
            where TSource : struct
            where TResult : struct
        {
            TResult result;
            return source.ToString().EnumTryParse(true, out result)
                       ? result
                       : default(TResult);
        }
    }
}