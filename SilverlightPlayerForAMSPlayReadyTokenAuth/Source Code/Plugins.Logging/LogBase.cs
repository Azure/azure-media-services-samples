using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging.Logs;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// The base class for all logs. The goal of this class is to store the dictionary that holds the key value pairs and provide a set of static methods to help serialize and deserialize values.
    /// </summary>
    public abstract class LogBase
    {
        protected LogBase(IDictionary<string, string> Data)
        {
            this.Data = Data;
        }

        public LogBase(string type)
            : this(new Dictionary<string, string>())
        {
            Type = type;
        }

        /// <summary>
        /// The log type. All logs must have a unique log type. (e.g. VideoStart, VideoEnd)
        /// </summary>
        public string Type
        {
            get
            {
                return GetRefValue<string>(LogAttributes.Type);
            }
            set
            {
                SetRefValue<string>(LogAttributes.Type, value);
            }
        }

        private IDictionary<string, string> data;
        /// <summary>
        /// The underlying data set for all log values. Values are always stored as strings in order to support easy serialization and deserialization.
        /// </summary>
        public IDictionary<string, string> Data
        {
            get { return data; }
            protected set { data = value; }
        }

        /// <summary>
        /// Retrieves the value for the specified key and converts it to the appropriate type
        /// </summary>
        /// <typeparam name="T">The datatype of the value you expect to receive</typeparam>
        /// <param name="Name">The key of the value</param>
        /// <returns>The value itself (casted back to the original type)</returns>
        private T GetPrimativeValue<T>(string Name) where T : struct
        {
            string value = Data[Name];
            if (typeof(T) == typeof(bool))
                return (T)(object)Convert.ToBoolean(Convert.ToInt32(value));
            else if (typeof(T) == typeof(Guid))
                return (T)(object)(new Guid(value));
            else if (typeof(T) == typeof(DateTimeOffset))
                return (T)(object)new DateTimeOffset(Convert.ToInt64(value), TimeSpan.Zero);
            else if (typeof(T) == typeof(DateTime))
                return (T)(object)new DateTime(Convert.ToInt64(value));
            else
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets the value for a given key and converts it to a string for storage.
        /// </summary>
        /// <typeparam name="T">The datatype of the value you expect to receive</typeparam>
        /// <param name="Name">The key of the value</param>
        /// <param name="Value">The value you which to store</param>
        private void SetPrimativeValue<T>(string Name, T Value) where T : struct
        {
            string result;
            if (typeof(T) == typeof(bool))
                result = Convert.ToInt32(((bool)(object)Value)).ToString();
            else if (typeof(T) == typeof(DateTimeOffset))
                result = ((DateTimeOffset)(object)Value).Ticks.ToString();
            else if (typeof(T) == typeof(DateTime))
                result = ((DateTime)(object)Value).Ticks.ToString();
            else
                result = Convert.ToString(Value, CultureInfo.InvariantCulture);

            Data[Name] = result;
        }

        /// <summary>
        /// Retrieves a primitive value for the specified key and converts it to the appropriate nullable type.
        /// </summary>
        /// <typeparam name="T">The datatype of the value you expect to receive</typeparam>
        /// <param name="Name">The key of the value</param>
        /// <returns>The value itself (casted back to the original type)</returns>
        public Nullable<T> GetValue<T>(string Name) where T : struct
        {
            if (Data.ContainsKey(Name))
                return GetPrimativeValue<T>(Name);
            else
                return null;
        }

        /// <summary>
        /// Sets a primitive value for a given key.
        /// </summary>
        /// <typeparam name="T">The datatype of the value you are setting</typeparam>
        /// <param name="Name">The key associated with the value</param>
        /// <param name="Value">The value you which to store</param>
        public void SetValue<T>(string Name, Nullable<T> Value) where T : struct
        {
            if (Value.HasValue)
                SetPrimativeValue(Name, Value.Value);
            else if (Data.ContainsKey(Name))
                Data.Remove(Name);
        }

        /// <summary>
        /// Retrieves a reference value for the specified key and converts it to the appropriate type. Null is returned if the value does not exist
        /// </summary>
        /// <typeparam name="T">The datatype of the value you expect to receive</typeparam>
        /// <param name="Name">The key of the value</param>
        /// <returns>The value itself (casted back to the original type)</returns>
        public T GetRefValue<T>(string Name) where T : class
        {
            if (Data.ContainsKey(Name))
                return (T)Convert.ChangeType(Data[Name], typeof(T), CultureInfo.InvariantCulture);
            else
                return null;
        }

        /// <summary>
        /// Sets a reference type value for a given key. Pass null to remove the value from the underlying dictionary.
        /// </summary>
        /// <typeparam name="T">The datatype of the value you are setting</typeparam>
        /// <param name="Name">The key associated with the value</param>
        /// <param name="Value">The value you which to store</param>
        public void SetRefValue<T>(string Name, T Value) where T : class
        {
            if (Value != null)
                Data[Name] = Convert.ToString(Value, CultureInfo.InvariantCulture);
            else if (Data.ContainsKey(Name))
                Data.Remove(Name);
        }

        /// <summary>
        /// Deserializes the log from an xml reader
        /// </summary>
        /// <param name="xmlReader">XmlReader containing the log info</param>
        /// <param name="logBase">The log object you would like to populate</param>
        /// <returns>Indicates whether or not it was successful. If no data is found in the xml, it will return false</returns>
        public static bool DeserializeData(XmlReader xmlReader, LogBase logBase)
        {
            if (xmlReader.MoveToFirstAttribute())
            {
                do
                {
                    logBase.Data[xmlReader.Name] = xmlReader.Value;
                } while (xmlReader.MoveToNextAttribute());
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Serializes the current log to Xml
        /// </summary>
        /// <param name="xmlWriter">The XmlWriter object you would like to store the log's values in</param>
        public void SerializeData(XmlWriter xmlWriter)
        {
            // write all named value pairs
            foreach (var nvp in data.Where(nvp => nvp.Value != null))
            {
                xmlWriter.WriteAttributeString(nvp.Key, nvp.Value);
            }
        }
    }
}
