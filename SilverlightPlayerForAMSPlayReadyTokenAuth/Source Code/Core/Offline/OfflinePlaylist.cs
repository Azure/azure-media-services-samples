using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Core.Media;

namespace Microsoft.SilverlightMediaFramework.Core.Offline
{
    [XmlRoot(ElementName = "Playlist")]
    public class OfflinePlaylist : List<PlaylistItem>
    {

    }
}
