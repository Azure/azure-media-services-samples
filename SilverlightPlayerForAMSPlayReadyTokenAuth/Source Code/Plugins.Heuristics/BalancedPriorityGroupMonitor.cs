using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    public class BalancedPriorityGroupMonitor
    {
        private const int TolerableProblematicPlugins = 0;
        private const int TolerableSubOptimalTrackDifferences = 1;
        private const double TolerableDroppedFramesPercentage = .2;
        private const long MonitorFrequencyMillis = 1000;
        private const long StableBufferSize = 20000;
        private const long AvoidProblematicBitrateDurationMillis = 10000;

        private readonly TimeSpan _avoidProblematicBitrateDuration;
        private readonly DispatcherTimer _dispatcherTimer;
        private readonly IList<ProblematicTransitionRecord> _problematicTransitions;
        private readonly IList<IAdaptiveMediaPlugin> _registeredPlugins;
        private readonly TimeSpan _stableBufferSize;
        private long _maxAllowedBitrate;

        public BalancedPriorityGroupMonitor(int priority, BalancedHeuristicsSettings settings)
        {
            Priority = priority;
            _stableBufferSize = TimeSpan.FromMilliseconds(StableBufferSize);
            _avoidProblematicBitrateDuration = TimeSpan.FromMilliseconds(AvoidProblematicBitrateDurationMillis);
            _registeredPlugins = new List<IAdaptiveMediaPlugin>();
            _problematicTransitions = new List<ProblematicTransitionRecord>();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += (s, e) => MonitorPlugins();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(MonitorFrequencyMillis);
        }

        /// <summary>
        /// Gets or sets whether to allow bitrate increases.
        /// </summary>
        public bool AllowBitrateIncreases { get; set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        public int Priority { get; private set; }

        private long NextBitrate
        {
            get
            {
                return AvailableBitrates.Where(i => i > MaxAllowedBitrate)
                    .DefaultIfEmpty()
                    .Min();
            }
        }

        private bool IsReadyForBitrateIncrease
        {
            get
            {
                return _registeredPlugins.All(i => IsPluginReadyForBitrateIncrease(i, MaxAllowedBitrate))
                       && !IsBitrateProblematic(NextBitrate);
            }
        }

        private bool IsProblematic
        {
            get
            {
                return _registeredPlugins.Where(i => IsPluginProblematic(i, MaxAllowedBitrate)).Count() >
                       TolerableProblematicPlugins;
            }
        }

        /// <summary>
        /// Gets or sets the maximum allowed bitrate.
        /// </summary>
        public long MaxAllowedBitrate
        {
            get { return _maxAllowedBitrate; }
            private set
            {
                if (_maxAllowedBitrate != value
                    && value > 0
                    && (AllowBitrateIncreases || value < _maxAllowedBitrate))
                {
                    _maxAllowedBitrate = value;
                    _registeredPlugins.ForEach(i => i.SetVideoBitrateRange(0, _maxAllowedBitrate));
                }
            }
        }

        private IEnumerable<long> AvailableBitrates
        {
            get
            {
                return _registeredPlugins.Where(i => i.VideoDownloadTrack != null)
                    .SelectMany(i => i.VideoDownloadTrack.ParentStream.AvailableTracks)
                    .Select(i => i.Bitrate)
                    .Distinct()
                    .OrderBy(i => i);
            }
        }

        private IEnumerable<long> ActiveDownloadBitrates
        {
            get
            {
                return _registeredPlugins.Where(i => i.VideoDownloadTrack != null)
                    .Select(i => i.VideoDownloadTrack.Bitrate)
                    .Distinct()
                    .OrderBy(i => i);
            }
        }

        /// <summary>
        /// Registers a plugin so that it can be managed by this BalancedPriorityGroupMonitor.
        /// </summary>
        /// <param name="plugin">The plugin to register.</param>
        public void RegisterPlugin(IAdaptiveMediaPlugin plugin)
        {
            lock (_registeredPlugins)
            {
                if (!_registeredPlugins.Contains(plugin))
                {
                    _registeredPlugins.Add(plugin);
                }

                if (!_dispatcherTimer.IsEnabled)
                {
                    _dispatcherTimer.Start();
                }
            }
        }

        /// <summary>
        /// Unregisters a plugin so that it is no longer managed by this BalancedPriorityGroupMonitor.
        /// </summary>
        /// <param name="plugin">The plugin to unregister.</param>
        public void UnregisterPlugin(IAdaptiveMediaPlugin plugin)
        {
            lock (_registeredPlugins)
            {
                if (_registeredPlugins.Contains(plugin))
                {
                    _registeredPlugins.Remove(plugin);
                }

                if (_registeredPlugins.Count == 0 && _dispatcherTimer.IsEnabled)
                {
                    _dispatcherTimer.Stop();
                }
            }
        }

        /// <summary>
        /// Gets whether the specified plugin is registered with this BalancedPriorityGroupMonitor.
        /// </summary>
        /// <param name="plugin">The plugin to check for.</param>
        /// <returns>True indicates that the specified plugin is registered with this BalancedPriorityGroupMonitor.</returns>
        public bool IsPluginRegistered(IAdaptiveMediaPlugin plugin)
        {
            return _registeredPlugins.Contains(plugin);
        }

        private void MonitorPlugins()
        {
            lock (_registeredPlugins)
            {
                if (MaxAllowedBitrate == 0)
                {
                    MaxAllowedBitrate = AvailableBitrates.DefaultIfEmpty().Min();
                }
                else if (IsReadyForBitrateIncrease)
                {
                    Debug.WriteLine("Ready For Bitrate Increase");
                    MaxAllowedBitrate = NextBitrate;
                }
                else if (IsProblematic)
                {
                    Debug.WriteLine("Problematic Plugins Found");
                    var record = new ProblematicTransitionRecord
                                     {
                                         Bitrate = MaxAllowedBitrate,
                                         Time = DateTime.Now
                                     };
                    _problematicTransitions.Add(record);
                    MaxAllowedBitrate = ActiveDownloadBitrates.DefaultIfEmpty()
                        .Min();
                }
                else
                {
                    Debug.WriteLine("Stable");
                    _registeredPlugins.ForEach(i => i.BufferingTime = _stableBufferSize);
                }
            }
        }


        private bool IsBitrateProblematic(long bitrate)
        {
            TimeSpan timeSinceLastAttempt = TimeSpan.Zero;
            bool hasBeenProblematic = _problematicTransitions.Where(i => i.Bitrate == bitrate)
                .Any();

            if (hasBeenProblematic)
            {
                DateTime problematicOccurence = _problematicTransitions.Where(i => i.Bitrate == bitrate)
                    .DefaultIfEmpty()
                    .Max(i => i.Time);

                timeSinceLastAttempt = DateTime.Now.Subtract(problematicOccurence);
            }

            bool result = hasBeenProblematic && timeSinceLastAttempt < _avoidProblematicBitrateDuration;
            if (result) Debug.WriteLine("Bitrate Found To Be Problematic: {0}", bitrate);

            return hasBeenProblematic && timeSinceLastAttempt < _avoidProblematicBitrateDuration;
        }

        private static bool IsPluginProblematic(IAdaptiveMediaPlugin plugin, long maxAllowedBitrate)
        {
            double droppedFramePercentage = plugin.DroppedFramesPerSecond/
                                            (plugin.DroppedFramesPerSecond + plugin.RenderedFramesPerSecond);

            int subObtimalTrackDifference = plugin.VideoDownloadTrack != null
                                                ? plugin.VideoDownloadTrack.ParentStream.AvailableTracks
                                                      .Where(i => i.Bitrate > plugin.VideoDownloadTrack.Bitrate
                                                                  && i.Bitrate <= maxAllowedBitrate)
                                                      .Count()
                                                : 0;


            if (plugin.VideoDownloadTrack != null)
            {
                Debug.WriteLine(
                    "SOTD: {0} VideoDownloadTrack.Bitrate: {1} MaxAllowedBitrate: {2} Dropped Frame Percentage: {3}",
                    subObtimalTrackDifference, plugin.VideoDownloadTrack.Bitrate, maxAllowedBitrate,
                    droppedFramePercentage);
            }

            return subObtimalTrackDifference > TolerableSubOptimalTrackDifferences ||
                   droppedFramePercentage > TolerableDroppedFramesPercentage;
        }

        private static bool IsPluginReadyForBitrateIncrease(IAdaptiveMediaPlugin plugin, long maxAllowedBitrate)
        {
            double droppedFramePercentage = plugin.DroppedFramesPerSecond/
                                            (plugin.DroppedFramesPerSecond + plugin.RenderedFramesPerSecond);
            long highestPossibleBitrate = plugin.VideoDownloadTrack.ParentStream.SelectedTracks
                .Select(i => i.Bitrate)
                .Where(i => i <= maxAllowedBitrate)
                .DefaultIfEmpty()
                .Max();

            long currentPlaybackBitrate = plugin.VideoPlaybackTrack != null
                                              ? plugin.VideoPlaybackTrack.Bitrate
                                              : 0;

            return currentPlaybackBitrate == highestPossibleBitrate
                   && droppedFramePercentage <= TolerableDroppedFramesPercentage;
        }

        private static void SetMaxBitrate(long maxBitrate, IEnumerable<IAdaptiveMediaPlugin> plugins)
        {
            Debug.WriteLine("Set MaxBitrate: {0}", maxBitrate);
            plugins.ForEach(i => i.SetVideoBitrateRange(0, maxBitrate));
        }

        #region Nested type: ProblematicTransitionRecord

        private struct ProblematicTransitionRecord
        {
            public long Bitrate { get; set; }
            public DateTime Time { get; set; }
        }

        #endregion
    }
}