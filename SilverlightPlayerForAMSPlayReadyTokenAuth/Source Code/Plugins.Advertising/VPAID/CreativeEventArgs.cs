using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// EventArgs used to send information about when an individual creative's progress changes.
    /// </summary>
    public class CreativeProgressEventArgs : CreativeEventArgs
    {
        readonly TimeSpan timeRemaining;
        internal CreativeProgressEventArgs(ICreativeSource AdSource, TimeSpan TimeRemaining)
            : base(AdSource)
        {
            timeRemaining = TimeRemaining;
        }

        /// <summary>
        /// Returns the time remaining for the ad.
        /// </summary>
        public TimeSpan TimeRemaining { get { return timeRemaining; } }
    }

    /// <summary>
    /// EventArgs used to send information about when an individual creative fails.
    /// </summary>
    public class CreativeEventArgs : EventArgs
    {
        readonly ICreativeSource adSource;

        internal CreativeEventArgs(ICreativeSource AdSource)
        {
            adSource = AdSource;
        }

        /// <summary>
        /// Extra information about the source of the creative.
        /// </summary>
        public ICreativeSource AdSource { get { return adSource; } }
    }
}
