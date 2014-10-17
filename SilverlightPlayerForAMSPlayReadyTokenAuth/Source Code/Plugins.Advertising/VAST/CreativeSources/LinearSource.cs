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
    /// Represents a VAST linear creative to be used by a VPAID plugin
    /// </summary>
    public class LinearSource : ICreativeSource
    {
        readonly VASTADInLine ad;
        readonly VASTADInLineCreativeLinearMediaFile media;
        readonly VASTADInLineCreativeLinear linear;
        readonly VASTADWrapper[] wrappers;

        internal LinearSource(VASTADInLine Ad, VASTADInLineCreativeLinearMediaFile Media, VASTADInLineCreativeLinear Linear, params VASTADWrapper[] Wrappers)
        {
            ad = Ad;
            media = Media;
            linear = Linear;
            wrappers = Wrappers;
        }

        public string MediaSource
        {
            get { return media.Value; }
        }

        public string Id
        {
            get { return media != null ? media.id : null; }
        }

        private IEnumerable<IGrouping<TrackingEventEnum, string>> TrackingEvents
        {
            get
            {
                var result = Enumerable.Empty<IGrouping<TrackingEventEnum, string>>();
                if (linear.TrackingEvents != null)
                    result = linear.TrackingEvents.GroupBy(te => (TrackingEventEnum)Enum.Parse(typeof(TrackingEventEnum), te.@event.ToString(), false), te => te.Value);
                if (linear.VideoClicks.ClickTracking != null)
                    result = result.Concat(linear.VideoClicks.ClickTracking.GroupBy(te => TrackingEventEnum.click, te => te.Value));
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

        public TimeSpan? Duration
        {
            get 
            {
                if (linear.Duration < DateTime.Today)
                    return linear.Duration.Subtract(new DateTime());
                else
                    return linear.Duration.Subtract(DateTime.Today);
            }
        }

        public string MimeType
        {
            get { return media.type; }
        }

        public string ClickUrl
        {
            get { return linear.VideoClicks.ClickThrough.Value; }
        }

        public Size Dimensions
        {
            get
            {
                if (media.width.ToDouble().HasValue && media.height.ToDouble().HasValue)
                {
                    return new Size(media.width.ToDouble().Value, media.height.ToDouble().Value);
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        public Size ExpandedDimensions
        {
            get { return Size.Empty; }
        }

        public CreativeSourceType Type
        {
            get { return CreativeSourceType.Linear; }
        }

        public MediaSourceEnum MediaSourceType
        {
            get
            {
                return MediaSourceEnum.Static;
            }
        }

        public string ExtraInfo
        {
            get
            {
                return linear.AdParameters;
            }
        }

        public bool IsScalable
        {
            get
            {
                return media.scalableSpecified ? media.scalable : true;
            }
        }

        public bool MaintainAspectRatio
        {
            get
            {
                return media.maintainAspectRatioSpecified ? media.maintainAspectRatio : true;
            }
        }

        public string AltText
        {
            get { return string.Empty; }
        }

        public bool IsStreaming
        {
            get { return media.delivery == VASTADInLineCreativeLinearMediaFileDelivery.streaming; }
        }
    }

}
