namespace Microsoft.SilverlightMediaFramework.Plugins.Metadata
{
    /// <summary>
    /// Is used internally for querying and filtering available ILogWriter plugins.
    /// </summary>
    public interface ILogWriterMetadata : IPluginMetadata
    {
        /// <summary>
        /// The Id of this LogWriter, used by the Player to load specific LogWriters.
        /// </summary>
        string LogWriterId { get; }
    }
}