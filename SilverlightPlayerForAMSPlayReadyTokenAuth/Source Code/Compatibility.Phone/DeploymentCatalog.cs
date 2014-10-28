using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;


namespace System.ComponentModel.Composition.Hosting
{
    public class DeploymentCatalog : ComposablePartCatalog
    {
        public event EventHandler<AsyncCompletedEventArgs> DownloadCompleted;
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;
        IQueryable<ComposablePartDefinition> parts;

        public Uri Uri { get; set; }

        public DeploymentCatalog(Predicate<Assembly> Include)
        {
            List<Type> types = new List<Type>();
            AggregateCatalog cat = new AggregateCatalog();
            foreach (AssemblyPart part in Deployment.Current.Parts)
            {
                try
                {
                    string assemblyName = part.Source.Replace(".dll", string.Empty);
                    Assembly assembly = Assembly.Load(assemblyName);
                    if (Include == null || Include(assembly))
                    {
                        cat.Catalogs.Add(new AssemblyCatalog(assembly));
                    }
                }
                catch
                {
                }
            }
            CompositionContainer c = CompositionHost.Initialize(cat);
            int count = c.Catalog.Parts.Count();
            parts = c.Catalog.Parts;
        }

        public DeploymentCatalog()
            : this(null as Predicate<Assembly>)
        { }

        public DeploymentCatalog(Uri xapUri)
        {
        }

        public void DownloadAsync()
        {
            if (DownloadCompleted != null) DownloadCompleted(this, new AsyncCompletedEventArgs());
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                return parts.AsQueryable();
            }
        }
    }
}
