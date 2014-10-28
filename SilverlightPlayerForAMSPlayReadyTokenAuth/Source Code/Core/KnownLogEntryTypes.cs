namespace Microsoft.SilverlightMediaFramework.Core
{
    internal static class KnownLogEntryTypes
    {
        public const string GeneralErrorOccurred = "GeneralErrorOccurred";
        public const string GeneralLogEntry = "GeneralLogEntry";

        //Media Plugin Events
        public const string BufferingProgressChanged = "BufferingProgressChanged";
        public const string DownloadProgressChanged = "DownloadProgressChanged";
        public const string MediaEnded = "MediaEnded";
        public const string MediaFailed = "MediaFailed";
        public const string MediaOpened = "MediaOpened";
        public const string PlaybackRateChanged = "PlaybackRateChanged";
        public const string ScrubbingStarted = "ScrubbingStarted";
        public const string Scrubbed = "Scrubbed";
        public const string ScrubbingCompleted = "ScrubbingCompleted";
        public const string VideoPlaybackBitrateChanged = "VideoPlaybackBitrateChanged";  //Use IAdaptiveMediaPlugin.VideoPlaybackTrackChanged
        public const string VideoDownloadBitrateChanged = "VideoDownloadBitrateChanged";  //Use IAdaptiveMediaPlugin.VideoDownloadTrackChanged
        public const string ManifestReady = "ManifestReady";
        public const string AdaptiveStreamingErrorOccurred = "AdaptiveStreamingErrorOccurred";
        public const string StreamSelected = "StreamSelected";
        public const string StreamUnselected = "StreamUnselected";
        public const string StreamSelectionFailed = "StreamSelectionFailed";
        public const string StreamDataAdded = "StreamDataAdded";
        public const string StreamDataRemoved = "StreamDataRemoved";
        public const string DownloadStreamDataFailed = "DownloadStreamDataFailed";
        public const string DownloadStreamDataCompleted = "DownloadStreamDataCompleted";
        public const string AdClickThrough = "AdClickThrough";
        public const string AdProgressUpdated = "AdProgressUpdated";
        public const string MaximumPlaybackBitrateChanged = "MaximumPlaybackBitrateChanged";

        //Player events
        public const string AddExternalPluginsCompleted = "AddExternalPluginsCompleted";
        public const string AddExternalPluginsFailed = "AddExternalPluginsFailed";
        public const string AddExternalPluginsDownloadProgressChanged = "AddExternalPackageDownloadProgressChanged";
        public const string ConfiguredVideoSize = "ConfiguredVideoSize";
        public const string PlayStateChanged = "PlayStateChanged";
        public const string PlaySpeedStateChanged = "PlaySpeedStateChanged";
        public const string PluginLoaded = "PluginLoaded";
        public const string CaptionRegionReached = "CaptionRegionReached";
        public const string CaptionRegionLeft = "CaptionRegionLeft";
        public const string CaptionRegionChanged = "CaptionRegionChanged";
        public const string MarkerReached = "MarkerReached";
        public const string MarkerLeft = "MarkerLeft";
        public const string MarkerSkipped = "MarkerSkipped";
        public const string MarkersRemoved = "MarkersRemoved";
        public const string MarkersAdded = "MarkersAdded";
        public const string PlaylistChanged = "PlaylistChanged";
        public const string PlaylistItemChanged = "PlaylistItemChanged";
        public const string RetrieveMarkersFailed = "RetrieveMarkersFailed";
        public const string RetryStarted = "RetryStarted";
        public const string RetrySuccessful = "RetrySuccessful";
        public const string RetryFailed = "RetryFailed";
        public const string RetryCancelled = "RetryCancelled";
        public const string RetryAttemptFailed = "RetryAttemptFailed";
        public const string LogWriteSuccessful = "LogWriteSuccessful";
        public const string DataReceived = "DataReceived";
        public const string AudioStreamChanged = "AudioStreamChanged";
        public const string CaptionStreamChanged = "CaptionStreamChanged";
        public const string VolumeLevelChanged = "VolumeLevelChanged";
        public const string IsMutedChanged = "IsMutedChanged";
        public const string FullScreenChanged = "FullScreenChanged";
        public const string UnableToLocateMediaPlugin = "UnableToLocateMediaPlugin";
        public const string UnableToLocateMarkerProvider = "UnableToLocateMarkerProvider";
        public const string PlayBlockAdded = "PlayBlockAdded";
        public const string PlayBlockReleased = "PlayBlockReleased";
        public const string AdNotHandled = "AdNotHandled";
        public const string AdSkippedFromSeek = "AdSkippedFromSeek";
        public const string AdSkippedFromFwdRwd = "AdSkippedFromFwdRwd";

        //Player API calls
        public const string Play = "Play";
        public const string Pause = "Pause";
        public const string Stop = "Stop";
        public const string ScheduleAdvertisement = "ScheduleAdvertisement";
        public const string GoToNextPlaylistItem = "GoToNextPlaylistItem";
        public const string GoToPreviousPlaylistItem = "GoToPreviousPlaylistItem";
        public const string GoToNextChapter = "GoToNextChapter";
        public const string GoToPreviousChapter = "GoToPreviousChapter";
        public const string SeekToPosition = "SeekToPosition";
        public const string StartFastForward = "StartFastForward";
        public const string StopFastForward = "StopFastForward";
        public const string IncrementFastForward = "IncrementFastForward";
        public const string DecrementFastForward = "DecrementFastForward";
        public const string StartRewind = "StartRewind";
        public const string StopRewind = "StopRewind";
        public const string IncrementRewind = "IncrementRewind";
        public const string DecrementRewind = "DecrementRewind";
        public const string StartSlowMotion = "StartSlowMotion";
        public const string StopSlowMotion = "StopSlowMotion";
        public const string Replay = "Replay";
        public const string BeginAddExternalPlugins = "BeginAddExternalPlugins";
        public const string SeekToLive = "SeekToLive";
    }
}