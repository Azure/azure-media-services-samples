using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedText.Styling
{
    /// <summary>
    /// For attribute values that are explicit inherit, insert this value. 
    /// It contains an object which can be used to cache the inherited value.
    /// </summary>
    public class Inherit
    {
        public object Cached
        {
            get;
            set;
        }
    }

    public enum TextDecorationAttributeValue
    {
        None,
        Underline,
        Overline,
        Throughline,
    };

    public enum FontStyleAttributeValue
    {
        Regular,
        Oblique,
        ReverseOblique,
        Italic,
    };

    public enum FontWeightAttributeValue
    {
        Regular,
        Bold,
    };


    public class Font
    {

        #region private variables
        string  m_familyName = "Arial";
        //Style m_style = Style.Regular;
        double m_size = 14.0;
        double m_stretch = 1.0;
        System.Windows.FontStyle m_style;
        System.Windows.FontWeight m_weight;
        //StyleAttribute m_styleAttribute;

        #region font metrics
        //int m_unitsPerEm;
        //int m_ascent;
        //int m_descent;
        //int m_averageWidth;
        //int m_maxWidth;
        //int m_capHeight;
        //int m_stemHeight;
        //int m_charHeight;
        //int m_stemV;
        //int m_leading;
        #endregion
  
        #endregion

        /// <summary>
        /// Is the text rendered left to right?
        /// </summary>
        public bool LeftToRight
        {
            get;
            set;
        }

        public Font(string familyName, double emHeight, FontWeightAttributeValue weight, FontStyleAttributeValue style)
        {
            LeftToRight = true;
            m_familyName = familyName;
            m_size = emHeight;
            switch (style)
            {
                case FontStyleAttributeValue.Italic:
                    m_style = System.Windows.FontStyles.Italic;
                    break;
                case FontStyleAttributeValue.Oblique:
                    m_style = System.Windows.FontStyles.Italic;
                    break;
                case FontStyleAttributeValue.ReverseOblique:
                    m_style = System.Windows.FontStyles.Italic;
                    break;
                default:
                    m_style = System.Windows.FontStyles.Normal;
                    break;
            }
            switch (weight)
            {
                case FontWeightAttributeValue.Bold: m_weight = System.Windows.FontWeights.Bold;
                    break;
                default:
                    m_weight = System.Windows.FontWeights.Normal;
                    break;
            }
            m_stretch = 1.0;
         }

        public string Family
        {
            get
            {
                return m_familyName;
            }
        }

        public double EmHeight
        {
            get
            {
                return m_size;
            }
        }

        public System.Windows.FontStyle Style
        {
            get
            {
                return m_style;
            }
        }

        public System.Windows.FontWeight Weight
        {
            get
            {
                return m_weight;
            }
        }

        
        #region WPF font handling
        //private void Initialize()
        //{
        //}

        public System.Windows.FontStretch Stretch()
        {
            if (m_stretch > 1.0)
            {
                return System.Windows.FontStretches.ExtraCondensed;
            } else if (m_stretch < 1.0)
            {
                return System.Windows.FontStretches.ExtraExpanded;
            }
            else 
            {
                return System.Windows.FontStretches.Normal;
            }
        }
        
        //System.Windows.Media.FontFamily m_family;
        ////System.Windows.FontStretch m_stretch;
        //System.Windows.Media.Typeface m_typeface;
        #endregion
    }
}
