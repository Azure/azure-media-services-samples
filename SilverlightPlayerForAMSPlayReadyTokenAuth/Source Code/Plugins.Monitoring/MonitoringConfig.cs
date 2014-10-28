using System.Xml;
using Microsoft.SilverlightMediaFramework.Diagnostics;
using Microsoft.SilverlightMediaFramework.Logging;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring
{
    /// <summary>
    /// The Main config object that contains information about how diagnostic and logging features should behave. This is normally loaded from Xml.
    /// </summary>
    public class MonitoringConfig
    {
        public MonitoringConfig()
        {
            AdditionalData = new Dictionary<string, string>();
        }

        /// <summary>
        /// Contains information required to configure the diagnostics component
        /// </summary>
        public DiagnosticsConfig DiagnosticsConfig { get; set; }

        /// <summary>
        /// Contains information required to configure the logging component
        /// </summary>
        public LoggingConfig LoggingConfig { get; set; }

        /// <summary>
        /// Contains additional data to add to every log.
        /// </summary>
        public Dictionary<string, string> AdditionalData { get; set; }

        public static MonitoringConfig Load(XmlReader reader)
        {
            MonitoringConfig result = new MonitoringConfig();

            reader.GoToElement();
            reader.ReadStartElement();
            if (!reader.IsEmptyElement)
            {
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "Diagnostics":
                            result.DiagnosticsConfig = DiagnosticsConfig.Load(reader);
                            break;
                        case "Logging":
                            result.LoggingConfig = LoggingConfig.Load(reader);
                            break;
                        case "AdditionalData":
                            string kvps = reader.ReadElementContentAsString();
                            foreach (var item in kvps.Split(','))
                            {
                                var kvp = item.Split('=');
                                result.AdditionalData.Add(kvp[0], kvp[1]);
                            }
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
            }

            return result;
        }

    }
}
