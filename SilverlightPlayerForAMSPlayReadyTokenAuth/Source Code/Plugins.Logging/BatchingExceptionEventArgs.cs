using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// EventArgs used to return an exception that occured during batching.
    /// </summary>
    public class BatchingExceptionEventArgs : EventArgs
    {
        internal BatchingExceptionEventArgs(Exception Exception)
        {
            exception = Exception;
        }

        private readonly Exception exception;

        public Exception Exception { get { return exception; } }
    }
}
