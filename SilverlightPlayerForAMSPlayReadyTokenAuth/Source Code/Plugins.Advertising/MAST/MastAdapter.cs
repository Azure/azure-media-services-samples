using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.Text;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Adapts an IPlayer to IMastAdapter.
    /// Used to provide the MAST conditions all the info they need to fire triggers.
    /// </summary>
    public class MastAdapter : IMastAdapter, IDisposable
    {
        IPlayer player = null;
        DateTime startPlayTimestamp;
        TimeSpan totalWatchedTime = TimeSpan.Zero;
        TimeSpan watchedTime = TimeSpan.Zero;

        public MastAdapter(IPlayer player)
        {
            if (player == null) throw new NullReferenceException("Player cannot be null.");
            if (!(player is FrameworkElement)) throw new NullReferenceException("Player must be a FrameworkElement.");
            this.player = player;
            HookPlayerEvents();
        }

        #region MAST Events

        public event EventHandler TimelineChanged;

        public event EventHandler OnPlay;

        public event EventHandler OnStop;

        public event EventHandler OnPause;

        public event EventHandler OnMute;

        public event EventHandler OnVolumeChange;

        public event EventHandler OnEnd;

        public event EventHandler OnItemStarting;

        public event EventHandler OnItemStart;

        public event EventHandler OnItemEnd;

        public event EventHandler OnSeek;

        public event EventHandler OnFullScreenChange;

        public event EventHandler OnError;

        public event EventHandler OnMouseOver;

        public event EventHandler OnPlayerSizeChanged;

        #endregion

        #region MAST Properties

        public TimeSpan Duration
        {
            get { return player.Duration; }
        }

        public TimeSpan Position
        {
            get { return player.Position; }
        }

        public TimeSpan WatchedTime
        {
            get
            {
                if (IsPlaying)
                    return watchedTime.Add(DateTime.Now.Subtract(startPlayTimestamp));
                else
                    return watchedTime;
            }
        }

        public TimeSpan TotalWatchedTime
        {
            get
            {
                if (IsPlaying)
                    return totalWatchedTime.Add(DateTime.Now.Subtract(startPlayTimestamp));
                else
                    return totalWatchedTime;
            }
        }

        public DateTime SystemTime
        {
            get { return DateTime.Now; }
        }

#if !WINDOWS_PHONE && !FULLSCREEN
        public bool FullScreen
        {
            get { return player.IsFullScreen; }
        }
#endif

        public bool IsPlaying
        {
            get { return player.PlayState == MediaPluginState.Playing; }
        }

        public bool IsPaused
        {
            get { return player.PlayState == MediaPluginState.Paused; }
        }

        public bool IsStopped
        {
            get { return player.PlayState == MediaPluginState.Stopped; }
        }

        public bool CaptionsActive
        {
            get { return player.CaptionsActive; }
        }

        public bool HasCaptions
        {
            get { return player.HasCaptions; }
        }

        public bool HasVideo
        {
            get { return !player.VideoResolution.IsEmpty; }
        }

        public bool HasAudio
        {
            get { return player.HasAudio; }
        }

        private int itemCount = 0;
        public int ItemsPlayed
        {
            get { return itemCount; }
        }

        public int PlayerWidth
        {
            get { return (int)player.MediaElementSize.Width; }
        }

        public int PlayerHeight
        {
            get { return (int)player.MediaElementSize.Height; }
        }

        public int ContentWidth
        {
            get { return (int)player.VideoResolution.Width; }
        }

        public int ContentHeight
        {
            get { return (int)player.VideoResolution.Height; }
        }

        public long ContentBitrate
        {
            get { return player.PlaybackBitrate; }
        }

        public string ContentTitle
        {
            get { return player.ContentTitle; }
        }

        public string ContentUrl
        {
            get { return player.ContentUrl.OriginalString; }
        }

        private bool MediaOpened;
        private bool MediaFailed;
        private bool IsReady;

        public void Ready()
        {
            IsReady = true;
            if (MediaOpened)
            {
                if (OnItemStarting != null) OnItemStarting(this, EventArgs.Empty);
                if (OnItemStart != null) OnItemStart(this, EventArgs.Empty);
            }
            else if (MediaFailed)
            {
                if (OnError != null) OnError(this, EventArgs.Empty);
            }
        }

        #endregion

        #region MAST Event Handling

        void HookPlayerEvents()
        {
            {
                var element = player as FrameworkElement;
                element.SizeChanged += new SizeChangedEventHandler(player_SizeChanged);
                element.MouseEnter += new System.Windows.Input.MouseEventHandler(element_MouseEnter);
            }
#if !WINDOWS_PHONE && !FULLSCREEN
            player.FullScreenChanged += new EventHandler(player_FullScreenChanged);
#endif
            player.VolumeChanged += new EventHandler(player_VolumeChanged);
            player.MediaOpened += new EventHandler(player_MediaOpened);
            player.SeekCompleted += new EventHandler(player_SeekCompleted);
            player.StateChanged += new EventHandler(player_StateChanged);
            player.MediaEnded += new EventHandler(player_MediaEnded);
            player.MediaFailed += new EventHandler(player_MediaFailed);
            player.PlayEnded += new EventHandler(player_PlayEnded);
            player.TimelineChanged += new EventHandler(player_TimelineChanged);
        }

        void UnhookPlayerEvents()
        {
            {
                var element = player as FrameworkElement;
                element.SizeChanged -= new SizeChangedEventHandler(player_SizeChanged);
                element.MouseEnter -= new System.Windows.Input.MouseEventHandler(element_MouseEnter);
            }
#if !WINDOWS_PHONE && !FULLSCREEN
            player.FullScreenChanged -= new EventHandler(player_FullScreenChanged);
#endif
            player.VolumeChanged -= new EventHandler(player_VolumeChanged);
            player.MediaOpened -= new EventHandler(player_MediaOpened);
            player.SeekCompleted -= new EventHandler(player_SeekCompleted);
            player.StateChanged -= new EventHandler(player_StateChanged);
            player.MediaEnded -= new EventHandler(player_MediaEnded);
            player.MediaFailed -= new EventHandler(player_MediaFailed);
            player.PlayEnded -= new EventHandler(player_PlayEnded);
            player.TimelineChanged -= new EventHandler(player_TimelineChanged);
        }

        void player_TimelineChanged(object sender, EventArgs e)
        {
            if (TimelineChanged != null) TimelineChanged(this, EventArgs.Empty);
        }

        void player_MediaFailed(object sender, EventArgs e)
        {
            MediaFailed = true;
            //System.Diagnostics.Debug.WriteLine("OnError");
            if (IsReady)
            {
                if (OnError != null) OnError(this, EventArgs.Empty);
            }
        }

        void player_PlayEnded(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnEnd");
            if (OnEnd != null) OnEnd(this, EventArgs.Empty);
        }

        void element_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnMouseOver");
            if (OnMouseOver != null) OnMouseOver(this, EventArgs.Empty);
        }

        bool isPlaying = false;
        void player_StateChanged(object sender, EventArgs e)
        {
            switch (player.PlayState)
            {
                case MediaPluginState.Paused:
                    if (isPlaying)  // added just to be safe so we don't somehow run this more than once
                    {
                        isPlaying = false;
                        var now = DateTime.Now;
                        watchedTime = watchedTime.Add(now.Subtract(startPlayTimestamp));
                        totalWatchedTime = totalWatchedTime.Add(now.Subtract(startPlayTimestamp));
                    }
                    //System.Diagnostics.Debug.WriteLine("OnPause");
                    if (OnPause != null) OnPause(this, e);
                    break;
                case MediaPluginState.Playing:
                    isPlaying = true;
                    startPlayTimestamp = DateTime.Now;
                    //System.Diagnostics.Debug.WriteLine("OnPlay");
                    if (OnPlay != null) OnPlay(this, e);
                    break;
                case MediaPluginState.Stopped:
                    if (isPlaying)  // added just to be safe so we don't somehow run this more than once
                    {
                        isPlaying = false;
                        watchedTime = TimeSpan.Zero;
                        totalWatchedTime = totalWatchedTime.Add(DateTime.Now.Subtract(startPlayTimestamp));
                    }
                    //System.Diagnostics.Debug.WriteLine("OnStop");
                    if (OnStop != null) OnStop(this, e);
                    break;
                case MediaPluginState.Closed:
                    if (isPlaying)  // added just to be safe so we don't somehow run this more than once
                    {
                        isPlaying = false;
                        watchedTime = TimeSpan.Zero;
                        totalWatchedTime = totalWatchedTime.Add(DateTime.Now.Subtract(startPlayTimestamp));
                    }
                    break;
            }
        }

        void player_SeekCompleted(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnSeek");
            if (OnSeek != null) OnSeek(this, e);
        }

        void player_MediaEnded(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnItemEnd");
            if (OnItemEnd != null) OnItemEnd(this, e);
        }

        void player_MediaOpened(object sender, EventArgs e)
        {
            MediaOpened = true;
            //System.Diagnostics.Debug.WriteLine("OnItemStart");
            if (IsReady)
            {
                if (OnItemStarting != null) OnItemStarting(this, EventArgs.Empty);
                if (OnItemStart != null) OnItemStart(this, EventArgs.Empty);
            }

            itemCount++;
        }

        void player_VolumeChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnVolumeChange");
            if (OnVolumeChange != null) OnVolumeChange(this, e);
            if (player.Volume <= 0)
            {
                //System.Diagnostics.Debug.WriteLine("OnMute");
                if (OnMute != null) OnMute(this, e);
            }
        }

        void player_FullScreenChanged(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnFullScreenChange");
            if (OnFullScreenChange != null) OnFullScreenChange(this, e);
        }

        void player_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnPlayerSizeChanged");
            if (OnPlayerSizeChanged != null) OnPlayerSizeChanged(this, e);
        }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Duration: " + Duration);
            sb.AppendLine("Position: " + Position);
            sb.AppendLine("WatchedTime: " + WatchedTime);
            sb.AppendLine("TotalWatchedTime: " + TotalWatchedTime);
            sb.AppendLine("SystemTime: " + SystemTime);
