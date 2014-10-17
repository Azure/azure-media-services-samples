using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Xml.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    /// <summary>
    /// Reads heuristics settings from heuristics.xml and manages BalancedPriorityGoupMonitors.
    /// </summary>
    public class BalancedHeuristicsManager
    {
        private const string HeuristicsSettingsFileName = "heuristics.xml";
        private const string PollingFrequencySettingName = "pollingfrequencymillis";
        private const string TolerableProblematicPluginsSettingName = "tolerableproblematicplugins";
        private const string TolerableSubOptimalTrackDifferencesSettingName = "tolerablesuboptimaltrackdifferences";
        private const string TolerableDroppedFramesPercentageSettingName = "tolerabledroppedframespercentage";
        private const string StableBufferSizeSettingName = "stablebuffersize";
        private const string BitrateBlackListDurationSettingName = "bitrateblacklistduration";

        private readonly IList<BalancedPriorityGroupMonitor> _priorityGroups;

        private BalancedHeuristicsSettings _settings;

        public BalancedHeuristicsManager()
        {
            _priorityGroups = new List<BalancedPriorityGroupMonitor>();
        }

        /// <summary>
        /// Registers the plugin so that it can be managed.
        /// </summary>
        /// <param name="plugin">The plugin to register.</param>
        /// <param name="priority">The priority of the specified plugin.</param>
        public void RegisterPlugin(IAdaptiveMediaPlugin plugin, int priority)
        {
            if (_settings == null)
            {
                _settings = ReadSettings();
            }

            BalancedPriorityGroupMonitor priorityGroup = _priorityGroups.Where(i => i.Priority == priority)
                .FirstOrDefault();

            if (priorityGroup == null)
            {
                priorityGroup = new BalancedPriorityGroupMonitor(priority, _settings);
                priorityGroup.AllowBitrateIncreases = true;
                _priorityGroups.Add(priorityGroup);
            }

            priorityGroup.RegisterPlugin(plugin);
        }

        /// <summary>
        /// Unregisters a plugin so that it is no longer managed.
        /// </summary>
        /// <param name="plugin"></param>
        public void UnregisterPlugin(IAdaptiveMediaPlugin plugin)
        {
            _priorityGroups.Where(i => i.IsPluginRegistered(plugin))
                .FirstOrDefault()
                .IfNotNull(i => i.UnregisterPlugin(plugin));
        }


        private static BalancedHeuristicsSettings ReadSettings()
        {
            var result = new BalancedHeuristicsSettings();

            try
            {
                XDocument document = XDocument.Load(HeuristicsSettingsFileName);
                result.PollingFrequencyMillis = document.Root.Descendants(PollingFrequencySettingName)
                    .First()
                    .GetValueAsLong().Value;

                result.TolerableProblematicPlugins = document.Root.Descendants(TolerableProblematicPluginsSettingName)
                    .First()
                    .GetValueAsInt().Value;

                result.TolerableSubOptimalTrackDifferences =
                    document.Root.Descendants(TolerableSubOptimalTrackDifferencesSettingName)
                        .First()
                        .GetValueAsInt().Value;

                result.BitrateBlackListDuration = document.Root.Descendants(BitrateBlackListDurationSettingName)
                    .First()
                    .GetValueAsLong().Value;

                result.StableBufferSize = document.Root.Descendants(StableBufferSizeSettingName)
                    .First()
                    .GetValueAsLong().Value;

                result.TolerableDroppedFramesPercentage =
                    document.Root.Descendants(TolerableDroppedFramesPercentageSettingName)
                        .First()
                        .GetValueAsDouble().Value;
            }
            catch (Exception)
            {
                result = new BalancedHeuristicsSettings();
            }


            return result;
        }
    }
}