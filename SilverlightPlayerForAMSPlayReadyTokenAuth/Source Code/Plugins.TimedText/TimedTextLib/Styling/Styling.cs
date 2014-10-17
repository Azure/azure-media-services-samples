
using System.Text.RegularExpressions;
using System;
using System.Windows.Media;
using System.Globalization;
//using System.Windows.Media;
namespace TimedText.Styling
{
    /* ColorExpression:
            tts:backgroundColor
            tts:color
            tts:opacity

            tts:direction

            tts:display

            tts:displayAlign

8.2.7 tts:dynamicFlow

8.2.8 tts:extent

        TextBox.Font*
            tts:fontFamily
            tts:fontSize
            tts:fontStyle
            tts:fontWeight
            tts:lineHeight


8.2.15 tts:origin

8.2.16 tts:overflow

8.2.17 tts:padding

8.2.18 tts:showBackground

8.2.19 tts:textAlign

8.2.20 tts:textDecoration

8.2.21 tts:textOutline

8.2.22 tts:unicodeBidi

8.2.23 tts:visibility

8.2.24 tts:wrapOption

8.2.25 tts:writingMode

8.2.26 tts:zIndex
    */
    public enum WritingMode
    {
        LeftRightTopBottom,
        RightLeftTopBottom,
        TopBottomRightLeft,
        TopBottomLeftRight
    }

    public enum Unit
    {
        Pixel,
        Em,
        Cell,
        Percent,
        PixelProportional
    }


    /// <summary>
    /// Basic rectangle type for reference areas.
    /// </summary>
    public struct Rectangle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    /// <summary>
    /// Lineheight is modeled on NumberPair, but only takes a single value.
    /// </summary>
    public class LineHeight : NumberPair
    {
        /// <summary>
        /// Size in the vertical direction
        /// </summary>
        public double Height
        {
            get
            {
                return Second;
            }
        }

        /// <summary>
        /// Size in the horizontal direction
        /// </summary>
        public double Width
        {
            get
            {
                return First;
            }
        }
        /// <summary>
        /// Parse a line height in a context
        /// </summary>
        /// <param name="expression"></param>
        public LineHeight(string expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Create an absolute line height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public LineHeight(double height)
        {
            HorizontalValue = VerticalValue = height;
            IsRelativeHorizontal = IsRelativeVertical = false;
            IsRelativeFontHorizontal = IsRelativeFontVertical = false;
        }
    }

    /// <summary>
    /// A nnormal lineheight is returned when the specified value is "normal"; 
    /// this determines line height based on its children.
    /// </summary>
    public class NormalHeight : LineHeight
    {
        public NormalHeight()
            : base(-1)
        {
        }
    }

    public class FontSize : NumberPair
    {
        /// <summary>
        /// Size in the horizontal direction
        /// </summary>
        public double FontWidth
        {
            get
            {
                return First;
            }
        }

        /// <summary>
        /// Size in the vertical direction
        /// </summary>
        public double FontHeight
        {
            get
            {
                return Second;
            }
        }

        /// <summary>
        /// Parse a font size expression
        /// </summary>
        /// <param name="expression"></param>
        public FontSize(string expression, FontSize context, double width, double height)
            : base(expression)
        {
            if (context != null)
            {
                SetFontContext(context.FontWidth, context.FontHeight);
            }
            SetContext(width, height);

        }

        /// <summary>
        /// Create an absolute font size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public FontSize(double width, double height)
        {
            HorizontalValue = width;
            VerticalValue = height;
            IsRelativeHorizontal = IsRelativeVertical = false;
            IsRelativeFontHorizontal = IsRelativeFontVertical = false;
        }

        /// <summary>
        /// Create an absolute font size.
        /// </summary>
        /// <param name="size"></param>
        public FontSize(double size)
        {
            VerticalValue = HorizontalValue = size;
            IsRelativeHorizontal = IsRelativeVertical = false;
            IsRelativeFontHorizontal = IsRelativeFontVertical = false;
        }
    }

    /// <summary>
    /// Class to model a position in 2D space.
    /// </summary>
    public class Origin : NumberPair
    {
        /// <summary>
        /// Horizontal position
        /// </summary>
        public double X
        {
            get
            {
                return First;
            }
        }

        /// <summary>
        /// Vertical position
        /// </summary>
        public double Y
        {
            get
            {
                return Second;
            }
        }

        /// <summary>
        /// Parse an extent expression in context
        /// </summary>
        /// <param name="expression"></param>
        public Origin(string expression)
            : base(expression)
        {
            if (UnitMeasureHorizontal == Unit.Percent)
            {
                IsRelativeFontHorizontal = false;
                IsRelativeHorizontal = true;
            }
            if (UnitMeasureVertical == Unit.Percent)
            {
                IsRelativeFontVertical = false;
                IsRelativeVertical = true;
            }
        }

        /// <summary>
        /// Create an absolute position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Origin(double x, double y)
        {
            HorizontalValue = x;
            VerticalValue = y;
            IsRelativeHorizontal = IsRelativeVertical = false;
            IsRelativeFontHorizontal = IsRelativeFontVertical = false;
        }

    }

    /// <summary>
    /// An auto extent is returned when the specified value is "auto"; this needs to be converted
    /// into a real extent.
    /// </summary>
    public class AutoOrigin : Origin
    {
        public AutoOrigin()
            : base(-1, -1)
        {
        }
    }


    public class TextOutline
    {
        NumberPair p;

