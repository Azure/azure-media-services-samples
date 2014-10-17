using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    public class VideoSyncManager : VideoSyncManagerBase
    {
        private readonly PrioritizedHeuristicsSettings HeuristicSettings;

        private SmoothStreamingMediaElement masterMedia;

        public VideoSyncManager(PrioritizedHeuristicsSettings heuristicSettings)
        {
            HeuristicSettings = heuristicSettings;
            HeuristicManager = new PrioritizedHeuristicsManager();
            HeuristicManager.Clear();
            HeuristicManager.MaxReenableAttempts = heuristicSettings.MaxReenableAttempts;
            HeuristicManager.MonitorIntervalInMilliseconds = heuristicSettings.PollingFrequencyMillis;
            HeuristicManager.RecommendationChanged += HeuristicManager_RecommendationChanged;
        }

        public PrioritizedHeuristicsManager HeuristicManager { get; private set; }

        public SmoothStreamingMediaElement HeuristicMasterMedia
        {
            get { return masterMedia; }
            set
            {
                if (masterMedia != null)
                {
                    masterMedia.MediaOpened -= masterMedia_MediaOpened;
                }

                masterMedia = value;

                HeuristicManager.StopMonitoring();
                HeuristicManager.Clear();

                if (masterMedia != null)
                {
                    HeuristicManager.AddSmoothStreamingMediaElement(masterMedia, HeuristicSettings.PrimaryMinBitRate,
                                                                HeuristicSettings.PrimaryMinFrameRate);
                    masterMedia.MediaOpened += masterMedia_MediaOpened;
                }

                
            }
        }

        public event RoutedEventHandler ResettingHeuristics;


        //Assuming Master is ready
        //Assuming Slaves are in a heuristic loadable state
		public void ResetHeuristics()
		{
			if(ResettingHeuristics != null)
			{
				ResettingHeuristics(this, null);
			}

			HeuristicManager.StopMonitoring();
			HeuristicManager.Clear();

			if(HeuristicMasterMedia != null)
			{
				HeuristicManager.AddSmoothStreamingMediaElement(HeuristicMasterMedia, HeuristicSettings.PrimaryMinBitRate,
																HeuristicSettings.PrimaryMinFrameRate);
				// TODO:  determin min values depending on size of master

				foreach(SlaveMediaElement slave in SubMediaElements)
				{
					if(slave.MediaElement != HeuristicMasterMedia)
					{
						// reset current heuristic enabled flag
						slave.IsPhysicalSyncEnabled = false;

						//if (slave.IsLogicalSyncEnabled) // does it logically needed to be playing, if so, add it to HM
						//{
						if(slave.MediaElement != null)
						{
							HeuristicManager.AddSmoothStreamingMediaElement(slave.MediaElement,
																			HeuristicSettings.SecondaryMinBitRate,
																			HeuristicSettings.SecondaryMinFrameRate);
						}
					}
				}

				HeuristicManager.StartMonitoring();
			}
		}

        public void RegisterMaster(SmoothStreamingMediaElement media, TimeSpan position)
        {
            UnhookEvents();

            //MainMedia is for syncing
            //HeuristicsMasterMedia is used for Heuristics
            MainMedia = media;
            //MainMedia.Position = position;

            HookEvents();

            //UpdateSlaves();

            if (media == HeuristicMasterMedia)
            {
                HeuristicManager.StartMonitoring();
            }
            else
            {
                HeuristicManager.StopMonitoring();
            }
        }

        protected override void HookEvents()
        {
            base.HookEvents();

            //MainMedia.TrickPlayStateChanged += MainMedia_TrickPlayStateChanged;
            MainMedia.MediaFailed += MainMedia_MediaFailed;
            //MainMedia.RetrySuccessful += MainMedia_RetrySuccessful;
        }

        private void MainMedia_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            PauseAll();
        }

        private void MainMedia_RetrySuccessful(object sender, RoutedEventArgs e)
        {
            //RetrySlaves();

            UnpauseAll();
        }

        //private void RetrySlaves()
        //{
        //    foreach (SlaveMediaElement slave in SubMediaElements)
        //    {
        //        if (slave.MediaElement.CurrentState == SmoothStreamingMediaElementState.Closed)
        //        {
        //            slave.MediaElement.Retry();
        //        }
        //    }
        //}

        private void UnpauseAll()
        {
            HeuristicManager.StartMonitoring();

            MainMedia.Play();
        }

        private void PauseAll()
        {
            HeuristicManager.StopMonitoring();

            foreach (SlaveMediaElement slave in SubMediaElements)
            {
                if (slave.MediaElement != MainMedia)
                {
                    if (slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Paused
                        && slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Closed)
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

        

        protected override void UnhookEvents()
        {
            if (MainMedia != null)
            {
                //Moved w/i if{} block to prevent null exception -Kevin R. 5-4-10
                base.UnhookEvents();

                //MainMedia.TrickPlayStateChanged -= MainMedia_TrickPlayStateChanged;
                MainMedia.MediaFailed -= MainMedia_MediaFailed;
            }
        }

        public void UpdateSlaves()
        {
            foreach (SlaveMediaElement slave in SubMediaElements)
            {
                if (slave.IsLogicalSyncEnabled)
                {
                    slave.MediaElement.Play();
                }
            }
        }

        public void SuspendSync()
        {
            UnhookEvents();
            Timer.Stop();

            PauseAll();
        }

        public void ResumeSync()
        {
            ResumeSync(true);
        }

        public void ResumeSync(bool forceresumePlay)
        {
            HookEvents();
            Timer.Start();

            if (forceresumePlay)
            {
                UnpauseAll();
            }
            else
            {
                HeuristicManager.StartMonitoring();
            }
        }

        public void Suspend(SmoothStreamingMediaElement media)
        {
            if (media != MainMedia)
            {
                // disabling slave media
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (slave.MediaElement == media)
                    {
                        slave.IsLogicalSyncEnabled = false;
                        try
                        {
                            slave.MediaElement.Pause();
                        }
                        catch (InvalidOperationException) { }
                    }
                }
            }
            else
            {
                // disable main media
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    slave.IsLogicalSyncEnabled = false;
                }

                try
                {
                    MainMedia.Pause();
                }
                catch (InvalidOperationException) { }
            }
        }

        public void Resume(SmoothStreamingMediaElement media)
        {
            if (media != MainMedia)
            {
                // disabling slave media
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (slave.MediaElement == media)
                    {
                        slave.IsLogicalSyncEnabled = true;

                        if (slave.IsPhysicalSyncEnabled)
                        {
                            slave.MediaElement.Play();
                        }
                    }
                }
            }
            else
            {
                // disable main media
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    slave.IsLogicalSyncEnabled = true;
                }

                MainMedia.Play();
            }
        }

        private void masterMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            HeuristicManager.StartMonitoring();

            // the live video intermittently starts at the beginning of the live video
            // instead of the end, it has been difficult to reproduce and resolve, calling
            // StartSeekToLive is a workaround to solve the problem, it can be removed if 
            // find the root problem of the issue
            //
            // only want to run the workaround for live streams and when the initial 
            // video is loaded, not when the stream is restored by the auto retry 
            //if (MainMedia.IsLive && MainMedia.RetryState == RetryState.None)
            if (MainMedia.IsLive)
            {
                MainMedia.StartSeekToLive();
            }

            MainMedia.Play();
        }

        private void HeuristicManager_RecommendationChanged(object sender, RecommendationChangedEventArgs e)
        {
            Debug.WriteLine("*** Recommend Enabled count: {0}", e.RecommendEnable.Count);
            Debug.WriteLine("--- Recommend disabled count: {0}", e.RecommendDisable.Count);
            foreach (SmoothStreamingMediaElement ssme in e.RecommendEnable)
            {
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (ssme == slave.MediaElement)
                    {
                        slave.IsPhysicalSyncEnabled = true;

                        if (MainMedia != null && MainMedia.CurrentState != SmoothStreamingMediaElementState.Paused)
                        {
                            slave.MediaElement.Play();
                        }
                        
                        //if (slave.MediaElement.DataContext is Video)
                        //{
                        //    if (MainMedia.RetryState != RetryState.None)
                        //    {
                        //        ((Video)slave.MediaElement.DataContext).IsMiniCamOn = false;
                        //    }
                        //    else
                        //    {
                        //        ((Video)slave.MediaElement.DataContext).IsMiniCamOn = true;
                        //    }
                        //}
                    }
                }
            }

            foreach (SmoothStreamingMediaElement ssme in e.RecommendDisable)
            {
                foreach (SlaveMediaElement slave in SubMediaElements)
                {
                    if (ssme == slave.MediaElement)
                    {
                        slave.IsPhysicalSyncEnabled = false;

                        if (slave.MediaElement.CurrentState != SmoothStreamingMediaElementState.Paused)
                        {
                            try
                            {
                                slave.MediaElement.Pause();
                            }
                            catch (InvalidOperationException) { }
                        }

                        //if (slave.MediaElement.DataContext is Video)
                        //{
                        //    ((Video)slave.MediaElement.DataContext).IsMiniCamOn = false;
                        //}
                    }
                }
            }
        }

        // Switching to the alt. cam should trigger retry logic, which should retry all slaves
        //public void RetryAllSlaves()
        //{
        //    foreach (SlaveMediaElement slave in SubMediaElements)
        //    {
        //        if (slave.MediaElement.RetryState != RetryState.None)
        //        {
        //            slave.MediaElement.ResetAutoRetry();
        //            slave.MediaElement.Retry();
        //        }
        //    }
        //}
    }
}