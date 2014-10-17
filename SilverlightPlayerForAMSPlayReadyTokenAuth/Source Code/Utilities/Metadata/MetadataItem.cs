namespace Microsoft.SilverlightMediaFramework.Utilities.Metadata
{
    public class MetadataItem
    {
        public MetadataItem()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        private MetadataItem(MetadataItem metadataItem)
        {
            Key = metadataItem.Key;
            Value = metadataItem.Value;
        }

        public string Key { get; set; }
        public object Value { get; set; }

        public MetadataItem Clone()
        {
            return new MetadataItem(this);
        }
    }
}