using System;
using System.Xml;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Stores the information necessary for configuring the trace monitor.
    /// The trace monitor is the module that polls information from SSME at a regular interval on a background thread as well as retrieves system information.
    /// </summary>
    public class TracingConfig
    {
        public TracingConfig()
        {
            SetDefaultSettings();
        }

        void SetDefaultSettings()
        {
            PollingInterval = TimeSpan.FromSeconds(2);
            TracingConfigFile = null;
#if !IGNORECPU
            RecordCpuLoad = true;
#endif
        }

        /// <summary>
        /// The relative Uri that contains the information used to configure the SSME tracing feature.
        /// Default tracing config is provided if this is set not set.
        /// See http://msdn.microsoft.com/en-us/library/ee532534(v=VS.90).aspx for more information.
        /// </summary>
        public string TracingConfigFile { get; set; }

        /// <summary>
        /// This is the polling interval used to retrieve info from the SSME trace entry queue.
        /// Default value is 2 seconds.
        /// </summary>
        public TimeSpan PollingInterval {get; set;}

        /// <summary>
        /// Indicates whether or not Cpu information should be gathered at the regular polling interval.
        /// Default is true. Set to False if you do not need this information and need to improve performance.
        /// </summary>
        public bool RecordCpuLoad { get; set; }

        /// <summary>
        /// Creates an instance of the TracingConfig from an Xml file.
        /// </summary>
        public static TracingConfig Load(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var result = new TracingConfig();

            reader.GoToElement();
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "TracingConfigFile":
                            result.TracingConfigFile = reader.ReadElementContentAsString();
                            break;
                        case "PollingMilliseconds":
                            result.PollingInterval = TimeSpan.FromMilliseconds(reader.ReadElementContentAsInt());
                            break;
                        case "RecordCpuLoad":
                            result.RecordCpuLoad = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
                reader.ReadEndElement();
            }
            else
                reader.Skip();

            return result;
        }
    }
}
