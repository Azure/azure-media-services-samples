using System;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Logging;

namespace Microsoft.Logging.LocalConnection
{
    [Export(typeof(ILogAgent))]
    public class LogAgent : ILogAgent
    {
        LocalConnectionMessageService svc;
        string channelName = null;

        public LogAgent() { }

        [ImportingConstructor]
        public LogAgent([Import("LocalChannelName", AllowDefault = true)] string ChannelName) 
        {
            channelName = ChannelName;
        }

        public void Log(Log log)
        {
            svc.SendMessage(log);
        }

        public void BroadcastException(Exception exception)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(exception.ToString());
#endif
        }

        bool isSessionStarted;
        public bool IsSessionStarted
        {
            get { return isSessionStarted; }
        }

        public bool StartSession()
        {
            try
            {
                svc = new LocalConnectionMessageService(true, channelName);
                isSessionStarted = true;
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public void StopSession()
        {
            svc = null;
            isSessionStarted = false;
        }

        public ILogFilter Filter
        {
            get { return null; }
        }
    }
}
