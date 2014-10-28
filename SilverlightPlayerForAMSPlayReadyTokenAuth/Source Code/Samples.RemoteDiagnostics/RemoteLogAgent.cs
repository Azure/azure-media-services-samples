using System;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Logging;
using Microsoft.SilverlightMediaFramework.Plugins.Monitoring;

namespace Microsoft.SilverlightMediaFramework.Samples.RemoteDiagnostics
{
    [Export(typeof(ILogAgent))]
    public class RemoteLogAgent : RemoteVideoLogAgent
    {
        public RemoteLogAgent() : base(GetConfig()) { }

        static BatchingConfig GetConfig()
        {
            var result = BatchingConfig.Load(new Uri("LoggingConfiguration.xml", UriKind.Relative));
            result.BatchAgent = new MyBatchAgent();
            return result;
        }
    }
}
