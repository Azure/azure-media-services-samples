// -----------------------------------------------------------------------------
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

namespace CloudVideoClient.Xaml.Crypto
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using Microsoft.Media.AdaptiveStreaming;
	using Windows.Foundation;
	using Windows.Security.Cryptography;
	using Windows.Security.Cryptography.Core;

	public class DecoderDownloadPlugin : IDownloaderPlugin 
	{
		private static byte[] key = new byte[0];
		private static byte[] iv = new byte[0];

		public string TokenAuthorizationBaseUri { get; set; }


		public IAsyncOperation<DownloaderResponse> RequestAsync(DownloaderRequest pDownloaderRequest)
		{
			// Get the DownloaderResponse object for this request.
			Task<DownloaderResponse> t = this.RequestAsyncHelper(pDownloaderRequest);

			// convert the task into the Win8 app IAsyncOperation, which will automatically complete when the underlying task completes.
			return t.AsAsyncOperation();
		}
        
		public async Task<DownloaderResponse> RequestAsyncHelper(DownloaderRequest pDownloaderRequest)
		{
			DownloaderResponse response;

			/*
			 * The first call that the player makes is to request the manifest for the video to play.
			 * The response of this call is an xml with information about the video.
			 * Encrypted videos have an extra element called <Protection> that contains decryption information such as the initialization vector (IV).
			 * This <Protection> element currently not recognized by the player framework and will generate a parse error if it is returned,
			 * so in this prototype, the <Protection> element is deleted.
			 * In a full implementation, the <Protection> element would be read to get the IV.  For the prototype, the IV is hardcoded, and the decryption key
			 * is loaded from a hardcoded unsecured URL.
			 */
			if (pDownloaderRequest.RequestUri.AbsolutePath.IndexOf("manifest", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				
				WebRequest manifestRequest = WebRequest.Create(pDownloaderRequest.RequestUri);
				using (WebResponse webResponseManifest = await manifestRequest.GetResponseAsync())
				{
					var responseStream = webResponseManifest.GetResponseStream();
					XDocument xmlDocument = XDocument.Load(responseStream);

					var protectionElement = (from xmlElement in xmlDocument.Descendants("Protection") select xmlElement).FirstOrDefault();
					if (protectionElement != null)
					{
						XNamespace sea = "urn:mpeg:dash:schema:sea:2012";
                        //XNamespace sea = "urn:microsoft:azure:mediaservices:contentkeyidentifier";
						var cryptoPeriodElement = (from xmlElement in protectionElement.Descendants(sea + "CryptoPeriod") select xmlElement).FirstOrDefault();
						if (cryptoPeriodElement != null)
						{
							var ivAtrribute = cryptoPeriodElement.Attribute("IV");
							var keyUriTemplateAttribute = cryptoPeriodElement.Attribute("keyUriTemplate");

							var location = keyUriTemplateAttribute.Value.IndexOf("kid=");
							var keyGuid = keyUriTemplateAttribute.Value.Substring(location + 4);

							if (key.Length == 0)
							{
								key = await KeyLoader.LoadKey(new Uri(keyUriTemplateAttribute.Value),keyGuid);
								iv = StringToByteArray(ivAtrribute.Value.Substring(2));
							}
						}


						protectionElement.Remove();
					}

					Stream output = new MemoryStream();
					xmlDocument.Save(output);
					output.Position = 0;

					// Return an input stream of the modified xml.
					return new DownloaderResponse(pDownloaderRequest.RequestUri, output.AsInputStream(), (ulong)output.Length, webResponseManifest.ContentType, null, false);
				}
			}

			if (key.Length == 0)
			{
				// load the key from the known URL and parse the known IV from a string.  This will eventually be taken from the manifest file instead.
				//key = await LoadKey();
				//iv = StringToByteArray("123456789abcdef0123456789abcdef0"); // IV
			}

			/*
			 * A workaround is currently needed to truncate the encrypted stream to the length of the unecrypted stream.
			 * For some reason, the encrypted stream has 1-6 extra bytes on each response that corrupt the ability to play the video.
			 * Truncating the encrypted stream to the length of the clear stream resolves this problem, but it requires an extra server call.
			 */
			//Uri clearUri = new Uri(pDownloaderRequest.RequestUri.AbsoluteUri.Replace("encrypted", "clear") + "?test=test");

			WebRequest request = WebRequest.Create(pDownloaderRequest.RequestUri);
			//WebRequest requestClear = WebRequest.Create(clearUri);

			using (WebResponse webResponse = await request.GetResponseAsync())
			//using (WebResponse webResponseClear = await requestClear.GetResponseAsync())
			{
				var responseStream = webResponse.GetResponseStream();

				/*
				 * Read the encrypted response into a byte array for decrption
				 */
				byte[] responseBytes = ReadToEnd(responseStream);

				/*
				 * Perform the decryption
				 */
				byte[] decryptedBytes;
				this.Decrypt(responseBytes, out decryptedBytes);

				/*
				 * Write out the decrypted bytes to a MemoryStream so it can be returned as a DownloaderResponse.
				 * Note, the constructor MemoryStream(byte[] bytes) cannot be used here since that prevents the GetBuffer method from being usable.
				 * The player framework uses GetBuffer to read from the input stream.
				 */
				MemoryStream ms = new MemoryStream();
				ms.Write(decryptedBytes, 0, (int)decryptedBytes.Length);
				ms.Position = 0;

				response = new DownloaderResponse(pDownloaderRequest.RequestUri, ms.AsInputStream(), (ulong) ms.Length, webResponse.ContentType, null, false);
			}
			
			return response;
		}


		public static byte[] ReadToEnd(Stream stream)
		{
			long originalPosition = 0;

			if (stream.CanSeek)
			{
				originalPosition = stream.Position;
				stream.Position = 0;
			}

			try
			{
				byte[] readBuffer = new byte[4096];

				int totalBytesRead = 0;
				int bytesRead;

				while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
				{
					totalBytesRead += bytesRead;

					if (totalBytesRead == readBuffer.Length)
					{
						int nextByte = stream.ReadByte();
						if (nextByte != -1)
						{
							byte[] temp = new byte[readBuffer.Length * 2];
							System.Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
							System.Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
							readBuffer = temp;
							totalBytesRead++;
						}
					}
				}

				byte[] buffer = readBuffer;
				if (readBuffer.Length != totalBytesRead)
				{
					buffer = new byte[totalBytesRead];
					System.Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
				}
				return buffer;
			}
			finally
			{
				if (stream.CanSeek)
				{
					stream.Position = originalPosition;
				}
			}
		}

		private static byte[] StringToByteArray(String hex)
		{
			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			return bytes;
		}

		public void ResponseData(DownloaderRequest pDownloaderRequest, DownloaderResponse pDownloaderResponse)
		{
			// nothing to save here
		}

		public void Decrypt(byte[] cipher, out byte[] decryptedBytes)
		{
			var provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);

			var cryptKey = provider.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(key));

			var cryptIv = CryptographicBuffer.CreateFromByteArray(iv);

			var cipherBuffer = CryptographicBuffer.CreateFromByteArray(cipher);

			var decryptedBuffer = CryptographicEngine.Decrypt(cryptKey, cipherBuffer, cryptIv);

			CryptographicBuffer.CopyToByteArray(decryptedBuffer, out decryptedBytes);
		}
	}
}

