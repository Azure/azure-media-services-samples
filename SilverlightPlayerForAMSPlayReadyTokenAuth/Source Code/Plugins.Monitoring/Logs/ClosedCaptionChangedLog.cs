
namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The closed captioning has either been turned on or off. A different log type ID will be set depending on which.
    /// </summary>
    public class ClosedCaptionChangedLog : SimpleVideoEventLog
    {
        public ClosedCaptionChangedLog(bool visible)
            : base(visible ? VideoLogTypes.ClosedCaptionOn : VideoLogTypes.ClosedCaptionOff)
        { }

        /// <summary>
        /// Indicates whether or not closed captioning is visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return base.Type == VideoLogTypes.ClosedCaptionOn;
            }
        }
    }
}
