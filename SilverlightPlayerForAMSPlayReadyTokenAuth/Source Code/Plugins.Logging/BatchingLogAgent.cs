using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// A special kind of LogAgent that is capable of batching logs.
    /// It will queue all logs, polling from that queue on a separate thread, and batch the results.
    /// Ultimately, the logs (wrapped in a Batch object) are passed onto an implementation of IBatchAgent.
    /// </summary>
    public abstract class BatchingLogAgent : ILogAgent, IDisposable
    {
        /// <summary>
        /// The possible states for the log agent.
        /// </summary>
        public enum BatchingLogAgentStates
        {
            Failed = 0,
            Enabled = 1,
            Disabled = 3,
            NotInitialized = 4,
        }

        /// <summary>
        /// Fired when a handled exception occurs
        /// </summary>
        public event EventHandler<BatchingExceptionEventArgs> BatchException;

        /// <summary>
        /// Fired before the batch is sent
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSending;

        /// <summary>
        /// Fired after the batch has been successfully sent
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSendSuccess;

        /// <summary>
        /// Fired after the batch failed to send
        /// </summary>
        public event EventHandler<BatchEventArgs> BatchSendFailed;

        /// <summary>
        /// Fired when more logs have been dropped
        /// </summary>
        public event EventHandler TotalLogsDroppedChanged;

        private BatchingLogAgentStates state;
        private Queue<Log> queue;
        private QueueManager queueManager;
        private Uri DefaultConfigUri = new Uri("LoggingConfiguration.xml", UriKind.Relative);

        protected BatchingLogAgent()
        {
            queue = new Queue<Log>();

            state = BatchingLogAgentStates.NotInitialized;
        }

        public BatchingLogAgent(Uri configUri, IBatchAgent batchAgent)
            : this(configUri, true)
        {
            Configuration.BatchAgent = batchAgent;
        }

        public BatchingLogAgent(Uri configUri)
            : this(configUri, true)
        { }

        public BatchingLogAgent(BatchingConfig Config)
            : this()
        {
            if (Config == null)
                ConfigUri = DefaultConfigUri;
            else
                Configuration = Config;
        }

        private BatchingLogAgent(Uri configUri, bool LoadConfigImmediately)
            : this()
        {
            try
            {
                ConfigUri = configUri;

                // load the config file
                if (LoadConfigImmediately)
                    LoadConfig();
            }
            catch (Exception ex)
            {
                state = BatchingLogAgentStates.Failed;
                BroadcastException(ex);
            }
        }

        /// <summary>
        /// Used for debugging. All ignorable exceptions that occur in the process of logging are sent here.
        /// </summary>
        public void BroadcastException(Exception ex)
        {
            if (BatchException != null) BatchException(this, new BatchingExceptionEventArgs(ex));
        }

        /// <summary>
        /// Indicates that the agent is currently running
        /// </summary>
        public bool IsSessionStarted
        {
            get
            {
                return state == BatchingLogAgentStates.Enabled;
            }
        }

        /// <summary>
        /// Used to spin up the agent.
        /// </summary>
        /// <returns>Indicates success</returns>
        public bool StartSession()
        {
            if (state == BatchingLogAgentStates.Enabled)
                throw new Exception("Session is already started");

            // load the config file
            if (Configuration == null)
                LoadConfig();

            try
            {
                if (Configuration.LoggingEnabled)
                {
                    // initialize new queue manager
                    queueManager = new QueueManager(this);
                    queueManager.BatchSending += queueManager_BatchSending;
                    queueManager.BatchSendSuccess += queueManager_BatchSendSuccess;
                    queueManager.BatchSendFailed += queueManager_BatchSendFailed;

                    // set SessionId & StartTime
                    SessionId = Guid.NewGuid();
                    SessionStartTime = DateTime.Now;

                    // enable logging
                    state = BatchingLogAgentStates.Enabled;
                }
                else
                    state = BatchingLogAgentStates.Disabled;

                return true;
            }
            catch (Exception ex)
            {
                BroadcastException(ex);
                state = BatchingLogAgentStates.Disabled;
                return false;
            }
        }

        /// <summary>
        /// Stops the agent.
        /// </summary>
        public void StopSession()
        {
            if (queueManager != null)
            {
                queueManager.Dispose();
                queueManager = null;
            }
            state = BatchingLogAgentStates.Disabled;
        }

        void queueManager_BatchSendFailed(object sender, BatchEventArgs e)
        {
            if (BatchSendFailed != null)
                BatchSendFailed(this, e);
        }

        void queueManager_BatchSending(object sender, BatchEventArgs e)
        {
            if (BatchSending != null)
                BatchSending(this, e);
        }

        void queueManager_BatchSendSuccess(object sender, BatchEventArgs e)
        {
            if (BatchSendSuccess != null)
                BatchSendSuccess(this, e);
        }

        /// <summary>
        /// The current generated session ID.
        /// </summary>
        public Guid SessionId { get; private set; }

        /// <summary>
        /// The Timestamp of when the session started
        /// </summary>
        public DateTime SessionStartTime { get; private set; }

        /// <summary>
        /// The server time offset (used to correct the local time so the server can get accurate time)
        /// </summary>
        public TimeSpan? ServerTimeOffset { get; internal set; }

        /// <summary>
        /// The current duration of the session
        /// </summary>
        public TimeSpan SessionDuration { get { return DateTime.Now.Subtract(SessionStartTime); } }

        /// <summary>
        /// Adds a log to the queue
        /// </summary>
        /// <param name="log">The new log being logged. This has already passed the filter condition if one was specified in the MappingPolicy.</param>
        public void Log(Log log)
        {
            try
            {
                if (State == BatchingLogAgentStates.Enabled)
                    // check to see if we have reached our max events threshold, if over we disable logging
                    if (Configuration.MaxSessionLogs.HasValue && TotalLogsQueued > Configuration.MaxSessionLogs.Value)
                    {
                        IncrementTotalLogsDropped(1);
                        State = BatchingLogAgentStates.Disabled;
                        return;
                    }
                    // check to see if we already have too many events to log
                    else if (Configuration.MaxQueueLength.HasValue && queue.Count > Configuration.MaxQueueLength.Value)
                    {
                        IncrementTotalLogsDropped(1);
                        return;
                    }
                    else
                    {
                        // set log time
                        if (ServerTimeOffset.HasValue)
                            log.TimeStamp = log.TimeStamp.Value.Add(ServerTimeOffset.Value);
                        log.TimeOffset = SessionDuration;

                        Enqueue(log);
                        IncrementTotalLogsQueued(1);
                    }
                else
                {
                    IncrementTotalLogsDropped(1);
                }
            }
            catch (Exception ex)
            {
                IncrementTotalLogsDropped(1);
                BroadcastException(ex);
            }
        }

        Uri configUri;
        /// <summary>
        /// The Uri of the config file to load once the session starts. If the agent is already running an exception will be thrown.
        /// </summary>
        public Uri ConfigUri
        {
            get { return configUri; }
            set
            {
                if (state == BatchingLogAgentStates.Enabled)
                    throw new Exception("Once a session has started, you cannot load a new configuration file");
                configUri = value;
            }
        }

        /// <summary>
        /// Causes the ConfigUri to be loaded.
        /// </summary>
        public void LoadConfig()
        {
            Configuration = BatchingConfig.Load(ConfigUri);
        }

        /// <summary>
        /// The config object to use. It is not recommended you change this while the session is running.
        /// </summary>
        public BatchingConfig Configuration { get; set; }

        /// <summary>
        /// The state of the agent.
        /// </summary>
        public BatchingLogAgentStates State
        {
            get { return state; }
            internal set { state = value; }
        }

        /// <summary>
        /// The total number of dropped logs
        /// </summary>
        public int TotalLogsDropped { get; private set; }
        private readonly object dropCountLock = new object();
        internal void IncrementTotalLogsDropped(int amount)
        {
            lock (dropCountLock)
            {
                TotalLogsDropped += amount;
            }
            // raise event
            if (TotalLogsDroppedChanged != null)
                TotalLogsDroppedChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// The total number of logs sent
        /// </summary>
        public int TotalLogsSent { get; private set; }
        private readonly object sentCountLock = new object();
        internal void IncrementTotalLogsSent(int amount)
        {
            lock (sentCountLock)
            {
                TotalLogsSent += amount;
            }
        }

        /// <summary>
        /// The total number of logs currently in the queue
        /// </summary>
        public int TotalLogsQueued { get; private set; }
        private readonly object queuedCountLock = new object();
        internal void IncrementTotalLogsQueued(int amount)
        {
            lock (queuedCountLock)
            {
                TotalLogsQueued += amount;
            }
        }

        /// <summary>
        /// The total number of batches sent
        /// </summary>
        public int TotalBatchesSent { get; private set; }
        private readonly object batchesSentCountLock = new object();
        internal void IncrementTotalBatchesSent(int amount)
        {
            lock (batchesSentCountLock)
            {
                TotalBatchesSent += amount;
            }
        }

        /// <summary>
        /// The total number of batches that failed to get sent.
        /// </summary>
        public int TotalBatchesFailed { get; private set; }
        private readonly object batchesFailedCountLock = new object();
        internal void IncrementTotalBatchesFailed(int amount)
        {
            lock (batchesFailedCountLock)
            {
                TotalBatchesFailed += amount;
            }
        }

        private void Enqueue(Log log)
        {
            lock (queue)
            {
                queue.Enqueue(log);
            }
        }

        internal Log[] PurgeQueue()
        {
            List<Log> logsToProcess = new List<Log>();

            lock (queue)
            {
                // if list is not empty then post errors
                while (queue.Count > 0 && (!Configuration.MaxBatchLength.HasValue || logsToProcess.Count < Configuration.MaxBatchLength.Value))
                {
                    // copy queue
                    logsToProcess.Add(queue.Dequeue());
                }
            }

            return logsToProcess.ToArray();
        }

        internal Batch MapBatch(Batch batch)
        {
            if (Configuration.MappingRules == null)
                return batch;
            else
            {
                IDictionary<string, string> data;
                foreach (Exception ex in Configuration.MappingRules.TryMap(batch, out data))
                    BroadcastException(ex);
                return new Batch(data) { Logs = batch.Logs };
            }
        }

        internal Batch MapBatchAndLogs(Batch batch)
        {
            if (Configuration.MappingRules == null)
                return batch;
            else
            {
                IDictionary<string, string> data;
                foreach (Exception ex in Configuration.MappingRules.TryMap(batch, out data))
                    BroadcastException(ex);
                return new Batch(data) { Logs = GetMappedLogs(batch.Logs) };
            }
        }

        private IEnumerable<Log> GetMappedLogs(IEnumerable<Log> logs)
        {
            foreach (Log log in logs)
            {
                IDictionary<string, string> data;
                foreach (Exception ex in Configuration.MappingRules.TryMap(log, out data))
                    BroadcastException(ex);

                yield return new Log(data);
            }
        }

        /// <summary>
        /// The filter used by the LoggingService to know which logs to pass to the .Log function.
        /// </summary>
        public virtual ILogFilter Filter
        {
            get { return Configuration.MappingRules; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (IsSessionStarted)
                    StopSession();
            }
        }

    }
}
