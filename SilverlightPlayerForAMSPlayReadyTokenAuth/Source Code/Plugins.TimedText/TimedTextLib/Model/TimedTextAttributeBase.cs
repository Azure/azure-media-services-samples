// <copyright file="TimedText.cs" company="Microsoft Corporation">
// ===============================================================================
// MICROSOFT CONFIDENTIAL
// Microsoft Accessibility Business Unit
// Incubation Lab
// Project: Timed Text Library
// ===============================================================================
// Copyright 2009  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
using System;
using TimedText.Styling;

namespace TimedText
{
    /// <summary>
    /// Representation of Timed Text attribute data.
    /// </summary>
    public class TimedTextAttributeBase
    {
        #region Private Variables
        private TimedTextElementBase m_parent;
        private string m_localName;
        private string m_namespaceName;
        private string m_value;
        #endregion

        #region Properties
        /// <summary>
        /// Element whic owns this attribute
        /// </summary>
        public TimedTextElementBase Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }
        /// <summary>
        /// Name of the attribute without namespace
        /// </summary>
        public string LocalName
        {
            get { return m_localName; }
            set { m_localName = value; }
        }
        /// <summary>
        /// Namespace of the attribute
        /// </summary>
        public string NamespaceName
        {
            get { return m_namespaceName; }
            set { m_namespaceName = value; }
        }
        /// <summary>
        /// Value of the attribute
        /// </summary>
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        #endregion

        #region Default values
        /// <summary>
        /// Return the Initial value for attributes as defined in the 
        /// timed text specification.
        /// </summary>
        /// <param name="property">Name of the property to look up</param>
        /// <returns>property value</returns>        
        public static object GetInitialStyle(string property)
        {
            switch (property)
            {
                case "backgroundColor": return ColorExpression.Parse("transparent");
                case "color": return ColorExpression.Parse("black");  // spec says transparent
                case "direction": return "auto";  // this is not what the spec says, but we need it to respect writingMode.
                case "display": return "auto";
                case "displayAlign": return "before";
                case "dynamicFlow": return "none";
                case "extent": return new TimedText.Styling.AutoExtent();
                case "fontFamily": return "default";
                case "fontSize": return "1c";
                case "fontStyle": return FontStyleAttributeValue.Regular; ;
                case "fontWeight": return FontWeightAttributeValue.Regular;
                case "lineHeight": return new NormalHeight();  // stand in for normal.
                case "opacity": return 1.0;
                case "origin": return new TimedText.Styling.Origin(0, 0);
                case "overflow": return "hidden";
                case "padding": return new Styling.PaddingThickness("0px");
                case "showBackground": return "always";
                case "textAlign": return "start";
                case "textDecoration": return TextDecorationAttributeValue.None;
                case "textOutline": return new Styling.TextOutline("none");
                case "unicodeBidi": return "undefined";
                case "visibility": return "visible";
                case "wrapOption": return "wrap";
                case "writingMode": return "lrtb";
                case "zIndex": return "auto";

                // these are defaults for the xml attributes
                case "space": return "default";
                case "lang": return "en-us";

                case "region": return "";  // this is not a style as such, but we use the same mechanics.

                // the following cases are for internal styles
                case "#preserve": return false;
                default: return "";
            }
        }
        #endregion

        #region Test Namespaces
        /// <summary>
        /// Test whether this attribute is in the Timed Text Parameter 
        /// namespace
        /// </summary>
        /// <returns></returns>
        public bool IsParameterAttribute()
        {
            bool result = false;
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#parameter");
            result |= (m_namespaceName == "http://www.w3.org/ns/ttml#parameter");
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in the Timed Text Parameter namespace
        /// </summary>
        /// <returns></returns>
        public bool IsProfileAttribute()
        {
            bool result = false;
            result |= (m_namespaceName.StartsWith("http://www.w3.org/2006/10/ttaf1/profile", StringComparison.Ordinal));
            result |= (m_namespaceName.StartsWith("http://www.w3.org/ns/ttml/profile", StringComparison.Ordinal));
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in a Timed Text Style namespace
        /// </summary>
        /// <returns></returns>
        public bool IsStyleAttribute()
        {
            bool result = false;
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#style");
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#styling");
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#style-extensions");
            result |= (m_namespaceName == "http://www.w3.org/ns/ttml#styling");
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in a Timed Text Metadata namespace
        /// </summary>
        /// <returns></returns>
        public bool IsMetadataAttribute()
        {
            bool result = false;
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#metadata");
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1#metadata-extensions");
            result |= (m_namespaceName == "http://www.w3.org/ns/ttml#metadata");
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in a Timed Text Featture namespace
        /// </summary>
        /// <returns></returns>
        public bool IsFeatureAttribute()
        {
            bool result = false;
            result |= (m_namespaceName.StartsWith("http://www.w3.org/2006/10/ttaf1/feature", StringComparison.Ordinal));
            result |= (m_namespaceName.StartsWith("http://www.w3.org/ns/ttml/feature/", StringComparison.Ordinal));
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in a Timed Text Extension 
        /// namespace
        /// [deprecated] this is expected to be removed from timed text
        /// </summary>
        /// <returns></returns>
        public bool IsExtensionAttribute()
        {
            bool result = false;
            result |= (m_namespaceName.StartsWith("http://www.w3.org/2006/10/ttaf1/extension", StringComparison.Ordinal));
            result |= (m_namespaceName.StartsWith("http://www.w3.org/ns/ttml/extension/", StringComparison.Ordinal));
            return result;
        }

        /// <summary>
        /// Test whether this attribute is in an intrinsic XML attribute
        /// </summary>
        /// <returns></returns>
        public bool IsXmlAttribute()
        {
            return m_namespaceName == "http://www.w3.org/XML/1998/namespace";
        }

        /// <summary>
        /// Test whether this attribute is in any Timed Text namespace
        /// </summary>
        /// <returns></returns>
        public bool IsTimedTextAttribute()
        {
            bool result = false;
            result |= IsMetadataAttribute() || IsStyleAttribute() || IsParameterAttribute();
            result |= (m_namespaceName == "http://www.w3.org/2006/10/ttaf1");
            result |= (m_namespaceName == "http://www.w3.org/ns/ttml");
            return result;
        }
        #endregion
    }
}
