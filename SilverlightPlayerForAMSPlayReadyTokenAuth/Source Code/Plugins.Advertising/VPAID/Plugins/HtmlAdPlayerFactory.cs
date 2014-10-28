using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Provides an IVpaidFactory implementation for the HtmlAdPlayer
    /// </summary>
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    public class HtmlAdPlayerFactory : IGenericPlugin, IVpaidFactory
    {
        private const string PluginName = "HtmlAdPlayerFactory";
        private const string PluginDescription = "A player for HTML based ads";
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
            if (!(AdTarget is HtmlElementAdTarget)) 
                return PriorityCriteriaEnum.NotSupported;
            else
                return PriorityCriteriaEnum.Static;
        }

        public IVpaid GetVpaidPlayer(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            if (!(AdTarget is HtmlElementAdTarget))
                throw new ArgumentException("Target must be of type HtmlElementAdTarget");

            return new HtmlAdPlayer(AdSource, AdTarget as HtmlElementAdTarget);
        }
    }

}
