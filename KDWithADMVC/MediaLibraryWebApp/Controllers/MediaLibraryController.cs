//----------------------------------------------------------------------------------------------
//    Copyright 2014 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using MediaLibraryWebApp.Models;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using WebGrease.Css.Extensions;

namespace MediaLibraryWebApp.Controllers
{
    [Authorize]
    public class MediaLibraryController : Controller
    {
        //
        // GET: Display a list ofstreamable  media assets 
        public async Task<ActionResult> Index()
        {
            //Initializing a model
            MediaLibraryModel model = new MediaLibraryModel();
            model.VideoList = new List<Tuple<IAsset, ILocator, Uri>>();
            model.IsCurrentUserMemberOfAdminGroup = IsAdminUser();

            try
            {
                CloudMediaContext cloudMediaContext = Factory.GetCloudMediaContext();

                IStreamingEndpoint streamingEndPoint = cloudMediaContext.StreamingEndpoints.FirstOrDefault();
                model.StreamingEndPoint = streamingEndPoint;

                // Find 30-day read-only access policy. 
                string streamingPolicy = "30d Streaming policy";
                var accessPolicy = cloudMediaContext.AccessPolicies.Where(c => c.Name == streamingPolicy).FirstOrDefault();

                //Locate all files with smooth streaming Manifest
                ListExtensions.ForEach(cloudMediaContext.Files.Where(c => c.Name.EndsWith(".ism")), file =>
                
                {
                    //skip all assets where DynamicEncryption can't be applied
                    if (file.Asset.Options != AssetCreationOptions.None)
                    {
                        return;
                    }

                    ILocator originLocator = null;
                    //Display only assets which associated with streaming 30 day policy
                    if (accessPolicy != null)
                    {
                        originLocator =
                            file.Asset.Locators.Where(
                                c => c.AccessPolicyId == accessPolicy.Id && c.Type == LocatorType.OnDemandOrigin)
                                .FirstOrDefault();
                    }
                    //If no policy has been found we are storing nulls in a model
                    Tuple<IAsset, ILocator, Uri> item = new Tuple<IAsset, ILocator, Uri>(file.Asset, originLocator, originLocator != null ? new Uri(originLocator.Path + file.Name) : null);
                    model.VideoList.Add(item);

                });

                return View(model);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> CleanAllPermissions()
        {
            var cloudMediaContext = Factory.GetCloudMediaContext();

            //Cleaning all associations with Asset
            ListExtensions.ForEach(cloudMediaContext.Files.Where(c => c.Name.EndsWith(".ism")), file => CleanAssetAccessEntities(cloudMediaContext, file.Asset));

            //Cleaning all access policies
            try
            {
                var policyDeleteTasks = cloudMediaContext.AccessPolicies.ToList().Select(policy => policy.DeleteAsync()).ToArray();
                Task.WaitAll(policyDeleteTasks);
            }
            catch (AggregateException)
            {
                
            }

            try
            {
                var keyAythPolicicesTasks = cloudMediaContext.ContentKeyAuthorizationPolicies.ToList().Select(c => c.DeleteAsync()).ToArray();
                Task.WaitAll(keyAythPolicicesTasks);
            }
            catch (AggregateException)
            {
                
            }

            try
            {
                var optionPolicyOptions = cloudMediaContext.ContentKeyAuthorizationPolicyOptions.ToList().Select(c => c.DeleteAsync()).ToArray();
                Task.WaitAll(optionPolicyOptions);
            }
            catch (AggregateException)
            {
                
            }

            return RedirectToAction("Index");
        }

        private static void CleanAssetAccessEntities(CloudMediaContext context, IAsset asset)
        {
            //Removing all locators associated with asset
            var tasks = context.Locators.Where(c => c.AssetId == asset.Id)
                    .ToList()
                    .Select(locator => locator.DeleteAsync())
                    .ToArray();
            Task.WaitAll(tasks);

            //Removing all delivery policies associated with asset
            for (int j = 0; j < asset.DeliveryPolicies.Count; j++)
            {
                asset.DeliveryPolicies.RemoveAt(0);
            }

            //removing all content keys associated with assets
            for (int j = 0; j < asset.ContentKeys.Count; j++)
            {
                asset.ContentKeys.RemoveAt(0);
            }

            Task<IMediaDataServiceResponse>[] deleteTasks = context.ContentKeyAuthorizationPolicies.Where(c => c.Name == asset.Id).ToList().Select(policy => policy.DeleteAsync()).ToArray();
            Task.WaitAll(deleteTasks);

            deleteTasks = context.ContentKeyAuthorizationPolicyOptions.Where(c => c.Name == asset.Id).ToList().Select(policyOption => policyOption.DeleteAsync()).ToArray();
            Task.WaitAll(deleteTasks);

            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> EnableJWTTokenAuthentication(string assetId,string claimType, string claimValue)
        {
            var cloudMediaContext = Factory.GetCloudMediaContext();

            // Create a 30-day readonly access policy. 
            string streamingPolicy = "30d Streaming policy";

            var accessPolicy = cloudMediaContext.AccessPolicies.Where(c => c.Name == streamingPolicy).FirstOrDefault();
            if (accessPolicy == null)
            {
                accessPolicy = cloudMediaContext.AccessPolicies.Create(streamingPolicy,
                    TimeSpan.FromDays(30),
                    AccessPermissions.Read);
            }

            var assetToConfigure = cloudMediaContext.Assets.Where(asset => asset.Id == assetId).FirstOrDefault();

            if (assetToConfigure!=null)
            {
                //Creating content keys
                CreateEnvelopeTypeContentKey(assetToConfigure, cloudMediaContext);
                //Create asset delivery policy
                CreateAssetDeliveryPolicy(assetToConfigure, assetToConfigure.ContentKeys[0], cloudMediaContext);
                //Create Authorization policy
                AddAuthorizationPolicyToContentKey(assetId, cloudMediaContext, assetToConfigure.ContentKeys[0], claimType, claimValue);

                // Create a locator to the streaming content on an origin. 
                cloudMediaContext.Locators.CreateLocator(LocatorType.OnDemandOrigin, assetToConfigure, accessPolicy, DateTime.UtcNow.AddMinutes(-5));
            }

            return RedirectToAction("Index");

        }


        public async Task<ActionResult> RemoveJWTTokenAuthentication(string assetId)
        {
            CloudMediaContext cloudMediaContext = Factory.GetCloudMediaContext();
            IAsset asset = cloudMediaContext.Assets.Where(c => c.Id == assetId).FirstOrDefault();
            CleanAssetAccessEntities(cloudMediaContext,asset);
            return RedirectToAction("Index");
        }

        public static IContentKey AddAuthorizationPolicyToContentKey(string assetID, CloudMediaContext mediaContext, IContentKey objIContentKey, string claimType, string claimValue)
        {
           //we name auth policy same as asset
            var policy = mediaContext.ContentKeyAuthorizationPolicies.Where(c => c.Name == assetID).FirstOrDefault();

            // Create ContentKeyAuthorizationPolicy with restrictions and create authorization policy             
            if (policy == null)
            {
                policy = mediaContext.ContentKeyAuthorizationPolicies.CreateAsync(assetID).Result;
            }
           
            //naming policyOption same as asset
            var policyOption = mediaContext.ContentKeyAuthorizationPolicyOptions.Where(name => name.Name == assetID).FirstOrDefault();

            if (policyOption == null)
            {

                List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>();

                List<X509Certificate2> certs = GetX509Certificate2FromADMetadataEndpoint();
                JwtSecurityToken token = GetJwtSecurityToken();

                TokenRestrictionTemplate template = new TokenRestrictionTemplate();
                template.TokenType = TokenType.JWT;
                template.PrimaryVerificationKey = new X509CertTokenVerificationKey(certs[0]);
                certs.GetRange(1, certs.Count - 1).ForEach(c => template.AlternateVerificationKeys.Add(new X509CertTokenVerificationKey(c)));
               
                
                //Ignore Empty claims
                if (!String.IsNullOrEmpty(claimType) && !String.IsNullOrEmpty(claimValue))
                {
                    template.RequiredClaims.Add(new TokenClaim(claimType, claimValue));
                }

                var audience = token.Audiences.First();
                template.Audience = new Uri(audience);
                template.Issuer = new Uri(token.Issuer);
                string requirements = TokenRestrictionTemplateSerializer.Serialize(template);

                ContentKeyAuthorizationPolicyRestriction restriction = new ContentKeyAuthorizationPolicyRestriction
                {
                    Name = "Authorization Policy with Token Restriction",
                    KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
                    Requirements = requirements
                };

                restrictions.Add(restriction);

                policyOption =
                    mediaContext.ContentKeyAuthorizationPolicyOptions.Create(assetID,
                        ContentKeyDeliveryType.BaselineHttp, restrictions, null);
                policy.Options.Add(policyOption);
                policy.UpdateAsync();
            }


            // Add ContentKeyAutorizationPolicy to ContentKey
            objIContentKey.AuthorizationPolicyId = policy.Id;
            IContentKey IContentKeyUpdated = objIContentKey.UpdateAsync().Result;

            return IContentKeyUpdated;
        }

        private static JwtSecurityToken GetJwtSecurityToken()
        {
            string jwtToken = ClaimsPrincipal.Current.FindFirst("ADJwtTokenClaim").Value;
            JwtSecurityToken token = new JwtSecurityToken(jwtToken);
            return token;
        }

        private static List<X509Certificate2> GetX509Certificate2FromADMetadataEndpoint()
        {
            List<X509Certificate2> certs = new List<X509Certificate2>();
            XPathDocument xmlReader = new XPathDocument(Configuration.MetadataUri);
            XPathNavigator navigator = xmlReader.CreateNavigator();
            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("", "urn:oasis:names:tc:SAML:2.0:metadata");
            manager.AddNamespace("ns1", "urn:oasis:names:tc:SAML:2.0:metadata");
            manager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            manager.PushScope();

            //Reading all certs since AD periodically do cert rollover
            XPathNodeIterator nodes =
                navigator.Select(
                    "//ns1:EntityDescriptor/ns1:RoleDescriptor/ns1:KeyDescriptor[@use='signing']/ds:KeyInfo/ds:X509Data/ds:X509Certificate",
                    manager);
            while (nodes.MoveNext())
            {
                XPathNavigator nodesNavigator = nodes.Current;
                //Cert body is base64 encoded in metadata doc
                certs.Add(new X509Certificate2(Convert.FromBase64String(nodesNavigator.InnerXml)));
            }

            
            return certs;
        }


        static public IContentKey CreateEnvelopeTypeContentKey(IAsset asset,CloudMediaContext context)
        {
            // Create envelope encryption content key
            Guid keyId = Guid.NewGuid();
            byte[] contentKey = GetRandomBuffer(16);

            IContentKey key = context.ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    asset.Name + "ContentKey",
                                    ContentKeyType.EnvelopeEncryption);
            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }

        static private byte[] GetRandomBuffer(int size)
        {
            byte[] randomBytes = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }


        static public void CreateAssetDeliveryPolicy(IAsset asset, IContentKey key,CloudMediaContext cloudMediaContext)
        {
            Uri keyAcquisitionUri = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.BaselineHttp);

            string envelopeEncryptionIV = Convert.ToBase64String(GetRandomBuffer(16));

            // The following policy configuration specifies: 
            //   key url that will have KID=<Guid> appended to the envelope and
            //   the Initialization Vector (IV) to use for the envelope encryption.
            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration =
                new Dictionary<AssetDeliveryPolicyConfigurationKey, string> 
                {
                    {AssetDeliveryPolicyConfigurationKey.EnvelopeKeyAcquisitionUrl, keyAcquisitionUri.ToString()},
                    {AssetDeliveryPolicyConfigurationKey.EnvelopeEncryptionIVAsBase64, envelopeEncryptionIV}
                };

            IAssetDeliveryPolicy assetDeliveryPolicy =
                cloudMediaContext.AssetDeliveryPolicies.Create(
                            "myAssetDeliveryPolicy",
                            AssetDeliveryPolicyType.DynamicEnvelopeEncryption,
                            AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS,
                            assetDeliveryPolicyConfiguration);

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);
        }

        static public string GetStreamingOriginLocator(IAsset asset,CloudMediaContext context )
        {

            // Get a reference to the streaming manifest file from the  
            // collection of files in the asset. 

            var assetFile = asset.AssetFiles.Where(f => f.Name.ToLower().
                                        EndsWith(".ism")).
                                        FirstOrDefault();

            // Create a 30-day readonly access policy. 
            IAccessPolicy policy = context.AccessPolicies.Create("Streaming policy",
                TimeSpan.FromDays(30),
                AccessPermissions.Read);

            // Create a locator to the streaming content on an origin. 
            ILocator originLocator = context.Locators.CreateLocator(LocatorType.OnDemandOrigin, asset,
                policy,
                DateTime.UtcNow.AddMinutes(-5));

            // Create a URL to the streaming manifest file. 
            return originLocator.Path + assetFile.Name;
        }

        private bool IsAdminUser()
        {
              JwtSecurityToken token = GetJwtSecurityToken();
            var admin = token.Claims.FirstOrDefault(c => c.Type == "groups" && c.Value == Configuration.AdminGroupId);
             return admin!=null ? true:false; 
        }

    }
}