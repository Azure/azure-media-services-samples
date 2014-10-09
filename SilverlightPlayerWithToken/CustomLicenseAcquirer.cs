using System;
using System.Net;
using System.Windows.Media;
using System.IO;

namespace SmoothStreamingPlayer
{
    public class CustomLicenseAcquirer : LicenseAcquirer
    {
        private string challengeString;  //hold licenseChallenge string 

        // called before MediaOpened is raised, and when the Media pipeline is building a topology
        protected override void OnAcquireLicense(Stream licenseChallenge, Uri licenseServerUri)
        {
            StreamReader objStreamReader = new StreamReader(licenseChallenge);
            challengeString = objStreamReader.ReadToEnd();

            // set License Server URL, based on whether there is an override
            Uri resolvedLicenseServerUri;
            if (LicenseServerUriOverride == null)
                resolvedLicenseServerUri = licenseServerUri;
            else
                resolvedLicenseServerUri = LicenseServerUriOverride;

            //SMFPlayer bug: converting & to &amp; in query string parameters. This line fixes it.
            resolvedLicenseServerUri = new Uri(System.Windows.Browser.HttpUtility.HtmlDecode(resolvedLicenseServerUri.AbsoluteUri));
            
            //construct HttpWebRequest to license server
            HttpWebRequest objHttpWebRequest = WebRequest.Create(resolvedLicenseServerUri) as HttpWebRequest;
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentType = "application/xml";
            //The headers below are necessary so that error handling and redirects are handled properly via the Silverlight client.
            objHttpWebRequest.Headers["msprdrm_server_redirect_compat"]  = "false";
            objHttpWebRequest.Headers["msprdrm_server_exception_compat"] = "false";

            //for testing ContentKeyRestrictionType.TokenRestricted
            if (Constants.AddAuthorizationToken)
            {
                string kid   = Constants.KeyId;   //"42b3ddc1-2b93-4813-b50b-af1ec3c9c771";
                string token = ContentKeyAuthorizationHelper.GenerateToken(ContentKeyAuthorizationHelper.TokenTemplateKeyClaim, kid);
                objHttpWebRequest.Headers["Authorization"] = token;   //e.g.: Bearer=urn:microsoft:azure:mediaservices:contentkeyidentifier=42b3ddc1-2b93-4813-b50b-af1ec3c9c771&urn%3aServiceAccessible=service&http%3a%2f%2fschemas.microsoft.com%2faccesscontrolservice%2f2010%2f07%2fclaims%2fidentityprovider=https%3a%2f%2fnimbusvoddev.accesscontrol.windows.net%2f&Audience=urn%3atest&ExpiresOn=1406385025&Issuer=http://testacs.com&HMACSHA256=kr1fHp0chSNaMcRimmENpk1E8LaS1ufknb8mR3xQhx4%3d
            }

            //  Initiate getting request stream  
            IAsyncResult objIAsyncResult = objHttpWebRequest.BeginGetRequestStream(new AsyncCallback(RequestStreamCallback), objHttpWebRequest);
        }

        // This method is called when the asynchronous operation completes.
        void RequestStreamCallback(IAsyncResult ar)
        {
            HttpWebRequest objHttpWebRequest = ar.AsyncState as HttpWebRequest;  
            objHttpWebRequest.ContentType = "text/xml";
            //add challengeString to HttpWebRequest
            Stream objStream = objHttpWebRequest.EndGetRequestStream(ar);
            StreamWriter objStreamWriter = new StreamWriter(objStream, System.Text.Encoding.UTF8);
            objStreamWriter.Write(challengeString);
            objStreamWriter.Close();

            // Make async call for response  
            objHttpWebRequest.BeginGetResponse(new AsyncCallback(ResponseCallback), objHttpWebRequest);
        }

        private void ResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest objHttpWebRequest = ar.AsyncState as HttpWebRequest;
            WebResponse objWebResponse = objHttpWebRequest.EndGetResponse(ar);       
            Stream objStream = objWebResponse.GetResponseStream(); 

            //get license body        
            //StreamReader objStreamReader = new StreamReader(objStream, System.Text.Encoding.UTF8);
            //string prLicense = objStreamReader.ReadToEnd();

            //objStream.Seek(0, SeekOrigin.Begin);
            SetLicenseResponse(objStream);
        }

    }  //class
}  //namespace
