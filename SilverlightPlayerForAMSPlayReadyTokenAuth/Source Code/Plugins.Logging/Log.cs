using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging.Logs;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// This is the base class for all logs. Contains only the bare minimum set of properties.
    /// </summary>
    public class Log : LogBase
    {
        public Log(string logType)
            : base(logType)
        {
            LogId = Guid.NewGuid();
            TimeStamp = DateTimeOffset.Now;
        }

        public Log(IDictionary<string, string> Data)
            : base(Data)
        { }

        /// <summary>
        /// The ID of the log. This is a uniquely generated GUID.
        /// </summary>
        public Guid? LogId
        {
            get
            {
                return GetValue<Guid>(LogAttributes.LogId);
            }
            set
            {
                SetValue<Guid>(LogAttributes.LogId, value);
            }
        }

        /// <summary>
        /// Optional. The ID of a related log. (e.g. the ID from the VideoStartLog might set here for the VideoEndLog)
        /// </summary>
        public Guid? RelatedLogId
        {
            get
            {
                return GetValue<Guid>(LogAttributes.RelatedLogId);
            }
            set
            {
                SetValue<Guid>(LogAttributes.RelatedLogId, value);
            }
        }

        /// <summary>
        /// The time that has ellapsed since the logging session began.
        /// </summary>
        public TimeSpan? TimeOffset
        {
            get
            {
                return GetValue<TimeSpan>(LogAttributes.TimeOffset);
            }
            set
            {
                SetValue<TimeSpan>(LogAttributes.TimeOffset, value);
            }
        }

        /// <summary>
        /// The time that the log was created or when the event that it represents happened.
        /// </summary>
        public DateTimeOffset? TimeStamp
        {
            get
            {
                return GetValue<DateTimeOffset>(LogAttributes.TimeStamp);
            }
            set
            {
                SetValue<DateTimeOffset>(LogAttributes.TimeStamp, value);
            }
        }

        /// <summary>
        /// Optional. A placeholder for an application startup parameter.
        /// </summary>
        public string StartupParam
        {
            get
            {
                return GetRefValue<string>(LogAttributes.StartupParam);
            }
            set
            {
                SetRefValue<string>(LogAttributes.StartupParam, value);
            }
        }

        /// <summary>
        /// Casts a log of one type to another. Very useful for deserialization
        /// </summary>
        public T CastLog<T>() where T : Log
        {
            return CreateLog<T>(Data);
        }

        /// <summary>
        /// Static function to create a log of a specific type from a dictionary of data. Useful for deserialization.
        /// </summary>
        public static T CreateLog<T>(IDictionary<string, string> Data) where T : Log
        {
            T result = Activator.CreateInstance<T>();
            result.Data = Data;
            return result;
        }

        public override string ToString()
        {
            return this.Type;
        }

        #region Serialization

        public const string LogNodeName = "log";

        /// <summary>
        /// Deserializes a log from Xml. The type of log created will always be Log. To cast after the fact, call CreateLog or CastLog.
        /// </summary>
        public static Log Deserialize(XmlReader xmlReader)
        {
            Log result = new Log(new Dictionary<string, string>());
            if (!LogBase.DeserializeData(xmlReader, result))
                throw new Exception("Log is empty");
            return result;
        }

        /// <summary>
        /// Serializes a log to Xml.
        /// </summary>
        public void Serialize(XmlWriter xmlWriter)
        {
            // write base log
            xmlWriter.WriteStartElement(LogNodeName);
            // write all named value pairs
            base.SerializeData(xmlWriter);
            // close base log
            xmlWriter.WriteEndElement();
        }

        #endregion
    }
}
