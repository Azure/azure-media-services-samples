using System.Collections.Generic;

namespace Microsoft.HealthMonitor.Data
{
	public class SavedSourcesDataClient
	{
		static string StorageFileName = "StreamingSources.txt";
        
		static public void Save(IList<string> sources)
		{
            SavedListDataClient.Save(sources, StorageFileName);
        }

		public static IList<string> Load()
		{
            return SavedListDataClient.Load(StorageFileName);
		}
	}
}
