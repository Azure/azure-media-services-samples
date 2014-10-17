using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Contains information about the Ad that was clicked. Info is included to give the player the option for handling the event.
    /// </summary>
    public class AdClickThruEventArgs : ClickThroughEventArgs
    {
        public AdClickThruEventArgs(string Url, string Id, bool PlayerHandles)
        {
            url = Url;
            id = Id;
            playerHandles = PlayerHandles;
        }

        readonly string url;
        /// <summary>
        /// The url can be used for the ad to specify the click thru url.
        /// </summary>
        public override string Url
        {
            get { return url; }
        }

        readonly string id;
        /// <summary>
        /// The ID of the Ad that was clicked. Can be used for tracking purposes.
        /// </summary>
        public override string Id
        {
            get { return id; }
        }

        readonly bool playerHandles;
        /// <summary>
        /// If true, the player must handle the event by opening a new browser window to the url. false, the ad will handle the event.
        /// </summary>
        public override bool PlayerHandles
        {
            get { return playerHandles; }
        }
    }
}
