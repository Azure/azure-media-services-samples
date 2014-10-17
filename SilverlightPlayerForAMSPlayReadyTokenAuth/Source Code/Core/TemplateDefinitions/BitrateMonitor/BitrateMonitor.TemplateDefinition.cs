using System.ComponentModel;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentage0,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentage25,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentage50,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentage75,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentage100,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    [TemplateVisualState(Name = BitrateMonitorVisualStates.BitrateStates.BitratePercentageHD,
        GroupName = BitrateMonitorVisualStates.GroupNames.BitrateStates)]
    public partial class BitrateMonitor
    {
        #region PlaybackBitrate
        /// <summary>
        /// PlaybackBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlaybackBitrateProperty =
            DependencyProperty.Register("PlaybackBitrate", typeof (long), typeof (BitrateMonitor),
                                        new PropertyMetadata(OnPropertyChanged));

        /// <summary>
        /// Gets or sets the current playback bitrate.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long PlaybackBitrate
        {
            get { return (long) GetValue(PlaybackBitrateProperty); }
            set { SetValue(PlaybackBitrateProperty, value); }
        }

        #endregion

        #region MaximumPlaybackBitrate
        /// <summary>
        /// MaximumPlayback DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MaximumPlaybackBitrateProperty =
            DependencyProperty.Register("MaximumPlaybackBitrate", typeof (long), typeof (BitrateMonitor),
                                        new PropertyMetadata(OnPropertyChanged));

        /// <summary>
        /// Gets or sets the highest bitrate for playback.
        /// </summary>
        [Category("Bitrate Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public long MaximumPlaybackBitrate
        {
            get { return (long) GetValue(MaximumPlaybackBitrateProperty); }
            set { SetValue(MaximumPlaybackBitrateProperty, value); }
        }

        #endregion

        #region HighDefinitionBitrate
        /// <summary>
        /// MaximumPlayback DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty HighDefinitionBitrateProperty =
            DependencyProperty.Register("HighDefinitionBitrate", typeof (long), typeof (BitrateMonitor),
                                        new PropertyMetadata(OnPropertyChanged));

        /// <summary>
        /// Gets or sets the bitrate value for high definition video.
        /// </summary>
        [Category("Bitrate Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public long HighDefinitionBitrate
        {
            get { return (long) GetValue(HighDefinitionBitrateProperty); }
            set { SetValue(HighDefinitionBitrateProperty, value); }
        }

        #endregion

        // PlaybackBitrate and MaximumPlaybackBitrate dependency property callback
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as BitrateMonitor;
            source.IfNotNull(i => i.UpdateVisualState());
        }
    }
}