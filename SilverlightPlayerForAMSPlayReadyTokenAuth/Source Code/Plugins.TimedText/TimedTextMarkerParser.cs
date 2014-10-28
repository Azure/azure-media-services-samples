using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using TimedText;
using TimedText.Timing;
using TimedTextMetadata = TimedText;
using TimedTextTiming = TimedText.Timing;
#if WINDOWS_PHONE || OOB
using System.Net;
#else
using System.Windows.Browser;
#endif

namespace Microsoft.SilverlightMediaFramework.Plugins.TimedText
{
    /// <summary>
    /// Accepts a string of XML defining the markers and returns a collection of MediaMarker objects.
    /// </summary>
    /// <remarks>
    /// For more information about Timed Text Authoring Format 1.0, 
    /// see http://www.w3.org/TR/2009/WD-ttaf1-dfxp-20090602/#dfxp-example-subtitle-1
    /// </remarks>
    internal class TimedTextMarkerParser : IMarkerParser
    {
        #region IMarkerParser Members

        public IEnumerable<CaptionRegion> ParseMarkerCollection(XDocument markerXml, TimeSpan timeOffset, TimeSpan defaultEndTime)
        {
            try
            {
                TtElement document = BuildDocument(markerXml, timeOffset, defaultEndTime);
                return BuildRegions(document);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TimedTextMarkerParser.ParseMarkerCollection received invalid input.", ex);
            }
        }

        #endregion






        private static TtElement BuildDocument(XDocument xml, TimeSpan timeOffset, TimeSpan defaultEndTime)
        {
            TtElement parsetree = null;

            try
            {
                parsetree = (TtElement)TimedTextElementBase.Parse(xml.Root);
            }
            catch (TimedTextException) { }

            if (parsetree == null) throw new TimedTextException("No Parse tree returned");
            if (!parsetree.Valid()) throw new TimedTextException("Document is Well formed XML, but invalid Timed Text");

            var startTime = new TimeCode(timeOffset, TimeExpression.CurrentSmpteFrameRate);
            var endTime = new TimeCode(defaultEndTime, TimeExpression.CurrentSmpteFrameRate);

            parsetree.ComputeTimeIntervals(TimeContainer.Par, startTime, endTime);

            return parsetree;
        }

        private static IEnumerable<CaptionRegion> BuildRegions(TtElement document)
        {
            var regionElementsHash = document.Head != null
                                    ? document.Head.Children.Where(i => i is LayoutElement)
                                                           .SelectMany(i => i.Children)
                                                           .Cast<RegionElement>()
                                                           .ToDictionary(i => i.Id, i => i)
                                    : new Dictionary<string, RegionElement>();

            var captionRegionsHash = regionElementsHash.Values
                                                   .Select(MapToCaptionRegion)
                                                   .ToDictionary(i => i.Id, i => i);

            if (document.Body != null)
            {
                BuildCaptions(document.Body, regionElementsHash, captionRegionsHash);
            }

            return captionRegionsHash.Values.ToList();
        }

