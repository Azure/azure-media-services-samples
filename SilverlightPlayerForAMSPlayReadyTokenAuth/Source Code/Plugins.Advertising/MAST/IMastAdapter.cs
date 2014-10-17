using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Defines all the properties and events that MAST conditions can be based on
    /// </summary>
    public interface IMastAdapter
    {
        #region Events

        /// <summary>
        /// fires when the position changes. Used to drive the MAST trigger evaluation
        /// </summary>
        event EventHandler TimelineChanged;

        /// <summary>
        /// Defined as anytime the play command is issued, even after a pause
        /// </summary>
        event EventHandler OnPlay;

        /// <summary>
        /// The stop command is given
        /// </summary>
        event EventHandler OnStop;

        /// <summary>
        /// The pause command is given
        /// </summary>
        event EventHandler OnPause;

        /// <summary>
        /// The player was muted, or volume brought to 0
        /// </summary>
        event EventHandler OnMute;

        /// <summary>
        /// Volume was changed
        /// </summary>
        event EventHandler OnVolumeChange;

        /// <summary>
        /// The player has stopped naturally, with no new content
        /// </summary>
        event EventHandler OnEnd;

        /// <summary>
        /// The player was manually seeked
        /// </summary>
        event EventHandler OnSeek;

        /// <summary>
        /// A new item is being started. Occurs immediately before OnItemStart and should be used for internal purposes only
        /// </summary>
        event EventHandler OnItemStarting;

        /// <summary>
        /// A new item is being started
        /// </summary>
        event EventHandler OnItemStart;

        /// <summary>
        /// The current item is coming to the end
        /// </summary>
        event EventHandler OnItemEnd;

        /// <summary>
        /// Fullscreen has been toggled
        /// </summary>
        event EventHandler OnFullScreenChange;

        /// <summary>
        /// Player size has changed
        /// </summary>
        event EventHandler OnPlayerSizeChanged;

        /// <summary>
        /// An error has occurred, typically of enough severity to warrant display to the user
        /// </summary>
        event EventHandler OnError;

        /// <summary>
        /// The mouse has moved 
        /// </summary>
        event EventHandler OnMouseOver;

        #endregion

        #region Properties

        /// <summary>
        /// The duration of the current content
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// The position of the current content
        /// </summary>
        TimeSpan Position { get; }

        /// <summary>
        /// The amount of time that this item has rendered, regardless of seeks
        /// </summary>
        TimeSpan WatchedTime { get; }

        /// <summary>
        /// The total amount of content that has been rendered in this session
        /// </summary>
        TimeSpan TotalWatchedTime { get; }

        /// <summary>
        /// The current system time
        /// </summary>
        DateTime SystemTime { get; }

#if !WINDOWS_PHONE && !FULLSCREEN
        /// <summary>
        /// True if the player is fullscreen
        /// </summary>
        bool FullScreen { get; }
#endif
        
        /// <summary>
        /// True if the player is playing content
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// True if the player is paused
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// True if the player is stopped, or not yet started
        /// </summary>
        bool IsStopped { get; }

        /// <summary>
        /// True if captions are active and being shown
        /// </summary>
        bool CaptionsActive { get; }

        /// <summary>
        /// True if the current content has a video stream
        /// </summary>
        bool HasVideo { get; }

        /// <summary>
        /// True if the current content has an audio stream
        /// </summary>
        bool HasAudio { get; }

        /// <summary>
        /// True if the current content has captions available
        /// </summary>
        bool HasCaptions { get; }

        /// <summary>
        /// The count of items that have been displayed in full or part
        /// </summary>
        int ItemsPlayed { get; }

        /// <summary>
        /// The physical width of the player application
        /// </summary>
        int PlayerWidth { get; }

        /// <summary>
        /// The physical height of the player application
        /// </summary>
        int PlayerHeight { get; }

        /// <summary>
        /// The native width of the current content
        /// </summary>
        int ContentWidth { get; }

        /// <summary>
        /// The native height of the current content
        /// </summary>
        int ContentHeight { get; }

        /// <summary>
        /// The bitrate-in-use of the current content
        /// </summary>
        long ContentBitrate { get; }

        /// <summary>
        /// The title of the current content
        /// </summary>
        string ContentTitle { get; }

        /// <summary>
        /// The URL that the current content was received from 
        /// </summary>
        string ContentUrl { get; }

        /// <summary>
        /// Indicates that we are ready to start receiving events. If there were any events that happened before this moment, repeat them.
        /// </summary>
        void Ready();

        #endregion
    }
}
