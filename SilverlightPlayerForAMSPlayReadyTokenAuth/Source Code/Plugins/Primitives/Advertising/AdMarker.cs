using System.Windows.Browser;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    /// <summary>
    /// Represents an advertisment marker to be handled by an IAdPayloadHandlerPlugin.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The position of a AdMarker represents its specific time location in a media clip.
    /// </para>
    /// </remarks>
    [ScriptableType]
    public class AdMarker : MediaMarker
    {
        public AdMarker()
        {
            Type = "ad";
        }

        /// <summary>
        /// Gets or sets a value indicating whether MediaMarker.Begin should be ignored and the marker should be triggered immediately.
        /// </summary>
        public bool Immediate { get; set; }


        /// <summary>
        /// Gets or sets the ScheduledAd object associated with this AdMarker. This contains required information about the Ad Trigger and Payload produced once the AdMarker is handled.
        /// </summary>
        public ScheduledAd ScheduledAd
        {
            get { return base.Content as ScheduledAd; }
            set { base.Content = value; }
        }
    }
}