        private static string GetInheritedAttribute(TimeTree<TimedTextElementBase, TimedTextAttributeBase> timedTextElement, string attributeName)
        {
            var regionNameAttribute = timedTextElement.Attributes.FirstOrDefault(i => i.LocalName == attributeName);
            if (regionNameAttribute == null)
            {
                var parent = timedTextElement.Parent;
                if (parent != null)
                {
                    return GetInheritedAttribute(parent, attributeName);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return regionNameAttribute.Value;
            }
        }

        private static string GetDescendentAttribute(TimeTree<TimedTextElementBase, TimedTextAttributeBase> timedTextElement, string attributeName)
        {
            foreach (var child in timedTextElement.Children)
            {
                var regionNameAttribute = timedTextElement.Attributes.FirstOrDefault(i => i.LocalName == attributeName);
                if (regionNameAttribute != null) return regionNameAttribute.Value;
            }
            return null;
        }

        private static void BuildCaptions(TimedTextElementBase timedTextElement, IDictionary<string, RegionElement> regionElementsHash, IDictionary<string, CaptionRegion> captionRegionsHash)
        {
            var pElement = timedTextElement as PElement;

            if (pElement != null)
            {
                // region inheritence per spec: http://www.w3.org/TR/ttaf1-dfxp/#semantics-region-layout-step-1
                string regionName =
                    GetInheritedAttribute(timedTextElement, "region") ??
                    GetDescendentAttribute(timedTextElement, "region") ??
                    RegionElement.DefaultRegionName ??
                    string.Empty;

                RegionElement regionElement;
                if (regionElementsHash.TryGetValue(regionName, out regionElement))
                {
                    var captionElement = MapToCaption(pElement, regionElement);

                    CaptionRegion captionRegion;
                    if (captionRegionsHash.TryGetValue(regionName, out captionRegion))
                    {
                        captionElement.Index = captionRegion.Children.Count;
                        captionRegion.Children.Add(captionElement);
                    }
                }
            }
            else if (timedTextElement.Children != null)
            {
                timedTextElement.Children
                                .Cast<TimedTextElementBase>()
                                .ForEach(i => BuildCaptions(i, regionElementsHash, captionRegionsHash));
            }
        }

        private static CaptionRegion MapToCaptionRegion(RegionElement regionElement)
        {
            var endTime = regionElement.End.TotalSeconds >= TimeSpan.MaxValue.TotalSeconds
                            ? TimeSpan.MaxValue
                            : TimeSpan.FromSeconds(regionElement.End.TotalSeconds);

            var captionRegion = new CaptionRegion
            {
                Id = regionElement.Id,
                Begin = TimeSpan.FromSeconds(regionElement.Begin.TotalSeconds),
                End = endTime,
                Style = TimedTextStyleParser.MapStyle(regionElement, null)
            };

            foreach (TimedTextElementBase element in regionElement.Children)
            {
                TimedTextElement child = BuildTimedTextElements(element, null);
                if (child != null && child.CaptionElementType == TimedTextElementType.Animation)
                {
                    captionRegion.Animations.Add(child as TimedTextAnimation);
                }
            }

            return captionRegion;
        }

        private static CaptionElement MapToCaption(PElement pElement, RegionElement region)
        {
            var captionElement = BuildTimedTextElements(pElement, region);
            captionElement.Id = pElement.Id ?? Guid.NewGuid().ToString();

            return captionElement as CaptionElement;
        }

        private static TimedTextElement BuildTimedTextElements(TimedTextElementBase element, RegionElement region)
        {
            TimedTextElement timedTextElement = CreateTimedTextElement(element, region);

            foreach (TimedTextElementBase c in element.Children)
            {
                TimedTextElement child = BuildTimedTextElements(c, region);
                if (child is TimedTextAnimation)
                {
                    timedTextElement.Animations.Add((TimedTextAnimation)child);
                }
                else if (timedTextElement is CaptionElement && child is CaptionElement)
                {
                    ((CaptionElement)child).Index = timedTextElement.Children.Count;
                    timedTextElement.Children.Add((CaptionElement)child);
                }
            }

            return timedTextElement;
        }

        private static TimedTextElement CreateTimedTextElement(TimedTextElementBase element, RegionElement region)
        {
            var captionElement = element is SetElement
                                    ? (TimedTextElement) BuildCaptionAnimationElement(element)
                                    : new CaptionElement();

            var endTime = element.End.TotalSeconds >= TimeSpan.MaxValue.TotalSeconds
                ? TimeSpan.MaxValue
                : TimeSpan.FromSeconds(element.End.TotalSeconds);

            captionElement.End = endTime;
            captionElement.Begin = TimeSpan.FromSeconds(element.Begin.TotalSeconds);

            if (element is BrElement)
            {
                captionElement.CaptionElementType = TimedTextElementType.LineBreak;
            }
            else if (element is AnonymousSpanElement)
            {
                var span = element as AnonymousSpanElement;
                captionElement.CaptionElementType = TimedTextElementType.Text;
                captionElement.Content = HttpUtility.HtmlDecode(span.Text);
                captionElement.Style = TimedTextStyleParser.MapStyle(element, region);
            }
            else if (!(element is SetElement))
            {
                captionElement.CaptionElementType = TimedTextElementType.Container;
                captionElement.Style = TimedTextStyleParser.MapStyle(element, region);
            }

            return captionElement;
        }

        private static TimedTextAnimation BuildCaptionAnimationElement(TimedTextElementBase element)
        {
            var propertyName = element.Attributes
                                      .Where(i => TimedTextStyleParser.IsValidAnimationPropertyName(i.LocalName))
                                      .Select(i => i.LocalName)
                                      .FirstOrDefault();

            return !propertyName.IsNullOrWhiteSpace()
                    ? new TimedTextAnimation
                        {
                            CaptionElementType = TimedTextElementType.Animation,
                            PropertyName = propertyName,
                            Style = TimedTextStyleParser.MapStyle(element, null)
                        }
                    : null;
        }
    }
}