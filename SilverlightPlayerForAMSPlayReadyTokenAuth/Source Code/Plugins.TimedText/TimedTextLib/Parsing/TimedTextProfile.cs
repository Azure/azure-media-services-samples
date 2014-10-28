// <copyright file="TimedTextProfile.cs" company="Microsoft Corporation">
// ===============================================================================
// MICROSOFT CONFIDENTIAL
// Microsoft Accessibility Business Unite
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
#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Globalization;
using System.Collections.ObjectModel;

#endregion

namespace TimedText.Parameter
{

    public struct FeatureValue
    {
        public string Label { get; set; }
        public bool Required { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is FeatureValue)
            {
                return this == (FeatureValue)obj;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

       public static bool operator ==(FeatureValue value1, FeatureValue value2)
        {

            bool sameLabel = value1.Label == value2.Label;
            if (sameLabel) return (value1.Required == value2.Required);
            return false;
        }
        public static bool operator !=(FeatureValue value1, FeatureValue value2)
        {
            bool sameLabel = value1.Label == value2.Label;
            if (sameLabel) return !(value1.Required == value2.Required);
            return true;
        }
    };

   public abstract class ParameterElement : TimedText.TimedTextElementBase
    {
 
        static Dictionary<string, bool> m_transformProfile = new Dictionary<string, bool>();
        static Dictionary<string, bool> m_presentationProfile = new Dictionary<string, bool>();

        static Dictionary<string, bool> m_features = new Dictionary<string, bool>();
        static Dictionary<string, bool> m_extensions = new Dictionary<string, bool>();
        static List<FeatureValue> m_profile = new List<FeatureValue>();

        public static void InitializeParameters()
        {
            Features.Clear();
            Extensions.Clear();
        }

        public static void SetProfile(XContainer profile)
        {
            string baseUri = "http://www.w3.org/ns/ttml/feature";

            var features = from f in profile.Descendants("{http://www.w3.org/ns/ttml#parameter}feature")
                           select new FeatureValue
                           {
                               Required = f.Attribute("value").Value == "required",
                               Label = baseUri + f.Value
                           };
            m_profile.Clear();
            foreach (var f in features)
            {
                m_profile.Add(f);
            }
        }

        public static void SetStaticProfiles(XContainer transform, XContainer presentation)
        {
            string baseUri = "http://www.w3.org/ns/ttml/feature";

            var features = from f in transform.Descendants("{http://www.w3.org/ns/ttml#parameter}feature")
                           select new FeatureValue
                           {
                               Required = f.Attribute("value").Value == "required",
                               Label = f.Value
                           };
            m_transformProfile.Clear();
            foreach (var f in features)
            {
                m_transformProfile.Add(baseUri + f.Label, f.Required);
            }

            features = from f in presentation.Descendants("{http://www.w3.org/ns/ttml#parameter}feature")
                       select new FeatureValue
                       {
                           Required = f.Attribute("value").Value == "required",
                           Label = f.Value
                       };
            m_presentationProfile.Clear();
            foreach (var f in features)
            {
                m_presentationProfile.Add(baseUri + f.Label, f.Required);
            }
        }


        public static Collection<string> NonFeatures
        {
            get
            {
                Collection<string> set = new Collection<string>();
                foreach (var f in m_profile)
                {
                    if (!f.Required)
                        set.Add(f.Label);
                }
                return set;
            }
        }

        public static Dictionary<string, bool> Features
        {
            get { return m_features; }
        }
        public static Dictionary<string, bool> Extensions
        {
            get { return m_extensions; }
        }
        public static Dictionary<string, bool> TransformProfile
        {
            get { return m_transformProfile; }
        }
        public static Dictionary<string, bool> PresentationProfile
        {
            get { return m_presentationProfile; }
        }

        protected void ValidParameterAttribute(bool feature, TimedTextAttributeBase attribute, string value)
        {
            string baseUri = (Parent as ParameterElement).BaseAttributeValue();
            string key = baseUri + value;
            switch (attribute.LocalName)
            {
                case "value":
                    if (feature)
                    {
                        if (Features.ContainsKey(key))
                        {
                            Features[key] |= attribute.Value == "required";
                        }
                        else
                        {
                            Features[key] = attribute.Value == "required";
                        }
                    }
                    else
                    {
                        if (Extensions.ContainsKey(key))
                        {
                            Extensions[key] |= attribute.Value == "required";
                        }
                        else
                        {
                            Extensions[key] = attribute.Value == "required";
                        }
                    }
                    break;
                default:
                    Error("Erroneous parameter attribute " + attribute.LocalName + " on " + this.ToString());
                    break;
            };
        }

        protected string BaseAttributeValue()
        {
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                switch (attribute.LocalName)
                {
                    case "base":
                        return attribute.Value;
                }
            }
            return "http://www.w3.org/ns/ttml/feature";
        }
    }


    public class ProfileElement : ParameterElement
    {
        protected override void ValidAttributes()
        {

            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    ValidXmlAttributeValue(attribute);
                }
                else
                {
                    switch (attribute.LocalName)
                    {
                        case "use":
                            ValidProfileAttribute(attribute);
                            break;

                        default:
                            Error("Erroneous feature attribute " + attribute.LocalName + " on " + this.ToString());
                            break;
                    }
                }
            }

