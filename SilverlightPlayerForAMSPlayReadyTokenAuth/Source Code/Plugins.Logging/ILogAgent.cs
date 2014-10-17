using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// LogAgents handle logs sent to the logging service
    /// </summary>
    public interface ILogAgent
    {
        /// <summary>
        /// Logs are passed to this method
        /// </summary>
        /// <param name="log">The actual log object</param>
        void Log(Log log);

        /// <summary>
        /// Provides a handler for dealing with exceptions that occur due to the logging process.
        /// Note, this is different from unhandled exceptions that occur in your application. 
        /// UEs are turned into ApplicationExceptionLog objects and passed to the Log function (assuming this is enabled in the config).
        /// </summary>
        /// <param name="exception">The exception that was trapped.</param>
        void BroadcastException(Exception exception);

        /// <summary>
        /// Indicates that the session is active.
        /// </summary>
        bool IsSessionStarted { get; }

        /// <summary>
        /// Starts the logging session.
        /// </summary>
        /// <returns></returns>
        bool StartSession();

        /// <summary>
        /// Stops the logging session.
        /// </summary>
        void StopSession();

        /// <summary>
        /// Defined which logs will be passed to the Log method. Optional, return null if no filtering is to occur.
        /// </summary>
        ILogFilter Filter { get; }
    }
}
