using System;
using System.Linq;
using System.Windows.Threading;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public abstract class RetryManager : IDisposable
    {
        private const int DefaultMaximumConcurrentRequests = 1;
        private const int DefaultMaximumRetryAttempts = 3;
        private const long DefaultTimeoutMillis = 5000;
        private const long DefaultTimeoutPollingMillis = 1000;

        private readonly object _threadSync;
        private readonly DispatcherTimer _timeoutMonitor;
        private readonly List<RetryQueueRequest> _activeRequests;

        public RetryManager()
        {
            Timeout = TimeSpan.FromMilliseconds(DefaultTimeoutMillis);
            MaximumRetryAttempts = DefaultMaximumRetryAttempts;
            MaximumConcurrentRequests = DefaultMaximumConcurrentRequests;

            _threadSync = new object();
            _activeRequests = new List<RetryQueueRequest>();
            _timeoutMonitor = new DispatcherTimer();
            _timeoutMonitor.Interval = TimeSpan.FromMilliseconds(DefaultTimeoutPollingMillis);
            _timeoutMonitor.Tick += TimeoutMonitor_Tick;
        }

        public TimeSpan Timeout { get; protected set; }
        public int MaximumRetryAttempts { get; protected set; }
        public int MaximumConcurrentRequests { get; protected set; }
        public abstract bool HasPendingRequests { get; }

        public IEnumerable<RetryQueueRequest> ActiveRequests
        {
            get { return _activeRequests.ToList(); }
        }

        //public void AddRequest(RetryQueueRequest request)
        //{
        //    _requestQueue.Enqueue(request);
        //    AttemptToStartRequests();
        //}

        protected abstract RetryQueueRequest NextRequest();
        protected abstract void BeginRequest(RetryQueueRequest request);
        protected virtual void OnRetryingRequest(RetryQueueRequest request) { }
        protected virtual void OnRequestExceededMaximumRetryAttempts(RetryQueueRequest request) { }
        protected virtual void OnRequestCompleted(RetryQueueRequest request) { }
        protected virtual void CancelRequest(RetryQueueRequest request) { }

        public virtual void Cancel()
        {
            lock (_threadSync)
            {
                _activeRequests.ForEach(CancelRequest);
            }
        }

        protected void NotifyRequestAdded()
        {
            AttemptToStartRequests();
        }

        protected void NotifyRequestSuccessful(RetryQueueRequest request)
        {
            lock (_threadSync)
            {
                if (_activeRequests.Contains(request))
                {
                    _activeRequests.Remove(request);
                }
            }

            OnRequestCompleted(request);
            AttemptToStartRequests();
        }

        private void AttemptToStartRequests()
        {
            lock (_threadSync)
            {
                while (HasPendingRequests && _activeRequests.Count < MaximumConcurrentRequests)
                {
                    RetryQueueRequest nextItem = NextRequest();
                    if (nextItem != null)
                    {
                        nextItem.LastAttemptStarted = DateTime.Now;
                        _activeRequests.Add(nextItem);

                        BeginRequest(nextItem);

                        if (!_timeoutMonitor.IsEnabled)
                        {
                            _timeoutMonitor.Start();
                        }
                    }
                    else
                    {
                        if (!_timeoutMonitor.IsEnabled)
                        {
                            _timeoutMonitor.Start();
                        } 
                        break;
                    }
                }
            }
        }

        private void TimeoutMonitor_Tick(object sender, EventArgs e)
        {
            var failedRequests = new List<RetryQueueRequest>();
            var retryingRequests = new List<RetryQueueRequest>();

            lock (_threadSync)
            {
                if (_activeRequests.Any())
                {
                    var timedOutRequests = _activeRequests.Where(i => DateTime.Now.Subtract(i.LastAttemptStarted) > Timeout).ToList();

                    foreach (RetryQueueRequest request in timedOutRequests)
                    {
                        if (request.RetryAttempts < MaximumRetryAttempts)
                        {
                            retryingRequests.Add(request);
                            request.RetryAttempts++;

                            BeginRequest(request);
                        }
                        else
                        {
                            _activeRequests.Remove(request);
                            failedRequests.Add(request);
                        }
                    }
                }
                else
                {
                    AttemptToStartRequests();
                    _timeoutMonitor.Stop();
                }
            }

            if (failedRequests.Any())
            {
                AttemptToStartRequests();
            }

            retryingRequests.ForEach(OnRetryingRequest);
            failedRequests.ForEach(OnRequestExceededMaximumRetryAttempts);
        }


        public virtual void Dispose()
        {
            _timeoutMonitor.Tick -= TimeoutMonitor_Tick;
            if (_timeoutMonitor.IsEnabled) _timeoutMonitor.Stop();
        }
    }

    public class RetryQueueRequest
    {
        public DateTime LastAttemptStarted { get; set; }
        public int RetryAttempts { get; set; }
    }
}
