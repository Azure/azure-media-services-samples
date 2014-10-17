using System;
using System.Net;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities;

namespace Microsoft.SilverlightMediaFramework.Core.Offline
{
    public class OfflineTask : RetryQueueRequest
    {
        public OfflineTaskType Type { get; private set; }
        public Uri ResourceLocation { get; private set; }
        public PlaylistItem PlaylistItem { get; private set; }
        public WebClient DownloadWebClient { get; set; }

        public OfflineTask(PlaylistItem playlistItem, OfflineTaskType type)
            : this(playlistItem, type, null) { }

        public OfflineTask(PlaylistItem playlistItem, OfflineTaskType type, Uri resourceLocation)
        {
            if (playlistItem == null) throw new ArgumentNullException("playlistItem");

            Type = type;
            ResourceLocation = resourceLocation;
            PlaylistItem = playlistItem;
        }

    }
}
