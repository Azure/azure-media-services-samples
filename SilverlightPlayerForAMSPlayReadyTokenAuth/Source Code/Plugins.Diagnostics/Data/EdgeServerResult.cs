using System.Net;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Contains the result of the edge server detection operation
    /// </summary>
    public class EdgeServerResult
    {
        internal EdgeServerResult(string EdgeServer, string ClientIP)
        {
            edgeServer = EdgeServer;
            clientIP = ClientIP;
        }

        readonly string edgeServer;
        /// <summary>
        /// The edge server address. This could be a domain name or IP address.
        /// </summary>
        public string EdgeServer { get { return edgeServer; } }

        readonly string clientIP;
        /// <summary>
        /// The Client IP as returned by the edge server dataclient.
        /// </summary>
        public string ClientIP { get { return clientIP; } }
    }
}
