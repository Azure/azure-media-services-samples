using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using Microsoft.SilverlightMediaFramework.Core.Resources;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Offline
{
    public class OfflineStorageManager
    {
        private const long DefaultBufferSize = 10000;

        public long BufferSize { get; set; }

        public event Action<OfflineStorageManager, string , long, long> OfflineStorageProgressChanged;
        public event Action<OfflineStorageManager, string , long> OfflineStorageCompleted;

        public OfflineStorageManager()
        {
            BufferSize = DefaultBufferSize;
        }

        public static bool OfflineFileExists(string fileName)
        {
            var userStore = IsolatedStorageFile.GetUserStoreForApplication();
            return userStore.FileExists(fileName);
        }

        public void StoreResource(Stream inputStream, string offlineFilename)
        {
            int readTotal = 0;
            var userStore = IsolatedStorageFile.GetUserStoreForApplication();

            if (EnsureSpaceAvailable(inputStream.Length))
            {
                var offlineFile = userStore.CreateFile(offlineFilename);

                using (offlineFile)
                {
                    long bufferSize = Math.Min(BufferSize, inputStream.Length);
                    var buffer = new byte[bufferSize];
                    int readSize;

                    while ((readSize = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        offlineFile.Write(buffer, 0, readSize);
                        readTotal += readSize;

                        OfflineStorageProgressChanged.IfNotNull(i => i(this, offlineFilename, readTotal, inputStream.Length));
                    }

                    offlineFile.Close();
                }
            }
            else
            {
                string message =
                    string.Format(
                        SilverlightMediaFrameworkResources.InsufficientQuotaSpaceToStoreResourcesErrorMessage,
                        inputStream.Length);
                throw new OfflineException(message);
            }

            OfflineStorageCompleted.IfNotNull(i => i(this, offlineFilename, readTotal));
        }

        public static bool EnsureSpaceAvailable(long spaceRequired)
        {
            IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForApplication();
            long additionalQuotaRequired = spaceRequired - (userStore.Quota - userStore.UsedSize);

            return additionalQuotaRequired <= 0 || userStore.IncreaseQuotaTo(userStore.Quota + additionalQuotaRequired);
        }

        public void SerializeItemToFile<TItem>(TItem item, string filename)
        {   
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(TItem));
                serializer.Serialize(memoryStream, item);
                memoryStream.Position = 0;
                StoreResource(memoryStream, filename);
                memoryStream.Close();
            }
        }

        public static TItem ReadSerializedItemFromFile<TItem>(string filename)
        {
            TItem result;
            var userStore = IsolatedStorageFile.GetUserStoreForApplication();

            using (var file = userStore.OpenFile(filename, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(TItem));
                result = (TItem)serializer.Deserialize(file);
                file.Close();
            }

            return result;
        }

        
    }
}