        public Color StrokeColor { get; set; }
        public double Width
        {
            get
            {
                return p.First;
            }
        }

        public double Blur
        {
            get
            {
                return p.Second;
            }
        }

        public Unit UnitMeasureWidth
        {
            get
            {
                return p.UnitMeasureHorizontal;
            }
        }

        public Unit UnitMeasureBlur
        {
            get
            {
                return p.UnitMeasureVertical;
            }
        }

        public bool ColorDefined { get; set; }


        /// <summary>
        /// create an absolute text outline
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="blur"></param>
        public TextOutline(Color color, double width, double blur)
        {
            StrokeColor = color;
            ColorDefined = true;
        }

        /// <summary>
        /// % sizes are not valid unless this has been set.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetContext(double width, double height)
        {
            p.SetContext(width, height);
        }

        /// <summary>
        /// em sizes are not valid unless this has been set
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetFontContext(double width, double height)
        {
            p.SetFontContext(width, height);
        }

        /// <summary>
        /// Parse a text outline expression
        /// </summary>
        /// <param name="expression"></param>
        public TextOutline(string expression)
        {
            ColorDefined = false;
            char[] sep = { ' ', ',' };

            if (expression == "none")
            {
                StrokeColor = Colors.Black;
                p = new NumberPair("0px", true);
                return;
            }

            /// the spec implies no whitespace except between components
            /// and this will fail eg if someone does rgb(1, 2, 3)
            string[] components = expression.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                // treat first component as a color
                StrokeColor = ColorExpression.Parse(components[0]);
                ColorDefined = true;
            }
            catch (TimedTextException)
            {
            }

            if (components.Length == 3)
            {
                p = new NumberPair(components[1] + " " + components[2]);
            }
            else if (components.Length == 2 && !ColorDefined)
            {
                p = new NumberPair(components[0] + " " + components[1]);
            }
            else if (components.Length == 2 && ColorDefined)
            {
                p = new NumberPair(components[1] + " 0px");
            }
            else if (components.Length == 1)
            {
                p = new NumberPair(components[0] + " 0px");
            }
            else
            {
                throw new TimedTextException("invalid outline expression");
            }
        }
    }

    /// <summary>
    /// Padding is a fourway, modeled by two pairs
    /// </summary>
    public class PaddingThickness
    {
        //Exposing these as public to gain access to the unit information
        //after the width specifications (i.e. %, px, em, etc)
        public NumberPair p1, p2;

        public double WidthBefore
        {
            get
            {
                return p1.First;
            }
        }

        public Unit WidthBeforeUnit
        {
            get { return p1.UnitMeasureHorizontal; }
        }

        public double WidthAfter
        {
            get
            {
                return p1.Second;
            }
        }

        public Unit WidthAfterUnit
        {
            get { return p1.UnitMeasureVertical; }
        }

        public double WidthStart
        {
            get
            {
                return p2.First;
            }
        }

        public Unit WidthStartUnit
        {
            get { return p2.UnitMeasureHorizontal; }
        }

        public double WidthEnd
        {
            get
            {
                return p2.Second;
            }
        }

        public Unit WidthEndUnit
        {
            get { return p2.UnitMeasureVertical; }
        }

        /// <summary>
        /// % sizes are not valid unless this has been set.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetContext(double width, double height)
        {
            p1.SetContext(width, height);
            p2.SetContext(width, height);
        }

        /// <summary>
        /// em sizes are not valid unless this has been set
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetFontContext(double width, double height)
        {
            p1.SetFontContext(width, height);
            p2.SetFontContext(width, height);
        }

        /// <summary>
        /// Parse a padding thickness expression
        /// </summary>
        /// <param name="expression"></param>
        public PaddingThickness(string expression)
        {
            char[] whitespace = { ' ' };

            /// the spec implies no whitespace except between components
            /// and this will fail eg if someone does rgb(1, 2, 3)
            string[] components = expression.Split(whitespace);
            // NumberPair p;

            // double dummy = 0;

            if (components.Length == 4)
            { // If four <length> specifications are provided, then they apply to before, end, after, and start edges, respectively.

                p1 = new NumberPair(components[0] + " " + components[2]);
                //WidthBefore = p.First;
                //WidthEnd = p.Second;
                p2 = new NumberPair(components[3] + " " + components[1]);
                //WidthAfter = p.First;
                //WidthStart = p.Second;
            }
            else if (components.Length == 3)
            { // If three <length> specifications are provided, then the first applies to the
                // before edge, the second applies to the start and end edges, and the third 
                // applies to the after edge
                p1 = new NumberPair(components[0] + " " + components[2]);
                //WidthBefore = p.First;
                //WidthStart = WidthEnd = p.Second;
                p2 = new NumberPair(components[1] + components[1]);
                //WidthAfter = p.First;
            }
            else if (components.Length == 2)
            { // If the value consists of two <length> specifications, then the first applies 
                // to the before and after edges, and the second applies to the start and end edges
                p1 = new NumberPair(components[0] + " " + components[0]);
                p2 = new NumberPair(components[1] + " " + components[1]);
                //WidthBefore = WidthAfter = p.First;
                //WidthStart = WidthEnd = p.Second;
            }
            else if (components.Length == 1)
            {
                p1 = new NumberPair(components[0] + " " + components[0]);
                p2 = new NumberPair(components[0] + " " + components[0]);
                // WidthBefore = WidthAfter = WidthStart = WidthEnd = p.First;
            }
            else
            {
                throw new TimedTextException("invalid padding expression");
            }
        }
    }
}
