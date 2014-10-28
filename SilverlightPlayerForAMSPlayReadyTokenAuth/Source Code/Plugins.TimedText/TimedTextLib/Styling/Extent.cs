
namespace TimedText.Styling
{
    /// <summary>
    /// Class to record a two dimensional size
    /// </summary>
    public class Extent : NumberPair
    {
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
        /// Parse a string in a context
        /// </summary>
        /// <param name="expression"></param>
        public Extent(string expression)
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
        /// Create an absolute extent.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Extent(double width, double height)
        {
            HorizontalValue = width;
            VerticalValue = height;
            IsRelativeHorizontal = IsRelativeVertical = false;
            IsRelativeFontHorizontal = IsRelativeFontVertical = false;
        }
    }

    /// <summary>
    /// An auto extent is returned when the specified value is "auto"; this needs to be converted
    /// into a real extent.
    /// </summary>
    public class AutoExtent : Extent
    {
        public AutoExtent()
            : base(1, 1)
        {
            base.UnitMeasureHorizontal = Unit.Percent;
            base.UnitMeasureVertical = Unit.Percent;
        }
    }
}
