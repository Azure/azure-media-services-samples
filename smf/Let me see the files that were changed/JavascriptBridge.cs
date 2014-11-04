using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    [ScriptableType]
    public class JavascriptBridge : IDisposable
    {
        private static readonly Dictionary<string, JavascriptBridge> Instances =
            new Dictionary<string, JavascriptBridge>();

        private readonly SMFPlayer _player;
        private readonly string _scriptableName;

        protected JavascriptBridge(SMFPlayer player, string scriptableName)
        {
            _player = player;
            _scriptableName = scriptableName;
            HtmlPage.RegisterScriptableObject(scriptableName, this);
            _player.TimelineMarkerReached += Player_MarkerReached;
            _player.AddExternalPluginsCompleted += Player_AddExternalPluginsCompleted;
            _player.AddExternalPluginsFailed += Player_AddExternalPluginsFailed;
			_player.PlaybackPositionChanged += Player_PlaybackPositionChanged;
            _player.PlayStateChanged += Player_PlayStateChanged;
            _player.PlaySpeedStateChanged += Player_PlaySpeedStateChanged;
            _player.TimelineMarkerLeft += Player_MarkerLeft;
            _player.TimelineMarkerSkipped += Player_MarkerSkipped;
            _player.FullScreenChanged += Player_FullScreenChanged;
            _player.PlaylistChanged += Player_PlaylistChanged;
            _player.PlaylistItemChanged += Player_PlaylistItemChanged;
            _player.MediaFailed += Player_MediaFailed;
            _player.MediaEnded += Player_MediaEnded;
            _player.MediaOpened += Player_MediaOpened;
			_player.VolumeLevelChanged += Player_VolumeLevelChanged;
			_player.SeekCompleted += Player_SeekCompleted;
			Application.Current.Exit += Application_Exit;

            try
            {
                HtmlPage.Window.Invoke("onPlayerReady", this);
            }
            catch
            {
                // ignore this exception, it will happen if the page doesn't have a onPlayerReady function.
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ScriptableMember]
        public string Name
        {
            get { return _scriptableName; }
        }

        /// <summary>
        /// Gets the playback position as seconds.
        /// </summary>
        [ScriptableMember]
        public double PlaybackPositionSeconds
        {
            get { return _player.PlaybackPosition.TotalSeconds; }
        }

        /// <summary>
        /// Gets the playback position as text.
        /// </summary>
        [ScriptableMember]
        public string PlaybackPositionText
        {
            get { return _player.PlaybackPosition.Format(); }
        }

        /// <summary>
        /// Gets the start position as seconds.
        /// </summary>
        [ScriptableMember]
        public double StartPositionSeconds
        {
            get { return _player.StartPosition.TotalSeconds; }
        }

        /// <summary>
        /// Gets the start position as text.
        /// </summary>
        [ScriptableMember]
        public string StartPositionText
        {
            get { return _player.StartPosition.Format(); }
        }

        /// <summary>
        /// Gets the end position as seconds.
        /// </summary>
        [ScriptableMember]
        public double EndPositionSeconds
        {
            get { return _player.EndPosition.TotalSeconds; }
        }

        /// <summary>
        /// Gets the end position as text.
        /// </summary>
        [ScriptableMember]
        public string EndPositionText
        {
            get { return _player.EndPosition.Format(); }
        }

		/// <summary>
		/// Gets or sets whether the control strip should be displayed.
		/// </summary>
		[ScriptableMember]
		public bool IsControlStripVisible
		{
			get { return _player.IsControlStripVisible; }
			set { _player.IsControlStripVisible = value; }
		}

		/// <summary>
		/// Gets the current play state of the Player.
		/// </summary>
		[ScriptableMember]
		public MediaPluginState PlayState
		{
			get { return _player.PlayState; }
		}

        /// <summary>
        /// Gets or sets whether media should start muted.
        /// </summary>
        [ScriptableMember]
        public bool StartMuted
        {
            get { return _player.StartMuted; }
            set { _player.StartMuted = value; }
        }

		/// <summary>
		/// Gets or sets whether media is muted.
		/// </summary>
		[ScriptableMember]
		public bool IsMuted
		{
			get { return _player.IsMuted; }
			set { _player.IsMuted = value; }
		}

        /// <summary>
        /// Gets or sets whether playlists should be autoloaded.
        /// </summary>
        [ScriptableMember]
        public bool AutoLoad
        {
            get { return _player.AutoLoad; }
            set { _player.AutoLoad = value; }
        }

        /// <summary>
        /// Gets or sets whether playlist items should autoplay.
        /// </summary>
        [ScriptableMember]
        public bool AutoPlay
        {
            get { return _player.AutoPlay; }
            set { _player.AutoPlay = value; }
        }

        /// <summary>
        /// Gets or sets whether cache composition is turned on.
        /// </summary>
        [ScriptableMember]
        public bool EnableCachedComposition
        {
            get { return _player.EnableCachedComposition; }
            set { _player.EnableCachedComposition = value; }
        }

        /// <summary>
        /// Gets or sets whether the player should skip to the first item in a playlist after the last is done playing.
        /// </summary>
        [ScriptableMember]
        public bool ContinuousPlay
        {
            get { return _player.ContinuousPlay; }
            set { _player.ContinuousPlay = value; }
        }

        /// <summary>
        /// Gets or sets the amount of time in seconds the player should spend retrying a media.
        /// </summary>
        [ScriptableMember]
        public double RetryDurationSeconds
        {
            get { return _player.RetryDuration.TotalSeconds; }
            set { _player.RetryDuration = TimeSpan.FromSeconds(value); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _player.TimelineMarkerReached -= Player_MarkerReached;
            _player.AddExternalPluginsCompleted -= Player_AddExternalPluginsCompleted;
            _player.AddExternalPluginsFailed -= Player_AddExternalPluginsFailed;
			_player.PlaybackPositionChanged -= Player_PlaybackPositionChanged;
            _player.PlayStateChanged -= Player_PlayStateChanged;
            _player.PlaySpeedStateChanged -= Player_PlaySpeedStateChanged;
            _player.TimelineMarkerLeft -= Player_MarkerLeft;
            _player.TimelineMarkerSkipped -= Player_MarkerSkipped;
            _player.FullScreenChanged -= Player_FullScreenChanged;
            _player.PlaylistChanged -= Player_PlaylistChanged;
            _player.PlaylistItemChanged -= Player_PlaylistItemChanged;
            _player.MediaFailed -= Player_MediaFailed;
            _player.MediaEnded -= Player_MediaEnded;
            _player.MediaOpened -= Player_MediaOpened;
			_player.VolumeLevelChanged -= Player_VolumeLevelChanged;
			_player.SeekCompleted -= Player_SeekCompleted;
        }

        #endregion

        /// <summary>
        /// Occurs when full screen changes.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<EventArgs> FullScreenChanged;

        /// <summary>
        /// Occurs when a marker is reached.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<ScriptMediaMarker>> MarkerReached;

        /// <summary>
        /// Occurs when a marker is left.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<ScriptMediaMarker>> MarkerLeft;

        /// <summary>
        /// Occurs when a marker is skipped.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<ScriptMediaMarker>> MarkerSkipped;


        /// <summary>
        /// Occurs when the addition of an external plugin has completed.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<EventArgs> AddExternalPluginsCompleted;

        /// <summary>
        /// Occurs when the addition of an external plugin has failed.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<string>> AddExternalPluginsFailed;

		/// <summary>
		/// Occurs when the PlaybackPosition changes.
		/// </summary>
		[ScriptableMember]
		public event EventHandler<ScriptEventArgs<double>> PlaybackPositionChanged;

        /// <summary>
        /// Occurs when the PlayState changes.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<string>> PlayStateChanged;

        /// <summary>
        /// Occurs when the PlaySpeedState changes.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<string>> PlaySpeedStateChanged;

        /// <summary>
        /// Occurs when the Playlist changes.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<ScriptPlaylist>> PlaylistChanged;
		
        /// <summary>
        /// Occurs when the current Playlist Item changes.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<ScriptPlaylistItem>> PlaylistItemChanged;

        /// <summary>
        /// Occurs when a media fails.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<ScriptEventArgs<string>> MediaFailed;

        /// <summary>
        /// Occurs when a media ends.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<EventArgs> MediaEnded;

        /// <summary>
        /// Occurs when a media is opened.
        /// </summary>
        [ScriptableMember]
        public event EventHandler<EventArgs> MediaOpened;

		/// <summary>
		/// Occurs when the volume changes.
		/// </summary>
		[ScriptableMember]
		public event EventHandler<ScriptEventArgs<double>> VolumeLevelChanged;

		/// <summary>
		/// Occurs when the user has completed seeking.
		/// </summary>
		[ScriptableMember]
		public event EventHandler<EventArgs> SeekCompleted;

		/// <summary>
		/// Occurs when the application exits.
		/// </summary>
		[ScriptableMember]
		public event EventHandler<ScriptEventArgs<double>> ApplicationExit;

        internal static JavascriptBridge Initialize(SMFPlayer player, string scriptableName)
        {
            JavascriptBridge bridge = null;
            if (HtmlPage.IsEnabled && !Instances.ContainsKey(scriptableName))
            {
                bridge = new JavascriptBridge(player, scriptableName);
                Instances.Add(scriptableName, bridge);
            }
            return bridge;
        }

        private void Player_MediaOpened(object player, EventArgs args)
        {
            MediaOpened.IfNotNull(i => i(this, null));
        }

        private void Player_MediaEnded(object player, EventArgs args)
        {
            MediaEnded.IfNotNull(i => i(this, null));
        }

        private void Player_MediaFailed(object player, CustomEventArgs<Exception> args)
        {
            MediaFailed.IfNotNull(i => i(this, new ScriptEventArgs<string>(args.Value.ToString())));
        }

        private void Player_PlaylistItemChanged(object player, CustomEventArgs<PlaylistItem> args)
        {
            if (PlaylistItemChanged != null)
            {
                var pli = new ScriptPlaylistItem(args.Value);
                PlaylistItemChanged(this, new ScriptEventArgs<ScriptPlaylistItem>(pli));
            }
        }

        private void Player_PlaylistChanged(object player, CustomEventArgs<IList<PlaylistItem>> args)
        {
            if (PlaylistChanged != null)
            {
                var pl = new ScriptPlaylist(args.Value);
                PlaylistChanged(this, new ScriptEventArgs<ScriptPlaylist>(pl));
            }
        }

        private void Player_FullScreenChanged(object player, EventArgs args)
        {
            FullScreenChanged.IfNotNull(i => i(this, null));
        }

        private void Player_MarkerSkipped(object player, CustomEventArgs<TimelineMediaMarker> args)
        {
            MarkerSkipped.IfNotNull(i =>
                                        {
                                            var mm = new ScriptMediaMarker(args.Value);
                                            i(this, new ScriptEventArgs<ScriptMediaMarker>(mm));
                                        });
        }

        private void Player_MarkerLeft(object player, CustomEventArgs<TimelineMediaMarker> args)
        {
            MarkerLeft.IfNotNull(i =>
                                     {
                                         var mm = new ScriptMediaMarker(args.Value);
                                         i(this, new ScriptEventArgs<ScriptMediaMarker>(mm));
                                     });
        }

        private void Player_PlaySpeedStateChanged(object player, CustomEventArgs<PlaySpeedState> args)
        {
            PlaySpeedStateChanged.IfNotNull(i => i(this, new ScriptEventArgs<string>(args.Value.ToString())));
        }

		private void Player_PlaybackPositionChanged(object player, CustomEventArgs<TimeSpan> args)
		{
			PlaybackPositionChanged.IfNotNull(i => i(this, new ScriptEventArgs<double>(args.Value.TotalSeconds)));
		}

        private void Player_PlayStateChanged(object player, CustomEventArgs<MediaPluginState> args)
        {
            PlayStateChanged.IfNotNull(i => i(this, new ScriptEventArgs<string>(args.Value.ToString())));
        }

        private void Player_AddExternalPluginsFailed(object player, CustomEventArgs<Exception> args)
        {
            AddExternalPluginsFailed.IfNotNull(i => i(this, new ScriptEventArgs<string>(args.Value.ToString())));
        }

        private void Player_AddExternalPluginsCompleted(object player, EventArgs args)
        {
            AddExternalPluginsCompleted.IfNotNull(i => i(this, null));
        }

		private void Player_VolumeLevelChanged(object player, CustomEventArgs<double> args)
		{
			VolumeLevelChanged.IfNotNull(i => i(this, new ScriptEventArgs<double>(args.Value)));
		}

		private void Player_SeekCompleted(object player, EventArgs args)
		{
			SeekCompleted.IfNotNull(i => i(this, null));
		}

		private void Application_Exit(object application, EventArgs args)
		{
			ApplicationExit.IfNotNull(i => i(this, null));
		}

        [ScriptableMember]
        private void Player_MarkerReached(object sender, TimelineMarkerReachedInfo args)
        {
            if (MarkerReached != null)
            {
                var mm = new ScriptMediaMarker(args.Marker);
                MarkerReached(this, new ScriptEventArgs<ScriptMediaMarker>(mm));
            }
        }

        /// <summary>
        /// Plays the current Playlist Item.
        /// </summary>
        [ScriptableMember]
        public void Play()
        {
            _player.Play();
        }

        /// <summary>
        /// Pauses the current Playlist Item.
        /// </summary>
        [ScriptableMember]
        public void Pause()
        {
            _player.Pause();
        }

        /// <summary>
        /// Stops the current Playlist Item.
        /// </summary>
        [ScriptableMember]
        public void Stop()
        {
            _player.Stop();
        }

        /// <summary>
        /// Goes to the next Playlist Item.
        /// </summary>
        [ScriptableMember]
        public void GoToNextPlaylistItem()
        {
            _player.GoToNextPlaylistItem();
        }

        /// <summary>
        /// Goes to the previous Playlist Item.
        /// </summary>
        [ScriptableMember]
        public void GoToPreviousPlaylistItem()
        {
            _player.GoToPreviousPlaylistItem();
        }

        /// <summary>
        /// Goes the Playlist Item at the specified index within the Playlist.
        /// </summary>
        /// <param name="index"></param>
        [ScriptableMember]
        public void GoToPlaylistItem(int index)
        {
            _player.GoToPlaylistItem(index);
        }

        /// <summary>
        /// Goes to the next chapter.
        /// </summary>
        [ScriptableMember]
        public void GoToNextChapter()
        {
            _player.GoToNextChapter();
        }

        /// <summary>
        /// Goes to the previous chapter.
        /// </summary>
        [ScriptableMember]
        public void GoToPreviousChapter()
        {
            _player.GoToPreviousChapter();
        }

        /// <summary>
        /// Goes to the chapter at the specified index within the Playlist Item.
        /// </summary>
        /// <param name="index"></param>
        [ScriptableMember]
        public void GoToChapterItem(int index)
        {
            _player.GoToChapterItem(index);
        }

        /// <summary>
        /// Seeks to the specified position, in seconds, within the current media.
        /// </summary>
        /// <param name="seconds">The position, in seconds, to seek to within the current media.</param>
        [ScriptableMember]
        public void SeekToPosition(double seconds)
        {
            _player.SeekToPosition(seconds);
        }

        /// <summary>
        /// Starts fast forward.
        /// </summary>
        [ScriptableMember]
        public void StartFastForward()
        {
            _player.StartFastForward();
        }

        /// <summary>
        /// Stops fast forward.
        /// </summary>
        [ScriptableMember]
        public void StopFastForward()
        {
            _player.StopFastForward();
        }

        /// <summary>
        /// Starts rewinding.
        /// </summary>
        [ScriptableMember]
        public void StartRewind()
        {
            _player.StartRewind();
        }

        /// <summary>
        /// Stops rewinding.
        /// </summary>
        [ScriptableMember]
        public void StopRewind()
        {
            _player.StopRewind();
        }

        /// <summary>
        /// Starts slow motion.
        /// </summary>
        [ScriptableMember]
        public void StartSlowMotion()
        {
            _player.StartSlowMotion();
        }

        /// <summary>
        /// Stops slow motion.
        /// </summary>
        [ScriptableMember]
        public void StopSlowMotion()
        {
            _player.StopSlowMotion();
        }

        /// <summary>
        /// Replays the amount of time specified in ReplayOffset.
        /// </summary>
        [ScriptableMember]
        public void Replay()
        {
            _player.Replay();
        }

        /// <summary>
        /// Asynchronously loads plugins from the xap at the specified location.
        /// </summary>
        /// <param name="xapLocation"></param>
        [ScriptableMember]
        public void BeginAddExternalPlugins(string xapLocation)
        {
            _player.BeginAddExternalPlugins(new Uri(xapLocation, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Seeks to the live position within the current media.
        /// </summary>
        [ScriptableMember]
        public void SeekToLive()
        {
            _player.SeekToLive();
        }

        /// <summary>
        /// Converts the specified numeric value of seconds into text.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        [ScriptableMember]
        public string SecondsToText(double seconds)
        {
            TimeSpan ts = TimeSpan.FromSeconds(seconds);
            return MediaMarker.Format(ts);
        }

        /// <summary>
        /// Creates an empty playlist.
        /// </summary>
        /// <returns></returns>
        [ScriptableMember]
        public ScriptPlaylist CreatePlaylist()
        {
            var playlist = new ScriptPlaylist();
            return playlist;
        }

        /// <summary>
        /// Creates a Playlist Item with the specified property values.
        /// </summary>
        /// <param name="mediaSource">The source of the media to be played.</param>
        /// <param name="thumbSource">The source of a thumbnail to be used with the media.</param>
        /// <param name="title">The title of the Playlist Item.</param>
        /// <param name="description">A description of the Playlist Item.</param>
        /// <returns>A Playlist Item with the specified property values.</returns>
        [ScriptableMember]
        public ScriptPlaylistItem CreatePlaylistItem(string mediaSource, string thumbSource, string title,
                                                     string description)
        {
            return new ScriptPlaylistItem
                       {
                           MediaSource = mediaSource,
                           ThumbSource = thumbSource,
                           Title = title,
                           Description = description
                       };
        }

		/// <summary>
		/// Creates an empty Metadata collection.
		/// </summary>
		/// <returns></returns>
		[ScriptableMember]
		public MetadataCollection CreateMetadataCollection()
		{
			return new MetadataCollection();
		}

		/// <summary>
		/// Creates an emtpy Metadata item.
		/// </summary>
		[ScriptableMember]
		public MetadataItem CreateMetadataItem()
		{
			return new MetadataItem();
		}

        /// <summary>
        /// Creates an empty marker.
        /// </summary>
        /// <returns>An empty marker.</returns>
        [ScriptableMember]
        public ScriptMediaMarker CreateMarker()
        {
            return new ScriptMediaMarker();
        }

        /// <summary>
        /// Creates an empty chapter.
        /// </summary>
        /// <returns>An empty chapter.</returns>
        [ScriptableMember]
        public ScriptChapter CreateChapter()
        {
            return new ScriptChapter();
        }

        /// <summary>
        /// Sets the active playlist.
        /// </summary>
        /// <param name="playlist">The playlist to be played.</param>
        [ScriptableMember]
        public void SetPlaylist(ScriptPlaylist playlist)
        {
            ObservableCollection<PlaylistItem> newPlaylist = playlist.ToPlaylist();
            foreach (var item in newPlaylist)
            {
                if (!String.IsNullOrWhiteSpace(item.BearerToken))
                {
                    item.LicenseAcquirer = 
                        new Microsoft.SilverlightMediaFramework.Core.AMSTokenAuthentication.AMSBearerTokenLicenseAcquirer()
                    {
                        AddAuthorizationToken = true,
                        Token = item.BearerToken
                    };
                }
            }

            _player.Playlist = newPlaylist;
        }


        /// <summary>
        /// Gets the index of the active Playlist Item within the current Playlist.
        /// </summary>
        /// <returns></returns>
        [ScriptableMember]
        public int GetCurrentPlaylistIndex()
        {
            if (_player.CurrentPlaylistIndex != null)
            {
                return _player.CurrentPlaylistIndex.Value;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the current volume level.
        /// </summary>
        /// <returns></returns>
        [ScriptableMember]
        public double GetVolume()
        {
            return _player.VolumeLevel;
        }

        [ScriptableMember]
        public void SetVolume(double volumeLevel)
        {
            _player.VolumeLevel = volumeLevel;
        }
    }
}