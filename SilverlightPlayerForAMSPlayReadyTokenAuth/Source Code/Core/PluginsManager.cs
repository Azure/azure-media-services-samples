using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Utilities.Metadata;
using System.Reflection;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using System.Net;
using System.ComponentModel.Composition.ReflectionModel;
using Microsoft.SilverlightMediaFramework.Utilities;
using System.ComponentModel.Composition.Primitives;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Handles loading of plug-ins.
    /// </summary>
    /// <remarks>
    /// Works with MEF to load all types of plug-ins.
    /// </remarks>
    public class PluginsManager
    {
        private readonly static object SyncObject = new object();
        private static AggregateCatalog PluginsCatalog;
        private static CompositionContainer PluginsContainer;
        // keep this around so we can release this instance and the exports from memory
        private ComposablePart composablePart = null;

#if !PROGRAMMATICCOMPOSITION
        private const bool AllowRecomposition = true;
#else
        private const bool AllowRecomposition = false;
#endif

        /// <summary>
        ///  Using ReflectionModelServices in this way is not generally recommended.  
        ///  However, We are only using this to
        ///  retrieve Assembly information simply for diagnosability.  This
        ///  property should not be used for any other purpose.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return PluginsCatalog.Parts.Select(p => ReflectionModelServices.GetPartType(p).Value.Assembly);
            }
        }

        /// <summary>
        /// Import target for Generic Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IGenericPlugin, IDictionary<string, object>>> GenericPluginsImport { get; set; }

        /// <summary>
        /// Import target for AdPayloadHandler Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IAdPayloadHandlerPlugin, IDictionary<string, object>>> AdPayloadHandlerPluginsImport { get; set; }

        /// <summary>
        /// Import target for 3D Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IS3DPlugin, IDictionary<string, object>>> S3DPluginsImport { get; set; }

        /// <summary>
        /// Import target for Heuristcs Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IHeuristicsPlugin, IDictionary<string, object>>> MultiHeuristicsPluginsImport { get; set; }

        /// <summary>
        /// Import target for Log Writer Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<ILogWriter, IDictionary<string, object>>> LogWriterPluginsImport { get; set; }

        /// <summary>
        /// Import target for Media Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IMediaPlugin, IDictionary<string, object>>> MediaPluginsImport { get; set; }

        /// <summary>
        /// Import target for Marker Provider Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IMarkerProvider, IDictionary<string, object>>> MarkerProviderPluginsImport { get; set; }

        /// <summary>
        /// Import target for Presentation Plugins
        /// </summary>
        [ImportMany(AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<IUIPlugin, IDictionary<string, object>>> PresentationPluginsImport { get; set; }

        /// <summary>
        /// Import target for Adaptive Cache Provider Plugins
        /// </summary>
        [ImportMany(ExportAdaptiveCacheProviderAttribute.AdaptiveCacheProviderContractName, AllowRecomposition = AllowRecomposition)]
        public IEnumerable<Lazy<object>> AdaptiveCacheProviders { get; set; }


        /// <summary>
        /// Available Generic Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IGenericPlugin, IPluginMetadata>> GenericPlugins
        {
            get
            {
                return GenericPluginsImport.Select(i => new LooseMetadataLazy<IGenericPlugin, IPluginMetadata>(i));
            }
        }


        /// <summary>
        /// Available AdPayloadHandler Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IAdPayloadHandlerPlugin, IAdPayloadHandlerMetadata>> AdPayloadHandlerPlugins
        {
            get
            {
                return AdPayloadHandlerPluginsImport.Select(i => new LooseMetadataLazy<IAdPayloadHandlerPlugin, IAdPayloadHandlerMetadata>(i));
            }
        }

        /// <summary>
        /// Available 3D Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IS3DPlugin, IPluginMetadata>> S3DPlugins
        {
            get
            {
                return S3DPluginsImport.Select(i => new LooseMetadataLazy<IS3DPlugin, IPluginMetadata>(i));
            }
        }

        /// <summary>
        /// Available Heuristics Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IHeuristicsPlugin, IPluginMetadata>> HeuristicsPlugins
        {
            get
            {
                return MultiHeuristicsPluginsImport.Select(i => new LooseMetadataLazy<IHeuristicsPlugin, IPluginMetadata>(i));
            }
        }

        /// <summary>
        /// Available LogWriter Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<ILogWriter, ILogWriterMetadata>> LogWriterPlugins
        {
            get
            {
                return LogWriterPluginsImport.Select(i => new LooseMetadataLazy<ILogWriter, ILogWriterMetadata>(i));
            }
        }

        /// <summary>
        /// Available Media Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata>> MediaPlugins
        {
            get
            {
                return MediaPluginsImport.Select(i => new LooseMetadataLazy<IMediaPlugin, IMediaPluginMetadata>(i));
            }
        }

        /// <summary>
        /// Available Marker Provider Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata>> MarkerProviderPlugins
        {
            get
            {
                return MarkerProviderPluginsImport.Select(i => new LooseMetadataLazy<IMarkerProvider, IMarkerProviderMetadata>(i));
            }
        }

        /// <summary>
        /// Available Presentation Plugins
        /// </summary>
        public IEnumerable<LooseMetadataLazy<IUIPlugin, IPluginMetadata>> PresentationPlugins
        {
            get
            {
                return PresentationPluginsImport.Select(i => new LooseMetadataLazy<IUIPlugin, IPluginMetadata>(i));
            }
        }

        internal PluginsManager()
        {
            try
            {
                GenericPluginsImport = new List<Lazy<IGenericPlugin, IDictionary<string, object>>>();
                AdPayloadHandlerPluginsImport = new List<Lazy<IAdPayloadHandlerPlugin, IDictionary<string, object>>>();
                S3DPluginsImport = new List<Lazy<IS3DPlugin, IDictionary<string, object>>>();
                MultiHeuristicsPluginsImport = new List<Lazy<IHeuristicsPlugin, IDictionary<string, object>>>();
                LogWriterPluginsImport = new List<Lazy<ILogWriter, IDictionary<string, object>>>();
                MediaPluginsImport = new List<Lazy<IMediaPlugin, IDictionary<string, object>>>();
                MarkerProviderPluginsImport = new List<Lazy<IMarkerProvider, IDictionary<string, object>>>();
                PresentationPluginsImport = new List<Lazy<IUIPlugin, IDictionary<string, object>>>();
                AdaptiveCacheProviders = new List<Lazy<object>>();

                InitializeCompositionContainer();
                UpdatePlugins();
            }
            catch (Exception) { }
        }

#if WINDOWS_PHONE
        static PluginsManager() 
        {
            AssembliesToExclude = new List<string>();
            AssembliesToExclude.Add("Microsoft.Phone.Controls,");
            AssembliesToExclude.Add("Microsoft.Phone.Controls.Toolkit,");
            AssembliesToExclude.Add("Microsoft.Web.Media.SmoothStreaming,");
            AssembliesToExclude.Add("System.Windows.Interactivity,");
        }

        /// <summary>
        /// Provides a list of assembly names to exclude from the plugin search. This is available for 2 reasons:
        /// 1) This can help optimize MEF so it doesn't try to look for plugins that we know won't be found.
        /// 2) WP7 calls static constructors for all types being searched by MEF. This can help prevent problems associated with this. GestureListener in the Toolkit for example causes a problem.
        /// </summary>
        public static List<string> AssembliesToExclude { get; private set; }
#endif

        /// <summary>
        /// Can optionally be called ahead of time to preload all the plugins and speed up the loading of video
        /// </summary>
        private static void InitializeCompositionContainer()
        {
            lock (SyncObject)
            {
                if (PluginsCatalog == null || PluginsContainer == null)
                {
#if WINDOWS_PHONE
                    var currentAssembliesCatalog = new DeploymentCatalog(a => !AssembliesToExclude.Any(n => a.FullName.StartsWith(n)));
#else
                    var currentAssembliesCatalog = new DeploymentCatalog();
#endif
                    PluginsCatalog = new AggregateCatalog(currentAssembliesCatalog);
                    PluginsContainer = new CompositionContainer(PluginsCatalog);
                }
            }
        }
        
        public void UpdatePlugins()
        {
            composablePart = AttributedModelServices.CreatePart(this);
            CompositionBatch batch = new CompositionBatch(new[] { composablePart }, Enumerable.Empty<ComposablePart>());
            PluginsContainer.Compose(batch);
        }

        public void UnloadPlugins()
        {
            CompositionBatch batch = new CompositionBatch(Enumerable.Empty<ComposablePart>(), new[] { composablePart });
            PluginsContainer.Compose(batch);
        }

#if !PROGRAMMATICCOMPOSITION
        
        /// <summary>
        /// Occurs when loading of the plug-ins from the .XAP file is completed.
        /// </summary>
        public event Action<PluginsManager, Uri> AddExternalPackageCompleted;

        /// <summary>
        /// Occurs if an error occurs when loading the plug-ins from the .XAP file.
        /// </summary>
        public event Action<PluginsManager, Uri, Exception> AddExternalPackageFailed;

        /// <summary>
        /// Occurs when the progress changes on the download of an external package.
        /// </summary>
        public event Action<PluginsManager, Uri, DownloadProgressChangedEventArgs> AddExternalPackageDownloadProgressChanged;

        internal void AddCatalog(ComposablePartCatalog catalog)
        {
            lock (SyncObject)
            {
                PluginsCatalog.Catalogs.Add(catalog);
            }
        }

        /// <summary>
        /// Attempts to load the plug-ins at the specified location.
        /// </summary>
        /// <param name="xapLocation"></param>
        public void BeginAddExternalPackage(Uri xapLocation)
        {
            var catalog = new DeploymentCatalog(xapLocation);
            catalog.DownloadCompleted += new EventHandler<AsyncCompletedEventArgs>(Catalog_DownloadCompleted);
            catalog.DownloadProgressChanged += new EventHandler<DownloadProgressChangedEventArgs>(Catalog_DownloadProgressChanged);
            catalog.DownloadAsync();
        }

        private void Catalog_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var catalog = sender as DeploymentCatalog;

            if (catalog != null && AddExternalPackageDownloadProgressChanged != null)
            {
                AddExternalPackageDownloadProgressChanged(this, catalog.Uri, e);
            }
        }

        private void Catalog_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var catalog = sender as DeploymentCatalog;
            
            if (catalog != null)
            {
                if (e.Error != null)
                {
                    AddExternalPackageFailed.IfNotNull(i => i(this, catalog.Uri, e.Error));
                }
                else if (!e.Cancelled)
                {
                    AddCatalog(catalog);
                    UpdatePlugins();
                    AddExternalPackageCompleted.IfNotNull(i => i(this, catalog.Uri));
                }
            }
        }
#endif
    }
}
