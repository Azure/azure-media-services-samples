using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public abstract class QueuedRetryManager : RetryManager
    {
        private Queue<RetryQueueRequest> _requestQueue;

        public QueuedRetryManager()
        {
            _requestQueue = new Queue<RetryQueueRequest>();
        }

        public override bool HasPendingRequests
        {
            get { return _requestQueue.Count > 0; }
        }

        protected override RetryQueueRequest NextRequest()
        {
            return _requestQueue.Dequeue();
        }

        protected void AddRequest(RetryQueueRequest request)
        {
            _requestQueue.Enqueue(request);
            NotifyRequestAdded();
        }

        public override void Cancel()
        {
            _requestQueue.Clear();
            base.Cancel();
        }
    }
}
