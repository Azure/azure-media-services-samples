
namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// Interface to help determine which logs should be filtered before being passed to a LogAgent.
    /// Each LogAgent can optionally return an object of this type to control filtering.
    /// </summary>
    public interface ILogFilter
    {
        /// <summary>
        /// Indicates whether or not the log should be filtered or included and therefore sent to the log agent.
        /// </summary>
        /// <param name="Log">The log in question.</param>
        /// <returns>True indicates that the log should be included. False, the log will not be passed to the LogAgent.</returns>
        bool IncludeLog(LogBase Log);
    }
}
