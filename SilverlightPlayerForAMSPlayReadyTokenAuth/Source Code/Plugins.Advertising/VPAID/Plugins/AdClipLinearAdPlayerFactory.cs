using System;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using System.ComponentModel.Composition;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID.Plugins
{
    /// <summary>
    /// Provides an IVpaidFactory implementation for the AdClipLinearAdPlayer
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    public class AdClipLinearAdPlayerFactory : IGenericPlugin, IVpaidFactory
    {
        private const string PluginName = "AdClipLinearAdPlayerFactory";
        private const string PluginDescription = "An ad player capable of playing video ads inside the player's ActiveMediaPlugin.";
        private const string PluginVersion = "2.2012.0605.0";

        public const string Key_IsScheduleClipEnabled = "Microsoft.Advertising.Vpaid.IsScheduleClipEnabled";

        public bool IsScheduleClipEnabled { get; set; }

        IPlayer player;

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
            player = null;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        public virtual void SetPlayer(FrameworkElement player)
        {
            this.player = player as IPlayer;
            IsScheduleClipEnabled = GetIsScheduleClipEnabled(this.player);
        }

        public static bool GetIsScheduleClipEnabled(IPlayer player)
        {
            // use a metadata item to determine priority of this vpaid plugin
            if (player.GlobalConfigMetadata != null && player.GlobalConfigMetadata.ContainsKeyIgnoreCase(Key_IsScheduleClipEnabled))
            {
                var isScheduleClipEnabledObject = player.GlobalConfigMetadata[Key_IsScheduleClipEnabled];

                if (isScheduleClipEnabledObject is bool)
                {
                    return (bool)isScheduleClipEnabledObject;
                }
                else if (isScheduleClipEnabledObject is string)
                {
                    var isScheduleClipEnabledString = (string)isScheduleClipEnabledObject;
                    bool isScheduleClipEnabledResult;
                    if (bool.TryParse(isScheduleClipEnabledString, out isScheduleClipEnabledResult))
                    {
                        return isScheduleClipEnabledResult;
                    }
                }
            }
            return true;
        }
        #endregion

        public PriorityCriteriaEnum CheckSupport(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            if (!IsScheduleClipEnabled) return PriorityCriteriaEnum.NotSupported;
            if (AdSource.MimeType == null) return PriorityCriteriaEnum.NotSupported;
            if (AdSource.Type != CreativeSourceType.Linear) return PriorityCriteriaEnum.NotSupported;
            if (AdTarget.TargetSource == null || AdTarget.TargetSource.Region != TargetRegions.VideoArea) return PriorityCriteriaEnum.NotSupported;
            if (!(player.ActiveMediaPlugin is IAdaptiveMediaPlugin) || !player.ActiveMediaPlugin.SupportsAdScheduling) return PriorityCriteriaEnum.NotSupported;
#if HTTP_ONLY
            if (!AdSource.MediaSource.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return PriorityCriteriaEnum.NotSupported;
            }
#endif

            switch (AdSource.MimeType.ToLower())
            {
                case "video/mp4":
                case "video/x-ms-wmv":
                    return PriorityCriteriaEnum.Dynamic | PriorityCriteriaEnum.Native | PriorityCriteriaEnum.Progressive | PriorityCriteriaEnum.InPlayer;
                case "text/xml":
                    return PriorityCriteriaEnum.Dynamic | PriorityCriteriaEnum.Native | PriorityCriteriaEnum.Adaptive | PriorityCriteriaEnum.InPlayer;
                default:
                    return PriorityCriteriaEnum.NotSupported;
            }
        }

        public IVpaid GetVpaidPlayer(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            return new AdClipLinearAdPlayer(AdSource, AdTarget, player);
        }
    }

}
