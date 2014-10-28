using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising
{
    /// <summary>
    /// Retrieves MediaMarkers (triggers) using the IAB MAST specification.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The MastMarkerProvider can read the MAST format and return a collection of MediaMarker objects from it.
    /// A MastMarkerProvider has a reference to a PollingRequest object which determines the polling interval to be used, if any, for checking the source for new markers (polling is optional).
    /// </para>
    /// <para>
    /// This class has a collection of new markers and a collection of markers removed since the last request to check for markers. This check is done using the Marker Id property.
    /// </para>
    /// </remarks>
    public abstract class MastMarkerProviderBase : IMarkerProvider, IPlayerConsumer
    {
        private Mainsail Mainsail;
        private readonly Dictionary<IAdSequencingTrigger, MediaMarker> activeTriggers;
        protected string PluginLogName;

        public MastMarkerProviderBase()
        {
            activeTriggers = new Dictionary<IAdSequencingTrigger, MediaMarker>();

            SendLogEntry(LogEntryTypes.Instantiated, message: MastMarkerProviderResources.MastMarkerProviderInstantiatedLogMessage);
        }

        #region IMarkerProvider Members
        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when retrieving markers fails.
        /// </summary>
        public event Action<IMarkerProvider, Exception> RetrieveMarkersFailed;

        /// <summary>
        /// Occurs when markers have been removed.
        /// </summary>
        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> MarkersRemoved;

        /// <summary>
        /// Occurs when new markers are found.
        /// </summary>
        public event Action<IMarkerProvider, IEnumerable<MediaMarker>> NewMarkers;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets or sets the location where the markers are retrieved.
        /// </summary>
        public virtual Uri Source { get; set; }

        /// <summary>
        /// The time interval between checking for new and removed markers.
        /// </summary>
        public virtual TimeSpan? PollingInterval { get; set; }

        /// <summary>
        /// Begins the polling for retrieving markers.
        /// </summary>
        public virtual void BeginRetrievingMarkers()
        {
            player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = Visibility.Collapsed);
            // block the player from playing until we've had a chance to look for a pre-roll trigger. This is important to have immediate playback of pre-rolls without seeing the main video briefly
            BlockPlayer();
        }

        /// <summary>
        /// Stops polling for markers.
        /// </summary>
        public virtual void StopRetrievingMarkers()
        {
        }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public virtual void Load()
        {
            try
            {
                IsLoaded = true;

                SendLogEntry(LogEntryTypes.Loaded, message: MastMarkerProviderResources.MastMarkerProviderLoadedLogMessage);
                PluginLoaded.IfNotNull(i => i(this));
            }
            catch (Exception ex)
            {
                PluginLoadFailed.IfNotNull(i => i(this, ex));
            }
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public virtual void Unload()
        {
            try
            {
                Mainsail.ActivateTrigger -= new EventHandler<TriggerEventArgs>(Mainsail_ActivateTrigger);
                Mainsail.DeactivateTrigger -= new EventHandler<TriggerEventArgs>(Mainsail_DeactivateTrigger);
                Mainsail.TriggerEvaluationFailed -= new EventHandler<TriggerFailureEventArgs>(Mainsail_TriggerEvaluationFailed);
                Mainsail.Dispose();
                if (Mainsail.MastInterface is IDisposable) ((IDisposable)Mainsail.MastInterface).Dispose();
                Mainsail = null;
                Source = null;
                StopRetrievingMarkers();
                player = null;
                IsLoaded = false;
                PluginUnloaded.IfNotNull(i => i(this));
                SendLogEntry(LogEntryTypes.Unloaded, message: MastMarkerProviderResources.MastMarkerProviderUnloadedLogMessage);
            }
            catch (Exception ex)
            {
                PluginUnloadFailed.IfNotNull(i => i(this, ex));
            }
        }

        #endregion

        protected void LoadMastDoc(string mastData)
        {
            if (mastData != null)
            {
                try
                {
                    Mainsail.AddMastDoc(mastData);
                }
                catch (Exception ex)
                {
                    SendLogEntry(LogEntryTypes.ErrorOccurred, LogLevel.Warning, string.Format(MastMarkerProviderResources.ParseErrorLogMessage, ex.Message));
                    RetrieveMarkersFailed.IfNotNull(i => i(this, ex));
                }
            }
            // monitor the player for when the item starts
            Mainsail.MastInterface.OnItemStarting += MastInterface_OnItemStarting;
            Mainsail.MastInterface.OnItemStart += MastInterface_OnItemStart;
            Mainsail.MastInterface.OnError += MastInterface_OnError;
            // tell the player we are now monitoring it. This will fire off events if any occurred before we were listening
            Mainsail.MastInterface.Ready();
        }

        protected void SendLogEntry(string type,
                                  LogLevel severity = LogLevel.Information,
                                  string message = null,
                                  DateTime? timeStamp = null,
                                  IEnumerable<KeyValuePair<string, object>> extendedProperties = null)
        {
            if (LogReady != null)
            {
                var logEntry = new LogEntry
                {
                    Severity = severity,
                    Message = message,
                    SenderName = PluginLogName,
                    Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);
                LogReady(this, logEntry);
            }

        }

        protected IPlayer player;
        public void SetPlayer(FrameworkElement Player)
        {
            player = Player as IPlayer;

            var adapter = new MastAdapter(player);
            Mainsail = new Mainsail(adapter);
            Mainsail.ActivateTrigger += new EventHandler<TriggerEventArgs>(Mainsail_ActivateTrigger);
            Mainsail.DeactivateTrigger += new EventHandler<TriggerEventArgs>(Mainsail_DeactivateTrigger);
            Mainsail.TriggerEvaluationFailed += new EventHandler<TriggerFailureEventArgs>(Mainsail_TriggerEvaluationFailed);
        }

        void Mainsail_TriggerEvaluationFailed(object sender, TriggerFailureEventArgs e)
        {
            SendLogEntry(LogEntryTypes.TriggerEvaluationFailed, LogLevel.Error, string.Format(MastMarkerProviderResources.TriggerEvaluationFailed, e.Trigger.Id, e.Exception));
        }

        void MastInterface_OnError(object sender, EventArgs e)
        {
            Mainsail.MastInterface.OnItemStarting -= MastInterface_OnItemStarting;
            Mainsail.MastInterface.OnItemStart -= MastInterface_OnItemStart;
            Mainsail.MastInterface.OnError -= MastInterface_OnError;
            // make sure we show the mediaelement again.
            player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = Visibility.Visible);
            // the media has failed, if we were holding a playblock, release it
            ReleasePlayer();
        }

        private void MastInterface_OnItemStarting(object sender, EventArgs e)
        {
            Mainsail.MastInterface.OnItemStarting -= MastInterface_OnItemStarting;

            player.ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = Visibility.Visible);
        }

        private void MastInterface_OnItemStart(object sender, EventArgs e)
        {
            Mainsail.MastInterface.OnItemStart -= MastInterface_OnItemStart;
            Mainsail.MastInterface.OnError -= MastInterface_OnError;

            // the item has started, if we were holding a playblock, release it 
            ReleasePlayer();
        }

        private void Mainsail_ActivateTrigger(object sender, TriggerEventArgs e)
        {
            var t = e.Trigger;

            var marker = new AdMarker();
            marker.Id = t.Id;
            marker.Immediate = true;    // Being will get set automatically for us by setting Immediate to true
            marker.Begin = TimeSpan.Zero;
            marker.End = TimeSpan.FromDays(1);
            marker.Type = "Ad";
            marker.ScheduledAd = new ScheduledAd(t);
            marker.ScheduledAd.Deactivated += new EventHandler(AdMarkerContent_Deactivated);

            activeTriggers.Add(t, marker);

            this.NewMarkers(this, new[] { marker });

            SendLogEntry(LogEntryTypes.TriggerActivated, LogLevel.Information, string.Format(MastMarkerProviderResources.TriggerActivated, t.Id, t.Description));
        }

        /// <summary>
        /// An active payload has been deactivated (due to an error or finishing)
        /// </summary>
        private void AdMarkerContent_Deactivated(object sender, EventArgs e)
        {
            var adContent = sender as ScheduledAd;
            adContent.Deactivated -= AdMarkerContent_Deactivated;
            // pass on the deactivation to the mainsail, this will trigger the mainsail to raise the DeactiveTrigger event
            Mainsail.Deactivate(adContent.Trigger as Trigger);
        }

        /// <summary>
        /// The mainsail wants us to deactivate the trigger. This could be because the end condition was met OR we told it to deactivate the trigger
        /// </summary>
        private void Mainsail_DeactivateTrigger(object sender, TriggerEventArgs e)
        {
            var t = e.Trigger;

            if (activeTriggers.ContainsKey(t))
            {
                // tell the marker manager about it. This may have come from the marker manager in which case it will ignore
                var marker = activeTriggers[t];
                activeTriggers.Remove(t);
                this.MarkersRemoved(this, new[] { marker });

                SendLogEntry(LogEntryTypes.TriggerDeactivated, LogLevel.Information, string.Format(MastMarkerProviderResources.TriggerDeactivated, t.Id));
            }
        }

        protected void BlockPlayer()
        {
            player.AddPlayBlock(this);  // don't let someone else programmatically call play
        }

        protected void ReleasePlayer()
        {
            // release the play block (this will start the player again if a play operation was pending)
            player.ReleasePlayBlock(this);
        }

        protected void OnRetrieveMarkersFailed(Exception exception)
        {
            if (RetrieveMarkersFailed != null)
            {
                RetrieveMarkersFailed(this, exception);
            }
        }
    }
}
