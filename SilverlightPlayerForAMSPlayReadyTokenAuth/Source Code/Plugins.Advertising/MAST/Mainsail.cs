using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// The MAST Engine
    /// </summary>
    public class Mainsail : IDisposable
    {
        #region Other Members
        readonly IMastAdapter mastInterface;
        /// <summary>
        /// A reference to the player. Trigger conditions are based on player properties and events.
        /// </summary>
        public IMastAdapter MastInterface { get { return mastInterface; } }

        /// <summary>
        /// The conditions for a trigger were met. The trigger is now active.
        /// </summary>
        public event EventHandler<TriggerEventArgs> ActivateTrigger;

        /// <summary>
        /// The conditions to deactivate a trigger were met. The trigger is no longer active.
        /// </summary>
        public event EventHandler<TriggerEventArgs> DeactivateTrigger;

        /// <summary>
        /// The MAST document as been successfully downloaded and parsed.
        /// </summary>
        public event EventHandler MastDocLoaded;

        /// <summary>
        /// The MAST document failed to download or parse.
        /// </summary>
        public event EventHandler<MastFailureEventArgs> MastDocFailed;

        /// <summary>
        /// Evaluating the conditions for a trigger caused an error.
        /// </summary>
        public event EventHandler<TriggerFailureEventArgs> TriggerEvaluationFailed;

        /// <summary>
        /// The list of triggers we're monitoring
        /// </summary>
        private List<TriggerManager> Triggers = new List<TriggerManager>();

        /// <summary>
        /// Timer to allow us to check property conditions periodically
        /// </summary>
        private Timer timer;

        private TimeSpan pollingFrequency = TimeSpan.Zero;
        /// <summary>
        /// Check frequency of the timer
        /// </summary>
        public TimeSpan PollingFrequency
        {
            get { return pollingFrequency; }
            set
            {
                if (pollingFrequency != value)
                {
                    pollingFrequency = value;
                    //use this as an opportunity to update the timer
                    if (timer != null)
                    {
                        if (value > TimeSpan.Zero)
                        {
                            timer.Change(value, value);
                        }
                        else
                        {
                            timer.Change(Timeout.Infinite, Timeout.Infinite);
                        }
                    }
                }
            }
        }

        private void StartTimer()
        {
            if (timer != null) timer.Dispose();
            timer = new Timer(new TimerCallback(OnTimer), null, TimeSpan.Zero, pollingFrequency);
        }

        public bool UseDispatcherThread { get; set; }
        public bool UseTimelineChanged { get; set; }

        #endregion

        public Mainsail(IMastAdapter MastInterface)
        {
            UseDispatcherThread = true;
            UseTimelineChanged = true;

            mastInterface = MastInterface;
            mastInterface.TimelineChanged += mastInterface_TimelineChanged;
            PollingFrequency = TimeSpan.FromMilliseconds(500);
        }

        void mastInterface_TimelineChanged(object sender, EventArgs e)
        {
            if (UseTimelineChanged)
            {
                foreach (TriggerManager tm in Triggers)
                {
                    Evaluate(tm);
                }
            }
        }

        private void OnTimer(object state)
        {
            if (!UseTimelineChanged)
            {
                //avoid stacking up threads here
                if (Monitor.TryEnter(timer))
                {
                    try
                    {
                        foreach (TriggerManager tm in Triggers)
                        {
                            if (UseDispatcherThread)
                            {
                                EvaluateAsync(tm);
                            }
                            else
                            {
                                Evaluate(tm);
                            }
                        }
                    }
                    finally
                    {
                        Monitor.Exit(timer);
                    }
                }
            }
        }
        
        private void EvaluateAsync(TriggerManager tm)
        {
            AsyncHelper.UI(delegate { Evaluate(tm); }, Deployment.Current.Dispatcher, true, ReenteranceMode.Bypass);
        }

        private void Evaluate(TriggerManager tm)
        {
            try
            {
                //If it succeeds, it will take care of itself and fire event if ready
                tm.Evaluate();
            }
            catch (Exception ex)
            {
                TriggerEvaluationFailed(this, new TriggerFailureEventArgs(tm.Trigger, ex));
            }
        }

        private void tm_Activate(object sender, EventArgs e)
        {
            OnTriggerActivate(sender as TriggerManager);
        }

        private readonly List<TriggerManager> ActiveTriggers = new List<TriggerManager>();
        protected void OnTriggerActivate(TriggerManager tm)
        {
            ActiveTriggers.Add(tm);

            // Note: this should always be on the main thread if UseDispatcherThread == true
            //a trigger is letting us know it's ready, pass it on.
            if (ActivateTrigger != null)
                ActivateTrigger(this, new TriggerEventArgs(tm.Trigger));
        }

        private void tm_Deactivate(object sender, EventArgs e)
        {
            // this can come from a bg thread
            TriggerManager tm = sender as TriggerManager;
            if (UseDispatcherThread)
            {
                AsyncHelper.UI(delegate { OnTriggerDeactivate(tm); }, Deployment.Current.Dispatcher, true, ReenteranceMode.Bypass);
            }
            else
            {
                OnTriggerDeactivate(tm);
            }
        }

        internal void Deactivate(Trigger t)
        {
            var tm = ActiveTriggers.FirstOrDefault(at => at.Trigger == t);
            if (tm != null)
            {
                OnTriggerDeactivate(tm);
            }
        }

        /// <summary>
        /// Used to force the deactivation of a trigger
        /// </summary>
        /// <param name="t">The trigger that is to be deactivated</param>
        protected void OnTriggerDeactivate(TriggerManager tm)
        {
            ActiveTriggers.Remove(tm);

            if (DeactivateTrigger != null)
                DeactivateTrigger(this, new TriggerEventArgs(tm.Trigger));
        }

        #region Add/remove MAST docs and triggers

        public void AddMastDoc(Uri mastUri)
        {
            if (mastUri == null) throw new NullReferenceException("Mast URI cannot be null");
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(MastDownloadCompleted);
            wc.DownloadStringAsync(mastUri, mastUri);
        }

        private void MastDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    AddMastDoc(e.Result);
                    if (MastDocLoaded != null)
                        MastDocLoaded(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    if (MastDocFailed != null)
                        MastDocFailed(this, new MastFailureEventArgs((Uri)e.UserState, ex));
                }
            }
            else
            {
                if (MastDocFailed != null)
                    MastDocFailed(this, new MastFailureEventArgs((Uri)e.UserState, e.Error));
            }
        }

        public void AddMastDoc(string xml)
        {
            if (xml == null)
            {
                throw new NullReferenceException("Mast doc cannot be null");
            }

            MAST mast;
            Exception ex;
            if (MAST.Deserialize(xml, out mast, out ex) && mast != null)
            {
                AddMastDoc(mast);
                return;
            }

            if (ex != null)
            {
                throw new Exception("Failed to deserialize Mast doc.", ex);
            }
            throw new Exception("Unknown error deserializing doc");
        }

        public void AddMastDoc(MAST mast)
        {
            if (mast == null || mast.triggers == null)
            {
                throw new NullReferenceException("Mast doc/triggers cannot be null");
            }

            foreach (Trigger t in mast.triggers)
            {
                AddMastTrigger(t);
            }

            // start the timer if it isn't already, this will also kick off a timer tick immediately
            if (timer == null && !UseTimelineChanged)
            {
                StartTimer();
            }
        }

        private void AddMastTrigger(Trigger t)
        {
            var oldtm = Triggers.FirstOrDefault(trig => trig.Trigger.Id == t.Id);

            if (oldtm != null)
            {
                if (ActiveTriggers.Contains(oldtm))
                {
                    // trigger with the same ID already exists and is currently active (in which case we don't want to update it or it could re-evaluate).
                    return;
                }
                else
                {
                    // trigger with the same ID already exists, replace it
                    Triggers.Remove(oldtm);
                    UnHookTrigger(oldtm);
                }
            }

            // add the new trigger
            TriggerManager tm = new TriggerManager(t, MastInterface);
            Triggers.Add(tm);
            HookUpTrigger(tm);
        }

        private void RemoveTrigger(TriggerManager tm)
        {
            lock (Triggers)
            {
                if (Triggers.Contains(tm))
                {
                    Triggers.Remove(tm);
                    UnHookTrigger(tm);
                }
            }
        }

        protected void HookUpTrigger(TriggerManager tm)
        {
            tm.Activate += new EventHandler(tm_Activate);
            tm.Deactivate += new EventHandler(tm_Deactivate);
        }

        protected void UnHookTrigger(TriggerManager tm)
        {
            tm.Activate -= new EventHandler(tm_Activate);
            tm.Deactivate -= new EventHandler(tm_Deactivate);
            tm.Dispose();
        }

        #endregion

        private void Clear()
        {
            foreach (TriggerManager t in Triggers.ToArray())
            {
                RemoveTrigger(t);
            }
        }

        public void Dispose()
        {
            if (mastInterface != null) mastInterface.TimelineChanged -= mastInterface_TimelineChanged;
            if (timer != null)
            {
                Monitor.Enter(timer);
                try
                {
                    Clear();
                    timer.Dispose();
                }
                finally
                {
                    Monitor.Exit(timer);
                }
            }
        }
    }
}
