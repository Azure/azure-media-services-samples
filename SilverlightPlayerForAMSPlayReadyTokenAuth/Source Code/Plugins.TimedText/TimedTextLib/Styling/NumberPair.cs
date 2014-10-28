using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace TimedText.Styling
{
    public class NumberPair
    {
        // A few TT types are pairs of lengths, this base class models the general concept.
        private const string s_pairExpression = @"^((?<v1>(\+|\-)?\d+(\.\d+)?)(?<u1>(px|em|c|\%)))( +((?<v2>(\+|\-)?\d+(\.\d+)?)(?<u2>(px|em|c|\%))))?$";
        static Regex matchPairRE = new Regex(s_pairExpression);
        static Regex matchRowColRE = new Regex(@"^((?<cols>\d+) +(?<rows>\d+))$");


        public Unit UnitMeasureHorizontal
        {
            get;
            set;
        }

        public Unit UnitMeasureVertical
        {
            get;
            set;
        }

        /// <summary>
        /// default is 
        /// </summary>
        static private int m_cellColumns = 32;

        protected static int CellColumns
        {
            get { return NumberPair.m_cellColumns; }
            set { NumberPair.m_cellColumns = value; }
        }
        static private int m_cellRows = 15;

        protected static int CellRows
        {
            get { return NumberPair.m_cellRows; }
            set { NumberPair.m_cellRows = value; }
        }

        static private int m_authoringWidth = 0;
        static private int m_authoringHeight = 0;
        protected static int AuthoringWidth
        {
            get { return NumberPair.m_authoringWidth; }
        }
        protected static int AuthoringHeight
        {
            get { return NumberPair.m_authoringHeight; }
        }

        private bool m_verticalProportions = false;


        /// <summary>
        /// Set the number of cells to divide the render canvas up into
        /// </summary>
        /// <param name="expression"></param>
        public static void SetCellSize(string expression)
        {
            if (matchRowColRE.IsMatch(expression.Trim()))
            {
                Match m = matchRowColRE.Match(expression);
                if (!string.IsNullOrEmpty(m.Groups["cols"].Value))
                {
                    m_cellRows = Int32.Parse(m.Groups["cols"].Value, CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(m.Groups["rows"].Value))
                {
                    m_cellColumns = Int32.Parse(m.Groups["rows"].Value, CultureInfo.InvariantCulture);
                }
            }
        }

        public static void SetAuthoringContextExtent(string expression)
        {
            if (matchPairRE.IsMatch(expression.Trim()))
            {
                Match m = matchPairRE.Match(expression);

                // we need absolute pixel units for this top level context
                if (m.Groups["u1"].Value != "px" || m.Groups["u2"].Value != "px")
                {
                    throw new ArgumentException("Pixel dimensions required for top level extent");
                }
                if (!string.IsNullOrEmpty(m.Groups["v1"].Value))
                {
                    m_authoringWidth = Int32.Parse(m.Groups["v1"].Value, CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(m.Groups["v2"].Value))
                {
                    m_authoringHeight = Int32.Parse(m.Groups["v2"].Value, CultureInfo.InvariantCulture);
                }
            }
        }

        protected bool IsRelativeFontHorizontal
        {
            get;
            set;
        }
        protected bool IsRelativeFontVertical
        {
            get;
            set;
        }
        protected bool IsRelativeHorizontal
        {
            get;
            set;
        }
        protected bool IsRelativeVertical
        {
            get;
            set;
        }
        private double m_horizontalContext = 1;

        protected double HorizontalContext
        {
            get { return m_horizontalContext; }
            set { m_horizontalContext = value; }
        }
        private double m_verticalFontContext = 1;

        protected double VerticalFontContext
        {
            get { return m_verticalFontContext; }
            set { m_verticalFontContext = value; }
        }
        private double m_horizontalFontContext = 1;

        protected double HorizontalFontContext
        {
            get { return m_horizontalFontContext; }
            set { m_horizontalFontContext = value; }
        }
        private double m_verticalContext = 1;

        protected double VerticalContext
        {
            get { return m_verticalContext; }
            set { m_verticalContext = value; }
        }
        private double m_horizontalValue = 1;

        protected double HorizontalValue
        {
            get { return m_horizontalValue; }
            set { m_horizontalValue = value; }
        }
        private double m_verticalValue = 1;

        protected double VerticalValue
        {
            get { return m_verticalValue; }
            set { m_verticalValue = value; }
        }

        public double First
        {
            get
            {
                if (IsRelativeFontHorizontal)
                {
                    return m_horizontalFontContext * m_horizontalValue;
                }
                else if (IsRelativeVertical)
                {
                    return m_horizontalContext * m_horizontalValue;
                }
                else
                {
                    return m_horizontalValue;
                }
            }
        }

        public double Second
        {
            get
            {
                if (IsRelativeFontVertical)
                {
                    return m_verticalFontContext * m_verticalValue;
                }
                else if (IsRelativeVertical)
                {
                    return m_verticalContext * m_verticalValue;
                }
                else
                {
                    return m_verticalValue;
                }
            }
        }


        /// <summary>
        /// Parse a string in a context
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="fVertical"></param>
        public NumberPair(string expression, bool fVertical = false)
        {
            if (expression == "0px 0px")
            {
                UnitMeasureHorizontal = Unit.Pixel;
                IsRelativeFontVertical = IsRelativeFontHorizontal = false;
                IsRelativeVertical = IsRelativeHorizontal = false;
                UnitMeasureVertical = Unit.Pixel;
                IsRelativeFontVertical = false;
                IsRelativeVertical = false;
                m_horizontalValue = m_verticalValue = 0;
                m_verticalProportions = fVertical;
                return;
            }

            if (matchPairRE.IsMatch(expression.Trim()))
            {
                double value;
                Match m = matchPairRE.Match(expression);
                if (!string.IsNullOrEmpty(m.Groups["v1"].Value))
                {
                    #region Horizontal Value
                    value = Double.Parse((m.Groups["v1"].Value), CultureInfo.InvariantCulture);
                    int scale = 1;
                    switch (m.Groups["u1"].Value)
                    {
                        case "px":
                            // we decided how proportionas will be handled based on explicit flag and whether or
                            // not a non-zero authoring content has been established
                            scale = string.IsNullOrEmpty(m.Groups["v2"].Value) ? m_authoringHeight : m_authoringWidth;
                            scale = m_verticalProportions ? m_authoringHeight : scale;
                            if (scale != 0)
                            {
                                value = value / scale;
                                UnitMeasureHorizontal = Unit.PixelProportional;
                                IsRelativeVertical = IsRelativeHorizontal = true;
                            }
                            else
                            {
                                UnitMeasureHorizontal = Unit.Pixel;
                                IsRelativeVertical = IsRelativeHorizontal = false;
                            }
                            IsRelativeFontVertical = IsRelativeFontHorizontal = false;
                            break;
                        case "em":
                            UnitMeasureHorizontal = Unit.Em;
                            IsRelativeFontVertical = IsRelativeFontHorizontal = true;
                            IsRelativeVertical = IsRelativeHorizontal = false;
                            //v = v * context.First;
                            break;
                        case "c":
                            UnitMeasureHorizontal = Unit.Cell;
                            scale = string.IsNullOrEmpty(m.Groups["v2"].Value) ? m_cellRows : m_cellColumns;
                            value = value / scale;
                            IsRelativeFontVertical = IsRelativeFontHorizontal = false;
                            IsRelativeVertical = IsRelativeHorizontal = true;
                            break;
                        case "%":
                            UnitMeasureHorizontal = Unit.Percent;
                            IsRelativeFontVertical = IsRelativeFontHorizontal = true;
                            IsRelativeVertical = IsRelativeHorizontal = false;
                            value = (value / 100);
                            break;
                        default:
                            break;
                    }
                    UnitMeasureVertical = UnitMeasureHorizontal;
                    #endregion
                    m_horizontalValue = m_verticalValue = value;
                }
                if (!string.IsNullOrEmpty(m.Groups["v2"].Value))
                {
                    value = Double.Parse((m.Groups["v2"].Value), CultureInfo.InvariantCulture);
                    switch (m.Groups["u2"].Value)
                    {
                        case "px":
                            if (m_authoringHeight != 0)
                            {
                                value = value / m_authoringHeight;
                                UnitMeasureVertical = Unit.PixelProportional;
                                IsRelativeVertical = true;
                            }
                            else
                            {
                                UnitMeasureVertical = Unit.Pixel;
                                IsRelativeVertical = false;
                            }
                            IsRelativeFontVertical = false;
                            break;
                        case "em":
                            UnitMeasureVertical = Unit.Em;
                            IsRelativeFontVertical = true;
                            IsRelativeVertical = false;
                            //v = v * context.Second;
                            break;
                        case "c":
                            UnitMeasureVertical = Unit.Cell;
                            IsRelativeFontVertical = false;
                            IsRelativeVertical = true;
                            value = value / m_cellRows;
                            break;
                        case "%":
                            UnitMeasureVertical = Unit.Percent;
                            IsRelativeFontVertical = true;
                            IsRelativeVertical = false;
                            value = (value / 100);  //*context.Second;
                            break;
                        default:
                            break;
                    }
                    m_verticalValue = value;
                }
            }
        }

        /// <summary>
        /// % sizes are not valid unless this has been set.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetContext(double width, double height)
        {
            m_horizontalContext = width;
            m_verticalContext = height;
        }

        /// <summary>
        /// em sizes are not valid unless this has been set
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetFontContext(double width, double height)
        {

            m_horizontalFontContext = width;
            m_verticalFontContext = height;
        }

        protected NumberPair()
        {
        }

        public NumberPair(NumberPair src)
        {
            UnitMeasureHorizontal = src.UnitMeasureHorizontal;
            IsRelativeFontVertical = src.IsRelativeFontVertical;
            IsRelativeFontHorizontal = src.IsRelativeFontHorizontal;
            IsRelativeVertical = src.IsRelativeVertical;
            IsRelativeHorizontal = src.IsRelativeHorizontal;
            UnitMeasureVertical = src.UnitMeasureVertical;
            IsRelativeFontVertical = src.IsRelativeFontVertical;
            IsRelativeVertical = src.IsRelativeVertical;
            m_horizontalValue = src.m_horizontalValue;
            m_verticalValue = src.m_verticalValue;
        }
    }

}
