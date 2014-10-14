using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace PlayReadyOnlyServices
{
    class Program
    {
        // Read values from the App.config file.
        private static readonly string _mediaServicesAccountName =
            ConfigurationManager.AppSettings["MediaServicesAccountName"];
        private static readonly string _mediaServicesAccountKey =
            ConfigurationManager.AppSettings["MediaServicesAccountKey"];

        private static readonly Uri _sampleIssuer =
            new Uri(ConfigurationManager.AppSettings["Issuer"]);
        private static readonly Uri _sampleAudience =
            new Uri(ConfigurationManager.AppSettings["Audience"]);

        // Field for service context.
        private static CloudMediaContext _context = null;
        private static MediaServicesCredentials _cachedCredentials = null;

        static void Main(string[] args)
        {
            // Create and cache the Media Services credentials in a static class variable.
            _cachedCredentials = new MediaServicesCredentials(
                            _mediaServicesAccountName,
                            _mediaServicesAccountKey);
            // Used the cached credentials to create CloudMediaContext.
            _context = new CloudMediaContext(_cachedCredentials);

            bool tokenRestriction = false;
            string tokenTemplateString = null;

          
            IContentKey key = CreateCommonTypeContentKey();

            // Print out the key ID and Key in base64 string format
            Console.WriteLine("Created key {0} with key value {1} ", key.Id, System.Convert.ToBase64String(key.GetClearKeyValue()));
            Console.WriteLine("PlayReady License Key delivery URL: {0}", key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense));
            
if (tokenRestriction)
    tokenTemplateString = AddTokenRestrictedAuthorizationPolicy(key);
else
    AddOpenAuthorizationPolicy(key);

Console.WriteLine("Added authorization policy: {0}", key.AuthorizationPolicyId);
Console.WriteLine();
Console.ReadLine();
        }

static public void AddOpenAuthorizationPolicy(IContentKey contentKey)
{

    // Create ContentKeyAuthorizationPolicy with Open restrictions 
    // and create authorization policy          

    List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
{
    new ContentKeyAuthorizationPolicyRestriction 
    { 
        Name = "Open", 
        KeyRestrictionType = (int)ContentKeyRestrictionType.Open, 
        Requirements = null
    }
};

    // Configure PlayReady license template.
    string newLicenseTemplate = ConfigurePlayReadyLicenseTemplate();

    IContentKeyAuthorizationPolicyOption policyOption =
        _context.ContentKeyAuthorizationPolicyOptions.Create("",
            ContentKeyDeliveryType.PlayReadyLicense,
                restrictions, newLicenseTemplate);

    IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                ContentKeyAuthorizationPolicies.
                CreateAsync("Deliver Common Content Key with no restrictions").
                Result;


    contentKeyAuthorizationPolicy.Options.Add(policyOption);

    // Associate the content key authorization policy with the content key.
    contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
    contentKey = contentKey.UpdateAsync().Result;
}

public static string AddTokenRestrictedAuthorizationPolicy(IContentKey contentKey)
{
    string tokenTemplateString = GenerateTokenRequirements();

    IContentKeyAuthorizationPolicy policy = _context.
                            ContentKeyAuthorizationPolicies.
                            CreateAsync("HLS token restricted authorization policy").Result;

    List<ContentKeyAuthorizationPolicyRestriction> restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
    {
        new ContentKeyAuthorizationPolicyRestriction 
        { 
            Name = "Token Authorization Policy", 
            KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
            Requirements = tokenTemplateString, 
        }
    };

    // Configure PlayReady license template.
    string newLicenseTemplate = ConfigurePlayReadyLicenseTemplate();

    IContentKeyAuthorizationPolicyOption policyOption =
        _context.ContentKeyAuthorizationPolicyOptions.Create("Token option",
            ContentKeyDeliveryType.PlayReadyLicense,
                restrictions, newLicenseTemplate);

    IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                ContentKeyAuthorizationPolicies.
                CreateAsync("Deliver Common Content Key with no restrictions").
                Result;

    policy.Options.Add(policyOption);

    // Add ContentKeyAutorizationPolicy to ContentKey
    contentKeyAuthorizationPolicy.Options.Add(policyOption);

    // Associate the content key authorization policy with the content key
    contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
    contentKey = contentKey.UpdateAsync().Result;

    return tokenTemplateString;
}

static private string GenerateTokenRequirements()
{
    TokenRestrictionTemplate template = new TokenRestrictionTemplate();

    template.PrimaryVerificationKey = new SymmetricVerificationKey();
    template.AlternateVerificationKeys.Add(new SymmetricVerificationKey());
    template.Audience = _sampleAudience;
    template.Issuer = _sampleIssuer;
    template.RequiredClaims.Add(TokenClaim.ContentKeyIdentifierClaim);

    return TokenRestrictionTemplateSerializer.Serialize(template);
}

static private string ConfigurePlayReadyLicenseTemplate()
{
    // The following code configures PlayReady License Template using .NET classes
    // and returns the XML string.

    PlayReadyLicenseResponseTemplate responseTemplate = new PlayReadyLicenseResponseTemplate();
    PlayReadyLicenseTemplate licenseTemplate = new PlayReadyLicenseTemplate();

    responseTemplate.LicenseTemplates.Add(licenseTemplate);

    return MediaServicesLicenseTemplateSerializer.Serialize(responseTemplate);
}


static public IContentKey CreateCommonTypeContentKey()
{
    // Create envelope encryption content key
    Guid keyId = Guid.NewGuid();
    byte[] contentKey = GetRandomBuffer(16);

    IContentKey key = _context.ContentKeys.Create(
                            keyId,
                            contentKey,
                            "ContentKey",
                            ContentKeyType.CommonEncryption);

    return key;
}



static private byte[] GetRandomBuffer(int length)
{
    var returnValue = new byte[length];

    using (var rng =
        new System.Security.Cryptography.RNGCryptoServiceProvider())
    {
        rng.GetBytes(returnValue);
    }

    return returnValue;
}


    }
}
