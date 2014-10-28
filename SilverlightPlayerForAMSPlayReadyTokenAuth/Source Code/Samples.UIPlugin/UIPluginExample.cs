using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Samples.UIPlugin
{
    [ExportUIPlugin(PluginName = "UIPluginExample",
                    PluginDescription = "This is an example of a UI Plugin",
                    PluginVersion = "1.0")]
    public class UIPluginExample : IUIPlugin
    {
        private readonly DispatcherTimer _timer;

        public event Action<IPlugin, LogEntry> LogReady;
        public event Action<IPlugin> PluginLoaded;
        public event Action<IPlugin> PluginUnloaded;
        public event Action<IPlugin, Exception> PluginLoadFailed;
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        public string Target
        {
            get { return string.Empty; }
        }

        public FrameworkElement Element { get; private set; }

        

        public UIPluginExample()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(15)
            };
            _timer.Tick += (s, e) => Toggle();
        }

        public bool IsLoaded { get; private set; }

        public void Load()
        {
            Element = new TextBlock
                {
                    Text = "UI Plugin Example",
                    Foreground = new SolidColorBrush(Colors.Red),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));


            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }
        }

        private void Toggle()
        {
            if (IsLoaded)
            {
                Unload();
            }
            else
            {
                Load();
            }
        }

        public void Unload()
        {
            IsLoaded = false;
            Element = null;
            PluginUnloaded.IfNotNull(i => i(this));
        }
    }
}
