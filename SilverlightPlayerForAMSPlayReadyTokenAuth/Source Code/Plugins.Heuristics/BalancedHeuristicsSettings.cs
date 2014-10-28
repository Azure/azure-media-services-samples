namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    /// <summary>
    /// Contains the settings information used to configure the BalancedHeuristicsPlugin
    /// </summary>
    public class BalancedHeuristicsSettings
    {
        private const int DefaultTolerableProblematicPlugins = 0;
        private const int DefaultTolerableSubOptimalTrackDifferences = 1;
        private const double DefaultTolerableDroppedFramesPercentage = .2;
        private const long DefaultPollingFrequencyMillis = 1000;
        private const long DefaultStableBufferSize = 20000;
        private const long DefaultBitrateBlackListDuration = 10000;

        public BalancedHeuristicsSettings()
        {
            TolerableProblematicPlugins = DefaultTolerableProblematicPlugins;
            TolerableSubOptimalTrackDifferences = DefaultTolerableSubOptimalTrackDifferences;
            TolerableDroppedFramesPercentage = DefaultTolerableDroppedFramesPercentage;
            PollingFrequencyMillis = DefaultPollingFrequencyMillis;
            StableBufferSize = DefaultStableBufferSize;
            BitrateBlackListDuration = DefaultBitrateBlackListDuration;
        }

        /// <summary>
        /// The number of problematic plugins that are tolerated before scaling back.
        /// </summary>
        public int TolerableProblematicPlugins { get; set; }

        /// <summary>
        /// The tolerable distance between a media plugin's currently playing track 
        /// and it's optimal track before the plugin is considered problematic.
        /// </summary>
        public int TolerableSubOptimalTrackDifferences { get; set; }

        /// <summary>
        /// The tolerable percentage of dropped frames until the media plugin is considered problematic.
        /// </summary>
        public double TolerableDroppedFramesPercentage { get; set; }

        /// <summary>
        /// The frequency with which the BalancedHeuristicsPlugin will reassess the it's registered media plugins.
        /// </summary>
        public long PollingFrequencyMillis { get; set; }

        /// <summary>
        /// The allowable buffer size for stable media plugins.
        /// </summary>
        public long StableBufferSize { get; set; }

        /// <summary>
        /// The length of time a bitrate should be blacklisted.
        /// </summary>
        public long BitrateBlackListDuration { get; set; }
    }
}