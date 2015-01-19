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
using System.Web;
using MediaLibraryWebApp.Utils;
using Microsoft.WindowsAzure.MediaServices.Client;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Security.Claims;
using AuthenticationContext = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext;

namespace MediaLibraryWebApp
{
    public partial class Startup
    {
        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The App Key is a credential used to authenticate the application to Azure AD.  Azure AD supports password and certificate credentials.
        // The Metadata Address is used by the application to retrieve the signing keys used by Azure AD.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Authority is the sign-in URL of the tenant.
        // The Post Logout Redirect Uri is the URL where the user will be redirected after they sign out.
        //


        // This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = MediaLibraryWebApp.Configuration.ClientId,
                    Authority = MediaLibraryWebApp.Configuration.Authority,
                    PostLogoutRedirectUri = MediaLibraryWebApp.Configuration.PostLogoutRedirectUri,

                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        //
                        // If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
                        //
                        AuthorizationCodeReceived = (context) =>
                        {
                            var code = context.Code;

                            ClientCredential credential = new ClientCredential(MediaLibraryWebApp.Configuration.ClientId, MediaLibraryWebApp.Configuration.AppKey);
                            string userObjectID = context.AuthenticationTicket.Identity.FindFirst(MediaLibraryWebApp.Configuration.ClaimsObjectidentifier).Value;
                            
                            AuthenticationContext authContext = new AuthenticationContext(MediaLibraryWebApp.Configuration.Authority, new NaiveSessionCache(userObjectID));
                            AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(code, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)), credential, MediaLibraryWebApp.Configuration.GraphResourceId);

                            //Initializing  MediaServicesCredentials in order to obtain access token to be used to connect 
                            var amsCredentials = new MediaServicesCredentials(MediaLibraryWebApp.Configuration.MediaAccount, MediaLibraryWebApp.Configuration.MediaKey);
                            //Forces to get access token
                            amsCredentials.RefreshToken();

                            //Adding token to a claim so it can be accessible within controller
                            context.AuthenticationTicket.Identity.AddClaim(new Claim(MediaLibraryWebApp.Configuration.ClaimsJwtToken,result.AccessToken));
                            context.AuthenticationTicket.Identity.AddClaim(new Claim(MediaLibraryWebApp.Configuration.ClaimsAmsAcessToken, amsCredentials.AccessToken));
                            return Task.FromResult(0);
                        }

                    }

                });
        }
    }
}