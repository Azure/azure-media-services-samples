namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    /// <summary>
    /// Contains the settings information used to configure the PrioritizedHeuristicsPlugin
    /// </summary>
    public class PrioritizedHeuristicsSettings
    {
        public PrioritizedHeuristicsSettings()
        {
            PollingFrequencyMillis = 2000;
            MaxReenableAttempts = 10;
            SyncToleranceMillis = 2000;
        }

        /// <summary>
        /// Gets the min bitrate of the primary SSME.
        /// </summary>
        public ulong PrimaryMinBitRate { get; set; }

        /// <summary>
        /// Gets the min framerate of the primary SSME.
        /// </summary>
        public int PrimaryMinFrameRate { get; set; }

        /// <summary>
        /// Gets the min bitrate of secondary SSME's.
        /// </summary>
        public ulong SecondaryMinBitRate { get; set; }

        /// <summary>
        /// Gets the min framerate of secondary SSME's.
        /// </summary>
        public int SecondaryMinFrameRate { get; set; }

        /// <summary>
        /// Gets the frequency with which the SSME's are reassessed.
        /// </summary>
        public int PollingFrequencyMillis { get; set; }

        /// <summary>
        /// Gets the max reenable attempts.
        /// </summary>
        public int MaxReenableAttempts { get; set; }

        /// <summary>
        /// Gets the sync tolerance in milliseconds.
        /// </summary>
        public int SyncToleranceMillis { get; set; }
    }
}