#if !WINDOWS_PHONE && !FULLSCREEN
            sb.AppendLine("FullScreen: " + FullScreen);
#endif
            sb.AppendLine("IsPlaying: " + IsPlaying);
            sb.AppendLine("IsPaused: " + IsPaused);
            sb.AppendLine("IsStopped: " + IsStopped);
            sb.AppendLine("CaptionsActive: " + CaptionsActive);
            sb.AppendLine("HasVideo: " + HasVideo);
            sb.AppendLine("HasAudio: " + HasAudio);
            sb.AppendLine("HasCaptions: " + HasCaptions);
            sb.AppendLine("ItemsPlayed: " + ItemsPlayed);
            sb.AppendLine("PlayerWidth: " + PlayerWidth);
            sb.AppendLine("PlayerHeight: " + PlayerHeight);
            sb.AppendLine("ContentWidth: " + ContentWidth);
            sb.AppendLine("ContentHeight: " + ContentHeight);
            sb.AppendLine("ContentBitrate: " + ContentBitrate);
            sb.AppendLine("ContentTitle: " + ContentTitle);
            sb.AppendLine("ContentUrl: " + ContentUrl);
            return sb.ToString();
        }

        public void Dispose()
        {
            if (player != null)
            {
                UnhookPlayerEvents();
                player = null;
            }
        }
    }
}
