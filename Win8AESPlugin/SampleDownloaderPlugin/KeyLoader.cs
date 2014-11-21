namespace CloudVideoClient.Xaml.Crypto
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Runtime.Serialization;
	using System.Runtime.Serialization.Json;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
    using Windows.Data.Json;
	
	[DataContract]
	public class KeyIdData
	{
		[DataMember]
		public string KeyId { get; set; }
	}

	public sealed class KeyLoader
	{
		private static ManualResetEvent allDone = new ManualResetEvent(false);

		public static async Task<byte[]> LoadKey(Uri keyUri, string keyId)
		{
            //var keyIdData = new KeyIdData()
            //{
            //    KeyId = keyId,
            //};

            string authToken = "PUT_IN_YOUR_TOKEN_HERE";
           
            byte[] response = GetDeliveryKey(keyUri, authToken).Result;

			return response;
		}

		private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

			// End the operation
			request.EndGetRequestStream(asynchronousResult);
			allDone.Set();
		}


		private static async Task<byte[]> GetDeliveryKey(Uri keyDeliveryUri, string token)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(keyDeliveryUri);

			request.Method = "POST";
			request.ContentType = "text/xml";
			if (!string.IsNullOrEmpty(token))
			{
				request.Headers["Authorization"] = token;
			}

			request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
			allDone.WaitOne();
		

			var response = await request.GetResponseAsync();
			return GetDeliveryKey(response);
		}

		private static byte[] GetDeliveryKey(WebResponse response)
		{
			var stream = response.GetResponseStream();
			if (stream == null)
			{
				throw new NullReferenceException("Response stream is null");
			}

			var buffer = new byte[256];
			var length = 0;
			while (stream.CanRead && length <= buffer.Length)
			{
				var nexByte = stream.ReadByte();
				if (nexByte == -1)
				{
					break;
				}
				buffer[length] = (byte)nexByte;
				length++;
			}
			//response.Close();

			var key = new byte[length];
			Array.Copy(buffer, key, length);
			return key;
		}
	}
}