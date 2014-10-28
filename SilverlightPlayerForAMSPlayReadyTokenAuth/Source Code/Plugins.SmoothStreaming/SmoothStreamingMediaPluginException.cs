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

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    /// <summary>
    /// Indicates that an error has occurred within the SmoothStreamingMediaPlugin.
    /// </summary>
	public class SmoothStreamingMediaPluginException : Exception
	{
		public SmoothStreamingMediaPluginException(string message)
			: base(message) {}

		public SmoothStreamingMediaPluginException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}
