// Copyright (c) 2008 CodeToast.com and Nicholas Brookins
//This code is free to use in any application for any use if this notice is left intact.
//Just don't sue me if it gets you fired.  Enjoy!

using System;
using System.Collections.Generic;
using System.Windows;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers
{
    /// <summary>
    /// Static class containing async helper methods
    /// </summary>
    internal static class AsyncHelper
    {
        static Dictionary<string, object> methodLocks = new Dictionary<string, object>();
        static Dictionary<object, TimerEx> timers = new Dictionary<object, TimerEx>();

        #region Async 'Dof overloads, for ease of use
        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed. 
        /// This overload always tries the ThreadPool and DOES NOT check for reentrance.
        /// </summary>
        /// <param name="d">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return: Async.Do((Func&lt;object&gt;)MyMethod);</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, etc.</returns>
        public static AsyncRes Do(Func<object> d, bool getRetVal)
        {
            return Do(d, getRetVal, ReenteranceMode.Allow);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed. 
        /// This overload always tries the ThreadPool and DOES NOT check for reentrance.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod)</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, etc.</returns>
        public static AsyncRes Do(Action d)
        {
            return Do(d, ReenteranceMode.Allow);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return: Async.Do((Func&lt;object&gt;)MyMethod);</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, resturn and result values, etc.</returns>
        public static AsyncRes Do(Func<object> d, bool getRetVal, ReenteranceMode rMode)
        {
            return Do(d, null, getRetVal, null, true, rMode, true, null);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod);</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes Do(Action d, ReenteranceMode rMode)
        {
            return Do(null, d, false, null, true, rMode, true, null);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return: Async.Do((Func&lt;object&gt;)MyMethod);</param>
        /// <param name="state">A user object that can be tracked through the returned result</param>
        /// <param name="tryThreadPool">True to use the TP, otherwise just go to a ful lthread - good for long running tasks.</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, resturn and result values, etc.</returns>
        public static AsyncRes Do(Func<object> d, bool getRetVal, object state, bool tryThreadPool, ReenteranceMode rMode)
        {
            return Do(d, null, getRetVal, state, tryThreadPool, rMode, true, null);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod);</param>
        /// <param name="state">A user object that can be tracked through the returned result</param>
        /// <param name="tryThreadPool">True to use the TP, otherwise just go to a ful lthread - good for long running tasks.</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes Do(Action d, object state, bool tryThreadPool, ReenteranceMode rMode)
        {
            return Do(null, d, false, state, tryThreadPool, rMode, true, null);
        }
        #endregion

        #region The Big Main private 'Dof method - called by all overloads.
        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate.</param>
        /// <param name="dr">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return.</param>
        /// <param name="state">A user object that can be tracked through the returned result</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <param name="tryThreadPool">True to use the TP, otherwise just go to a ful lthread - good for long running tasks.</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        private static AsyncRes Do(Func<object> dr, Action d, bool getRetVal, object state, bool tryThreadPool, ReenteranceMode rMode, bool isAsync, Dispatcher dispatcher)
        {
            //get a generic MethodInfo for checks..
            MethodInfo mi = ((dr != null) ? dr.Method : d.Method);
            //make a unique key for output usage
            string key = string.Format("{0}{1}{2}{3}", ((getRetVal) ? "<-" : ""), mi.DeclaringType, ((mi.IsStatic) ? ":" : "."), mi.Name);
            //our custom return value, holds our delegate, state, key, etc.
            AsyncRes res = new AsyncRes(state, ((dr != null) ? (Delegate)dr : (Delegate)d), key, rMode);

            //Create a delegate wrapper for what we will actually invoke..
            Action Action = (Action)delegate
            {
                if (!BeforeInvoke(res)) return; //checks for reentrance issues and sets us up
                try
                {
                    if (res.IsCompleted) return;
                    if (dr != null)
                    {
                        res.retVal = dr();//use this one if theres a return
                    }
                    else
                    {
                        d();//otherwise the simpler Action
                    }
                }
                catch (Exception ex)
                { //we never want a rogue exception on a random thread, it can't bubble up anywhere
                    System.Diagnostics.Debug.WriteLine("Async Exception:" + ex);
                }
                finally
                {
                    FinishInvoke(res);//this will fire our callback if they used it, and clean up
                }
            };

            if (dispatcher != null)
            {
                res.control = dispatcher;
                res.result = AsyncAction.ControlInvoked;
                if (!isAsync)
                {
                    if (dispatcher.CheckAccess())
                    {
                        res.completedSynchronously = true;
                        Action();
                    }
                    else
                    {
                        dispatcher.BeginInvoke(Action);
                    }
                }
                else
                {
                    dispatcher.BeginInvoke(Action);
                }
                return res;
            } //don't catch these errors - if this fails, we shouldn't try a real thread or threadpool!

            if (tryThreadPool)
            {
                //we are going to use the .NET threadpool
                try
                {
                    //this is what actually fires this task off..
                    bool result = ThreadPool.QueueUserWorkItem(delegate { Action(); });
                    if (result)
                    {
                        res.result = AsyncAction.ThreadPool;
                        //this means success in queueing and running the item
                        return res;
                    }
                    else
                    {
                        //according to docs, this "won't ever happen" - exception instead, but just for kicks.
                        System.Diagnostics.Debug.WriteLine("Failed to queue in threadpool. Method: " + key);
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to queue in threadpool: " + ex.Message, "Method: " + key);
                }
            }

            //if we got this far, then something up there failed, or they wanted a dedicated thread
            Thread t = new Thread((ThreadStart)delegate { Action(); }) { IsBackground = true, Name = ("Async_" + key) };
            res.result = AsyncAction.Thread;
            t.Start();

            return res;
        }
        #endregion

        #region UI Overloads
        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod);</param>
        /// <param name="async">Whether to run async, or try on current thread if invoke is not required.</param>
        /// <param name="dispatcher">A dispatcher to Invoke upon GUI thread of, if needed. Null if unused.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes UI(Action d, Dispatcher dispatcher, bool isAsync)
        {
            return Do(null, d, false, null, false, ReenteranceMode.Allow, isAsync, dispatcher);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod);</param>
        /// <param name="async">Whether to run async, or try on current thread if invoke is not required.</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <param name="dispatcher">A dispatcher to Invoke upon GUI thread of, if needed. Null if unused.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes UI(Action d, Dispatcher dispatcher, bool isAsync, ReenteranceMode rMode)
        {
            return Do(null, d, false, null, false, rMode, isAsync, dispatcher);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return: Async.Do((Func&lt;object&gt;)MyMethod);</param>
        /// <param name="async">Whether to run async, or try on current thread if invoke is not required.</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <param name="dispatcher">A dispatcher to Invoke upon GUI thread of, if needed. Null if unused.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes UI(Func<object> d, bool getRetVal, Dispatcher dispatcher, bool isAsync)
        {
            return Do(d, null, getRetVal, null, false, ReenteranceMode.Allow, isAsync, dispatcher);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A void delegate - can be cast to (Action) from an anonymous delgate or method:  Async.Do((Action)MyVoidMethod);</param>
        /// <param name="state">A user object that can be tracked through the returned result</param>
        /// <param name="async">Whether to run async, or try on current thread if invoke is not required.</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <param name="dispatcher">A dispatcher to Invoke upon GUI thread of, if needed. Null if unused.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes UI(Action d, Dispatcher dispatcher, object state, bool isAsync, ReenteranceMode rMode)
        {
            return Do(null, d, false, state, false, rMode, isAsync, dispatcher);
        }

        /// <summary>
        /// Fires off your delegate asyncronously, using the threadpool or a full managed thread if needed.
        /// </summary>
        /// <param name="d">A delegate with a return value of some sort - can be cast to (Func&lt;object&gt;) from an anonymous delgate with a return: Async.Do((Func&lt;object&gt;)MyMethod);</param>
        /// <param name="state">A user object that can be tracked through the returned result</param>
        /// <param name="async">Whether to run async, or try on current thread if invoke is not required.</param>
        /// <param name="getRetVal">If true, and the method/delgete returns something, it is included in the AsyncRes returned (after the method completes)</param>
        /// <param name="rMode">If true, will make sure no other instances are running your method.</param>
        /// <param name="dispatcher">A dispatcher to Invoke upon GUI thread of, if needed. Null if unused.</param>
        /// <returns>AsyncRes with all kind of goodies for waiting, result values, etc.</returns>
        public static AsyncRes UI(Func<object> d, bool getRetVal, Dispatcher dispatcher, object state, bool isAsync, ReenteranceMode rMode)
        {
            return Do(d, null, getRetVal, state, false, rMode, isAsync, dispatcher);
        }
        #endregion

        #region Before and after - helper methods

        private static bool BeforeInvoke(AsyncRes res)
        {
            //if marked as completed then we abort.
            if (res.IsCompleted) return false;
            //if mode is 'allow' there is nothing to check.  Otherwise...
            if (res.RMode != ReenteranceMode.Allow)
            {
                //be threadsafe with our one and only member field
                lock (methodLocks)
                {
                    if (!methodLocks.ContainsKey(res.Method))
                    {
                        //make sure we have a generic locking object in the collection, it will already be there if we are reentering
                        methodLocks.Add(res.Method, new object());
                    }
                    //if bypass mode and we can't get or lock, we dump out.
                    if (res.RMode == ReenteranceMode.Bypass)
                    {
                        if (!Monitor.TryEnter(methodLocks[res.Method]))
                        {
                            res.result = AsyncAction.Reenterant;
                            return false;
                        }
                    }
                    else
                    {
                        //Otherwise in 'stack' mode, we just wait until someone else releases it...
                        Monitor.Enter(methodLocks[res.Method]);
                    }

                    //if we are here, all is good.  
                    //Set some properties on the result class to show when we started, and what thread we are on
                    res.isStarted = true;
                    res.startTime = DateTime.Now;
                    res.thread = Thread.CurrentThread;
                }
            }

            return true;
        }

        private static void FinishInvoke(AsyncRes res)
        {
            if (res == null) return;
            try
            {
                //finish a few more properties
                res.isCompleted = true;
                res.completeTime = DateTime.Now;
                //set the resetevent, in case someone is using the waithandle to know when we have completed.
                res.mre.Set();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting wait handle on " + (res.Method ?? "NULL") + ex);
            }

            if (res.RMode != ReenteranceMode.Allow)
            {
                //if mode is bypass or stack, then we must have a lock that needs releasing
                try
                {
                    if (res.Method != null)
                    {
                        if (methodLocks.ContainsKey(res.Method))
                        {
                            Monitor.Exit(methodLocks[res.Method]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error releasing reentrant lock on " + (res.Method ?? "NULL") + ex);
                }
            }
        }

        #endregion

        #region Timers
        public static void SetTimer(Action d, object key, ReenteranceMode rMode, TimeSpan initial, TimeSpan frequency)
        {
            TimerEx t = null;
            lock (timers)
            {
                if (timers.ContainsKey(key))
                {
                    t = timers[key];
                    if (d == null)
                    {
                        t.Close();
                        timers.Remove(key);
                    }
                    else
                    {
                        t.d = d;
                        t.Timer.Change(initial, frequency);
                        t.Mode = rMode;
                    }
                }
                else
                {
                    if (d != null)
                    {
                        t = new TimerEx(d, rMode, initial, frequency);
                        timers.Add(key, t);
                    }
                }
            }
        }

        public static void StopTimer(object key)
        {
            SetTimer(null, key, ReenteranceMode.Bypass, TimeSpan.Zero, TimeSpan.Zero);
        }

        private class TimerEx
        {
            public System.Threading.Timer Timer = null;
            public ReenteranceMode Mode = ReenteranceMode.Bypass;
            public Action d = null;
            private readonly object lck = new object();

            public TimerEx(Action d, ReenteranceMode rMode, TimeSpan initial, TimeSpan frequency)
            {
                this.d = d;
                this.Mode = rMode;
                Timer = new System.Threading.Timer(TimerMethod, null, initial, frequency);
            }

            public void Close()
            {
                d = null;
                if (Timer != null) Timer.Dispose();
            }

            private void TimerMethod(object o)
            {
                if (d == null)
                {
                    Console.WriteLine("Empty Delegate, disposing timer.");
                    if (Timer != null) Timer.Dispose();
                    return;
                }
                ReenteranceMode m = Mode;
                if (m == ReenteranceMode.Bypass)
                {
                    if (!Monitor.TryEnter(lck)) return;
                }
                else if (m == ReenteranceMode.Stack)
                {
                    Monitor.Enter(lck);
                }
                try
                {
                    //Do stuff
                    d();

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Async Timer Exception:" + ex);
                }
                finally
                {
                    if (m != ReenteranceMode.Allow)
                    {
                        Monitor.Exit(lck);
                    }
                }
            }
        }

        #endregion
    }

    #region AsyncRes class
    /// <summary>
    /// Used with the Async helper class, This class is mostly a holder for a lot of tracking fields and properties, with a few things mandated by the IAsyncResult interface.
    /// </summary>
    internal class AsyncRes : IAsyncResult
    {

        internal AsyncRes(object state, Delegate d, string key, ReenteranceMode rMode)
        {
            this.state = state;
            this.asyncDelegate = d;
            this.key = key;
            this.RMode = rMode;
        }

        internal ReenteranceMode RMode = ReenteranceMode.Allow;

        internal Thread thread = null;

        private string key = null;
        public string Method { get { return key; } }

        private Delegate asyncDelegate = null;
        public Delegate AsyncDelegate { get { return asyncDelegate; } }

        internal AsyncAction result = AsyncAction.Unknown;
        public AsyncAction Result { get { return result; } }

        internal Dispatcher control = null;
        public Dispatcher Control { get { return control; } }

        internal DateTime createTime = DateTime.Now;
        public DateTime TimeCreated { get { return createTime; } }

        internal DateTime completeTime = DateTime.MinValue;
        public DateTime TimeCompleted { get { return completeTime; } }

        internal DateTime startTime = DateTime.Now;
        public DateTime TimeStarted { get { return startTime; } }

        public TimeSpan TimeElapsed
        {
            get { return ((completeTime > DateTime.MinValue) ? completeTime : DateTime.Now) - createTime; }
        }

        public TimeSpan TimeRunning
        {
            get { return (startTime == DateTime.MinValue) ? TimeSpan.Zero : ((completeTime > DateTime.MinValue) ? completeTime : DateTime.Now) - startTime; }
        }

        internal object retVal = null;
        public object ReturnValue { get { return retVal; } }

        internal bool isStarted = false;
        public bool IsStarted { get { return isStarted; } }

        private object state = null;
        public object AsyncState { get { return state; } }
        
        internal ManualResetEvent mre = new ManualResetEvent(false);
        public WaitHandle AsyncWaitHandle { get { return mre; } }

        internal bool completedSynchronously = false;
        public bool CompletedSynchronously { get { return completedSynchronously; } }

        internal bool isCompleted = false;
        public bool IsCompleted { get { return isCompleted; } }
    }
    #endregion

    #region Definitions of enums and delegates

    internal enum AsyncAction
    {
        Unknown = 0,
        ThreadPool = 1,
        Thread = 2,
        Failed = 4,
        Reenterant = 8,
        ControlInvoked = 16
    }

    internal enum ReenteranceMode
    {
        Allow = 1,
        Bypass = 2,
        Stack = 4,
    }
    #endregion

    internal class StopWatch
    {
        private Dictionary<object, long> starts = new Dictionary<object, long>();
        private Dictionary<object, long> lastCheck = new Dictionary<object, long>();
        private Dictionary<object, DateTime> list = new Dictionary<object, DateTime>();

        private static StopWatch sw = new StopWatch();

        public static void Start_(object key) { sw.Start(key); }

        public void Start() { Start(this); }

        public void Start(object key)
        {
            starts.Combine(key, DateTime.Now.Ticks);
        }

        public static TimeSpan Check_(object key) { return sw.Check(key, false); }
        public static TimeSpan Check_(object key, bool fromStart) { return sw.Check(key, fromStart); }

        public TimeSpan Check() { return Check(this); }
        public TimeSpan Check(object key) { return Check(key, false); }

        public TimeSpan Check(object key, bool fromStart)
        {
            if (!starts.ContainsKey(key)) return TimeSpan.Zero;

            long n = DateTime.Now.Ticks;// TimeTools.GetTime();
            long s = lastCheck.ContainsKey(key) && !fromStart ? lastCheck[key] : starts[key];

            lastCheck.Combine(key, n);

            return (TimeSpan.FromTicks(s) - TimeSpan.FromTicks(n)).Duration();
            //TimeSpan.FromMilliseconds(TimeTools.TimeInterval(s, n));
        }

        public static TimeSpan Stop_() { return sw.Stop(sw); }
        public static TimeSpan Stop_(object key) { return sw.Stop(key); }

        public TimeSpan Stop() { return Stop(this); }

        public TimeSpan Stop(object key)
        {
            TimeSpan t = Check(key, true);
            lastCheck.Remove(key);
            starts.Remove(key);
            return t;
        }


        public static string PrintMs_(object key) { return sw.PrintMs(key); }

        public string PrintMs() { return PrintMs(this); }

        public string PrintMs(object key)
        {
            TimeSpan t = Check(key, false);
            return t.TotalMilliseconds + "ms";
        }


        public static bool Throttle_(object key, int seconds) { return sw.Throttle(key, seconds); }
        public static bool Throttle_(object key, TimeSpan period) { return sw.Throttle(key, period); }

        public bool Throttle(int seconds) { return Throttle(this, seconds); }
        public bool Throttle(TimeSpan period) { return Throttle(this, period); }
        public bool Throttle(object key, int seconds) { return Throttle(key, TimeSpan.FromSeconds(seconds)); }

        public bool Throttle(object key, TimeSpan period)
        {
            if (!list.ContainsKey(key))
            {
                try
                {
                    list.Add(key, DateTime.Now);
                }
                catch { }
                return true;
            }
            if (DateTime.Now - list[key] > period)
            {
                list[key] = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