            CheckSupportedFeatures();
        }

        private static void CheckSupportedFeatures()
        {
            bool noExtensions = true;
            StringBuilder sb = new StringBuilder("The following features are not supported:\n");
            foreach (var feature in Extensions.Keys)
            {
                if (Extensions[feature])
                {
                    sb.Append("\n");
                    sb.Append(feature);
                    noExtensions = false;
                }
            }
            foreach (var feature in Features.Keys)
            {
                if (Features[feature] && NonFeatures.Contains(feature))
                {
                    sb.Append("\n");
                    sb.Append(feature);
                    noExtensions = false;
                }
            }

            if (!noExtensions)
                throw new TimedTextException(sb.ToString());
        }

        protected override void ValidElements()
        {
            int child = 0;

            while ((child < Children.Count)
                && ((Children[child] is MetadataElement)
                  || (Children[child] is Metadata.MetadataElement)
                  ))
            {
                child++;
            }
            while ((child < Children.Count)
                 && ((Children[child] is Parameter.FeaturesElement)
                    ))
            {
                child++;
            }
            while ((child < Children.Count)
                  && ((Children[child] is Parameter.ExtensionsElement)
                     ))
            {
                child++;
            }

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }

            // now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
        }
    }

    public class FeaturesElement : ParameterElement
    {
        protected override void ValidAttributes()
        {
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    switch (attribute.LocalName)
                    {
                        case "base":
                            break;
                        default:
                            ValidXmlAttributeValue(attribute);
                            break;
                    }
                }
                else
                {
                    Error("Erroneous feature attribute " + attribute.LocalName + " on " + this.ToString());
                }
            }
        }

        protected override void ValidElements()
        {
            int child = 0;

            while ((child < Children.Count)
                && ((Children[child] is MetadataElement)
                  || (Children[child] is Metadata.MetadataElement)
                  ))
            {
                child++;
            }
            while ((child < Children.Count)
                 && ((Children[child] is Parameter.FeatureElement)
                    ))
            {
                child++;
            }

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }

            // now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
        }
    }

    public class FeatureElement : ParameterElement
    {
        public string Text { get; set; }

        protected override void ValidAttributes()
        {
            // now check each of the attributes is individually valid
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    ValidXmlAttributeValue(attribute);
                }
                else
                {
                    switch (attribute.LocalName)
                    {
                        case "value":
                            ValidParameterAttribute(true, attribute, Text);
                            break;

                        default:
                            Error("Erroneous feature attribute " + attribute.LocalName + " on " + this.ToString());
                            break;
                    }
                }
            }
        }

        protected override void ValidElements()
        {
            int child = 0;
            StringBuilder sb = new StringBuilder();
            while ((child < Children.Count)
                && ((Children[child] is AnonymousSpanElement)

                  ))
            {
                sb.Append((Children[child] as AnonymousSpanElement).Text);
                child++;
            }
            Text = sb.ToString();

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }
        }
    }

    public class ExtensionsElement : ParameterElement
    {
        string m_base = "http://www.w3.org/2006/10/ttaf1/feature";

        public string BaseAttributeValue
        {
            get { return m_base; }
        }

        protected override void ValidAttributes()
        {
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    switch (attribute.LocalName)
                    {
                        case "base":
                            m_base = attribute.Value;
                            break;
                        default:
                            ValidXmlAttributeValue(attribute);
                            break;
                    }
                }
                else
                {
                    Error("Erroneous extensions attribute " + attribute.LocalName + " on " + this.ToString());
                }
            }
        }

        protected override void ValidElements()
        {
            int child = 0;

            while ((child < Children.Count)
                && ((Children[child] is MetadataElement)
                  || (Children[child] is Metadata.MetadataElement)
                  ))
            {
                child++;
            }
            while ((child < Children.Count)
                  && ((Children[child] is Parameter.ExtensionElement)
                     ))
            {
                child++;
            }

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }

            // now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
        }
    }

    public class ExtensionElement : ParameterElement
    {
        public string Text { get; set; }

        protected override void ValidAttributes()
        {
            // now check each of the attributes is individually valid
            foreach (TimedTextAttributeBase attribute in Attributes)
            {
                if (attribute.IsXmlAttribute())
                {
                    ValidXmlAttributeValue(attribute);
                }
                else
                {
                    switch (attribute.LocalName)
                    {
                        case "value":
                            ValidParameterAttribute(false, attribute, Text);
                            break;

                        default:
                            Error("Erroneous extension attribute " + attribute.LocalName + " on " + this.ToString());
                            break;
                    }
                }
            }
        }

        protected override void ValidElements()
        {
            int child = 0;
            StringBuilder sb = new StringBuilder();

            while ((child < Children.Count)
                && ((Children[child] is AnonymousSpanElement)

                  ))
            {
                sb.Append((Children[child] as AnonymousSpanElement).Text);
                child++;
            }
            Text = sb.ToString();

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }
        }
    }

}
