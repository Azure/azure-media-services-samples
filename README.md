# This Repository is for the legacy v2 API which is now deprecated. Customers should migrate to use the v3 API only.

Please use the latest v3 API for .NET. 
- [Configure the v3 API for .NET](https://docs.microsoft.com/en-us/azure/media-services/latest/configure-connect-dotnet-howto)
- See the v3 Tutorials [Tutorial: Encode a remote file based on URL and stream the video - .NET](https://docs.microsoft.com/en-us/azure/media-services/latest/stream-files-dotnet-quickstart)
- Check out the [v3 .NET Samples repo](https://github.com/Azure-Samples/media-services-v3-dotnet)

The new .NET SDK is located here in Nuget
https://www.nuget.org/packages/Microsoft.Azure.Management.Media/

To install the newest SDK using the .NET CLI
```
dotnet add package Microsoft.Azure.Management.Media
```

## IMPORTANT! Update your Azure Media Services REST API and SDKs to v3 by 29 February 2024

Because version 3 of Azure Media Services REST API and client SDKs for .NET and Java offers more capabilities than version 2, we’re retiring version 2 of the Azure Media Services REST API and client SDKs for .NET and Java. We encourage you to make the switch sooner to gain the richer benefits of version 3 of Azure Media Services REST API and client SDKs for .NET and Java. Version 3 provides: 

### Action Required:
To minimize disruption to your workloads, review the migration guide to transition your code from the version 2 to version 3 API and SDK before 29 February 2024. 

After 29 February 2024, Azure Media Services will no longer accept traffic on the version 2 REST API, the ARM account management API version 2015-10-01, or from the version 2 .NET client SDKs. This includes any 3rd party open-source client SDKS that may call the version 2 API.  

See [Update your Azure Media Services REST API and SDKs to v3 by 29 February 2024](https://azure.microsoft.com/en-us/updates/update-your-azure-media-services-rest-api-and-sdks-to-v3-by-29-february-2024)
