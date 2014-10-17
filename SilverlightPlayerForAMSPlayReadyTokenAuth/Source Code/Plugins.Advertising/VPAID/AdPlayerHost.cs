using System.Windows.Controls;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// This is glorified contentcontrol that defaults the alignment and allows the background color to be set.
    /// </summary>
    public class AdPlayerHost : ContentControl
    {
        /// <summary>
        /// Creates a new AdPlayerHost control
        /// </summary>
        public AdPlayerHost()
        {
            this.DefaultStyleKey = typeof(AdPlayerHost);
        }
    }
}
