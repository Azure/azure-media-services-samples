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
using Microsoft.SilverlightMediaFramework.Utilities;

namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    public static class MetadataExtensions
    {
#if WINDOWS_PHONE
        public static bool IsValueCreated(this LooseMetadataLazy<object, object> metadata)
        {
            return false;
        }
#endif
    }
}
