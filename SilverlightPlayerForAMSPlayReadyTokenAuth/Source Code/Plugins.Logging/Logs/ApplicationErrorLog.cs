
namespace Microsoft.SilverlightMediaFramework.Logging.Logs
{
    /// <summary>
    /// Contains information about unhandled exceptions. To enable you must set LoggingConfig.LogUnhandledExceptions = true.
    /// </summary>
    public class ApplicationErrorLog : Log
    {
        public ApplicationErrorLog(string applicationArea, int? MaxExceptionLength)
            : this()
        {
            ApplicationArea = applicationArea;
            this.MaxExceptionLength = MaxExceptionLength;
        }

        public ApplicationErrorLog()
            : base(LogTypes.ApplicationException)
        {

        }

        /// <summary>
        /// The max length that Exception.ToString() can be. If greater, it will be truncated.
        /// </summary>
        public int? MaxExceptionLength { get; set; }

        /// <summary>
        /// The area of the application that the exception occured in. This allows you to tag the log with custom info about the exception.
        /// </summary>
        public string ApplicationArea
        {
            get
            {
                return GetRefValue<string>(LogAttributes.ApplicationArea);
            }
            set
            {
                SetRefValue<string>(LogAttributes.ApplicationArea, value);
            }
        }

        /// <summary>
        /// The actual exception message being logged. This will be truncated to MaxExceptionLength if it set.
        /// </summary>
        public string Message
        {
            get
            {
                return GetRefValue<string>(LogAttributes.Message);
            }
            set
            {
                SetRefValue<string>(LogAttributes.Message, MaxExceptionLength.HasValue && value.Length > MaxExceptionLength.Value ? value.Substring(0, MaxExceptionLength.Value) : value);
            }
        }

    }
}
