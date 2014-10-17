using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Diagnostics;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring
{
    /// <summary>
    /// Responsible for initializing the HealthMonitorLogger (the bridge between the diagnostic and logging components).
    /// Also captures additional higher-level events from SMF directly and passes them onto the logging component.
    /// Normally, this is created from within the SMF plugin (DiagnosticsPlugin) but could be used directly from code by itself.
    /// </summary>
    public class SMFLogger : IDisposable
    {
        const string MetadataKey = "Microsoft.SilverlightMediaFramework.Plugins.Monitoring.AdditionalData.";
        Uri DefaultConfigUri = new Uri("/Microsoft.SilverlightMediaFramework.Plugins.Monitoring;component/MonitoringConfig.xml", UriKind.Relative);
        SmoothStreamingMediaElement ssme;
        HealthMonitor healthMonitor;
        HealthMonitorLogger healthMonitorLogger;
        FeatureVisibility CaptionsVisibility;
        IEnumerable<MetadataItem> currentMetadataData = null;
        
        public SMFLogger(DiagnosticsConfig diagConfig, LoggingConfig loggingConfig)
        {
            DiagConfig = diagConfig;
            healthMonitorLogger = new HealthMonitorLogger(loggingConfig);
        }

        public SMFLogger()
        {
            // load a default config
            DiagConfig = DiagnosticsConfig.Load(DefaultConfigUri);
            healthMonitorLogger = new HealthMonitorLogger(new LoggingConfig());
        }

        /// <summary>
        /// Indicates that everything is attached (SSME, SMF, diagnostics, and logging)
        /// </summary>
        public bool IsAttached { get; private set; }

        /// <summary>
        /// The diagnostics configuration object to be used.
        /// </summary>
        public DiagnosticsConfig DiagConfig { get; private set; }

        /// <summary>
        /// Contains optional/additional information that should be attached to all logs before they are sent to the logging framework.
        /// </summary>
        public Dictionary<string, string> AdditionalLogData
        {
            get
            {
                return healthMonitorLogger.AdditionalLogData;
            }
            internal set
            {
                healthMonitorLogger.AdditionalLogData = value;
            }
        }

        /// <summary>
        /// Exposes the edge server that was detected by the diagnostic component. Refer to the diagnostic config for more information about detecting the edge server.
        /// </summary>
        public string EdgeServer
        {
            get
            {
                return healthMonitor == null ? null : healthMonitor.EdgeServer;
            }
        }

        /// <summary>
        /// Exposes the client IP that was detected by the diagnostic component as part of the edge server detection process. Refer to the diagnostic config for more information about detecting the edge server.
        /// </summary>
        public string ClientIp
        {
            get
            {
                return healthMonitor == null ? null : healthMonitor.ClientIP;
            }
        }

        /// <summary>
        /// Attaches the SMFPlayer so it can be monitored
        /// </summary>
        public void AttachToSMF(SMFPlayer smfPlayer)
        {
            smfPlayer.LogLevel = LogLevel.Information;
            CaptionsVisibility = smfPlayer.CaptionsVisibility;
            smfPlayer.CaptionsVisibilityChanged += smfPlayer_CaptionsVisibilityChanged;
            smfPlayer.AudioStreamChanged += smfPlayer_AudioStreamChanged;
            smfPlayer.RetryFailed += smfPlayer_RetryFailed;
            smfPlayer.RetrySuccessful += smfPlayer_RetrySuccessful;
            smfPlayer.PlayStateChanged += smfPlayer_PlayStateChanged;
            smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
#if PROGRAMMATICCOMPOSITION
            // get a list of logagents from the players generic plugins
            var logAgents = ((IPlayer)smfPlayer).Plugins.OfType<ILogAgent>().ToList();
            // merge the plugins with the existing ones
            LoggingService.Current.Agents = LoggingService.Current.Agents.Concat(logAgents).Distinct();
#endif
        }

        void smfPlayer_PlaylistItemChanged(object sender, CustomEventArgs<Core.Media.PlaylistItem> e)
        {
            if (currentMetadataData != null)
            {
                foreach (var item in currentMetadataData)
                {
                    var key = item.Key.Substring(MetadataKey.Length);
                    if (healthMonitorLogger.AdditionalLogData.ContainsKey(key))
                    {
                        healthMonitorLogger.AdditionalLogData.Remove(key);
                    }
                }
            }

            if (e.Value != null)
            {
#if VIDEOID
                healthMonitorLogger.VideoId = e.Value.MediaAssetId;
#endif

                IEnumerable<MetadataItem> newMetadataData = null;
                if (e.Value != null && e.Value.CustomMetadata != null)
                {
                    newMetadataData = e.Value.CustomMetadata.Where(m => m.Key.StartsWith(MetadataKey));
                    foreach (var item in newMetadataData)
                    {
                        var key = item.Key.Substring(MetadataKey.Length);
                        healthMonitorLogger.AdditionalLogData[key] = item.Value.ToString();
                    }
                }

                currentMetadataData = newMetadataData;
            }
            else 
            {
#if VIDEOID
                healthMonitorLogger.VideoId = null;
#endif
                currentMetadataData = null;
            }
        }

        void smfPlayer_RetrySuccessful(object sender, EventArgs e)
        {
            SMFPlayer smfPlayer = sender as SMFPlayer;
            var retrySucceededLog = new RetrySucceededLog();
            healthMonitorLogger.PopulateVideoEventLog(retrySucceededLog);
            retrySucceededLog.EdgeIP = healthMonitor.EdgeServer;
            retrySucceededLog.MediaElementId = ssme.Name;
            retrySucceededLog.IsLive = smfPlayer.IsPositionLive;
            LoggingService.Current.Log(retrySucceededLog);
        }

        void smfPlayer_RetryFailed(object sender, EventArgs e)
        {
            SMFPlayer smfPlayer = sender as SMFPlayer;
            var retryFailedLog = new RetryFailedLog();
            healthMonitorLogger.PopulateVideoEventLog(retryFailedLog);
            retryFailedLog.EdgeIP = healthMonitor.EdgeServer;
            retryFailedLog.MediaElementId = ssme.Name;
            retryFailedLog.IsLive = smfPlayer.IsPositionLive;
            LoggingService.Current.Log(retryFailedLog);
        }

        void smfPlayer_AudioStreamChanged(object sender, CustomEventArgs<Core.Media.StreamMetadata> e)
        {
            SMFPlayer smfPlayer = sender as SMFPlayer;
            var audioLog = new AudioTrackChangedLog(e.Value.Language);
            healthMonitorLogger.PopulateVideoEventLog(audioLog);
            audioLog.MediaElementId = ssme.Name;
            audioLog.IsLive = smfPlayer.IsPositionLive;
            LoggingService.Current.Log(audioLog);
        }

        void smfPlayer_CaptionsVisibilityChanged(object sender, EventArgs e)
        {
            SMFPlayer smfPlayer = sender as SMFPlayer;
            if (CaptionsVisibility != smfPlayer.CaptionsVisibility)
            {
                // only log when the state has changed, this weeds out the initial state
                CaptionsVisibility = smfPlayer.CaptionsVisibility;
                var ccLog = new ClosedCaptionChangedLog(smfPlayer.CaptionsVisibility == FeatureVisibility.Visible);
                healthMonitorLogger.PopulateSimpleVideoLog(ccLog);
                LoggingService.Current.Log(ccLog);
            }
        }

        /// <summary>
        /// Detaches the SMFPlayer so it no longer monitored.
        /// </summary>
        /// <param name="smfPlayer"></param>
        public void DetachFromSMF(SMFPlayer smfPlayer)
        {
            smfPlayer.CaptionsVisibilityChanged -= smfPlayer_CaptionsVisibilityChanged;
            smfPlayer.AudioStreamChanged -= smfPlayer_AudioStreamChanged;
            smfPlayer.RetryFailed -= smfPlayer_RetryFailed;
            smfPlayer.RetrySuccessful -= smfPlayer_RetrySuccessful;
            smfPlayer.PlayStateChanged -= smfPlayer_PlayStateChanged;
            smfPlayer.PlaylistItemChanged -= smfPlayer_PlaylistItemChanged;

            DetachFromMediaElement();
        }

        void smfPlayer_PlayStateChanged(object sender, CustomEventArgs<MediaPluginState> e)
        {
            SMFPlayer player = (SMFPlayer)sender;

            if (e.Value == MediaPluginState.Opening || e.Value == MediaPluginState.Playing)
            {
                var cc = FindMediaPresenter(player);
                if (cc != null)
                {
                    var mediaElement = cc.Content as SmoothStreamingMediaElement;
                    if (mediaElement != null && mediaElement != ssme)
                    {
                        AttachToMediaElement(mediaElement);
                    }
                }
            }
            else if ((e.Value == MediaPluginState.Stopped) && ssme != null)
            {
                if (IsAttached)
                    DetachFromMediaElement();
            }
        }

        /// <summary>
        /// Attaches the SmoothStreamingMediaElement to the diagnostic component so it can be monitored.
        /// It is recommended that you do not call this directly and instead use AttachToSMF
        /// </summary>
        /// <param name="element">An instance of SSME</param>
        internal void AttachToMediaElement(SmoothStreamingMediaElement element)
        {
            // clean up the old media element just in case
            if (IsAttached)
                DetachFromMediaElement();

            // start logging agent if it hasn't been already
            if (!LoggingService.Current.IsSessionStarted)
                LoggingService.Current.StartSession();

            ssme = element;
            ssme.Unloaded += ssme_Unloaded;

            healthMonitor = new HealthMonitor(DiagConfig);
            healthMonitor.Attach(ssme);
            healthMonitorLogger.AttachMonitor(healthMonitor);

            IsAttached = true;
        }

        /// <summary>
        /// Detaches the SmoothStreamingMediaElement from the diagnostic component.
        /// It is recommended that you do not call this directly and instead use DetachFromSMF
        /// </summary>
        internal void DetachFromMediaElement()
        {
            ssme.Unloaded -= ssme_Unloaded;
            if (healthMonitor != null) 
            {
                healthMonitor.Flush();
            }

            healthMonitorLogger.DetachMonitor();
            if (healthMonitor != null)
            {
                healthMonitor.Detach();
                healthMonitor = null;
            }
            ssme = null;
            IsAttached = false;
        }

        void ssme_Unloaded(object sender, RoutedEventArgs e)
        {
            DetachFromMediaElement();
        }

        /// <summary>
        /// Extracts the SSME from the SMFPlayer
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        ContentControl FindMediaPresenter(FrameworkElement element)
        {
            if (element == null) return null;
            if (element.Name == "MediaPresenterElement")
            {
                ContentControl tmp = element as ContentControl;
                return tmp;
            }
            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; i++)
            {
                FrameworkElement obj = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                var ele = FindMediaPresenter(obj);
                if (ele != null) return ele;
            }
            return null;
        }

        /// <summary>
        /// Allows you to programmatically create a custom video log. The log will be populated with certain basic video information.
        /// You still need to pass the log on to LoggingService.Current.Log(log);
        /// </summary>
        /// <param name="logType">A string value representing the type of your custom log.</param>
        /// <returns>The newly created log object.</returns>
        public SimpleVideoEventLog CreateVideoLog(string logType)
        {
            return healthMonitorLogger.CreateVideoLog(logType);
        }

        /// <summary>
        /// Allows you to programmatically create a custom video log and specify which video information should be included in the log.
        /// You still need to pass the log on to LoggingService.Current.Log(log);
        /// </summary>
        /// <param name="logType">A string value representing the type of your custom log.</param>
        /// <param name="includeVideoData">A bitwise enum used to control which information should be included with the custom log.</param>
        /// <returns>The newly created log object.</returns>
        public SimpleVideoEventLog CreateVideoLog(string logType, VideoDataEnum includeVideoData)
        {
            var result = healthMonitorLogger.CreateVideoLog(logType);
            if ((includeVideoData & VideoDataEnum.VideoSession) == VideoDataEnum.VideoSession && healthMonitor != null)
            {
                result.VideoSessionId = healthMonitor.VideoSessionId;
                result.VideoSessionDuration = healthMonitor.VideoSessionRunningTime;
            }
            if ((includeVideoData & VideoDataEnum.CurrentVideoPosition) == VideoDataEnum.CurrentVideoPosition && ssme != null)
                result.VideoPosition = ssme.Position;
            if ((includeVideoData & VideoDataEnum.MediaElementId) == VideoDataEnum.MediaElementId && ssme != null)
                result.MediaElementId = ssme.Name;
            return result;
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
                if (healthMonitor != null)
                    healthMonitor.Dispose();
            }
        }
    }

    [Flags]
    public enum VideoDataEnum
    {
        VideoSession = 0x01,
        CurrentVideoPosition = 0x02,
        MediaElementId = 0x04,
    }
}
