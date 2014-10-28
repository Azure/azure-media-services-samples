using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    internal class SeekCommand
    {
        // is currently seeking or not
        public bool IsSeeking;

        // if should play when seek complets
        public bool Play;

        // if should seek to a new position when seek completes

        // if should set a new playback rate
        public double? PlaybackRate;
        public TimeSpan? Position;

        // if should seek to live when seek completes
        public bool StartSeekToLive;
    }
}