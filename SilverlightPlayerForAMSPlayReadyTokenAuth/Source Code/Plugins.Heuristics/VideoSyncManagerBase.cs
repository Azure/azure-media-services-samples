using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    public class SlaveMediaElement
    {
        public SmoothStreamingMediaElement MediaElement { get; set; }
        public bool IsPhysicalSyncEnabled { get; set; }
        public bool IsLogicalSyncEnabled { get; set; }
        public TimeSpan Tolerance { get; set; }
        public ulong BitRate { get; set; }
    }

    public class VideoSyncManagerBase
    {
        public DispatcherTimer Timer;
        private SmoothStreamingMediaElement mainMedia;

        public VideoSyncManagerBase()
        {
            SubMediaElements = new ObservableCollection<SlaveMediaElement>();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += timer_Tick;
        }

        public SmoothStreamingMediaElement MainMedia
        {
            get { return mainMedia; }
            set
            {
                if (mainMedia != value)
                {
                    mainMedia = value;

                    CleanUp();


                    if (mainMedia != null)
                    {
                        HookEvents();
                    }
                }
            }
        }

        public ObservableCollection<SlaveMediaElement> SubMediaElements { get; set; }

        private void CleanUp()
        {
            SlaveMediaElement mainMedia =
                (from Media in SubMediaElements where Media.MediaElement == MainMedia select Media).SingleOrDefault();

            if (mainMedia != null)
            {
                SubMediaElements.Remove(mainMedia);
            }
        }

        public void DisableSync(SmoothStreamingMediaElement slave)
        {
            if (slave != null)
            {
                foreach (SlaveMediaElement video in SubMediaElements)
                {
                    if (video.MediaElement != null && video.MediaElement == slave)
                    {
                        video.IsLogicalSyncEnabled = false;
                        break;
                    }
                }
            }
        }

        public void EnableSync(SmoothStreamingMediaElement slave)
        {
            if (slave != null)
            {
                foreach (SlaveMediaElement video in SubMediaElements)
                {
                    if (video.MediaElement != null && video.MediaElement == slave)
                    {
                        video.IsLogicalSyncEnabled = true;
                        break;
                    }
                }
            }
        }


        protected virtual void UnhookEvents()
        {
            MainMedia.MediaOpened -= MainMedia_MediaOpened;
        }

        protected virtual void HookEvents()
        {
            MainMedia.MediaOpened += MainMedia_MediaOpened;
            MainMedia.CurrentStateChanged += MainMedia_CurrentStateChanged;

            MainMedia.CurrentStateChanged += MainMedia_CurrentPlaybackStateChanged;
        }

        protected void MainMedia_CurrentPlaybackStateChanged(object sender, RoutedEventArgs e)
        {
            foreach (SlaveMediaElement slave in SubMediaElements)
            {
                if (slave.IsPhysicalSyncEnabled && slave.IsLogicalSyncEnabled && MainMedia != null && MainMedia.PlaybackRate.HasValue)
                {
                    slave.MediaElement.SetPlaybackRate(MainMedia.PlaybackRate.Value);
                }
            }
        }

        protected void MainMedia_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (MainMedia != null && MainMedia.CurrentState == SmoothStreamingMediaElementState.Paused)
            {
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Paused)
                    {
                        //if (slave.IsPhysicalSyncEnabled && slave.IsLogicalSyncEnabled)
                        //{
                        try
                        {
                            slave.MediaElement.Pause();
                        }
                        catch (InvalidOperationException)
                        {
                            //This happens when the end of the video has been reached.
                        }
                        //}
                    }
                }
            }
        }

        protected void MainMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            Timer.Start();

            foreach (SlaveMediaElement slave in SubMediaElements)
            {
                if (slave.IsPhysicalSyncEnabled && slave.IsLogicalSyncEnabled)
                {
                    slave.MediaElement.Position = MainMedia.Position;
                    slave.MediaElement.Play();
                }
            }
        }

        protected virtual void timer_Tick(object sender, EventArgs e)
        {
            lock (Timer)
            {
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (slave.IsPhysicalSyncEnabled && MainMedia != null)
                    {
                        if (slave != null && slave.MediaElement.SmoothStreamingSource != null)
                        {
                            if (slave.IsLogicalSyncEnabled)
                            {
                                if (slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Buffering
                                    && slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Closed
                                    &&
                                    (slave.MediaElement.Position > MainMedia.Position.Add(slave.Tolerance) ||
                                     slave.MediaElement.Position < MainMedia.Position.Subtract(slave.Tolerance)))
                                {
                                    //if (slave.MediaElement.RetryState == RetryState.None)
                                    //{
                                    slave.MediaElement.Position = MainMedia.Position;
                                    //}
                                }

                                if (MainMedia.CurrentState == SmoothStreamingMediaElementState.Playing &&
                                    slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Playing
                                    && slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Closed)
                                {
                                    slave.MediaElement.Play();
                                }
                                else if (MainMedia.CurrentState == SmoothStreamingMediaElementState.Paused &&
                                         slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Paused
                                        && slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Closed)
                                {
                                    try
                                    {
                                        slave.MediaElement.Pause();
                                    }
                                    catch (InvalidOperationException) { }
                                }

                                slave.MediaElement.IsMuted = true;
                            }
                        }
                        else
                        {
                            if (slave.MediaElement != MainMedia)
                            {
                                if (slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Paused)
                                {
                                    try
                                    {
                                        slave.MediaElement.Pause();
                                    }
                                    catch (InvalidOperationException) { }
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            slave.MediaElement.Pause();
                        }
                        catch (InvalidOperationException) { }
                    }
                }
            }
        }
    }
}