using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.Progressive.Resources;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Progressive
{
    /// <summary>
    /// Represents a media plug-in that can play progressive download media.
    /// </summary>
    [ExportMediaPlugin(PluginName = PluginName,
        PluginDescription = PluginDescription,
        PluginVersion = PluginVersion,
        SupportedDeliveryMethods = SupportedDeliveryMethodsInternal,
        SupportedMediaTypes = new string[] { })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProgressiveMediaPlugin : IMediaPlugin
    {
        private const string PluginName = "ProgressiveMediaPlugin";

        private const string PluginDescription =
            "Provides Progressive Download capabilities for the Silverlight Media Framework by wrapping the MediaElement.";

        private const string PluginVersion = "2.2012.0605.0";

        private const DeliveryMethods SupportedDeliveryMethodsInternal =
            DeliveryMethods.ProgressiveDownload | DeliveryMethods.Streaming;

#if WINDOWS_PHONE || XBOX
        private const double SupportedPlaybackRate = 1;
        private static double[] supportedPlaybackRates = new double[] { SupportedPlaybackRate };
#else
        private static double[] supportedPlaybackRates = new double[] { -32, -16, -8, -4, -2, -1, 0, .5, 1, 2, 4, 8, 16, 32 };
#endif

        protected MediaElement MediaElement { get; set; }

        #region Events

        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when an exception occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when an exception occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        //IMediaPlugin Events

        /// <summary>
        /// Occurs when a seek operation has completed.
        /// </summary>
        public event Action<IMediaPlugin> SeekCompleted;

        /// <summary>
        /// Occurs when the percent of the media being buffered changes.
        /// </summary>
        public event Action<IMediaPlugin, double> BufferingProgressChanged;

        /// <summary>
        /// Occurs when the percent of the media downloaded changes.
        /// </summary>
        public event Action<IMediaPlugin, double> DownloadProgressChanged;

        /// <summary>
        /// Occurs when a marker defined for the media file has been reached.
        /// </summary>
        public event Action<IMediaPlugin, MediaMarker> MarkerReached;

        /// <summary>
        /// Occurs when the media reaches the end.
        /// </summary>
        public event Action<IMediaPlugin> MediaEnded;

        /// <summary>
        /// Occurs when the media does not open successfully.
        /// </summary>
        public event Action<IMediaPlugin, Exception> MediaFailed;

        /// <summary>
        /// Occurs when the media successfully opens.
        /// </summary>
        public event Action<IMediaPlugin> MediaOpened;

        /// <summary>
        /// Occurs when the state of playback for the media changes.
        /// </summary>
        public event Action<IMediaPlugin, MediaPluginState> CurrentStateChanged;

#pragma warning disable 67
        /// <summary>
        /// Occurs when the user clicks on an ad.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IAdContext> AdClickThrough;

        /// <summary>
        /// Occurs when there is an error playing an ad.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IAdContext> AdError;

        /// <summary>
        /// Occurs when the progress of the currently playing ad has been updated.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IAdContext, AdProgress> AdProgressUpdated;

        /// <summary>
        /// Occurs when the state of the currently playing ad has changed.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IAdContext> AdStateChanged;

        /// <summary>
        /// Occurs when the media's playback rate changes.
        /// </summary>
        public event Action<IMediaPlugin> PlaybackRateChanged;
#pragma warning restore 67

        #endregion

        #region Properties

        public CacheMode CacheMode
        {
            get { return MediaElement != null ? MediaElement.CacheMode : null; }
            set { MediaElement.IfNotNull(i => i.CacheMode = value); }
        }

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the media file starts to play immediately after it is opened.
        /// </summary>
        public bool AutoPlay
        {
            get { return MediaElement != null && MediaElement.AutoPlay; }
            set { MediaElement.IfNotNull(i => i.AutoPlay = value); }
        }

        /// <summary>
        /// Gets or sets the ratio of the volume level across stereo speakers.
        /// </summary>
        /// <remarks>
        /// The value is in the range between -1 and 1. The default value of 0 signifies an equal volume between left and right stereo speakers.
        /// A value of -1 represents 100 percent volume in the speakers on the left, and a value of 1 represents 100 percent volume in the speakers on the right. 
        /// </remarks>
        public double Balance
        {
            get { return MediaElement != null ? MediaElement.Balance : default(double); }
            set { MediaElement.IfNotNull(i => i.Balance = value); }
        }

        /// <summary>
        /// Gets a value indicating if the current media item can be paused.
        /// </summary>
        public bool CanPause
        {
            get { return MediaElement != null && MediaElement.CanPause; }
        }

        /// <summary>
        /// Gets a value indicating if the current media item allows seeking to a play position.
        /// </summary>
        public bool CanSeek
        {
            get { return MediaElement != null && MediaElement.CanSeek; }
        }

        /// <summary>
        /// Gets the total time of the current media item.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return MediaElement != null && MediaElement.NaturalDuration.HasTimeSpan
                           ? MediaElement.NaturalDuration.TimeSpan
                           : TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Gets the end time of the current media item.
        /// </summary>
        public TimeSpan EndPosition
        {
            get { return Duration; }
        }

        /// <summary>
        /// Gets or sets a value indicating if the current media item is muted so that no audio is playing.
        /// </summary>
        public bool IsMuted
        {
            get { return MediaElement != null && MediaElement.IsMuted; }
            set { MediaElement.IfNotNull(i => i.IsMuted = value); }
        }

        /// <summary>
        /// Gets or sets the LicenseAcquirer associated with the IMediaPlugin. 
        /// The LicenseAcquirer handles acquiring licenses for DRM encrypted content.
        /// </summary>
        public LicenseAcquirer LicenseAcquirer
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.LicenseAcquirer
                           : null;
            }
            set { MediaElement.IfNotNull(i => i.LicenseAcquirer = value); }
        }

        /// <summary>
        /// Gets the size value (unscaled width and height) of the current media item.
        /// </summary>
        public Size NaturalVideoSize
        {
            get
            {
                return MediaElement != null
                           ? new Size(MediaElement.NaturalVideoWidth, MediaElement.NaturalVideoHeight)
                           : Size.Empty;
            }
        }

        /// <summary>
        /// Gets the play speed of the current media item.
        /// </summary>
        /// <remarks>
        /// A rate of 1.0 is normal speed.
        /// </remarks>
#if WINDOWS_PHONE || XBOX
        public double PlaybackRate
        {
            get { return SupportedPlaybackRate; }
            set
            {
                if (value != SupportedPlaybackRate)
                {
                    throw new InvalidPlaybackRateException(value);
                }
            }
        }
#else
        public double PlaybackRate
        {
            get
            {
                return MediaElement != null ? MediaElement.PlaybackRate : 0;
            }
            set
            {
                if (MediaElement.PlaybackRate != value &&
                    (MediaElement.CurrentState == MediaElementState.Paused ||
                     MediaElement.CurrentState == MediaElementState.Playing ||
                     MediaElement.CurrentState == MediaElementState.Buffering))
                {
                    MediaElement.PlaybackRate = value;
                    PlaybackRateChanged.IfNotNull(i => i(this));
                }
            }
        }
#endif

        /// <summary>
        /// Gets the current state of the media item.
        /// </summary>
        public MediaPluginState CurrentState
        {
            get
            {
                return MediaElement != null
                           ? ConvertToPlayState(MediaElement.CurrentState)
                           : MediaPluginState.Stopped;
            }
        }

#if !WINDOWS_PHONE
        /// <summary>
        /// Gets the current position of the clip.
        /// </summary>
        public TimeSpan ClipPosition
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the media is being decoded by the GPU.
        /// </summary>
        public bool IsDecodingOnGPU
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.IsDecodingOnGPU
                           : false;
            }
        }

        /// <summary>
        /// Only used when a custom MediaStreamSource is set.
        /// </summary>
        public event EventHandler<MediaDrmSetupDecryptorCompletedEventArgs> MediaDrmSetupDecryptorCompleted;
