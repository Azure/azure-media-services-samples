using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins
{
    /// <summary>
    /// Represents a plug-in that refers to a FrameworkElement.
    /// </summary>
    public interface IUIPlugin : IPlugin
    {
        /// <summary>
        /// Gets a name for the location within the Visual Tree where this plugin will be placed, typically a ContentControl.
        /// </summary>
        string Target { get; }

        /// <summary>
        /// The FrameworkElement that should be bound to the Visual Tree.
        /// </summary>
        FrameworkElement Element { get; }
    }
}