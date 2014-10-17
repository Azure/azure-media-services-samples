using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    /// <summary>
    /// Provides information about a scheduled ad. This class is intended to be assigned to AdMarker.ScheduledAd
    /// </summary>
    public class ScheduledAd
    {
        /// <summary>
        /// Creates a new ScheduledAd.
        /// </summary>
        /// <param name="Trigger">The ad sequencing trigger that defines source and target information for the ad.</param>
        public ScheduledAd(IAdSequencingTrigger Trigger)
        {
            trigger = Trigger;
        }

        readonly IAdSequencingTrigger trigger;
        /// <summary>
        /// The ad sequencing trigger that defines source and target information for the ad.
        /// </summary>
        public IAdSequencingTrigger Trigger { get { return trigger; } }

        /// <summary>
        /// Fired when the payload changes. This indicates that an ad payload handler has handled the ad.
        /// </summary>
        public event EventHandler PayloadChanged;
        protected void OnPayloadChanged()
        {
            if (PayloadChanged != null)
                PayloadChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fired when the ad payload is deactivated.
        /// </summary>
        public event EventHandler Deactivated;

        /// <summary>
        /// Forces the ad to be deactivated.
        /// </summary>
        public void Deactivate()
        {
            if (Payload != null)
                Payload.Deactivate();
            else
                OnDeactivate();
        }

        private void OnDeactivate()
        {
            if (Deactivated != null)
                Deactivated(this, EventArgs.Empty);
        }

        private IAdPayload payload;
        /// <summary>
        /// This is the tie to an active ad.
        /// </summary>
        public IAdPayload Payload
        {
            get { return payload; }
            set
            {
                if (payload != null)
                {
                    payload.Deactivated -= new EventHandler(payload_Deactivated);
                }

                payload = value;

                if (payload != null)
                {
                    payload.Deactivated += new EventHandler(payload_Deactivated);
                }

                OnPayloadChanged();
            }
        }

        private void payload_Deactivated(object sender, EventArgs e)
        {
            Payload = null;
            OnDeactivate();
        }
    }
}
