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
using System.Configuration;
using System.Globalization;

namespace MediaLibraryWebApp
{
    public class Configuration
    {
        //Claim name to store Azure Media Services access token
        public const string ClaimsAmsAcessToken = "AMSAccessToken";
        //Claim name to store JWT token issued by Azure Active Directory
        public const string ClaimsJwtToken = "ADJwtTokenClaim";
        //Object Identifier Claim name 
        public const string ClaimsObjectidentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        //Client ID of registered app In Azure Active Directory
        public static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        //Application key from Azure Active Directory
        public static readonly string AppKey = ConfigurationManager.AppSettings["ida:AppKey"];
        //Default instance of Azure Active Directory
        public static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        //Azure Active Directory tenant
        public static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        //Post logout redirection uri
        public static readonly string PostLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        //Azure Media Services Account Name
        public static string MediaAccount = ConfigurationManager.AppSettings["ams:MediaServicesAccount"];
        //Azure Media Services Account Key
        public static string MediaKey = ConfigurationManager.AppSettings["amsKey:MediaServicesKey"];
        public static readonly string Authority = String.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);
        //Azure Active Directory Graph API endpoint
        public static string GraphResourceId = ConfigurationManager.AppSettings["ida:GraphResourceId"];
        //Federation metadata Url
        public static string MetadataUri = ConfigurationManager.AppSettings["ida:FederationMetaDataUri"];
        //Object Id of AD security group which has application Administrator rights. Used in sample to create media asset auth policies
        public static string AdminGroupId = ConfigurationManager.AppSettings["ida:AdminGroupObjectId"];
    }
}