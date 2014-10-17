using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents a control that displays the current bitrate for adaptive media on the client.
    /// </summary>
    /// <remarks>
    /// By default, if the value of the Player class IsMediaAdaptive property is <c>false</c>, the BitrateMonitor is not visible.
    /// </remarks>
    public partial class BitrateMonitor : Control
    {
        public BitrateMonitor()
        {
            DefaultStyleKey = typeof (BitrateMonitor);
        }

        // set visual state based on percentage of PlaybackBitrate to MaximumPlaybackBitrate
        protected virtual void UpdateVisualState()
        {
            double percentage = MaximumPlaybackBitrate != 0
                                    ? (double) PlaybackBitrate/MaximumPlaybackBitrate
                                    : 0;

            string state =
                HighDefinitionBitrate != 0 && PlaybackBitrate >= HighDefinitionBitrate
                    ? BitrateMonitorVisualStates.BitrateStates.BitratePercentageHD
                    : percentage < .25
                          ? BitrateMonitorVisualStates.BitrateStates.BitratePercentage0
                          : percentage < .5
                                ? BitrateMonitorVisualStates.BitrateStates.BitratePercentage25
                                : percentage < .75
                                      ? BitrateMonitorVisualStates.BitrateStates.BitratePercentage50
                                      : percentage < 1
                                            ? BitrateMonitorVisualStates.BitrateStates.BitratePercentage75
                                            : BitrateMonitorVisualStates.BitrateStates.BitratePercentage100;

            this.GoToVisualState(state);
        }
    }
}