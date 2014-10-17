using System;
using Microsoft.SilverlightMediaFramework.Logging;
using System.Xml;
using System.IO;
using Microsoft.SilverlightMediaFramework.Samples.RemoteDiagnostics.ServiceReference1;

namespace Microsoft.SilverlightMediaFramework.Samples.RemoteDiagnostics
{
    public class MyBatchAgent : IBatchAgent
    {
        LoggingServiceClient svc;

        public MyBatchAgent()
        {
            svc = new LoggingServiceClient();
            svc.LogBatchCompleted += new EventHandler<ServiceReference1.LogBatchCompletedEventArgs>(svc_LogBatchCompleted);
        }

        void svc_LogBatchCompleted(object sender, ServiceReference1.LogBatchCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                // success!
                var result = new LogBatchResult()
                {
                    IsEnabled = true,
                    QueuePollingInterval = null,
                    ServerTime = e.Result
                };

                if (LogBatchCompleted != null)
                    LogBatchCompleted(this, new Microsoft.SilverlightMediaFramework.Logging.LogBatchCompletedEventArgs(result, e.UserState as Batch));
            }
            else
            {
                // error
                if (LogBatchCompleted != null)
                    LogBatchCompleted(this, new Microsoft.SilverlightMediaFramework.Logging.LogBatchCompletedEventArgs(e.Error, e.UserState as Batch));
            }
        }

        public bool LogBatchAsync(Batch batch)
        {
            using (Stream stream = new MemoryStream())
            {
                batch.Serialize(XmlWriter.Create(stream));
                stream.Position = 0;
                svc.LogBatchAsync(new StreamReader(stream).ReadToEnd(), batch);
            }
            return true;
        }

        public event EventHandler<Microsoft.SilverlightMediaFramework.Logging.LogBatchCompletedEventArgs> LogBatchCompleted;
    }
}
