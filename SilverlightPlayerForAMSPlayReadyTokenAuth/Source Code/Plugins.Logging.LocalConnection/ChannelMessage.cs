using System;
using System.Windows.Messaging;

namespace Microsoft.Logging.LocalConnection
{
    public class MyMessageReceivedEventArgs : EventArgs
    {
        public MyMessageReceivedEventArgs(MessageReceivedEventArgs e, string message)
        {
            this.NameScope = e.NameScope;
            this.ReceiverName = e.ReceiverName;
            this.Response = e.Response;
            this.SenderDomain = e.SenderDomain;
            this.Message = message;
        }

        public string Message { get; private set; }
        public ReceiverNameScope NameScope { get; private set; }
        public string ReceiverName { get; private set; }
        public string Response { get; set; }
        public string SenderDomain { get; private set; }
    }
}
