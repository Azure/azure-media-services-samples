using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Core.Logging;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Xml.Extensions;
using System.ComponentModel.Composition.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Converters;
#if !WINDOWS_PHONE && !OOB
using Microsoft.SilverlightMediaFramework.Core.Offline;
using Microsoft.SilverlightMediaFramework.Utilities.Offline;
#endif

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents the area where video is displayed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Player uses a plug-in architecture so that the type of plug-in required to play a media clip (item in a Playlist) 
    /// determines which plug-in is selected. For example, for a progressive download media clip, the Player will load a 
    /// ProgressiveMediaPlugIn.
    /// </para>
    /// <para>
    /// The Player has child controls including the <see cref="T:Microsoft.SilverlightMediaFramework.Core.BitrateMonitor">BitrateMonitor</see>, 
    /// <see cref="T:Microsoft.SilverlightMediaFramework.Core.Timeline">Timeline</see>, 
    /// and <see cref="T:Microsoft.SilverlightMediaFramework.Core.VolumeControl">VolumeControl</see> controls. These controls and the control strip (Play/Pause, FastForward, Rewind, and Stop buttons) 
    /// and Playlist are displayed on the Player.
    /// </para>
    /// <para>
    /// The Player has boolean properties you can check (for example, IsFullScreen, IsPlaylistVisible, IsAdPlaying) to determine the state of the Player.
    /// </para>
    /// <para>
    /// There are several named parts (TemplateParts) defined for the Player. These allow a designer to completely redefine the appearance of the visual child controls of the Player with a completely customized appearance.
    /// Typically a designer will use a tool such as Expression Blend to define the look of the Player and its child controls.
    /// </para>
    /// <para>
    /// Many visual states are defined for the Player that allow a designer to customize how the player will look in many different states, 
    /// such as when an ad is playing or not playing. 
    /// </para>
    /// <para>
    /// Events occur when certain things happen in the Player, such as PlayStateChanged, MediaOpened, MediaFailed, and MarkerReached. 
    /// You can write event handlers to do something when one of these events occur.
    /// </para>
    /// <para>
    ///  Many public methods have a <c>[ScriptableMember]</c> attribute allowing you to write code
    ///  in JavaScript to control the player (for example, you can call the Play or Pause method).
    ///  Properties decorated with a <c>[ScriptableMember]</c> attribute
    ///  allow you to get the state of the Player from 
    ///  JavaScript code. Many of these properties have private setters, but some properties allow full read/write access, 
    ///  such as the Playlist property which you can use to access the current Playlist and its items or to set the property value to a new Playlist from
    ///  your JavaScript code. 
    /// </para>
    /// </remarks>
    public partial class SMFPlayer : Control, IDisposable
    {
        private const long DefaultPositionLiveBufferMillis = 3000;
        private const long DefaultMaximumBitrate = 1500000;
        private const long DefaultMaximumCaptionSeekSearchWindowMillis = 60000; //1 minutes
        private const double DefaultVolume = .5;

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamSubTypes =
            new ReadOnlyCollection<string>(new List<string> { "CAPT", "SUBT", "SMPTE-TT-608" });

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamFourCCValues =
            new ReadOnlyCollection<string>(new List<string> { "TTML", "DFXP" });

        private readonly Logger _logger;
        private readonly DispatcherTimer _positionTimer;
        private readonly DoubleClickMonitor _videoDoubleClickMonitor;
        private readonly AdMarkerManager _adMarkerManager;
        private readonly TimelineMarkerManager _timelineMarkerManager;
        private readonly MediaMarkerManager<CaptionRegion> _captionManager;
#if !WINDOWS_PHONE && !OOB
        private readonly OfflineManager _offlineManager;
#endif
        private IMediaPlugin _activeMediaPlugin;
        private PlaylistItem _firstRetryItem;
        private bool _isTemplateApplied;
        private List<IMarkerProvider> _markerProviders;
        private RetryMonitor _retryMonitor;
        private IList<FrameworkElement> containers = new List<FrameworkElement>();
        private bool isSeekActive = false;
        private bool retryPending = false;


        #region Events

        /// <summary>
        /// Indicates that the timeline has changed. You can drive period tests on this event to avoid creating multiple timers
        /// </summary>
        public event EventHandler TimelineChanged;

        /// <summary>
        /// TimelineMarkers collection has changed
        /// </summary>
        public event EventHandler TimelineMarkersChanged;

        /// <summary>
        /// Chapters collection has changed
        /// </summary>
        public event EventHandler ChaptersChanged;

        /// <summary>
        /// Raised immediately after the MediaPlugin has been selected and created. Used for advanced purposes only where applications need access to the underlying MediaPlugin. In most cases, you should not use this event.
        /// </summary>
        public event EventHandler<CustomEventArgs<IMediaPlugin>> MediaPluginRegistered;

        /// <summary>
        /// Occurs when AvailableAudioStreams changes.
        /// </summary>
        public event EventHandler AvailableAudioStreamsChanged;

        /// <summary>
        /// Occurs when CaptionsVisibility changes.
        /// </summary>
        public event EventHandler CaptionsVisibilityChanged;

        /// <summary>
        /// Occurs when loading plugins has completed.
        /// </summary>
        public event EventHandler AddExternalPluginsCompleted;

        /// <summary>
        /// Occurs when loading plugins has failed.
        /// </summary>
        public event EventHandler<CustomEventArgs<Exception>> AddExternalPluginsFailed;

        /// <summary>
        /// Occurs when the progress changes on the download of an external package.
        /// </summary>
        public event EventHandler<ExternalPackageDownloadProgressInfo> AddExternalPackageDownloadProgressChanged;

        /// <summary>
        /// Occurs when the percent of the media being buffered changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<double>> BufferingProgressChanged;

        /// <summary>
        /// Occurs when the current playback position of this Player changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> PlaybackPositionChanged;

        /// <summary>
        /// Occurs when the state of this Player changes.
        /// </summary>
        /// <remarks>
        /// The PlayState enumeration indicates the current state of the Player.
        /// </remarks>
        public event EventHandler<CustomEventArgs<MediaPluginState>> PlayStateChanged;

        /// <summary>
        /// Occurs when the play speed of this Player changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<PlaySpeedState>> PlaySpeedStateChanged;

        /// <summary>
        /// Occurs when a caption region is reached.
        /// </summary>
        public event EventHandler<CustomEventArgs<CaptionRegion>> CaptionReached;

        /// <summary>
        /// Occurs when a caption region is left.
        /// </summary>
        public event EventHandler<CustomEventArgs<CaptionRegion>> CaptionLeft;

        /// <summary>
        /// Occurs when a marker is reached.
        /// </summary>
        public event EventHandler<TimelineMarkerReachedInfo> TimelineMarkerReached;

        /// <summary>
        /// Occurs when a marker is left.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimelineMediaMarker>> TimelineMarkerLeft;

        /// <summary>
        /// Occurs when a marker is skipped.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimelineMediaMarker>> TimelineMarkerSkipped;

#if !WINDOWS_PHONE && !FULLSCREEN
        /// <summary>
        /// Occurs when full screen changes.
        /// </summary>
        public event EventHandler FullScreenChanged;
#endif

        /// <summary>
        /// Occurs when the Playlist changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<IList<PlaylistItem>>> PlaylistChanged;

        /// <summary>
        /// Occurs when the Playlist changes.
        /// </summary>
        /// <remarks>
        /// The current PlaylistItem is passed to the event handler.  
        /// </remarks>
        public event EventHandler<CustomEventArgs<PlaylistItem>> PlaylistItemChanged;

        /// <summary>
        /// Occurs when the Playlist reaches the end. This does not occur when ContinuousPlay == true
        /// </summary>
        /// <remarks>
        /// This will fire anytime we attempt to play a PlaylistItem index greater than the total number of playlist items.
        /// </remarks>
        public event EventHandler PlaylistReachedEnd;

        /// <summary>
        /// Occurs when media fails.
        /// </summary>
        public event EventHandler<CustomEventArgs<Exception>> MediaFailed;

        /// <summary>
        /// Occurs when media ends.
        /// </summary>
        public event EventHandler MediaEnded;

        /// <summary>
        /// Occurs when media has opened.
        /// </summary>
        public event EventHandler MediaOpened;

        /// <summary>
        /// Occurs when the user starts changing the timeline position.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> ScrubbingStarted;

        /// <summary>
        /// Occurs when the user is changing the timeline position.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> Scrubbing;

        /// <summary>
        /// Occurs when the user finishes changing the timeline position.
        /// </summary>
        public event EventHandler<CustomEventArgs<TimeSpan>> ScrubbingCompleted;

        /// <summary>
        /// Occurs when the user has completed seeking.
        /// </summary>
        public event EventHandler SeekCompleted;

        /// <summary>
        /// Occurs when the Player starts retrying.
        /// </summary>
        public event EventHandler RetryStarted;

        /// <summary>
        /// Occurs when retrying has successfully completed.
        /// </summary>
        public event EventHandler RetrySuccessful;

        /// <summary>
        /// Occurs when retrying has failed.
        /// </summary>
        public event EventHandler RetryFailed;

        /// <summary>
        /// Occurs when retrying has been cancelled.
        /// </summary>
        public event EventHandler RetryCancelled;

        /// <summary>
        /// Occurs when a retry attempt has failed.
        /// </summary>
        public event EventHandler<CustomEventArgs<Exception>> RetryAttemptFailed;

        /// <summary>
        /// Occurs when writing a Log Entry fails.
        /// </summary>
        /// <remarks>
        /// Plug-ins send logging events to the Player. This event allows an application to know if 
        /// an error occurs when writing to a LogWriter fails.
        /// </remarks>
        public event EventHandler<LogWriteErrorOccurredInfo> LogWriteErrorOccurred;

        /// <summary>
        /// Occurs when writing a Log Entry completes successfully.
        /// </summary>
        /// <remarks>
        /// Plug-ins send logging events to the Player. This event allows an application to know  
        /// when writing to a LogWriter succeeds.
        /// </remarks>
        public event EventHandler<CustomEventArgs<LogEntry>> LogWriteSuccessful;

        /// <summary>
        /// Occurs when a Log Entry has been received.
        /// </summary>
        public event EventHandler<CustomEventArgs<LogEntry>> LogEntryReceived;

        /// <summary>
        /// Occurs when data is received from a plugin.
        /// </summary>
        public event EventHandler<DataReceivedInfo> DataReceived;

        /// <summary>
        /// Occurs when the audio stream changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<StreamMetadata>> AudioStreamChanged;

        /// <summary>
        /// Occurs when the caption stream changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<StreamMetadata>> CaptionStreamChanged;

        /// <summary>
        /// Occurs when the volume level changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<double>> VolumeLevelChanged;

        /// <summary>
        /// Occurs when the player is muted or unmuted.
        /// </summary>
        public event EventHandler<CustomEventArgs<bool>> IsMutedChanged;

        /// <summary>
        /// Occurs when the bitrate of the media that is playing changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<long>> PlaybackBitrateChanged;

        /// <summary>
        /// Occurs when the bitrate of media that is downloading changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<long>> DownloadBitrateChanged;

        /// <summary>
        /// Occurs when the download progress of media that is downloading changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<double>> DownloadProgressChanged;

        /// <summary>
        /// Occurs when the duration of a live media exceeds the EstimatedLiveDuration.
        /// </summary>
        public event EventHandler EstimatedLiveDurationExceeded;

        /// <summary>
        /// Occurs when the progress of an advertisement changes.
        /// </summary>
        public event EventHandler<AdvertisementProgressChangedInfo> AdvertisementProgressChanged;

        /// <summary>
        /// Occurs when the state of an advertisement changes.
        /// </summary>
        public event EventHandler<AdvertisementStateChangedInfo> AdvertisementStateChanged;

        /// <summary>
        /// Occurs when there is an error playing an advertisement.
        /// </summary>
        public event EventHandler<CustomEventArgs<IAdContext>> AdvertisementError;

        /// <summary>
        /// Occurs when a user clicks on an advertisement.
        /// </summary>
        public event EventHandler<CustomEventArgs<IAdContext>> AdvertisementClickThrough;

        #endregion

        #region Properties

        TimeSpan positionUpdateInterval = TimeSpan.FromMilliseconds(250);

        /// <summary>
        /// Gets/sets the interval used for checking markers and updating the timeline
        /// </summary>
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        [Category("Player Controls")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public TimeSpan PositionUpdateInterval
        {
            get { return positionUpdateInterval; }
            set
            {
                positionUpdateInterval = value;
                _positionTimer.Interval = positionUpdateInterval;
            }
        }

        public IList<FrameworkElement> Containers { get { return containers; } }

        public Panel VideoArea { get { return this.VideoAreaElement; } }

        protected PluginsManager PluginsManager { get; private set; }

        protected PlaySpeedManager PlaySpeedManager { get; private set; }

        protected IHeuristicsPlugin ActiveHeuristicsPlugin { get; private set; }

        private bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(this); }
        }

        protected List<IMarkerProvider> MarkerProviders
        {
            get { return _markerProviders; }
            private set
            {

                _markerProviders.IfNotNull(i =>
                {
                    foreach (IMarkerProvider p in _markerProviders)
                    {
                        UnregisterMarkerProvider(p);
                    }
                });
                _markerProviders = value;
                _markerProviders.IfNotNull(i =>
                {
                    foreach (IMarkerProvider p in _markerProviders)
                    {
                        RegisterMarkerProvider(p);
                        // supports injecting the plugin with the player (if it implements the IPlayerConsumer interface)
                        p.IfType<IPlayerConsumer>(pc => pc.SetPlayer(this));
                        p.Load();
                    }
                });
            }
        }

        internal protected IMediaPlugin ActiveMediaPlugin
        {
            get { return _activeMediaPlugin; }

            private set
            {
                _activeMediaPlugin.IfNotNull(UnregisterMediaPlugin);
                _activeMediaPlugin = value;
                _activeMediaPlugin.IfNotNull(RegisterMediaPlugin);
            }
        }

        protected TimeSpan RelativeMediaPluginPosition
        {
            get
            {
                return ActiveMediaPlugin != null
                           ? CalculateRelativeMediaPosition(ActiveMediaPlugin.Position)
                           : TimeSpan.Zero;
            }
        }

        protected Logger Logger
        {
            get { return _logger; }
        }

        internal protected IAdaptiveMediaPlugin ActiveAdaptiveMediaPlugin
        {
            get { return _activeMediaPlugin as IAdaptiveMediaPlugin; }
        }

        protected ILiveDvrMediaPlugin ActiveLiveMediaPlugin
        {
            get { return _activeMediaPlugin as ILiveDvrMediaPlugin; }
        }

        private bool IsAdaptiveMediaSupported
        {
            get { return _activeMediaPlugin is IAdaptiveMediaPlugin; }
        }

        private bool IsLiveMediaSupported
        {
            get { return _activeMediaPlugin is ILiveDvrMediaPlugin; }
        }

        private bool IsMediaPluginPlayReady
        {
            get
            {
                return ActiveMediaPlugin != null
                       && (ActiveMediaPlugin.CurrentState == MediaPluginState.Buffering
                           || ActiveMediaPlugin.CurrentState == MediaPluginState.Paused
                           || ActiveMediaPlugin.CurrentState == MediaPluginState.Playing
                           || ActiveMediaPlugin.CurrentState == MediaPluginState.ClipPlaying
                           );
            }
        }

        /// <summary>
        /// Gets the current index of the current Playlist Item state.
        /// </summary>
        /// <remarks>
        /// Returns null if there is no current Playlist.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? CurrentPlaylistIndex
        {
            get
            {
                int? index = null;

                if (Playlist != null && CurrentPlaylistItem != null)
                {
                    index = Playlist.IndexOf(CurrentPlaylistItem);
                }

                return index;
            }
        }

        #endregion

        public SMFPlayer()
        {
            DefaultStyleKey = typeof(SMFPlayer);

            _logger = new Logger();
            _videoDoubleClickMonitor = new DoubleClickMonitor();
            _positionTimer = new DispatcherTimer();

            Playlist = new ObservableCollection<PlaylistItem>();
            HeuristicsPluginRequiredMetadata = new MetadataCollection();
            GlobalConfigMetadata = new MetadataCollection();

            _captionManager = new MediaMarkerManager<CaptionRegion>
            {
                SeekingSearchWindow = TimeSpan.FromMilliseconds(DefaultMaximumCaptionSeekSearchWindowMillis)
            };
            _captionManager.MarkerLeft += CaptionManager_MarkerLeft;
            _captionManager.MarkerReached += CaptionManager_MarkerReached;

            _timelineMarkerManager = new TimelineMarkerManager();
            _timelineMarkerManager.MarkerLeft += MarkerManager_MarkerLeft;
            _timelineMarkerManager.MarkerReached += MarkerManager_MarkerReached;
            _timelineMarkerManager.MarkerSkipped += MarkerManager_MarkerSkipped;

            _adMarkerManager = new AdMarkerManager(this);
            _adMarkerManager.MarkerLeft += AdManager_MarkerLeft;
            _adMarkerManager.MarkerReached += AdManager_MarkerReached;
            _adMarkerManager.MarkerSkipped += AdManager_MarkerSkipped;

            if (!IsInDesignMode)
            {
#if !WINDOWS_PHONE && !OOB
                _offlineManager = new OfflineManager();
#endif
                PluginsManager = new PluginsManager();
                _logger.LogWriteSuccessful += Logger_LogWriteSuccessful;
                _logger.LogWriteErrorOccurred += Logger_LogWriteErrorOccurred;

                _videoDoubleClickMonitor.ElementDoubleClicked += (s, e) => MediaPresenterElement_DoubleClicked();
                _positionTimer.Interval = PositionUpdateInterval;
                _positionTimer.Tick += (s, e) => UpdateTimelinePositions();

#if !PROGRAMMATICCOMPOSITION
                PluginsManager.AddExternalPackageDownloadProgressChanged += PluginsManager_AddExternalPackageDownloadProgressChanged;
                PluginsManager.AddExternalPackageCompleted += PluginsManager_AddExternalPackageCompleted;
                PluginsManager.AddExternalPackageFailed += PluginsManager_AddExternalPackageFailed;
#endif

#if !WINDOWS_PHONE && !FULLSCREEN
                IsFullScreen = Application.Current.Host.Content.IsFullScreen;
                Application.Current.Host.Content.FullScreenChanged += Application_FullScreenChanged;
#endif
            }
        }

        #region Private Methods

        private void MediaPresenterElement_DoubleClicked()
        {
            if (AllowDoubleClickToggle)
            {
                IsFullScreen = !IsFullScreen;
            }
        }

        private void LoadLoggingPlugins(IEnumerable<string> logWriterIds)
        {
            foreach (string logWriterId in logWriterIds)
            {
                ILogWriter logWriter = SelectLogWriter(logWriterId);

                if (logWriter != null && !logWriter.IsLoaded)
                {
                    logWriter.PluginLoadFailed += Plugin_PluginLoadFailed;
                    _logger.RegisterLogWriter(logWriter);
                    logWriter.PluginLoadFailed -= Plugin_PluginLoadFailed;
                }
            }

            SendLogEntry(KnownLogEntryTypes.PluginLoaded, LogLevel.Information, SilverlightMediaFrameworkResources.LogWritersLoadedLogMessage);
        }

        private void LoadUIPlugins()
        {
            IEnumerable<IUIPlugin> plugins = PluginsManager.PresentationPlugins
                .Where(i => !i.IsValueCreated)
                .Select(i => i.Value);

            foreach (IUIPlugin plugin in plugins)
            {
                plugin.PluginLoaded += UIPlugin_PluginLoaded;
                plugin.PluginUnloaded += UIPlugin_PluginUnloaded;
                plugin.PluginLoadFailed += Plugin_PluginLoadFailed;
                plugin.LogReady += Plugin_LogReady;
                plugin.Load();
            }

            SendLogEntry(KnownLogEntryTypes.PluginLoaded, LogLevel.Information, SilverlightMediaFrameworkResources.UIPluginsLoadedLogMessage);
        }

        private void UnloadUIPlugins()
        {
            IEnumerable<IUIPlugin> plugins = PluginsManager.PresentationPlugins.Select(i => i.Value).Where(v => v.IsLoaded);

            foreach (IUIPlugin plugin in plugins)
            {
                plugin.PluginLoaded -= UIPlugin_PluginLoaded;
                plugin.PluginLoadFailed -= Plugin_PluginLoadFailed;
                plugin.Unload();
                plugin.PluginUnloaded -= UIPlugin_PluginUnloaded;
                plugin.LogReady -= Plugin_LogReady;
            }
        }

        private void LoadGenericPlugins()
        {
            IEnumerable<IGenericPlugin> plugins = PluginsManager.GenericPlugins
                .Where(i => !i.IsValueCreated)
                .Select(i => i.Value);

            foreach (IGenericPlugin plugin in plugins)
            {
                plugin.PluginLoadFailed += Plugin_PluginLoadFailed;
                plugin.LogReady += Plugin_LogReady;
                plugin.SetPlayer(this);
                plugin.Load();
            }

            SendLogEntry(KnownLogEntryTypes.PluginLoaded, LogLevel.Information, SilverlightMediaFrameworkResources.GenericPluginsLoadedLogMessage);
        }

        private void UnloadGenericPlugins()
        {
            IEnumerable<IGenericPlugin> plugins = PluginsManager.GenericPlugins.Select(i => i.Value).Where(v => v.IsLoaded);

            foreach (IGenericPlugin plugin in plugins)
            {
                plugin.PluginLoadFailed -= Plugin_PluginLoadFailed;
                plugin.Unload();
                plugin.LogReady -= Plugin_LogReady;
            }
        }

        private void LoadAdPayloadHandlerPlugins()
        {
            IEnumerable<IAdPayloadHandlerPlugin> plugins = PluginsManager.AdPayloadHandlerPlugins
                .Where(i => !i.IsValueCreated)
                .Select(i => i.Value).ToList();

            foreach (IAdPayloadHandlerPlugin plugin in plugins)
            {
                plugin.PluginLoadFailed += Plugin_PluginLoadFailed;
                plugin.LogReady += Plugin_LogReady;
                plugin.SetPlayer(this);
                plugin.Load();
                plugin.InitTimeout = adInitTimeout;
                plugin.StartTimeout = adStartTimeout;
                plugin.StopTimeout = adStopTimeout;
                plugin.FailurePolicy = adFailurePolicy;
                plugin.CloseCompanionsOnComplete = adCloseCompanionsOnComplete;
                plugin.HandleCompleted += AdPayloadHandlerPlugins_HandleCompleted;
            }

            IEnumerable<LooseMetadataLazy<IAdPayloadHandlerPlugin, IAdPayloadHandlerMetadata>> results = PluginsManager.AdPayloadHandlerPlugins;

            //_adMarkerManager.PayloadHandlers = plugins.ToList();
            _adMarkerManager.PayloadHandlers = results;

            SendLogEntry(KnownLogEntryTypes.PluginLoaded, LogLevel.Information, SilverlightMediaFrameworkResources.PluginsLoadedAdPayloadHandlerLogMessage);
        }

        private void UnloadAdPayloadHandlerPlugins()
        {
            IEnumerable<IAdPayloadHandlerPlugin> plugins = PluginsManager.AdPayloadHandlerPlugins.Select(i => i.Value).Where(v => v.IsLoaded);

            foreach (IAdPayloadHandlerPlugin plugin in plugins)
            {
                plugin.HandleCompleted -= AdPayloadHandlerPlugins_HandleCompleted;
                plugin.PluginLoadFailed -= Plugin_PluginLoadFailed;
                plugin.Unload();
                plugin.LogReady -= Plugin_LogReady;
            }

            _adMarkerManager.PayloadHandlers = null;
        }

        private void LoadS3DPlugins()
        {
            IEnumerable<IS3DPlugin> plugins = PluginsManager.S3DPlugins
                .Where(i => !i.IsValueCreated)
                .Select(i => i.Value);

            foreach (IS3DPlugin plugin in plugins)
            {
                plugin.MediaPresenterElement = MediaPresenterElement;
                plugin.PluginLoadFailed += Plugin_PluginLoadFailed;
                plugin.LogReady += Plugin_LogReady;
                plugin.SetPlayer(this);
                plugin.Load();
            }

            SendLogEntry(KnownLogEntryTypes.PluginLoaded, LogLevel.Information, SilverlightMediaFrameworkResources.PluginsLoaded3DLogMessage);
        }

        private void UnloadS3DPlugins()
        {
            IEnumerable<IS3DPlugin> plugins = PluginsManager.S3DPlugins.Select(i => i.Value).Where(v => v.IsLoaded);

            foreach (IS3DPlugin plugin in plugins)
            {
                plugin.PluginLoadFailed -= Plugin_PluginLoadFailed;
                plugin.LogReady -= Plugin_LogReady;
                plugin.MediaPresenterElement = null;
                plugin.Unload();
            }
        }

        private void LoadHeuristicsPlugin()
        {
            ActiveHeuristicsPlugin = SelectHeuristicsPlugin();

            if (ActiveHeuristicsPlugin != null && !ActiveHeuristicsPlugin.IsLoaded)
            {
                ActiveHeuristicsPlugin.Load();
            }
        }

        private void UnloadHeuristicsPlugin()
        {
            if (ActiveHeuristicsPlugin != null && !ActiveHeuristicsPlugin.IsLoaded)
            {
                ActiveHeuristicsPlugin.Unload();
                ActiveHeuristicsPlugin = null;
            }
        }

        protected virtual void ConfigureVideoSize(bool goFullScreen = false)
        {
            if (goFullScreen || IsFullScreen)
            {
                ActiveMediaPlugin.IfNotNull(i => i.Stretch = CurrentPlaylistItem != null ? CurrentPlaylistItem.VideoStretchMode : PlaylistItem.DefaultVideoStretchMode);
                VideoHeight = double.NaN;
                VideoWidth = double.NaN;
            }
            else if (CurrentPlaylistItem != null)
            {
                ActiveMediaPlugin.IfNotNull(i => i.Stretch = CurrentPlaylistItem.VideoStretchMode);
                VideoHeight = CurrentPlaylistItem.VideoHeight;
                VideoWidth = CurrentPlaylistItem.VideoWidth;
            }
            else
            {
                ActiveMediaPlugin.IfNotNull(i => i.Stretch = PlaylistItem.DefaultVideoStretchMode);
                VideoHeight = PlaylistItem.DefaultVideoHeight;
                VideoWidth = PlaylistItem.DefaultVideoWidth;
            }

            if (!IsInDesignMode)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.ConfiguredVideoSizeLogMessage, VideoHeight, VideoWidth);
                SendLogEntry(message: message, type: KnownLogEntryTypes.ConfiguredVideoSize,
                            extendedProperties: new Dictionary<string, object> { { "Height", VideoHeight }, { "Width", VideoWidth } });
            }
        }

        private void RegisterMediaPlugin(IMediaPlugin mediaPlugin)
        {
            var adaptiveMediaPlugin = mediaPlugin as IAdaptiveMediaPlugin;

            mediaPlugin.CurrentStateChanged += MediaPlugin_CurrentStateChanged;
            mediaPlugin.MediaOpened += MediaPlugin_MediaOpened;
            mediaPlugin.MediaFailed += MediaPlugin_MediaFailed;
            mediaPlugin.MediaEnded += MediaPlugin_MediaEnded;
            mediaPlugin.LogReady += Plugin_LogReady;
            mediaPlugin.MarkerReached += MediaPlugin_MarkerReached;
            mediaPlugin.BufferingProgressChanged += MediaPlugin_BufferingProgressChanged;
            mediaPlugin.DownloadProgressChanged += MediaPlugin_DownloadProgressChanged;
            mediaPlugin.PluginLoaded += MediaPlugIn_PluginLoaded;
            mediaPlugin.PluginLoadFailed += MediaPlugIn_PluginLoadFailed;
            mediaPlugin.PlaybackRateChanged += MediaPlugin_PlaybackRateChanged;
            mediaPlugin.AdClickThrough += MediaPlugin_AdClickThrough;
            mediaPlugin.AdProgressUpdated += MediaPlugin_AdProgressUpdated;
            mediaPlugin.AdStateChanged += mediaPlugin_AdStateChanged;
            mediaPlugin.AdError += MediaPlugin_AdError;
            mediaPlugin.SeekCompleted += MediaPlugin_SeekCompleted;


            if (adaptiveMediaPlugin != null)
            {
                adaptiveMediaPlugin.ManifestReady += MediaPlugin_ManifestReady;
                adaptiveMediaPlugin.StreamSelected += MediaPlugin_StreamSelected;
                adaptiveMediaPlugin.StreamUnselected += MediaPlugin_StreamUnselected;
                adaptiveMediaPlugin.StreamSelectionFailed += MediaPlugin_StreamSelectionFailed;
                adaptiveMediaPlugin.StreamDataAdded += MediaPlugin_StreamDataAdded;
                adaptiveMediaPlugin.StreamDataRemoved += MediaPlugin_StreamDataRemoved;
                adaptiveMediaPlugin.DownloadStreamDataFailed += MediaPlugin_DownloadStreamDataFailed;
                adaptiveMediaPlugin.DownloadStreamDataCompleted += MediaPlugin_DownloadStreamDataCompleted;
                adaptiveMediaPlugin.AdaptiveStreamingErrorOccurred += MediaPlugin_AdaptiveStreamingErrorOccurred;
                //adaptiveMediaPlugin.VideoPlaybackTrackChanged += MediaPlugin_VideoPlaybackTrackChanged;
                adaptiveMediaPlugin.VideoDownloadTrackChanged += MediaPlugin_VideoDownloadTrackChanged;
            }
            if (mediaPlugin is IVariableBitrateMediaPlugin)
            {
                ((IVariableBitrateMediaPlugin)mediaPlugin).VideoPlaybackBitrateChanged += MediaPlugin_VideoPlaybackBitrateChanged;
            }

            PlaySpeedManager = new PlaySpeedManager(mediaPlugin);
            InitPlaySpeedManager();

            _retryMonitor = new RetryMonitor
            {
                MediaPlugin = mediaPlugin
            };

            _retryMonitor.RetryAttemptFailed += RetryMonitor_RetryAttemptFailed;
            _retryMonitor.RetryCancelled += RetryMonitor_RetryCancelled;
            _retryMonitor.RetryFailed += RetryMonitor_RetryFailed;
            _retryMonitor.RetryStarted += RetryMonitor_RetryStarted;
            _retryMonitor.RetrySuccessful += RetryMonitor_RetrySuccessful;

            MediaPluginRegistered.IfNotNull(i => i(this, new CustomEventArgs<IMediaPlugin>(mediaPlugin)));
        }

        partial void InitPlaySpeedManager();

        private void UnregisterMediaPlugin(IMediaPlugin mediaPlugin)
        {
            var adaptiveMediaPlugin = mediaPlugin as IAdaptiveMediaPlugin;

            _adMarkerManager.Reset();
            _captionManager.Reset();
            _timelineMarkerManager.Reset();

            mediaPlugin.CurrentStateChanged -= MediaPlugin_CurrentStateChanged;
            mediaPlugin.MediaOpened -= MediaPlugin_MediaOpened;
            mediaPlugin.MediaFailed -= MediaPlugin_MediaFailed;
            mediaPlugin.MediaEnded -= MediaPlugin_MediaEnded;
            mediaPlugin.LogReady -= Plugin_LogReady;
            mediaPlugin.MarkerReached -= MediaPlugin_MarkerReached;
            mediaPlugin.BufferingProgressChanged -= MediaPlugin_BufferingProgressChanged;
            mediaPlugin.DownloadProgressChanged -= MediaPlugin_DownloadProgressChanged;
            mediaPlugin.PluginLoaded -= MediaPlugIn_PluginLoaded;
            mediaPlugin.PluginLoadFailed -= MediaPlugIn_PluginLoadFailed;
            mediaPlugin.PlaybackRateChanged -= MediaPlugin_PlaybackRateChanged;
            mediaPlugin.AdClickThrough -= MediaPlugin_AdClickThrough;
            mediaPlugin.AdProgressUpdated -= MediaPlugin_AdProgressUpdated;
            mediaPlugin.AdStateChanged -= mediaPlugin_AdStateChanged;
            mediaPlugin.AdError -= MediaPlugin_AdError;
            mediaPlugin.SeekCompleted -= MediaPlugin_SeekCompleted;

            if (adaptiveMediaPlugin != null)
            {
                ActiveHeuristicsPlugin.IfNotNull(i => i.UnregisterPlugin(adaptiveMediaPlugin));
                adaptiveMediaPlugin.ManifestReady -= MediaPlugin_ManifestReady;
                adaptiveMediaPlugin.StreamSelected -= MediaPlugin_StreamSelected;
                adaptiveMediaPlugin.StreamSelectionFailed -= MediaPlugin_StreamSelectionFailed;
                adaptiveMediaPlugin.StreamDataAdded -= MediaPlugin_StreamDataAdded;
                adaptiveMediaPlugin.DownloadStreamDataFailed -= MediaPlugin_DownloadStreamDataFailed;
                adaptiveMediaPlugin.DownloadStreamDataCompleted -= MediaPlugin_DownloadStreamDataCompleted;
                adaptiveMediaPlugin.AdaptiveStreamingErrorOccurred -= MediaPlugin_AdaptiveStreamingErrorOccurred;
                adaptiveMediaPlugin.StreamUnselected -= MediaPlugin_StreamUnselected;
                adaptiveMediaPlugin.StreamDataRemoved -= MediaPlugin_StreamDataRemoved;
                //adaptiveMediaPlugin.VideoPlaybackTrackChanged -= MediaPlugin_VideoPlaybackTrackChanged;
                adaptiveMediaPlugin.VideoDownloadTrackChanged -= MediaPlugin_VideoDownloadTrackChanged;
            }
            if (mediaPlugin is IVariableBitrateMediaPlugin)
            {
                ((IVariableBitrateMediaPlugin)mediaPlugin).VideoPlaybackBitrateChanged -= MediaPlugin_VideoPlaybackBitrateChanged;
            }

            if (PlaySpeedManager != null)
            {
                PlaySpeedManager.Dispose();
                PlaySpeedManager = null;
            }

            if (_retryMonitor != null)
            {
                _retryMonitor.RetryAttemptFailed -= RetryMonitor_RetryAttemptFailed;
                _retryMonitor.RetryCancelled -= RetryMonitor_RetryCancelled;
                _retryMonitor.RetryFailed -= RetryMonitor_RetryFailed;
                _retryMonitor.RetryStarted -= RetryMonitor_RetryStarted;
                _retryMonitor.RetrySuccessful -= RetryMonitor_RetrySuccessful;
                _retryMonitor.Dispose();
                _retryMonitor = null;
            }

            if (mediaPlugin.IsLoaded)
            {
                mediaPlugin.Unload();
            }

            MediaPresenterElement.IfNotNull(i => i.Content = null);
        }

        private void RegisterMarkerProvider(IMarkerProvider markerProvider)
        {
            markerProvider.MarkersRemoved += MarkerProvider_MarkersRemoved;
            markerProvider.NewMarkers += MarkerProvider_NewMarkers;
            markerProvider.RetrieveMarkersFailed += MarkerProvider_RetrieveMarkersFailed;
            markerProvider.LogReady += Plugin_LogReady;
            markerProvider.PluginLoadFailed += Plugin_PluginLoadFailed;
            markerProvider.PluginLoaded += MarkerProvider_PluginLoaded;
        }

        private void UnregisterMarkerProvider(IMarkerProvider markerProvider)
        {
            markerProvider.MarkersRemoved -= MarkerProvider_MarkersRemoved;
            markerProvider.NewMarkers -= MarkerProvider_NewMarkers;
            markerProvider.RetrieveMarkersFailed -= MarkerProvider_RetrieveMarkersFailed;
            markerProvider.LogReady -= Plugin_LogReady;
            markerProvider.PluginLoadFailed -= Plugin_PluginLoadFailed;
            markerProvider.PluginLoaded -= MarkerProvider_PluginLoaded;
            markerProvider.StopRetrievingMarkers();
            markerProvider.Unload();
        }

        private bool seekInProgress;    // allows recursive operations to maintain the seek state
        protected virtual void UpdateTimelinePositions(bool isSeeking = false, bool isStopping = false)
        {
            if (!isSeekActive)
            {
                seekInProgress = seekInProgress || isSeeking;
                try
                {
                    StartPosition = CalculateRelativeMediaPosition(ActiveMediaPlugin.StartPosition);
                    EndPosition = CalculateEndPosition();

                    DroppedFramesPerSecond = ActiveMediaPlugin != null
                                                 ? ActiveMediaPlugin.DroppedFramesPerSecond
                                                 : 0;

                    RenderedFramesPerSecond = ActiveMediaPlugin != null
                                                  ? ActiveMediaPlugin.RenderedFramesPerSecond
                                                  : 0;

                    MaximumPlaybackBitrate = ActiveMediaPlugin != null && ActiveMediaPlugin is IVariableBitrateMediaPlugin
                                                  ? ((IVariableBitrateMediaPlugin)ActiveMediaPlugin).MaximumPossibleBitrate
                                                  : 0;

                    TimeSpan playbackPosition = RelativeMediaPluginPosition;
                    if (!IsScrubbing)
                    {
                        this.PlaybackPosition = playbackPosition;
                    }

                    LivePosition = IsLiveMediaSupported && ActiveLiveMediaPlugin.IsSourceLive
                                       ? CalculateRelativeMediaPosition(ActiveLiveMediaPlugin.LivePosition)
                                       : (TimeSpan?)null;

                    IsMediaLive = ActiveLiveMediaPlugin != null && ActiveLiveMediaPlugin.IsSourceLive;

                    var liveThreshold = IsPositionLive
                                            ? TimeSpan.FromSeconds(PositionLiveBuffer.TotalSeconds + (PositionLiveBuffer.TotalSeconds * .1))
                                            : TimeSpan.FromSeconds(PositionLiveBuffer.TotalSeconds - (PositionLiveBuffer.TotalSeconds * .1));

                    IsPositionLive = LivePosition.HasValue
                                      && LivePosition.Value.Subtract(PlaybackPosition) < liveThreshold;

                    PlaySpeedState = PlaySpeedManager == null || PlaySpeedManager.IsPlaySpeedNormal
                                         ? PlaySpeedState.NormalPlayback
                                         : PlaySpeedManager.IsFastForwarding
                                               ? PlaySpeedState.FastForwarding
                                               : PlaySpeedManager.IsRewinding
                                                     ? PlaySpeedState.Rewinding
                                                     : PlaySpeedManager.IsSlowMotion
                                                           ? PlaySpeedState.SlowMotion
                                                           : PlaySpeedState.NormalPlayback;

                    if (!isStopping && RetryState == RetryStateEnum.NotRetrying && ActiveMediaPlugin != null)
                    {
                        _captionManager.CheckMarkerPositions(playbackPosition, Captions, seekInProgress);
                        CaptionsPresenterElement.IfNotNull(i => i.UpdateCaptions(playbackPosition, seekInProgress));

                        if (seekInProgress)
                        {
                            _timelineMarkerManager.CheckForSkippedMarkers(playbackPosition, TimelineMarkers);
                            _adMarkerManager.CheckForSkippedMarkers(playbackPosition, AdMarkers);
                        }

                        _adMarkerManager.CheckMarkerPositions(playbackPosition, AdMarkers, seekInProgress);
                        _timelineMarkerManager.CheckMarkerPositions(playbackPosition, TimelineMarkers, seekInProgress);
                    }
                }
                catch (Exception err)
                {
                    string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                                   "UpdateTimelinePositions", err.Message);
                    SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
                }
                finally
                {
                    seekInProgress = false;
                }
            }

            UpdateTimeline();
            TimelineChanged.IfNotNull(i => i(this, EventArgs.Empty));
        }

        protected virtual TimeSpan CalculateEndPosition()
        {
            var endPosition = ActiveMediaPlugin != null
                                  ? CalculateRelativeMediaPosition(ActiveMediaPlugin.EndPosition)
                                  : TimeSpan.Zero;

            if (IsMediaLive)
            {
                if (endPosition < EstimatedLiveDuration)
                {
                    endPosition = IsStartPositionOffset
                                      ? EstimatedLiveDuration
                                      : EstimatedLiveDuration.Add(StartPosition);
                }
                else if (EstimatedLiveDurationExceeded != null)
                {
                    EstimatedLiveDurationExceeded(this, EventArgs.Empty);
                }
            }


            return endPosition;
        }

        public TimeSpan CalculateAbsoluteMediaPosition(TimeSpan relativePosition)
        {
            return IsStartPositionOffset && ActiveMediaPlugin != null
                           ? relativePosition.Add(ActiveMediaPlugin.StartPosition)
                           : relativePosition;
        }

        public TimeSpan CalculateRelativeMediaPosition(TimeSpan absolutePosition)
        {
            var startPosition = ActiveMediaPlugin.StartPosition;
            return IsStartPositionOffset && ActiveMediaPlugin != null
                ? (absolutePosition < startPosition ? startPosition : absolutePosition.Subtract(startPosition))
                : absolutePosition;
        }

        private void OnPlaylistChanged()
        {
            try
            {
                retryPending = false;
                var wasPlaySuspended = playSuspended;
                Stop();
                playSuspended = wasPlaySuspended;
                UpdateMute();

                SendLogEntry(message: SilverlightMediaFrameworkResources.PlaylistChangedLogMessage, type: KnownLogEntryTypes.PlaylistChanged);
                PlaylistChanged.IfNotNull(i => i(this, new CustomEventArgs<IList<PlaylistItem>>(Playlist)));

                if (Playlist != null)
                {
                    if (CurrentPlaylistItem != null && !Playlist.Contains(CurrentPlaylistItem))
                    {
                        CurrentPlaylistItem = null;
                    }

                    if (AutoLoad && CurrentPlaylistItem == null)
                    {
                        CurrentPlaylistItem = Playlist.FirstOrDefault();
                    }
                }
                else
                {
                    CurrentPlaylistItem = null;
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnPlaylistChanged", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private void SetChunkDownloadStrategy(ChunkDownloadStrategy strategy)
        {
            if (_activeMediaPlugin is IAdaptiveMediaPlugin)
            {
                if (strategy == ChunkDownloadStrategy.Unspecified)
                {
                    strategy = this.ChunkDownloadStrategy;
                }
                if (strategy == ChunkDownloadStrategy.Unspecified)
                {
                    strategy = ChunkDownloadStrategy.AsNeeded;
                }
                (_activeMediaPlugin as IAdaptiveMediaPlugin).ChunkDownloadStrategy = strategy;
            }
        }

        private void OnCurrentPlaylistItemChanged(PlaylistItem previousPlaylistItem)
        {
            try
            {
                retryPending = false;
                var wasPlaySuspended = playSuspended;
                Stop();
                playSuspended = wasPlaySuspended;
                RestorePlayerDefaults();
                BitrateGraphElement.IfNotNull(i => i.Reset());
                FramerateGraphElement.IfNotNull(i => i.Reset());
                UpdateCurrentPlaylistItemSelection();
                previousPlaylistItem.IfNotNull(i => i.LicenseAcquirer = null);

                if (CurrentPlaylistItem != null)
                {
                    IsPosterVisible = !AutoPlay;

                    CurrentPlaylistItem.TimelineMarkers.ForEach(TimelineMarkers.Add);
                    CurrentPlaylistItem.Chapters.ForEach(Chapters.Add);

                    if (CurrentPlaylistItem.Captions.Any())
                    {
                        CurrentPlaylistItem.Captions.ForEach(Captions.Add);
                        _captionManager.CheckMarkerPositions(RelativeMediaPluginPosition, Captions, ignoreSearchWindow: true);
                    }

                    ActiveMediaPlugin = SelectMediaPlugin(CurrentPlaylistItem);
                    if (ActiveMediaPlugin != null)
                    {
                        ActiveMediaPlugin.Load();
                        SendLogEntry(KnownLogEntryTypes.PlaylistItemChanged, message: SilverlightMediaFrameworkResources.PlaylistItemChangedLogMessage);
                        OnPlaylistItemChanged();
                        SetChunkDownloadStrategy(CurrentPlaylistItem.ChunkDownloadStrategy);
                    }
                    else
                    {
                        SendLogEntry(KnownLogEntryTypes.UnableToLocateMediaPlugin, LogLevel.Warning, SilverlightMediaFrameworkResources.UnableToLocateMediaPluginLogMessage);
                        OnPlaylistItemChanged();
                    }
                }
                else
                {
                    MarkerProviders = null;
                    ActiveMediaPlugin = null;
                    SendLogEntry(message: SilverlightMediaFrameworkResources.PlaylistItemChangedLogMessage, type: KnownLogEntryTypes.PlaylistItemChanged);
                    OnPlaylistItemChanged();
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnCurrentPlaylistItemChanged", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        partial void OnPlaylistItemChanged();

        protected virtual void OnMediaPluginLoaded()
        {
            try
            {
                MediaPresenterElement.IfNotNull(i =>
                {
                    /*
                    If a Plugin-injected Canvas is loaded as a child of the MediaPresenter, then
                    a Plugin has taken over the loading of the media plugin, so skip this step.
                    */
                    if ((i.Content == null) ||
                        ((i.Content.GetType()) != typeof(Canvas)) ||
                        (((Canvas)i.Content).Name != "PluginInjectedCanvas"))
                    {
                        i.Content = ActiveMediaPlugin.VisualElement;
                        i.UpdateLayout();
                    }
                });

                ConfigureVideoSize();
                ActiveMediaPlugin.AutoPlay = AutoPlay;
                if (BufferingTime.HasValue) // leave the default value as is unless a value has been explicitly set
                {
                    ActiveMediaPlugin.BufferingTime = BufferingTime.Value;
                }
                ActiveMediaPlugin.Volume = VolumeLevel;
                ActiveMediaPlugin.IsMuted = IsMuted || StartMuted;
                CurrentPlaylistItem.LicenseAcquirer.IfNotNull(i => ActiveMediaPlugin.LicenseAcquirer = i);

                if (EnableCachedComposition && ActiveMediaPlugin.CacheMode == null)
                {
                    ActiveMediaPlugin.CacheMode = new BitmapCache();
                }

                if (ActiveAdaptiveMediaPlugin != null)
                {
                    object cacheProvider = SelectAdaptiveCacheProvider();
                    cacheProvider.IfNotNull(ActiveAdaptiveMediaPlugin.SetCacheProvider);
                    try
                    {
                        if (HeuristicsPriority.HasValue && ActiveHeuristicsPlugin != null && ActiveHeuristicsPlugin.SupportsPlugin(ActiveMediaPlugin))
                        {
                            ActiveHeuristicsPlugin.RegisterPlugin(ActiveAdaptiveMediaPlugin, HeuristicsPriority.Value, EnableSync);
                        }
                    }
                    catch (Exception err)
                    {
                        string message =
                            string.Format(SilverlightMediaFrameworkResources.ConfigureMediaPluginFailedLogMessage, err.Message);
                        SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
                    }
                }

                if (ActiveMediaPlugin.SupportsAdScheduling)
                {
                    if (CurrentPlaylistItem.PreRollAdvertisement != null)
                    {
                        CurrentPlaylistItem.PreRollAdvertisement.StartTime = null;
                        ScheduleAdvertisement(CurrentPlaylistItem.PreRollAdvertisement);
                    }

                    if (CurrentPlaylistItem.PostRollAdvertisement != null)
                    {
                        CurrentPlaylistItem.PostRollAdvertisement.StartTime = null;
                        ScheduleAdvertisement(CurrentPlaylistItem.PostRollAdvertisement);
                    }

                    if (CurrentPlaylistItem.InterstitialAdvertisements != null &&
                        CurrentPlaylistItem.InterstitialAdvertisements.Count > 0)
                    {
                        CurrentPlaylistItem.InterstitialAdvertisements
                                           .ForEach<Advertisement, IAdContext>(ScheduleAdvertisement);
                    }
                }

                if (CurrentPlaylistItem.StreamSource != null)
                {
                    ActiveMediaPlugin.StreamSource = CurrentPlaylistItem.StreamSource;
                }
                else if (CurrentPlaylistItem.DeliveryMethod == DeliveryMethods.AdaptiveStreaming &&
                        ActiveAdaptiveMediaPlugin != null)
                {
                    ActiveAdaptiveMediaPlugin.AdaptiveSource = CurrentPlaylistItem.MediaSource;
                }
                else
                {

#if !WINDOWS_PHONE && !OOB
                    if (CurrentPlaylistItem.MediaSource != null
                        && CurrentPlaylistItem.MediaSource.Scheme != OfflineExtensions.IsolatedStorageScheme)
                    {
                        ActiveMediaPlugin.Source = CurrentPlaylistItem.MediaSource;
                    }
                    else
                    {
                        ActiveMediaPlugin.StreamSource = OfflineManager.OpenIsolatedStorageUri(CurrentPlaylistItem.MediaSource);
                    }
#else
                    ActiveMediaPlugin.Source = CurrentPlaylistItem.MediaSource;
                    ActiveMediaPlugin.Play();
#endif

                }

                if (CurrentPlaylistItem.MarkerResources != null && CurrentPlaylistItem.MarkerResources.Count > 0)
                {
                    var markerProviders = new List<IMarkerProvider>();
                    foreach (var r in CurrentPlaylistItem.MarkerResources)
                    {
                        var provider = InitializeMarkerProvider(r);
                        if (provider != null)
                        {
                            markerProviders.Add(provider);
                        }
                    }
                    if (markerProviders.Count > 0)
                    {
                        MarkerProviders = markerProviders;
                    }
                }
                else
                {
                    MarkerProviders = null;
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.ConfigureMediaPluginFailedLogMessage,
                                               err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private IMarkerProvider InitializeMarkerProvider(MarkerResource resource)
        {
            var markerProvider = SelectMarkerProvider(resource);
            if (markerProvider != null)
            {
                markerProvider.Source = resource.Source;
                markerProvider.PollingInterval = resource.PollingInterval;
            }
            else
            {
                SendLogEntry(KnownLogEntryTypes.UnableToLocateMarkerProvider, LogLevel.Warning,
                    SilverlightMediaFrameworkResources.UnableToLocateMarkerProviderLogMessage);
            }
            return markerProvider;
        }

        protected virtual void OnMediaPluginLoadFailed()
        {
            ActiveMediaPlugin = null;
            GoToNextPlaylistItem();
        }



        #region Caption Streams
        private void SelectPlaylistCaptionStream()
        {
            if (!CurrentPlaylistItem.SelectedCaptionStreamName.IsNullOrWhiteSpace() ||
                        !CurrentPlaylistItem.SelectedCaptionStreamLanguage.IsNullOrWhiteSpace())
            {
                SelectedCaptionStream =
                    AvailableCaptionStreams.Where(
                        i =>
                        CurrentPlaylistItem.SelectedCaptionStreamName.IsNullOrWhiteSpace() ||
                        string.Equals(i.Name, CurrentPlaylistItem.SelectedCaptionStreamName,
                                      StringComparison.CurrentCultureIgnoreCase))
                        .Where(
                            i =>
                            CurrentPlaylistItem.SelectedCaptionStreamLanguage.IsNullOrWhiteSpace() ||
                            string.Equals(i.Language, CurrentPlaylistItem.SelectedCaptionStreamLanguage,
                                          StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();
            }
        }

        private void UpdateAvailableCaptionStreams()
        {
            try
            {
                AvailableCaptionStreams = ActiveAdaptiveMediaPlugin != null
                                          && ActiveAdaptiveMediaPlugin.CurrentSegment != null
                                          && ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams != null
                                              ? ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams
                                                    .Where(IsCaptionStream)
                                                    .Select(i => new StreamMetadata
                                                                     {
                                                                         Id = i.Id,
                                                                         Attributes = i.Attributes
                                                                     })
                                                    .OrderBy(i => i.Name)
                                                    .ToList()
                                              : Enumerable.Empty<StreamMetadata>();
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "UpdateAvailableCaptionStreams", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private void SelectCaptionStream()
        {
            if (ActiveAdaptiveMediaPlugin != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams != null
                && (SelectedCaptionStream == null || !ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams.Any(i => i.Id == SelectedCaptionStream.Id)))
            {
                try
                {
                    //Clear existing captions before switching streams
                    Captions.Clear();
                    VisibleCaptions.Clear();

                    //Get currently selected streams
                    List<IMediaStream> captionStreams =
                        ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams.Where(i => IsCaptionStream(i)).ToList();


                    IMediaStream captionStream = SelectedCaptionStream != null
                                                     ? captionStreams.Where(i => i.Id == SelectedCaptionStream.Id)
                                                           .FirstOrDefault()
                                                     : null;

                    //Remove all caption streams from the list
                    var streamsToRemove = captionStreams;
                    var streamsToAdd = new List<IMediaStream>();

                    if (captionStream != null && captionStream.AvailableTracks.Any())
                    {
                        //If one exists w/ the currently specified language add it to the list and set the streams
                        streamsToRemove.Remove(captionStream);
                        streamsToAdd.Add(captionStream);
                        ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, streamsToAdd, streamsToRemove);
                        CaptionStreamChanged.IfNotNull(i => i(this, new CustomEventArgs<StreamMetadata>(SelectedCaptionStream)));
                        SendLogEntry(KnownLogEntryTypes.CaptionStreamChanged,
                            extendedProperties: new Dictionary<string, object> { { "StreamName", captionStream.Name } });
                    }
                    else
                    {
                        //If not set the streams and clear the selected language
                        ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, streamsToAdd, streamsToRemove);
                        SelectedCaptionStream = null;
                    }
                }
                catch (Exception err)
                {
                    string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                                   "SelectCaptionStream", err.Message);
                    SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
                }
            }
        }
        #endregion

        #region Audio Streams
        private void SelectPlaylistAudioStream()
        {
            if (!CurrentPlaylistItem.SelectedAudioStreamName.IsNullOrWhiteSpace() ||
                        !CurrentPlaylistItem.SelectedAudioStreamLanguage.IsNullOrWhiteSpace())
            {
                SelectedAudioStream =
                    AvailableAudioStreams.Where(
                        i =>
                        CurrentPlaylistItem.SelectedAudioStreamName.IsNullOrWhiteSpace() ||
                        string.Equals(i.Name, CurrentPlaylistItem.SelectedAudioStreamName,
                                      StringComparison.CurrentCultureIgnoreCase))
                        .Where(
                            i =>
                            CurrentPlaylistItem.SelectedAudioStreamLanguage.IsNullOrWhiteSpace() ||
                            string.Equals(i.Language, CurrentPlaylistItem.SelectedAudioStreamLanguage,
                                          StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();
            }
            else
            {
                UpdateSelectedAudioStream();
            }
        }

        private void UpdateAvailableAudioStreams()
        {
            try
            {
                AvailableAudioStreams = ActiveAdaptiveMediaPlugin != null
                                        && ActiveAdaptiveMediaPlugin.CurrentSegment != null
                                        && ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams != null
                                            ? ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams
                                                  .Where(i => i.Type == StreamType.Audio)
                                                  .Select(i => new StreamMetadata
                                                                   {
                                                                       Id = i.Id,
                                                                       Attributes = i.Attributes
                                                                   })
                                                  .OrderBy(i => i.Name)
                                                  .ToList()
                                            : Enumerable.Empty<StreamMetadata>();
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMediaOpened", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private void UpdateSelectedAudioStream()
        {
            try
            {
                SelectedAudioStream = ActiveAdaptiveMediaPlugin != null
                                      && ActiveAdaptiveMediaPlugin.CurrentSegment != null
                                      && ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams != null
                                          ? ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams
                                                .Where(i => i.Type == StreamType.Audio)
                                                .Select(i =>
                                                        new StreamMetadata
                                                            {
                                                                Id = i.Id,
                                                                Attributes = i.Attributes
                                                            })
                                                .FirstOrDefault()
                                          : null;
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMediaOpened", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private void SelectAudioStream()
        {
            if (ActiveAdaptiveMediaPlugin != null
                && SelectedAudioStream != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams != null
                && ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams != null
                && !ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams.Any(i => i.Id == SelectedAudioStream.Id))
            {
                try
                {
                    //Get currently selected streams
                    List<IMediaStream> audioStreams =
                        ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams.Where(i => i.Type == StreamType.Audio).ToList();



                    IMediaStream audioStream = audioStreams
                                                         .Where(i => i.Id == SelectedAudioStream.Id)
                                                         .FirstOrDefault();
                    //Remove all audio streams from the list
                    List<IMediaStream> streamsToRemove =
                        audioStreams.Where(i => i.Id != audioStream.Id)
                        .ToList();

                    if (audioStream != null && audioStream.AvailableTracks.Count() > 0)
                    {
                        //If one exists w/ the currently specified language add it to the list and set the streams
                        List<IMediaStream> streamsToAdd = new List<IMediaStream>();
                        streamsToAdd.Add(audioStream);
                        ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, streamsToAdd, streamsToRemove);
                        AudioStreamChanged.IfNotNull(i => i(this, new CustomEventArgs<StreamMetadata>(SelectedAudioStream)));
                        SendLogEntry(KnownLogEntryTypes.AudioStreamChanged,
                            extendedProperties: new Dictionary<string, object> { { "StreamName", audioStream.Name } });
                    }
                    else
                    {
                        UpdateSelectedAudioStream();
                    }
                }
                catch (Exception err)
                {
                    string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                                   "OnMediaOpened", err.Message);
                    SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
                }
            }
        }
        #endregion




        protected virtual void RestorePlayerDefaults()
        {
            if (MarkerProviders != null)
            {
                MarkerProviders = null;
            }

            AvailableVideoBitrates = Enumerable.Empty<long>();
            Chapters.IfNotNull(i => i.Clear());
            Captions.IfNotNull(i => i.Clear());
            VisibleCaptions.IfNotNull(i => i.Clear());
            TimelineMarkers.IfNotNull(i => i.Clear());
            PlaySpeedState = PlaySpeedState.NormalPlayback;
            IsMediaAdaptive = false;
            IsMediaLive = false;
            MaximumPlaybackBitrate = DefaultMaximumBitrate;
            LivePosition = null;
            PlaybackPosition = TimeSpan.Zero;
            StartPosition = TimeSpan.Zero;
            EndPosition = TimeSpan.Zero;
            AvailableAudioStreams = Enumerable.Empty<StreamMetadata>();
            SelectedAudioStream = null;
            PlaybackBitrate = 0;
            DownloadBitrate = 0;
            DownloadProgress = 0;
            BufferingProgress = 0;
            VideoHeight = PlaylistItem.DefaultVideoHeight;
            VideoWidth = PlaylistItem.DefaultVideoWidth;
            PlayState = MediaPluginState.Stopped;
        }

        internal void SendLogEntry(string type,
                                  LogLevel severity = LogLevel.Information,
                                  string message = null,
                                  string senderName = null,
                                  DateTime? timeStamp = null,
                                  IEnumerable<KeyValuePair<string, object>> extendedProperties = null)
        {
            if ((severity & LogLevel) != LogLevel.None)
            {
                var logEntry = new LogEntry
                                   {
                                       Severity = severity,
                                       Message = message ?? string.Empty,
                                       Type = type ?? KnownLogEntryTypes.GeneralLogEntry,
                                       SenderName = !string.IsNullOrEmpty(senderName) ? senderName : "Player",
                                       Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                                   };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);

                OnLogEntryReceived(logEntry);
            }
        }

        private void ResetRetryInformation()
        {
            _firstRetryItem = null;
        }

        private void TryNextPlaylistItem()
        {
            bool canTryNext = true;
            if (_firstRetryItem == null)
            {
                _firstRetryItem = CurrentPlaylistItem;
            }
            else
            {
                canTryNext = (CurrentPlaylistItem != _firstRetryItem);
            }
            if (canTryNext)
            {
                GoToNextPlaylistItem();
            }
        }
        #endregion



        #region Protected Methods

        protected virtual void ConfigureCaptionPresenterSize()
        {
            if (ActiveMediaPlugin != null && CaptionsPresenterElement != null)
            {
                var aspectRatio = ActiveMediaPlugin.NaturalVideoSize.Width / ActiveMediaPlugin.NaturalVideoSize.Height;
                var aspectPresentationWidth = MediaPresenterElement.ActualHeight * aspectRatio;

                if (aspectPresentationWidth > MediaPresenterElement.ActualWidth)
                {
                    //Video will have black bars on top and bottom
                    CaptionsPresenterElement.Width = MediaPresenterElement.ActualWidth;
                    CaptionsPresenterElement.Height = MediaPresenterElement.ActualWidth / aspectRatio;
                }
                else if (aspectPresentationWidth < MediaPresenterElement.ActualWidth)
                {
                    //Video will have black bars on the sides
                    CaptionsPresenterElement.Height = MediaPresenterElement.ActualHeight;
                    CaptionsPresenterElement.Width = MediaPresenterElement.ActualHeight * aspectRatio;
                }
                else
                {
                    CaptionsPresenterElement.Width = MediaPresenterElement.ActualWidth;
                    CaptionsPresenterElement.Height = MediaPresenterElement.ActualHeight;
                }
            }
        }

        protected virtual void OnMediaOpened()
        {
            try
            {
                isSeekActive = false;

                SendLogEntry(message: SilverlightMediaFrameworkResources.MediaOpenedLogMessage, type: KnownLogEntryTypes.MediaOpened);

                if (ActiveMediaPlugin != null)
                {
                    //ActiveMediaPlugin.Volume = DefaultVolume;
                    VolumeLevel = ActiveMediaPlugin.Volume;
                    IsMuted = ActiveMediaPlugin.IsMuted;
                    IsSlowMotionEnabled = PlaySpeedManager.SupportsSlowMotion;
                    IsMediaLive = IsLiveMediaSupported && ActiveLiveMediaPlugin.IsSourceLive;
                    IsMediaAdaptive = IsAdaptiveMediaSupported && ActiveAdaptiveMediaPlugin.IsSourceAdaptive;
                    AvailableVideoBitrates = IsMediaAdaptive
                                            ? ActiveAdaptiveMediaPlugin.CurrentSegment.AvailableStreams
                                                        .Where(i => i.Type == StreamType.Video)
                                                        .SelectMany(i => i.AvailableTracks)
                                                        .Select(i => i.Bitrate)
                                                        .Distinct()
                                            : Enumerable.Empty<long>();

                    ConfigureCaptionPresenterSize();
                    UpdateTimelinePositions();

                    if (CurrentPlaylistItem.StartPosition.HasValue)
                    {
                        SeekToPosition(CurrentPlaylistItem.StartPosition.Value);
                    }
                    else if (CurrentPlaylistItem.JumpToLive && IsMediaLive && ActiveLiveMediaPlugin != null)
                    {
                        SeekToLive();
                    }


                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMediaOpened", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }

            MediaOpened.IfNotNull(i => i(this, EventArgs.Empty));
        }

        protected virtual void OnMediaEnded()
        {
            try
            {
                playSuspended = false;
                autoPlaySuspended = null;
                isSeekActive = false;
                _adMarkerManager.Reset();
                _timelineMarkerManager.Reset();
                _captionManager.Reset();
                SendLogEntry(message: SilverlightMediaFrameworkResources.MediaEndedLogMessage, type: KnownLogEntryTypes.MediaEnded);
                MediaEnded.IfNotNull(i => i(this, EventArgs.Empty));
                GoToNextPlaylistItem();
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMediaEnded", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        protected virtual void OnMediaFailed(Exception error)
        {
            try
            {
                isSeekActive = false;

                string logMessage = string.Format(SilverlightMediaFrameworkResources.MediaFailedLogMessage,
                                                  error.Message);
                SendLogEntry(KnownLogEntryTypes.MediaFailed, LogLevel.Warning, logMessage);
                MediaFailed.IfNotNull(i => i(this, new CustomEventArgs<Exception>(error)));

                if (!IsPlayBlocked)
                {
                    OnStartRetry();
                }
                else 
                {
                    retryPending = true;
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMediaFailed", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        private void OnStartRetry()
        {
            if (Playlist != null
                && RetryDuration > TimeSpan.Zero
                && CurrentPlaylistItem != null
                && _retryMonitor != null
                && (ActiveMediaPlugin.Source != null ||
                        (ActiveAdaptiveMediaPlugin != null && ActiveAdaptiveMediaPlugin.AdaptiveSource != null))
                && !_retryMonitor.IsRetrying)
            {
                _adMarkerManager.Reset();
                _timelineMarkerManager.Reset();
                _captionManager.Reset();
                StartRetry();
            }
            else if ((_retryMonitor == null || !_retryMonitor.IsRetrying) && ContinuousPlay)
            {
                playSuspended = false;
                autoPlaySuspended = null;
                _adMarkerManager.Reset();
                _timelineMarkerManager.Reset();
                _captionManager.Reset();
                GoToNextPlaylistItem();
            }
        }

        //Initialize plugins with static lifetimes
        protected virtual void OnAddExternalPackageCompleted()
        {
            IList<string> logWriterIds = LogWriters.ToDelimitedList();
            LoadLoggingPlugins(logWriterIds);
            LoadUIPlugins();
            LoadGenericPlugins();
            LoadS3DPlugins();
            LoadAdPayloadHandlerPlugins();
            LoadHeuristicsPlugin();
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnLogEntryReceived(LogEntry logEntry)
        {
            LogEntryReceived.IfNotNull(i => i(this, new CustomEventArgs<LogEntry>(logEntry)));
            _logger.SendLogEntry(logEntry);

            if (LoggingDisplayElement != null)
            {
                string output = string.Format(SilverlightMediaFrameworkResources.LoggingOutputFormat, logEntry.Severity,
                                              logEntry.SenderName, logEntry.Type, logEntry.Message, logEntry.Timestamp);
                LoggingDisplayElement.Text = output + Environment.NewLine + LoggingDisplayElement.Text;
            }
        }

        /// <summary>
        /// Watches for certain key combinations to display additional information on screen.
        /// </summary>
        /// <remarks>
        /// The Alt-Ctrl-V key combination displays the Version Information window.
        /// The Alt-Ctrl-L key combination displays logging information.
        /// </remarks>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (VersionInformationVisibility == FeatureVisibility.Hidden
                && e.Key == Key.V
                && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt
                && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                VersionInformationVisibility = FeatureVisibility.Visible;
                if (LoggingConsoleVisibility == FeatureVisibility.Visible)
                {
                    LoggingConsoleVisibility = FeatureVisibility.Hidden;
                }
            }

            if (LoggingConsoleVisibility == FeatureVisibility.Hidden
                && e.Key == Key.L
                && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt
                && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                LoggingConsoleVisibility = FeatureVisibility.Visible;
                if (VersionInformationVisibility == FeatureVisibility.Visible)
                {
                    VersionInformationVisibility = FeatureVisibility.Hidden;
                }
            }

            if ((e.Key == Key.Space || e.Key == Key.Escape) && !IsFullScreen)
            {
                if (VersionInformationVisibility == FeatureVisibility.Visible ||
                    LoggingConsoleVisibility == FeatureVisibility.Visible)
                {
                    if (LoggingConsoleVisibility == FeatureVisibility.Visible)
                    {
                        LoggingConsoleVisibility = FeatureVisibility.Hidden;
                    }

                    if (VersionInformationVisibility == FeatureVisibility.Visible)
                    {
                        VersionInformationVisibility = FeatureVisibility.Hidden;
                    }
                }
                else
                {
                    if (ActiveMediaPlugin != null && AllowSpaceBarToggle)
                    {
                        if (ActiveMediaPlugin.CurrentState == MediaPluginState.Playing)
                        {
                            ActiveMediaPlugin.Pause();
                        }
                        else if (ActiveMediaPlugin.CurrentState == MediaPluginState.Paused)
                        {
                            ActiveMediaPlugin.Play();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Selects a MediaPlugin plugin that can play a specified Playlist item.
        /// </summary>
        /// <remarks>
        /// The PlaylistItem passed to this method is used to determine which type of IMediaPlugin to return.
        /// If more than one plugin can play the Playlist item (based on the required capabilities it lists in its attributes), the first plugin found that matches that item's play requirements in used.
        /// For the selection of which plugin to use if several match the requirements, a plugin already instantiated will take precedence over other plugins.
        /// </remarks>
        /// <returns>
        /// An IMediaPlugin that can play the type of Playlist item passed as a parameter.
        /// </returns>
#if !WINDOWS_PHONE && !PROGRAMMATICCOMPOSITION
        protected virtual IMediaPlugin SelectMediaPlugin(PlaylistItem playlistItem)
        {
            IEnumerable<LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata>> results = PluginsManager.MediaPlugins
                .Where(
                    i =>
                    (playlistItem.DeliveryMethod == DeliveryMethods.NotSpecified ||
                        (i.Metadata.SupportedDeliveryMethods & playlistItem.DeliveryMethod) != DeliveryMethods.NotSpecified)
                        && playlistItem.MediaPluginRequiredMetadata.CompareMetadata(i.LooseMetadata));
            //If the media is live and we have implementations that support the live interface then
            //filter the list down to these implementations as they will provide
            //a better experience for the user, however, this is not a required to play the live media

            results = playlistItem.LiveDvrRequired && results.Any(i => i.Metadata.SupportsLiveDvr)
                            ? results.Where(i => i.Metadata.SupportsLiveDvr)
                            : results;

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata> result = results.Any(i => i.IsValueCreated)
                                                                                ? results.First(i => i.IsValueCreated)
                                                                                : results.FirstOrDefault();

            return result != null ? result.Value : null;
        }
#else
        protected virtual IMediaPlugin SelectMediaPlugin(PlaylistItem playlistItem)
        {
            IEnumerable<LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata>> results = PluginsManager.MediaPlugins;
            if (playlistItem.DeliveryMethod != DeliveryMethods.NotSpecified)
            {
                results = results.Where(i => i.LooseMetadata.ContainsKey("SupportedDeliveryMethods"));
                results = results.Where(i => (((DeliveryMethods)i.LooseMetadata["SupportedDeliveryMethods"]) & playlistItem.DeliveryMethod) != 0);
            }

            IEnumerable<LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata>> subResults;
            if (playlistItem.MediaType == null)
            {
                subResults = results.Where(i => !((string[])i.LooseMetadata["SupportedMediaTypes"]).Any());
            }
            else
            {
                subResults = results.Where(i => ((string[])i.LooseMetadata["SupportedMediaTypes"]).Contains(playlistItem.MediaType));
            }
            // make this filter forgiving. Ignore if it filtered out all media plugins
            if (subResults.Any())
            {
                results = subResults;
            }

            //If the media is live and we have implementations that support the live interface then
            //filter the list down to these implementations as they will provide
            //a better experience for the user, however, this is not a required to play the live media
            if (playlistItem.LiveDvrRequired)
            {
                results = results.Where(i => i.LooseMetadata.ContainsKey("SupportsLiveDvr"));
                results = results.Where(i => ((bool)i.LooseMetadata["SupportsLiveDvr"]));
            }

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata> result = results.Any(i => i.IsValueCreated)
                                                                                ? results.First(i => i.IsValueCreated)
                                                                                : results.FirstOrDefault();
            return result != null ? result.Value : null;
        }
#endif

        protected virtual object SelectAdaptiveCacheProvider()
        {
            Lazy<object> result = PluginsManager.AdaptiveCacheProviders.FirstOrDefault();

            return result != null
                       ? result.Value
                       : null;
        }

        protected virtual IHeuristicsPlugin SelectHeuristicsPlugin()
        {
            IEnumerable<LooseMetadataLazy<IHeuristicsPlugin, IPluginMetadata>> results = PluginsManager.
                HeuristicsPlugins
                .Where(i => HeuristicsPluginRequiredMetadata.CompareMetadata(i.LooseMetadata));

            LooseMetadataLazy<IHeuristicsPlugin, IPluginMetadata> result = results.Any(i => i.IsValueCreated)
                                                                               ? results.First(i => i.IsValueCreated)
                                                                               : results.FirstOrDefault();

            return result != null ? result.Value : null;
        }


#if !WINDOWS_PHONE && !PROGRAMMATICCOMPOSITION
        /// <summary>
        /// Selects an IMarkerProvider plugin that supports the type of specified Marker.
        /// </summary>
        /// <remarks>
        /// The MarkerResource passed to this method is used to determine which type of MarkerProvider to return.
        /// If more than one plugin supports the MarkerResource (based on the required capabilities in its attributes), the first plugin found that matches the requirements in used.
        /// If more than one plugin matches the requirements, a plugin already instantiated will take precedence over other plugins.
        /// </remarks>
        /// <returns>
        /// An IMarkerProvider that supports the type of marker passed as a parameter.
        /// </returns>
        protected virtual IMarkerProvider SelectMarkerProvider(MarkerResource markerResource)
        {
            IEnumerable<LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata>> results = PluginsManager.
                MarkerProviderPlugins
                .Where(
                    i =>
                    (i.Metadata.SupportedFormat == markerResource.Format)
                    && (i.Metadata.SupportsPolling || !markerResource.PollingInterval.HasValue));

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata> result = results.Any(i => i.IsValueCreated)
                                                                                     ? results.First(
                                                                                         i => i.IsValueCreated)
                                                                                     : results.FirstOrDefault();

            return result != null ? result.Value : null;
        }
#else

        protected virtual IMarkerProvider SelectMarkerProvider(MarkerResource markerResource)
        {
            var results = new List<LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata>>();

            foreach (var markerProviderPlugin in PluginsManager.MarkerProviderPlugins)
            {
                if (markerProviderPlugin.LooseMetadata.ContainsKeyIgnoreCase("supportedformat")
                    && markerProviderPlugin.LooseMetadata.GetEntryIgnoreCase("supportedformat").Equals(markerResource.Format))
                {
                    bool supportsPolling;

                    if (!markerResource.PollingInterval.HasValue ||
                        (markerProviderPlugin.LooseMetadata.ContainsKeyIgnoreCase("supportspolling")
                        && bool.TryParse(markerProviderPlugin.LooseMetadata.GetEntryIgnoreCase("supportspolling").ToString(), out supportsPolling)
                        && supportsPolling))
                    {
                        results.Add(markerProviderPlugin);
                    }
                }
            }

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata> result = results.Any(i => i.IsValueCreated)
                                                                                     ? results.First(
                                                                                         i => i.IsValueCreated)
                                                                                     : results.FirstOrDefault();

            return result != null ? result.Value : null;
        }
#endif
        /// <summary>
        /// Queries the available plugins and selects an ILogWriter plugin.
        /// </summary>
        /// <remarks>
        /// The text passed to this method is used to determine which type of ILogWriter to return.
        /// If more than one plugin matches the requirements for the log writer (based on the comma-separated list of Log writers in its attributes), the first plugin found that matches the requirements in used.
        /// If more than one plugin matches the requirements, a plugin already instantiated will take precedence over other plugins.
        /// </remarks>
        protected virtual ILogWriter SelectLogWriter(string logWriterId)
        {
#if !WINDOWS_PHONE && !PROGRAMMATICCOMPOSITION
            IEnumerable<LooseMetadataLazy<ILogWriter, ILogWriterMetadata>> results = PluginsManager.LogWriterPlugins
                .Where(i => i.Metadata.LogWriterId == logWriterId);

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<ILogWriter, ILogWriterMetadata> result = results.Any(i => i.IsValueCreated)
                                                                           ? results.First(i => i.IsValueCreated)
                                                                           : results.FirstOrDefault();

            return result != null ? result.Value : null;
#else
            var results = new List<LooseMetadataLazy<ILogWriter, ILogWriterMetadata>>();

            foreach (var logWriterPlugin in PluginsManager.LogWriterPlugins)
            {
                if (logWriterPlugin.LooseMetadata.ContainsKeyIgnoreCase("logwriterId")
                    && logWriterPlugin.LooseMetadata.GetEntryIgnoreCase("logwriterId").Equals(logWriterId))
                {
                    results.Add(logWriterPlugin);
                }
            }

            //Favor Plugins that have already been instantiated
            LooseMetadataLazy<ILogWriter, ILogWriterMetadata> result = results.Any(i => i.IsValueCreated)
                                                                                     ? results.First(
                                                                                         i => i.IsValueCreated)
                                                                                     : results.FirstOrDefault();

            return result != null ? result.Value : null;
#endif
        }

        protected virtual void OnManifestReady()
        {
            try
            {
                VolumeLevel = ActiveMediaPlugin.Volume;
                SendLogEntry(KnownLogEntryTypes.ManifestReady);

                UpdateAvailableAudioStreams();
                UpdateAvailableCaptionStreams();
                SelectPlaylistAudioStream();
                SelectPlaylistCaptionStream();

                RestrictTracks();
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnManifestReady", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        partial void RestrictTracks();

        /// <summary>
        /// Raises the AdMarkerReached event.
        /// </summary>
        protected virtual void OnAdMarkerReached(AdMarker mediaMarker, bool seekedInto)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerReachedLogMessage,
                                               mediaMarker.Type, mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerReached);

                if (mediaMarker.Type == KnownMarkerTypes.Title)
                {
                    MediaTitleContent = mediaMarker.Content;
                }

                AdMarkerReached.IfNotNull(i => i(this, new AdMarkerReachedInfoEventArgs() { Marker = mediaMarker, SeekedInto = seekedInto }));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnAdMarkerReached", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the AdMarkerLeft event.
        /// </summary>
        protected virtual void OnAdMarkerLeft(AdMarker mediaMarker)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerLeftLogMessage, mediaMarker.Type,
                                               mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerLeft);

                AdMarkerLeft.IfNotNull(i => i(this, new AdMarkerEventArgs(mediaMarker)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnAdMarkerLeft", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the AdMarkerSkipped event.
        /// </summary>
        protected virtual void OnAdMarkerSkipped(AdMarker mediaMarker)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerSkippedLogMessage,
                                               mediaMarker.Type, mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerSkipped);

                AdMarkerSkipped.IfNotNull(i => i(this, new AdMarkerEventArgs(mediaMarker)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnAdMarkerSkipped", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the TimelineMarkerReached event.
        /// </summary>
        protected virtual void OnTimelineMarkerReached(TimelineMediaMarker mediaMarker, bool seekedInto)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerReachedLogMessage,
                                               mediaMarker.Type, mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerReached);

                if (mediaMarker.Type == KnownMarkerTypes.Title)
                {
                    MediaTitleContent = mediaMarker.Content;
                }

                TimelineMarkerReached.IfNotNull(i => i(this, new TimelineMarkerReachedInfo { Marker = mediaMarker, SeekedInto = seekedInto }));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMarkerReached", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the TimelineMarkerLeft event.
        /// </summary>
        protected virtual void OnTimelineMarkerLeft(TimelineMediaMarker mediaMarker)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerLeftLogMessage, mediaMarker.Type,
                                               mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerLeft);

                TimelineMarkerLeft.IfNotNull(i => i(this, new CustomEventArgs<TimelineMediaMarker>(mediaMarker)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMarkerLeft", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the TimelineMarkerSkipped event.
        /// </summary>
        protected virtual void OnTimelineMarkerSkipped(TimelineMediaMarker mediaMarker)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.MarkerSkippedLogMessage,
                                               mediaMarker.Type, mediaMarker.Begin, mediaMarker.End);
                SendLogEntry(message: message, type: KnownLogEntryTypes.MarkerSkipped);

                TimelineMarkerSkipped.IfNotNull(i => i(this, new CustomEventArgs<TimelineMediaMarker>(mediaMarker)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnMarkerSkipped", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the CaptionLeft event.
        /// </summary>
        protected virtual void OnCaptionRegionLeft(CaptionRegion region)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.CaptionRegionLeftLogMessage, region.Id);
                SendLogEntry(message: message, type: KnownLogEntryTypes.CaptionRegionLeft);

                VisibleCaptions.Remove(region);
                CaptionLeft.IfNotNull(i => i(this, new CustomEventArgs<CaptionRegion>(region)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnCaptionRegionLeft", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        /// <summary>
        /// Raises the CaptionReached event.
        /// </summary>
        protected virtual void OnCaptionRegionReached(CaptionRegion region)
        {
            try
            {
                string message = string.Format(SilverlightMediaFrameworkResources.CaptionRegionReachedLogMessage,
                                               region.Id);
                SendLogEntry(message: message, type: KnownLogEntryTypes.CaptionRegionReached);

                VisibleCaptions.Add(region);
                CaptionReached.IfNotNull(i => i(this, new CustomEventArgs<CaptionRegion>(region)));
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnCaptionRegionReached", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        protected virtual void OnStreamSelected(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream)
        {
            try
            {
                if ((stream.Type == StreamType.Binary || stream.Type == StreamType.Text) &&
                    stream.AvailableTracks.Any())
                {
                    IMediaTrack track = stream.AvailableTracks.First();
                    ActiveAdaptiveMediaPlugin.DownloadStreamData(track);
                    SendLogEntry(KnownLogEntryTypes.StreamSelected, LogLevel.Debug, "OnStreamSelected",
                        extendedProperties: new Dictionary<string, object> { { "StreamName", stream.Name } });
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnStreamSelected", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        protected virtual void OnStreamUnselected(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream)
        {
            try
            {
                if ((stream.Type == StreamType.Binary || stream.Type == StreamType.Text) &&
                    stream.AvailableTracks.Any())
                {
                    IMediaTrack track = stream.AvailableTracks.First();
                    ActiveAdaptiveMediaPlugin.CancelDownloadStreamData(track);
                    SendLogEntry(KnownLogEntryTypes.StreamUnselected, LogLevel.Debug, "OnStreamUnselected",
                        extendedProperties: new Dictionary<string, object> { { "StreamName", stream.Name } });
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnStreamUnselected", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        protected virtual void OnStreamDataAdded(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream,
                                                 IDataChunk dataChunk)
        {
            try
            {
                if (stream.AvailableTracks.Count() > 0)
                {
                    IMediaTrack track = stream.AvailableTracks.First();
                    ActiveAdaptiveMediaPlugin.DownloadStreamData(track, dataChunk);
                    SendLogEntry(KnownLogEntryTypes.StreamDataAdded,
                extendedProperties: new Dictionary<string, object> { { "StreamName", stream.Name } });
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnStreamDataAdded", err.Message);
                SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, message);
            }
        }

        protected virtual void OnDownloadStreamDataCompleted(IAdaptiveMediaPlugin mediaPlugin, IMediaTrack track,
                                                             IStreamDownloadResult result)
        {
            try
            {
                if (DataReceived != null)
                {
                    int length = (int)result.Stream.Length;
                    var data = new byte[length];
                    int count;
                    int sum = 0;

                    do
                    {
                        count = result.Stream.Read(data, sum, length - sum);
                        sum += count;
                    } while (count > 0 && sum < length);

                    DataReceived(this, new DataReceivedInfo(data, result.DataChunk, track.ParentStream.Attributes));
                    SendLogEntry(KnownLogEntryTypes.DataReceived);
                }
            }
            catch (Exception err)
            {
                string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                               "OnDownloadStreamDataCompleted", err.Message);
                SendLogEntry(KnownLogEntryTypes.DownloadStreamDataCompleted, LogLevel.Error, message);
            }
        }

        protected virtual void OnPluginLogReady(IPlugin plugin, LogEntry logEntry)
        {
            if ((logEntry.Severity & LogLevel) != LogLevel.None)
            {
                OnLogEntryReceived(logEntry);
            }
        }

        protected virtual void OnUIPluginLoaded(IUIPlugin uiPlugin)
        {
            Panel targetControl = !uiPlugin.Target.IsNullOrWhiteSpace()
                                ? this.GetVisualChild<Panel>(uiPlugin.Target)
                                : PlayerRoot;

            targetControl.IfNotNull(i => i.Children.Add(uiPlugin.Element));
        }

        protected virtual void OnUIPluginUnloaded(IUIPlugin uiPlugin)
        {
            Panel targetControl = !uiPlugin.Target.IsNullOrWhiteSpace()
                                ? this.GetVisualChild<Panel>(uiPlugin.Target)
                                : PlayerRoot;

            targetControl.IfNotNull(i => i.Children.Remove(uiPlugin.Element));
        }

        protected virtual void OnVideoDownloadTrackChanged(long bitrate)
        {
            DownloadBitrate = bitrate;
            DownloadBitrateChanged.IfNotNull(i => i(this, new CustomEventArgs<long>(bitrate)));
            SendLogEntry(KnownLogEntryTypes.VideoDownloadBitrateChanged,
                extendedProperties: new Dictionary<string, object> { { "Bitrate", bitrate } });
        }

        protected virtual void OnVideoPlaybackTrackChanged(long bitrate)
        {
            PlaybackBitrate = bitrate;
            PlaybackBitrateChanged.IfNotNull(i => i(this, new CustomEventArgs<long>(bitrate)));
            SendLogEntry(KnownLogEntryTypes.VideoPlaybackBitrateChanged,
                extendedProperties: new Dictionary<string, object> { { "Bitrate", bitrate } });
        }

        protected virtual void OnScrubbingStarted(TimeSpan scrubbingPosition)
        {
            IsScrubbing = true;
            ScrubbingStarted.IfNotNull(i => i(this, new CustomEventArgs<TimeSpan>(scrubbingPosition)));
            SendLogEntry(KnownLogEntryTypes.ScrubbingStarted,
                extendedProperties: new Dictionary<string, object> { { "NewPosition", scrubbingPosition } });
        }

        protected virtual void OnScrubbing(TimeSpan scrubbingPosition)
        {
            if (SeekWhileScrubbing)
            {
                PlaybackPosition = scrubbingPosition;
                Scrubbing.IfNotNull(i => i(this, new CustomEventArgs<TimeSpan>(scrubbingPosition)));
                SendLogEntry(KnownLogEntryTypes.Scrubbed,
                    extendedProperties: new Dictionary<string, object> { { "NewPosition", scrubbingPosition } });
            }
        }

        protected virtual void OnScrubbingCompleted(TimeSpan scrubbingPosition)
        {
            SeekToPosition(scrubbingPosition);
            IsScrubbing = false;
            ScrubbingCompleted.IfNotNull(i => i(this, new CustomEventArgs<TimeSpan>(scrubbingPosition)));
            SendLogEntry(KnownLogEntryTypes.ScrubbingCompleted,
                extendedProperties: new Dictionary<string, object> { { "NewPosition", scrubbingPosition } });
        }
        #endregion

        #region Event Handlers

        partial void AdProgressUpdated(IAdContext adContext, AdProgress adProgress);
        partial void AdStateChanged(IAdContext adContext);
        partial void AdError(IAdContext adContext);

        private void MediaPlugin_AdProgressUpdated(IAdaptiveMediaPlugin mediaPlugin, IAdContext adContext, AdProgress adProgress)
        {
            SendLogEntry(KnownLogEntryTypes.AdProgressUpdated,
                extendedProperties: new Dictionary<string, object> { { "Progress", adProgress.ToString() } });

            AdProgressUpdated(adContext, adProgress);

            AdvertisementProgressChanged.IfNotNull(i => i(this, new AdvertisementProgressChangedInfo { AdContext = adContext, AdProgress = adProgress }));
        }


        private void mediaPlugin_AdStateChanged(IAdaptiveMediaPlugin mediaPlugin, IAdContext adContext)
        {
            SendLogEntry(KnownLogEntryTypes.AdProgressUpdated,
                extendedProperties: new Dictionary<string, object> { { "State", adContext.CurrentAdState.ToString() } });

            AdStateChanged(adContext);

            AdvertisementStateChanged.IfNotNull(i => i(this, new AdvertisementStateChangedInfo { AdContext = adContext }));
        }

        private void MediaPlugin_AdClickThrough(IAdaptiveMediaPlugin mediaPlugin, IAdContext adContext)
        {
            SendLogEntry(KnownLogEntryTypes.AdClickThrough);
            AdvertisementClickThrough.IfNotNull(i => i(this, new CustomEventArgs<IAdContext>(adContext)));
        }

        private void MediaPlugin_AdError(IAdaptiveMediaPlugin mediaPlugin, IAdContext adContext)
        {
            AdError(adContext);

            AdvertisementError.IfNotNull(i => i(this, new CustomEventArgs<IAdContext>(adContext)));
        }

        private void MediaPlugin_SeekCompleted(IMediaPlugin obj)
        {
            isSeekActive = false;
            UpdateTimelinePositions(true);
            SeekCompleted.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void MediaPlugin_AdaptiveStreamingErrorOccurred(IAdaptiveMediaPlugin mediaPlugin, Exception err)
        {
            SendLogEntry(KnownLogEntryTypes.AdaptiveStreamingErrorOccurred, message: err.Message);
        }

        private void MediaPlugin_PlaybackRateChanged(IMediaPlugin mediaPlugin)
        {
            SendLogEntry(KnownLogEntryTypes.PlaybackRateChanged,
                    extendedProperties: new Dictionary<string, object> { { "PlaybackRate", mediaPlugin.PlaybackRate } });
        }

        private void Plugin_LogReady(IPlugin plugin, LogEntry logEntry)
        {
            Dispatcher.BeginInvoke(() => OnPluginLogReady(plugin, logEntry));
        }

        private void UIPlugin_PluginLoaded(IPlugin plugin)
        {
            var uiPlugin = plugin as IUIPlugin;
            uiPlugin.IfNotNull(OnUIPluginLoaded);
        }

        private void UIPlugin_PluginUnloaded(IPlugin plugin)
        {
            var uiPlugin = plugin as IUIPlugin;
            uiPlugin.IfNotNull(OnUIPluginUnloaded);
        }

        private void Plugin_PluginLoadFailed(IPlugin plugin, Exception err)
        {
            string errorMessage = string.Format(SilverlightMediaFrameworkResources.PluginLoadFailedLogMessage,
                                                plugin.GetType(), err.Message);
            SendLogEntry(KnownLogEntryTypes.GeneralErrorOccurred, LogLevel.Error, errorMessage);
        }

        private void MarkerProvider_PluginLoaded(IPlugin plugin)
        {
            if (MarkerProviders.Contains(plugin as IMarkerProvider))
            {
                (plugin as IMarkerProvider).BeginRetrievingMarkers();
            }
        }

        private void Logger_LogWriteSuccessful(ILogWriter logWriter, LogEntry logEntry)
        {
            LogWriteSuccessful.IfNotNull(i => i(this, new CustomEventArgs<LogEntry>(logEntry)));
        }

        private void Logger_LogWriteErrorOccurred(ILogWriter logWriter, LogEntry logEntry, Exception err)
        {
            LogWriteErrorOccurred.IfNotNull(i => i(this, new LogWriteErrorOccurredInfo { LogEntry = logEntry, Error = err }));
        }

        private void RetryMonitor_RetrySuccessful(RetryMonitor retryMonitor)
        {
            SendLogEntry(message: SilverlightMediaFrameworkResources.RetrySuccessfulLogMessage, type: KnownLogEntryTypes.RetrySuccessful);
            RetryState = RetryStateEnum.NotRetrying;
            ResetRetryInformation();
            RetrySuccessful.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void RetryMonitor_RetryStarted(RetryMonitor retryMonitor)
        {
            SendLogEntry(message: SilverlightMediaFrameworkResources.RetryStartedLogMessage, type: KnownLogEntryTypes.RetryStarted);
            RetryState = RetryStateEnum.Retrying;
            RetryStarted.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void RetryMonitor_RetryFailed(RetryMonitor retryMonitor)
        {
            SendLogEntry(KnownLogEntryTypes.RetryFailed, LogLevel.Warning, SilverlightMediaFrameworkResources.RetryFailedLogMessage);
            RetryState = RetryStateEnum.RetryFailed;
            RetryFailed.IfNotNull(i => i(this, EventArgs.Empty));
            TryNextPlaylistItem();
        }

        private void RetryMonitor_RetryCancelled(RetryMonitor retryMonitor)
        {
            SendLogEntry(message: SilverlightMediaFrameworkResources.RetryCancelledLogMessage, type: KnownLogEntryTypes.RetryCancelled);
            RetryState = RetryStateEnum.NotRetrying;
            ResetRetryInformation();
            RetryCancelled.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void RetryMonitor_RetryAttemptFailed(RetryMonitor retryMonitor, Exception error)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.RetryAttemptFailedLogMessage,
                                              error.Message);
            SendLogEntry(KnownLogEntryTypes.RetryAttemptFailed, LogLevel.Warning, logMessage);
            RetryAttemptFailed.IfNotNull(i => i(this, new CustomEventArgs<Exception>(error)));
        }

        private void MarkerProvider_MarkersRemoved(IMarkerProvider markerProvider,
                                                   IEnumerable<MediaMarker> removedMarkers)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.MarkersRemovedLogMessage,
                                              removedMarkers.Count());
            SendLogEntry(message: logMessage, type: KnownLogEntryTypes.MarkersRemoved,
                extendedProperties: new Dictionary<string, object> { { "Count", removedMarkers.Count() } });

            foreach (MediaMarker marker in removedMarkers)
            {
                if (marker is AdMarker)
                {
                    AdMarkers.Remove(marker as AdMarker);
                }
                else if (marker is TimelineMediaMarker && marker.Type == KnownMarkerTypes.Timeline)
                {
                    TimelineMarkers.Remove(marker as TimelineMediaMarker);
                }
                else if (marker is Chapter && marker.Type == KnownMarkerTypes.Chapter)
                {
                    Chapters.Remove(marker as Chapter);
                }
                else if (marker is CaptionRegion && marker.Type == KnownMarkerTypes.Caption)
                {
                    Captions.Remove(marker as CaptionRegion);
                }
            }
        }

        private void MarkerProvider_NewMarkers(IMarkerProvider markerProvider, IEnumerable<MediaMarker> newMarkers)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.NewMarkersLogMessage,
                                              newMarkers.Count());
            SendLogEntry(message: logMessage, type: KnownLogEntryTypes.MarkersAdded,
                extendedProperties: new Dictionary<string, object> { { "Count", newMarkers.Count() } });

            foreach (MediaMarker marker in newMarkers)
            {
                if (marker is AdMarker)
                {
                    var adMarker = marker as AdMarker;
                    // Immediate == true will trigger the timeline marker immediately instead of waiting for polling to occur
                    if (adMarker.Immediate)
                    {
                        var duration = adMarker.Duration;
                        adMarker.Begin = RelativeMediaPluginPosition;
                        adMarker.End = adMarker.Begin.Add(duration);    // update the end based on the duration
                        AdMarkers.Add(adMarker);
                        // force a check on the postions, we know there is one that needs to be fired
                        if (!isSeekActive) _adMarkerManager.CheckMarkerPositions(RelativeMediaPluginPosition, AdMarkers, seekInProgress);
                    }
                    else
                    {
                        AdMarkers.Add(adMarker);
                    }
                }
                if (marker is TimelineMediaMarker)
                {
                    var timelineMarker = marker as TimelineMediaMarker;
                    TimelineMarkers.Add(timelineMarker);
                }
                else if (marker is Chapter)
                {
                    Chapters.Add(marker as Chapter);
                }
                else if (marker is CaptionRegion)
                {
                    Captions.Add(marker as CaptionRegion);
                }
            }
        }

        private void MarkerProvider_RetrieveMarkersFailed(IMarkerProvider markerProvider, Exception error)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.RetrieveMarkersFailedLogMessage, error.Message);
            SendLogEntry(KnownLogEntryTypes.RetrieveMarkersFailed, LogLevel.Error, logMessage);
        }

        private void PluginsManager_AddExternalPackageDownloadProgressChanged(PluginsManager pluginsManager,
                                                                              Uri xapLocation,
                                                                              DownloadProgressChangedEventArgs e)
        {
            string logMessage =
                string.Format(SilverlightMediaFrameworkResources.AddExternalPackageDownloadProgressChangedLogMessage,
                              xapLocation, e.ProgressPercentage);
            SendLogEntry(KnownLogEntryTypes.AddExternalPluginsDownloadProgressChanged, LogLevel.Warning, logMessage);
            AddExternalPackageDownloadProgressChanged.IfNotNull(i => i(this, new ExternalPackageDownloadProgressInfo { XapLocation = xapLocation, DownloadProgress = e }));
        }

        private void PluginsManager_AddExternalPackageFailed(PluginsManager pluginsManager, Uri xapLocation,
                                                             Exception error)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.AddExternalPackageFailedLogMessage,
                                              xapLocation, error.Message);
            SendLogEntry(KnownLogEntryTypes.AddExternalPluginsFailed, LogLevel.Warning, logMessage);
            AddExternalPluginsFailed.IfNotNull(i => i(this, new CustomEventArgs<Exception>(error)));
        }

        private void PluginsManager_AddExternalPackageCompleted(PluginsManager pluginsManager, Uri xapLocation)
        {
            string logMessage = string.Format(SilverlightMediaFrameworkResources.AddExternalPackageCompletedLogMessage,
                                              xapLocation);
            SendLogEntry(message: logMessage, type: KnownLogEntryTypes.AddExternalPluginsCompleted);
            OnAddExternalPackageCompleted();
            AddExternalPluginsCompleted.IfNotNull(i => i(this, EventArgs.Empty));
        }

        private void MediaPlugIn_PluginLoadFailed(IPlugin plugIn, Exception err)
        {
            Plugin_PluginLoadFailed(plugIn, err);
            Dispatcher.BeginInvoke(OnMediaPluginLoadFailed);
        }


        private void MediaPlugIn_PluginLoaded(IPlugin plugin)
        {
            Dispatcher.BeginInvoke(OnMediaPluginLoaded);
        }


        private void MediaPlugin_MediaOpened(IMediaPlugin mediaPlugin)
        {
            //All MediaPlugin events are being dispatched to the UI thread because
            //they may likely be coming in from another thread and errors can occur
            //if they interact with the UI
            Dispatcher.BeginInvoke(OnMediaOpened);
        }

        private void MediaPlugin_MediaEnded(IMediaPlugin mediaPlugin)
        {
            Dispatcher.BeginInvoke(OnMediaEnded);
        }

        private void MediaPlugin_MediaFailed(IMediaPlugin mediaPlugin, Exception error)
        {
            Dispatcher.BeginInvoke(() => OnMediaFailed(error));
        }

        private void MediaPlugin_MarkerReached(IMediaPlugin mediaPlugin, MediaMarker mediaMarker)
        {
            var timelineMarker = new TimelineMediaMarker
            {
                Begin = mediaMarker.Begin,
                Content = mediaMarker.Content,
                End = mediaMarker.End,
                Id = mediaMarker.Id,
                Type = mediaMarker.Type
            };
            Dispatcher.BeginInvoke(() => OnTimelineMarkerReached(timelineMarker, false));
        }

        private void MediaPlugin_DownloadProgressChanged(IMediaPlugin progressiveMediaPlugin, double downloadProgress)
        {
            Dispatcher.BeginInvoke(() => DownloadProgress = downloadProgress);
        }

        private void MediaPlugin_ManifestReady(IAdaptiveMediaPlugin obj)
        {
            OnManifestReady();
        }

        private void MediaPlugin_BufferingProgressChanged(IMediaPlugin progressiveMediaPlugin, double bufferingProgress)
        {
            Dispatcher.BeginInvoke(() => BufferingProgress = bufferingProgress);
        }

        private void MediaPlugin_VideoPlaybackBitrateChanged(IVariableBitrateMediaPlugin mediaPlugin, long bitrate)
        {
            Dispatcher.BeginInvoke(() => OnVideoPlaybackTrackChanged(bitrate));
        }

        //private void MediaPlugin_VideoPlaybackTrackChanged(IAdaptiveMediaPlugin adaptiveMediaPlugin, IMediaTrack track)
        //{
        //    Dispatcher.BeginInvoke(() => OnVideoPlaybackTrackChanged(track.Bitrate));
        //}

        private void MediaPlugin_VideoDownloadTrackChanged(IAdaptiveMediaPlugin adaptiveMediaPlugin, IMediaTrack track)
        {
            Dispatcher.BeginInvoke(() => OnVideoDownloadTrackChanged(track.Bitrate));
        }

        private void MediaPlugin_CurrentStateChanged(IMediaPlugin mediaPlugin, MediaPluginState playState)
        {
            Dispatcher.BeginInvoke(() => PlayState = playState);
        }

        private void MediaPlugin_StreamSelected(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream)
        {
            Dispatcher.BeginInvoke(() => OnStreamSelected(mediaPlugin, stream));
        }

        private void MediaPlugin_StreamUnselected(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream)
        {
            OnStreamUnselected(mediaPlugin, stream);
        }

        private void MediaPlugin_DownloadStreamDataCompleted(IAdaptiveMediaPlugin mediaPlugin, IMediaTrack track,
                                                             IStreamDownloadResult result)
        {
            //Dispatcher.BeginInvoke(() => OnDownloadStreamDataCompleted(mediaPlugin, track, result));
            OnDownloadStreamDataCompleted(mediaPlugin, track, result);
        }

        private void MediaPlugin_StreamSelectionFailed(IAdaptiveMediaPlugin mediaPlugin,
                                                       IEnumerable<IMediaStream> attemptedSelection, Exception error)
        {
            string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                           "MediaPlugin_StreamSelectionFailed", error.Message);

            SendLogEntry(KnownLogEntryTypes.StreamSelectionFailed, LogLevel.Error, message);
        }

        private void MediaPlugin_DownloadStreamDataFailed(IAdaptiveMediaPlugin mediaPlugin, IMediaTrack track,
                                                          IDataChunk dataChunk, Exception error)
        {
            string message = string.Format(SilverlightMediaFrameworkResources.GenericErrorOccurredLogMessage,
                                           "MediaPlugin_DownloadStreamDataFailed", error.Message);

            SendLogEntry(KnownLogEntryTypes.DownloadStreamDataFailed, LogLevel.Error, message);
        }

        private void MediaPlugin_StreamDataAdded(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream,
                                                 IDataChunk dataChunk)
        {
            Dispatcher.BeginInvoke(() => OnStreamDataAdded(mediaPlugin, stream, dataChunk));
        }

        private void MediaPlugin_StreamDataRemoved(IAdaptiveMediaPlugin mediaPlugin, IMediaStream mediaStream, TimeSpan timeStamp)
        {
            SendLogEntry(KnownLogEntryTypes.StreamDataRemoved,
                extendedProperties: new Dictionary<string, object> { { "StreamName", mediaStream.Name } });
        }

        private void AdManager_MarkerSkipped(MediaMarkerManager<AdMarker> markerManager, AdMarker mediaMarker)
        {
            OnAdMarkerSkipped(mediaMarker);
        }

        private void AdManager_MarkerReached(MediaMarkerManager<AdMarker> markerManager, AdMarker mediaMarker, bool seekedInto)
        {
            OnAdMarkerReached(mediaMarker, seekedInto);
        }

        private void AdManager_MarkerLeft(MediaMarkerManager<AdMarker> markerManager, AdMarker mediaMarker)
        {
            OnAdMarkerLeft(mediaMarker);
        }

        private void MarkerManager_MarkerSkipped(MediaMarkerManager<TimelineMediaMarker> markerManager, TimelineMediaMarker mediaMarker)
        {
            OnTimelineMarkerSkipped(mediaMarker);
        }

        private void MarkerManager_MarkerReached(MediaMarkerManager<TimelineMediaMarker> markerManager, TimelineMediaMarker mediaMarker, bool seekedInto)
        {
            OnTimelineMarkerReached(mediaMarker, seekedInto);
        }

        private void MarkerManager_MarkerLeft(MediaMarkerManager<TimelineMediaMarker> markerManager, TimelineMediaMarker mediaMarker)
        {
            OnTimelineMarkerLeft(mediaMarker);
        }

        private void CaptionManager_MarkerReached(MediaMarkerManager<CaptionRegion> captionManager, CaptionRegion region, bool skippedInto)
        {
            OnCaptionRegionReached(region);
        }

        private void CaptionManager_MarkerLeft(MediaMarkerManager<CaptionRegion> captionManager, CaptionRegion region)
        {
            OnCaptionRegionLeft(region);
        }
#if !WINDOWS_PHONE && !FULLSCREEN
        private void Application_FullScreenChanged(object sender, EventArgs e)
        {
            IsFullScreen = Application.Current.Host.Content.IsFullScreen;
        }
#endif
        #endregion

        #region Public Methods
#if !PROGRAMMATICCOMPOSITION
        /// <summary>
        /// Adds the specified catalog to the list of available plugins.
        /// </summary>
        /// <param name="catalog"></param>
        public void AddPluginsCatalog(ComposablePartCatalog catalog)
        {
            PluginsManager.IfNotNull(i => i.AddCatalog(catalog));
        }

        /// <summary>
        /// Begins loading plugins from the .XAP file at the specified URI.
        /// </summary>
        /// <remarks>
        /// This method calls the PluginsManager which loads any plugins in the .XAP file at the specified URI.
        /// The plugins will then be available for the application to use. 
        ///
        /// This method can be called at any time (rather than just at initialization of the Player). This method is not called automatically.
        ///
        /// The AddExternalPluginsCompleted event is fired when loading plugins is done. The AddExternalPluginsFailed event is fired if there is an error loading the plugins. 
        /// </remarks>
        public void BeginAddExternalPlugins(Uri xapLocation)
        {
            PluginsManager.BeginAddExternalPackage(xapLocation);
            SendLogEntry(KnownLogEntryTypes.BeginAddExternalPlugins);
        }
#endif

#if !WINDOWS_PHONE && !OOB
        /// <summary>
        /// Opens a playlist that has been stored offline.
        /// </summary>
        /// <param name="playlistFilename">The name of the file where the playlist has been stored.</param>
        /// <returns>The playlist items.</returns>
        public IEnumerable<PlaylistItem> OpenOfflinePlaylist(string playlistFilename)
        {
            return _offlineManager.ReadStoredPlaylist(playlistFilename);
        }

        /// <summary>
        /// This method will download the playlist content to Isolated Storage.  Also, it will write out the playlist items to an XML file
        /// indicated by playlistFilename.
        /// </summary>
        /// <param name="playlistFilename">The name of the file where the playlist items should be stored (in XML format).</param>
        /// <param name="playlist">A collection of playlist items, whose media content should be stored locally in Isolated Storage.</param>
        /// <param name="spaceRequired">The amount of space required to store all of the playlist items on disk.</param>
        public void StorePlaylistContentOffline(string playlistFilename, IEnumerable<PlaylistItem> playlist = null, long? spaceRequired = null)
        {
            playlist = playlist ?? Playlist;
            spaceRequired = spaceRequired ?? playlist.Sum(i => i.FileSize);

            _offlineManager.TakeOffline(playlist, spaceRequired.Value, playlistFilename);
        }
#endif

        /// <summary>
        /// Plays the current item in the playlist.
        /// </summary>
        public virtual void Play()
        {
            if (blocks.Count == 0)
            {
                if (CurrentPlaylistItem == null && Playlist != null && Playlist.Count > 0)
                {
                    AutoPlay = true;
                    CurrentPlaylistItem = Playlist.First();
                }
                else if (ActiveMediaPlugin != null && ActiveMediaPlugin.CurrentState != MediaPluginState.Playing)
                {
                    if (PlaySpeedManager.IsFastForwarding || PlaySpeedManager.IsRewinding)
                    {
                        PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }

                    ActiveMediaPlugin.Play();
                    SendLogEntry(KnownLogEntryTypes.Play);
                }
                playSuspended = false;
            }
            else
            {
                playSuspended = true;
            }
        }

        private bool? autoPlaySuspended = null;
        private bool playSuspended = false;
        private List<object> blocks = new List<object>();

        /// <summary>
        /// Blocks the player from starting a video or pauses the video if active. This is useful for pre-roll ads.
        /// Warning: You MUST call ReleasePlayBlock eventually to get the player to play again.
        /// </summary>
        /// <param name="blocker">The object applying the block. If the same object is added twice, it will be ignored.</param>
        public void AddPlayBlock(object blocker)
        {
            if (!blocks.Contains(blocker))
            {
                if (!IsPlayBlocked)
                {
                    if (ActiveMediaPlugin != null)
                    {
                        if (ActiveMediaPlugin.CurrentState == MediaPluginState.Playing ||
                            ActiveMediaPlugin.CurrentState == MediaPluginState.Buffering ||
                            ActiveMediaPlugin.CurrentState == MediaPluginState.ClipPlaying)
                        {
                            if (ActiveMediaPlugin.CanPause)
                                ActiveMediaPlugin.Pause();
                            else
                                ActiveMediaPlugin.Stop();
                            playSuspended = true;
                        }
                        else if (ActiveMediaPlugin.AutoPlay)
                        {
                            ActiveMediaPlugin.AutoPlay = false;
                            playSuspended = true;
                            // calling AddPlayBlock during PlaylistItemChanged event will not work without this. This is because the AutoPlay is actually used on the Dispatcher after the event fires causing ActiveMediaPlugin.AutoPlay to be overwritten
                            if (AutoPlay)
                            {
                                autoPlaySuspended = true;
                                AutoPlay = false;
                            }
                        }
                        else
                        {
                            // we are blocking but there is nothing to restore since we're not doing anything.
                        }
                    }
                    else if (AutoPlay)
                    {
                        autoPlaySuspended = true;
                        AutoPlay = false;
                        playSuspended = true;
                    }
                }
                blocks.Add(blocker);
                SendLogEntry(KnownLogEntryTypes.PlayBlockAdded, LogLevel.Information, string.Format(SilverlightMediaFrameworkResources.PlayBlockAdded, blocker.GetType().Name));
            }
        }

        /// <summary>
        /// Releases a play block created with AddPlayBlock
        /// </summary>
        /// <param name="blocker">The object applying the block. If the object does not have a block, it will be ignored.</param>
        public void ReleasePlayBlock(object blocker)
        {
            if (blocks.Contains(blocker))
            {
                blocks.Remove(blocker);
                SendLogEntry(KnownLogEntryTypes.PlayBlockReleased, LogLevel.Information, string.Format(SilverlightMediaFrameworkResources.PlayBlockReleased, blocker.GetType().Name));
                if (!IsPlayBlocked)
                {
                    if (retryPending) 
                    {
                        retryPending = false;
                        OnStartRetry();
                    }
                    else if (playSuspended)
                    {
                        playSuspended = false;
                        if (ActiveMediaPlugin != null)
                        {
                            if (PlayState == MediaPluginState.Opening)
                                ActiveMediaPlugin.AutoPlay = true;
                            else
                                Play();
                        }
                        if (autoPlaySuspended.GetValueOrDefault(false))
                        {
                            AutoPlay = true;
                        }
                        autoPlaySuspended = null;
                    }
                }
            }
        }

        /// <summary>
        /// Indicates whether or not play is current blocked via AddPlayBlock. This is useful for advertising.
        /// </summary>
        public bool IsPlayBlocked
        {
            get { return blocks.Any(); }
        }

        /// <summary>
        /// Pauses play of the current playlist item.
        /// </summary>
        public virtual void Pause()
        {
            playSuspended = false;
            if (ActiveMediaPlugin != null && ActiveMediaPlugin.CanPause)
            {
                ActiveMediaPlugin.Pause();
                SendLogEntry(KnownLogEntryTypes.Pause);
            }
        }

        /// <summary>
        /// Stops playing the current playlist item.
        /// </summary>
        public virtual void Stop()
        {
            playSuspended = false;
            if (_retryMonitor != null && _retryMonitor.IsRetrying)
            {
                _retryMonitor.CancelRetrying();
            }

            if (ActiveMediaPlugin != null)
            {
                ActiveMediaPlugin.Stop();
                UpdateTimelinePositions(isStopping: true);
                SendLogEntry(KnownLogEntryTypes.Stop);
            }
        }

        private IAdContext ScheduleAdvertisement(Advertisement advertisement)
        {
            return ScheduleAdvertisement(advertisement.AdSource, advertisement.DeliveryMethod, advertisement.StartTime, null,
                                         advertisement.ClickThrough, advertisement.Duration, advertisement.PauseTimeline, null, null, advertisement.IsLinearClip);
        }

        /// <summary>
        /// Begins downloading data from the streams with the specified names.  Listen to DataReceived event for download notifications.
        /// </summary>
        /// <param name="streamNames">A list of the stream names to subscribe to.</param>
        public void SubscribeToDataStreams(params string[] streamNames)
        {
            if (ActiveAdaptiveMediaPlugin != null && ActiveAdaptiveMediaPlugin.CurrentSegment != null)
            {
                var streamsToSelect = ActiveAdaptiveMediaPlugin.CurrentSegment
                    .AvailableStreams
                    .Where(i => streamNames.Contains(i.Name))
                    .ToList();

                if (streamsToSelect.Count > 0)
                {
                    ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, streamsToSelect, null);
                }
            }
        }

        /// <summary>
        /// Stops downloading data from the specified streams.
        /// </summary>
        /// <param name="streamNames">A list of the stream names to unsubscribe to.</param>
        public void UnsubscribeToDataStreams(params string[] streamNames)
        {
            if (ActiveAdaptiveMediaPlugin != null && ActiveAdaptiveMediaPlugin.CurrentSegment != null)
            {
                var streamsToRemove = ActiveAdaptiveMediaPlugin.CurrentSegment
                    .AvailableStreams
                    .Where(i => streamNames.Contains(i.Name))
                    .ToList();

                if (streamsToRemove.Count > 0)
                {
                    ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, null, streamsToRemove);
                }
            }
        }

        /// <summary>
        /// Schedules an ad to be played by this plugin.
        /// </summary>
        /// <param name="adSource">The source of the ad content.</param>
        /// <param name="deliveryMethod">The delivery method of the ad content.</param>
        /// <param name="duration">The duration of the ad content that should be played.  If ommitted the plugin will play the full duration of the ad content.</param>
        /// <param name="startTime">The position within the media where this ad should be played.  If ommited ad will begin playing immediately.</param>
        /// <param name="clickThrough">The URL where the user should be directed when they click the ad.</param>
        /// <param name="pauseTimeline">Indicates if the timeline of the currently playing media should be paused while the ad is playing.</param>
        /// <param name="appendToAd">Another scheduled ad that this ad should be appended to.  If ommitted this ad will be scheduled independently.</param>
        /// <param name="data">User data.</param>
        /// <returns>A reference to the IAdContext that contains information about the scheduled ad.</returns>
        public IAdContext ScheduleAdvertisement(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? startTime, TimeSpan? startOffset,
                                                Uri clickThrough = null, TimeSpan? duration = null,
                                                bool pauseTimeline = true, IAdContext appendToAd = null, object data = null, bool isLinearClip = false)
        {
            IAdContext adContext = null;
            if (ActiveMediaPlugin != null && ActiveMediaPlugin.SupportsAdScheduling)
            {
                if (deliveryMethod == DeliveryMethods.NotSpecified ||
                    (ActiveMediaPlugin.SupportedDeliveryMethods & deliveryMethod) != DeliveryMethods.NotSpecified)
                {
                    adContext = ActiveMediaPlugin.ScheduleAd(adSource, deliveryMethod, duration, startTime, startOffset, clickThrough,
                                                             pauseTimeline, appendToAd, data, isLinearClip);
                }
                else
                {
                    string message =
                        string.Format(SilverlightMediaFrameworkResources.UnsupportedAdDeliveryMethodExceptionMessage,
                                      deliveryMethod);
                    SendLogEntry(KnownLogEntryTypes.ScheduleAdvertisement, LogLevel.Warning, message);
                    throw new ArgumentException(message, "deliveryMethod");
                }
            }
            return adContext;
        }

        /// <summary>
        /// Schedules an ad that is to be handled by an AdPayloadHandlerPlugin.
        /// A valid AdPayloadHandlerPlugin must be part of your application or this will not be handled.
        /// </summary>
        /// <param name="adSource">The Uri of Ad's source. Often a VAST document</param>
        /// <param name="format">The format of the ad source. e.g. "vast"</param>
        /// <param name="startTime">The position within the media where this ad should be played.  If ommited ad will begin playing immediately.</param>
        /// <param name="duration">The duration of the ad content that should be played.  If ommitted the plugin will play the full duration of the ad content.</param>
        /// <returns>An object that contains information about the scheduled ad.</returns>
        public ScheduledAd ScheduleAdTrigger(string adSource, string format = "vast", TimeSpan? startTime = null, TimeSpan? duration = null)
        {
            var adSequencingSource = new AdSequencingSource();
            adSequencingSource.Format = format;
            adSequencingSource.Uri = adSource;

            var adTrigger = new AdSequencingTrigger();
            adTrigger.Duration = duration.GetValueOrDefault(TimeSpan.FromDays(1));
            adTrigger.Sources.Add(adSequencingSource);

            return ScheduleAdTrigger(adTrigger, startTime);
        }

        /// <summary>
        /// Schedules an ad that is to be handled by an AdPayloadHandlerPlugin.
        /// A valid AdPayloadHandlerPlugin must be part of your application or this will not be handled.
        /// </summary>
        /// <param name="adTrigger">An object containing information about the ad source and target</param>
        /// <param name="startTime">The position within the media where this ad should be played. If ommited ad will begin playing immediately.</param>
        /// <returns>An object that contains information about the scheduled ad.</returns>
        public ScheduledAd ScheduleAdTrigger(IAdSequencingTrigger adTrigger, TimeSpan? startTime = null)
        {
            var adStartTime = startTime.GetValueOrDefault(RelativeMediaPluginPosition);

            var result = new ScheduledAd(adTrigger);
            var adMarker = new AdMarker()
            {
                Immediate = !startTime.HasValue,
                Begin = adStartTime,
                Id = Guid.NewGuid().ToString(),
                ScheduledAd = result,
                End = adStartTime.Add(adTrigger.Duration.GetValueOrDefault(TimeSpan.FromDays(1)))    // update the end based on the duration
            };

            // Immediate == true will trigger the timeline marker immediately instead of waiting for polling to occur
            if (adMarker.Immediate)
            {
                var duration = adMarker.Duration;
                adMarker.Begin = RelativeMediaPluginPosition;
                adMarker.End = adMarker.Begin.Add(duration);    // update the end based on the duration
                AdMarkers.Add(adMarker);
                // force a check on the postions, we know there is one that needs to be fired
                if (!isSeekActive) _adMarkerManager.CheckMarkerPositions(RelativeMediaPluginPosition, AdMarkers, seekInProgress);
            }
            else
            {
                AdMarkers.Add(adMarker);
            }

            return result;
        }

        /// <summary>
        /// Removes a scheduled ad created by scheduling an ad trigger
        /// </summary>
        /// <param name="ScheduledAd"></param>
        public void RemoveScheduledAd(ScheduledAd ScheduledAd)
        {
            var marker = AdMarkers.FirstOrDefault(m => m.ScheduledAd == ScheduledAd);
            if (marker != null)
            {
                AdMarkers.Remove(marker);
            }
        }

        /// <summary>
        /// Goes to the next playlist item.
        /// </summary>
        public void GoToNextPlaylistItem()
        {
            if (CurrentPlaylistIndex.HasValue && Playlist != null)
            {
                int index = CurrentPlaylistIndex.Value == Playlist.Count - 1 && ContinuousPlay
                                ? 0
                                : CurrentPlaylistIndex.Value + 1;
                GoToPlaylistItem(index);
                SendLogEntry(KnownLogEntryTypes.GoToNextPlaylistItem);
            }
        }

        /// <summary>
        /// Goes to the previous playlist item.
        /// </summary>
        public void GoToPreviousPlaylistItem()
        {
            if (CurrentPlaylistIndex.HasValue)
            {
                int index = CurrentPlaylistIndex.Value == 0 && ContinuousPlay
                                ? Playlist.Count - 1
                                : CurrentPlaylistIndex.Value - 1;

                GoToPlaylistItem(index);
                SendLogEntry(KnownLogEntryTypes.GoToPreviousPlaylistItem);
            }
        }

        /// <summary>
        /// Goes to the playlist item at the specified Playlist index.
        /// </summary>
        public void GoToPlaylistItem(int index)
        {
            if (Playlist != null && index >= 0)
            {
                if (index < Playlist.Count)
                {
                    if (CurrentPlaylistItem == Playlist[index])
                    {
                        CurrentPlaylistItem = null;
                    }

                    CurrentPlaylistItem = Playlist[index];
                }
                else
                {
                    // we reached the end of the play list
                    PlaylistReachedEnd.IfNotNull(i => i(this, EventArgs.Empty));
                }
            }
        }

        /// <summary>
        /// Goes to the specified playlist item.
        /// </summary>
        public void GoToPlaylistItem(PlaylistItem playlistItem)
        {
            if (Playlist != null && playlistItem != null && playlistItem != CurrentPlaylistItem &&
                Playlist.Contains(playlistItem))
            {
                CurrentPlaylistItem = playlistItem;
            }
        }

        /// <summary>
        /// Goes to the next chapter item.
        /// </summary>
        public void GoToNextChapter()
        {
            if (ActiveMediaPlugin != null && CurrentPlaylistItem != null && CurrentPlaylistItem.Chapters.Count > 0)
            {
                CurrentPlaylistItem.Chapters
                    .Where(i => i.Begin > RelativeMediaPluginPosition)
                    .OrderBy(i => i.Begin)
                    .FirstOrDefault()
                    .IfNotNull(GoToChapterItem);

                SendLogEntry(KnownLogEntryTypes.GoToNextChapter);
            }
        }

        /// <summary>
        /// Goes to the previous chapter item.
        /// </summary>
        public void GoToPreviousChapter()
        {
            if (ActiveMediaPlugin != null && CurrentPlaylistItem != null && CurrentPlaylistItem.Chapters.Count > 0)
            {
                CurrentPlaylistItem.Chapters
                    .Where(i => i.Begin < RelativeMediaPluginPosition && i.End < RelativeMediaPluginPosition)
                    .OrderByDescending(i => i.Begin)
                    .FirstOrDefault()
                    .IfNotNull(GoToChapterItem);

                SendLogEntry(KnownLogEntryTypes.GoToPreviousChapter);
            }
        }

        /// <summary>
        /// Goes to the chapter item at the specified index.
        /// </summary>
        public void GoToChapterItem(int index)
        {
            if (CurrentPlaylistItem != null && index > 0 && index < CurrentPlaylistItem.Chapters.Count - 1)
            {
                Chapter chapter = CurrentPlaylistItem.Chapters[index];
                GoToChapterItem(chapter);
            }
        }

        /// <summary>
        /// Goes to the specified chapter item.
        /// </summary>
        public void GoToChapterItem(Chapter chapterItem)
        {
            if (chapterItem == null) throw new ArgumentNullException("chapterItem");

            if (Playlist != null && CurrentPlaylistItem != null)
            {
                if (!CurrentPlaylistItem.Chapters.Contains(chapterItem))
                {
                    PlaylistItem playlistItem = Playlist.Where(i => i.Chapters.Contains(chapterItem))
                        .FirstOrDefault();

                    if (playlistItem != null)
                    {
                        CurrentPlaylistItem = playlistItem;
                    }
                    else
                    {
                        throw new ArgumentException(
                            SilverlightMediaFrameworkResources.ChapterItemDoesNotBelongToCurrentPlaylistMessage);
                    }
                }

                UpdateChapterSelection(chapterItem);
                SeekToPosition(chapterItem.Begin);
            }
        }


        /// <summary>
        /// Seeks to the given time.
        /// </summary>
        /// <param name="seconds">Time to seek to.</param>
        public void SeekToPosition(double seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            SeekToPosition(timeSpan);
        }

        /// <summary>
        /// Seeks to the time passed in as a Timespan.
        /// </summary>
        /// <param name="position">Time to seek to.</param>
        public void SeekToPosition(TimeSpan position)
        {
            position = CalculateAbsoluteMediaPosition(position);

            if (IsMediaPluginPlayReady
                && ActiveMediaPlugin != null
                && ActiveMediaPlugin.CanSeek
                && position >= ActiveMediaPlugin.StartPosition
                && position <= ActiveMediaPlugin.EndPosition)
            {
                isSeekActive = true;
                PlaybackPosition = position;
                ActiveMediaPlugin.Position = position;
                UpdateTimelinePositions();
                SendLogEntry(KnownLogEntryTypes.SeekToPosition);
            }
        }

        /// <summary>
        /// Starts FastForward play of the video.
        /// </summary>
        public void StartFastForward()
        {
            if (IsMediaPluginPlayReady)
            {
                PlaySpeedManager.FastForward();
                SendLogEntry(KnownLogEntryTypes.StartFastForward);
            }
        }

        /// <summary>
        /// Stops FastForward play and restores the natural play speed of the video.
        /// </summary>
        public void StopFastForward()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsFastForwarding)
            {
                PlaySpeedManager.RestoreNaturalPlaySpeed();

                if (ActiveMediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    Play();
                }
                SendLogEntry(KnownLogEntryTypes.StopFastForward);
            }
        }

        /// <summary>
        /// Increment the fast forward speed to the next fastest supported value.
        /// </summary>
        public void IncrementFastForward()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsFastForwarding)
            {
                PlaySpeedManager.IncrementFastForward();
                SendLogEntry(KnownLogEntryTypes.IncrementFastForward);
            }
        }

        /// <summary>
        /// Decrement the fast forward speed to the next slowest supported value.
        /// </summary>
        public void DecrementFastForward()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsFastForwarding)
            {
                PlaySpeedManager.DecrementFastForward();
                SendLogEntry(KnownLogEntryTypes.DecrementFastForward);
            }
        }

        /// <summary>
        /// Starts rewinding of the video.
        /// </summary>
        public void StartRewind()
        {
            if (IsMediaPluginPlayReady)
            {
                PlaySpeedManager.Rewind();
                SendLogEntry(KnownLogEntryTypes.StartRewind);
            }
        }

        /// <summary>
        /// Stops rewind and restores the natural play speed of the video.
        /// </summary>
        public void StopRewind()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsRewinding)
            {
                PlaySpeedManager.RestoreNaturalPlaySpeed();

                if (ActiveMediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    Play();
                }
                SendLogEntry(KnownLogEntryTypes.StopRewind);
            }
        }

        /// <summary>
        /// Increment the rewind speed to the next fastest supported value.
        /// </summary>
        public void IncrementRewind()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsRewinding)
            {
                PlaySpeedManager.IncrementRewind();
                SendLogEntry(KnownLogEntryTypes.IncrementRewind);
            }
        }

        /// <summary>
        /// Increment the rewind speed to the next slowest supported value.
        /// </summary>
        public void DecrementRewind()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsRewinding)
            {
                PlaySpeedManager.DecrementRewind();
                SendLogEntry(KnownLogEntryTypes.DecrementRewind);
            }
        }

        /// <summary>
        /// Starts slowmotion playing of the video.
        /// </summary>
        public void StartSlowMotion()
        {
            if (IsMediaPluginPlayReady)
            {
                PlaySpeedManager.SlowMotion();
                SendLogEntry(KnownLogEntryTypes.StartSlowMotion);
            }
        }

        /// <summary>
        /// Stops slow motion and restores the natural play speed of the video.
        /// </summary>
        public void StopSlowMotion()
        {
            if (PlaySpeedManager != null && PlaySpeedManager.IsSlowMotion)
            {
                PlaySpeedManager.RestoreNaturalPlaySpeed();
                SendLogEntry(KnownLogEntryTypes.StopSlowMotion);
            }
        }

        /// <summary>
        /// Subtracts the amount of time specified by the ReplayOffset property from the current Position.
        /// </summary>
        public void Replay()
        {
            if (IsMediaPluginPlayReady)
            {
                TimeSpan newPosition = PlaybackPosition.Subtract(ReplayOffset);
                if (newPosition < StartPosition)
                {
                    newPosition = StartPosition;
                }

                SeekToPosition(newPosition);

                if (ActiveMediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    Play();
                }

                SendLogEntry(KnownLogEntryTypes.Replay);
            }
        }

        /// <summary>
        /// Sets the current play position to the current live position for a live video stream.
        /// </summary>
        public void SeekToLive()
        {
            if (IsMediaPluginPlayReady
                && IsLiveMediaSupported
                && ActiveLiveMediaPlugin.IsSourceLive
                && !ActiveLiveMediaPlugin.IsLivePosition)
            {
                var position = CalculateRelativeMediaPosition(ActiveLiveMediaPlugin.LivePosition);

                SeekToPosition(position);

                if (ActiveMediaPlugin != null && ActiveMediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    Play();
                }
                SendLogEntry(KnownLogEntryTypes.SeekToLive);
            }
        }

        private static PlaylistItem ImportExpressionEncoderPlaylistItem(XElement element)
        {
            var item = new PlaylistItem
            {
                Description = HttpUtility.UrlDecode(element.Element("Description").GetValue(""))
            };
            long? fileSize = element.Element("Description").GetValueAsLong();
            if (fileSize != null) item.FileSize = fileSize.Value;
            double? frameRate = element.Element("FrameRate").GetValueAsDouble();
            if (frameRate != null) item.FrameRate = frameRate.Value;
            double? height = element.Element("Height").GetValueAsDouble();
            if (height != null) item.VideoHeight = height.Value;
            double? width = element.Element("Width").GetValueAsDouble();
            if (width != null) item.VideoWidth = width.Value;
            bool? isAdaptiveStreaming = element.Element("IsAdaptiveStreaming").GetValueAsBoolean();
            if (isAdaptiveStreaming != null && isAdaptiveStreaming == true)
            {
                item.DeliveryMethod = DeliveryMethods.AdaptiveStreaming;
            }
            else
            {
                string deliveryMethod = element.Element("DeliveryMethod").GetValue();
                if (deliveryMethod != null)
                    item.DeliveryMethod = Enum.IsDefined(typeof(DeliveryMethods), deliveryMethod) ? (DeliveryMethods)Enum.Parse(typeof(DeliveryMethods), deliveryMethod, true) : DeliveryMethods.NotSpecified;
            }
            string mediaSource = element.Element("MediaSource").GetValue();
            if (mediaSource != null)
            {
                mediaSource = HttpUtility.UrlDecode(mediaSource);
                if (mediaSource != null)
                {
                    if (mediaSource.ToLower().StartsWith("http:") ||
                    mediaSource.ToLower().StartsWith("https:"))
                    {
                        item.MediaSource = new Uri(mediaSource, UriKind.Absolute);
                    }
                    else
                    {
                        item.MediaSource = new Uri(mediaSource, UriKind.Relative);
                    }
                }
            }
            string thumbSource = element.Element("ThumbSource").GetValue();
            if (thumbSource != null)
            {
                thumbSource = HttpUtility.UrlDecode(thumbSource);
                if (thumbSource != null)
                {
                    if (thumbSource.ToLower().StartsWith("http:") ||
                    thumbSource.ToLower().StartsWith("https:"))
                    {
                        item.ThumbSource = new Uri(thumbSource, UriKind.Absolute);
                    }
                    else
                    {
                        item.ThumbSource = new Uri(thumbSource, UriKind.Relative);
                    }
                }
            }
            item.Title = HttpUtility.UrlDecode(element.Element("Title").GetValue(""));

            XElement s3DPropItems = element.Element("S3DProperties");
            if (s3DPropItems != null)
            {
                item.S3DProperties = ImportHTMLS3DProperties(s3DPropItems);
            }

            XElement customMetaDataItems = element.Element("CustomMetadata");
            if (customMetaDataItems != null)
            {
                item.CustomMetadata = ImportHTMLCustomMetaData(customMetaDataItems);
            }

            return item;
        }

        private static MetadataCollection ImportHTMLCustomMetaData(XElement customMetaDataItems)
        {
            MetadataCollection metaDataCollection = new MetadataCollection();

            foreach (XElement customMetadataItem in customMetaDataItems.Elements())
            {
                metaDataCollection.Add(customMetadataItem.Name.ToString(), customMetadataItem.Value);
            }

            return metaDataCollection;
        }

        private static S3DProperties ImportHTMLS3DProperties(XElement s3DPropItems)
        {
            S3DProperties s3DProperties = new S3DProperties();

            foreach (XElement s3DPropSubItem in s3DPropItems.Elements())
            {
                switch (s3DPropSubItem.Name.ToString())
                {
                    case "S3DContent":
                        {
                            s3DProperties.S3DContent = Enum.IsDefined(typeof(S3D_Contents), s3DPropSubItem.Value) ? (S3D_Contents)Enum.Parse(typeof(S3D_Contents), s3DPropSubItem.Value, true) : S3D_Contents.None;
                            break;
                        }
                    case "S3DEyePriority":
                        {
                            s3DProperties.S3DEyePriority = Enum.IsDefined(typeof(S3D_EyePriorities), s3DPropSubItem.Value) ? (S3D_EyePriorities)Enum.Parse(typeof(S3D_EyePriorities), s3DPropSubItem.Value, true) : S3D_EyePriorities.LeftFirst;
                            break;
                        }
                    case "S3DFormat":
                        {
                            s3DProperties.S3DFormat = Enum.IsDefined(typeof(S3D_Formats), s3DPropSubItem.Value) ? (S3D_Formats)Enum.Parse(typeof(S3D_Formats), s3DPropSubItem.Value, true) : S3D_Formats.DiscreteTrack;
                            break;
                        }
                    case "S3DLeftEyePAR":
                        {
                            s3DProperties.S3DLeftEyePAR = (double)s3DPropSubItem.GetValueAsDouble();
                            break;
                        }
                    case "S3DRightEyePAR":
                        {
                            s3DProperties.S3DRightEyePAR = (double)s3DPropSubItem.GetValueAsDouble();
                            break;
                        }
                    case "S3DSubsamplingModes":
                        {
                            s3DProperties.S3DSubsamplingModes = Enum.IsDefined(typeof(S3D_SubsamplingModes), s3DPropSubItem.Value) ? (S3D_SubsamplingModes)Enum.Parse(typeof(S3D_SubsamplingModes), s3DPropSubItem.Value, true) : S3D_SubsamplingModes.None;
                            break;
                        }
                    case "S3DSubsamplingOrders":
                        {
                            s3DProperties.S3DSubsamplingOrders = Enum.IsDefined(typeof(S3D_SubsamplingOrders), s3DPropSubItem.Value) ? (S3D_SubsamplingOrders)Enum.Parse(typeof(S3D_SubsamplingOrders), s3DPropSubItem.Value, true) : S3D_SubsamplingOrders.None;
                            break;
                        }
                }
            }

            return s3DProperties;
        }

#if !WINDOWS_PHONE && !OOB
        /// <summary>
        /// Load settings from init params
        /// </summary>
        /// <param name="initParams"></param>
        public void LoadInitParams(IDictionary<string, string> initParams)
        {
            bool autoPlay;
            FeatureVisibility playerGraphVisibility;

            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.Title))
            {
                MediaTitleContent = initParams.GetEntryIgnoreCase(SupportedInitParams.Title);
            }

            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.ScriptableName)
                && !initParams.GetEntryIgnoreCase(SupportedInitParams.ScriptableName).IsNullOrWhiteSpace())
            {
                ScriptableName = initParams.GetEntryIgnoreCase(SupportedInitParams.ScriptableName);
            }

            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.AutoPlay)
                && bool.TryParse(initParams.GetEntryIgnoreCase(SupportedInitParams.AutoPlay), out autoPlay))
            {
                AutoPlay = autoPlay;
            }

            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.PlayerGraphVisibility)
                && FeatureVisibility.TryParse(initParams.GetEntryIgnoreCase(SupportedInitParams.PlayerGraphVisibility), true, out playerGraphVisibility))
            {
                PlayerGraphVisibility = playerGraphVisibility;
            }

            bool isStartPositionOffset = true;
            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.IsStartPositionOffset) && bool.TryParse(initParams.GetEntryIgnoreCase(SupportedInitParams.IsStartPositionOffset), out isStartPositionOffset))
            {
                IsStartPositionOffset = isStartPositionOffset;
            }

            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.MediaUrl)
                && !initParams.GetEntryIgnoreCase(SupportedInitParams.MediaUrl).IsNullOrWhiteSpace())
            {
                var playlist = new ObservableCollection<PlaylistItem>();

                var playlistItem = new PlaylistItem
                {
                    MediaSource = new Uri(initParams.GetEntryIgnoreCase(SupportedInitParams.MediaUrl))
                };


                DeliveryMethods deliveryMethod;
                if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.DeliveryMethod)
                    && initParams.GetEntryIgnoreCase(SupportedInitParams.DeliveryMethod).EnumTryParse(true, out deliveryMethod))
                {
                    playlistItem.DeliveryMethod = deliveryMethod;
                }


                if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.SelectedCaptionStream)
                    && !initParams.GetEntryIgnoreCase(SupportedInitParams.SelectedCaptionStream).IsNullOrWhiteSpace())
                {
                    playlistItem.SelectedCaptionStreamName = initParams.GetEntryIgnoreCase(SupportedInitParams.SelectedCaptionStream);
                }

                if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.ThumbnailUrl)
                    && !initParams.GetEntryIgnoreCase(SupportedInitParams.ThumbnailUrl).IsNullOrWhiteSpace())
                {
                    playlistItem.ThumbSource = new Uri(initParams.GetEntryIgnoreCase(SupportedInitParams.ThumbnailUrl));
                }

                playlist.Add(playlistItem);
                Playlist = playlist;
            }
            else if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.PlayerSettings)
                && !initParams.GetEntryIgnoreCase(SupportedInitParams.PlayerSettings).IsNullOrWhiteSpace())
            {
                ImportPlayerSettings(initParams.GetEntryIgnoreCase(SupportedInitParams.PlayerSettings));
            }

            Uri pluginSource;
            if (initParams.ContainsKeyIgnoreCase(SupportedInitParams.PluginUrl)
                && !initParams.GetEntryIgnoreCase(SupportedInitParams.PluginUrl).IsNullOrWhiteSpace()
                && Uri.TryCreate(initParams.GetEntryIgnoreCase(SupportedInitParams.PluginUrl), UriKind.RelativeOrAbsolute, out pluginSource))
            {
                BeginAddExternalPlugins(pluginSource);
            }
        }
