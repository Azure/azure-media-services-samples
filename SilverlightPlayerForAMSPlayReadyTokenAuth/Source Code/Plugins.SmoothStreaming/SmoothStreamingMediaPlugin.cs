using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming.Resources;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// Represents a media plug-in that can play multiple-bitrate encoded adaptive media (Smooth Streaming).
    /// </summary>
    [ExportMediaPlugin(PluginName = PluginName,
        PluginDescription = PluginDescription,
        PluginVersion = PluginVersion,
        SupportsLiveDvr = true,
        SupportedDeliveryMethods = SupportedDeliveryMethodsInternal,
        SupportedMediaTypes = new string[] { }
        )]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SmoothStreamingMediaPlugin : IAdaptiveMediaPlugin, ILiveDvrMediaPlugin
    {
        private const string PluginName = "SmoothStreamingMediaPlugin";

        private const string PluginDescription =
            "Provides Smooth Streaming capabilities for the Silverlight Media Framework by wrapping the SmoothStreamingMediaElement.";

        private const string PluginVersion = "2.2012.0605.0";

        private const DeliveryMethods SupportedDeliveryMethodsInternal =
#if !WINDOWS_PHONE && !ADAPTIVE_ONLY
 DeliveryMethods.ProgressiveDownload |
#endif
#if !ADAPTIVE_ONLY
 DeliveryMethods.Streaming | 
#endif 
            DeliveryMethods.AdaptiveStreaming;

#if !WINDOWS_PHONE
        private readonly ChunkDownloadManager _chunkDownloadManager;
        private readonly IList<ScheduledAd> _scheduledAds;
        private LinearAdContext linearAdContext;
#endif
        private StreamSelectionManager _streamSelectionManager;
        private SeekCommand _seekCommand;
        private Stream _streamSource;

        public SmoothStreamingMediaPlugin()
        {
#if !WINDOWS_PHONE
            _scheduledAds = new List<ScheduledAd>();
            _chunkDownloadManager = new ChunkDownloadManager(this);
            _chunkDownloadManager.DownloadCompleted += ChunkDownloadManager_ChunkDownloadCompleted;
            _chunkDownloadManager.RetryingDownload += ChunkDownloadManager_RetryingChunkDownload;
            _chunkDownloadManager.DownloadExceededMaximumRetries += ChunkDownloadManager_ChunkDownloadExceededMaximumRetryAttempts;
#endif
            _seekCommand = new SeekCommand();
        }

        protected SmoothStreamingMediaElement MediaElement { get; private set; }

        //IPlugin Events

        #region IAdaptiveMediaPlugin Members

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
        /// 
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        //IMediaPlugin Events

        /// <summary>
        /// 
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
        /// Occurs when the PlaybackRate changes.
        /// </summary>
        public event Action<IMediaPlugin> PlaybackRateChanged;

        /// <summary>
        /// Occurs when the state of playback for the media changes.
        /// </summary>
        public event Action<IMediaPlugin, MediaPluginState> CurrentStateChanged;

        //IAdaptiveMediaPlugin Events
        /// <summary>
        /// Occurs when an error occurs during playback.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, Exception> AdaptiveStreamingErrorOccurred;

        /// <summary>
        /// Occurs when the manifest has been downloaded.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin> ManifestReady;

        /// <summary>
        /// Occurs when the bitrate used for playback changes.
        /// </summary>
        public event Action<IVariableBitrateMediaPlugin, long> VideoPlaybackBitrateChanged;

        /// <summary>
        /// Occurs when the bitrate used for playback changes.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaTrack> VideoPlaybackTrackChanged;

        /// <summary>
        /// Occurs when the bitrate used for downloading changes.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaTrack> VideoDownloadTrackChanged;

        //IAdaptiveMediaPlugin Events

        /// <summary>
        /// Occurs when a stream has been selected.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaStream> StreamSelected;

        /// <summary>
        /// Occurs when a stream has been unselected.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaStream> StreamUnselected;

        /// <summary>
        /// Occurs when the selection of a stream fails.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IEnumerable<IMediaStream>, Exception> StreamSelectionFailed;

        /// <summary>
        /// Occurs when data has been added to a stream.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaStream, IDataChunk> StreamDataAdded;

        /// <summary>
        /// Occurs when data has been removed from a stream.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaStream, TimeSpan> StreamDataRemoved;

        /// <summary>
        /// Occurs when the download of data from a stream fails.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaTrack, IDataChunk, Exception> DownloadStreamDataFailed;

        /// <summary>
        /// Occurs when the download of data from a stream completes.
        /// </summary>
        public event Action<IAdaptiveMediaPlugin, IMediaTrack, IStreamDownloadResult> DownloadStreamDataCompleted;

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

#if !WINDOWS_PHONE
        /// <summary>
        /// Occurs when a chunk failed to download.
        /// </summary>
        public event EventHandler<DataChunkDownloadedEventArgs> ChunkDownloadFailed;
#endif

        //ILiveMediaPlugin Events

        /// <summary>
        /// Starts playing the current media file from its current position.
        /// </summary>
        public void Play()
        {
            if (MediaElement != null)
            {
                if (_seekCommand.IsSeeking)
                {
                    _seekCommand.Play = true;
                }
                else
                {
                    try
                    {
                        MediaElement.Play();
                    }
                    catch (InvalidOperationException)
                    {
                        //SSME will fail if this is called shortly after setting the source. In this case, setting AutoPlay=true will cause it to play as soon as it's ready
                        AutoPlay = true;
                    }
                }
            }
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
            if (MediaElement != null
                && (MediaElement.Source != null || MediaElement.SmoothStreamingSource != null))
            {
                try
                {
                    MediaElement.Stop();
                }
                catch (InvalidOperationException)
                {
                    //SSME does strange things sometimes, we'll ignore this.
                }
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
        /// Empties the contents of the buffer leaving only the specified amount of buffered time.
        /// </summary>
        /// <param name="timeSpan">The amount of time to leave in the buffer.</param>
        public void ClearBuffer(TimeSpan timeSpan)
        {
            MediaElement.IfNotNull(i => i.FlushBuffers(timeSpan, true, true));
        }

        /// <summary>
        /// Loads a plug-in for playing adaptive media.
        /// </summary>
        public void Load()
        {
            try
            {
                InitializeSmoothStreamingMediaElement();
                IsLoaded = true;
                SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaPluginLoaded, message: SmoothStreamingResources.SmoothStreamingMediaPluginLoadedLogMessage);
                PluginLoaded.IfNotNull(i => i(this));
            }
            catch (Exception err)
            {
                IsLoaded = false;
                string message =
                    string.Format(SmoothStreamingResources.SmoothStreamingMediaPluginLoadFailedLogMessage,
                                  err.Message);
                SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaPluginErrorOccurred, LogLevel.Error, message);
                PluginLoadFailed.IfNotNull(i => i(this, err));
            }
        }

        /// <summary>
        /// Unloads a plug-in for adaptive media.
        /// </summary>
        public void Unload()
        {
            try
            {
                IsLoaded = false;
#if !WINDOWS_PHONE
                _chunkDownloadManager.Dispose();
                _scheduledAds.Clear();
                linearAdContext = null;
#endif
                UnloadManifest();
                DestroySmoothStreamingMediaElement();
                PluginUnloaded.IfNotNull(i => i(this));
                SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaPluginUnloaded, message: SmoothStreamingResources.SmoothStreamingMediaPluginUnloadedLogMessage);
            }
            catch (Exception err)
            {
                string message =
                    string.Format(SmoothStreamingResources.SmoothStreamingMediaPluginUnloadFailedLogMessage,
                                  err.Message);
                SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaPluginErrorOccurred, severity: LogLevel.Error, message: message);
                PluginUnloadFailed.IfNotNull(i => i(this, err));
            }
        }


        /// <summary>
        /// Downloads all of the available data from the specified track.
        /// </summary>
        /// <param name="track">the track that contains the data to be downloaded.</param>
        public void DownloadStreamData(IMediaTrack track)
        {
#if !WINDOWS_PHONE
            var mediaTrack = track as MediaTrack;
            if (mediaTrack != null)
            {
                _chunkDownloadManager.AddRequests(mediaTrack.ParentStream.DataChunks.Select(chunk => new Tuple<MediaTrack, TimeSpan>(mediaTrack, chunk.Timestamp)));
            }
#endif
        }

        public void CancelDownloadStreamData(IMediaTrack track)
        {
#if !WINDOWS_PHONE
            var mediaTrack = track as MediaTrack;
            if (mediaTrack != null)
            {
                _chunkDownloadManager.RemoveRequests(mediaTrack);
            }
#endif
        }

        /// <summary>
        /// Downloads the chunk of data that is part of the specified track and has the specified timestamp id.
        /// </summary>
        /// <param name="track">the track that contains the data to be downloaded.</param>
        /// <param name="chunk">the chunk to be downloaded.</param>
        public void DownloadStreamData(IMediaTrack track, IDataChunk chunk)
        {
#if !WINDOWS_PHONE
            var mediaTrack = track as MediaTrack;

            if (mediaTrack != null)
            {
                _chunkDownloadManager.AddRequest(mediaTrack, chunk.Timestamp);
            }
#endif
        }

        /// <summary>
        /// Specifies the range of video bitrates that can be downloaded.
        /// </summary>
        /// <param name="minimumVideoBitrate">the minimum bitrate that can be downloaded.</param>
        /// <param name="maximumVideoBitrate">the maximum bitrate that can be downloaded.</param>
        /// <param name="flushBuffer">flush the buffer</param>
        public void SetVideoBitrateRange(long minimumVideoBitrate, long maximumVideoBitrate, bool flushBuffer = true)
        {
            if (CurrentSegment != null)
            {
                MediaStream videoStream = CurrentSegment.SelectedStreams.Where(i => i.Type == StreamType.Video)
                    .Cast<MediaStream>()
                    .FirstOrDefault();

                if (videoStream != null)
                {
                    List<TrackInfo> selectedTracks = videoStream.StreamInfo.AvailableTracks
                        .Where(i => (long)i.Bitrate >= minimumVideoBitrate && (long)i.Bitrate <= maximumVideoBitrate)
                        .ToList();

                    if (selectedTracks.Any())
                    {
                        videoStream.StreamInfo.SelectTracks(selectedTracks, flushBuffer);
                    }
                }
            }
        }

        /// <summary>
        /// Specifies the cache provider that should be used.
        /// </summary>
        /// <param name="cacheProvider">the cache provider</param>
        public void SetCacheProvider(object cacheProvider)
        {
            if (MediaElement != null)
            {
                MediaElement.SmoothStreamingCache = cacheProvider as ISmoothStreamingCache;
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
        /// <param name="isLinearClip">Indicates that SSME.ScheduleLinearClip should be called instead of ScheduleClip. pauseTimeline must be false.</param>
        /// <returns>A reference to the IAdContext that contains information about the scheduled ad.</returns>
        public IAdContext ScheduleAd(Uri adSource, DeliveryMethods deliveryMethod, TimeSpan? duration = null,
                                     TimeSpan? startTime = null, TimeSpan? startOffset = null, Uri clickThrough = null, bool pauseTimeline = true,
                                     IAdContext appendToAd = null, object data = null, bool isLinearClip = false)
        {
#if !WINDOWS_PHONE
            if (adSource == null) throw new ArgumentNullException("adSource");

            Duration clipDuration = duration.HasValue
                                        ? new Duration(duration.Value)
                                        : TimeSpan.MaxValue;
            var adContext = appendToAd as AdContext;
            bool isSmoothStreamingSource = deliveryMethod == DeliveryMethods.AdaptiveStreaming;
            var clipInformation = new ClipInformation(isSmoothStreamingSource, adSource, clickThrough, clipDuration);

            var scheduledAd = new ScheduledAd
                                  {
                                      IsLinearClip = isLinearClip,
                                      ClipInformation = clipInformation,
                                      StartTime = startTime,
                                      StartOffset = startOffset,
                                      PauseTimeline = pauseTimeline,
                                      Data = data,
                                      AppendToAd = appendToAd != null
                                                       ? adContext.ScheduledAd
                                                       : null
                                  };

            if (CurrentState == MediaPluginState.Closed)
            {
                //Queue the ads until ManifestReady fires
                _scheduledAds.Add(scheduledAd);
            }
            else
            {
                ScheduleAd(scheduledAd);
            }

            if (isLinearClip)
            {
                return new LinearAdContext(scheduledAd, MediaElement, data);
            }
            else
            {
                return new AdContext(scheduledAd, data);
            }
#else
            throw new NotImplementedException();
#endif
        }


        public void SetSegmentSelectedStreams(ISegment segment, IEnumerable<IMediaStream> streams)
        {
            var seg = segment as Segment;
            var mediaStreams = streams.Cast<MediaStream>().ToList();

            if (_streamSelectionManager != null && seg != null)
            {
                _streamSelectionManager.IfNotNull(i => i.EnqueueRequest(seg, mediaStreams));
            }
        }

        public void ModifySegmentSelectedStreams(ISegment segment, IEnumerable<IMediaStream> streamsToAdd, IEnumerable<IMediaStream> streamsToRemove)
        {
            var seg = segment as Segment;
            var mediaStreamsToAdd = streamsToAdd == null ? Enumerable.Empty<MediaStream>() : streamsToAdd.Cast<MediaStream>().ToList();
            var mediaStreamsToRemove = streamsToRemove == null ? Enumerable.Empty<MediaStream>() : streamsToRemove.Cast<MediaStream>().ToList();

            if (_streamSelectionManager != null && seg != null)
            {
                _streamSelectionManager.IfNotNull(i => i.EnqueueRequest(seg, mediaStreamsToAdd, mediaStreamsToRemove));
            }
        }

        #endregion

        #region ILiveDvrMediaPlugin Members
        /// <summary>
        /// Occurs when a live event has completed.
        /// </summary>
        public event Action<ILiveDvrMediaPlugin> LiveEventCompleted;

        /// <summary>
        /// Attempts to set current playback position to the LivePosition.
        /// </summary>
        /// <remarks>
        /// This method is used on a live media stream with DVR capabilities, such as an adaptive stream (where the client can scrub to a 
        /// point on the timeline previous to the live position).
        /// </remarks>
        public void SeekToLive()
        {
            if (MediaElement != null)
            {
                if (_seekCommand.IsSeeking)
                {
                    _seekCommand.StartSeekToLive = true;
                }
                else
                {
                    MediaElement.StartSeekToLive();
                }
            }
        }

        #endregion

#if HACKMODE
        static int NameIndex = 0;
        public static string GetNewName()
        {
            NameIndex++;
            return string.Format("SSME{0}", NameIndex);
        }
#endif

        private void InitializeSmoothStreamingMediaElement()
        {
            if (MediaElement == null)
            {
                _seekCommand = new SeekCommand();

                MediaElement = new SmoothStreamingMediaElement();
#if HACKMODE
                MediaElement.Name = GetNewName(); // TODO: remove HACK once SSME is fixed in LV
#endif
#if !WINDOWS_PHONE
                MediaElement.ConfigPath = "Config.xml";
#endif
                MediaElement.ManifestReady += MediaElement_ManifestReady;
                MediaElement.PlaybackTrackChanged += MediaElement_PlaybackTrackChanged;
                MediaElement.MediaOpened += MediaElement_MediaOpened;
                MediaElement.MediaFailed += MediaElement_MediaFailed;
                MediaElement.MediaEnded += MediaElement_MediaEnded;
                MediaElement.CurrentStateChanged += MediaElement_CurrentStateChanged;
                MediaElement.BufferingProgressChanged += MediaElement_BufferingProgressChanged;
                MediaElement.DownloadProgressChanged += MediaElement_DownloadProgressChanged;
                MediaElement.LogReady += MediaElement_LogReady;
                MediaElement.SmoothStreamingErrorOccurred += MediaElement_SmoothStreamingErrorOccurred;
                MediaElement.SeekCompleted += MediaElement_SeekCompleted;
                MediaElement.LiveEventCompleted += MediaElement_LiveEventCompleted;
                MediaElement.DownloadTrackChanged += MediaElement_DownloadTrackChanged;
#if !WINDOWS_PHONE
                MediaElement.SetPlaybackRangeCompleted += MediaElement_SetPlaybackRangeCompleted;
                MediaElement.DrmSetupDecryptorCompleted += MediaElement_DrmSetupDecryptorCompleted;
                MediaElement.MarkerReached += MediaElement_MarkerReached;
                MediaElement.ClipClickThrough += MediaElement_ClipClickThrough;
                MediaElement.ClipError += MediaElement_ClipError;
                MediaElement.ClipProgressUpdate += MediaElement_ClipProgressUpdate;
                MediaElement.ClipStateChanged += MediaElement_ClipStateChanged;
                MediaElement.LinearClipChanged += MediaElement_LinearClipChanged;
                MediaElement.ChunkDownloadFailed += MediaElement_ChunkDownloadFailed;
#endif

                SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaElementInstantiated,
                    message: SmoothStreamingResources.SmoothStreamingMediaPluginInstantiatedLogMessage);
            }
        }

        private void DestroySmoothStreamingMediaElement()
        {
            if (MediaElement != null)
            {
                MediaElement.ManifestReady -= MediaElement_ManifestReady;
                MediaElement.PlaybackTrackChanged -= MediaElement_PlaybackTrackChanged;
                MediaElement.MediaOpened -= MediaElement_MediaOpened;
                MediaElement.MediaFailed -= MediaElement_MediaFailed;
                MediaElement.MediaEnded -= MediaElement_MediaEnded;
                MediaElement.CurrentStateChanged -= MediaElement_CurrentStateChanged;
                MediaElement.BufferingProgressChanged -= MediaElement_BufferingProgressChanged;
                MediaElement.DownloadProgressChanged -= MediaElement_DownloadProgressChanged;
                MediaElement.LogReady -= MediaElement_LogReady;
                MediaElement.SmoothStreamingErrorOccurred -= MediaElement_SmoothStreamingErrorOccurred;
                MediaElement.SeekCompleted -= MediaElement_SeekCompleted;
                MediaElement.LiveEventCompleted -= MediaElement_LiveEventCompleted;
                MediaElement.DownloadTrackChanged -= MediaElement_DownloadTrackChanged;
#if !WINDOWS_PHONE
                MediaElement.SetPlaybackRangeCompleted -= MediaElement_SetPlaybackRangeCompleted;
                MediaElement.DrmSetupDecryptorCompleted -= MediaElement_DrmSetupDecryptorCompleted;
                MediaElement.MarkerReached -= MediaElement_MarkerReached;
                MediaElement.ClipClickThrough -= MediaElement_ClipClickThrough;
                MediaElement.ClipError -= MediaElement_ClipError;
                MediaElement.ClipProgressUpdate -= MediaElement_ClipProgressUpdate;
                MediaElement.ClipStateChanged -= MediaElement_ClipStateChanged;
                MediaElement.LinearClipChanged -= MediaElement_LinearClipChanged;
                MediaElement.ChunkDownloadFailed -= MediaElement_ChunkDownloadFailed;
#endif
                MediaElement.Dispose();
                MediaElement = null;
            }
        }

#if !WINDOWS_PHONE
        private void ScheduleAd(ScheduledAd scheduledAd)
        {
            if (!scheduledAd.IsScheduled)
            {
                if (scheduledAd.AppendToAd != null && !scheduledAd.AppendToAd.IsScheduled)
                {
                    ScheduleAd(scheduledAd.AppendToAd);
                }

                if (scheduledAd.StartTime.HasValue)
                {
                    if (!scheduledAd.IsLinearClip)
                    {
                        scheduledAd.ClipContext = MediaElement.ScheduleClip(scheduledAd.ClipInformation,
                                                                            scheduledAd.StartTime.Value,
                                                                            scheduledAd.PauseTimeline, scheduledAd.Data);
                    }
                    else
                    {
                        scheduledAd.ClipContext = MediaElement.ScheduleLinearClip(scheduledAd.ClipInformation,
                                                                                scheduledAd.StartTime.Value,
                                                                                scheduledAd.Data);
                    }
                }
                else if (scheduledAd.StartOffset.HasValue)
                {
                    scheduledAd.ClipContext = MediaElement.ScheduleClip(scheduledAd.ClipInformation,
                                                                        scheduledAd.PauseTimeline,
                                                                        scheduledAd.StartOffset.Value,
                                                                        scheduledAd.Data);
                }
                else if (scheduledAd.AppendToAd != null)
                {
                    scheduledAd.ClipContext = MediaElement.ScheduleClip(scheduledAd.ClipInformation,
                                                                        scheduledAd.AppendToAd.ClipContext, scheduledAd.Data);
                }
                else
                {
                    scheduledAd.ClipContext = MediaElement.ScheduleClip(scheduledAd.ClipInformation,
                                                                        scheduledAd.PauseTimeline, scheduledAd.Data);
                }
            }
        }
#endif

#if !WINDOWS_PHONE && !RESTRICTEDACCESS
        protected virtual Stream GetLocalFileStream(Uri fileSource)
        {
            return new FileStream(fileSource.LocalPath, FileMode.Open);
        }
#endif

#if !WINDOWS_PHONE

        private void MediaElement_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
        {
            string logMessage = string.Format(SmoothStreamingResources.TimelineMarkerReached, e.Marker.Time,
                                              e.Marker.Type, e.Marker.Text);
            SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaElementMarkerReached, message: logMessage);

            var mediaMarker = new MediaMarker
            {
                Type = e.Marker.Type,
                Begin = e.Marker.Time,
                End = e.Marker.Time,
                Content = e.Marker.Text
            };

            NotifyMarkerReached(mediaMarker);
        }

        public event EventHandler<MediaSetPlaybackRangeCompletedEventArgs> SetPlaybackRangeCompleted;

        public void SetPlaybackRangeAsync(TimeSpan leftEdge, TimeSpan rightEdge, object userState)
        {
            MediaElement.SetPlaybackRangeAsync(leftEdge, rightEdge, userState);
        }

        void MediaElement_SetPlaybackRangeCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs args)
        {
            var eventArgs = args as SetPlaybackRangeCompletedEventArgs;
            SetPlaybackRangeCompleted.IfNotNull(i => i(this, new MediaSetPlaybackRangeCompletedEventArgs(eventArgs.LeftEdge, eventArgs.RightEdge, eventArgs.Error, eventArgs.Cancelled, eventArgs.UserState)));
        }

        /// <summary>
        /// Occurs when the DrmSetupDecryptorCompleted event completes, regardless if the setup succeeded or failed.
        /// </summary>
        public event EventHandler<MediaDrmSetupDecryptorCompletedEventArgs> MediaDrmSetupDecryptorCompleted;

        void MediaElement_DrmSetupDecryptorCompleted(object sender, SSMEDrmSetupDecryptorCompletedEventArgs e)
        {
            MediaDrmSetupDecryptorCompleted.IfNotNull(i => i(this, new MediaDrmSetupDecryptorCompletedEventArgs(e.KeyId, new DataChunk(e.ChunkInfo), e.Error, e.Cancelled, e.UserState)));
        }

        void MediaElement_ChunkDownloadFailed(object sender, ChunkDownloadedEventArgs e)
        {
            ChunkDownloadFailed.IfNotNull(i => i(this, new DataChunkDownloadedEventArgs(e)));
        }

        void MediaElement_LinearClipChanged(object sender, ClipEventArgs e)
        {
            if (MediaElement.CurrentLinearClipContext != null)
            {
                CurrentStateChanged.IfNotNull(i => i(this, MediaPluginState.ClipPlaying));
                linearAdContext = new LinearAdContext(e.Context, MediaElement);
                AdStateChanged.IfNotNull(i => i(this, linearAdContext));
            }
            else
            {
                AdStateChanged.IfNotNull(i => i(this, linearAdContext));
                linearAdContext = null;
                CurrentStateChanged.IfNotNull(i => i(this, CurrentState));
            }
        }

        private void MediaElement_ClipStateChanged(object sender, ClipEventArgs e)
        {
            var adContext = new AdContext(e.Context);
            AdStateChanged.IfNotNull(i => i(this, adContext));
        }

        private void MediaElement_ClipProgressUpdate(object sender, ClipPlaybackEventArgs e)
        {
            AdProgress adProgress;
#if SILVERLIGHT3
            if (AdProgressUpdated != null && SystemExtensions.TryParse(e.Progress.ToString(), true, out adProgress))
#else
            if (AdProgressUpdated != null && Enum.TryParse(e.Progress.ToString(), true, out adProgress))
#endif
            {
                var adContext = new AdContext(e.Context);
                AdProgressUpdated(this, adContext, adProgress);
            }
        }

        private void MediaElement_ClipError(object sender, ClipEventArgs e)
        {
            if (AdError != null)
            {
                var adContext = new AdContext(e.Context);
                AdError(this, adContext);
            }
        }

        private void MediaElement_ClipClickThrough(object sender, ClipEventArgs e)
        {
            if (AdClickThrough != null)
            {
                var adContext = new AdContext(e.Context);
                AdClickThrough(this, adContext);
            }
        }

        private void ChunkDownloadManager_RetryingChunkDownload(ChunkDownloadManager chunkDownloadManager, MediaTrack mediaTrack, TimeSpan chunkTimestamp)
        {
            SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaElementDownloadRetry);
        }

        private void ChunkDownloadManager_ChunkDownloadExceededMaximumRetryAttempts(ChunkDownloadManager chunkDownloadManager, MediaTrack mediaTrack, TimeSpan chunkTimestamp)
        {
            SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaElementDownloadExceededMaximumRetryAttempts, LogLevel.Error);

            DownloadStreamDataFailed.IfNotNull(i => i(this, mediaTrack, null, new DownloadStreamDataTimeOutException()));
        }

        private void ChunkDownloadManager_ChunkDownloadCompleted(ChunkDownloadManager downloadManager, MediaTrack track,
                                                                 TimeSpan timestamp, ChunkResult result)
        {
            try
            {
                var dataChunk = track.ParentStream.DataChunks.First(i => i.Timestamp == timestamp);

                if (result.Result == ChunkResult.ChunkResultState.Succeeded)
                {
                    var downloadResult = new StreamDownloadResult
                                             {
                                                 DataChunk = dataChunk,
                                                 Stream = result.ChunkData,
                                                 Type = track.ParentStream.Type
                                             };

                    DownloadStreamDataCompleted.IfNotNull(i => i(this, track, downloadResult));
                }
                else
                {
                    Exception err = result.Error ?? new DownloadStreamDataTimeOutException();

                    DownloadStreamDataFailed.IfNotNull(i => i(this, track, dataChunk, err));
                }
            }
            catch (Exception ex)
            {
                DownloadStreamDataFailed.IfNotNull(i => i(this, track, null, ex));
            }
        }
