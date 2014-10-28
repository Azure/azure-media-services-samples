//#define LATENCYTEST

using System;
using System.Net;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Utilities.Data
{
    /// <summary>
    /// Sends requests for data (i.e. XML files) to allow objects and collections
    /// to be periodically synchronized with the latest data defined on the server.
    /// </summary>
    public class PollingRequest : IDisposable
    {
        private readonly object _downloadLock = new object();
        private readonly object _pollingLock = new object();
        private readonly DispatcherTimer _timer;
        private WebClient _webClient;

        public PollingRequest()
            : this(null)
        {
        }

        /// <summary>
        /// Setup a non-reoccurring data request.
        /// The StartPolling method must be called to begin the request.
        /// </summary>
        /// <param name="source"></param>
        public PollingRequest(Uri source)
            : this(source, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// Setup a reoccurring data client.
        /// The StartPolling method must be called to begin the request.
        /// </summary>
        public PollingRequest(Uri source, TimeSpan pollingInterval)
        {
            Source = source;

            _timer = new DispatcherTimer
            {
                Interval = pollingInterval,
            };
            _timer.Tick += (s, e) => BeginRequest();
        }

        #region Methods

        /// <summary>
        /// Starts processing metadata requests.
        /// </summary>
        public void StartPolling()
        {
#if LATENCYTEST
            IsDownloading = true;
            var latencySimulatorTimer = new DispatcherTimer();
            latencySimulatorTimer.Interval = TimeSpan.FromSeconds(3);
            latencySimulatorTimer.Tick += new EventHandler(latencySimulatorTimer_Tick);
            latencySimulatorTimer.Start();
#else
            BeginRequest();
#endif

            lock (_pollingLock)
            {
                if (!_timer.IsEnabled && PollingInterval != TimeSpan.Zero)
                {
                    _timer.Start();
                }
            }
        }

#if LATENCYTEST
        void latencySimulatorTimer_Tick(object sender, EventArgs e)
        {
            IsDownloading = false;
            var latencySimulatorTimer = sender as DispatcherTimer;
            latencySimulatorTimer.Stop();
            latencySimulatorTimer.Tick -= latencySimulatorTimer_Tick;
            BeginRequest();
        }
#endif

        /// <summary>
        /// Stops processing requests.
        /// </summary>
        public void StopPolling()
        {
            lock (_pollingLock)
            {
                if (_timer.IsEnabled)
                {
                    _timer.Stop();
                }
            }
        }

        protected void BeginRequest()
        {
            lock (_downloadLock)
            {
                //Don't spin up more than 1 download at a time.
                if (!IsDownloading && Source != null)
                {
                    Uri downloadUri = Source.IsAbsoluteUri
                                          ? CacheBust(Source)
                                          : Source;

                    if (_webClient == null)
                    {
                        _webClient = new WebClient();
                        _webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                        _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    }

                    _webClient.DownloadStringAsync(downloadUri);
                    IsDownloading = true;
                }
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (RequestProgressChanged != null)
            {
                RequestProgressChanged(this, e.ProgressPercentage);
            }
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            IsDownloading = false;

            if (e.Error == null)
            {
                if (!e.Cancelled)
                {
                    OnRequestCompleted(e.Result);
                    RequestCompleted.IfNotNull(i => i(this, e.Result));
                }
                else 
                {
                    RequestCanceled.IfNotNull(i => i(this));
                }
            }
            else
            {
                OnRequestFailed(e.Error);
                RequestFailed.IfNotNull(i => i(this, e.Error));
            }
        }

        protected virtual void OnRequestFailed(Exception error)
        {
        }

        protected virtual void OnRequestCompleted(string result)
        {
        }

        private static Uri CacheBust(Uri uri)
        {
            return uri.IsAbsoluteUri || string.IsNullOrEmpty(uri.Query)
                       ? new Uri(string.Format("{0}?ignore={1}", uri, Guid.NewGuid()))
                       : new Uri(string.Format("{0}&ignore={1}", uri, Guid.NewGuid()));
        }

        #endregion

        public Uri Source { get; set; }
        public bool IsDownloading { get; private set; }

        public TimeSpan PollingInterval
        {
            get { return _timer.Interval; }
            set
            {
                if (value == TimeSpan.Zero)
                {
                    StopPolling();
                }

                _timer.Interval = value;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            StopPolling();
            if (_webClient != null && _webClient.IsBusy)
            {
                _webClient.DownloadStringCompleted -= WebClient_DownloadStringCompleted;
                _webClient.DownloadProgressChanged -= WebClient_DownloadProgressChanged;
                _webClient.CancelAsync();
                _webClient = null;
            }
        }

        #endregion

        public event Action<PollingRequest> RequestCanceled;
        public event Action<PollingRequest, string> RequestCompleted;
        public event Action<PollingRequest, Exception> RequestFailed;
        public event Action<PollingRequest, Double> RequestProgressChanged;
    }
}