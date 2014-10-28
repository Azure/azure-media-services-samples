using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Xml.Extensions;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Heuristics
{
    /// <summary>
    /// Supports the ability to prioritize the resource consumption of multiple IAdaptiveMediaPlugin's running in the same application.
    /// </summary>
    [ExportMultiHeuristicsPlugin(PluginName = PluginName, PluginDescription = PluginDescription,
        PluginVersion = PluginVersion, SupportsSync = true)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportMetadata("SupportsMasterSlaveMode", "True")]
    public class PrioritizedHeuristicsPlugin : IHeuristicsPlugin
    {
        private const string PluginName = "MultiHeuristicsPlugin";

        private const string PluginDescription =
            "Supports the ability to prioritize the resource consumption of multiple IAdaptiveMediaPlugin's running in the same application.";

        private const string PluginVersion = "2.2012.0605.0";
        private const string HeuristicsSettingsFileName = "heuristics.xml";
        private const string PollingFrequencySettingName = "pollingfrequencymillis";
        private const string PrimaryMinBitrateSettingName = "primaryminbitrate";
        private const string PrimaryMinFrameRateSettingName = "primaryminframerate";
        private const string SecondaryMinBitrateSettingName = "secondaryminbitrate";
        private const string SecondaryMinFrameRateSettingName = "secondaryminframerate";
        private const string SyncToleranceMillisSettingName = "synctolerancemillis";

        private const int DefaultMaxReenableAttempts = 10;
        private const int DefaultPollingFrequencyMillis = 5000;
        private const long DefaultPrimaryMinBitrate = 0;
        private const int DefaultPrimaryMinFramerate = 0;
        private const long DefaultSecondaryMinBitrate = 0;
        private const int DefaultSecondaryMinFramerate = 0;
        private const int DefaultSyncToleranceMillis = 3000;

        private PrioritizedHeuristicsSettings _settings;
        private VideoSyncManager _syncManager;

        #region IHeuristicsPlugin Members

        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
#pragma warning disable 67
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Determines if this plugin supports management of the specified media plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin.</param>
        /// <returns>True if this media plugin is supported.</returns>
        public bool SupportsPlugin(IMediaPlugin mediaPlugin)
        {
            return mediaPlugin.VisualElement is SmoothStreamingMediaElement;
        }

        /// <summary>
        /// Registers the specified media plugin so that it can be managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to register.</param>
        /// <param name="priority">The relative priority of this media plugin</param>
        /// <param name="enableSync">Indicates if synchronization of this media plugin with others should be enabled.</param>
        public void RegisterPlugin(IMediaPlugin mediaPlugin, int priority, bool enableSync)
        {
            var ssme = mediaPlugin.VisualElement as SmoothStreamingMediaElement;

            if (IsLoaded && ssme != null)
            {
                if (priority == 0)
                {
                    _syncManager.HeuristicMasterMedia = ssme;
                    _syncManager.RegisterMaster(ssme, ssme.Position);
                    _syncManager.ResetHeuristics();
                }
                else
                {
                    var slave = new SlaveMediaElement
                                    {
                                        IsLogicalSyncEnabled = enableSync,
                                        MediaElement = ssme,
                                        Tolerance = TimeSpan.FromMilliseconds(_settings.SyncToleranceMillis)
                                    };

                    _syncManager.SubMediaElements.Add(slave);
                    if (_syncManager.HeuristicMasterMedia != null)
                    {
                        _syncManager.ResetHeuristics();
                    }
                }
            }
        }

        /// <summary>
        /// Unregisters the specified media plugin so that it is no longer managed by this heuristics plugin.
        /// </summary>
        /// <param name="mediaPlugin">The media plugin to unregister.</param>
        public void UnregisterPlugin(IMediaPlugin mediaPlugin)
        {
            var ssme = mediaPlugin.VisualElement as SmoothStreamingMediaElement;

            if (IsLoaded && ssme != null)
            {
                if (ssme == _syncManager.HeuristicMasterMedia)
                {
                    _syncManager.HeuristicManager.StopMonitoring();
                    _syncManager.HeuristicMasterMedia = null;
                }

                if (ssme == _syncManager.MainMedia)
                {
                    _syncManager.MainMedia = null;
                }

                SlaveMediaElement subMediaElement =
                    _syncManager.SubMediaElements.Where(i => i.MediaElement == ssme).FirstOrDefault();

                if (subMediaElement != null)
                {
                    _syncManager.SubMediaElements.Remove(subMediaElement);
                    _syncManager.ResetHeuristics();
                }
            }
        }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public void Load()
        {
            try
            {
                _settings = ReadSettings();
                _syncManager = new VideoSyncManager(_settings);
                IsLoaded = true;
                PluginLoaded.IfNotNull(i => i(this));
            }
            catch (Exception err)
            {
                PluginLoadFailed.IfNotNull(i => i(this, err));
            }
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public void Unload()
        {
            try
            {
                _syncManager.SuspendSync();
                _syncManager = null;
                IsLoaded = false;
                PluginUnloaded.IfNotNull(i => i(this));
            }
            catch (Exception err)
            {
                PluginUnloadFailed.IfNotNull(i => i(this, err));
            }
        }

        #endregion

        private static PrioritizedHeuristicsSettings ReadSettings()
        {
            var result = new PrioritizedHeuristicsSettings
            {
                MaxReenableAttempts = DefaultMaxReenableAttempts,
                PollingFrequencyMillis = DefaultPollingFrequencyMillis,
                PrimaryMinBitRate = DefaultPrimaryMinBitrate,
                PrimaryMinFrameRate =  DefaultPrimaryMinFramerate,
                SecondaryMinBitRate = DefaultSecondaryMinBitrate,
                SecondaryMinFrameRate = DefaultSecondaryMinFramerate,
                SyncToleranceMillis = DefaultSyncToleranceMillis
            };

            try
            {
                XDocument document = XDocument.Load(HeuristicsSettingsFileName);

                result.MaxReenableAttempts = 10;
                result.PollingFrequencyMillis = document.Root.Descendants(PollingFrequencySettingName)
                    .First()
                    .GetValueAsInt().Value;

                result.PrimaryMinBitRate = (ulong)document.Root.Descendants(PrimaryMinBitrateSettingName)
                                                        .First()
                                                        .GetValueAsLong().Value;

                result.PrimaryMinFrameRate = document.Root.Descendants(PrimaryMinFrameRateSettingName)
                    .First()
                    .GetValueAsInt().Value;

                result.SecondaryMinBitRate = (ulong)document.Root.Descendants(SecondaryMinBitrateSettingName)
                                                            .First()
                                                            .GetValueAsLong().Value;

                result.SecondaryMinFrameRate = document.Root.Descendants(SecondaryMinFrameRateSettingName)
                    .First()
                    .GetValueAsInt().Value;

                XElement syncToleranceMillisNode = document.Root.Descendants(SyncToleranceMillisSettingName)
                    .FirstOrDefault();

                if (syncToleranceMillisNode != null && syncToleranceMillisNode.GetValueAsInt().HasValue)
                {
                    result.SyncToleranceMillis = syncToleranceMillisNode.GetValueAsInt().Value;
                }
            }
            catch
            {
                //Eat errors and use defaults
            }


            return result;
        }
    }
}