
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The audio track has been changed to a different language
    /// </summary>
    public class AudioTrackChangedLog : VideoEventLog
    {
        public AudioTrackChangedLog(string Language)
            : base(VideoLogTypes.AudioTrackSelect)
        {
            this.Language = Language;
        }

        /// <summary>
        /// The language chosen.
        /// </summary>
        public string Language
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.Language);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.Language, value);
            }
        }

    }
}
