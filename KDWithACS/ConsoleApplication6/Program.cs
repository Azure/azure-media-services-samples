using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;

namespace ConsoleApplication6
{
    //
    //  A simple data contract to use with the DataContractJsonSerializer to parse the token resposne from ACS
    //
    [DataContract]
    public class OAuth2TokenResponse
    {
        /// <summary> 
        /// Gets or sets current access token value.
        /// </summary> 
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary> 
        /// Gets or sets current refresh token value. 
        /// </summary> 
        [DataMember(Name = "expires_in")]
        public int ExpirationInSeconds { get; set; }
    }

    class Program
    {
        /// <summary>
        /// A helper method to generate a content key value
        /// </summary>
        /// <returns>A content key value</returns>
        private static byte[] GetRandomKeyValue()
        {
            byte[] keyValue = new byte[16];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyValue);
            }

            return keyValue;
        }
        
        /// <summary>
        /// This method shows how to acquire a token from ACS
        /// </summary>
        /// <param name="clientId">The name of the Service Identity account used for authentication</param>
        /// <param name="clientSecret">The password or key credential used along with the clientId for authentication</param>
        /// <param name="scope">The Realm configured in the ACS Relying Party Application</param>
        /// <param name="issuer">The ACS endpoint to send the token request to</param>
        /// <returns>An authentication token from ACS</returns>
        public static string GetToken(string clientId, string clientSecret, Uri scope, Uri issuer)
        {
            string tokenToReturn = null;

            using (WebClient client = new WebClient())
            {
                //
                //  Create the authentication request to get a token
                //
                client.BaseAddress = issuer.AbsoluteUri;

                var oauthRequestValues = new NameValueCollection
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"scope", scope.AbsoluteUri},
                };

                byte[] responseBytes = null;

                try
                {
                    responseBytes = client.UploadValues("/v2/OAuth2-13", "POST", oauthRequestValues);
                }
                catch (WebException we)
                {
                    //
                    //  We hit an exception trying to acquire the token.  Write out the response and then throw
                    //
                    Stream stream = we.Response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    Console.WriteLine("Error response when trying to acquire the token: {0}", reader.ReadToEnd());
                    
                    throw;
                }

                //
                //  Process the response from ACS to get the token
                //
                using (var responseStream = new MemoryStream(responseBytes))
                {
                    OAuth2TokenResponse tokenResponse = (OAuth2TokenResponse)new DataContractJsonSerializer(typeof(OAuth2TokenResponse)).ReadObject(responseStream);
                    tokenToReturn = tokenResponse.AccessToken;
                }
            }

            return tokenToReturn;
        }

        /// <summary>
        /// This is a helper method to setup a Content Key for delivery from Azure Media Services
        /// </summary>
        /// <param name="context">The CloudMediaContext instance used to communicate with Azure Media Services</param>
        /// <param name="restrictions">One or more restrictions to add to the ContentKeyAuthorizationPolicy associated with the ContentKey</param>
        /// <returns>A ContentKey that is configured for delivery to clients</returns>
        private static IContentKey SetupContentKey(CloudMediaContext context, List<ContentKeyAuthorizationPolicyRestriction> restrictions)
        {
            //
            //  Create the ContentKeyAuthorizationPolicyOption
            //
            IContentKeyAuthorizationPolicyOption option =
                context.ContentKeyAuthorizationPolicyOptions.Create("Option with Token Restriction",
                                                                    ContentKeyDeliveryType.BaselineHttp,
                                                                    restrictions,
                                                                    null);

            //
            //  Create the ContentKeyAuthorizationPolicy and add the option from above
            //
            IContentKeyAuthorizationPolicy policy = context.ContentKeyAuthorizationPolicies.CreateAsync("Policy to illustrate integration with ACS").Result;
            policy.Options.Add(option);

            //
            //  Create the ContentKey and associate the policy from above
            //
            byte[] keyValue = GetRandomKeyValue();
            Guid keyId = Guid.NewGuid();
            IContentKey contentKey = context.ContentKeys.Create(keyId, keyValue, "Test Key", ContentKeyType.EnvelopeEncryption);
            contentKey.AuthorizationPolicyId = policy.Id;
            contentKey.Update();

            return contentKey;
        }

        /// <summary>
        /// This function sets up a ContentKeyAuthorizationPolicyRestriction from the given inputs.  This enables the
        /// Key Delivery Service to validate tokens presented by clients against this values, ensuring that the tokens
        /// are signed with the correct key and that they have the specified issuer and scope value.
        /// </summary>
        /// <param name="name">The name given to the ContentKeyAuthorizationPolicyRestriction object</param>
        /// <param name="issuer">The ACS endpoint to send the token request to</param>
        /// <param name="scope">The Realm configured in the ACS Relying Party Application</param>
        /// <param name="signingKey">The TokenSigning Keyfrom the ACS Relying Party Application</param>
        /// <returns>A list containing a single policy restriction requiring the client to provide a token with the given parameters</returns>
        private static List<ContentKeyAuthorizationPolicyRestriction> GetTokenRestriction(string name, Uri issuer, Uri scope, byte[] signingKey)
        {
            TokenRestrictionTemplate tokenTemplate = new TokenRestrictionTemplate();
            tokenTemplate.Issuer = issuer;
            tokenTemplate.Audience = scope;
            tokenTemplate.TokenType = TokenType.SWT;
            tokenTemplate.PrimaryVerificationKey = new SymmetricVerificationKey(signingKey);

            string requirements = TokenRestrictionTemplateSerializer.Serialize(tokenTemplate);

            List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>()
                {
                    new ContentKeyAuthorizationPolicyRestriction()
                        { 
                            KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted, 
                            Requirements = requirements, 
                            Name = name
                        }
                };

            return restrictions;
        }

        static void Main(string[] args)
        {
            //
            //  Get all of the values we need from the App.Config
            //
            string mediaServicesAccountName = ConfigurationManager.AppSettings["MediaServiceAccountName"];
            string mediaServicesAccountKey = ConfigurationManager.AppSettings["MediaServiceAccountKey"];
            string clientId = ConfigurationManager.AppSettings["AcsAccountName"];
            string clientSecret = ConfigurationManager.AppSettings["AcsAccountKey"];
            string issuerString = ConfigurationManager.AppSettings["AcsEndpoint"];
            string scopeString = ConfigurationManager.AppSettings["AcsScope"];
            string signingKeyString = ConfigurationManager.AppSettings["AcsSigningKey"];
            Uri issuer = new Uri(issuerString);
            Uri scope = new Uri(scopeString);
            byte[] signingKey = Convert.FromBase64String(signingKeyString);

            //
            //  Create the context to talk to Azure Media Services
            //
            MediaServicesCredentials creds = new MediaServicesCredentials(mediaServicesAccountName, mediaServicesAccountKey);
            CloudMediaContext context = new CloudMediaContext(creds);

            //
            //  Setup Media Services for Key Delivery of an Envelope Content Key with a Token Restriction.
            //  The GetTokenRestriction method has all of the details on how the ACS parameters from the App.Config
            //  are used to configure the token validation logic associated with delivering the content key.
            //
            List<ContentKeyAuthorizationPolicyRestriction> restrictions = GetTokenRestriction("Token Restriction using token from ACS", issuer, scope, signingKey);

            IContentKey contentKey = SetupContentKey(context, restrictions);

            //
            //  Now simulate a client downloading the content key to use for playback by 
            //  using ACS to get an authentication token and using the token to download
            //  the Envelope key from the Key Delivery service.
            //
            string authToken = GetToken(clientId, clientSecret, scope, issuer);

            Uri keyUrl = contentKey.GetKeyDeliveryUrl(ContentKeyDeliveryType.BaselineHttp);

            using (WebClient client = new WebClient())
            {
                Console.WriteLine("Token=Bearer " + authToken);
                client.Headers["Authorization"] = "Bearer " + authToken;
                byte[] downloadedKeyValue = client.DownloadData(keyUrl);

                Console.WriteLine("Content Key acquired successfully!");
            }

            Console.ReadLine();
        }
    }
}
