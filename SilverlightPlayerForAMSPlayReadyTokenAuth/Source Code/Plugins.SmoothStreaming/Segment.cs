using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    public class Segment : ISegment
    {
        private readonly SegmentInfo _segmentInfo;

        public Segment(SegmentInfo segmentInfo)
        {
            if (segmentInfo == null) throw new ArgumentNullException("segmentInfo");
            _segmentInfo = segmentInfo;
        }

        internal SegmentInfo SegmentInfo
        {
            get { return _segmentInfo; }
        }

        #region ISegment Members

#if !WINDOWS_PHONE
        /// <summary>
        /// Sets the streans that are allowed to be used in this ISegment.
        /// </summary>
        /// <param name="streams">The list of streams that are allowed to be used.</param>
        public void SetRestrictStreams(IEnumerable<IMediaStream> streams)
        {
            _segmentInfo.RestrictStreams(streams.OfType<MediaStream>().Select(i => i.StreamInfo).ToList());
        }
#endif

        /// <summary>
        /// Gets the start position of this ISegment.
        /// </summary>
        public TimeSpan StartPosition
        {
            get { return _segmentInfo.StartPosition; }
        }

        /// <summary>
        /// Gets the end position of this ISegment.
        /// </summary>
        public TimeSpan EndPosition
        {
            get { return _segmentInfo.EndPosition; }
        }

        /// <summary>
        /// Gets the streams available in this ISegment.
        /// </summary>
        public IEnumerable<IMediaStream> AvailableStreams
        {
            get
            {
                return _segmentInfo.AvailableStreams
                    .Select(i => new MediaStream(i))
                    .Cast<IMediaStream>()
                    .ToList();
            }
        }

        /// <summary>
        /// Gets the streams that are currently selected within this ISegment.
        /// </summary>
        public IEnumerable<IMediaStream> SelectedStreams
        {
            get
            {
                return _segmentInfo.SelectedStreams
                    .Select(i => new MediaStream(i))
                    .Cast<IMediaStream>()
                    .ToList();
            }
        }

        /// <summary>
        /// Sets the streams that are selected within this ISegment.
        /// </summary>
        /// <param name="streams">All of the streams that should be selected when this operation completes.</param>
        public void SetSelectedStreams(IEnumerable<IMediaStream> streams)
        {
            List<StreamInfo> streamInfos = streams.Cast<MediaStream>()
                .Select(i => i.StreamInfo)
                .ToList();
            _segmentInfo.SelectStreamsAsync(streamInfos);
        }

        #endregion

        public override bool Equals(object obj)
        {
            var segment = obj as Segment;
            return segment != null && segment.SegmentInfo == SegmentInfo;
        }

        public override int GetHashCode()
        {
            return _segmentInfo.GetHashCode();
        }
    }
}