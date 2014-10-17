using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST.VPAID
{
    /// <summary>
    /// Represents a VAST non linear creative to be used by a VPAID plugin
    /// </summary>
    public class NonLinearSource : ICreativeSource
    {
        readonly NonLinear_type nonLinear;
        readonly VASTADInLineCreativeNonLinearAds nonLinearParent;
        readonly VASTADInLine ad;

        internal NonLinearSource(VASTADInLine Ad, VASTADInLineCreativeNonLinearAds NonLinearParent, NonLinear_type NonLinear)
        {
            ad = Ad;
            nonLinearParent = NonLinearParent;
            nonLinear = NonLinear;
        }

        public string MediaSource
        {
            get
            {
                switch (nonLinear.ItemElementName)
                {
                    case ItemChoiceType1.StaticResource:
                        var resource = nonLinear.Item as NonLinear_typeStaticResource;
                        return resource.Value;
                    case ItemChoiceType1.IFrameResource:
                        return nonLinear.Item as string;
                    case ItemChoiceType1.HTMLResource:
                        return nonLinear.Item as string;
                    default:
                        return null;
                }
            }
        }

        public string Id
        {
            get { return nonLinear.id; }
        }

        public TimeSpan? Duration
        {
            get
            {
                if (!nonLinear.minSuggestedDurationSpecified)
                    return null;
                else if (nonLinear.minSuggestedDuration < DateTime.Today)
                    return nonLinear.minSuggestedDuration.Subtract(new DateTime());
                else
                    return nonLinear.minSuggestedDuration.Subtract(DateTime.Today);
            }
        }

        public string MimeType
        {
            get
            {
                switch (nonLinear.ItemElementName)
                {
                    case ItemChoiceType1.StaticResource:
                        var resource = nonLinear.Item as NonLinear_typeStaticResource;
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
                if (nonLinearParent.TrackingEvents != null)
                    result = nonLinearParent.TrackingEvents.GroupBy(te => (TrackingEventEnum)Enum.Parse(typeof(TrackingEventEnum), te.@event.ToString(), false), te => te.Value);
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
            get { return nonLinear.NonLinearClickThrough; }
        }

        public Size Dimensions
        {
            get
            {
                if (nonLinear.width.ToDouble().HasValue && nonLinear.height.ToDouble().HasValue)
                {
                    return new Size(nonLinear.width.ToDouble().Value, nonLinear.height.ToDouble().Value);
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        public Size ExpandedDimensions
        {
            get
            {
                if (nonLinear.expandedWidth.ToDouble().HasValue && nonLinear.expandedHeight.ToDouble().HasValue)
                {
                    return new Size(nonLinear.expandedWidth.ToDouble().Value, nonLinear.expandedHeight.ToDouble().Value);
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        public CreativeSourceType Type
        {
            get { return CreativeSourceType.NonLinear; }
        }

        public MediaSourceEnum MediaSourceType
        {
            get
            {
                switch (nonLinear.ItemElementName)
                {
                    case ItemChoiceType1.StaticResource:
                        return MediaSourceEnum.Static;
                    case ItemChoiceType1.IFrameResource:
                        return MediaSourceEnum.IFrame;
                    case ItemChoiceType1.HTMLResource:
                        return MediaSourceEnum.HTML;
                    default:
                        return default(MediaSourceEnum);
                }
            }
        }

        public string ExtraInfo
        {
            get { return nonLinear.AdParameters; }
        }

        public bool IsScalable
        {
            get { return nonLinear.scalableSpecified ? nonLinear.scalable : false; }
        }

        public bool MaintainAspectRatio
        {
            get { return nonLinear.maintainAspectRatioSpecified ? nonLinear.maintainAspectRatio : true; }
        }

        public string AltText
        {
            get { return string.Empty; }
        }

        public bool IsStreaming
        {
            get { return false; }
        }
    }
}
