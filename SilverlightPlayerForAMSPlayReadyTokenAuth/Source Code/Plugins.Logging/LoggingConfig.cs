using System.Xml;
using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// Used to configure the behavior of the logging component
    /// </summary>
    public class LoggingConfig
    {
        public LoggingConfig() 
        {
            LogUnhandledExceptions = true;
            PreventUnhandledExceptions = false; // false by default because it could easily cause mysterious results for developers who are not aware of this.
            MaxExceptionLength = 2048;
            QueryStringParam = null;
        }

        /// <summary>
        /// Contains the querystring param that should be added to each log. Optional.
        /// </summary>
        public string QueryStringParam { get; set; }

        /// <summary>
        /// Causes all unhandled exceptions to be logged. True by default.
        /// </summary>
        public bool LogUnhandledExceptions { get; set; }

        /// <summary>
        /// Causes unhandled exceptions to be 'handled' and therefore not let other observers know about them. This is false by default but should be set to true if you do not have your own global unhandled exception handler.
        /// </summary>
        public bool PreventUnhandledExceptions { get; set; }

        /// <summary>
        /// Truncates exceptions to this maximum length. Set to null to indicate no truncation should occur. The default is 2048.
        /// </summary>
        public int? MaxExceptionLength { get; set; }

        /// <summary>
        /// Loads the configuration from Xml.
        /// </summary>
        /// <param name="reader">An XmlReader containing your configuration data.</param>
        /// <returns>A new instance of a LoggingConfig object.</returns>
        public static LoggingConfig Load(XmlReader reader)
        {
            var result = new LoggingConfig();

            reader.GoToElement();

            if (!reader.IsEmptyElement)
            {
                result.MaxExceptionLength = null;   // assume no limit is intended if param is omitted

                reader.ReadStartElement();
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "LogUnhandledExceptions":
                            result.LogUnhandledExceptions = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "PreventUnhandledExceptions":
                            result.PreventUnhandledExceptions = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "MaxExceptionLength":
                            result.MaxExceptionLength = reader.ReadElementContentAsInt();
                            break;
                        case "QueryStringParam":
                            result.QueryStringParam = reader.ReadElementContentAsString();
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
