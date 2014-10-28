using System;
using System.IO;
using Microsoft.Xna.Framework.Storage;

namespace Microsoft.SilverlightMediaFramework.Core.Data
{
    // loads / saves settings to isolated storage
    internal class PlayerIdDataClient
    {
        // name of storage file
        private static Guid _playerIdGuid = Guid.Empty;

        const string FileName = "PlayerId.dat";
        const string ContainerName = "MMPPF";

        private StorageDevice storageDevice;

        public PlayerIdDataClient()
        {
            storageDevice = new StorageDevice();
        }

        public void LoadPlayerIdAsync()
        {
            var ar = storageDevice.BeginOpenContainer(ContainerName, FileName, PersistedStorageCallbackLoad, null);
        }

        public event Action<PlayerIdDataClient, Guid> LoadPlayerIdCompleted;

        private void PersistedStorageCallbackLoad(IAsyncResult result)
        {
            Guid playerId = Guid.Empty;
            bool success = false;
            using (StorageContainer container = storageDevice.EndOpenContainer(result))
            {
                try
                {
                    if (container.FileExists(FileName))
                    {
                        using (var reader = container.OpenFile(FileName, FileMode.Open))
                        {
                            byte[] buffer = new byte[reader.Length];
                            reader.Read(buffer, 0, (int)reader.Length);
                            playerId = new Guid(buffer);
                            success = true;
                        }
                    }
                }
                catch { /* swallow errors */ }

                if (!success)
                {
                    playerId = Guid.NewGuid();

                    try
                    {
                        using (var writer = container.CreateFile(FileName))
                        {
                            var bytes = playerId.ToByteArray();
                            writer.Write(bytes, 0, bytes.Length);
                        }
                    }
                    catch { /* swallow errors */ }
                }
            }

            _playerIdGuid = playerId;
            if (LoadPlayerIdCompleted != null) System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => LoadPlayerIdCompleted(this, playerId));
        }
    }
}