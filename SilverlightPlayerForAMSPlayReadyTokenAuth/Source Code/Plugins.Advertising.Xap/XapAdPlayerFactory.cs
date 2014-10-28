using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID.Plugins
{
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    public class XapAdPlayerFactory : IGenericPlugin, IVpaidFactory
    {
        private const string PluginName = "XapAdPlayerFactory";
        private const string PluginDescription = "An ad player capable of playing Silverlight interactive ads.";
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
            if (AdTarget is HtmlElementAdTarget) return PriorityCriteriaEnum.NotSupported;
            if (AdSource.MimeType == null) return PriorityCriteriaEnum.NotSupported;

            switch (AdSource.MimeType.ToLower())
            {
                case "application/x-silverlight-app":
                    return PriorityCriteriaEnum.Interactive | PriorityCriteriaEnum.Dynamic | PriorityCriteriaEnum.Native;
                default:
                    return PriorityCriteriaEnum.NotSupported;
            }
        }

        public IVpaid GetVpaidPlayer(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            return new XapAdPlayer(AdSource, AdTarget);
        }
    }

}