#endif

        private void MediaElement_DownloadTrackChanged(object sender, TrackChangedEventArgs e)
        {
            if (e.StreamType == MediaStreamType.Video)
            {
                VideoDownloadTrackChanged.IfNotNull(i => i(this, new MediaTrack(e.NewTrack)));
            }
        }

        private void MediaElement_ManifestReady(object sender, EventArgs e)
        {
            UnloadManifest();

#if !WINDOWS_PHONE
            MediaElement.ManifestInfo.ChunkListChanged += ManifestInfo_ChunkListChanged;
#endif

            _streamSelectionManager = new StreamSelectionManager(MediaElement.ManifestInfo);
            _streamSelectionManager.StreamSelectionCompleted += StreamSelectionManager_StreamSelectionCompleted;
            _streamSelectionManager.StreamSelectionExceededMaximumRetries += StreamSelectionManager_StreamSelectionExceededMaximumRetries;
#if !WINDOWS_PHONE
            _scheduledAds.Where(i => !i.IsScheduled)
                .ForEach(ScheduleAd);
            _scheduledAds.Clear();
#endif
            ManifestReady.IfNotNull(i => i(this));
        }

        private void UnloadManifest()
        {
#if !WINDOWS_PHONE
            if (MediaElement != null && MediaElement.ManifestInfo != null)
            {
                MediaElement.ManifestInfo.ChunkListChanged -= ManifestInfo_ChunkListChanged;
            }
#endif
            if (_streamSelectionManager != null)
            {
                _streamSelectionManager.StreamSelectionCompleted -= StreamSelectionManager_StreamSelectionCompleted;
                _streamSelectionManager.StreamSelectionExceededMaximumRetries -= StreamSelectionManager_StreamSelectionExceededMaximumRetries;
                _streamSelectionManager.Dispose();
                _streamSelectionManager = null;
            }
        }

        private void StreamSelectionManager_StreamSelectionExceededMaximumRetries(StreamSelectionManager streamSelectionManager, Segment segment, IEnumerable<MediaStream> streams)
        {
            StreamSelectionFailed.IfNotNull(i => i(this, streams.Cast<IMediaStream>(), new StreamSelectionTimeOutException()));
        }

        private void StreamSelectionManager_StreamSelectionCompleted(StreamSelectionManager streamSelectionManager, Segment segment, IEnumerable<MediaStream> streams, StreamUpdatedListEventArgs e)
        {
            if (e.Error == null)
            {
                foreach (StreamUpdatedEventArgs update in e.StreamUpdatedEvents)
                {
                    if (update.Action == StreamUpdatedEventArgs.StreamUpdatedAction.StreamSelected)
                    {
                        StreamSelected.IfNotNull(i => i(this, new MediaStream(update.Stream)));
                    }
                    else if (update.Action == StreamUpdatedEventArgs.StreamUpdatedAction.StreamDeselected)
                    {
                        StreamUnselected.IfNotNull(i => i(this, new MediaStream(update.Stream)));
                    }
                }
            }
            else
            {
                StreamSelectionFailed.IfNotNull(i => i(this, streams.Cast<IMediaStream>(), e.Error));
            }
        }


        private void ManifestInfo_ChunkListChanged(object sender, StreamUpdatedEventArgs e)
        {
            if (e.Action == StreamUpdatedEventArgs.StreamUpdatedAction.ChunkAdded)
            {
                if (StreamDataAdded != null && e.Timestamp.HasValue)
                {
                    var mediaStream = new MediaStream(e.Stream);
                    var dataChunk = mediaStream.DataChunks.First(i => i.Timestamp == e.Timestamp);
                    StreamDataAdded(this, mediaStream, dataChunk);
                }
            }
            else if (e.Action == StreamUpdatedEventArgs.StreamUpdatedAction.ChunkRemoved)
            {
                if (StreamDataRemoved != null && e.Timestamp.HasValue)
                {
                    var mediaStream = new MediaStream(e.Stream);
                    StreamDataRemoved(this, mediaStream, e.Timestamp.Value);
                }
            }
        }

        private void MediaElement_LiveEventCompleted(object sender, EventArgs e)
        {
            LiveEventCompleted.IfNotNull(i => i(this));
        }

        private void MediaElement_SeekCompleted(object sender, SeekCompletedEventArgs e)
        {
            _seekCommand.IsSeeking = false;

            if (_seekCommand.Play)
            {
                Play();
                _seekCommand.Play = false;
            }

            if (_seekCommand.Position.HasValue)
            {
                Position = _seekCommand.Position.Value;
                _seekCommand.Position = null;
            }

            if (_seekCommand.PlaybackRate.HasValue && _seekCommand.PlaybackRate.Value != MediaElement.PlaybackRate)
            {
                PlaybackRate = _seekCommand.PlaybackRate.Value;
            }
            else
            {
                _seekCommand.PlaybackRate = null;
            }

            if (_seekCommand.StartSeekToLive)
            {
                SeekToLive();
                _seekCommand.StartSeekToLive = false;
            }

            SeekCompleted.IfNotNull(i => i(this));
        }

        private void MediaElement_PlaybackTrackChanged(object sender, TrackChangedEventArgs e)
        {
            if (e.StreamType == MediaStreamType.Video)
            {
                VideoPlaybackTrackChanged.IfNotNull(i => i(this, e.NewTrack != null ? new MediaTrack(e.NewTrack) : null));
                VideoPlaybackBitrateChanged.IfNotNull(i => i(this, e.NewTrack != null ? (long)e.NewTrack.Bitrate : 0));
            }
        }

        private void MediaElement_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            DownloadProgressChanged.IfNotNull(i => i(this, MediaElement.DownloadProgress));
        }

        private void MediaElement_BufferingProgressChanged(object sender, RoutedEventArgs e)
        {
            BufferingProgressChanged.IfNotNull(i => i(this, MediaElement.BufferingProgress));
        }

        private void MediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
