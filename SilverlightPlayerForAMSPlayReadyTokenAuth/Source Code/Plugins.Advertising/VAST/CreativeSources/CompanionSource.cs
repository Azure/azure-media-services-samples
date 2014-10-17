using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using System.Net;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST.VPAID
{
    /// <summary>
    /// Represents a VAST companion creative to be used by a VPAID plugin
    /// </summary>
    public class CompanionSource : ICreativeSource
    {
        readonly Companion_type comp;
        readonly VASTADInLine ad;

        internal CompanionSource(VASTADInLine Ad, Companion_type Comp)
        {
            ad = Ad;
            comp = Comp;
        }

        public string MediaSource
        {
            get
            {
                switch (comp.ItemElementName)
                {
                    case ItemChoiceType.StaticResource:
                        var resource = comp.Item as Companion_typeStaticResource;
                        return resource.Value;
                    case ItemChoiceType.IFrameResource:
                        return comp.Item as string;
                    case ItemChoiceType.HTMLResource:
                        return comp.Item as string;
                    default:
                        return null;
                }
            }
        }

        public string Id
        {
            get { return comp.id; }
        }

        public TimeSpan? Duration
        {
            get { return null; }
        }

        public string MimeType
        {
            get
            {
                switch (comp.ItemElementName)
                {
                    case ItemChoiceType.StaticResource:
                        var resource = comp.Item as Companion_typeStaticResource;
                        return resource.creativeType;
                    default:
                        return null;
                }
            }
        }

        private IEnumerable<IGrouping<TrackingEventEnum, string>> TrackingEvents
        {
            get
            {
                var result = Enumerable.Empty<IGrouping<TrackingEventEnum, string>>();
                if (comp.TrackingEvents != null)
                    result = comp.TrackingEvents.GroupBy(te => (TrackingEventEnum)Enum.Parse(typeof(TrackingEventEnum), te.@event.ToString(), false), te => te.Value);
                if (ad.Error != null)
                    result = result.Concat((new[] { ad.Error }).GroupBy(te => TrackingEventEnum.error, te => te));
                if (ad.Impression != null)
                    result = result.Concat(ad.Impression.GroupBy(te => TrackingEventEnum.impression, te => te.Value));
                return result;
            }
        }

        public void Track(TrackingEventEnum eventToTrack)
        {
            TrackingEvents
                .Where(te => te.Key == eventToTrack)
                .SelectMany(v => v)
                .ForEach(v => FireTracking(v));
        }

        void FireTracking(string simpleURL)
        {
            if (!string.IsNullOrEmpty(simpleURL))
            {
                new WebClient().DownloadStringAsync(new Uri(simpleURL));
            }
        }

        public string ClickUrl
        {
            get { return comp.CompanionClickThrough; }
        }

        public Size ExpandedDimensions
        {
            get
            {
                if (comp.expandedWidth.ToDouble().HasValue && comp.expandedHeight.ToDouble().HasValue)
                {
                    return new Size(comp.expandedWidth.ToDouble().Value, comp.expandedHeight.ToDouble().Value);
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        public Size Dimensions
        {
            get
            {
                if (comp.width.ToDouble().HasValue && comp.height.ToDouble().HasValue)
                {
                    return new Size(comp.width.ToDouble().Value, comp.height.ToDouble().Value);
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        public CreativeSourceType Type
        {
            get { return CreativeSourceType.Companion; }
        }

        public MediaSourceEnum MediaSourceType
        {
            get
            {
                switch (comp.ItemElementName)
                {
                    case ItemChoiceType.StaticResource:
                        return MediaSourceEnum.Static;
                    case ItemChoiceType.IFrameResource:
                        return MediaSourceEnum.IFrame;
                    case ItemChoiceType.HTMLResource:
                        return MediaSourceEnum.HTML;
                    default:
                        return default(MediaSourceEnum);
                }
            }
        }

        public string ExtraInfo
        {
            get { return comp.AdParameters; }
        }

        public bool IsScalable
        {
            get { return false; }
        }

        public bool MaintainAspectRatio
        {
            get { return true; }
        }

        public string AltText
        {
            get { return comp.AltText; }
        }

        public bool IsStreaming
        {
            get { return false; }
        }
    }
}
