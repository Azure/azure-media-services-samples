using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.Web.Media.SmoothStreaming;
using System.Diagnostics;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    internal class StreamSelectionManager: QueuedRetryManager
    {
        public event Action<StreamSelectionManager, Segment, IEnumerable<MediaStream>> RetryingStreamSelection, StreamSelectionExceededMaximumRetries;
        public event Action<StreamSelectionManager, Segment, IEnumerable<MediaStream>, StreamUpdatedListEventArgs> StreamSelectionCompleted;

        private readonly ManifestInfo _manifestInfo;

        public StreamSelectionManager(ManifestInfo ManifestInfo)
        {
            MaximumConcurrentRequests = 1;
            Timeout = TimeSpan.FromSeconds(15);

            _manifestInfo = ManifestInfo;
            _manifestInfo.SelectStreamsCompleted += ManifestInfo_SelectStreamsCompleted;
        }

        public void EnqueueRequest(Segment segment, IEnumerable<MediaStream> streams)
        {
            var request = new StreamSelectionRequest(segment, streams);
            AddRequest(request);
        }

        public void EnqueueRequest(Segment segment, IEnumerable<MediaStream> streamsToAdd, IEnumerable<MediaStream> streamsToRemove)
        {
            AddRequest(new StreamModifyRequest(segment, streamsToAdd, streamsToRemove));
        }

        protected override void BeginRequest(RetryQueueRequest request)
        {
            //Adding an intentional delay here to work around a known timing issue
            //with the SSME.  If this is called too soon after initialization the
            //request will be aborted with no indication raised from the SSME.
            //TODO: Remove this workaround once the SSME has been fixed.
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            EventHandler tickHandler = null;
            tickHandler = (s, e) =>
                {
                    Segment segment = null;
                    List<StreamInfo> streams = null;
                    var streamSelectionRequest = request as StreamSelectionRequest;
                    if (streamSelectionRequest != null)
                    {
                        streams = streamSelectionRequest.Streams.Select(i => i.StreamInfo).ToList();
                        segment = streamSelectionRequest.Segment;
                    }
                    else
                    {
                        var streamModifyRequest = request as StreamModifyRequest;
                        if (streamModifyRequest != null)
                        {
                            streams = streamModifyRequest.Streams.Select(i => i.StreamInfo).ToList();
                            segment = streamModifyRequest.Segment;
                        }
                    }                    
                    segment.SegmentInfo.SelectStreamsAsync(streams, request);
                    timer.Tick -= tickHandler;
                    timer.Stop();
                };
            timer.Tick += tickHandler;
            timer.Start();
        }

        private void ManifestInfo_SelectStreamsCompleted(object sender, StreamUpdatedListEventArgs e)
        {
            var request = e.UserState as IStreamSelectionRequest;

            if (request != null)
            {
                NotifyRequestSuccessful(request as RetryQueueRequest);
                StreamSelectionCompleted(this, request.Segment, request.Streams, e);
            }
        }


        protected override void OnRequestExceededMaximumRetryAttempts(RetryQueueRequest request)
        {
            base.OnRequestExceededMaximumRetryAttempts(request);

            var streamSelectionRequest = request as IStreamSelectionRequest;
            if (StreamSelectionExceededMaximumRetries != null && streamSelectionRequest != null)
            {
                StreamSelectionExceededMaximumRetries(this, streamSelectionRequest.Segment, streamSelectionRequest.Streams);
            }
        }

        protected override void OnRetryingRequest(RetryQueueRequest request)
        {
            base.OnRetryingRequest(request);

            var streamSelectionRequest = request as IStreamSelectionRequest;
            if (RetryingStreamSelection != null && streamSelectionRequest != null)
            {
                RetryingStreamSelection(this, streamSelectionRequest.Segment, streamSelectionRequest.Streams);
            }
        }

        public override void Dispose()
        {
            _manifestInfo.SelectStreamsCompleted -= ManifestInfo_SelectStreamsCompleted;
            base.Dispose();
        }
    }

    interface IStreamSelectionRequest
    {
        Segment Segment { get; }
        IEnumerable<MediaStream> Streams { get; }
    }

    internal class StreamModifyRequest : RetryQueueRequest, IStreamSelectionRequest
    {
        public Segment Segment { get; private set; }
        public IEnumerable<MediaStream> StreamsToAdd { get; private set; }
        public IEnumerable<MediaStream> StreamsToRemove { get; private set; }

        public StreamModifyRequest(Segment segment, IEnumerable<MediaStream> mediaStreamsToAdd, IEnumerable<MediaStream> mediaStreamsToRemove)
        {
            Segment = segment;
            StreamsToAdd = mediaStreamsToAdd;
            StreamsToRemove = mediaStreamsToRemove;
        }

        public IEnumerable<MediaStream> Streams
        {
            get
            {
                List<MediaStream> ret;
                ret = new List<MediaStream>();
                foreach (var s in Segment.SelectedStreams)
                {
                    ret.Add(s as MediaStream);
                }
                if (StreamsToAdd != null)
                {
                    foreach (var s in StreamsToAdd)
                    {
                        if (ret.Contains(s) == false) ret.Add(s);
                    }
                }
                if (StreamsToRemove != null)
                {
                    foreach (var stream in StreamsToRemove)
                    {
                        if (ret.Contains(stream))
                        {
                            ret.Remove(stream);
                        }
                    }
                }
                return ret;
            }
        }
    }

    internal class StreamSelectionRequest : RetryQueueRequest, IStreamSelectionRequest
    {
        public Segment Segment { get; private set; }
        public IEnumerable<MediaStream> Streams { get; private set; }

        public StreamSelectionRequest(Segment segment, IEnumerable<MediaStream> mediaStreams)
        {
            Segment = segment;
            Streams = mediaStreams;
        }
    }
}
