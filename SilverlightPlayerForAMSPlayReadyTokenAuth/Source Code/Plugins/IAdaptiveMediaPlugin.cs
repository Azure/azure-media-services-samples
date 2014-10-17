using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Represents a media plug-in that supports variable bitrates.
    /// </summary>
    /// <remarks>
    /// Implement IAdaptiveMediaPlugin instead for adaptive media plugins. However, this subset interface can be used for plug-ins that supports variable bitrates without track or stream info.
    /// </remarks>
    public interface IVariableBitrateMediaPlugin : IMediaPlugin 
    { 
        /// <summary>
        /// Gets the value for the maximum bitrate.
        /// </summary>
        long MaximumPossibleBitrate { get; }

        /// <summary>
        /// Occurs when the bitrate used for playback changes.
        /// </summary>
        event Action<IVariableBitrateMediaPlugin, long> VideoPlaybackBitrateChanged;
    }

    /// <summary>
    /// Represents a media plug-in that can play adaptive media encoded with multiple bitrates (IIS Smooth Streaming).
    /// </summary>
    /// <remarks>
    /// You implement this interface for a plug-in that supports adaptive media streams.
    /// </remarks>
    public interface IAdaptiveMediaPlugin : IVariableBitrateMediaPlugin
    {
        /// <summary>
        /// Gets or sets the location of the adaptive media.
        /// </summary>
        Uri AdaptiveSource { get; set; }

        /// <summary>
        /// Gets or sets the strategy to be used when downloading captions and ad markers.
        /// </summary>
        ChunkDownloadStrategy ChunkDownloadStrategy { get; set; }

        /// <summary>
        /// Gets a value indicating whether the media is adaptive (multiple bitrate encoded).
        /// </summary>
        bool IsSourceAdaptive { get; }

        /// <summary>
        /// Gets the list of segments that are part of the current adaptive manifest
        /// </summary>
        IEnumerable<ISegment> Segments { get; }

        /// <summary>
        /// Gets the segment from the adaptive manifest that is currently active
        /// </summary>
        ISegment CurrentSegment { get; }

        /// <summary>
        /// Gets the track that is currently being used to download video
        /// </summary>
        IMediaTrack VideoDownloadTrack { get; }

        /// <summary>
        /// Gets the video track with the highest bitrate that can be played, adjusted according to the current display settings
        /// </summary>
        IMediaTrack MaximumPlayableVideoTrack { get; }

        /// <summary>
        /// Gets the track that is currently being used to play video
        /// </summary>
        IMediaTrack VideoPlaybackTrack { get; }

        /// <summary>
        /// Gets attributes that are a part of the current adaptive manifest
        /// </summary>
        IDictionary<string, string> ManifestAttributes { get; }

        /// <summary>
        /// Occurs when the bitrate used for playback changes.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaTrack> VideoPlaybackTrackChanged;

        /// <summary>
        /// Occurs when the bitrate used for downloading changes.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaTrack> VideoDownloadTrackChanged;

        /// <summary>
        /// Occurs when an error occurs during playback.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, Exception> AdaptiveStreamingErrorOccurred;

        /// <summary>
        /// Occurs when the manifest has been downloaded.
        /// </summary>
        event Action<IAdaptiveMediaPlugin> ManifestReady;

        /// <summary>
        /// Occurs when a stream has been selected.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaStream> StreamSelected;

        /// <summary>
        /// Occurs when a stream has been unselected.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaStream> StreamUnselected;

        /// <summary>
        /// Occurs when the selection of a stream fails.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IEnumerable<IMediaStream>, Exception> StreamSelectionFailed;

        /// <summary>
        /// Occurs when data has been added to a stream.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaStream, IDataChunk> StreamDataAdded;

        /// <summary>
        /// Occurs when data has been removed from a stream.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaStream, TimeSpan> StreamDataRemoved;

        /// <summary>
        /// Occurs when the download of data from a stream fails.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaTrack, IDataChunk, Exception> DownloadStreamDataFailed;
        
        /// <summary>
        /// Occurs when the download of data from a stream completes.
        /// </summary>
        event Action<IAdaptiveMediaPlugin, IMediaTrack, IStreamDownloadResult> DownloadStreamDataCompleted;

        /// <summary>
        /// Empties the contents of the buffer leaving only the specified amount of buffered time.
        /// </summary>
        /// <param name="timeSpan">The amount of time to leave in the buffer.</param>
        void ClearBuffer(TimeSpan timeSpan);

        /// <summary>
        /// Downloads all of the available data from the specified track.
        /// </summary>
        /// <param name="track">the track that contains the data to be downloaded.</param>
        void DownloadStreamData(IMediaTrack track);

        /// <summary>
        /// Cancels any queued downloads for the specified track.
        /// </summary>
        /// <param name="track">the track that contains the data that should not be downloaded.</param>
        void CancelDownloadStreamData(IMediaTrack track);

        /// <summary>
        /// Downloads the specified chunk from the specified track.
        /// </summary>
        /// <param name="track">the track that contains the data to be downloaded.</param>
        /// <param name="chunk">the chunk to be downloaded.</param>
        void DownloadStreamData(IMediaTrack track, IDataChunk chunk);

        /// <summary>
        /// Specifies the range of video bitrates that can be downloaded.
        /// </summary>
        /// <param name="minimumVideoBitrate">the minimum bitrate that can be downloaded.</param>
        /// <param name="maximumVideoBitrate">the maximum bitrate that can be downloaded.</param>
        /// <param name="flushBuffer">flush the buffer</param>
        void SetVideoBitrateRange(long minimumVideoBitrate, long maximumVideoBitrate, bool flushBuffer = true);

        /// <summary>
        /// Specifies the cache provider that should be used.
        /// </summary>
        /// <param name="cacheProvider">the cache provider</param>
        void SetCacheProvider(object cacheProvider);

        void SetSegmentSelectedStreams(ISegment segment, IEnumerable<IMediaStream> streams);

        void ModifySegmentSelectedStreams(ISegment segment, IEnumerable<IMediaStream> streamsToAdd, IEnumerable<IMediaStream> streamsToRemove);
    }
}