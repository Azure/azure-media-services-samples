using System;
using System.Linq;
using Microsoft.Web.Media.SmoothStreaming;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    internal class ChunkDownloadManager : RetryManager
    {
        private const int DefaultMaximumConcurrentRequests = 2;
        private const int DefaultMaximumRetryAttempts = 3;
        private const long DefaultTimeoutMillis = 6000;
        private const long DefaultMaximumRequestRate = 20;
        private const long DefaultAsNeededWindowMillis = 60000;

        public event Action<ChunkDownloadManager, MediaTrack, TimeSpan> RetryingDownload, DownloadExceededMaximumRetries;
        public event Action<ChunkDownloadManager, MediaTrack, TimeSpan, ChunkResult> DownloadCompleted;
        private readonly IMediaPlugin _mediaPlugin;
        private readonly DownloadRequestCollection _requests;
        private readonly RateMonitor _rateMonitor;
        private readonly long _maximumRequestRate;
        private readonly DispatcherTimer _refreshTimer;
        private ChunkDownloadStrategy _chunkDownloadStrategy;

        public ChunkDownloadManager(IMediaPlugin mediaPlugin, long maximumRequestRate = DefaultMaximumRequestRate)
        {
            MaximumRetryAttempts = DefaultMaximumRetryAttempts;
            MaximumConcurrentRequests = DefaultMaximumConcurrentRequests;
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeoutMillis);
            _chunkDownloadStrategy = ChunkDownloadStrategy.AsNeeded;
            _mediaPlugin = mediaPlugin;
            _maximumRequestRate = maximumRequestRate;
            _requests = new DownloadRequestCollection();
            _rateMonitor = new RateMonitor();
            _refreshTimer = new DispatcherTimer();
            _refreshTimer.Interval = TimeSpan.FromMilliseconds(DefaultAsNeededWindowMillis - DefaultTimeoutMillis);
            _refreshTimer.Tick += _refreshTimer_Tick;
        }

        public ChunkDownloadStrategy ChunkDownloadStrategy
        {
            get
            {
                return _chunkDownloadStrategy;
            }
            set
            {
                _chunkDownloadStrategy = (value == ChunkDownloadStrategy.Unspecified) ? ChunkDownloadStrategy.AsNeeded : value;
            }
        }

        void _refreshTimer_Tick(object sender, EventArgs e)
        {
            NotifyRequestAdded();
        }

        void _mediaPlugin_SeekCompleted(IMediaPlugin obj)
        {
            NotifyRequestAdded();
        }

        public override bool HasPendingRequests
        {
            get { return _requests.Count > 0; }
        }



        protected override RetryQueueRequest NextRequest()
        {
            DownloadRequest next = null;

            switch (ChunkDownloadStrategy)
            {
                case Primitives.ChunkDownloadStrategy.AsNeeded:
                    next = _requests.WhereAfterPosition(_mediaPlugin.Position - TimeSpan.FromSeconds(3), 1).FirstOrDefault();
                    if (next != null && next.ChunkTimestamp > _mediaPlugin.Position + TimeSpan.FromMilliseconds(DefaultAsNeededWindowMillis))
                    {
                        next = null;
                    }
                    next = next ?? _requests.WhereAfterPosition(_mediaPlugin.Position - TimeSpan.FromMilliseconds(DefaultAsNeededWindowMillis), 1).LastOrDefault();
                    if (next != null && next.ChunkTimestamp > _mediaPlugin.Position + TimeSpan.FromMilliseconds(DefaultAsNeededWindowMillis))
                    {
                        next = null;
                    }
                    break;
                case Primitives.ChunkDownloadStrategy.AggressiveFuture:
                    next = _requests.WhereAfterPosition(_mediaPlugin.Position - TimeSpan.FromSeconds(3), 1).FirstOrDefault();
                    break;
                case Primitives.ChunkDownloadStrategy.AggressiveFromStart:
                    next = _requests.WhereAfterPosition(_mediaPlugin.Position - TimeSpan.FromSeconds(3), 1).FirstOrDefault();
                    next = next ?? _requests.FirstOrDefault();
                    break;
                case Primitives.ChunkDownloadStrategy.AggressiveFromCurrent:
                    next = _requests.WhereAfterPosition(_mediaPlugin.Position - TimeSpan.FromSeconds(3), 1).FirstOrDefault();
                    next = next ?? _requests.LastOrDefault();
                    break;
            }
            if (next != null)
            {
                _requests.Remove(next);
            }

            return next;
        }

        public void RemoveRequests(MediaTrack MediaTrack)
        {
            foreach (var request in _requests.Where(r => r.MediaTrack.TrackInfo == MediaTrack.TrackInfo).ToList())
            {
                _requests.Remove(request);
            }
        }

        /// <summary>
        /// Optimized version designed for adding multiple requests at once
        /// </summary>
        public void AddRequests(IEnumerable<Tuple<MediaTrack, TimeSpan>> Requests)
        {
            foreach (var Request in Requests)
            {
                _requests.Add(new DownloadRequest(Request.Item1, Request.Item2));
            }
            NotifyRequestAdded();

            if (ChunkDownloadStrategy == ChunkDownloadStrategy.AsNeeded && !_refreshTimer.IsEnabled)
            {
                _refreshTimer.Start();
                _mediaPlugin.SeekCompleted += new Action<IMediaPlugin>(_mediaPlugin_SeekCompleted);
            }
        }

        public void AddRequest(MediaTrack track, TimeSpan chunkTimestamp)
        {
            var request = new DownloadRequest(track, chunkTimestamp);
            _requests.Add(request);
            NotifyRequestAdded();

            if (ChunkDownloadStrategy == ChunkDownloadStrategy.AsNeeded && !_refreshTimer.IsEnabled)
            {
                _refreshTimer.Start();
                _mediaPlugin.SeekCompleted += new Action<IMediaPlugin>(_mediaPlugin_SeekCompleted);
            }
        }

        protected override void BeginRequest(RetryQueueRequest request)
        {
            var downloadRequest = request as DownloadRequest;
            if (downloadRequest != null)
            {
                _rateMonitor.UpdateRate();
                if (_rateMonitor.CurrentRate > _maximumRequestRate)
                {
                    var delay = _rateMonitor.RecommendDelay(_maximumRequestRate);
                    var timer = new DispatcherTimer
                    {
                        Interval = delay
                    };

                    EventHandler tickHandler = null;
                    tickHandler = (s, e) =>
                    {
                        timer.Tick -= tickHandler;
                        timer.Stop();
                        downloadRequest.MediaTrack.TrackInfo.BeginGetChunk(
                            downloadRequest.ChunkTimestamp, BeginGetChunk_Completed,
                            downloadRequest);
                    };
                    timer.Tick += tickHandler;
                    timer.Start();
                }
                else
                {
                    downloadRequest.MediaTrack.TrackInfo.BeginGetChunk(downloadRequest.ChunkTimestamp, BeginGetChunk_Completed, downloadRequest);
                }
            }
        }

        private void BeginGetChunk_Completed(IAsyncResult asyncResult)
        {
            var downloadRequest = (DownloadRequest)asyncResult.AsyncState;
            MediaTrack track = downloadRequest.MediaTrack;

            ChunkResult chunk = track.TrackInfo.EndGetChunk(asyncResult);

            NotifyRequestSuccessful(downloadRequest);
            DownloadCompleted(this, downloadRequest.MediaTrack, downloadRequest.ChunkTimestamp, chunk);
        }

        protected override void OnRequestExceededMaximumRetryAttempts(RetryQueueRequest request)
        {
            base.OnRequestExceededMaximumRetryAttempts(request);

            var downloadRequest = request as DownloadRequest;
            if (DownloadExceededMaximumRetries != null && downloadRequest != null)
            {
                DownloadExceededMaximumRetries(this, downloadRequest.MediaTrack, downloadRequest.ChunkTimestamp);
            }
        }

        public override void Cancel()
        {
            _mediaPlugin.SeekCompleted -= new Action<IMediaPlugin>(_mediaPlugin_SeekCompleted);
            _refreshTimer.Stop();
            _requests.Clear();
        }

        protected override void OnRetryingRequest(RetryQueueRequest request)
        {
            base.OnRetryingRequest(request);

            var downloadRequest = request as DownloadRequest;
            if (RetryingDownload != null && downloadRequest != null)
            {
                RetryingDownload(this, downloadRequest.MediaTrack, downloadRequest.ChunkTimestamp);
            }
        }

        public override void Dispose()
        {
            _refreshTimer.Tick -= _refreshTimer_Tick;
            if (_refreshTimer.IsEnabled) _refreshTimer.Stop();
            base.Dispose();
        }
    }

    internal class DownloadRequest : RetryQueueRequest
    {
        public MediaTrack MediaTrack { get; private set; }
        public TimeSpan ChunkTimestamp { get; private set; }

        public DownloadRequest(MediaTrack mediaTrack, TimeSpan timestamp)
        {
            MediaTrack = mediaTrack;
            ChunkTimestamp = timestamp;
        }
    }
}