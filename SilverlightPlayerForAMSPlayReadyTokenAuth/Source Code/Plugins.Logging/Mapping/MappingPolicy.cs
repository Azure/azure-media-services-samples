using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging.Logs;

namespace Microsoft.SilverlightMediaFramework.Logging.Mapping
{
    /// <summary>
    /// Stores the rules for how to map key value pairs.
    /// Also implements methods to do the mappings.
    /// Also implements IFilter and uses these rules to tell the LoggingService which logs to filter out before they are passed onto the BatchingLogAgent.
    /// </summary>
    public class MappingRules : ILogFilter
    {
        public MappingRules()
        {
            LogMappingsRules = new Dictionary<string, LogMappings>();
        }

        /// <summary>
        /// Uses the rules to determine which logs to filter.
        /// </summary>
        /// <param name="Log">The log that is going to be logged.</param>
        /// <returns>True indicates the log should be logged and not filtered.</returns>
        public bool IncludeLog(LogBase Log)
        {
            return LogMappingsRules.ContainsKey(Log.Type);
        }

        /// <summary>
        /// A dictionary of mappings.
        /// </summary>
        public Dictionary<string, LogMappings> LogMappingsRules { get; private set; }

        /// <summary>
        /// Attempts to map a dictionary using the LogMappings
        /// </summary>
        /// <param name="Input">The log to map</param>
        /// <param name="Output">A dictionary of mapped key value pairs</param>
        /// <returns></returns>
        public IEnumerable<Exception> TryMap(LogBase Input, out IDictionary<string, string> Output)
        {
            List<Exception> errors = new List<Exception>();
            if (LogMappingsRules.ContainsKey(Input.Type))
            {
                bool ErrorOccurred = false;
                var typePolicy = LogMappingsRules[Input.Type];
                if (typePolicy.Conditions != null)
                {
                    // filtering based on condition is also available, a log must have a param set to a specific value. This is used to exclude the Snapshot quality logs
                    foreach (var condition in typePolicy.Conditions)
                    {
                        if (!Input.Data.ContainsKey(condition.Key) || Input.Data[condition.Key] != condition.Value)
                        {
                            // this log did not satisfy the condition
                            errors.Add(new Exception(string.Format("Log condition '{0}' not met for log {1}.", condition.Key, Input.Type)));
                            ErrorOccurred = true;
                        }
                    }
                }

                if (!ErrorOccurred)
                {
                    Output = new Dictionary<string, string>();
                    foreach (var policy in typePolicy.Values)
                    {
                        if (Input.Data.ContainsKey(policy.OriginalKey))
                        {
                            string value = Input.Data[policy.OriginalKey];
                            if (value != null)
                            {
                                if (policy.OriginalKey == LogAttributes.Type)
                                {
                                    if (typePolicy.SerializedId != null)
                                        Output.Add(policy.NewKey, typePolicy.SerializedId);
                                }
                                else
                                {
                                    Output.Add(policy.NewKey, value);
                                }
                            }
                            else if (!policy.Optional)
                            {
                                // policy not found for the log value
                                errors.Add(new Exception(string.Format("Log property '{0}' must be set.", policy.OriginalKey)));
                                ErrorOccurred = true;
                            }
                        }
                        else
                        {
                            // policy not found for the log value
                            errors.Add(new Exception(string.Format("Log property '{0}' not found.", policy.OriginalKey)));
                            ErrorOccurred = true;
                        }
                    }
                    if (ErrorOccurred)
                        Output.Add("err", "1");
                }
                else
                    Output = null;
            }
            else
            {
                // policy not found for the log type
                Output = null;
                errors.Add(new Exception(string.Format("Mapping Rule not found for log type '{0}'", Input.Type)));
            }

            return errors;
        }

        /// <summary>
        /// Helper method to deserialize mapping rules form xml
        /// </summary>
        public static MappingRules Load(XmlReader reader)
        {
            MappingRules result = new MappingRules();
            reader.ReadStartElement();
            while (reader.GoToSibling())
            {
                switch (reader.LocalName)
                {
                    case "Log":
                        var log = LogMappings.Load(reader);
                        result.LogMappingsRules.Add(log.Type, log);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            reader.ReadEndElement();

            return result;
        }
    }

    /// <summary>
    /// Contains the mapping rules for an individual log type
    /// </summary>
    public class LogMappings : LogMappingBase
    {
        public string Type { get; set; }
        public string SerializedId { get; set; }
        public Dictionary<string, string> Conditions;

        public static LogMappings Load(XmlReader reader)
        {
            LogMappings result = new LogMappings();

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "Type":
                        result.Type = reader.Value;
                        break;
                    case "Id":
                        result.SerializedId = reader.Value;
                        break;
                    default:
                        if (result.Conditions == null)
                            result.Conditions = new Dictionary<string, string>();
                        result.Conditions.Add(reader.Name, reader.Value);
                        break;
                }
            }

            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                result.Values = LoadValues(reader).ToList();
                reader.ReadEndElement();
            }
            else
            {
                result.Values = Enumerable.Empty<KeyValuePairMapping>().ToList();
                reader.Skip(); // no children exist, just move on
            }

            return result;
        }
    }

    /// <summary>
    /// Contains the list of key value pair mappings. This is used as both a base class for LogMapping (what a log uses to map its values) and as the actual class for batch mappings.
    /// </summary>
    public class LogMappingBase
    {
        public IList<KeyValuePairMapping> Values { get; set; }

        public static IEnumerable<KeyValuePairMapping> LoadValues(XmlReader reader)
        {
            while (reader.GoToSibling())
            {
                switch (reader.LocalName)
                {
                    case "Value":
                        yield return KeyValuePairMapping.Load(reader);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// The mapping rule for an individual key value pair
    /// </summary>
    public class KeyValuePairMapping
    {
        public string OriginalKey { get; set; }
        public string NewKey { get; set; }
        public bool Optional { get; set; }

        public static KeyValuePairMapping Load(XmlReader reader)
        {
            KeyValuePairMapping result = new KeyValuePairMapping();
            result.OriginalKey = reader.GetAttribute("Name");
            result.NewKey = reader.GetAttribute("Id");
            result.Optional = Convert.ToBoolean(Convert.ToInt32(reader.GetAttribute("Optional")));

            // advance the xml reader before departing
            reader.Skip();

            return result;
        }
    }

}
