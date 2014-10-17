using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Checks the status of timeline markers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class tracks which timeline markers have been reached, which timeline markers have been left 
    /// (when media playing has passed the end position of a timeline marker), 
    /// and which timeline markers were skipped (due to the user seeking to a new position in the timeline, for example).
    /// This class raises the MarkerReached, MarkerLeft, and MarkerSkipped events respectively when these conditions occur.
    /// </para>
    /// </remarks>
    internal class TimelineMarkerManager : SkippableMarkerManager<TimelineMediaMarker>
    {
        
    }
}