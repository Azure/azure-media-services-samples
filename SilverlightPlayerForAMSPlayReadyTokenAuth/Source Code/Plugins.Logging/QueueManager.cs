using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.SilverlightMediaFramework.Logging.Data;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    internal class QueueManager : IDisposable
    {
        /// <summary>
        /// The states of the queuemanager
        /// </summary>
        public enum QueueManagerStates
        {
            Uninitialized = 0,
            Polling = 1,
            Busy = 2,
            Failed = 3,
        }

        /// <summary>
        /// Batch is being sent to BatchAgent
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSending;

        /// <summary>
        /// Batch was successfully sent via BatchAgent
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSendSuccess;

        /// <summary>
        /// Batch failed to send
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSendFailed;

        private BatchingLogAgent logAgent;
        private IBatchAgent dataClient;

        private int failedSendCount;
        private int sendCount;
        private QueueManagerStates state;
        private Timer pollingTimer;  // timer for polling queue
        private bool IsThrottled = false;
        private IEnumerable<Log> LogsToRetry;
        private int RetryCount = 0;

        private TimeSpan queuePollingInterval;
        public TimeSpan QueuePollingInterval
        {
            get { return queuePollingInterval; }
            set
            {
                if (queuePollingInterval != value)
                {
                    queuePollingInterval = value;
                    if (pollingTimer != null)
                    {
                        // adjust the timer with the polling interval in case it failed
                        pollingTimer.Change(QueuePollingInterval, QueuePollingInterval);
                    }
                }
            }
        }

        public QueueManager(BatchingLogAgent LogAgent)
        {
            try
            {
                logAgent = LogAgent;
                dataClient = logAgent.Configuration.BatchAgent;
                dataClient.LogBatchCompleted += LogBatchCompleted;

                state = QueueManagerStates.Uninitialized;
                QueuePollingInterval = logAgent.Configuration.QueuePollingInterval;

                failedSendCount = 0;
                sendCount = 0;
                state = QueueManagerStates.Polling;
                pollingTimer = new System.Threading.Timer(pollingTimer_Tick, null, TimeSpan.FromSeconds(2), QueuePollingInterval);
            }
            catch
            {
                state = QueueManagerStates.Failed;
                throw;   // re-throw the error
            }
        }

        /// <summary>
        /// The total number of send failures
        /// </summary>
        public int FailedSendCount
        {
            get { return failedSendCount; }
            internal set
            {
                failedSendCount = value;

                if (logAgent.Configuration.MaxSendErrors.HasValue && failedSendCount >= logAgent.Configuration.MaxSendErrors.Value)
                {
                    // we are done forever, shut it down!
                    logAgent.State = BatchingLogAgent.BatchingLogAgentStates.Disabled;
                    state = QueueManagerStates.Failed;
                    QueuePollingInterval = TimeSpan.Zero;
                }
                else if (logAgent.Configuration.MaxSendErrorsThrottled.HasValue && failedSendCount >= logAgent.Configuration.MaxSendErrorsThrottled.Value)
                {
                    if (!IsThrottled)
                    {
                        // we reached a critical threshold, throttle the polling interval
                        IsThrottled = true;    // this is used to ignore the response from the server that would otherwise change this interval
                        QueuePollingInterval = logAgent.Configuration.QueuePollingIntervalThrottled;
                    }
                }
                else if (IsThrottled)
                {
                    // we came back from our lethargic state, start speeding things up again
                    IsThrottled = false;
                    QueuePollingInterval = logAgent.Configuration.QueuePollingInterval;
                }
            }
        }

        /// <summary>
        /// The state of the queue manager
        /// </summary>
        public QueueManagerStates State
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// The total number of batches sent
        /// </summary>
        public int SendCount
        {
            get
            {
                return (sendCount);
            }
            internal set
            {
                sendCount = value;
            }
        }

        void pollingTimer_Tick(object sender)
        {
            lock (this)
            {
                if (state == QueueManagerStates.Polling)
                    state = QueueManagerStates.Busy;
                else
                    return; // ignore call, we're already trying
            }
            if (!ProcessQueue())
            {
                state = QueueManagerStates.Polling;
            }
        }

        private bool ProcessQueue()
        {
            IEnumerable<Log> logs = null;
            try
            {
                if (LogsToRetry != null)
                {
                    // we have logs that need to be resent, try again
                    logs = LogsToRetry;
                    LogsToRetry = null; // this will get reset if there's a problem
                    return ProcessQueue(logs, true);
                }
                else
                {
                    RetryCount = 0;
                    logs = logAgent.PurgeQueue();
                    return ProcessQueue(logs, false);
                }
            }
            catch (Exception ex)
            {
                // errors should be caught in complete callback, if we get here treat as send error
                LogsFailedToSend(ex, logs);
                return false;
            }
        }

        private void LogsFailedToSend(Exception e, IEnumerable<Log> logs)
        {
            try
            {
                FailedSendCount++;
                if (!logAgent.Configuration.MaxRetries.HasValue || RetryCount < logAgent.Configuration.MaxRetries.Value)
                {
                    LogsToRetry = logs;
                    RetryCount++;
                }
                else // we exceeded the max number of retries, time to move on
                {
                    LogsToRetry = null;
                    RetryCount = 0;
                    logAgent.IncrementTotalLogsDropped(logs.Count());
                }
                logAgent.BroadcastException(e);
            }
            finally
            {
                // don't forget to reset this!
                if (state == QueueManagerStates.Busy)
                    state = QueueManagerStates.Polling;
            }
        }

        private bool ProcessQueue(IEnumerable<Log> logs, bool IsAlreadyMapped)
        {
            if (logs != null && logs.Count() > 0)
            {
                Batch batch = new Batch();
                batch.ApplicationName = logAgent.Configuration.ApplicationName;
                batch.ApplicationVersion = logAgent.Configuration.ApplicationVersion;
                batch.ApplicationId = logAgent.Configuration.ApplicationId;
                batch.SessionId = logAgent.SessionId;
                batch.InstanceId = InstanceDataClient.InstanceId;
                batch.BatchId = Guid.NewGuid();
                batch.TimeStamp = logAgent.ServerTimeOffset.HasValue ? DateTimeOffset.Now.Add(logAgent.ServerTimeOffset.Value).Ticks : DateTimeOffset.Now.Ticks;

                batch.TotalFailures = FailedSendCount;
                batch.LogsDropped = logAgent.TotalLogsDropped;
                batch.LogsSent = logAgent.TotalLogsSent;
                batch.Logs = logs;

                Batch mappedBatch;
                if (!IsAlreadyMapped)
                {
                    // apply mappings. If mappings are defined, it will create new instance of the batch and the logs using different keys and dropping come elements.
                    mappedBatch = logAgent.MapBatchAndLogs(batch);
                }
                else
                {
                    // apply mappings but ONLY to the batch. The logs have already been mapped.
                    mappedBatch = logAgent.MapBatch(batch);
                }

                try
                {
                    if (BatchSending != null)
                        BatchSending(this, new BatchEventArgs(mappedBatch));
                }
                catch { /* swollow */ }

                SendCount++;

                // the data client does the work of actually sending the info to the server
                return dataClient.LogBatchAsync(mappedBatch);
            }
            else
                return false;
        }

        void LogBatchCompleted(object sender, LogBatchCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    logAgent.IncrementTotalLogsSent(e.Batch.Logs.Count());
                    logAgent.IncrementTotalBatchesSent(1);

                    if (e.Result != null)
                    {
                        if (e.Result.IsEnabled.HasValue)
                            logAgent.State = e.Result.IsEnabled.Value ? BatchingLogAgent.BatchingLogAgentStates.Enabled : BatchingLogAgent.BatchingLogAgentStates.Disabled;
                        if (e.Result.QueuePollingInterval.HasValue && !IsThrottled)
                            QueuePollingInterval = e.Result.QueuePollingInterval.Value;
                        if (e.Result.ServerTime.HasValue)
                        {
                            if (!logAgent.ServerTimeOffset.HasValue && e.Batch.TimeStamp.HasValue)
                            {
                                logAgent.ServerTimeOffset = TimeSpan.FromTicks(e.Result.ServerTime.Value.Ticks - e.Batch.TimeStamp.Value);
                            }
                        }
                    }

                    // decrement the FailedSendCount
                    FailedSendCount = Math.Max(FailedSendCount - 1, 0);
                    try
                    {
                        if (BatchSendSuccess != null)
                            BatchSendSuccess(this, new BatchEventArgs(e.Batch));
                    }
                    catch { /* swollow */ }
                }
                catch (Exception ex)
                {
                    LogBatchFailed(e.Batch, ex);
                }
                finally
                {
                    // ALWAYS reset this!
                    state = QueueManagerStates.Polling;
                }
            }
            else
            {
                LogBatchFailed(e.Batch, e.Error);
            }
        }

        private void LogBatchFailed(Batch batch, Exception error)
        {
            logAgent.IncrementTotalBatchesFailed(1);
            if (BatchSendFailed != null)
                BatchSendFailed(this, new BatchEventArgs(batch));
            LogsFailedToSend(error, batch.Logs);
        }

        /// <summary>
        /// Cleans up the queue manager
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                pollingTimer.Dispose();
                pollingTimer = null;
            }
            LogsToRetry = null;
            logAgent = null;
            if (dataClient != null)
            {
                dataClient.LogBatchCompleted -= LogBatchCompleted;
            }
            dataClient = null;
        }
    }
}
