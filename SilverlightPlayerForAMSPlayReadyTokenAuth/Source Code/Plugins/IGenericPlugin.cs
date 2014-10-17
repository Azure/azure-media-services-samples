using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// A generic plugin that will be loaded by the Player and given a reference to the Player so that it can
    /// perform the plugin specific functionality
    /// </summary>
    public interface IGenericPlugin : IPlugin, IPlayerConsumer
    {
    }
}