using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs;
using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring
{
    /// <summary>
    /// Provides a BatchingLogAgent base class that filters log types typically not sent to the server
    /// </summary>
    public abstract class RemoteVideoLogAgent : BatchingLogAgent
    {
        public RemoteVideoLogAgent(BatchingConfig Config)
            : base(Config)
        { }

        public RemoteVideoLogAgent(Uri configUri, IBatchAgent batchAgent)
            : base(configUri, batchAgent)
        { }

        /// <summary>
        /// A filter that uses the configuration if available or the default remote logging filter if not.
        /// This default filter will filter out trace logs and video quality snapshots.
        /// </summary>
        public override ILogFilter Filter
        {
            get
            {
                return base.Filter ?? DefaultRemoteLoggingFilter;
            }
        }

        static DefaultRemoteLogFilter defaultRemoteLoggingFilter;
        protected static DefaultRemoteLogFilter DefaultRemoteLoggingFilter
        {
            get
            {
                if (defaultRemoteLoggingFilter == null)
                    defaultRemoteLoggingFilter = new DefaultRemoteLogFilter();
                return defaultRemoteLoggingFilter;
            }
        }

        /// <summary>
        /// The default remote logging filter. Excludes TraceLog and VideoQualitySnapshotLog (which are intended for local, real-time measurements only).
        /// </summary>
        public class DefaultRemoteLogFilter : ILogFilter
        {
            internal DefaultRemoteLogFilter() { }

            public bool IncludeLog(LogBase Log)
            {
                return !(Log is TraceLog || Log is VideoQualitySnapshotLog);
            }
        }
    }

}
