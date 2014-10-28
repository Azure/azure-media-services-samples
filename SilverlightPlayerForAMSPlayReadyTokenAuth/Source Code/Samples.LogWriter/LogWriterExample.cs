using System;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Samples.LogWriter
{
    [ExportLogWriter(PluginName = "LogWriterExample",
                        PluginDescription = "This is an example of a Log Writer",
                        PluginVersion = "1.0",
                        LogWriterId = "LogWriterExampleId")]
    public class LogWriterExample : ILogWriter
    {
        public event Action<IPlugin, LogEntry> LogReady;
        public event Action<IPlugin> PluginLoaded;
        public event Action<IPlugin> PluginUnloaded;
        public event Action<IPlugin, Exception> PluginLoadFailed;
        public event Action<IPlugin, Exception> PluginUnloadFailed;
        public event Action<ILogWriter, LogEntry, Exception> LogWriteFailed;
        public event Action<ILogWriter, LogEntry> LogWriteSuccessful;

        public bool IsLoaded { get; private set; }

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

        public void WriteLog(LogEntry logEntry)
        {
            App.Current.RootVisual.Dispatcher.BeginInvoke(() =>
                        
                            System.Windows.Browser.HtmlPage.Window.Alert(
                                logEntry.Message)
                        );
        }
    }
}
