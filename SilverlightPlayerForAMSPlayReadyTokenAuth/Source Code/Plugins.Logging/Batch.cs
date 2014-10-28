using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging.Logs;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// A container for a set of logs. Includes data of it's own.
    /// </summary>
    public class Batch : LogBase
    {
        // used for deserialization (.Deserialize())
        internal Batch()
            : base("Batch")
        { }

        internal Batch(IDictionary<string, string> Data)
            : base(Data)
        { }

        /// <summary>
        /// The name of the client application. Pulled from the config file.
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return GetRefValue<string>(BatchAttributes.ApplicationName);
            }
            set
            {
                SetRefValue<string>(BatchAttributes.ApplicationName, value);
            }
        }

        /// <summary>
        /// The version of the client application. Pulled from the config file.
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                return GetRefValue<string>(BatchAttributes.ApplicationVersion);
            }
            set
            {
                SetRefValue<string>(BatchAttributes.ApplicationVersion, value);
            }
        }

        /// <summary>
        /// A unique ID for the client application. Pulled from the config file.
        /// </summary>
        public Guid? ApplicationId
        {
            get
            {
                return GetValue<Guid>(BatchAttributes.ApplicationId);
            }
            set
            {
                SetValue<Guid>(BatchAttributes.ApplicationId, value);
            }
        }

        /// <summary>
        /// The session ID. This is randomly generated for each session of the app.
        /// </summary>
        public Guid? SessionId
        {
            get
            {
                return GetValue<Guid>(BatchAttributes.SessionId);
            }
            set
            {
                SetValue<Guid>(BatchAttributes.SessionId, value);
            }
        }

        /// <summary>
        /// And instance ID that is persisted in isolated storage to help group users across multiple sessions.
        /// </summary>
        public Guid? InstanceId
        {
            get
            {
                return GetValue<Guid>(BatchAttributes.InstanceId);
            }
            set
            {
                SetValue<Guid>(BatchAttributes.InstanceId, value);
            }
        }

        /// <summary>
        /// A generated, unique ID for the batch.
        /// </summary>
        public Guid? BatchId
        {
            get
            {
                return GetValue<Guid>(BatchAttributes.BatchId);
            }
            set
            {
                SetValue<Guid>(BatchAttributes.BatchId, value);
            }
        }

        /// <summary>
        /// The timestamp of the batch. This can be calibrated to server time if LogBatchAsync returns a LogBatchResponse containing a time offset for the server.
        /// </summary>
        public long? TimeStamp
        {
            get
            {
                return GetValue<long>(BatchAttributes.TimeStamp);
            }
            set
            {
                SetValue<long>(BatchAttributes.TimeStamp, value);
            }
        }

        /// <summary>
        /// The total number of logs dropped because of log failures in the current session
        /// </summary>
        public int? LogsDropped
        {
            get
            {
                return GetValue<int>(BatchAttributes.LogsDropped);
            }
            set
            {
                SetValue<int>(BatchAttributes.LogsDropped, value);
            }
        }

        /// <summary>
        /// The total number of logs sent in the current session
        /// </summary>
        public int? LogsSent
        {
            get
            {
                return GetValue<int>(BatchAttributes.LogsSent);
            }
            set
            {
                SetValue<int>(BatchAttributes.LogsSent, value);
            }
        }

        /// <summary>
        /// The total number of failures in the current session
        /// </summary>
        public int? TotalFailures
        {
            get
            {
                return GetValue<int>(BatchAttributes.TotalFailures);
            }
            set
            {
                SetValue<int>(BatchAttributes.TotalFailures, value);
            }
        }

        /// <summary>
        /// The actual logs
        /// </summary>
        public IEnumerable<Log> Logs { get; set; }

        public override string ToString()
        {
            return base.Type;
        }

        #region Serialization

        public const string BatchNodeName = "batch";

        /// <summary>
        /// Helper method for default serialization
        /// </summary>
        public void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(BatchNodeName);
            SerializeData(xmlWriter);
            foreach (Log log in Logs)
            {
                xmlWriter.WriteStartElement(Log.LogNodeName);
                log.SerializeData(xmlWriter);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

            xmlWriter.Flush();
        }

        /// <summary>
        ///  Helper method to deserialize a batch serailized via the default serialization
        /// </summary>
        /// <returns>The deserialized batch</returns>
        public static Batch Deserialize(XmlReader xmlReader)
        {
            if (xmlReader.GoToElement())
            {
                Batch result = new Batch();
                LogBase.DeserializeData(xmlReader, result);

                xmlReader.ReadStartElement();

                List<Log> logs = new List<Log>();
                while (xmlReader.GoToSibling())
                {
                    switch (xmlReader.LocalName)
                    {
                        case Log.LogNodeName:
                            var log = Log.Deserialize(xmlReader);
                            logs.Add(log);
                            break;

                        default:
                            xmlReader.Skip();
                            break;
                    }
                }
                result.Logs = logs;
                xmlReader.ReadEndElement();
                return result;
            }
            else
                throw new Exception("Batch is empty");
        }

        #endregion

    }
}

