using Microsoft.SilverlightMediaFramework.Core;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    public class SMFPlayerStartupBitrate : SMFPlayer
    {
        public SMFPlayerStartupBitrate()
        {
            PlayStateChanged += Player_PlayStateChanged;
            PlaybackBitrateChanged += Player_PlaybackBitrateChanged;
        }

        /// <summary>
        /// LastBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty LastBitrateProperty =
            DependencyProperty.Register("LastBitrate", typeof(long?), typeof(SMFPlayer), new PropertyMetadata(null));

        /// <summary>
        /// The bitrate that was used while playing the previous video. This value should be persisted by the application when the player is unloaded and restored when the player is loaded.
        /// </summary>
        public long? LastBitrate
        {
            get { return (long?)GetValue(LastBitrateProperty); }
            set { SetValue(LastBitrateProperty, value); }
        }

        /// <summary>
        /// StartupBitratePercentage DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty StartupBitratePercentageProperty =
            DependencyProperty.Register("StartupBitratePercentage", typeof(double), typeof(SMFPlayer), new PropertyMetadata(.5));

        /// <summary>
        /// The percentage of the LastBitrate that should be used to drive the startup bitrate.
        /// </summary>
        public double StartupBitratePercentage
        {
            get { return (double)GetValue(StartupBitratePercentageProperty); }
            set { SetValue(StartupBitratePercentageProperty, value); }
        }

        private bool isStartupHeuristicsActive;

        private void Player_PlayStateChanged(object sender, CustomEventArgs<MediaPluginState> e)
        {
            if (isStartupHeuristicsActive && e.Value == MediaPluginState.Playing)
            {
                isStartupHeuristicsActive = false;
                if (ActiveAdaptiveMediaPlugin != null)
                {
                    var videoStream = ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams.Where(i => i.Type == StreamType.Video).FirstOrDefault();
                    if (videoStream != null)
                    {
                        videoStream.SetSelectedTracks(videoStream.AvailableTracks);
                    }
                }
            }
        }

        private void Player_PlaybackBitrateChanged(object sender, CustomEventArgs<long> e)
        {
            if (e.Value > 0)
            {
                LastBitrate = e.Value;
            }
        }

        protected override void OnManifestReady()
        {
            base.OnManifestReady();

            isStartupHeuristicsActive = false;
            if (ActiveAdaptiveMediaPlugin != null)
            {
                var videoStream = ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams.Where(i => i.Type == StreamType.Video).FirstOrDefault();

                if (videoStream != null)
                {
                    if (LastBitrate.HasValue)
                    {
                        var track = videoStream.AvailableTracks.OrderBy(o => Math.Abs(o.Bitrate - LastBitrate.Value / 2)).FirstOrDefault();
                        if (track != null)
                        {
                            videoStream.SetSelectedTracks(new[] { track });
                            isStartupHeuristicsActive = true;
                        }
                    }
                }
            }
        }
    }
}
