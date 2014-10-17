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
using System.Collections.Generic;
using System.Reflection;
using TimedText.Timing;
using System.Xml;
using System.Xml.Linq;
using TimedText.Styling;
using System.Globalization;
using System.Text.RegularExpressions;
using TimedText.Parameter;
using System.Linq;
using System.IO;

namespace TimedText
{
    /// <summary>
    /// Base class for all timed text elements. 
    /// Handles parsing and validity
    /// </summary>
    public abstract class TimedTextElementBase
        : Timing.TimeTree<TimedTextElementBase, TimedTextAttributeBase>.TreeType
    {
        #region Properties

        public string Language
        {
            get;
            set;
        }

        public string LocalName
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public virtual BodyElement Body
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public TtElement Root
        {
            get;
            set;
        }

        #endregion

        #region Local members
        // local inline styles.
        private Dictionary<string, object> m_styling = new Dictionary<string, object>();

        static double defaultZ = 0.001;  // make up our own z ordering.
        #endregion

        #region Error handler
        protected static void Error(string message)
        {
            throw new TimedTextException(message);
            //Console.WriteLine(message);
        }

        #endregion

        #region static tt_element GetElementFromName(string elem)
        /// <summary>
        /// Instantiate one of the TimedText.* classes from a string representing
        /// its name. 
        /// </summary>
        /// <param name="elem">name of the class to instantiate</param>
        /// <returns>descendent of tt_element</returns>
        static TimedTextElementBase GetElementFromName(string elem)
        {
            // Don't know why this isn't a core .NET function, but its not too
            // hard to drill through the classes.
            Assembly a = Assembly.GetExecutingAssembly();
            Type t = a.GetType(elem, false);
            if (t != null)
            {
                ConstructorInfo c = t.GetConstructor(System.Type.EmptyTypes);
                return c.Invoke(null) as TimedTextElementBase;
            }
            else
            {
                Error("No such element (" + elem + ") in namespace");
            }
            return null;
        }
        #endregion

        #region public virtual bool Valid()
        /// <summary>
        /// Test validity of this subtree
        /// </summary>
        /// <returns>true if valid</returns>
        public virtual bool Valid()
        {
            try
            {
                ValidElements();
                ValidAttributes();  // this has to happen 2nd as we move attributes in ValidElements
            }
            catch (TimedTextException ex)
            {
                Error(ex.Message);
                return false;
            }
            return true;
        }
        #endregion
        
        #region LocalStyles
        /// <summary>
        /// Set a local style on this element
        /// allows modification at runtime.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void SetLocalStyle(string property, object value)
        {
            m_styling[property] = value;
        }
        /// <summary>
        /// Clear local style on this element
        /// </summary>
        /// <param name="property"></param>
        public void ClearLocalStyle(string property)
        {
            m_styling.Remove(property);
        }
        #endregion

        #region public object GetReferentStyle(string property)
        /// <summary>
        /// Look up a style property in any referent style elements.
        /// </summary>
        /// <param name="property"></param>
        public object GetReferentStyle(string property)
        {
            // check for local override, this will always win
            int styleCount = m_styling.Count;
            if (styleCount > 0 && m_styling.ContainsKey(property))
            {
                return m_styling[property];
            }

            // find out if we refer to any other styles.
            if (styleCount > 0 && m_styling.ContainsKey("style"))
            {
                List<string> referentStyles = m_styling["style"] as List<string>;

                if (referentStyles == null || referentStyles.Count == 0)
                {
                    m_styling[property] = null;
                    return null;
                }
                // recursively check them in reverse order.

                for (int i = referentStyles.Count - 1; i >= 0; i--)
                {
                    string s = referentStyles[i];
                    if (Root.Styles.ContainsKey(s))
                    {
                        object result = Root.Styles[s].GetReferentStyle(property);
                        if (result != null)
                        {
                            m_styling[property] = result;
                            return result;
                        }
                    }
                }
            }

            return null;
        }
        #endregion

        #region public virtual object GetComputedStyle(string property)
        /// <summary>
        /// Get the final computed value for a style property from this element
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual object GetComputedStyle(string propertyName, RegionElement currentRegion)
        {
            object value;

            value = GetReferentStyle(propertyName);

            // to do: special case visibility, as the parent value=hidden trumps the child value.
            if (value == null || (value is Inherit))
            {
                value = GetInheritedStyle(propertyName, currentRegion);
            }
            // commenting out the following line enabled proper passing of color values, it avoids caching a computed property apparently
            //m_styling[propertyName] = value;
            return value;
        }

        public object GetInheritedStyle(string property, RegionElement currentRegion)
        {
            bool isBodyElement = this is BodyElement;
            bool canInherit = !(isBodyElement) && !(this is RegionElement);
            if (Parent != null && canInherit)
            {
                return ((TimedTextElementBase)Parent).GetComputedStyle(property, currentRegion);
            } if (isBodyElement && currentRegion != null)
            {
                // body needs to inherit from the region it has been parented to
                return currentRegion.GetComputedStyle(property, null);
            }
            else
            {
                /// return initial value.
                return TimedTextAttributeBase.GetInitialStyle(property);

            }
        }
        #endregion

        #region Layout
        /// <summary>
        /// sub classes need to override this.
        /// </summary>
        /// <returns></returns>
        public virtual Formatting.FormattingObject GetFormattingObject(TimeCode tick)
        {
            return null;
        }
        #endregion

        public virtual void WriteElement(XmlWriter writer)
        {
            if (LocalName == null) return;

            writer.WriteStartElement(this.LocalName, this.Namespace);
            WriteAttributes(writer);
            foreach (TimedTextElementBase element in Children)
            {
                element.WriteElement(writer);
            }
            writer.WriteEndElement();
        }

        public void WriteAttributes(XmlWriter writer)
        {
            foreach (var a in Attributes)
            {
                writer.WriteAttributeString(a.LocalName, a.NamespaceName, a.Value);
            }
        }

        public string Serialize()
        {
            MemoryStream ms = new MemoryStream();
            using (XmlWriter w = XmlWriter.Create(ms))
            {
                this.WriteElement(w);
            }
            ms.Seek(0, SeekOrigin.Begin);
            string result = "";
            using (StreamReader r = new StreamReader(ms))
            {
                result = r.ReadToEnd();
            }
            return result;
        }

        #region Attribute Validation

        /// <summary>
        /// Validate attribute classes
        /// </summary>
        /// <param name="parameterSet">true if required to validate parameter attributes</param>
        /// <param name="metadataSet">true if required to validate metadata attributes</param>
        /// <param name="styleSet">true if required to validate style attributes</param>
        /// <param name="timingSet">true if required to validate timing attributes</param>
        /// <param name="regionSet">true if required to validate region attributes</param>
        /// <param name="timeContainerSet">true if required to time container parameter attributes</param>
        protected void ValidateAttributes(bool parameterSet, bool metadataSet, bool styleSet, bool timingSet, bool regionSet, bool timeContainerSet)
        {
            bool seenStyleAttributeInSet = false;
            // now check each of the attributes is individually valid
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    ValidXmlAttributeValue(attribute);
                }
                else if (attribute.IsMetadataAttribute())
                {
                    if (metadataSet)
                        ValidMetadataAttributeValue(attribute);
                    else
                        Error("invalid style attribute");
                }
                else if (attribute.IsParameterAttribute())
                {
                    if (parameterSet)
                        ValidParameterAttribute(attribute);
                    else
                        Error("invalid style attribute");
                }
                else if (attribute.IsStyleAttribute())
                {
                    bool isSetElement = this is SetElement;
                    if (styleSet)
                    {
                        if (seenStyleAttributeInSet && isSetElement)
                        {
                            Error("set only allows a single style attribute");
                        }
                        if (isSetElement) seenStyleAttributeInSet = true;
                        ValidStyleAttributeValue(attribute);
                    }
                    else if (this is TtElement && attribute.LocalName == "extent")
                        ValidStyleAttributeValue(attribute);
                    else
                        Error("invalid style attribute");
                }
                else if (attribute.IsTimedTextAttribute())
                {
                    switch (attribute.LocalName)
                    {
                        case "id":
                            // this is actually invalid, but we ignore for the time being.
                            break;
                        case "style":
                            ValidStyleReference(attribute);
                            break;
                        #region timingSet
                        case "begin":
                            if (timingSet)
                                Timing["begin"] = TimeExpression.Parse(attribute.Value);
                            else
                                Error("invalid begin attribute");
                            break;
                        case "end":
                            if (timingSet)
                                Timing["end"] = TimeExpression.Parse(attribute.Value);
                            else
                                Error("invalid begin attribute");
                            break;
                        case "dur":
                            if (timingSet)
                                Timing["dur"] = TimeExpression.Parse(attribute.Value);
                            else
                                Error("invalid begin attribute");
                            break;
                        #endregion

                        case "region":
                            if (regionSet)
                            {
                                ValidRegionAttribute(attribute);
                                break;
                            }
                            else
                            {
                                Error("Erroneous region attribute " + attribute.LocalName + " on " + this.ToString());
                            }
                            break;
                        case "timeContainer":
                            if (timeContainerSet)
                            {
                                ValidTimeContainerAttribute(attribute);
                            }
                            else
                            {
                                Error("Erroneous time container attribute " + attribute.LocalName + " on " + this.ToString());
                                break;
                            }
                            break;
                        default:
                            Error("Erroneous tt: namespace attribute " + attribute.LocalName + " on " + this.ToString());
                            break;
                    }
                }
                else
                {
                    //Error("Erroneous attribute " + attribute.localName + " on " + this.ToString());
                }
            }
        }

        /// <summary>
        /// Validate a parameter attribute
        /// </summary>
        /// <param name="attribute"></param>
        protected void ValidParameterAttribute(TimedTextAttributeBase attribute)
        {
            if (!(this is TtElement)) return;

            switch (attribute.LocalName)
            {
                case "cellResolution":
                    Styling.NumberPair.SetCellSize(attribute.Value);
                    break;
                case "clockMode":
                    switch (attribute.Value)
                    {
                        case "local":
                            TimeExpression.CurrentClockMode = ClockMode.Local;
                            break;
                        case "gps":
                            TimeExpression.CurrentClockMode = ClockMode.Gps;
                            break;
                        case "utc":
                            TimeExpression.CurrentClockMode = ClockMode.Utc;
                            break;
                    }
                    break;
                case "frameRate":
                    int CurrentFrameRate;
                    if (Int32.TryParse(attribute.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out CurrentFrameRate))
                    {
                        TimeExpression.CurrentFrameRate = CurrentFrameRate;
                    }
                    else 
                    {
                        throw new TimedTextException("invalid frameRate: " + attribute.Value);
                    }
                    break;
                case "frameRateMultiplier":
                    string[] parts = attribute.Value.Split(new char[] { ':', ' ' });
                    if (parts.Length >= 2)
                    {
                        TimeExpression.CurrentFrameRateNominator = Int32.Parse(parts[0], CultureInfo.InvariantCulture);
                        TimeExpression.CurrentFrameRateDenominator = Math.Max(1, Int32.Parse(parts[1], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        throw new TimedTextException("invalid frameRateMultiplier: " + attribute.Value);
                    }
                    break;
                case "markerMode":
                    Root.Parameters[attribute.LocalName] = attribute.Value;
                    break;
                case "pixelAspectRatio":
                    Root.Parameters[attribute.LocalName] = attribute.Value;
                    break;
                case "profile":
                    if (Parameter.ParameterElement.Features.Count == 0)
                    { // we have not seen a profile element, OK to proceed
                        ValidProfileAttribute(attribute);
                    }
                    break;
                case "smpteMode":
                    switch (attribute.Value)
                    {
                        case "dropNTSC":
                            TimeExpression.CurrentSmpteMode = SmpteMode.DropNtsc;
                            break;
                        case "dropPAL":
                            TimeExpression.CurrentSmpteMode = SmpteMode.DropPal;
                            break;
                        case "nonDrop":
                            TimeExpression.CurrentSmpteMode = SmpteMode.NonDrop;
                            break;
                    }
                    break;
                case "subFrameRate":
                    int CurrentSubFrameRate;
                    if (Int32.TryParse(attribute.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out CurrentSubFrameRate))
                    {
                        TimeExpression.CurrentSubFrameRate = CurrentSubFrameRate;
                    }
                    else 
                    {
                        throw new TimedTextException("invalid subFrameRate: " + attribute.Value);
                    }
                    break;
                case "tickRate":
                    int CurrentTickRate;
                    if (Int32.TryParse(attribute.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out CurrentTickRate))
                    {
                        TimeExpression.CurrentTickRate = CurrentTickRate;
                    }
                    else 
                    {
                        throw new TimedTextException("invalid tickRate: " + attribute.Value);
                    }
                    break;
                case "timeBase":
                    switch (attribute.Value)
                    {
                        case "media":
                            TimeExpression.CurrentTimeBase = TimeBase.Media;
                            break;
                        case "smpte":
                            TimeExpression.CurrentTimeBase = TimeBase.Smpte;
                            break;
                        case "clock":
                            TimeExpression.CurrentTimeBase = TimeBase.Clock;
                            break;
                    }
                    break;
                default:
                    Error("Erroneous parameter attribute on  " + this.ToString());
                    break;
            }
        }

        /// <summary>
        /// Validate a profile attribute
        /// </summary>
        /// <param name="attribute"></param>
        protected static void ValidProfileAttribute(TimedTextAttributeBase attribute)
        {
            Uri designator;
            Uri profile = new Uri("http://www.w3.org/2006/10/ttaf1/profile/");
            try
            {
                designator = new Uri(profile, attribute.Value);
                switch (designator.AbsoluteUri)
                {
                    case "http://www.w3.org/2006/10/ttaf1/profile/dfxp-transformation":
                        foreach (var f in Parameter.ParameterElement.TransformProfile)
                        {
                            if (!Parameter.ParameterElement.Features.ContainsKey(f.Key))
                            {   // if local profile has added this, then dont over-ride it
                                Parameter.ParameterElement.Features[f.Key] = f.Value;
                            }
                        }
                        break;
                    case "http://www.w3.org/2006/10/ttaf1/profile/dfxp-presentation":
                        foreach (var f in Parameter.ParameterElement.PresentationProfile)
                        {
                            if (!Parameter.ParameterElement.Features.ContainsKey(f.Key))
                            {   // if local profile has added this, then dont over-ride it
                                Parameter.ParameterElement.Features[f.Key] = f.Value;
                            }
                        }
                        break;
                    default:
                        throw new TimedTextException("unrecognized profile " + designator.AbsoluteUri);
                }
            }
            catch (TimedTextException)
            {
                throw new TimedTextException("unrecognized profile " + attribute.Value);
            }
        }

        /// <summary>
        /// Validate a time container attribute
        /// </summary>
        /// <param name="attribute"></param>
        protected void ValidTimeContainerAttribute(TimedTextAttributeBase attribute)
        {
            switch (attribute.Value)
            {
                case "par":
                    Timing["timeContainer"] = TimeContainer.Par;
                    TimeSemantics = TimeContainer.Par;
                    break;
                case "seq":
                    Timing["timeContainer"] = TimeContainer.Seq;
                    TimeSemantics = TimeContainer.Seq;
                    break;
                default:
                    Error("Erroneous value for timeContainer on " + this.ToString());
                    break;
            }
        }

        /// <summary>
        /// Validate a region attribute
        /// </summary>
        /// <param name="attribute"></param>
        protected void ValidRegionAttribute(TimedTextAttributeBase attribute)
        {
            //// we should do a pattern match to ensure its legal.
            //List<string> idrefs = new List<string>();
            //char[] whitespace = { (char)0x20 };
            //foreach (string s in attribute.value.Split(whitespace))
            //{
            //    // to do - what we want to do here is check its in m_styles; however that wont work for 
            //    // forward references in styling; can we get a spec restriction here?.
            //    idrefs.Add(s.Trim());
            //}

            // strictly speaking region is not a style attribute, but this is convenient.
            m_styling["region"] = attribute.Value;
        }

        /// <summary>
        /// Validate a style reference attribute
        /// </summary>
        /// <param name="attribute"></param>
        protected void ValidStyleReference(TimedTextAttributeBase attribute)
        {
            //  IDREFS allow only a single space between names. Not sure what happens here if there are multiple.
            // we should do a pattern match to ensure its legal.
            List<string> idrefs = new List<string>();
            char[] whitespace = { (char)0x20 };
            foreach (string s in attribute.Value.Split(whitespace))
            {
                // to do - what we want to do here is check its in m_styles; however that wont work for 
                // forward references in styling; can we get a spec restriction here?.
                idrefs.Add(s.Trim());
            }
            // strictly speaking style is not a style attribute, but this is convenient.
            m_styling["style"] = idrefs;
        }

        #endregion

        #region regular expressions for attribute values
        private const string s_lengthExpression = @"((\+|\-)?\d+(\.\d+)?(px|em|c|\%))";
        private const string s_fontSizeExpression = @"^" + s_lengthExpression + "( +" + s_lengthExpression + ")?$";
        private const string s_paddingExpression = @"^" + s_lengthExpression + "( +" + s_lengthExpression + "( +" + s_lengthExpression + "( +" + s_lengthExpression + ")?)?)?$";
        private const string s_fontStyleExpression = @"^normal|italic|oblique|reverseOblique|inherit$";
        private const string s_fontWeightExpression = @"^normal|bold|inherit$";
        private const string s_directionExpression = "^ltr|rtl|inherit$";
        private const string s_displayExpression = "^auto|none|inherit$";
        private const string s_displayAlignExpression = "^before|center|after|inherit$";
        private const string s_lineHeightExpression = @"^normal|" + s_lengthExpression + "|inherit$";
        private const string s_opacityExpression = @"^(\d+)(\.\d+)?|inherit$";    /// not to spec --- but float is ridiculous.
        private const string s_overflowExpression = @"^visible|hidden|scroll|inherit$";
        private const string s_showBackgroundExpression = @"^always|whenActive|inherit$";
        private const string s_textDecorationExpression = @"^none|underline|noUnderline|lineThrough|noLineThrough|overline|noOverline|inherit$";
        private const string s_writingModeExpression = @"^lrtb|rltb|tbrl|tblr|lr|rl|tb|inherit$";
        private const string s_wrapOptionExpression = @"^wrap|noWrap|inherit$";
        private const string s_visibilityExpression = @"^visible|hidden|inherit$";
        private const string s_unicodeBidiExpression = @"^normal|embed|bidiOverride|inherit$";
        private const string s_zIndexExpression = @"^auto|((\+|\-)?\d+)|inherit$";
        private const string s_textAlignExpression = @"^left|center|right|start|end|inherit$";

        #endregion

        #region Attribute Value Validation

        static Dictionary<string, Regex> cachedRegex = new Dictionary<string, Regex>();
        /// <summary>
        /// Validate an attribute by testing syntax against the given regular expression
        /// </summary>
        /// <param name="matchExpression">Regular expression of syntax</param>
        /// <param name="attribute">Attribute to test</param>
        /// <returns></returns>
        private bool ValidAttributeValue(string matchExpression, TimedTextAttributeBase attribute)
        {
            Regex matchRE = null;
            if (cachedRegex.ContainsKey(matchExpression))
            {
                matchRE = cachedRegex[matchExpression];
            }
            else
            {
                matchRE = new Regex(matchExpression);
                cachedRegex.Add(matchExpression, matchRE);
            }

            if (matchRE.IsMatch(attribute.Value.Trim()))
            {
                m_styling[attribute.LocalName] = attribute.Value.Trim();
            }
            else
            {
                Error("Erroneous value " + attribute.Value + " for  attribute " + attribute.LocalName + " on " + this.ToString());
            }
            return true;
        }

        /// <summary>
        /// Validate a style attribute value
        /// </summary>
        /// <param name="attribute">timed text attribute to be validated</param>
        private void ValidStyleAttributeValue(TimedTextAttributeBase attribute)
        {
            if (attribute.Value == "inherit")
            {
                /// to do - remove inherit ias a legal value for attributes.
                m_styling[attribute.LocalName] = new Inherit();
            }
            else
            {
                switch (attribute.LocalName)
                {
                    #region Style Handlers
                    case "backgroundColor":
                        m_styling["backgroundColor"] = ColorExpression.Parse(attribute.Value);
                        break;
                    case "color":
                        m_styling["color"] = ColorExpression.Parse(attribute.Value);
                        break;
                    case "direction":
                        ValidAttributeValue(s_directionExpression, attribute);
                        break;
                    case "display":
                        ValidAttributeValue(s_displayExpression, attribute);
                        break;
                    case "displayAlign":
                        ValidAttributeValue(s_displayAlignExpression, attribute);
                        break;
                    case "dynamicFlow":
                        // dynamicFlow attribute not supported but can be ignored.
                        break;
                    case "extent":
                        if (attribute.Value.Trim() == "auto")
                        {
                            m_styling["extent"] = new AutoExtent();
                        }
                        else
                        {
                            m_styling["extent"] = new Styling.Extent(attribute.Value); 
                            if (this is TtElement)
                            {
                                NumberPair.SetAuthoringContextExtent(attribute.Value);
                            }
                        };
                        break;
                    case "fontFamily":
                        string newFontString = attribute.Value
                            .Replace("sansSerif", "Arial")
                            .Replace("serif", "Times New Roman")
                            .Replace("monospaceSansSerif", "Monaco, Lucida Console")
                            .Replace("monospaceSerif", "Courier New, Courier")
                            .Replace("monospace", "Courier New, Courier")
                            .Replace("proportionalSansSerif", "GlobalUserInterface.CompositeFont")
                            .Replace("proportionalSerif", "Times New Roman");
                        m_styling["fontFamily"] = newFontString;
                        break;
                    case "fontSize":
                        ValidAttributeValue(s_fontSizeExpression, attribute);
                        break;
                    case "fontStyle":
                        if (ValidAttributeValue(s_fontStyleExpression, attribute))
                        {
                            switch (attribute.Value.Trim())
                            {
                                case "italic": m_styling["fontStyle"] = FontStyleAttributeValue.Italic;
                                    break;
                                case "oblique": m_styling["fontStyle"] = FontStyleAttributeValue.Oblique;
                                    break;
                                case "reverseOblique": m_styling["fontStyle"] = FontStyleAttributeValue.ReverseOblique;
                                    break;
                                default: m_styling["fontStyle"] = FontWeightAttributeValue.Regular;
                                    break;
                            }
                        } break;
                    case "fontWeight":
                        if (ValidAttributeValue(s_fontWeightExpression, attribute))
                        {
                            switch (attribute.Value.Trim())
                            {
                                case "bold": m_styling["fontWeight"] = FontWeightAttributeValue.Bold;
                                    break;
                                default: m_styling["fontWeight"] = FontWeightAttributeValue.Regular;
                                    break;
                            }
                        }
                        break;
                    case "lineHeight":
                        if (ValidAttributeValue(s_lineHeightExpression, attribute))
                        {
                            switch (attribute.Value.Trim())
                            {
                                case "normal":
                                    m_styling["lineHeight"] = new NormalHeight();
                                    break;
                                default: m_styling["lineHeight"] = new LineHeight(attribute.Value);
                                    break;
                            }
                        }
                        break;
                    case "opacity":
                        if (ValidAttributeValue(s_opacityExpression, attribute))
                            m_styling["opacity"] = Double.Parse(attribute.Value, CultureInfo.InvariantCulture);
                        break;
                    case "origin":
                        switch (attribute.Value.Trim())
                        {
                            case "auto":
                                m_styling["origin"] = new AutoOrigin();
                                break;
                            case "inherit":
                                m_styling["origin"] = new Inherit();
                                break;
                            default:
                                m_styling["origin"] = new Styling.Origin(attribute.Value);
                                break;
                        };
                        break;
                    case "overflow":
                        ValidAttributeValue(s_overflowExpression, attribute);
                        break;
                    case "padding":
                        if (ValidAttributeValue(s_paddingExpression, attribute))
                        {
                            m_styling["padding"] = new Styling.PaddingThickness(attribute.Value);
                        }
                        break;
                    case "showBackground":
                        ValidAttributeValue(s_showBackgroundExpression, attribute);
                        break;
                    case "textAlign":
                        ValidAttributeValue(s_textAlignExpression, attribute);
                        break;
                    case "textDecoration":
                        if (ValidAttributeValue(s_textDecorationExpression, attribute))
                        {
                            switch (attribute.Value.Trim())
                            {   //underline | noUnderline ] || [ lineThrough  | noLineThrough ] || [ overline | noOverline
                                case "underline": m_styling["textDecoration"] = TextDecorationAttributeValue.Underline;
                                    break;
                                case "lineThrough": m_styling["textDecoration"] = TextDecorationAttributeValue.Throughline;
                                    break;
                                case "overline": m_styling["textDecoration"] = TextDecorationAttributeValue.Overline;
                                    break;
                                default: m_styling["textDecoration"] = TextDecorationAttributeValue.None;
                                    break;
                            }
                        }
                        break;
                    case "textOutline":
                        m_styling["textOutline"] = new Styling.TextOutline(attribute.Value);
                        break;
                    case "unicodeBidi":
                        ValidAttributeValue(s_unicodeBidiExpression, attribute);
                        break;
                    case "visibility":
                        ValidAttributeValue(s_visibilityExpression, attribute);
                        break;
                    case "wrapOption":
                        ValidAttributeValue(s_wrapOptionExpression, attribute);
                        break;
                    case "writingMode":
                        ValidAttributeValue(s_writingModeExpression, attribute);
                        break;
                    case "zIndex":
                        if (ValidAttributeValue(s_zIndexExpression, attribute))
                        {
                            if (attribute.Value.Trim() == "auto") m_styling["zIndex"] = defaultZ;
                            else m_styling["zIndex"] = ((double)Int32.Parse(attribute.Value, CultureInfo.InvariantCulture)) + defaultZ;
                            defaultZ += 0.0001; // allow for 10,000 regions with the same z order.
                        }
                        break;
                    #endregion
                    default:
                        Error("Erroneous style: namespace attribute " + attribute.LocalName + " on " + this.ToString());
                        break;
                };
            }
        }

        /// <summary>
        /// Validate an XML attribute value
        /// </summary>
        /// <param name="attribute"></param>
        protected void ValidXmlAttributeValue(TimedTextAttributeBase attribute)
        {
            switch (attribute.LocalName)
            {
                case "lang":
                    Language = attribute.Value;
                    break;
                case "space":
                    m_styling[attribute.LocalName] = attribute.Value;
                    break;
                case "id":
                    if (this.Id == null)
                    {
                        this.Id = attribute.Value;
                    }
                    else
                    {
                        Error("multiple xml:Id defined on " + this.ToString());
                    }
                    if (this is RegionElement) Root.Regions[attribute.Value] = (RegionElement)this;
                    if (this is StyleElement) Root.Styles[attribute.Value] = (StyleElement)this;
                    if (this is Metadata.AgentElement) Root.Agents[attribute.Value] = (Metadata.AgentElement)this;
                    break;
                default:
                    Error("Erroneous xml: namespace attribute " + attribute.LocalName + " on " + this.ToString());
                    break;
            };
        }

        /// <summary>
        /// validate a metadata attribute value
        /// </summary>
        /// <param name="attribute"></param>
        private void ValidMetadataAttributeValue(TimedTextAttributeBase attribute)
        {
            #region metadata attribute

            switch (attribute.LocalName)
            {
                case "agent":
                    // ToDo - ensure its an IDREF to an agent element.
                    break;
                case "role":
                    switch (attribute.Value)
                    {
                        case "action":
                        case "caption":
                        case "description":
                        case "dialog":
                        case "expletive":
                        case "kinesic":
                        case "lyrics":
                        case "music":
                        case "narration":
                        case "quality":
                        case "sound":
                        case "source":
                        case "suppressed":
                        case "reproduction":
                        case "thought":
                        case "title":
                        case "transcription":
                            this.Metadata[attribute.LocalName] = attribute.Value;
                            break;
                        default:
                            if (!attribute.Value.StartsWith("x-", StringComparison.Ordinal))
                            {
                                Error("Erroneous metadata namespace attribute " + attribute.LocalName + " on " + this.ToString());
                            }
                            else
                            {
                                this.Metadata[attribute.LocalName] = attribute.Value;
                            }
                            break;
                    }
                    break;
                default:
                    Error("Erroneous metadata namespace attribute " + attribute.LocalName + " on " + this.ToString());
                    break;
            };
            #endregion
        }
        #endregion

        #region Element Validity
        /// <summary>
        /// Test whether an element's content model, and all its descendants
        /// are valid Timed Text
        /// throws an exception if invalid.
        /// </summary>
        protected abstract void ValidElements();

        /// <summary>
        /// Test whether an elements attributes are allowed by Timed Text
        /// throws an exception if invalid.
        /// </summary>
        protected abstract void ValidAttributes();
        #endregion

        #region Parsing
        /// <summary>
        /// Convert an XElement to the internal TimedText classes.
        /// </summary>
        /// <param name="timedTextData">Raw XML construct</param>
        /// <returns>Timed text Element hierachy</returns>
        public static TimedTextElementBase Parse(XElement timedTextData)
        {
            InitialiseDefaults();
            return ParseRecursive(timedTextData, null, false);
        }

        /// <summary>
        /// Initialise all the components for this parse
        /// </summary>
        private static void InitialiseDefaults()
        {
            TimeExpression.InitializeParameters();
            ParameterElement.InitializeParameters();
        }

        /// <summary>
        /// Convert an XElement to the internal TimedText classes.
        /// </summary>
        /// <param name="timedTextData">Raw XML construct</param>
        /// <param name="root">root element of the tree</param>
        /// <returns>tt_element hierachy</returns>
        private static TimedTextElementBase ParseRecursive(XElement xmlElement, TtElement root, bool preserveContext)
        {
            string element = xmlElement.Name.LocalName;
            string nameSpace = NamespaceFromTimedTextNamespace(xmlElement.Name.NamespaceName);
            TimedTextElementBase parentNode = null;

            if (!string.IsNullOrEmpty(nameSpace))
            {
                // To meet MSFT naming conventions, have to manipulate the name.
                string initialCapital = element[0].ToString().ToUpperInvariant();
                string rest = element.Substring(1);
                string conventionName = initialCapital + rest + "Element";
                // if there is a namespace, then its a timed text element
                parentNode = GetElementFromName(nameSpace + conventionName);
                parentNode.LocalName = element;
                parentNode.Namespace = xmlElement.Name.NamespaceName;
            }
            /// if node is still null, either we failed to implement the element
            /// or its an element in a foreign namespace, either way we bail.
            if (parentNode == null) return null;

            #region test if root element
            TtElement newRoot = (root == null) ? parentNode as TtElement : root;
            // null should only occur in the first call,
            if (newRoot == null)
            {
                Error("tt not at root of document");
            }
            parentNode.Root = newRoot;
            #endregion

            bool localPreserve = preserveContext;  // record whether xml:space=preserve is in effect

            #region process raw xml attributes into timed text equivalents
            foreach (XAttribute xmlAttribute in xmlElement.Attributes())
            {
                if (!xmlAttribute.IsNamespaceDeclaration)
                {
                    // copy the attribute identity
                    TimedTextAttributeBase attribute = new TimedTextAttributeBase();
                    attribute.Parent = parentNode;
                    attribute.LocalName = xmlAttribute.Name.LocalName;
                    attribute.Value = xmlAttribute.Value;

                    // not sure if it is absolutely correct to move 
                    // empty namespace elements into tt namespace but seems
                    // to work.
                    attribute.NamespaceName =
                        (string.IsNullOrEmpty(xmlAttribute.Name.NamespaceName)
                            ? xmlElement.Name.NamespaceName
                            : xmlAttribute.Name.NamespaceName
                         );

                    // attach new attribute to current element
                    parentNode.Attributes.Add(attribute);

                    // check whether we are changing the space preserve behaviour
                    if (attribute.IsXmlAttribute() && attribute.LocalName == "space")
                    {
                        localPreserve = (attribute.Value == "preserve");
                    }
                    // record the type of preservation as a local style.
                    parentNode.SetLocalStyle("#preserve", localPreserve);
                }
            }
            #endregion

            #region process child elements
            foreach (XNode xmlNode in xmlElement.Nodes())
            {
                switch (xmlNode.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            #region convert XML Element to Timed Text Element
                            TimedTextElementBase child = ParseRecursive(xmlNode as XElement, newRoot, localPreserve);
                            if (child != null)
                            {
                                parentNode.Children.Add(child);
                                if (child is BodyElement)
                                {
                                    parentNode.Body = child as BodyElement;
                                }
                                if (parentNode is TtElement)
                                {
                                    TtElement ttElement = parentNode as TtElement;
                                    if (child is HeadElement)
                                    {
                                        ttElement.Head = child as HeadElement;
                                    }
                                }
                                child.Parent = parentNode;
                                child.Root = parentNode.Root;
                            }
                            #endregion
                            break;
                        }
                    case XmlNodeType.Text:
                    case XmlNodeType.CDATA:
                        {
                            #region convert XML Text into an anonymous span element
                            if (IsContentElement(parentNode))
                            {
                                #region elements that admit PCDATA as children get anonymous spans
                                SpanElement text;
                                if (!localPreserve)
                                {  // squeeze out all the redundant whitespace
                                    string normalised = NormaliseWhitespace(xmlNode);
                                    text = new AnonymousSpanElement(normalised);
                                }
                                else
                                {  // preserve the raw text as it came in
                                    text = new AnonymousSpanElement(xmlNode.ToString());
                                }
                                parentNode.Children.Add(text);
                                text.Parent = parentNode;
                                #endregion
                            }
                            else
                            {
                                #region test non content element for non-whitespace error.
                                if (NormaliseWhitespace(xmlNode) != " ")
                                {
                                    Error("Use of non whitespace in " + parentNode);
                                }
                                #endregion
                            }
                            #endregion
                            break;
                        }
                    case XmlNodeType.SignificantWhitespace:
                    case XmlNodeType.Whitespace:
                        {
                            #region for some reason we dont seem to get these.
                            if (IsContentElement(parentNode))
                            {
                                char[] newlines = { '\n' };// what other newlines are there?
                                foreach (var br in xmlNode.ToString().Split(newlines, StringSplitOptions.None))
                                {
                                    if (string.IsNullOrEmpty(br))
                                    {
                                        parentNode.Children.Add(new BrElement());
                                    }
                                    else
                                    {
                                        SpanElement text = new AnonymousSpanElement(br);
                                        parentNode.Children.Add(text);
                                        text.Parent = parentNode;
                                    }
                                }
                            }
                            #endregion
                            break;
                        }
                }
            }
            #endregion
            return parentNode;
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// convert newlines to space, and collpase runs of space to a single space
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string NormaliseWhitespace(XNode n)
        {
            string normalised = n.ToString().Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
            while (normalised.Contains("  "))
            {
                normalised = normalised.Replace("  ", " ");
            }
            return normalised;
        }

        /// <summary>
        /// Is it a content element for purposes of parenting anonymous span's?
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsContentElement(TimedTextElementBase node)
        {
            if (node is PElement) return true;
            if (node is SpanElement) return true;
            if (IsMetadataContentElement(node)) return true;
            if (IsParameterContentElement(node)) return true;
            return false;
        }

        /// <summary>
        /// Metadata items that admit PCDATA as content
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsMetadataContentElement(TimedTextElementBase node)
        {
            if (node is Metadata.TitleElement) return true;
            if (node is Metadata.NameElement) return true;
            if (node is Metadata.DescElement) return true;
            if (node is Metadata.CopyrightElement) return true;
            if (node is Metadata.AgentElement) return true;
            if (node is Metadata.ActorElement) return true;

            return false;
        }

        /// <summary>
        /// Parameter items that admit PCDATA as content
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsParameterContentElement(TimedTextElementBase node)
        {
            if (node is Parameter.ExtensionElement) return true;
            if (node is Parameter.FeatureElement) return true;
            return false;
        }

        #endregion

        #region  Namespace Handling
        /// <summary>
        /// Get the local C# namespace from the Timed Text XML namespace
        /// </summary>
        /// <param name="p">XML namespace</param>
        /// <returns>C# namespace prefix as a string</returns>
        private static string NamespaceFromTimedTextNamespace(string p)
        {
            switch (p)
            {   // got to be a better way to do this using reflection?
                case "http://www.w3.org/2006/10/ttaf1":
                case "http://www.w3.org/ns/ttml":
                    {
                        return "TimedText.";
                    }
                case "http://www.w3.org/2006/10/ttaf1#metadata":
                case "http://www.w3.org/ns/ttml#metadata":
                    {
                        return "TimedText.Metadata.";
                    }
                case "http://www.w3.org/2006/10/ttaf1#style":
                case "http://www.w3.org/2006/10/ttaf1#styling":
                case "http://www.w3.org/ns/ttml#styling":
                    {
                        return "TimedText.Style.";
                    }
                case "http://www.w3.org/2006/10/ttaf1#parameter":
                case "http://www.w3.org/ns/ttml#parameter":
                    {
                        return "TimedText.Parameter.";
                    }
                case "http://www.w3.org/ns/ttml/profile":
                    {
                        return "TimedText.Profile.";
                    }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// Get recorded metadata for the given attribute
        /// </summary>
        /// <param name="attribute">metadata attribute to retrieve</param>
        /// <returns>attribute value</returns>
        public string GetMetadata(string attribute)
        {
            if (this.Metadata.ContainsKey(attribute))
            {
                return (string)this.Metadata[attribute];
            }
            else
            {
                return ""; // 
            }
        }
    }
}
