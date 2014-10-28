using System;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A reusable base class based on AdPlayerBase to make it easy to build VPAID ad players with quartile events.
    /// </summary>
    public abstract class QuartileAdPlayerBase : AdPlayerBase
    {
        public QuartileAdPlayerBase(Size Dimensions, bool Scalable, bool Linear)
            : base(Dimensions, Scalable, Linear)
        { }

        private DateTime startTime;
        private DispatcherTimer timer;
        private double lastProgress;
        private DateTime pauseTime;
        private TimeSpan? duration = null;

        private bool isStopped = true;
        protected bool IsStopped { get { return isStopped; } }

        /// <summary>
        /// Provides the duration of the ad.
        /// </summary>
        /// <returns></returns>
        protected abstract TimeSpan? Duration { get; }

        /// <summary>
        /// Provides the position of the ad.
        /// This will use a timer to report the position unless it is overridden.
        /// </summary>
        /// <returns></returns>
        public virtual TimeSpan Position { get; set; }

        /// <summary>
        /// Seeks to a new position.
        /// </summary>
        /// <param name="position">The new position of the ad</param>
        public virtual void SeekToPosition(TimeSpan position)
        {
            Position = position;
            startTime = DateTime.Now.Subtract(position);
        }

        protected void StartVideo()
        {
            startTime = DateTime.Now;
            duration = Duration;
            Position = TimeSpan.Zero;
            lastProgress = 0.0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += timer_Tick;
            timer.Start();
            isStopped = false;

            OnAdImpression();
        }

        public virtual void Shutdown()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Tick -= timer_Tick;
                timer.Stop();
                timer = null;
            }
            isStopped = true;
        }

        public virtual void CompletedVideo()
        {
            OnAdVideoComplete();
            Shutdown();
            OnAdStopped();
        }

        public override void Resume()
        {
            startTime = DateTime.Now.Subtract(pauseTime.Subtract(startTime));
            timer.Start();
            base.Resume();
        }

        public override void Pause()
        {
            pauseTime = DateTime.Now;
            timer.Stop();
            base.Pause();
        }

        public override TimeSpan AdRemainingTime
        {
            get
            {
                if (duration.HasValue)
                {
                    return duration.Value.Subtract(Position);
                }
                else
                {
                    return base.AdRemainingTime;
                }
            }
        }

        protected virtual void UpdatePosition()
        {
            Position = DateTime.Now.Subtract(startTime);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (duration.HasValue && !isStopped)
            {
                UpdatePosition();
                OnAdRemainingTimeChange();

                double percent = Position.TotalSeconds / duration.Value.TotalSeconds;

                if (lastProgress == 0.0 && percent >= 0.0)
                {
                    OnAdVideoStart();
                }
                else if (lastProgress < .25 && percent >= .25)
                {
                    OnAdVideoFirstQuartile();
                }
                else if (lastProgress < .5 && percent >= .5)
                {
                    OnAdVideoMidpoint();
                }
                else if (lastProgress < .75 && percent >= .75)
                {
                    OnAdVideoThirdQuartile();
                }
                else if (lastProgress < 1 && percent >= 1)
                {
                    CompletedVideo();
                }
                lastProgress = percent;
            }
        }

        public override void Dispose()
        {
            Shutdown();
            base.Dispose();
        }
    }
}
