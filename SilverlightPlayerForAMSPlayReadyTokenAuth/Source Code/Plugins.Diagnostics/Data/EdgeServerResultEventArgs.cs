using System;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Contains the results of an async call to EdgeServerDataClient
    /// </summary>
    public class EdgeServerResultEventArgs : EventArgs
    {
        internal EdgeServerResultEventArgs(EdgeServerResult Result)
        {
            result = Result;
        }

        internal EdgeServerResultEventArgs(Exception Error)
        {
            error = Error;
        }

        readonly EdgeServerResult result;
        public EdgeServerResult Result { get { return result; } }

        readonly Exception error;
        public Exception Error { get { return error; } }
    }
}
