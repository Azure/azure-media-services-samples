using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    // Provides an implementation of the IPlayer interface. This is to be compatible with Akamai's OVP in order to share plugins that require references to the player.
    public partial class SMFPlayer : IPlayer
    {
        IAdContext IPlayer.PlayLinearAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? startTime, TimeSpan? startOffset, Uri clickThrough, TimeSpan? duration, bool pauseTimeline, IAdContext appendToAd, object data)
        {
            return this.ScheduleAdvertisement(adSource, deliveryMethod, startTime, startOffset, clickThrough, duration, pauseTimeline, appendToAd, data);
        }

        ScheduledAd IPlayer.ScheduleAdTrigger(IAdSequencingTrigger adTrigger, TimeSpan? startTime)
        {
            return this.ScheduleAdTrigger(adTrigger, startTime);
        }

        void IPlayer.RemoveScheduledAd(ScheduledAd ScheduledAd)
        {
            this.RemoveScheduledAd(ScheduledAd);
        }

        IDictionary<string, object> IPlayer.GlobalConfigMetadata
        {
            get { return this.GlobalConfigMetadata.ToDictionary(mi => mi.Key, mi => mi.Value); }
        }

        event EventHandler IPlayer.ContentChanged
        {
            add
            {
                PlaylistItemChanged += new EventHandler<CustomEventArgs<PlaylistItem>>(value);
            }
            remove
            {
                if (PlaylistItemChanged != null)
                {
                    var v = PlaylistItemChanged.GetInvocationList().Cast<EventHandler<CustomEventArgs<PlaylistItem>>>().FirstOrDefault(e => e.Target.Equals(value));
                    if (v != null) PlaylistItemChanged -= v;
                }
            }
        }

        IMediaPlugin IPlayer.ActiveMediaPlugin
        {
            get { return this.ActiveMediaPlugin; }
        }

        bool IAdHost.AdMode
        {
            get { return IsAdvertising; }
            set { IsAdvertising = value; }
        }

        EventHandler sizeChanged;
        event EventHandler IPlayer.SizeChanged
        {
            add
            {
                sizeChanged += value;
            }
            remove
            {
                sizeChanged -= value;
            }
        }

        partial void OnSizeChanged() 
        {
            if (sizeChanged != null) sizeChanged(this, EventArgs.Empty);
        }

        event EventHandler IPlayer.PlayEnded
        {
            add
            {
                PlaylistReachedEnd += new EventHandler(value);
            }
            remove
            {
                if (PlaylistReachedEnd != null)
                {
                    var v = PlaylistReachedEnd.GetInvocationList().Cast<EventHandler>().FirstOrDefault(e => e.Target.Equals(value));
                    if (v != null) PlaylistReachedEnd -= v;
                }
            }
        }

        event EventHandler IPlayer.MediaFailed
        {
            add
            {
                MediaFailed += new EventHandler<CustomEventArgs<Exception>>(value);
            }
            remove
            {
                if (MediaFailed != null)
                {
                    var v = MediaFailed.GetInvocationList().Cast<EventHandler<CustomEventArgs<Exception>>>().FirstOrDefault(e => e.Target.Equals(value));
                    if (v != null) MediaFailed -= v;
                }
            }
        }

        event EventHandler IAdHost.StateChanged
        {
            add
            {
                PlayStateChanged += new EventHandler<CustomEventArgs<MediaPluginState>>(value);
            }
            remove
            {
                if (PlayStateChanged != null)
                {
                    var v = PlayStateChanged.GetInvocationList().Cast<EventHandler<CustomEventArgs<MediaPluginState>>>().FirstOrDefault(e => e.Target.Equals(value));
                    if (v != null) PlayStateChanged -= v;
                }
            }
        }

        event EventHandler IAdHost.VolumeChanged
        {
            add
            {
                VolumeLevelChanged += new EventHandler<CustomEventArgs<double>>(value);
            }
            remove
            {
                if (VolumeLevelChanged != null)
                {
                    var v = VolumeLevelChanged.GetInvocationList().Cast<EventHandler<CustomEventArgs<double>>>().FirstOrDefault(e => e.Target.Equals(value));
                    if (v != null) VolumeLevelChanged -= v;
                }
            }
        }

        double IAdHost.Volume
        {
            get { return VolumeLevel; }
        }

        Size IPlayer.VideoResolution
        {
            get
            {
                if (ActiveMediaPlugin != null)
                {
                    return ActiveMediaPlugin.NaturalVideoSize;
                }
                else
                {
                    return Size.Empty;
                }
            }
        }

        Size IPlayer.MediaElementSize
        {
            get
            {
                return this.GetEffectiveSize();
            }
        }

        TimeSpan IPlayer.Position
        {
            get
            {
                return this.PlaybackPosition;
            }
        }

        TimeSpan IPlayer.Duration
        {
            get
            {
                return CurrentPlaylistItem.Duration.GetValueOrDefault(TimeSpan.Zero);
            }
        }

        string IPlayer.ContentTitle
        {
            get { return CurrentPlaylistItem.Title; }
        }

        Uri IPlayer.ContentUrl
        {
            get { return CurrentPlaylistItem.MediaSource; }
        }

        IDictionary<string, object> IPlayer.ContentMetadata
        {
            get { return CurrentPlaylistItem.CustomMetadata.ToDictionary(mi => mi.Key, mi => mi.Value); }
        }

        TimeSpan? IPlayer.ContentStartPosition 
        {
            get { return CurrentPlaylistItem.StartPosition; }
        }

        bool IPlayer.CaptionsActive
        {
            get { return CaptionsVisibility == FeatureVisibility.Visible; }
        }

        bool IPlayer.HasCaptions
        {
            get { return AvailableCaptionStreams.Any(); }
        }

        bool IPlayer.HasVideo
        {
            get { return VideoHeight > 0; }
        }

        bool IPlayer.HasAudio
        {
            get { return AvailableAudioStreams.Any(); }
        }

        IPlugin[] IAdHost.Plugins
        {
            get { return PluginsManager.GenericPlugins.Select(p => p.Value as IPlugin).ToArray(); }
        }
    }
}