#endif

        /// <summary>
        /// Loads settings from an Expression Encoder formatted XML string
        /// </summary>
        /// <param name="settings">The XML string containing the settings to be loaded.</param>
        private void ImportPlayerSettings(string settings)
        {
            try
            {
                XElement element = XElement.Parse(settings);

                bool? autoLoad = element.Element("AutoLoad").GetValueAsBoolean();
                if (autoLoad.HasValue) AutoLoad = autoLoad.Value;

                bool? autoPlay = element.Element("AutoPlay").GetValueAsBoolean();
                if (autoPlay.HasValue) AutoPlay = autoPlay.Value;

                bool? enableCachedComposition = element.Element("EnableCachedComposition").GetValueAsBoolean();
                if (enableCachedComposition.HasValue) EnableCachedComposition = enableCachedComposition.Value;

                bool? enableCaptions = element.Element("EnableCaptions").GetValueAsBoolean();
                CaptionsVisibility = (enableCaptions.HasValue && !enableCaptions.Value)
                                         ? FeatureVisibility.Disabled
                                         : FeatureVisibility.Hidden;

                bool? startMuted = element.Element("StartMuted").GetValueAsBoolean();
                if (startMuted != null) StartMuted = startMuted.Value;

                XElement items = element.Element("Items");
                if (items != null)
                {
                    var playlist = new ObservableCollection<PlaylistItem>();
                    foreach (XElement item in items.Elements())
                    {
                        PlaylistItem playlistItem = ImportExpressionEncoderPlaylistItem(item);
                        if (playlistItem != null) playlist.Add(playlistItem);
                    }
                    if (playlist.Count > 0) Playlist = playlist;
                }
            }
            catch (XmlException)
            {
                //Special Case - Encoder precompiled template, failing silently.
            }
        }

        #endregion

        //Allows a consuming application to manually restart the retry process.
        protected void StartRetry()
        {
            _retryMonitor.BeginRetrying(RetryDuration, RetryInterval, CurrentPlaylistItem.MediaSource,
                CurrentPlaylistItem.DeliveryMethod == DeliveryMethods.AdaptiveStreaming);
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            CurrentPlaylistItem = null;

            UnloadHeuristicsPlugin();
            UnloadGenericPlugins();
            UnloadS3DPlugins();
            UnloadUIPlugins();
            UnloadAdPayloadHandlerPlugins();

            OnDispose();

            if (_positionTimer.IsEnabled) _positionTimer.Stop();
            _videoDoubleClickMonitor.Dispose();
            _logger.Dispose();

            PluginsManager.UnloadPlugins();
        }

        partial void OnDispose();

        #endregion

        #region Advertising

        /// <summary>
        /// Indicates that if the media source is a live streaming source, the player should jump to the current live position if we were close to the live position to begin with.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool JumpToLiveAfterAd
        {
            get { return _adMarkerManager.JumpToLiveAfterAd; }
            set { _adMarkerManager.JumpToLiveAfterAd = value; }
        }

        /// <summary>
        /// Indicates that ads should still be triggered when seeked into
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool PlayAdsOnSeek
        {
            get { return _adMarkerManager.PlayAdsOnSeek; }
            set { _adMarkerManager.PlayAdsOnSeek = value; }
        }

        /// <summary>
        /// Indicates that ads should still be triggered when fast-forwarding or rewinding into.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool PlayAdsOnFwdRwd
        {
            get { return _adMarkerManager.PlayAdsOnFwdRwd; }
            set { _adMarkerManager.PlayAdsOnFwdRwd = value; }
        }

        private TimeSpan? adInitTimeout = null;
        /// <summary>
        /// The timeout for initializing/loading ads. Null indicates no timeout.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? AdInitTimeout
        {
            get { return adInitTimeout; }
            set
            {
                adInitTimeout = value;
                if (!IsInDesignMode)
                {
                    PluginsManager.AdPayloadHandlerPlugins.IfNotNull(p => p.Where(i => i.IsValueCreated).Select(i => i.Value).ForEach(i => i.InitTimeout = value));
                }
            }
        }

        private TimeSpan? adStartTimeout = null;
        /// <summary>
        /// The timeout for starting an ad. Null indicates no timeout.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? AdStartTimeout
        {
            get { return adStartTimeout; }
            set
            {
                adStartTimeout = value;
                if (!IsInDesignMode)
                {
                    PluginsManager.AdPayloadHandlerPlugins.IfNotNull(p => p.Where(i => i.IsValueCreated).Select(i => i.Value).ForEach(i => i.StartTimeout = value));
                }
            }
        }

        private TimeSpan? adStopTimeout = null;
        /// <summary>
        /// The timeout for stopping an ad. Null indicates no timeout.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        [TypeConverter(typeof(TimeSpanTypeConverter))]
        public TimeSpan? AdStopTimeout
        {
            get { return adStopTimeout; }
            set
            {
                adStopTimeout = value;
                if (!IsInDesignMode)
                {
                    PluginsManager.AdPayloadHandlerPlugins.IfNotNull(p => p.Where(i => i.IsValueCreated).Select(i => i.Value).ForEach(i => i.StopTimeout = value));
                }
            }
        }

        bool adCloseCompanionsOnComplete = false;
        /// <summary>
        /// Indicates that companion ads should be allowed to continue running after the ad has completed.
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public bool AdCloseCompanionsOnComplete
        {
            get { return adCloseCompanionsOnComplete; }
            set
            {
                adCloseCompanionsOnComplete = value;
                if (!IsInDesignMode)
                {
                    PluginsManager.AdPayloadHandlerPlugins.IfNotNull(p => p.Where(i => i.IsValueCreated).Select(i => i.Value).ForEach(i => i.CloseCompanionsOnComplete = value));
                }
            }
        }

        private FailurePolicyEnum adFailurePolicy = FailurePolicyEnum.Ignore;
        /// <summary>
        /// The failure strategy to use when handling an AdSequencingTrigger. Indicates what happens when a subset of an ad fails (such as when a companion ad fails).
        /// </summary>
        [Category("Advertising Properties")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [ScriptableMember]
        public FailurePolicyEnum AdFailurePolicy
        {
            get { return adFailurePolicy; }
            set
            {
                adFailurePolicy = value;
                if (!IsInDesignMode)
                {
                    PluginsManager.AdPayloadHandlerPlugins.IfNotNull(p => p.Where(i => i.IsValueCreated).Select(i => i.Value).ForEach(i => i.FailurePolicy = value));
                }
            }
        }

        internal protected void OnAdFailed(IAdSource AdSource)
        {
            AdFailed.IfNotNull(i => i(this, new CustomEventArgs<IAdSource>(AdSource)));
        }

        /// <summary>
        /// Raised when an ad fails. The FailurePolicy property will drive the criteria for when this is fired.
        /// </summary>
        public event EventHandler<CustomEventArgs<IAdSource>> AdFailed;

        internal protected void OnAdSucceeded(IAdSource AdSource)
        {
            AdSucceeded.IfNotNull(i => i(this, new CustomEventArgs<IAdSource>(AdSource)));
        }

        /// <summary>
        /// Raised when an ad fails. The FailurePolicy property will drive the criteria for when this is fired.
        /// </summary>
        public event EventHandler<CustomEventArgs<IAdSource>> AdSucceeded;

        /// <summary>
        /// Called by each ad payload handler
        /// </summary>
        private void AdPayloadHandlerPlugins_HandleCompleted(object sender, HandleCompletedEventArgs e)
        {
            if (e.Success)
                OnAdSucceeded(e.AdSource);
            else
                OnAdFailed(e.AdSource);
        }

        /// <summary>
        /// Raised when an ad marker is reached.
        /// </summary>
        public event EventHandler<AdMarkerReachedInfoEventArgs> AdMarkerReached;

        /// <summary>
        /// Raised when an ad marker has finished or is removed.
        /// </summary>
        public event EventHandler<AdMarkerEventArgs> AdMarkerLeft;

        /// <summary>
        /// Raised when an ad marker has been skipped.
        /// </summary>
        public event EventHandler<AdMarkerEventArgs> AdMarkerSkipped;

        /// <summary>
        /// Raised when the ad marker collection has changed.
        /// </summary>
        public event EventHandler AdMarkersChanged;

        /// <summary>
        /// An attached property to allow containers to be added to the SMFPlayer through Xaml
        /// </summary>
        public static readonly DependencyProperty ContainerHostProperty = DependencyProperty.RegisterAttached(
            "ContainerHost",              //Name of the property
            typeof(SMFPlayer),              //Type of the property
            typeof(FrameworkElement),       //Type of the provider of the registered attached property
            new PropertyMetadata(ContainerHostPropertyChanged));                          //Callback invoked in case the property value has changed

        /// <summary>
        /// Ads the element to the specified player's Containers collection. For programmatic use, add to the SMFPlayer.Containers collection is recommended.
        /// </summary>
        /// <param name="obj">The FrameworkElement we want to add to the player's Container collection.</param>
        /// <param name="player">The SMFPlayer whose Containers collection is to be added to.</param>
        public static void SetContainerHost(FrameworkElement obj, SMFPlayer player)
        {
            obj.SetValue(ContainerHostProperty, player);
        }

        /// <summary>
        /// Returns the SMFPlayer where the current container resides.
        /// </summary>
        /// <param name="obj">The FrameworkElement we want to add to the player's Container collection.</param>
        /// <returns>Returns the SMFPlayer where the current container resides.</returns>
        public static SMFPlayer GetContainerHost(FrameworkElement obj)
        {
            return (SMFPlayer)obj.GetValue(ContainerHostProperty);
        }

        private static void ContainerHostPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement)
            {
                var smfPlayer = e.NewValue as SMFPlayer;
                smfPlayer.Containers.Add((FrameworkElement)d);
            }
        }

        #endregion
    }
}
