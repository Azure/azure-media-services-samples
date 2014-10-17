using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.IO;
using System.ComponentModel;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Represents the base interface for any media plug-in.
    /// </summary>
    /// <remarks>
    /// This interface contains members common to all media plug-ins. Implementing this interface by itself does not define any specific playback capabilities. 
    /// </remarks>
    public interface IMediaPlugin : IPlugin
    {

        /// <summary>
        /// Gets or sets a value indicating whether the media plays immediately after the media file is opened.
        /// </summary>
        bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets the cache mode
        /// </summary>
        CacheMode CacheMode { get; set; }
        
#if !WINDOWS_PHONE
        /// <summary>
        /// Gets the current position of the clip.
        /// </summary>
        TimeSpan ClipPosition { get; }

        /// <summary>
        /// Gets a value indicating whether the media is being decoded by the GPU.
        /// </summary>
        bool IsDecodingOnGPU { get; }

        /// <summary>
        /// Occurs when the DrmSetupDecryptorCompleted event completes, regardless if the setup succeeded or failed.
        /// </summary>
        event EventHandler<MediaDrmSetupDecryptorCompletedEventArgs> MediaDrmSetupDecryptorCompleted;
#endif

        /// <summary>
        /// Gets or sets the ratio of the volume level across stereo speakers.
        /// </summary>
        /// <remarks>
        /// The value is in the range between -1 and 1. The default value of 0 signifies an equal volume between left and right stereo speakers.
        /// A value of -1 represents 100 percent volume in the speakers on the left, and a value of 1 represents 100 percent volume in the speakers on the right. 
        /// </remarks>
        double Balance { get; set; }

        /// <summary>
        /// Gets the percentage of the current buffering that is completed.
        /// </summary>
        double BufferingProgress { get; }

        /// <summary>
        /// Gets or sets the amount of time for the current buffering action.
        /// </summary>
        TimeSpan BufferingTime { get; set; }

        /// <summary>
        /// Gets a value indicating whether the media can pause.
        /// </summary>
        bool CanPause { get; }

        /// <summary>
        /// Gets a value indicating whether the media can seek to a new position.
        /// </summary>
        bool CanSeek { get; }

        /// <summary>
        /// Gets the current state of media playback.
        /// </summary>
        MediaPluginState CurrentState { get; }

        /// <summary>
        /// Gets the percentage of the current buffering that is completed
        /// </summary>
        double DownloadProgress { get; }

        /// <summary>
        /// Gets the download progress offset
        /// </summary>
        double DownloadProgressOffset { get; }

        /// <summary>
        /// Gets the total time for the current media.
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets the time position for the end of the media.
        /// </summary>
        TimeSpan EndPosition { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the media plug-in is muted.
        /// </summary>
        bool IsMuted { get; set; }

        /// <summary>
        /// Gets or sets the LicenseAcquirer associated with the IMediaPlugin. 
        /// The LicenseAcquirer handles acquiring licenses for DRM encrypted content.
        /// </summary>
        LicenseAcquirer LicenseAcquirer { get; set; }

        /// <summary>
        /// Gets or sets the playback rate for media that support different playback rates.
        /// </summary>
        double PlaybackRate { get; set; }

        /// <summary>
        /// Gets or sets the current playback position.
        /// </summary>
        TimeSpan Position { get; set; }

        /// <summary>
        /// Gets the start position for the media.
        /// </summary>
        TimeSpan StartPosition { get; }

        /// <summary>
        /// Gets the delivery methods supported by this plugin.
        /// </summary>
        DeliveryMethods SupportedDeliveryMethods { get; }

        /// <summary>
        /// Gets a collection of the supported playback rates for the media.
        /// </summary>
        IEnumerable<double> SupportedPlaybackRates { get; }

        /// <summary>
        /// Gets or sets the volume of the media that is playing.
        /// </summary>
        double Volume { get; set; }

        /// <summary>
        /// Gets or sets the location of the media file.
        /// </summary>
        Uri Source { get; set; }

        Stream StreamSource { get; set; }

        //Video Specific Properties

        /// <summary>
        /// Gets the size at which the media was encoded.
        /// </summary>
        Size NaturalVideoSize { get; }

        /// <summary>
        /// Gets or sets the stretch mode for the media.
        /// </summary>
        Stretch Stretch { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether to 
        /// enable GPU acceleration
        /// </summary>
        bool EnableGPUAcceleration { get; set; }

        /// <summary>
        /// Gets the control that is playing the media for this plug-in.
        /// </summary>
        FrameworkElement VisualElement { get; }

        /// <summary>
        /// Gets the dropped frames per second.
        /// </summary>
        double DroppedFramesPerSecond { get; }

        /// <summary>
        /// Gets the rendered frames per second.
        /// </summary>
        double RenderedFramesPerSecond { get; }

        /// <summary>
        /// Gets whether this plugin supports ad scheduling.
        /// </summary>
        bool SupportsAdScheduling { get; }

        /// <summary>
        /// Occurs when the percent of the media being buffered changes.
        /// </summary>
        event Action<IMediaPlugin, double> BufferingProgressChanged;

        /// <summary>
        /// Occurs when the percent of the media downloaded changes.
        /// </summary>
        event Action<IMediaPlugin, double> DownloadProgressChanged;

        /// <summary>
        /// Occurs when a marker defined for the media file has been reached.
        /// </summary>
        event Action<IMediaPlugin, MediaMarker> MarkerReached;

        /// <summary>
        /// Occurs when the media reaches the end.
        /// </summary>
        event Action<IMediaPlugin> MediaEnded;

        /// <summary>
        /// Occurs when the media does not open successfully.
        /// </summary>
        event Action<IMediaPlugin, Exception> MediaFailed;

        /// <summary>
        /// Occurs when the media successfully opens.
        /// </summary>
        event Action<IMediaPlugin> MediaOpened;

        /// <summary>
        /// Occurs when the PlaybackRate changes.
        /// </summary>
        event Action<IMediaPlugin> PlaybackRateChanged;

        /// <summary>
        /// Occurs when the state of playback for the media changes.
        /// </summary>
        event Action<IMediaPlugin, MediaPluginState> CurrentStateChanged;

        /// <summary>
        /// Occurs when the action of seeking a new position on the timeline is completed.
        /// </summary>
        event Action<IMediaPlugin> SeekCompleted;

        /// <summary>
        /// Occurs when the user clicks on an ad.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IAdContext> AdClickThrough;

        /// <summary>
        /// Occurs when there is an error playing an ad.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IAdContext> AdError;

        /// <summary>
        /// Occurs when the progress of the currently playing ad has been updated.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IAdContext, AdProgress> AdProgressUpdated;

        /// <summary>
        /// Occurs when the state of the currently playing ad has changed.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IAdContext> AdStateChanged;

        /// <summary>
        /// Starts playing the current media file from its current position.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the currently playing media.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops playing the current media.
        /// </summary>
        void Stop();

        /// <summary>
        /// Requests that this plugin generate a LogEntry via the LogReady event
        /// </summary>
        void RequestLog();

        /// <summary>
        /// Schedules an ad to be played by this plugin.
        /// </summary>
        /// <param name="adSource">The source of the ad content.</param>
        /// <param name="deliveryMethod">The delivery method of the ad content.</param>
        /// <param name="duration">The duration of the ad content that should be played.  If ommitted the plugin will play the full duration of the ad content.</param>
        /// <param name="startTime">The position within the media where this ad should be played.  If ommited ad will begin playing immediately.</param>
        /// <param name="startOffset">The position within the clip that the clip should start from. If ommited ad will begin playing from its beginning.</param>
        /// <param name="clickThrough">The URL where the user should be directed when they click the ad.</param>
        /// <param name="pauseTimeline">Indicates if the timeline of the currently playing media should be paused while the ad is playing.</param>
        /// <param name="appendToAd">Another scheduled ad that this ad should be appended to.  If ommitted this ad will be scheduled independently.</param>
        /// <param name="data">User data.</param>
        /// <param name="isLinearClip">Indicates that the clip should replace the main content for the cooresponding time slot.</param>
        /// <returns>A reference to the IAdContext that contains information about the scheduled ad.</returns>
        IAdContext ScheduleAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? duration = null,
                              TimeSpan? startTime = null, TimeSpan? startOffset = null, Uri clickThrough = null, bool pauseTimeline = true,
                              IAdContext appendToAd = null, object data = null, bool isLinearClip = false);
    }

#if !WINDOWS_PHONE
    public class MediaDrmSetupDecryptorCompletedEventArgs : AsyncCompletedEventArgs
    {
        public MediaDrmSetupDecryptorCompletedEventArgs(Guid keyId, IDataChunk dataChunk, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
        {
            KeyId = keyId;
            DataChunk = dataChunk;
        }

        public IDataChunk DataChunk { get; private set; }
        public Guid KeyId { get; private set; }
    }
#endif
}