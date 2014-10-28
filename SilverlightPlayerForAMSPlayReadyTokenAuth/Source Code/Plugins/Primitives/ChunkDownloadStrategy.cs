using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Indicates the strategy to use to dowbload data chunks, which are typically captions or ad markers.
    /// </summary>
    public enum ChunkDownloadStrategy
    {
        /// <summary>
        /// If set on a playlist item, the player's setting is used. If set on player,
        /// AsNeeded is used.
        /// </summary>
        Unspecified = 0,      
        /// <summary>
        /// All chunks are downloaded, starting with future chunks then working backwards from current position.
        /// </summary>
        AggressiveFromCurrent,
        /// <summary>
        /// All chunks are downloaded, starting with future chunks then working forwards from beginning of stream.
        /// </summary>
        AggressiveFromStart,
        /// <summary>
        /// All future chunks are downloaded.
        /// </summary>
        AggressiveFuture,
        /// <summary>
        /// Data chunks are only downloaded if the chunk timestamp falls near the current position. 
        /// Chunks in future (up to 60 seconds) are downloaded first, then most recent chunk within 60 seconds.
        /// </summary>
        AsNeeded,
    }
}
