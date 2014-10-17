using System.Collections.Generic;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    public interface IPlayerConsumer
    {        
        /// <summary>
        /// Passes a reference to the Player
        /// </summary>
        /// <param name="Player">A reference to the Player</param>
        void SetPlayer(FrameworkElement Player);
    }
}
