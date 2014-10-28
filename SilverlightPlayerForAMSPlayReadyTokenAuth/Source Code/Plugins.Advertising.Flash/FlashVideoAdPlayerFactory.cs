using System;
using System.Windows;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    public class FlashVideoAdPlayerFactory : IGenericPlugin, IVpaidFactory
    {
        private const string PluginName = "FlashVideoAdPlayerFactory";
        private const string PluginDescription = "A player for flash ads";
        private const string PluginVersion = "2.2012.0605.0";

        #region IGenericPlugin
        public event Action<IPlugin, Primitives.LogEntry> LogReady;

        public event Action<IPlugin> PluginLoaded;

        public event Action<IPlugin> PluginUnloaded;

        public event Action<IPlugin, Exception> PluginLoadFailed;

        public event Action<IPlugin, Exception> PluginUnloadFailed;

        bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
        }

        public virtual void Load()
        {
            isLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
        }

        public virtual void Unload()
        {
            isLoaded = false;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        public virtual void SetPlayer(FrameworkElement player)
        {
            // ignore
        }
        #endregion

        public PriorityCriteriaEnum CheckSupport(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            if (AdSource.MimeType == null) return PriorityCriteriaEnum.NotSupported;

            switch (AdSource.MimeType.ToLower())
            {
                case "application/x-shockwave-flash":
                    if (HtmlPage.Document.GetElementById("FlashPlayer") != null)
                        return PriorityCriteriaEnum.Dynamic | PriorityCriteriaEnum.Interactive;
                    else
                        return PriorityCriteriaEnum.NotSupported;
                case "video/mp4":
                case "video/x-flv":
                    if (HtmlPage.Document.GetElementById("FlashPlayer") != null)
                    {
                        return PriorityCriteriaEnum.Dynamic | (AdSource.IsStreaming ? PriorityCriteriaEnum.Adaptive : PriorityCriteriaEnum.Progressive);
                    }
                    else
                        return PriorityCriteriaEnum.NotSupported;
                default:
                    return PriorityCriteriaEnum.NotSupported;
            }
        }

        public IVpaid GetVpaidPlayer(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            return new FlashVideoAdPlayer(AdSource, AdTarget as HtmlElementAdTarget);
        }
    }

}
