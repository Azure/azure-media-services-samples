using System;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.SilverlightMediaFramework.Utilities;

namespace Microsoft.SilverlightMediaFramework.Core.Data
{
    // loads / saves settings to isolated storage
    internal class PlayerIdDataClient : ObservableObject
    {
        // name of storage file
        private const string StorageFileName = "PlayerId.txt";
        private static Guid _playerIdGuid = Guid.Empty;

        public static string PlayerId
        {
            get
            {
                if (_playerIdGuid == Guid.Empty)
                {
                    if (DesignerProperties.IsInDesignTool)
                    {
                        _playerIdGuid = Guid.NewGuid();
                    }
                    else
                    {
                        bool ret = Load();
                        if (ret == false)
                        {
                            _playerIdGuid = Guid.NewGuid();
                            Save();
                        }
                    }
                }
                return _playerIdGuid.ToString();
            }
        }

        public static bool Save()
        {
            try
            {
#if !WINDOWS_PHONE
                if (IsolatedStorageFile.IsEnabled)
                {
#endif
                    IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication();
                    using (var storageStream = new IsolatedStorageFileStream(
                        StorageFileName, FileMode.Create, storageFile))
                    {
                        using (var writer = new StreamWriter(storageStream))
                        {
                            // write settings to isolated storage
                            writer.WriteLine(_playerIdGuid.ToString());
                            writer.Flush();
                        }
                    }

                    return true;
#if !WINDOWS_PHONE
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

        public static bool Load()
        {
            try
            {
#if !WINDOWS_PHONE
                if (IsolatedStorageFile.IsEnabled)
                {
#endif
                    IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForApplication();
                    if (storageFile.FileExists(StorageFileName))
                    {
                        using (var storageStream = new IsolatedStorageFileStream(
                            StorageFileName, FileMode.Open, storageFile))
                        {
                            using (var reader = new StreamReader(storageStream))
                            {
                                // read settings from isolated storage
                                string guid = reader.ReadLine();
                                _playerIdGuid = new Guid(guid);
                            }
                        }

                        return true;
                    }
                    else return false;
#if !WINDOWS_PHONE
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