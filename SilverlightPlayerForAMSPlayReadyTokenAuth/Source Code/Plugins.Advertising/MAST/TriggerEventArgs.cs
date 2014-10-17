using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Provides information about an event related to a MAST trigger
    /// </summary>
    public class TriggerEventArgs : EventArgs
    {
        readonly Trigger trigger;

        internal TriggerEventArgs(Trigger Trigger)
        {
            trigger = Trigger;
        }

        public Trigger Trigger { get { return trigger; } }
    }

    /// <summary>
    /// Provides information about a failure involving a specific MAST trigger
    /// </summary>
    public class TriggerFailureEventArgs : TriggerEventArgs
    {
        readonly Exception exception;

        internal TriggerFailureEventArgs(Trigger Trigger, Exception Exception)
            : base(Trigger)
        {
            exception = Exception;
        }

        public Exception Exception { get { return exception; } }
    }
}
