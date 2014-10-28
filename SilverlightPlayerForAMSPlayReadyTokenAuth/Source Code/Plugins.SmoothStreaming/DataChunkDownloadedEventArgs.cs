using System;
using System.Net;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.Web.Media.SmoothStreaming;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    public class DataChunkDownloadedEventArgs : EventArgs
    {
        readonly ChunkDownloadedEventArgs _eventArgs;

        public DataChunkDownloadedEventArgs(ChunkDownloadedEventArgs eventArgs)
        {
            _eventArgs = eventArgs;
        }

        public Uri CanonicalUri { get { return _eventArgs.CanonicalUri; } }
        
        public IDataChunk Chunk { get { return new DataChunk(_eventArgs.Chunk); } }

        public DataChunkResultState DownloadResult
        {
            get
            {
                return (DataChunkResultState)Enum.Parse(typeof(ChunkResult.ChunkResultState), _eventArgs.RequestType.ToString(), true);
            }
        }
        
        public DataChunkRequestType RequestType
        {
            get
            {
                return (DataChunkRequestType)Enum.Parse(typeof(ChunkRequestType), _eventArgs.RequestType.ToString(), true);
            }
        }
        
        public HttpStatusCode StatusCode { get { return _eventArgs.StatusCode; } }

        public IMediaTrack Track { get { return new MediaTrack(_eventArgs.Track); } }
    }


    public enum DataChunkRequestType
    {
        Fragment = 0,
        FragmentInfo = 1,
        KeyFrame = 2,
    }

    public enum DataChunkResultState
    {
        Succeeded = 0,
        Failed = 1,
        TimedOut = 2,
    }
}