#if !WINDOWS_PHONE
            if (MediaElement.CurrentLinearClipContext != null && linearAdContext != null)
            {
                AdStateChanged.IfNotNull(i => i(this, linearAdContext));
            }
            else
#endif
            {
                MediaPluginState playState = ConvertToPlayState(MediaElement.CurrentState);
                CurrentStateChanged.IfNotNull(i => i(this, playState));
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaEnded.IfNotNull(i => i(this));
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            _seekCommand.IsSeeking = false;
#if !WINDOWS_PHONE
            if (linearAdContext != null)
            {
                AdError(this, linearAdContext);
            }
#endif
            MediaFailed.IfNotNull(i => i(this, e.ErrorException));
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            _seekCommand.IsSeeking = false;
            MediaOpened.IfNotNull(i => i(this));
        }

        private void MediaElement_LogReady(object sender, SSMELogReadyRoutedEventArgs e)
        {
            string message =
                string.Format(SmoothStreamingResources.SmoothStreamingMediaElementGeneratedLogMessageFormat,
                              e.LogSource);
            var extendedProperties = new Dictionary<string, object> { { "Log", e.Log } };
            SendLogEntry(KnownLogEntryTypes.SmoothStreamingMediaElementLogReady, LogLevel.Statistics, message, extendedProperties: extendedProperties);
        }

        private void NotifyMarkerReached(MediaMarker mediaMarker)
        {
            MarkerReached.IfNotNull(i => i(this, mediaMarker));
        }

        private void MediaElement_SmoothStreamingErrorOccurred(object sender, SmoothStreamingErrorEventArgs e)
        {
            var extendedProperties = new Dictionary<string, object>(1)
                                         {
                                             {"ErrorCode", e.ErrorCode}
                                         };

            SendLogEntry(KnownLogEntryTypes.SmoothStreamingErrorOccurred, LogLevel.Error, e.ErrorMessage, extendedProperties: extendedProperties);

            var err = e.ErrorException ?? new SmoothStreamingMediaPluginException(e.ErrorMessage);
            AdaptiveStreamingErrorOccurred.IfNotNull(i => i(this, err));
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
                                       Message = message ?? string.Empty,
                                       SenderName = PluginName,
                                       Timestamp = timeStamp.HasValue ? timeStamp.Value : DateTime.Now
                                   };

                extendedProperties.ForEach(logEntry.ExtendedProperties.Add);
                LogReady(this, logEntry);
            }
        }

        private MediaPluginState ConvertToPlayState(
            SmoothStreamingMediaElementState smoothStreamingMediaElementState)
        {
            return
                (MediaPluginState)Enum.Parse(typeof(MediaPluginState), smoothStreamingMediaElementState.ToString(), true);
        }

        #region IPlugin Properties
        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        #endregion

        #region IMediaPlugin Properties
        public CacheMode CacheMode
        {
            get { return null; }
            set { /* CacheMode setter not implemented for SSME because it handles its own caching */ }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the media file starts to play immediately after it is opened.
        /// </summary>
        public bool AutoPlay
        {
            get { return MediaElement != null && MediaElement.AutoPlay; }
            set { MediaElement.IfNotNull(i => i.AutoPlay = value); }
        }

        /// <summary>
        /// Gets the list of segments that are part of the current adaptive manifest
        /// </summary>
        public IEnumerable<ISegment> Segments
        {
            get
            {
                return MediaElement != null && MediaElement.ManifestInfo != null
                           ? MediaElement.ManifestInfo.Segments
                                 .Select(i => new Segment(i))
                                 .Cast<ISegment>()
                                 .ToList()
                           : Enumerable.Empty<ISegment>();
            }
        }

        /// <summary>
        /// Gets the segment from the adaptive manifest that is currently active
        /// </summary>
        public ISegment CurrentSegment
        {
            get
            {
                SegmentInfo segment = MediaElement != null
                                      && MediaElement.ManifestInfo != null
                                      && MediaElement.CurrentSegmentIndex.HasValue
                                          ? MediaElement.ManifestInfo.Segments[MediaElement.CurrentSegmentIndex.Value]
                                          : null;

                return segment != null ? new Segment(segment) : null;
            }
        }

        /// <summary>
        /// Gets the balance.
        /// </summary>
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
            get { return EndPosition.Subtract(StartPosition); }
        }

        /// <summary>
        /// Gets the end time of the current media item.
        /// </summary>
        public TimeSpan EndPosition
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.EndPosition
                           : TimeSpan.Zero;
            }
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
        /// Gets or sets the playback rate for media that support different playback rates.
        /// </summary>
        public double PlaybackRate
        {
            get
            {
                return MediaElement != null
                           ? _seekCommand.IsSeeking && _seekCommand.PlaybackRate.HasValue
                                 ? _seekCommand.PlaybackRate.Value
                                 : MediaElement.PlaybackRate.HasValue
                                       ? MediaElement.PlaybackRate.Value
                                       : 0
                           : 0;
            }
            set
            {
                if (MediaElement.PlaybackRate != value &&
                    (MediaElement.CurrentState == SmoothStreamingMediaElementState.Paused ||
                     MediaElement.CurrentState == SmoothStreamingMediaElementState.Playing ||
                     MediaElement.CurrentState == SmoothStreamingMediaElementState.Buffering))
                {
                    _seekCommand.PlaybackRate = value;

                    if (!_seekCommand.IsSeeking)
                    {
                        _seekCommand.IsSeeking = true;
                        MediaElement.SetPlaybackRate(value);
                        PlaybackRateChanged.IfNotNull(i => i(this));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current state of the media item.
        /// </summary>
        public MediaPluginState CurrentState
        {
            get
            {
#if !WINDOWS_PHONE
                if (linearAdContext != null)
                {
                    return MediaPluginState.ClipPlaying;
                }
                else
#endif
                {
                    return MediaElement != null
                               ? ConvertToPlayState(MediaElement.CurrentState)
                               : MediaPluginState.Stopped;
                }
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
                return MediaElement != null ? MediaElement.ClipPosition : TimeSpan.Zero;
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
#endif

        /// <summary>
        /// Gets the current position of the media item.
        /// </summary>
        public TimeSpan Position
        {
            get
            {
                return MediaElement != null
                           ? _seekCommand.IsSeeking && _seekCommand.Position.HasValue
                                 ? _seekCommand.Position.Value
                                 : MediaElement.Position > MediaElement.StartPosition
                                        ? MediaElement.Position
                                        : MediaElement.StartPosition
                           : TimeSpan.Zero;
            }
            set
            {
                TimeSpan maxPosition = IsSourceLive && LivePosition < EndPosition
                                           ? LivePosition
                                           : EndPosition;

                if (MediaElement != null && value >= StartPosition && value <= maxPosition)
                {
                    if (_seekCommand.IsSeeking)
                    {
                        _seekCommand.Position = value;
                    }
                    else
                    {
                        _seekCommand.IsSeeking = MediaElement.SmoothStreamingSource != null
                                                 &&
                                                 MediaElement.CurrentState == SmoothStreamingMediaElementState.Playing;

                        MediaElement.Position = value;
                        if (Source != null)
                        {
                            SeekCompleted.IfNotNull(i => i(this));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the start position for the media.
        /// </summary>
        public TimeSpan StartPosition
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.StartPosition
                           : TimeSpan.Zero;
            }
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
        /// enable GPU acceleration
        /// </summary>
        public bool EnableGPUAcceleration
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.EnableGPUAcceleration
                           : false;
            }
            set { MediaElement.IfNotNull(i => i.EnableGPUAcceleration = value); }
        }

        /// <summary>
        /// Gets a collection of the supported playback rates for the media.
        /// </summary>
        public IEnumerable<double> SupportedPlaybackRates
        {
            get
            {
                return IsSourceAdaptive
                           ? MediaElement.SupportedPlaybackRates
                           : new List<double> { 1 };
            }
        }

        /// <summary>
        /// Gets whether this plugin supports ad scheduling.
        /// </summary>
        public bool SupportsAdScheduling
        {
#if !WINDOWS_PHONE
            get { return true; }
#else
            get { return false; }
#endif
        }

        /// <summary>
        /// Gets the delivery methods supported by this plugin.
        /// </summary>
        public DeliveryMethods SupportedDeliveryMethods
        {
            get { return SupportedDeliveryMethodsInternal; }
        }

        /// <summary>
        /// Gets the control that is playing the media for this plug-in.
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

        #endregion

        #region IAdaptiveMediaPlugin Properties

        public Uri AdaptiveSource
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.SmoothStreamingSource
                           : null;
            }
            set
            {
                MediaElement.IfNotNull(i => i.SmoothStreamingSource = value);
#if !WINDOWS_PHONE
                _chunkDownloadManager.IfNotNull(i => i.Cancel());
#endif
            }
        }

        public bool IsSourceAdaptive
        {
            get { return MediaElement != null && MediaElement.SmoothStreamingSource != null; }
        }

        /// <summary>
        /// Maximum possible bitrate as calculated by display size, not currently implemented
        /// </summary>
        public long MaximumPossibleBitrate
        {
            get
            {
                return MediaElement != null && MediaElement.VideoHighestPlayableTrack != null
                           ? (long)MediaElement.VideoHighestPlayableTrack.Bitrate
                           : 0;
            }
        }

        public IDictionary<string, string> ManifestAttributes
        {
            get
            {
                return MediaElement != null
                       && MediaElement.ManifestInfo != null
                       && MediaElement.ManifestInfo.Attributes != null
                           ? MediaElement.ManifestInfo.Attributes
                           : new Dictionary<string, string>();
            }
        }

        public IMediaTrack VideoDownloadTrack
        {
            get
            {
                return MediaElement != null && MediaElement.VideoDownloadTrack != null
                           ? new MediaTrack(MediaElement.VideoDownloadTrack)
                           : null;
            }
        }

        public IMediaTrack MaximumPlayableVideoTrack
        {
            get
            {
                return MediaElement != null && MediaElement.VideoHighestPlayableTrack != null
                           ? new MediaTrack(MediaElement.VideoHighestPlayableTrack)
                           : null;
            }
        }

        public IMediaTrack VideoPlaybackTrack
        {
            get
            {
                return MediaElement != null && MediaElement.VideoPlaybackTrack != null
                           ? new MediaTrack(MediaElement.VideoPlaybackTrack)
                           : null;
            }
        }

        #endregion

        #region ILiveDvrMediaPlugin Properties

        public LivePlaybackStartPosition LivePlaybackStartPosition
        {
            get
            {
                LivePlaybackStartPosition result;
                return MediaElement != null
                       && MediaElement.LivePlaybackStartPosition.ToString().EnumTryParse(true, out result)
                           ? result
                           : LivePlaybackStartPosition.Beginning;
            }

            set
            {
                PlaybackStartPosition newValue;
                if (MediaElement != null && value.ToString().EnumTryParse(true, out newValue))
                {
                    MediaElement.LivePlaybackStartPosition = newValue;
                }
            }
        }

        public bool IsLivePosition
        {
            get { return MediaElement != null && MediaElement.IsLivePosition; }
        }

        public bool IsSourceLive
        {
            get { return MediaElement != null && MediaElement.IsLive; }
        }

        public TimeSpan LivePosition
        {
            get
            {
                return MediaElement != null
                           ? TimeSpan.FromSeconds(MediaElement.LivePosition)
                           : TimeSpan.Zero;
            }
        }

        #endregion

        #region IProgressiveMediaPlugin Properties

        public double BufferingProgress
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.BufferingProgress
                           : default(double);
            }
        }

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

        public double DownloadProgress
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.DownloadProgress
                           : default(double);
            }
        }

        public double DownloadProgressOffset
        {
            get
            {
                return MediaElement != null
                           ? MediaElement.DownloadProgressOffset
                           : default(double);
            }
        }

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

        public void SetStreamSource(Stream stream)
        {
            MediaElement.IfNotNull(i => i.SetSource(stream));
        }

        #endregion

        public ChunkDownloadStrategy ChunkDownloadStrategy
        {
            get
            {
#if !WINDOWS_PHONE
                return _chunkDownloadManager.ChunkDownloadStrategy;
#else
                return Primitives.ChunkDownloadStrategy.Unspecified;
#endif
            }
            set
            {
#if !WINDOWS_PHONE
                _chunkDownloadManager.ChunkDownloadStrategy = value;
#endif
            }
        }
    }
}
