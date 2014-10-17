
namespace Microsoft.SilverlightMediaFramework.Logging.Logs
{
    /// <summary>
    /// The logging session has started
    /// </summary>
    public class SessionStartLog : Log
    {
        public SessionStartLog()
            : base(LogTypes.ApplicationSessionStart)
        {
        }
    }
}
