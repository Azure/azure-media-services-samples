
namespace Microsoft.SilverlightMediaFramework.Logging.Logs
{
    /// <summary>
    /// Constants used for the log type of the base logs defined in the logging service
    /// </summary>
    public class LogTypes
    {
        public const string NotDefined = null;
        public const string ApplicationSessionStart = "ApplicationSessionStart";
        public const string ApplicationException = "ApplicationException";
    }

    /// <summary>
    /// Constants used for serialization of the base log types.
    /// </summary>
    public class LogAttributes
    {
        public const string Type = "Type";
        public const string LogId = "LogId";
        public const string RelatedLogId = "RelatedLogId";
        public const string TimeOffset = "TimeOffset";
        public const string TimeStamp = "TimeStamp";
        public const string ApplicationArea = "ApplicationArea";
        public const string Message = "Message";
        public const string StartupParam = "StartupParam";
    }

    /// <summary>
    /// Constants used serializaiton of the batch
    /// </summary>
    public class BatchAttributes
    {
        public const string ApplicationName = "ApplicationName";
        public const string ApplicationVersion = "ApplicationVersion";
        public const string ApplicationId = "ApplicationId";
        public const string SessionId = "SessionId";
        public const string InstanceId = "InstanceId";
        public const string BatchId = "BatchId";
        public const string TimeStamp = "TimeStamp";
        public const string LogsDropped = "LogsDropped";
        public const string LogsSent = "LogsSent";
        public const string TotalFailures = "TotalFailures";
    }
}
