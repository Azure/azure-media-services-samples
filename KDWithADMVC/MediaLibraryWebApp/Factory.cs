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
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace MediaLibraryWebApp
{
    public class Factory
    {
        public static ActiveDirectoryClient GetActiveDirectoryClientAsApplication(string token)
        {
            Uri servicePointUri = new Uri(Configuration.GraphResourceId);
            Uri serviceRoot = new Uri(servicePointUri, Configuration.Tenant);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot, async () => await Task.Run(() => token));
            return activeDirectoryClient;

        }
        public static CloudMediaContext GetCloudMediaContext()
        {
            var amsAccessToken = ClaimsPrincipal.Current.FindFirst(Configuration.ClaimsAmsAcessToken).Value;
            var amsCredentials = new MediaServicesCredentials(Configuration.MediaAccount, Configuration.MediaKey);
            amsCredentials.AccessToken = amsAccessToken;
            CloudMediaContext cntx = new CloudMediaContext(amsCredentials);
            return cntx;
        }
    }
}