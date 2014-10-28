using System;
using System.Globalization;
using Microsoft.SilverlightMediaFramework.Core.Resources;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    /// <summary>
    /// Represents errors that occur when an invalid playlist is specified.
    /// </summary>
    public class InvalidPlaylistException : Exception
    {
        public InvalidPlaylistException()
            : base(SilverlightMediaFrameworkResources.errorInvalidPlaylist)
        {
        }

        public InvalidPlaylistException(string message)
            : base(message)
        {
        }

        public InvalidPlaylistException(string nodeFound, string nodeExpected)
            : base(
                String.Format(CultureInfo.CurrentUICulture, SilverlightMediaFrameworkResources.errorInvalidPlaylistNode,
                              nodeFound, nodeExpected))
        {
        }
    }
}