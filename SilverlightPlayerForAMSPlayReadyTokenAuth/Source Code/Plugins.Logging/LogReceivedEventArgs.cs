using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// Used by SimpleLogAgent to provide access to relay logs sent to it
    /// </summary>
    public class LogReceivedEventArgs : EventArgs
    {
        public LogReceivedEventArgs(Log Log)
        {
            log = Log;
        }

        readonly Log log;
        /// <summary>
        /// The log object that has been logged.
        /// </summary>
        public Log Log
        {
            get { return log; }
        }
    }
}
