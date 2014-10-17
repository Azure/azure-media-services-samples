using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Microsoft.HealthMonitor.Data
{
    internal class SavedListDataClient
    {
        public static void Save(IList<string> sources, string StorageFileName)
        {
            try
            {
                IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication();
                using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream(
                    StorageFileName, System.IO.FileMode.Create, storageFile))
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(storageStream))
                    {
                        foreach (string source in sources)
                        {
                            writer.WriteLine(source);
                        }
                    }
                }
            }
            catch
            {
                // users can disable isolated storage
            }
        }

        public static IList<string> Load(string StorageFileName)
        {
            List<string> result = new List<string>();
            try
            {
                IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication();
                if (storageFile.FileExists(StorageFileName))
                {
                    using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream(
                        StorageFileName, System.IO.FileMode.Open, storageFile))
                    {
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(storageStream))
                        {
                            // read settings from isolated storage
                            while (reader.EndOfStream == false)
                            {
                                result.Add(reader.ReadLine());
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

    }
}
