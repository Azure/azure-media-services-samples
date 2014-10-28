using System;
using System.Collections.Generic;
using System.Reflection;
#if !WINDOWS_PHONE && !SILVERLIGHT3
using System.ComponentModel.Composition;
#endif

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public class LooseMetadataLazy<T, TMetadata> : Lazy<T, TMetadata>
    {
#if !WINDOWS_PHONE && !SILVERLIGHT3
        public LooseMetadataLazy(Func<T> factory, IDictionary<string, object> looseMetadata)
            : base(factory, AttributedModelServices.GetMetadataView<TMetadata>(looseMetadata))
        {
            LooseMetadata = looseMetadata;
        }
#else
        public LooseMetadataLazy(Func<T> factory, IDictionary<string, object> looseMetadata)
            : base(factory, default(TMetadata))
        {
            LooseMetadata = looseMetadata;
        }
#endif
        public LooseMetadataLazy(Lazy<T, IDictionary<string, object>> source)
            : this(() => source.Value, source.Metadata)
        {
        }

        public IDictionary<string, object> LooseMetadata { get; private set; }
    }
}