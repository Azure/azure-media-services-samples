using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// An interface that provides everything needed to play ads in the player. Internally, this is used by VPAID advertising plugins.
    /// </summary>
    public interface IAdHost
    {
        /// <summary>
        /// Indicates that an ad is playing
        /// </summary>
        bool AdMode { get; set; }
        /// <summary>
        /// Causes the player to play the video
        /// </summary>
        void Play();
        /// <summary>
        /// Causes the player to pause the video
        /// </summary>
        void Pause();
        /// <summary>
        /// The last known playback bitrate being used by the player
        /// </summary>
        long PlaybackBitrate { get; }
        /// <summary>
        /// The current volume level of the player
        /// </summary>
        double Volume { get; }
        /// <summary>
        /// The state of the player
        /// </summary>
        MediaPluginState PlayState { get; }
        /// <summary>
        /// Containers available to the player for hosting ads. This is useful for companion and nonlinear ads.
        /// </summary>
        IList<FrameworkElement> Containers { get; }
        /// <summary>
        /// The main video area panel. This is useful for hosting linear ads.
        /// </summary>
        Panel VideoArea { get; }
        /// <summary>
        /// A list of the currently available player plugins.
        /// </summary>
        IPlugin[] Plugins { get; }
        /// <summary>
        /// Blocks the player from starting a video or pauses the video if active. This is useful for pre-roll ads.
        /// Warning: You MUST call ReleasePlayBlock eventually to get the player to play again.
        /// </summary>
        /// <param name="blocker">The object applying the block. If the same object is added twice, it will be ignored.</param>
        void AddPlayBlock(object blocker);
        /// <summary>
        /// Releases a play block created with AddPlayBlock
        /// </summary>
        /// <param name="blocker">The object applying the block. If the object does not have a block, it will be ignored.</param>
        void ReleasePlayBlock(object blocker);
#if !WINDOWS_PHONE && !SILVERLIGHT3
        /// <summary>
        /// Indicates whether we are in fullscreen mode or not
        /// </summary>
        bool IsFullScreen { get; }
        /// <summary>
        /// Raised when fullscreen mode changes
        /// </summary>
        event EventHandler FullScreenChanged;
#endif
        /// <summary>
        /// Raised when the volume changes
        /// </summary>
        event EventHandler VolumeChanged;
        /// <summary>
        /// Raised when the player state changes
        /// </summary>
        event EventHandler StateChanged;
    }

    public interface IPlayer : IAdHost
    {
        /// <summary>
        /// Raised when the size of the player has changed
        /// </summary>
        event EventHandler SizeChanged;
        /// <summary>
        /// Returns the current video resolution
        /// </summary>
        Size VideoResolution { get; }
        /// <summary>
        /// Returns the size of the current media element responsible for playing the video
        /// </summary>
        Size MediaElementSize { get; }
        /// <summary>
        /// Returns the current position of the player
        /// </summary>
        TimeSpan Position { get; }
        /// <summary>
        /// Returns the current duration of the video
        /// </summary>
        TimeSpan Duration { get; }
        /// <summary>
        /// Raised when the user has finished seeking
        /// </summary>
        event EventHandler SeekCompleted;
        /// <summary>
        /// Raised when the media is opened. Note: This occurs before the first frame of the video is displayed to the user.
        /// </summary>
        event EventHandler MediaOpened;
        /// <summary>
        /// Raised when the media fails.
        /// </summary>
        event EventHandler MediaFailed;
        /// <summary>
        /// Raised when the media finishes playing.
        /// </summary>
        event EventHandler MediaEnded;
        /// <summary>
        /// Raised when the last item in the playlist has finished playing.
        /// </summary>
        event EventHandler PlayEnded;
        /// <summary>
        /// Raised when the timeline changes. This is a good alternative to creating your own timer.
        /// </summary>
        event EventHandler TimelineChanged;

        /// <summary>
        /// Indicates that the current media is adaptive vs. progressive
        /// </summary>
        bool IsMediaAdaptive { get; }
        /// <summary>
        /// Returns the title of the current video.
        /// </summary>
        string ContentTitle { get; }
        /// <summary>
        /// Returns the Url of the current video.
        /// </summary>
        Uri ContentUrl { get; }
        /// <summary>
        /// Returns a dictionary of metadata associated with the current video
        /// </summary>
        IDictionary<string, object> ContentMetadata { get; }
        /// <summary>
        /// Indicates whether or not closed captions are active or not.
        /// </summary>
        bool CaptionsActive { get; }
        /// <summary>
        /// Indicates whether or not the current video contains closed captions.
        /// </summary>
        bool HasCaptions { get; }
        /// <summary>
        /// Indicates that the current content contains video.
        /// </summary>
        bool HasVideo { get; }
        /// <summary>
        /// Indicates that the current content contains audio.
        /// </summary>
        bool HasAudio { get; }
        /// <summary>
        /// Returns the currently active media plugin used to play the current video.
        /// </summary>
        IMediaPlugin ActiveMediaPlugin { get; }
        /// <summary>
        /// Returns the startup position of the current content. This setting is what allows the application to automatically seek into a video on startup (useful for restoring a video position).
        /// </summary>
        TimeSpan? ContentStartPosition { get; }
        /// <summary>
        /// Returns custom metadata associated with the player instance.
        /// </summary>
        IDictionary<string, object> GlobalConfigMetadata { get; }
        /// <summary>
        /// Indicates that the current video has changed.
        /// </summary>
        event EventHandler ContentChanged;
        /// <summary>
        /// Schedules an ad that is to be handled by an AdPayloadHandlerPlugin.
        /// A valid AdPayloadHandlerPlugin must be part of your application or this will not be handled.
        /// </summary>
        /// <param name="adTrigger">An object containing information about the ad source and target</param>
        /// <param name="startTime">The position within the media where this ad should be played. If ommited ad will begin playing immediately.</param>
        /// <returns>An object that contains information about the scheduled ad.</returns>
        ScheduledAd ScheduleAdTrigger(IAdSequencingTrigger adTrigger, TimeSpan? startTime = null);
        /// <summary>
        /// Removes a scheduled ad that was the result of a call to ScheduleAdTrigger
        /// </summary>
        /// <param name="ScheduledAd">The scheduled ad created from the AdTrigger that was scheduled.</param>
        void RemoveScheduledAd(ScheduledAd ScheduledAd);
        /// <summary>
        /// Schedules an ad to be played by this plugin.
        /// </summary>
        /// <param name="adSource">The source of the ad content.</param>
        /// <param name="deliveryMethod">The delivery method of the ad content.</param>
        /// <param name="startTime">The position within the media where this ad should be played.  If ommited ad will begin playing immediately.</param>
        /// <param name="startOffset">The offset/startup position of the ad.</param>
        /// <param name="clickThrough">The URL where the user should be directed when they click the ad.</param>
        /// <param name="duration">The duration of the ad content that should be played.  If ommitted the plugin will play the full duration of the ad content.</param>
        /// <param name="pauseTimeline">Indicates if the timeline of the currently playing media should be paused while the ad is playing.</param>
        /// <param name="appendToAd">Another scheduled ad that this ad should be appended to.  If ommitted this ad will be scheduled independently.</param>
        /// <param name="data">User data.</param>
        /// <returns>A reference to the IAdContext that contains information about the scheduled ad.</returns>
        IAdContext PlayLinearAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? startTime, TimeSpan? startOffset, Uri clickThrough, TimeSpan? duration, bool pauseTimeline, IAdContext appendToAd, object data);

        /// <summary>
        /// Occurs when data is received from the media plugin.
        /// </summary>
        event EventHandler<DataReceivedInfo> DataReceived;
    }
}
