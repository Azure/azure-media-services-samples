using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Browser;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Responsible for hitting another url to retrieve information about the edge server and client IP address.
    /// Request is built and response is parsed according to rules defined in EdgeServerRules
    /// </summary>
    public class EdgeServerDataClient
    {
        /// <summary>
        /// Represents the default IP Address
        /// </summary>
        public const string IpNA = "255.255.255.255";

        /// <summary>
        /// Notifies the caller that the async request to retrieve edge server info has collected
        /// </summary>
        public event EventHandler<EdgeServerResultEventArgs> GetEdgeServerCompleted;

        /// <summary>
        /// Initiates the async request to retrieve edge server and client IP info.
        /// </summary>
        /// <param name="edgeServerRules">The rules to help define the request and how the response is parsed.</param>
        /// <param name="baseUri">The base Uri optionally used to create the request Uri. Only used if a pattern is specified in EdgeServerRules.</param>
        public void GetEdgeServerAsync(EdgeServerRules edgeServerRules, Uri baseUri)
        {
            BeginGetEdgeServer(edgeServerRules, baseUri, new AsyncCallback(ReadCallback), null);
        }

        /// <summary>
        /// Provides an async method to request to retrieve edge server and client IP info.
        /// </summary>
        /// <param name="edgeServerRules">The rules to help define the request and how the response is parsed.</param>
        /// <param name="baseUri">The base Uri optionally used to create the request Uri. Only used if a pattern is specified in EdgeServerRules.</param>
        public static IAsyncResult BeginGetEdgeServer(EdgeServerRules edgeServerRules, Uri baseUri, AsyncCallback callback, object state)
        {
            if (edgeServerRules == null)
                throw new ArgumentNullException("edgeServerRules");

            Uri ipRequestUri;
            if (baseUri != null)
                ipRequestUri = new Uri(string.Format(CultureInfo.InvariantCulture, edgeServerRules.EdgeResolverUrlPattern, baseUri.Host, baseUri.Port), UriKind.Absolute);
            else
                ipRequestUri = new Uri(edgeServerRules.EdgeResolverUrlPattern, UriKind.Absolute);

            // use the client networking stack so we can read headers
            HttpWebRequest request = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(ipRequestUri);
            for (int i = 0; i < edgeServerRules.EdgeResolverHeaders.Count; i = i + 2)
            {
                string key = edgeServerRules.EdgeResolverHeaders[i];
                string value = edgeServerRules.EdgeResolverHeaders[i + 1];
                request.Headers[key] = value;
            }
            request.Method = "GET";
            return request.BeginGetResponse(callback, new object[] { request, edgeServerRules, state });
        }

        /// <summary>
        /// The call back used in combination with BeginGetEdgeServer
        /// </summary>
        /// <returns>The result of the edge server detection request.</returns>
        public static EdgeServerResult EndGetEdgeServer(IAsyncResult asynchronousResult)
        {
            if (asynchronousResult == null)
                throw new ArgumentNullException("asynchronousResult");

            object[] args = (object[])asynchronousResult.AsyncState;
            HttpWebRequest request = (HttpWebRequest)args[0];
            EdgeServerRules edgeServerRules = (EdgeServerRules)args[1];

            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

            string result;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            string thingToParse;

            // get the edge server
            string edgeServer = "";
            thingToParse = result;
            if (edgeServerRules.EdgeHeader != null)
            {
                if (response.SupportsHeaders)
                    thingToParse = response.Headers[edgeServerRules.EdgeHeader].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
            if (edgeServerRules.EdgeRegex != null && thingToParse != null)
            {
                Regex regex = new Regex(edgeServerRules.EdgeRegex);
                if (regex.IsMatch(thingToParse))
                {
                    var matches = regex.Matches(thingToParse);
                    edgeServer = matches[0].Value;
                }
            }

            // get the client IP
            string clientIP = IpNA;
            thingToParse = result;
            if (edgeServerRules.ClientIPHeader != null)
            {
                if (response.SupportsHeaders)
                    thingToParse = response.Headers[edgeServerRules.ClientIPHeader];
            }
            if (edgeServerRules.ClientIPRegex != null && thingToParse != null)
            {
                Regex regex = new Regex(edgeServerRules.ClientIPRegex);
                if (regex.IsMatch(thingToParse))
                {
                    var matches = regex.Matches(thingToParse);
                    clientIP = matches[0].Value;
                }
            }

            return new EdgeServerResult(edgeServer, clientIP);
        }
        
        void ReadCallback(IAsyncResult asynchronousResult)
        {
            EdgeServerResultEventArgs result;

            try
            {
                result = new EdgeServerResultEventArgs(EndGetEdgeServer(asynchronousResult));
            }
            catch (Exception ex)
            {
                result = new EdgeServerResultEventArgs(ex);
            }

            if (GetEdgeServerCompleted != null)
                GetEdgeServerCompleted(this, result);
        }
    }
}
