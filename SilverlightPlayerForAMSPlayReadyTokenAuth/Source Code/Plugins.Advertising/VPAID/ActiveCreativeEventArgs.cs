using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// EventArgs used to pass information about an event associated with an ActiveCreative.
    /// </summary>
    public class ActiveCreativeEventArgs : EventArgs
    {
        internal ActiveCreativeEventArgs(ActiveCreative ActiveCreative, object UserState)
        {
            activeCreative = ActiveCreative;
            userState = UserState;
        }

        readonly ActiveCreative activeCreative;
        /// <summary>
        /// The ActiveCreative associated with the event.
        /// </summary>
        public ActiveCreative ActiveCreative
        {
            get { return activeCreative; }
        }

        readonly object userState;
        /// <summary>
        /// The UserState associated with the original call that ulimately caused this event to fire.
        /// </summary>
        public object UserState
        {
            get { return userState; }
        }
    }

    /// <summary>
    /// EventArgs used to pass information about a log event associated with an ActiveCreative for debugging purposes.
    /// </summary>
    public class ActiveCreativeLogEventArgs : EventArgs
    {
        internal ActiveCreativeLogEventArgs(ActiveCreative ActiveCreative, string Message)
        {
            activeCreative = ActiveCreative;
            message = Message;
        }

        readonly ActiveCreative activeCreative;
        /// <summary>
        /// The ActiveCreative associated with the event.
        /// </summary>
        public ActiveCreative ActiveCreative
        {
            get { return activeCreative; }
        }

        readonly string message;
        /// <summary>
        /// The log message.
        /// </summary>
        public string Message
        {
            get { return message; }
        }
    }
}
