using System;
using System.ComponentModel;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    public class MediaSetPlaybackRangeCompletedEventArgs : AsyncCompletedEventArgs
    {
        public MediaSetPlaybackRangeCompletedEventArgs(TimeSpan leftEdge, TimeSpan rightEdge, Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {
            LeftEdge = leftEdge;
            RightEdge = rightEdge;
        }

        public TimeSpan LeftEdge { get; private set; }
        public TimeSpan RightEdge { get; private set; }
    }
}
