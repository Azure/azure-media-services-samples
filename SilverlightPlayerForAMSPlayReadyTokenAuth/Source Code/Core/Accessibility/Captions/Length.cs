namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents a length to be applied to a caption property..
    /// </summary>
    public class Length
    {
        /// <summary>
        /// The numeric value of this length.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The unit of this length.
        /// </summary>
        public LengthUnit Unit { get; set; }

        public double ToPixelLength(double containerLength = 0)
        {
            switch (Unit)
            {
                case LengthUnit.Cell:
                case LengthUnit.Percent:
                case LengthUnit.PixelProportional:
                    return Value * containerLength;
                case LengthUnit.Pixel:
                default:
                    return Value;
            }
        }
    }
}