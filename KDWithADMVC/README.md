MediaLibraryWebApp
==================================

This sample shows how to build an MVC web application that uses Azure Media Services .NET SDK to display video gallery to user. Based on AD user group membership they will be able to see content. User belonging to admin AD group will be able to configure Azure Media Services Key Delivery authorization policies to restrict access for content keys. Content keys are used too dynamically decrypt video stream to user. 

## How To Run This Sample
To run this sample you will need:
- Visual Studio 2013
- An Internet connection
- An Azure subscription

Every Azure subscription has an associated Azure Active Directory tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [http://wwww.windowsazure.com](http://www.windowsazure.com).  All of the Azure AD features used by this sample are available free of charge.
Azure Media Services tenant can be provisioned through Azure Portal and regular pricing is applied.

### Step 1:  Clone or download this repository

From your shell or command line:

`git clone https://github.com/azure-media-services-samples.git`

### Step 2:  Provision  Azure Media Service account and encode few video files to be used in example

1. Use the Azure Management Portal to create an Azure Media Services account. For more information, see [How to Create a Media Services Account](http://azure.microsoft.com/en-us/documentation/articles/media-services-create-account/).

2. Use the Portal to upload an asset. See the steps described in the [How to: Upload content](http://azure.microsoft.com/en-us/documentation/articles/media-services-manage-content/) section. 

3.  Use the Portal to encode the asset. See the steps described in the [How to: Encode content](http://azure.microsoft.com/en-us/documentation/articles/media-services-manage-content/) section and choose the **Playback on PC/Mac (via Flash/Silverlight)** preset from the Azure Media Encoder dialog box.

4.  Use the Portal to publish the asset. See the steps described in the [How to: Publish content](http://azure.microsoft.com/en-us/documentation/articles/media-services-manage-content/) section.

Once your asset is published, you can use the steps described in [How to: Play content from the portal](http://azure.microsoft.com/en-us/documentation/articles/media-services-manage-content/) section to stream your asset. You can also use one of the following players to test your stream: [http://amsplayer.azurewebsites.net/](http://amsplayer.azurewebsites.net/) or [http://smf.cloudapp.net/healthmonitor](http://smf.cloudapp.net/healthmonitor)


### Step 3:  Create a few user accounts and groups in your Azure Active Directory tenant

1. If you already have a user account in your Azure Active Directory tenant, you can skip to the next step.  This sample will not work with a Microsoft account, so if you signed in to the Azure portal with a Microsoft account and have never created a user account in your directory before, you need to do that now.  If you create an account and want to use it to sign-in to the Azure portal, don't forget to add the user account as a co-administrator of your Azure subscription.
2.Create few more accounts to be able to see that different users have different access right to video gallery
3. Create Admin Group and save aside value ObjectID of this group
4. Create one or more additional groups
5. Assign one users to be in admin group. This user will be able to configure authorization policies within MediaLibraryWebApp 
6. Assign other users between other groups

### Step 4:  Register the sample with your Azure Active Directory tenant


#### Register the MediaLibraryWebApp web app

1. Sign in to the [Azure management portal](https://manage.windowsazure.com).
2. Click on Active Directory in the left hand nav.
3. Click the directory tenant where you wish to register the sample application.
4. Click the Applications tab.
5. In the drawer, click Add.
6. Click "Add an application my organization is developing".
7. Enter a friendly name for the application, for example "MediaLibraryWebApp", select "Web Application and/or Web API", and click next.
8. For the sign-on URL, enter the base URL for the sample, which is by default `https://localhost:44322/`.  NOTE:  It is important, due to the way Azure AD matches URLs, to ensure there is a trailing slash on the end of this URL.  If you don't include the trailing slash, you will receive an error when the application attempts to redeem an authorization code.
9. For the App ID URI, enter `https://<your_tenant_name>/MediaLibraryWebApp`, replacing `<your_tenant_name>` with the name of your Azure AD tenant.  Click OK to complete the registration.
10. While still in the Azure portal, click the Configure tab of your application.
11. Find the Client ID value and copy it aside, you will need this later when configuring your application.
12. Create a new key for the application.  Save the configuration so you can view the key value.  Save this aside for when you configure the project in Visual Studio.
13. Download 'MediaLibraryWebApp' application manifest from Azure portal
14. Find property `groupMembershipClaims` and change it value to `All`. `"groupMembershipClaims": "All",` 
15. Upload application manifest back to Azure portal
16. In section 'Permission to other applications ' select Windows Azure Active Directory Application permissions and check all checkboxes.

### Step 4:  Configure the sample to use your Azure AD tenant and Azure Media Service

#### Configure the MediaLibraryWebApp project

1. Open the solution in Visual Studio 2013.
2. Open the `web.config` file.
3. Find the app key `ida:Tenant` and replace the value with your AAD tenant name.
4. Find the app key `ida:ClientId` and replace the value with the Client ID for the MediaLibraryWebApp from the Azure portal.
5. Find the app key `ida:AppKey` and replace the value with the key for the MediaLibraryWebApp from the Azure portal.
6. If you changed the base URL of the MediaLibraryWebApp sample, find the app key `ida:PostLogoutRedirectUri` and replace the value with the new base URL of the sample.
7.  Find the app key `ida:FederationMetaDataUri` and replace it with the FederationMetaDataUri from the Azure portal. Click 'View Endpoints' in domain applications list screen.
8. Find the app key `ida:AdminGroupObjectId` and replace the value with your Admin group ObjectID obtained earlier in Step 3.
9.  Find the app key `ida:MediaServicesAccount` and replace the value with you Azure Media Services account name 
10. Find the app key `ida:MediaServicesKey` and replace the value with you Azure Media Services account key. You can find value in Azure portal.      




### Step 5:  Run the sample

Clean the solution, rebuild the solution, and run it.  You might want to go into the solution properties and set both projects as startup projects, with the service project starting first.



