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

// The following using statements were added for this sample.
using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MediaLibraryWebApp.Models;
using MediaLibraryWebApp.Utils;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.OpenIdConnect;
using AuthenticationContext = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext;


namespace MediaLibraryWebApp.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/
        public async Task<ActionResult> Index()
        {
            //
            // Retrieve the user's name, tenantID, and access token since they are parameters used to query the Graph API.
            //
            UserProfile profile;
            string jwtToken = ClaimsPrincipal.Current.FindFirst(Configuration.ClaimsJwtToken).Value;
            JwtSecurityToken token = new JwtSecurityToken(jwtToken);
            string userObjectID = ClaimsPrincipal.Current.FindFirst(Configuration.ClaimsObjectidentifier).Value;
            
            AuthenticationContext authContext = new AuthenticationContext(Configuration.Authority, new NaiveSessionCache(userObjectID));
            try
            {
                ActiveDirectoryClient activeDirectoryClient = Factory.GetActiveDirectoryClientAsApplication(jwtToken);
                var userProfile = (User)await activeDirectoryClient.Users.GetByObjectId(userObjectID).ExecuteAsync();
                var groups = await activeDirectoryClient.Groups.ExecuteAsync();
                profile = new UserProfile();
                profile.Token = token;
                profile.Groups = groups.CurrentPage;
                profile.User = userProfile;
                return View(profile);
            }
            catch (Exception)
            {
                //
                // If the call failed, then drop the current access token and show the user an error indicating they might need to sign-in again.
                //
                var todoTokens = authContext.TokenCache.ReadItems().Where(a => a.Resource == Configuration.GraphResourceId);
                foreach (TokenCacheItem tci in todoTokens)
                    authContext.TokenCache.DeleteItem(tci);

                //
                // If refresh is set to true, the user has clicked the link to be authorized again.
                //
                if (Request.QueryString["reauth"] == "True")
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //
                    HttpContext.GetOwinContext().Authentication.Challenge(OpenIdConnectAuthenticationDefaults.AuthenticationType);
                }

                //
                // The user needs to re-authorize.  Show them a message to that effect.
                //
                profile = new UserProfile();
                ViewBag.ErrorMessage = "AuthorizationRequired";
                return View(profile);

            }
        }
    }
}