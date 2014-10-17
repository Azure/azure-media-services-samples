using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Keeps track of an active Ad and allows it to be deactivated or to notify when it has been deactivated.
    /// </summary>
    public class AdSequencingPayload : IAdPayload
    {
        /// <summary>
        /// Creates a new AdSequencingPayload
        /// </summary>
        /// <param name="Trigger">The trigger causing this payload to be created</param>
        internal AdSequencingPayload(IAdSequencingTrigger Trigger)
        {
            adQueue = new Queue<IAdSequencingSource>(Trigger.Sources);
        }

        private readonly Queue<IAdSequencingSource> adQueue;
        /// <summary>
        /// The Queue of ad sequencing sources associated with this ad.
        /// </summary>
        internal Queue<IAdSequencingSource> AdQueue { get { return adQueue; } }

        /// <summary>
        /// Indicates that the ActiveSource has changed
        /// </summary>
        internal event EventHandler ActiveSourceChanged;

        private AdSequencingSourcePayload activeSource;
        /// <summary>
        /// Gets or Sets the ActiveSource. This is currently playing ad source and should no longer be in the queue.
        /// </summary>
        internal AdSequencingSourcePayload ActiveSource
        {
            get { return activeSource; }
            set
            {
                if (activeSource != null)
                {
                    activeSource.Deactivated -= new EventHandler(AdSourcePayload_Deactivated);
                }

                activeSource = value;

                if (activeSource != null)
                {
                    activeSource.Deactivated += new EventHandler(AdSourcePayload_Deactivated);
                }

                if (ActiveSourceChanged != null) ActiveSourceChanged(this, EventArgs.Empty);
                if (activeSource == null && !adQueue.Any())
                {
                    if (Deactivated != null) Deactivated(this, EventArgs.Empty);
                }
            }
        }

        private void AdSourcePayload_Deactivated(object sender, EventArgs e)
        {
            if (sender == ActiveSource) ActiveSource = null;
        }

        /// <summary>
        /// Deactivates the payload
        /// </summary>
        public void Deactivate()
        {
            adQueue.Clear();
            ActiveSource.Deactivate();
        }

        /// <summary>
        /// Provides notification that the payload has been deactivated
        /// </summary>
        public event EventHandler Deactivated;

        /// <summary>
        /// Indicates that we were at the live position when the ad began
        /// </summary>
        public bool WasAtLivePosition { get; set; }
    }

    /// <summary>
    /// Contains the payload for a single handled ad source
    /// </summary>
    internal class AdSequencingSourcePayload
    {
        private readonly List<AdSequencingSourcePayload> Children = new List<AdSequencingSourcePayload>();
        private readonly IAdPayload adSourcePayload;

        /// <summary>
        /// Creates a new payload for a single ad source.
        /// </summary>
        /// <param name="AdSourcePayload">The payload that came from the ad handler</param>
        public AdSequencingSourcePayload(IAdPayload AdSourcePayload)
        {
            adSourcePayload = AdSourcePayload;
            adSourcePayload.Deactivated += new EventHandler(AdSourcePayload_Deactivated);
        }

        /// <summary>
        /// Gets the payload that came from the ad handler
        /// </summary>
        public IAdPayload AdSourcePayload { get { return adSourcePayload; } }

        /// <summary>
        /// Notifies when the ad has been deactivated
        /// </summary>
        public event EventHandler Deactivated;

        /// <summary>
        /// Forces the ad to be deactivated
        /// </summary>
        public void Deactivate()
        {
            adSourcePayload.Deactivate();
        }

        private void AdSourcePayload_Deactivated(object sender, EventArgs e)
        {
            adSourcePayload.Deactivated -= new EventHandler(AdSourcePayload_Deactivated);
            foreach (var child in Children)
            {
                child.AdSourcePayload.Deactivate();
            }

            if (Deactivated != null) Deactivated(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adds a child payload assumed to be playing simultaneously with the parent. This is good for companion ads.
        /// </summary>
        /// <param name="Child">The child payload whose life cycle is dependent on the partent.</param>
        public void AddChild(AdSequencingSourcePayload Child)
        {
            Child.AdSourcePayload.Deactivated += new EventHandler(Child_Deactivated);
            Children.Add(Child);
        }

        private void Child_Deactivated(object sender, EventArgs e)
        {
            var child = sender as AdSequencingSourcePayload;
            child.AdSourcePayload.Deactivated -= new EventHandler(Child_Deactivated);
            child.Children.Remove(child);
        }
    }
}
