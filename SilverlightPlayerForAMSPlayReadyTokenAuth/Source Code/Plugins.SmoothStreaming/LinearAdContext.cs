using System;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    public class LinearAdContext : AdContext
    {
        SmoothStreamingMediaElement _ssme;

        internal LinearAdContext(ScheduledAd scheduledAd, SmoothStreamingMediaElement ssme, object data)
            : base(scheduledAd, data)
        {
            if (ssme == null) throw new ArgumentNullException("ssme");
            _ssme = ssme;
        }

        public LinearAdContext(ClipContext clipContext, SmoothStreamingMediaElement ssme)
            : base(clipContext)
        {
            if (ssme == null) throw new ArgumentNullException("ssme");
            _ssme = ssme;
        }

        /// <summary>
        /// The state of the ad.
        /// </summary>
        public override MediaPluginState CurrentAdState
        {
            get
            {
#if SILVERLIGHT3
                return ClipContext != null && SystemExtensions.TryParse(_ssme.CurrentState.ToString(), true, out result)? result: MediaPluginState.Closed;
#else
                MediaPluginState result;
                return Enum.TryParse(_ssme.CurrentState.ToString(), true, out result) ? result : MediaPluginState.Closed;
#endif
            }
        }

        internal override ClipContext ClipContext
        {
            get
            {
                return _ssme.CurrentLinearClipContext;
            }
        }
    }
}
