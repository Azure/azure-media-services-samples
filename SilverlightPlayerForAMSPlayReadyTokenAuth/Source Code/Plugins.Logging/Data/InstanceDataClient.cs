using System;
using System.IO.IsolatedStorage;

namespace Microsoft.SilverlightMediaFramework.Logging.Data
{
    /// <summary>
    /// Loads and saves settings to isolated storage
    /// </summary>
    internal static class InstanceDataClient
    {
        // name of storage file
        const string StorageKey = "InstanceId";
        const string StorageFileName = "InstanceId.txt";
        static Guid? instanceIdGuid;

        /// <summary>
        /// A unique GUID persisted and retrieved from isolated storage. Useful for grouping clients across multiple sessions.
        /// </summary>
        static public Guid InstanceId
        {
            get
            {
                if (!instanceIdGuid.HasValue)
                {
                    bool ret = LoadId();
                    if (!ret)
                    {
                        instanceIdGuid = Guid.NewGuid();
                        SaveId();
                    }
                }
                return instanceIdGuid.Value;
            }
        }

        static bool SaveId()
        {
            try
            {
#if !OOB
                using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream(StorageFileName, System.IO.FileMode.Create, storageFile))
                    {
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(storageStream);
                        // write settings to isolated storage
                        writer.WriteLine(instanceIdGuid.Value.ToString());
                        writer.Flush();
                    }

                    return true;
                }
#else
                if (IsolatedStorageSettings.ApplicationSettings.Contains(StorageKey))
                {
                    IsolatedStorageSettings.ApplicationSettings[StorageKey] = instanceIdGuid.Value.ToString();
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings.Add(StorageKey, instanceIdGuid.Value.ToString());
                }
                return true;
#endif
            }
            catch
            {
                // users can disable isolated storage
                return false;
            }
        }

        static bool LoadId()
        {
            try
            {
#if !OOB
                using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (storageFile.FileExists(StorageFileName))
                    {
                        using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream(StorageFileName, System.IO.FileMode.Open, storageFile))
                        {
                            System.IO.StreamReader reader = new System.IO.StreamReader(storageStream);
                            // read settings from isolated storage
                            string guid = reader.ReadLine();
                            instanceIdGuid = new Guid(guid);
                        }

                        return true;
                    }
                    else
                        return false;
                }
#else
                if (IsolatedStorageSettings.ApplicationSettings.Contains(StorageKey))
                {
                    var result = IsolatedStorageSettings.ApplicationSettings[StorageKey] as string;
                    instanceIdGuid = new Guid(result);
                    return true;
                }
                else return false;
#endif
            }
            catch
            {
                // users can disable isolated storage
                return false;
            }
        }
    }
}
