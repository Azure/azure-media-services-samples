using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Net;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Offline;

namespace Microsoft.SilverlightMediaFramework.Core.Offline
{
    public class OfflineManager : QueuedRetryManager
    {

        public event Action<OfflineManager, Exception> ErrorOccurred;
        public event Action<OfflineManager, PlaylistItem> TakePlaylistItemOfflineCompleted;

        private readonly OfflineStorageManager _offlineStorageManager;
        private readonly IDictionary<PlaylistItem, IList<OfflineTask>> _pendingOfflineTasks;


        public OfflineManager()
        {
            //No need to check for timeouts.
            Timeout = TimeSpan.MaxValue;
            _offlineStorageManager = new OfflineStorageManager();
            _pendingOfflineTasks = new Dictionary<PlaylistItem, IList<OfflineTask>>();
        }


        public IEnumerable<PlaylistItem> ReadStoredPlaylist(string filename)
        {
            return OfflineStorageManager.ReadSerializedItemFromFile<OfflinePlaylist>(filename);
        }

        public void TakeOffline(IEnumerable<PlaylistItem> playlistItems, long spaceRequired, string playlistFilename)
        {
            var offlinePlaylist = new OfflinePlaylist();

            foreach (var playlistItem in playlistItems)
            {
                var playlistItemCopy = playlistItem.Clone();
                ConvertPlaylistItemToOffline(playlistItemCopy);
                offlinePlaylist.Add(playlistItemCopy);
            }

            _offlineStorageManager.SerializeItemToFile(offlinePlaylist, playlistFilename);
            OfflineStorageManager.EnsureSpaceAvailable(spaceRequired);
            playlistItems.ForEach(TakeOffline);
        }

        private void TakeOffline(PlaylistItem playlistItem)
        {
            lock (_pendingOfflineTasks)
            {
                _pendingOfflineTasks.Add(playlistItem, new List<OfflineTask>());

                if (playlistItem.ThumbSource != null)
                {
                    QueueOfflineTask(playlistItem, playlistItem.ThumbSource, OfflineTaskType.DownloadFile);
                }

                foreach (var chapter in playlistItem.Chapters)
                {
                    if (chapter.ThumbSource != null)
                    {
                        QueueOfflineTask(playlistItem, chapter.ThumbSource, OfflineTaskType.DownloadFile);
                    }
                }

                if (playlistItem.MarkerResources != null)
                {
                    foreach (MarkerResource r in playlistItem.MarkerResources)
                    {
                        QueueOfflineTask(playlistItem, r.Source, OfflineTaskType.DownloadFile);
                    }
                }

                var mediaTaskType = playlistItem.DeliveryMethod == DeliveryMethods.AdaptiveStreaming
                                        ? OfflineTaskType.ProcessManifest
                                        : OfflineTaskType.DownloadFile;

                QueueOfflineTask(playlistItem, playlistItem.MediaSource, mediaTaskType);
            }

        }

        private void QueueOfflineTask(PlaylistItem playlistItem, Uri uri, OfflineTaskType taskType)
        {
            var task = new OfflineTask(playlistItem, taskType, uri);
            _pendingOfflineTasks[playlistItem].Add(task);
            AddRequest(task);
        }

        private void DequeueOfflineTask(OfflineTask offlineTask)
        {
            lock (_pendingOfflineTasks)
            {

                if (_pendingOfflineTasks.ContainsKey(offlineTask.PlaylistItem))
                {
                    var remainingPlaylistItemTasks = _pendingOfflineTasks[offlineTask.PlaylistItem];
                    remainingPlaylistItemTasks.Remove(offlineTask);

                    if (remainingPlaylistItemTasks.Count == 0)
                    {
                        _pendingOfflineTasks.Remove(offlineTask.PlaylistItem);
                        TakePlaylistItemOfflineCompleted.IfNotNull(i => i(this, offlineTask.PlaylistItem));
                    }
                }
            }
            NotifyRequestSuccessful(offlineTask);
        }

        protected override void CancelRequest(RetryQueueRequest request)
        {
            base.CancelRequest(request);

            var offlineTask = request as OfflineTask;

            if (offlineTask != null && offlineTask.DownloadWebClient != null)
            {
                offlineTask.DownloadWebClient.CancelAsync();
            }
        }

        protected override void BeginRequest(RetryQueueRequest request)
        {
            var offlineTask = request as OfflineTask;

            if (offlineTask != null)
            {
                switch (offlineTask.Type)
                {
                    case OfflineTaskType.DownloadFile:
                        BeginDownloadFile(offlineTask);
                        break;
                    case OfflineTaskType.ProcessManifest:
                        break;
                    case OfflineTaskType.QueueChunkList:
                        break;
                    default:
                        break;
                }
            }
        }

        private void BeginDownloadFile(OfflineTask task)
        {
            if(task.Type != OfflineTaskType.DownloadFile) throw new ArgumentException("task");

            var webClient = new WebClient();
            task.DownloadWebClient = webClient;
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.OpenReadCompleted += WebClient_OpenReadCompleted;
            webClient.OpenReadAsync(task.ResourceLocation, task);
        }

        private void WebClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            var task = e.UserState as OfflineTask;
            if (task != null)
            {
                if (e.Error == null)
                {
                    try
                    {
                        if (e.Result.Length > 0)
                        {
                            var filename = task.ResourceLocation.ComputeOfflineFilename();
                            _offlineStorageManager.StoreResource(e.Result, filename);
                        }
                        else
                        {
                            string message = string.Format(SilverlightMediaFrameworkResources.OfflineDownloadFailedErrorMessage,
                                                           task.ResourceLocation.AbsolutePath, e.Result.Length);
                            throw new OfflineException(message);
                        }
                    }
                    catch (Exception err)
                    {
                        ErrorOccurred.IfNotNull(i => i(this, err));
                    }
                    finally
                    {
                        e.Result.Close();
                    }
                }
                else if (!e.Cancelled)
                {
                    ErrorOccurred.IfNotNull(i => i(this, e.Error));
                }

                DequeueOfflineTask(task);
            }

            
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //TODO: Update Progress
        }

        private static void ConvertPlaylistItemToOffline(PlaylistItem playlistItem)
        {
            string offlineFilename;
            if (playlistItem.ThumbSource != null)
            {
                offlineFilename = playlistItem.ThumbSource.ComputeOfflineFilename();
                playlistItem.ThumbSource = new Uri(OfflineExtensions.IsolatedStorageUriPrefix + offlineFilename);
            }

            foreach (var chapter in playlistItem.Chapters)
            {
                if (chapter.ThumbSource != null)
                {
                    offlineFilename = chapter.ThumbSource.ComputeOfflineFilename();
                    chapter.ThumbSource = new Uri(OfflineExtensions.IsolatedStorageUriPrefix + offlineFilename);
                }
            }

            if (playlistItem.MarkerResources != null)
            {
                foreach (var r in playlistItem.MarkerResources)
                {
                    offlineFilename = r.Source.ComputeOfflineFilename();
                    r.Source = new Uri(OfflineExtensions.IsolatedStorageUriPrefix + offlineFilename);
                }
            }

            if (playlistItem.MediaSource != null)
            {
                offlineFilename = playlistItem.MediaSource.ComputeOfflineFilename();
                playlistItem.MediaSource = new Uri(OfflineExtensions.IsolatedStorageUriPrefix + offlineFilename);
            }
        }

        public static Stream OpenIsolatedStorageUri(Uri uri)
        {
            IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForApplication();
            string filename = uri.GetIsolatedStorageFilename();

            return userStore.OpenFile(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
    }
}
