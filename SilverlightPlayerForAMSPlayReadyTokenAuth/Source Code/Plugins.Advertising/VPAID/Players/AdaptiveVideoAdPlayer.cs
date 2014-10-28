using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Web.Media.SmoothStreaming;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A VPAID ad player that can play adaptive video ads
    /// </summary>
    public class AdaptiveVideoAdPlayer : QuartileAdPlayerBase
    {
        public AdaptiveVideoAdPlayer(ICreativeSource AdSource, IAdTarget AdTarget, IMediaPlugin ActiveMediaPlugin)
            : base(AdSource.Dimensions, AdSource.IsScalable, AdSource.Type == CreativeSourceType.Linear)
        {
            this.ActiveMediaPlugin = ActiveMediaPlugin;
            this.AdSource = AdSource;
            this.AdTarget = AdTarget;
            if (AdSource.ClickUrl != null)
            {
                this.NavigateUri = new Uri(AdSource.ClickUrl, UriKind.RelativeOrAbsolute);
            }
        }

        protected IMediaPlugin ActiveMediaPlugin;
        public IAdTarget AdTarget { get; private set; }
        public ICreativeSource AdSource { get; private set; }

        private SmoothStreamingMediaElementState lastState = SmoothStreamingMediaElementState.Stopped;
        private bool isStartupHeuristicsActive;

        protected override void OnClick()
        {
            OnAdClickThru(new AdClickThruEventArgs(AdSource.ClickUrl, AdSource.Id, false));
            base.OnClick();
        }

        protected override TimeSpan? Duration
        {
            get
            {
                if (AdSource.Duration.HasValue)
                    return AdSource.Duration;
                else if (MediaElement.NaturalDuration.HasTimeSpan)
                    return MediaElement.NaturalDuration.TimeSpan;
                else
                    return null;
            }
        }

        public override TimeSpan Position
        {
            get
            {
                return MediaElement.Position;
            }
        }

        protected override void UpdatePosition()
        {
            base.Position = this.Position;
        }

#if HACKMODE
        static int NameIndex = 0;
        public static string GetNewName()
        {
            NameIndex++;
            return string.Format("SSME_Ad{0}", NameIndex);
        }
#endif

        protected override FrameworkElement CreateContentsElement()
        {
            var me = new SmoothStreamingMediaElement();
#if HACKMODE
            me.Name = GetNewName();
#endif
            if (AdSource.IsScalable)
            {
                if (AdSource.MaintainAspectRatio)
                {
                    me.Stretch = Stretch.Uniform;
                }
                else
                {
                    me.Stretch = Stretch.Fill;
                }
            }
            else
            {
                me.Stretch = Stretch.None;
            }

            me.AutoPlay = false;
            me.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(OnAdPlayerMediaFailed);
            me.MediaEnded += new RoutedEventHandler(OnAdPlayerMediaEnded);
            me.MediaOpened += new RoutedEventHandler(OnAdPlayerMediaOpened);
            me.CurrentStateChanged += new RoutedEventHandler(OnAdPlayerCurrentStateChanged);
            me.ManifestReady += new EventHandler<EventArgs>(me_ManifestReady);

            return me;
        }

        void me_ManifestReady(object sender, EventArgs e)
        {
            OnManifestReady();
        }

        protected virtual void OnManifestReady()
        {
            isStartupHeuristicsActive = false;
            if (DesiredBitrate > 0)
            {
                var segment = MediaElement.ManifestInfo.Segments[MediaElement.CurrentSegmentIndex.Value];
                foreach (var videoStream in segment.SelectedStreams.Where(i => i.Type == MediaStreamType.Video))
                {
                    var startupTrack = videoStream.AvailableTracks.OrderBy(t => Math.Abs((long)(t.Bitrate / 1024) - DesiredBitrate / 2)).FirstOrDefault();
                    if (startupTrack != null)
                    {
                        isStartupHeuristicsActive = true;
                        videoStream.SelectTracks(new[] { startupTrack }, false);
                    }
                }
            }
        }

        protected SmoothStreamingMediaElement MediaElement
        {
            get
            {
                return ContentsElement as SmoothStreamingMediaElement;
            }
        }

        public override void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            lastState = SmoothStreamingMediaElementState.Closed;
            base.Init(width, height, viewMode, desiredBitrate, creativeData, environmentVariables);
            MediaElement.SmoothStreamingSource = new Uri(creativeData);

            if (AdLinear)
            {
                LoadLinear();
            }
        }

        public override double AdVolume
        {
            get
            {
                return MediaElement.Volume * 100;
            }
            set
            {
                MediaElement.Volume = value / 100.0;
            }
        }

        public override void StartAd()
        {
            base.Start();
            MediaElement.Play();
        }

        public override void StopAd()
        {
            base.Stop();
            MediaElement.Stop();
        }

        public override void ResumeAd()
        {
            base.Resume();
            MediaElement.Play();
        }

        public override void PauseAd()
        {
            base.Pause();
            MediaElement.Pause();
        }

        void OnAdPlayerMediaOpened(object sender, RoutedEventArgs e)
        {
            OnAdLoaded();
        }

        void OnAdPlayerMediaEnded(object sender, RoutedEventArgs e)
        {
            CompletedVideo();
        }

        void OnAdPlayerMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Shutdown();
            OnAdError(new AdMessageEventArgs(e.ErrorException.Message));
        }

        void OnAdPlayerCurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (isStartupHeuristicsActive && MediaElement.CurrentState == SmoothStreamingMediaElementState.Playing)
            {
                // open up all available tracks again. We selected a specific startup track in order to get better bitrates when the ad began.
                isStartupHeuristicsActive = false;
                var segment = MediaElement.ManifestInfo.Segments[MediaElement.CurrentSegmentIndex.Value];
                foreach (var videoStream in segment.SelectedStreams.Where(i => i.Type == MediaStreamType.Video))
                {
                    videoStream.SelectTracks(videoStream.AvailableTracks, false);
                }
            }

            if (lastState == MediaElement.CurrentState) return;

            switch (MediaElement.CurrentState)
            {
                case SmoothStreamingMediaElementState.Paused:
                    if (IsPaused)
                    {
                        OnAdPaused();
                    }
                    break;
                case SmoothStreamingMediaElementState.Playing:
                    if (IsResumed)
                    {
                        OnAdResumed();
                    }
                    else if (IsStopped)
                    {
                        OnAdStarted();
                        StartVideo();
                    }
                    break;
            }

            if (MediaElement != null)
            {
                OnAdLog(new AdMessageEventArgs(string.Format("Status changed from {0} to {1}", lastState, MediaElement.CurrentState)));

                //ignore these types, they arent relevent
                if (!(MediaElement.CurrentState == SmoothStreamingMediaElementState.Buffering || MediaElement.CurrentState == SmoothStreamingMediaElementState.AcquiringLicense || MediaElement.CurrentState == SmoothStreamingMediaElementState.Individualizing))
                {
                    lastState = MediaElement.CurrentState;
                }
            }
        }

        protected override void OnAdLinearChanged()
        {
            base.OnAdLinearChanged();
            if (AdLinear)
            {
                LoadLinear();
            }
            else
            {
                UnloadLinear();
            }
        }
        
        public override void Shutdown()
        {
            if (MediaElement != null && MediaElement.CurrentState == SmoothStreamingMediaElementState.Playing)
            {
                MediaElement.Stop();
            }
            base.Shutdown();
        }

        public override void Dispose()
        {
            if (AdLinear)
            {
                UnloadLinear();
            }

            if (MediaElement != null)
            {
                MediaElement.MediaFailed -= new EventHandler<ExceptionRoutedEventArgs>(OnAdPlayerMediaFailed);
                MediaElement.MediaEnded -= new RoutedEventHandler(OnAdPlayerMediaEnded);
                MediaElement.MediaOpened -= new RoutedEventHandler(OnAdPlayerMediaOpened);
                MediaElement.CurrentStateChanged -= new RoutedEventHandler(OnAdPlayerCurrentStateChanged);
                MediaElement.SmoothStreamingSource = null;
                MediaElement.Dispose();
            }

            ActiveMediaPlugin = null;

            base.Dispose();
        }

        //double previousVolume = .5;
        protected virtual void UnloadLinear() 
        {
            //if (ActiveMediaPlugin != null)
            //{
            //    ActiveMediaPlugin.CurrentStateChanged -= MediaPlugin_CurrentStateChanged;
            //    ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Visible);
            //    //ActiveMediaPlugin.Volume = previousVolume;
            //    ActiveMediaPlugin.Play();
            //}
        }

        protected virtual void LoadLinear() 
        {
            //if (ActiveMediaPlugin != null)
            //{
            //    PausePlayback(ActiveMediaPlugin);
            //    ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Collapsed);
            //    //previousVolume = ActiveMediaPlugin.Volume;
            //    //ActiveMediaPlugin.Volume = 0;
            //}
        }

        private void PausePlayback(IMediaPlugin mediaPlugin)
        {
            if (mediaPlugin.CurrentState == MediaPluginState.Buffering || mediaPlugin.CurrentState == MediaPluginState.Playing)
            {
                mediaPlugin.Pause();
            }
            else
            {
                mediaPlugin.CurrentStateChanged += MediaPlugin_CurrentStateChanged;
            }
        }

        void MediaPlugin_CurrentStateChanged(IMediaPlugin mediaPlugin, MediaPluginState state)
        {
            ActiveMediaPlugin.CurrentStateChanged -= MediaPlugin_CurrentStateChanged;
            PausePlayback(mediaPlugin);
        }
    }
}
