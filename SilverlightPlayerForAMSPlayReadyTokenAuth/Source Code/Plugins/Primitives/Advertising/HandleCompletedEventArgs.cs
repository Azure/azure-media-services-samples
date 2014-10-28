using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    /// <summary>
    /// EventArgs used to send information about a completed IAdPayloadHandler.Handle operation.
    /// </summary>
    public class HandleCompletedEventArgs : EventArgs
    {
        readonly IAdSource adSource;
        readonly bool success;
        public HandleCompletedEventArgs(IAdSource AdSource, bool Success)
        {
            adSource = AdSource;
            success = Success;
        }

        /// <summary>
        /// The payload associated with operation
        /// </summary>
        public IAdSource AdSource { get { return adSource; } }

        /// <summary>
        /// Flag indicating success vs. failure. Success criteria is based on the FailureStrategy property.
        /// </summary>
        public bool Success { get { return success; } }
    }
}
