using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Displays an overlay of information on a client computer about the version of SMF 
    /// running on that computer.
    /// </summary>
    /// <remarks>
    /// The information displayed about the client computer when a user presses Ctrl-Alt-V 
    /// includes a listing of each assembly and loaded plug-in and their version numbers.
    /// This information can be useful for troubleshooting and diagnostics.
    /// </remarks>
    public class VersionInformation
    {
        internal VersionInformation() { }

        public IList<string> AssemblyNames { get; internal set; }
        public IList<IPluginMetadata> PluginMetadata { get; internal set; }
        public string PlayerId { get; internal set; }
    }
}