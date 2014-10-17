using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Diagnostics;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring
{
    /// <summary>
    /// This is the Silverlight Media Framework plugin used to log diagnostic information coming from SMF.
    /// It is a wrapper around SMFLogger (where the real work happens) in order to make it behave as SMF plugin.
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    public class DiagnosticsPlugin : IGenericPlugin, IDisposable
    {
        private const string PluginName = "DiagnosticsPlugin";
        private const string PluginDescription = "Supports the ability to analyze trace logs and other information to monitor the health of the player.";
        private const string PluginVersion = "2.2012.0605.0";
        
        public const string MetaDataItemId = "Microsoft.SilverlightMediaFramework.Logging.ConfigUri";
        
        private SMFLogger monitor;

        /// <summary>
        /// Returns the underlying object responsible for monitoring the player
        /// </summary>
        public SMFLogger Monitor 
        {
            get { return monitor; }
        }

        bool _isLoaded = false;

        #region IPlugin Members

        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        public void Load()
        {
            if (!IsLoaded)
            {
                // save the real work for when the player is set
                _isLoaded = true;
            }
        }

        public event Action<IPlugin, Plugins.Primitives.LogEntry> LogReady;
        protected void OnLogReady(IPlugin plugin, Plugins.Primitives.LogEntry entry)
        {
            if (LogReady != null)
                LogReady(plugin, entry);
        }

        public event Action<IPlugin, Exception> PluginLoadFailed;
        protected void OnPluginLoadFailed(IPlugin plugin, Exception ex)
        {
            if (PluginLoadFailed != null)
                PluginLoadFailed(plugin, ex);
        }

        public event Action<IPlugin> PluginLoaded;
        protected void OnPluginLoaded(IPlugin plugin)
        {
            if (PluginLoaded != null)
                PluginLoaded(plugin);
        }

        public event Action<IPlugin, Exception> PluginUnloadFailed;
        protected void OnPluginUnloadFailed(IPlugin plugin, Exception ex)
        {
            if (PluginUnloadFailed != null)
                PluginUnloadFailed(plugin, ex);
        }

        public event Action<IPlugin> PluginUnloaded;
        protected void OnPluginUnloaded(IPlugin plugin)
        {
            if (PluginUnloaded != null)
                PluginUnloaded(plugin);
        }

        public void Unload()
        {
            monitor = null;
            _isLoaded = false;
        }

        #endregion

        #region IGenericPlugin Members

        /// <summary>
        /// Initializes the SMFLogger and attaches the SMFPlayer to it.
        /// </summary>
        /// <param name="player">An instance of SMFPlayer</param>
        public void SetPlayer(FrameworkElement player)
        {
            if (!IsLoaded)
                Load();

            SMFPlayer smfPlayer = player as SMFPlayer;
            if (monitor == null)
            {
                var metadata = smfPlayer.GlobalConfigMetadata.FirstOrDefault(item => item.Key == MetaDataItemId);
                if (metadata != null)
                {
                    Uri configUri = new Uri(Convert.ToString(metadata.Value), UriKind.Relative);
                    LoadHealthMonitor(configUri);
                }
                else
                {
                    LoadHealthMonitor();
                }
            }

            monitor.AttachToSMF(smfPlayer);
        }

        #endregion

        /// <summary>
        /// Loads the SMFLogger with the default config settings
        /// </summary>
        void LoadHealthMonitor()
        {
            monitor = new SMFLogger(new DiagnosticsConfig(), new LoggingConfig());
        }

        /// <summary>
        /// Loads the SMFLogger from a local config file. Config file should use BuildAction=Content.
        /// </summary>
        /// <param name="configUri"></param>
        void LoadHealthMonitor(Uri configUri)
        {
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(configUri.OriginalString, xmlSettings))
            {
                MonitoringConfig config = MonitoringConfig.Load(reader);
                monitor = new SMFLogger(config.DiagnosticsConfig, config.LoggingConfig);
                monitor.AdditionalLogData = config.AdditionalData;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (monitor != null)
                    monitor.Dispose();
            }
        }

    }
}
