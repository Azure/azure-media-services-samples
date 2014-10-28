using System.Windows.Browser;

namespace Microsoft.SilverlightMediaFramework.Utilities.Metadata
{
    public class MetadataCollection : ScriptableObservableCollection<MetadataItem>
    {
		[ScriptableMember]
        public void Add(string name, string value)
        {
            var metadataItem = new MetadataItem
            {
                Key = name,
                Value = value
            };

            base.Add(metadataItem);
        }
    }
}