using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// A stream that is part of an adaptive media manifest.
    /// </summary>
    public class MediaStream : IMediaStream
    {
        private const string NameAttribute = "name";
        private const string LanguageAttribute = "language";
        private const string EnabledAttribute = "enabled";
        private const string IsSparseStreamAttribute = "issparsestream";
        private const string DurationAttribute = "duration";
        private const string TimeScaleAttribute = "timescale";
        private const string TypeAttribute = "type";
        private const string FourCCAttribute = "fourcc";
        private const string SubTypeAttribute = "subtype";

        private readonly StreamInfo _streamInfo;

        public MediaStream(StreamInfo streamInfo)
        {
            if (streamInfo == null) throw new ArgumentNullException("streamInfo");
            _streamInfo = streamInfo;
        }

        internal StreamInfo StreamInfo
        {
            get { return _streamInfo; }
        }

        #region IMediaStream Members
        /// <summary>
        /// Gets the id of the IMediaStream.
        /// </summary>
        public string Id
        {
            get { return _streamInfo.UniqueId; }
        }

        /// <summary>
        /// Gets the name of the IMediaStream.
        /// </summary>
        public string Name
        {
            get { return _streamInfo.Attributes.GetEntryIgnoreCase(NameAttribute); }
        }

        /// <summary>
        /// Gets the language of the IMediaStream.
        /// </summary>
        public string Language
        {
            get { return _streamInfo.Attributes.GetEntryIgnoreCase(LanguageAttribute); }
        }

        /// <summary>
        /// Gets the FourCC property of this IMeidaStream.
        /// </summary>
        public string FourCC
        {
            get { return _streamInfo.Attributes.GetEntryIgnoreCase(FourCCAttribute); }
        }

        /// <summary>
        /// Gets the type of the IMediaStream.
        /// </summary>
        public StreamType Type
        {
            get
            {
                StreamType result;
                string value = _streamInfo.Attributes.GetEntryIgnoreCase(TypeAttribute);

                return value.EnumTryParse(true, out result)
                           ? result
                           : default(StreamType);
            }
        }

        /// <summary>
        /// Gets the subtype of the IMediaStream.
        /// </summary>
        public string SubType
        {
            get
            {
                //Retrieving this property from the attributes table for now b/c
                //it is not being reported correctly from the SSME. -Kevin Rohling 5-17-10
                return _streamInfo.Attributes.GetEntryIgnoreCase(SubTypeAttribute);
            }
        }

        /// <summary>
        /// Gets whether this IMediaStream is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                string value = _streamInfo.Attributes.GetEntryIgnoreCase(EnabledAttribute);

                bool result;
                return bool.TryParse(value, out result)
                           ? result
                           : true;
            }
        }

        /// <summary>
        /// Gets whether this IMediaStream is a sparse stream.
        /// </summary>
        public bool IsSparseStream
        {
            get
            {
                bool result;
                string value = _streamInfo.Attributes.GetEntryIgnoreCase(IsSparseStreamAttribute);

                bool.TryParse(value, out result);
                return result;
            }
        }

        /// <summary>
        /// Gets the Duration of this IMediaStream.
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                string value = _streamInfo.Attributes.GetEntryIgnoreCase(DurationAttribute);

                TimeSpan result;
                return TimeSpan.TryParse(value, out result)
                           ? result
                           : (TimeSpan?) null;
            }
        }

        /// <summary>
        /// Gets the TimeScale of this IMediaStream.
        /// </summary>
        public TimeSpan? TimeScale
        {
            get
            {
#if SUPPORTS_TIMESCALE
                return TimeSpan.FromTicks(_streamInfo.Timescale);
#else
                string value = _streamInfo.Attributes.GetEntryIgnoreCase(TimeScaleAttribute);

                TimeSpan timescale;
                return TimeSpan.TryParse(value, out timescale)
                           ? timescale
                           : (TimeSpan?) null;
#endif
            }
        }

        /// <summary>
        /// Gets the attributes that are part of this IMediaStream.
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get { return _streamInfo.Attributes; }
        }

        /// <summary>
        /// Gets the tracks available in this IMediaStream.
        /// </summary>
        public IEnumerable<IMediaTrack> AvailableTracks
        {
            get
            {
                return _streamInfo.AvailableTracks
                    .Select(i => new MediaTrack(i))
                    .Cast<IMediaTrack>()
                    .ToList();
            }
        }

        /// <summary>
        /// Gets the tracks that are currently selected in this IMediaStream.
        /// </summary>
        public IEnumerable<IMediaTrack> SelectedTracks
        {
            get
            {
                return _streamInfo.SelectedTracks
                    .Select(i => new MediaTrack(i))
                    .Cast<IMediaTrack>()
                    .ToList();
            }
        }

        /// <summary>
        /// Gets the child streams that are part of this IMediaStream.
        /// </summary>
        public IEnumerable<IMediaStream> ChildStreams
        {
            get
            {
                return _streamInfo.ChildStreams
                    .Select(i => new MediaStream(i))
                    .Cast<IMediaStream>()
                    .ToList();
            }
        }

        public IEnumerable<IDataChunk> DataChunks
        {
             get
             {
                 return _streamInfo.ChunkList
                     .Select(i => new DataChunk(i))
                     .Cast<IDataChunk>()
                     .ToList();
             }
        }

        /// <summary>
        /// Sets the tracks that are selected in this IMediaStream.
        /// </summary>
        /// <param name="tracks">All of the tracks that should be selected when this operation completes.</param>
        public void SetSelectedTracks(IEnumerable<IMediaTrack> tracks)
        {
            List<TrackInfo> trackInfos = tracks.Cast<MediaTrack>()
                .Select(i => i.TrackInfo)
                .ToList();

            _streamInfo.SelectTracks(trackInfos, true);
        }

        /// <summary>
        /// Sets the tracks that are allowed to be used in this IMediaStream.
        /// </summary>
        /// <param name="tracks">The list of tracks that are allowed to be used.</param>
        public void SetRestrictedTracks(IEnumerable<IMediaTrack> tracks)
        {
            List<TrackInfo> trackInfos = tracks.OfType<MediaTrack>()
                .Select(i => i.TrackInfo)
                .ToList();
            _streamInfo.RestrictTracks(trackInfos);
        }

        #endregion

        public override bool Equals(object obj)
        {
            var mediaStream = obj as MediaStream;

            return mediaStream != null && mediaStream.StreamInfo == StreamInfo;
        }

        public override int GetHashCode()
        {
            return _streamInfo.GetHashCode();
        }
    }
}