#endif

        /// <summary>
        /// Gets the current position of the media item.
        /// </summary>
        public TimeSpan Position
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.Position
                           : TimeSpan.Zero;
            }
            set
            {
                if (MediaElement != null)
                {
                    MediaElement.Position = value;
                    SeekCompleted.IfNotNull(i => i(this));
                }
            }
        }

        /// <summary>
        /// Gets whether this plugin supports ad scheduling.
        /// </summary>
        public bool SupportsAdScheduling
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the start position of the current media item (0).
        /// </summary>
        public TimeSpan StartPosition
        {
            get { return TimeSpan.Zero; }
        }

        /// <summary>
        /// Gets the stretch setting for the current media item.
        /// </summary>
        public Stretch Stretch
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.Stretch
                           : default(Stretch);
            }
            set { MediaElement.IfNotNull(i => i.Stretch = value); }
        }

        /// <summary>
        /// Gets or sets a boolean value indicating whether to 
        /// enable GPU acceleration.  In the case of the Progressive
        /// MediaElement, the CacheMode being set to BitmapCache
        /// is the equivalent of setting EnableGPUAcceleration = true
        /// </summary>
        public bool EnableGPUAcceleration
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.CacheMode is BitmapCache
                           : false;
            }
            set
            {
                if (value)
                    MediaElement.IfNotNull(i => i.CacheMode = new BitmapCache());
                else
                    MediaElement.IfNotNull(i => i.CacheMode = null);
            }
        }

        /// <summary>
        /// Gets the delivery methods supported by this plugin.
        /// </summary>
        public DeliveryMethods SupportedDeliveryMethods
        {
            get { return SupportedDeliveryMethodsInternal; }
        }

        /// <summary>
        /// Gets a collection of the playback rates for the current media item.
        /// </summary>
        public IEnumerable<double> SupportedPlaybackRates
        {
            get { return supportedPlaybackRates; }
        }

        /// <summary>
        /// Gets a reference to the media player control.
        /// </summary>
        public FrameworkElement VisualElement
        {
            get { return MediaElement; }
        }

        /// <summary>
        /// Gets or sets the initial volume setting as a value between 0 and 1.
        /// </summary>
        public double Volume
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.Volume
                           : 0;
            }
            set { MediaElement.IfNotNull(i => i.Volume = value); }
        }

        /// <summary>
        /// Gets the dropped frames per second.
        /// </summary>
        public double DroppedFramesPerSecond
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.DroppedFramesPerSecond
                           : 0;
            }
        }

        /// <summary>
        /// Gets the rendered frames per second.
        /// </summary>
        public double RenderedFramesPerSecond
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.RenderedFramesPerSecond
                           : 0;
            }
        }

        /// <summary>
        /// Gets the percentage of the current buffering that is completed.
        /// </summary>
        public double BufferingProgress
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.BufferingProgress
                           : default(double);
            }
        }

        /// <summary>
        /// Gets or sets the amount of time for the current buffering action.
        /// </summary>
        public TimeSpan BufferingTime
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.BufferingTime
                           : TimeSpan.Zero;
            }
            set { MediaElement.IfNotNull(i => i.BufferingTime = value); }
        }

        /// <summary>
        /// Gets the percentage of the current buffering that is completed
        /// </summary>
        public double DownloadProgress
        {
            get { return MediaElement.DownloadProgress; }
        }

        /// <summary>
        /// Gets the download progress offset
        /// </summary>
        public double DownloadProgressOffset
        {
            get { return MediaElement.DownloadProgressOffset; }
        }

        /// <summary>
        /// Gets or sets the location of the media file.
        /// </summary>
        public Uri Source
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.Source
                           : null;
            }
            set
            {
#if !WINDOWS_PHONE && !RESTRICTEDACCESS
                if (Application.Current.IsRunningOutOfBrowser && value != null &&
                    value.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    StreamSource = GetLocalFileStream(value);
                }
                else
#endif
                {
                    MediaElement.IfNotNull(i => i.Source = value);
                }
            }
        }

        private Stream _streamSource;

        public Stream StreamSource
        {
            get { return _streamSource; }

            set
            {
                if (MediaElement != null)
                {
                    MediaElement.SetSource(value);
                    _streamSource = value;
                }
            }
        }

        #endregion




        /// <summary>
        /// Starts playing the current media file from its current position.
        /// </summary>
        public void Play()
        {
            MediaElement.IfNotNull(i => i.Play());
        }

        /// <summary>
        /// Pauses the currently playing media.
        /// </summary>
        public void Pause()
        {
            MediaElement.IfNotNull(i => i.Pause());
        }

        /// <summary>
        /// Stops playing the current media.
        /// </summary>
        public void Stop()
        {
            MediaElement.IfNotNull(i => i.Stop());
        }

        /// <summary>
        /// Loads a plug-in for playing progressive download media.
        /// </summary>
        public void Load()
        {
            try
            {
                InitializeProgressiveMediaElement();
                IsLoaded = true;
                PluginLoaded.IfNotNull(i => i(this));
                SendLogEntry(KnownLogEntryTypes.ProgressiveMediaPluginLoaded, message: ProgressiveMediaPluginResources.ProgressiveMediaPluginLoadedLogMessage);
            }
            catch (Exception ex)
            {
                PluginLoadFailed.IfNotNull(i => i(this, ex));
            }
        }

        /// <summary>
        /// Unloads a plug-in for  progressive download media.
        /// </summary>
        public void Unload()
        {
            try
            {
                IsLoaded = false;
                DestroyProgressiveMediaElement();
                PluginUnloaded.IfNotNull(i => i(this));
                SendLogEntry(KnownLogEntryTypes.ProgressiveMediaPluginUnloaded, message: ProgressiveMediaPluginResources.ProgressiveMediaPluginUnloadedLogMessage);
            }
            catch (Exception ex)
            {
                PluginUnloadFailed.IfNotNull(i => i(this, ex));
            }
        }

        /// <summary>
        /// Requests that this plugin generate a LogEntry via the LogReady event
        /// </summary>
        public void RequestLog()
        {
            MediaElement.IfNotNull(i => i.RequestLog());
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
        public IAdContext ScheduleAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? duration = null,
                                     TimeSpan? startTime = null, TimeSpan? startOffset = null, Uri clickThrough = null, bool pauseTimeline = true,
                                     IAdContext appendToAd = null, object data = null, bool isLinearClip = false)
        {
            throw new NotImplementedException();
        }

        private void InitializeProgressiveMediaElement()
        {
            if (MediaElement == null)
            {
                MediaElement = new MediaElement();
                MediaElement.MediaOpened += MediaElement_MediaOpened;
                MediaElement.MediaFailed += MediaElement_MediaFailed;
                MediaElement.MediaEnded += MediaElement_MediaEnded;
                MediaElement.CurrentStateChanged += MediaElement_CurrentStateChanged;
#if !WINDOWS_PHONE
                MediaElement.MarkerReached += MediaElement_MarkerReached;
#endif
                MediaElement.BufferingProgressChanged += MediaElement_BufferingProgressChanged;
                MediaElement.DownloadProgressChanged += MediaElement_DownloadProgressChanged;
                MediaElement.LogReady += MediaElement_LogReady;
            }
        }

        private void DestroyProgressiveMediaElement()
        {
            if (MediaElement != null)
            {
                MediaElement.MediaOpened -= MediaElement_MediaOpened;
                MediaElement.MediaFailed -= MediaElement_MediaFailed;
                MediaElement.MediaEnded -= MediaElement_MediaEnded;
                MediaElement.CurrentStateChanged -= MediaElement_CurrentStateChanged;
#if !WINDOWS_PHONE
                MediaElement.MarkerReached -= MediaElement_MarkerReached;
#endif
                MediaElement.BufferingProgressChanged -= MediaElement_BufferingProgressChanged;
                MediaElement.DownloadProgressChanged -= MediaElement_DownloadProgressChanged;
                MediaElement.LogReady -= MediaElement_LogReady;
                MediaElement.Source = null;
                MediaElement = null;
            }
        }

