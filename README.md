# Azure-Media-Services-Samples

The Azure-Media-Services-Samples repository contains the following Azure Media Services samples.

## PlayReadyDynamicEncryptAndKeyDeliverySvc
Microsoft Azure Media Services enables you to deliver MPEG-DASH, Smooth Streaming, and Http-Live-Streaming (HLS) streams protected with Microsoft PlayReady DRM. Media Services also provides a service for delivering Microsoft PlayReady licenses. 
The **AESDynamicEncryptionAndKeyDeliverySvc** sample contains the code that does the following:

* Configures Media Services PlayReady license delivery service with the rights and restrictions that you want for the PlayReady DRM runtime to enforce when a user is trying to play back protected content.
* Configures the delivery policy for your asset in order to enable dynamic encryption.
 
The [Using PlayReady Dynamic Encryption and License Delivery Service](http://msdn.microsoft.com/en-us/library/azure/dn783467.aspx) explains relevant concepts, it also explains how all the parts of the **AESDynamicEncryptionAndKeyDeliverySvc** sample work.

## AESDynamicEncryptionAndKeyDeliverySvc

Media Services enables you to protect your content with AES encryption. Media Services also provides the Key Delivery service that delivers encryption keys to authorized users. If you want for Media Services to encrypt an asset, you need to associate an encryption key with the asset and also configure authorization policies for the key. When a stream is requested by a player, Media Services uses the specified key to dynamically encrypt your content using AES encryption. To decrypt the stream, the player will request the key from the key delivery service. To decide whether or not the user is authorized to get the key, the service evaluates the authorization policies that you specified for the key.

The **AESDynamicEncryptionAndKeyDeliverySvc** sample contains the code that does the following:

* Configure the key delivery service with authorization policies so that only authorized clients could receive the encryption keys. 
* Configures the delivery policy for your asset in order to enable dynamic encryption.

For a detailed explanation about relevant concepts and how the sample works, see [Using AES-128 Dynamic Encryption and Key Delivery Service](http://msdn.microsoft.com/en-us/library/azure/dn783457.aspx).

## PlayReadyStaticEncryptAndKeyDeliverySvc 

There are occasions when you need to use static encryption instead of dynamic encryption (for information about static vs. dynamic encryption, see [Dynamic Encryption vs. Static Encryption with Azure Media Services](http://mingfeiy.com/dynamic-encryption-vs-static-encryption-azure-media-services)).

The **PlayReadyStaticEncryptAndKeyDeliverySvc** sample contains code that uses static encryption to protect your content with PlayReady. 

The following article has explanations related to this sample.
[Using Static Encryption to Protect Smooth Stream and MPEG DASH with PlayReady](http://msdn.microsoft.com/en-us/library/azure/dn189154.aspx)


## AMSLiveStreaming

Microsoft Azure Media Services now enables you to stream your content live. The **AMSLiveStreaming** contains the code that you can use when performing tasks related to live streaming.

The [Working with Azure Media Services Live Streaming](http://msdn.microsoft.com/en-us/library/azure/dn783466.aspx) topic contains conceptual information related to live streaming with Media Services.

The [Creating a Live Streaming Application with the Media Services SDK for .NET](http://msdn.microsoft.com/en-us/library/azure/dn783465.aspx) topic contains information related to the code in the **AMSLiveStreaming** sample. 
