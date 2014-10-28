using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Plugins;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents a class that is used by the Player to control the play speed.
    /// </summary>
    public class PlaySpeedManager : IDisposable
    {
        private const double NaturalPlaySpeed = 1.0;
        private const long DefaultFastForwardStepFrequencyMillis = 750;
        private const long DefaultFastForwardStepSizeMillis = 5000;
        private const long DefaultRewindStepFrequencyMillis = 750;
        private const long DefaultRewindStepSizeMillis = 5000;
        private readonly IMediaPlugin _mediaPlugin;
        private readonly DispatcherTimer _stepTimer;

        private TimeSpan _currentStepSize;
        private bool _fakingFastForward;
        private bool _fakingRewind;

        public PlaySpeedManager(IMediaPlugin mediaPlugin, TimeSpan fastForwardStepSize, TimeSpan rewindStepSize,
                                TimeSpan fastForwardStepFrequency, TimeSpan rewindStepFrequency)
            : this(mediaPlugin)
        {
            FastForwardStepSize = fastForwardStepSize;
            FastForwardStepFrequency = fastForwardStepFrequency;
            RewindStepSize = rewindStepSize;
            RewindStepFrequency = rewindStepFrequency;
        }

        public PlaySpeedManager(IMediaPlugin mediaPlugin)
        {
            if (mediaPlugin == null) throw new ArgumentNullException("mediaPlugin");

            _mediaPlugin = mediaPlugin;
            _stepTimer = new DispatcherTimer();
            _stepTimer.Tick += StepTimer_Tick;

            FastForwardStepSize = TimeSpan.FromMilliseconds(DefaultFastForwardStepSizeMillis);
            RewindStepSize = TimeSpan.FromMilliseconds(DefaultRewindStepSizeMillis);
            FastForwardStepFrequency = TimeSpan.FromMilliseconds(DefaultFastForwardStepFrequencyMillis);
            RewindStepFrequency = TimeSpan.FromMilliseconds(DefaultRewindStepFrequencyMillis);
        }

        public TimeSpan FastForwardStepSize { get; set; }
        public TimeSpan RewindStepSize { get; set; }
        public TimeSpan FastForwardStepFrequency { get; set; }
        public TimeSpan RewindStepFrequency { get; set; }

        public IMediaPlugin MediaPlugin
        {
            get { return _mediaPlugin; }
        }

        /// <summary>
        /// Gets a value indicating whether the Player is currently fast forwarding.
        /// </summary>
        public bool IsFastForwarding
        {
            get { return _fakingFastForward || _mediaPlugin.PlaybackRate > NaturalPlaySpeed; }
        }

        public bool CanIncrementFastForward
        {
            get { return FastForwardPlaybackRates.FirstOrDefault(i => i > MediaPlugin.PlaybackRate) != default(double); }
        }

        /// <summary>
        /// Gets a value indicating whether the Player is currently rewinding.
        /// </summary>
        public bool IsRewinding
        {
            get { return _fakingRewind || _mediaPlugin.PlaybackRate < 0; }
        }

        public bool CanIncrementRewind
        {
            get { return RewindPlaybackRates.FirstOrDefault(i => i < MediaPlugin.PlaybackRate) != default(double); }
        }

        /// <summary>
        /// Gets a value indicating whether the Player is currently playing in slow motion.
        /// </summary>
        public bool IsSlowMotion
        {
            get { return _mediaPlugin.PlaybackRate > 0 && _mediaPlugin.PlaybackRate < NaturalPlaySpeed; }
        }

        /// <summary>
        /// Gets a value indicating whether the Player is currently playing at normal play speed.
        /// </summary>
        public bool IsPlaySpeedNormal
        {
            get { return _mediaPlugin.PlaybackRate == NaturalPlaySpeed && !_fakingFastForward && !_fakingRewind; }
        }

        /// <summary>
        /// Gets a value indicating whether the Player has any fast forwarding rates available to use.
        /// </summary>
        public bool SupportsNaturalFastForward
        {
            get { return FastForwardPlaybackRates.Any(); }
        }

        /// <summary>
        /// Gets a value indicating whether the Player has any rewind rates available to use.
        /// </summary>
        public bool SupportsNaturalRewind
        {
            get { return RewindPlaybackRates.Any(); }
        }

        /// <summary>
        /// Gets a value indicating whether the Player has any slow motion rates to use.
        /// </summary>
        public bool SupportsSlowMotion
        {
            get { return SlowMotionPlaybackRates.Any(); }
        }

        /// <summary>
        /// Gets a value indicating whether the Player has any fast forwarding rates to use.
        /// </summary>
        public IList<double> FastForwardPlaybackRates
        {
            get
            {
                return MediaPlugin.SupportedPlaybackRates
                    .Where(i => i > NaturalPlaySpeed)
                    .OrderBy(i => i)
                    .ToList();
            }
        }

        /// <summary>
        /// Gets a list of available rewind rates.
        /// </summary>
        public IList<double> RewindPlaybackRates
        {
            get
            {
                return MediaPlugin.SupportedPlaybackRates
                    .Where(i => i < 0)
                    .OrderByDescending(i => i)
                    .ToList();
            }
        }

        /// <summary>
        /// Gets a list of available slow motion rates.
        /// </summary>
        public IList<double> SlowMotionPlaybackRates
        {
            get
            {
                return MediaPlugin.SupportedPlaybackRates
                    .Where(i => i > 0 && i < NaturalPlaySpeed)
                    .OrderBy(i => i)
                    .ToList();
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Releases the resources used by the PlaySpeedManager.
        /// </summary>
        public void Dispose()
        {
            _stepTimer.Stop();
            _stepTimer.Tick -= StepTimer_Tick;
        }

        #endregion

        private void StepTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan newPosition;

            if (_fakingFastForward)
            {
                newPosition = MediaPlugin.Position.Add(_currentStepSize);

                if (newPosition < MediaPlugin.EndPosition)
                {
                    MediaPlugin.Position = newPosition;
                }
                else
                {
                    MediaPlugin.Position = MediaPlugin.EndPosition;
                    RestoreNaturalPlaySpeed();
                }
            }
            else if (_fakingRewind)
            {
                newPosition = MediaPlugin.Position.Subtract(_currentStepSize);

                if (newPosition > MediaPlugin.StartPosition)
                {
                    MediaPlugin.Position = newPosition;
                }
                else
                {
                    MediaPlugin.Position = MediaPlugin.StartPosition;
                    RestoreNaturalPlaySpeed();
                }
            }
        }

        private void StopTimer()
        {
            _stepTimer.Stop();
            _stepTimer.Interval = TimeSpan.Zero;
        }

        /// <summary>
        /// Begins fast forwarding if the media supports it.
        /// </summary>
        public void FastForward()
        {
            if (!IsFastForwarding)
            {
                StopTimer();

                if (SupportsNaturalFastForward)
                {
                    MediaPlugin.PlaybackRate = FastForwardPlaybackRates.First();
                }
                else
                {
                    RestoreNaturalPlaySpeedWithoutNotification();
                    _fakingFastForward = true;
                    _currentStepSize = FastForwardStepSize;
                    _stepTimer.Interval = FastForwardStepFrequency;
                    _stepTimer.Start();
                }
            }
        }

        /// <summary>
        /// Increments the fast forwarding rate if the media supports it.
        /// </summary>
        public void IncrementFastForward()
        {
            if (IsFastForwarding && SupportsNaturalFastForward)
            {
                double newPlaybackRate = FastForwardPlaybackRates
                    .Where(i => i > MediaPlugin.PlaybackRate)
                    .FirstOrDefault();

                if (newPlaybackRate != default(double))
                {
                    MediaPlugin.PlaybackRate = newPlaybackRate;
                }
            }
        }

        /// <summary>
        /// Decrements the fast forwarding rate if the media supports it.
        /// </summary>
        public void DecrementFastForward()
        {
            if (IsFastForwarding && SupportsNaturalFastForward)
            {
                double newPlaybackRate = FastForwardPlaybackRates
                    .Where(i => i < MediaPlugin.PlaybackRate)
                    .OrderByDescending(i => i)
                    .FirstOrDefault();

                if (newPlaybackRate != default(double))
                {
                    MediaPlugin.PlaybackRate = newPlaybackRate;
                }
            }
        }

        /// <summary>
        /// Begins rewinding if the media supports it.
        /// </summary>
        public void Rewind()
        {
            if (!IsRewinding)
            {
                StopTimer();

                if (SupportsNaturalRewind)
                {
                    MediaPlugin.PlaybackRate = RewindPlaybackRates.First();
                }
                else
                {
                    RestoreNaturalPlaySpeedWithoutNotification();
                    _fakingRewind = true;
                    _currentStepSize = RewindStepSize;
                    _stepTimer.Interval = RewindStepFrequency;
                    _stepTimer.Start();
                }
            }
        }

        /// <summary>
        /// Increments the rewinding rate if the media supports it.
        /// </summary>
        public void IncrementRewind()
        {
            if (IsRewinding && SupportsNaturalRewind)
            {
                double newPlaybackRate = RewindPlaybackRates
                    .Where(i => i < MediaPlugin.PlaybackRate)
                    .FirstOrDefault();

                if (newPlaybackRate != default(double))
                {
                    MediaPlugin.PlaybackRate = newPlaybackRate;
                }
            }
        }

        /// <summary>
        /// Decrements the rewinding rate if the media supports it.
        /// </summary>
        public void DecrementRewind()
        {
            if (IsRewinding && SupportsNaturalRewind)
            {
                double newPlaybackRate = RewindPlaybackRates
                    .Where(i => i > MediaPlugin.PlaybackRate)
                    .OrderBy(i => i)
                    .FirstOrDefault();

                if (newPlaybackRate != default(double))
                {
                    MediaPlugin.PlaybackRate = newPlaybackRate;
                }
            }
        }

        /// <summary>
        /// Begins slow motion playback if the media supports it.
        /// </summary>
        public void SlowMotion()
        {
            if (!IsSlowMotion && SupportsSlowMotion)
            {
                StopTimer();
                MediaPlugin.PlaybackRate = SlowMotionPlaybackRates.First();
            }
        }

        /// <summary>
        /// Sets the playback rate to normal speed.
        /// </summary>
        public void RestoreNaturalPlaySpeed()
        {
            if (!IsPlaySpeedNormal)
            {
                RestoreNaturalPlaySpeedWithoutNotification();
            }
        }

        private void RestoreNaturalPlaySpeedWithoutNotification()
        {
            StopTimer();
            _fakingFastForward = false;
            _fakingRewind = false;
            _mediaPlugin.PlaybackRate = NaturalPlaySpeed;
        }
    }
}