#if !WINDOWS_PHONE && !RESTRICTEDACCESS
        protected virtual Stream GetLocalFileStream(Uri fileSource)
        {
            return new FileStream(fileSource.LocalPath, FileMode.Open);
        }
#endif

        private void MediaElement_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            DownloadProgressChanged.IfNotNull(i => i(this, MediaElement.DownloadProgress));
        }

        private void MediaElement_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            BufferingProgressChanged.IfNotNull(i => i(this, MediaElement.BufferingProgress));
        }

        private void MediaElement_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
        {
            string logMessage = string.Format(ProgressiveMediaPluginResources.TimelineMarkerReached, e.Marker.Time,
                                              e.Marker.Type, e.Marker.Text);
            SendLogEntry(KnownLogEntryTypes.MediaElementMarkerReached, message: logMessage);

            var mediaMarker = new MediaMarker
                                  {
                                      Type = e.Marker.Type,
                                      Begin = e.Marker.Time,
                                      End = e.Marker.Time,
                                      Content = e.Marker.Text
                                  };

            NotifyMarkerReached(mediaMarker);
        }

        private void MediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            MediaPluginState playState = ConvertToPlayState(MediaElement.CurrentState);
            CurrentStateChanged.IfNotNull(i => i(this, playState));
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaEnded.IfNotNull(i => i(this));
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MediaFailed.IfNotNull(i => i(this, e.ErrorException));
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            MediaOpened.IfNotNull(i => i(this));
        }

        private void MediaElement_LogReady(object sender, LogReadyRoutedEventArgs e)
        {
            string message = string.Format(ProgressiveMediaPluginResources.MediaElementGeneratedLogMessageFormat,
                                           e.LogSource);
            var extendedProperties = new Dictionary<string, object> { { "Log", e.Log } };
            SendLogEntry(KnownLogEntryTypes.MediaElementLogReady, LogLevel.Statistics, message, extendedProperties: extendedProperties);
        }

        private void NotifyMarkerReached(MediaMarker mediaMarker)
        {
            MarkerReached.IfNotNull(i => i(this, mediaMarker));
        }

        private void SendLogEntry(string type, LogLevel severity = LogLevel.Information,
                                  string message = null,
                                  DateTime? timeStamp = null,
                                  IEnumerable<KeyValuePair<string, object>> extendedProperties = null)
        {
            if (LogReady != null)
            {
                var logEntry = new LogEntry
                                   {
                                       Type = type,
                                       Severity = severity,
                                       Message = message,
                                       SenderName = PluginName,
                                       Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                                   };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);
                LogReady(this, logEntry);
            }
        }

        private static MediaPluginState ConvertToPlayState(MediaElementState mediaElementState)
        {
            return (MediaPluginState)Enum.Parse(typeof(MediaPluginState), mediaElementState.ToString(), true);
        }
    }
}