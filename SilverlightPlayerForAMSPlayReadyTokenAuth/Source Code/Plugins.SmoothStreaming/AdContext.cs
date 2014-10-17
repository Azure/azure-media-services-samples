using System;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// Contains information about a scheduled advertisement
    /// </summary>
    public class AdContext : IAdContext
    {
        private readonly object _data;
        private readonly ClipContext _clipContext;
        private readonly ScheduledAd _scheduledAd;

        internal AdContext(ScheduledAd scheduledAd, object data)
        {
            if (scheduledAd == null) throw new ArgumentNullException("scheduledAd");
            _scheduledAd = scheduledAd;
            _data = data;
        }

        public AdContext(ClipContext clipContext)
        {
            if (clipContext == null) throw new ArgumentNullException("clipContext");
            _clipContext = clipContext;
        }

        internal ScheduledAd ScheduledAd
        {
            get { return _scheduledAd; }
        }

        internal virtual ClipContext ClipContext
        {
            get { return _clipContext ?? _scheduledAd.ClipContext; }
        }

        private ClipInformation ClipInformation
        {
            get { return ClipContext.ClipInformation; }
        }

        #region IAdContext Members
        /// <summary>
        /// The source of the ad content.
        /// </summary>
        public Uri AdSource
        {
            get { return ClipInformation.ClipUri; }
        }

        /// <summary>
        /// The URL where the user should be directed when they click the ad.
        /// </summary>
        public Uri ClickThrough
        {
            get { return ClipInformation.ClickThroughUri; }
        }

        /// <summary>
        /// Indicates if the ad content is adaptive media.
        /// </summary>
        public bool IsAdaptiveMedia
        {
            get { return ClipInformation.IsSmoothStreamingSource; }
        }

        /// <summary>
        /// The state of the ad.
        /// </summary>
        public virtual MediaPluginState CurrentAdState
        {
            get
            {
                MediaPluginState result;

                return ClipContext != null &&
#if SILVERLIGHT3
                    SystemExtensions.TryParse(ClipContext.CurrentClipState.ToString(), true, out result)
#else
 Enum.TryParse(ClipContext.CurrentClipState.ToString(), true, out result)
#endif
 ? result
                           : MediaPluginState.Closed;
            }
        }


        public object Data
        {
            get
            {
                return ClipContext != null
                           ? ClipContext.Data
                           : _data;
            }
        }

        /// <summary>
        /// Gets whether this ad has quartile events.
        /// </summary>
        public bool? HasQuartileEvents
        {
            get
            {
                return ClipContext != null
                           ? ClipContext.HasQuartileEvents
                           : (bool?)null;
            }
        }

        /// <summary>
        /// Gets the natural duration of the ad content.
        /// </summary>
        public TimeSpan? NaturalDuration
        {
            get
            {
                return ClipContext != null && ClipContext.NaturalDuration.HasTimeSpan
                           ? ClipContext.NaturalDuration.TimeSpan
                           : (TimeSpan?)null;
            }
        }

        /// <summary>
        /// Gets the playback duration of the ad content.
        /// </summary>
        public TimeSpan? PlaybackDuration
        {
            get
            {
                return ClipContext != null && ClipContext.PlaybackDuration.HasTimeSpan
                           ? ClipContext.PlaybackDuration.TimeSpan
                           : (TimeSpan?)null;
            }
        }


        #endregion
    }
}