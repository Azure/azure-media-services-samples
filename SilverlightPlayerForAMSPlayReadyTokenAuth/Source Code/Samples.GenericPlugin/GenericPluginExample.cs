using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Samples.GenericPlugin
{
    [ExportGenericPlugin(PluginName = "GenericPluginExample",
                        PluginDescription = "This is an example of a Generic Plugin",
                        PluginVersion = "1.0")]
    public class GenericPluginExample : IGenericPlugin
    {
        public event Action<IPlugin, LogEntry> LogReady;
        public event Action<IPlugin> PluginLoaded;
        public event Action<IPlugin> PluginUnloaded;
        public event Action<IPlugin, Exception> PluginLoadFailed;
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        public bool IsLoaded { get; private set; }

        public void SetPlayer(FrameworkElement player)
        {
            App.Current.RootVisual.Dispatcher.BeginInvoke(() =>
                            System.Windows.Browser.HtmlPage.Window.Alert("Generic plugin now has a reference to the player.")
                        );
        }

        public void Load()
        {
            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
        }

        public void Unload()
        {
            IsLoaded = false;
            PluginUnloaded.IfNotNull(i => i(this));
        }
    }
}
