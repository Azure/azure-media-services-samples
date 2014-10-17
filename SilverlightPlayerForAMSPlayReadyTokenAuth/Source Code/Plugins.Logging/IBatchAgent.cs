using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// The interface that must be implemented in order to receive batched logs
    /// </summary>
    public interface IBatchAgent
    {
        event EventHandler<LogBatchCompletedEventArgs> LogBatchCompleted;

        /// <summary>
        /// Called when a batch of logs are ready to be logged.
        /// </summary>
        /// <param name="batch">The batch to be logged</param>
        /// <returns>A bool indicating whether an async request is in progress. Only return false if there was an immediate failure.</returns>
        bool LogBatchAsync(Batch batch);
    }

    /// <summary>
    /// EventArg used to indicate the status of the request.
    /// </summary>
    public class LogBatchCompletedEventArgs : EventArgs
    {
        public LogBatchCompletedEventArgs(LogBatchResult Result, Batch Batch)
        {
            this.Batch = Batch;
            this.Result = Result;
        }

        public LogBatchCompletedEventArgs(Exception Error, Batch Batch)
        {
            this.Batch = Batch;
            this.Error = Error;
        }

        /// <summary>
        /// Set to null if the async operation was successful.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// The result from a successful async batch log operation.
        /// </summary>
        public LogBatchResult Result { get; private set; }

        /// <summary>
        /// The original batch that was logged.
        /// </summary>
        public Batch Batch { get; set; }
    }
}
