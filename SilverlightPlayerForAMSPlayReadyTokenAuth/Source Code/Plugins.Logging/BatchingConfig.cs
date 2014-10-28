using System;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging.Mapping;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// Configuration object to control queueing and batching behavior for the special logagent: BatchingLogAgent.
    /// </summary>
    public class BatchingConfig
    {
        /// <summary>
        /// A GUID to identify the current application
        /// </summary>
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// The application name
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The application version.
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// The max size of the queue. If null, no limit is enforced.
        /// </summary>
        public int? MaxQueueLength { get; set; }

        /// <summary>
        /// The max number of logs to pull from the queue and put in the batch. If null, no limit is enforced.
        /// </summary>
        public int? MaxBatchLength { get; set; }

        /// <summary>
        /// The max number of errors before the agent will get disabled. If null, no limit is enforced.
        /// </summary>
        public int? MaxSendErrors { get; set; }

        /// <summary>
        /// The max number of errors before the agent will get throttled. If null, no limit is enforced.
        /// </summary>
        public int? MaxSendErrorsThrottled { get; set; }

        /// <summary>
        /// The max number of logs for the entire session. If null, no limit is enforced.
        /// </summary>
        public int? MaxSessionLogs { get; set; }

        /// <summary>
        /// The max number of retries before dropping the batch and moving onto the next set of logs. This is useful if there is a chance that a particular log is causing the failure. If null, no limit is enforced.
        /// </summary>
        public int? MaxRetries { get; set; }

        /// <summary>
        /// The interval it which the queue should be processed.
        /// </summary>
        public TimeSpan QueuePollingInterval { get; set; }

        /// <summary>
        /// The interval for processing the queue when in throttled mode. This kicks in when MaxSendErrorsThrottled has been reached.
        /// </summary>
        public TimeSpan QueuePollingIntervalThrottled { get; set; }

        /// <summary>
        /// Setting to false will cause the agent to not run. This is a way to impose a kill switch via configuration.
        /// </summary>
        public bool LoggingEnabled { get; set; }

        /// <summary>
        /// The mapping rules for changing the log key value pairs to something else before passing onto the IBatchAgent.
        /// </summary>
        public MappingRules MappingRules { get; set; }

        /// <summary>
        /// The agent that batch is ultimately handed off to. Usually the batch is sent to a server.
        /// </summary>
        public IBatchAgent BatchAgent { get; set; }

        public BatchingConfig()
        {
            SetDefaultValues();
        }

        void SetDefaultValues()
        {
            ApplicationId = Guid.Empty;
            ApplicationName = "";
            ApplicationVersion = "1.0";
            MaxBatchLength = 10;
            MaxQueueLength = 200;
            MaxSendErrors = 60;
            MaxSendErrorsThrottled = 10;
            QueuePollingInterval = TimeSpan.FromSeconds(30);
            QueuePollingIntervalThrottled = TimeSpan.FromMinutes(2);
            MaxSessionLogs = 100000;
            LoggingEnabled = true;
            MaxRetries = 6;
            MappingRules = null;
        }

        public static BatchingConfig Load(Uri ConfigUri)
        {
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(ConfigUri.OriginalString, xmlSettings))
                return Load(reader);
        }

        public static BatchingConfig Load(XmlReader reader)
        {
            BatchingConfig result = new BatchingConfig();

            reader.GoToElement();
            reader.ReadStartElement();
            if (!reader.IsEmptyElement)
            {
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "Application":
                            if (!reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                                while (reader.GoToSibling())
                                {
                                    switch (reader.LocalName)
                                    {
                                        case "Id":
                                            result.ApplicationId = new Guid(reader.ReadElementContentAsString());
                                            break;
                                        case "Name":
                                            result.ApplicationName = reader.ReadElementContentAsString();
                                            break;
                                        case "Version":
                                            result.ApplicationVersion = reader.ReadElementContentAsString();
                                            break;
                                        case "Enabled":
                                            result.LoggingEnabled = reader.ReadElementContentAsBoolean();
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
                            break;
                            
                        case "Queue":
                            if (!reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                                while (reader.GoToSibling())
                                {
                                    switch (reader.LocalName)
                                    {
                                        case "maxLength":
                                            result.MaxQueueLength = reader.ReadElementContentAsInt();
                                            break;
                                        case "maxBatchLength":
                                            result.MaxBatchLength = reader.ReadElementContentAsInt();
                                            break;
                                        case "maxSendErrors":
                                            result.MaxSendErrors = reader.ReadElementContentAsInt();
                                            break;
                                        case "maxSendErrorsThrottled":
                                            result.MaxSendErrorsThrottled = reader.ReadElementContentAsInt();
                                            break;
                                        case "pollingIntervalSeconds":
                                            result.QueuePollingInterval = TimeSpan.FromSeconds(reader.ReadElementContentAsInt());
                                            break;
                                        case "pollingIntervalThrottledSeconds":
                                            result.QueuePollingIntervalThrottled = TimeSpan.FromSeconds(reader.ReadElementContentAsInt());
                                            break;
                                        case "maxRetries":
                                            result.MaxRetries = reader.ReadElementContentAsInt();
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
                            break;

                        case "Session":
                            if (!reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                                while (reader.GoToSibling())
                                {
                                    switch (reader.LocalName)
                                    {
                                        case "maxSessionLogs":
                                            result.MaxSessionLogs = reader.ReadElementContentAsInt();
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
                            break;

                        case "MappingRules":
                            if (!reader.IsEmptyElement)
                                result.MappingRules = MappingRules.Load(reader);
                            else
                                reader.Skip();
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
