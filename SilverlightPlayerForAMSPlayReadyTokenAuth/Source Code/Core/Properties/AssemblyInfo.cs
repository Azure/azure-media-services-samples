using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Microsoft.SilverlightMediaFramework")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("Microsoft Media Platform: Player Framework")]
[assembly: AssemblyCopyright("Copyright © 2011 Microsoft Corporation")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]


// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6585c320-c174-48ee-bb35-c71d156f05eb")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("2.2012.0605.0")]
[assembly: AssemblyFileVersion("2.2012.0605.0")]

[assembly: XmlnsPrefix("http://schemas.microsoft.com/smf/2010/xaml/player", "smf")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/smf/2010/xaml/player", "Microsoft.SilverlightMediaFramework.Core")]

[assembly: XmlnsPrefix("http://schemas.microsoft.com/smf/2010/xaml/media", "smf_media")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/smf/2010/xaml/media", "Microsoft.SilverlightMediaFramework.Core.Media")]

[assembly: XmlnsPrefix("http://schemas.microsoft.com/smf/2010/xaml/captions", "smf_captions")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/smf/2010/xaml/captions", "Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions")]

[assembly: XmlnsPrefix("http://schemas.microsoft.com/smf/2010/xaml/accesscontrols", "smf_accesscontrols")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/smf/2010/xaml/accesscontrols", "Microsoft.SilverlightMediaFramework.Core.Accessibility.Controls")]
