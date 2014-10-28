using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Provides information about a MAST document download or parsing error
    /// </summary>
    public class MastFailureEventArgs : EventArgs
    {
        readonly Exception exception;
        readonly Uri mastUri;

        internal MastFailureEventArgs(Uri MastUri, Exception Exception)
        {
            mastUri = MastUri;
            exception = Exception;
        }

        /// <summary>
        /// The reason for the failure.
        /// </summary>
        public Exception Exception { get { return exception; } }

        /// <summary>
        /// The Uri of the MAST document that failed to load
        /// </summary>
        public Uri MastUri { get { return mastUri; } }
    }
}
