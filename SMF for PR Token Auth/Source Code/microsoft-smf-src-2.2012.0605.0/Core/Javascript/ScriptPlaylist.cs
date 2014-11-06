using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Browser;
using System.Windows.Media;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    /// <summary>
    /// A script accessible playlist.
    /// </summary>
    public class ScriptPlaylist
    {
        private readonly List<ScriptPlaylistItem> _items = new List<ScriptPlaylistItem>();

        public ScriptPlaylist(IEnumerable<PlaylistItem> playlist)
        {
            foreach (PlaylistItem item in playlist)
            {
                _items.Add(new ScriptPlaylistItem(item));
            }
        }

        public ScriptPlaylist()
        {
        }

        /// <summary>
        /// Adds the specified playlist item to this playlist.
        /// </summary>
        /// <param name="playlistItem">The playlist item to add to this playlist.</param>
        [ScriptableMember]
        public void AddPlaylistItem(ScriptPlaylistItem playlistItem)
        {
            _items.Add(playlistItem);
        }

        /// <summary>
        /// Gets the playlist item at the specified index within this playlist.
        /// </summary>
        /// <param name="index">The index of the request playlist item.</param>
        /// <returns>The playlist item at the specified index within this playlist.</returns>
        [ScriptableMember]
        public ScriptPlaylistItem GetPlaylistItem(int index)
        {
            return _items[index];
        }

        /// <summary>
        /// Gets the number of playlist items within this playlist.
        /// </summary>
        /// <returns>The number of playlist items within this playlist.</returns>
        [ScriptableMember]
        public int GetPlaylistItemCount()
        {
            return _items.Count;
        }

        /// <summary>
        /// Converts this scriptable Playlist into playlist that can be consumed by the SMFPlayer.
        /// </summary>
        /// <returns>A collection of playlist items.</returns>
        public ObservableCollection<PlaylistItem> ToPlaylist()
        {
            var p = new ObservableCollection<PlaylistItem>();
            for (int i = 0; i < GetPlaylistItemCount(); i++)
            {
                ScriptPlaylistItem sItem = GetPlaylistItem(i);

                var thumbSource = !sItem.ThumbSource.IsNullOrWhiteSpace()
                                        ? new Uri(sItem.ThumbSource)
                                        : null;

                var item = new PlaylistItem
                {
                    DeliveryMethod =
                        (DeliveryMethods)
                        Enum.Parse(typeof(DeliveryMethods), sItem.DeliveryMethod, true),
                    Description = sItem.Description,
                    FileSize = sItem.FileSize,
                    FrameRate = sItem.FrameRate,
                    JumpToLive = sItem.JumpToLive,
                    MediaSource = new Uri(sItem.MediaSource),
                    ThumbSource = thumbSource,
                    Title = sItem.Title,
                    VideoHeight = sItem.VideoHeight,
                    VideoStretchMode =
                        (Stretch)Enum.Parse(typeof(Stretch), sItem.VideoStretchMode, true),
                    VideoWidth = sItem.VideoWidth,
                    CustomMetadata = sItem.CustomMetadata,
                    S3DProperties = (sItem.ScriptS3DProperties == null) ? null : sItem.ScriptS3DProperties.ConvertToS3DProperties(),
                    BearerToken = sItem.BearerToken /* we need this to provide AMS key auth support for license delivery */
                };

                for (int j = 0; j < sItem.MarkerCount; j++)
                {
                    ScriptMediaMarker smm = sItem.GetMarker(j);
                    
                    if(!smm.Type.IsNullOrWhiteSpace() && smm.Type.Equals("timeline", StringComparison.OrdinalIgnoreCase))
                    {
                        item.TimelineMarkers.Add(new TimelineMediaMarker
                                                     {
                                                         AllowSeek = smm.AllowSeek,
                                                         Begin = TimeSpan.FromSeconds(smm.Begin),
                                                         End = TimeSpan.FromSeconds(smm.End),
                                                         Id = smm.Id,
                                                         Content = smm.Content
                                                     });
                    }
                }
                for (int j = 0; j < sItem.ChapterCount; j++)
                {
                    ScriptChapter sci = sItem.GetChapter(j);
                    item.Chapters.Add(new Chapter
                                          {
                                              Begin = TimeSpan.FromSeconds(sci.Begin),
                                              End = TimeSpan.FromSeconds(sci.End),
                                              Id = sci.Id,
                                              Content = sci.Content,
                                              Description = sci.Description,
                                              Title = sci.Title,
                                              ThumbSource = new Uri(sci.ThumbSource),
                                              Type = "chapter"
                                          });
                }
                p.Add(item);
            }
            return p;
        }
    }
}