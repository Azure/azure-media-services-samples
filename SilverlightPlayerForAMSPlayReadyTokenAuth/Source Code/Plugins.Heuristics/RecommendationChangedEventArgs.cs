using System;
using System.Collections.Generic;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{

    /// <summary>
    /// Event arguments for a RecommendationChanged event.
    /// </summary>
    public class RecommendationChangedEventArgs : EventArgs
    {
        private readonly bool _recommendPermanentlyDisableAllSecondary;
        private IList<SmoothStreamingMediaElement> _disable;
        private IList<SmoothStreamingMediaElement> _enable;

        internal RecommendationChangedEventArgs(IList<SmoothStreamingMediaElement> enable,
                                                IList<SmoothStreamingMediaElement> disable,
                                                bool recommendPermanentlyDisableAllSecondary)
        {
            // should only raise the event if one is null
            if (enable == null && disable == null)
            {
                throw new ArgumentNullException();
            }

            _enable = enable;
            _disable = disable;
            _recommendPermanentlyDisableAllSecondary = recommendPermanentlyDisableAllSecondary;
        }

        public bool RecommendPermanentlyDisableAllSecondary
        {
            get { return _recommendPermanentlyDisableAllSecondary; }
        }

        /// <summary>
        /// RecommendDisable
        /// </summary>
        public IList<SmoothStreamingMediaElement> RecommendDisable
        {
            get
            {
                if (_disable == null)
                {
                    _disable = new List<SmoothStreamingMediaElement>(0);
                }
                return _disable;
            }
        }

        /// <summary>
        /// RecommendEnable
        /// </summary>
        public IList<SmoothStreamingMediaElement> RecommendEnable
        {
            get
            {
                if (_enable == null)
                {
                    _enable = new List<SmoothStreamingMediaElement>(0);
                }
                return _enable;
            }
        }
    }
}