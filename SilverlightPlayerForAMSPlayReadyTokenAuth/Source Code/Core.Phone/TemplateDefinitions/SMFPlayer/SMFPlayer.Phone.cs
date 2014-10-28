using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core
{
    public partial class SMFPlayer
    {
        const int MaxPixels = 384000; // smooth streaming supports a max pixel count of 800x480 (384000)

        partial void RestrictTracks()
        {
            OnRestrictTracks();
        }

        /// <summary>
        /// Called from OnManifestReady to restrict video tracks. Windows Phone 7 has restrictions on which tracks are allowed. Override this function to change the default logic.
        /// </summary>
        protected virtual void OnRestrictTracks()
        {
            if (ActiveAdaptiveMediaPlugin != null)
            {
                foreach (var videoStream in ActiveAdaptiveMediaPlugin.CurrentSegment.SelectedStreams.Where(i => i.Type == StreamType.Video))
                {
                    if (videoStream != null)
                    {
                        IEnumerable<IMediaTrack> excludedTracks;
                        if (IsMultiResolutionVideoSupported)
                        {
                            excludedTracks = videoStream.AvailableTracks.Where(o => o.Resolution.Height * o.Resolution.Width > MaxPixels);
                        }
                        else
                        {
                            var supportedTracks = videoStream.AvailableTracks.Where(o => o.Resolution.Height * o.Resolution.Width <= MaxPixels).ToList();
                            var unSupportedTracks = videoStream.AvailableTracks.Except(supportedTracks);

                            excludedTracks = unSupportedTracks.Concat(GetRestrictedTracks(supportedTracks.GroupBy(o => o.Resolution)));
                        }

                        if (excludedTracks.Any())
                        {
                            // restrict tracks to non-excluded tracks
                            videoStream.SetRestrictedTracks(videoStream.AvailableTracks.Except(excludedTracks));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Return a collection of tracks to be restricted.
        /// Note: Windows Phone 7 only supports tracks with the same resolution.
        /// Override this function to provide a custom selection algorithm. Remember to make sure all non-restricted tracks have the same resolution.
        /// </summary>
        /// <param name="AvailableTrackGroups">All available tracks, grouped by resolution.</param>
        /// <returns>The tracks that should be restricted.</returns>
        IEnumerable<IMediaTrack> GetRestrictedTracks(IEnumerable<IGrouping<Size, IMediaTrack>> AvailableTrackGroups)
        {
            // return the group containing the lowest bitrate
            return AvailableTrackGroups.OrderBy(g => g.Min(t => t.Bitrate)).Skip(1).SelectMany(g => g);
        }

        static bool IsMultiResolutionVideoSupported
        {
            get
            {
#if WINDOWS_PHONE && !WINDOWS_PHONE_70
                return Microsoft.Phone.Info.MediaCapabilities.IsMultiResolutionVideoSupported; 
#else
                return false;
#endif
            }
        }
    }
}
