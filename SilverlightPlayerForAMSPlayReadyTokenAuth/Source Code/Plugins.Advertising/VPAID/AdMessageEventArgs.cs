using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Contains information about an error or log associated with a VPAID event.
    /// </summary>
    public class AdMessageEventArgs : VpaidMessageEventArgs
    {
        public AdMessageEventArgs(string Message)
        {
            message = Message;
        }

        readonly string message;
        /// <summary>
        /// The error info or log message to be logged.
        /// </summary>
        public override string Message
        {
            get { return message; }
        }
    }
}
