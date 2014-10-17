using System;
using System.ComponentModel.Composition;
using System.Windows.Browser;

namespace Microsoft.HealthMonitorPlayer
{
    public class ConfigProvider
    {
        //[Export]
        //public Microsoft.Logging.Remote.BatchingConfig LoggingConfiguration
        //{
        //    get
        //    {
        //        return Microsoft.Logging.Remote.ConfigFactory.Load(new Uri("LoggingConfiguration.xml", UriKind.Relative));
        //    }
        //}

        [Export("LocalChannelName")]
        public string LocalChannelName
        {
            get
            {
                if (HtmlPage.Document.GetElementById("channelName") != null)
                    return HtmlPage.Document.GetElementById("channelName").GetAttribute("value");
                else
                    return null;
            }
        }
    }
}
