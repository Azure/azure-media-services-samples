using System.Windows.Controls;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// This is glorified contentcontrol that defaults the alignment and allows the background color to be set.
    /// Also inherits HyperlinkButton to skirt popup blocker issues
    /// </summary>
    public class ClickableAdPlayerHost : HyperlinkButton
    {
        /// <summary>
        /// Creates a new AdPlayerHost control
        /// </summary>
        public ClickableAdPlayerHost()
        {
            this.DefaultStyleKey = typeof(ClickableAdPlayerHost);
        }
    }
}
