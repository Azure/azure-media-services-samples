using System;
using System.Globalization;
using System.Windows.Media;
//using System.Windows.Media;   // to do abstract away from Windows dependancies.?

namespace TimedText.Styling
{
    public sealed class ColorExpression
    {
        private ColorExpression() { }

        /*
        <color>
          : "#" rrggbb
          | "#" rrggbbaa
          | "rgb" "(" r-value "," g-value "," b-value ")"
          | "rgba" "(" r-value "," g-value "," b-value "," a-value ")"
          | <namedColor>
        rrggbb
          :  <hexDigit>{6}
        rrggbbaa
          :  <hexDigit>{8}
        r-value | g-value | b-value | a-value
          : component-value
        component-value
          : non-negative-integer                    // valid range: [0,255]
        non-negative-integer
          : <digit>+
        */
        
        /// <summary>
        /// Create a Color object from a timed text colour expression
        /// </summary>
        /// <param name="colorExpression">colour expression</param>
        /// <returns>color</returns>
        public static System.Windows.Media.Color Parse(string colorExpression)
        {
            string input = colorExpression.Trim();
            System.Windows.Media.Color rgb = System.Windows.Media.Color.FromArgb(0xff, 0, 0, 0);

            char[] separators = { '(', ',', ')' };
            try
            {
                if (input.Contains("#"))
                {
                    rgb.R = Byte.Parse(input.Substring(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    rgb.G = Byte.Parse(input.Substring(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    rgb.B = Byte.Parse(input.Substring(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    if (input.Length > 7)
                    {
                        rgb.A = Byte.Parse(input.Substring(7, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    }
                }
                else if (input.Contains("rgb("))
                {
                    string[] parts = input.Split(separators);
                    // should be 5 parts, the first part is prefix, last is null.
                    NumberStyles digitOnly = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;
                    rgb.R = Byte.Parse(parts[1], digitOnly, CultureInfo.InvariantCulture);
                    rgb.G = Byte.Parse(parts[2], digitOnly, CultureInfo.InvariantCulture);
                    rgb.B = Byte.Parse(parts[3], digitOnly, CultureInfo.InvariantCulture);
                }
                else if (input.Contains("rgba("))
                {
                    string[] parts = input.Split(separators);
                    // should be 5 parts, the first part is prefix, last is null..
                    NumberStyles digitOnly = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;
                    rgb.R = Byte.Parse(parts[1], digitOnly, CultureInfo.InvariantCulture);
                    rgb.G = Byte.Parse(parts[2], digitOnly, CultureInfo.InvariantCulture);
                    rgb.B = Byte.Parse(parts[3], digitOnly, CultureInfo.InvariantCulture);
                    rgb.A = Byte.Parse(parts[4], digitOnly, CultureInfo.InvariantCulture);
                }
                else
                {
                    rgb = NamedColor(input.ToLower(CultureInfo.CurrentCulture));
                }
            }
            catch (Exception)
            {
                throw new TimedTextException("Invalid colour format string");
            }
            return rgb;
        }

        /// <summary>
        /// return a colour from one of the allowed timed text names.
        /// </summary>
        /// <param name="input">name of color</param>
        /// <returns>color</returns>
        private static Color NamedColor(string input)
        {
            switch (input)
            {
                case "transparent": return Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                case "black": return Color.FromArgb(0xff, 0x00, 0x00, 0x00);
                case "silver": return Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0);
                case "gray": return Color.FromArgb(0xff, 0x80, 0x80, 0x80);
                case "white": return Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                case "maroon": return Color.FromArgb(0xff, 0x80, 0x00, 0x00);
                case "red": return Color.FromArgb(0xff, 0xff, 0x00, 0x00);
                case "purple": return Color.FromArgb(0xff, 0x80, 0x00, 0x80);
                case "fuchsia": return Color.FromArgb(0xff, 0xff, 0x00, 0xff);
                case "magenta": return Color.FromArgb(0xff, 0xff, 0x00, 0xff);
                case "green": return Color.FromArgb(0xff, 0x00, 0x80, 0x00);
                case "lime": return Color.FromArgb(0xff, 0x00, 0xff, 0x00);
                case "olive": return Color.FromArgb(0xff, 0x80, 0x80, 0x00);
                case "yellow": return Color.FromArgb(0xff, 0xff, 0xff, 0x00);
                case "navy": return Color.FromArgb(0xff, 0x00, 0x00, 0x80);
                case "blue": return Color.FromArgb(0xff, 0x00, 0x00, 0xff);
                case "teal": return Color.FromArgb(0xff, 0x00, 0x80, 0x80);
                case "aqua": return Color.FromArgb(0xff, 0xff, 0x00, 0xff);
                case "cyan": return Color.FromArgb(0xff, 0x00, 0xff, 0xff);
            }
            throw new TimedTextException("named colour " + input + " not allowed");
        }

        /// <summary>
        /// Test the colour parser. Not comprehensive at this point
        /// </summary>
        /// <returns></returns>
        public static bool UnitTests()
        {
            Color reference = Color.FromArgb(0xff, 0xff, 0, 0);
            bool pass = true;

            // some basic tests, try to come up with some more devilish ones.
            pass &= Parse("red") == reference;
            pass &= Parse("rgb(255,00,00)") == reference;
            pass &= Parse("rgb(255,00,00,255)") == reference;
            pass &= Parse("#ff0000") == reference;
            pass &= Parse("#FF0000") == reference;
            pass &= Parse("#ff0000ff") == reference;
            pass &= Parse("#fF0000fF") == reference;

            return pass;
        }

    }
}
