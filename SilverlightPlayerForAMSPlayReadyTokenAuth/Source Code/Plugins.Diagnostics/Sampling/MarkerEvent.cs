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

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Base class for start/stop markers. Used in aggregation.
    /// </summary>
    internal abstract class MarkerEvent : SmoothStreamingEvent { }

    /// <summary>
    /// Indicates the video has resumed
    /// </summary>
    internal class MarkerEventStart : MarkerEvent { }

    /// <summary>
    /// Indicates the video has stopped/paused
    /// </summary>
    internal class MarkerEventStop : MarkerEvent { }
}
