using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using Microsoft.SilverlightMediaFramework.Logging;

namespace Microsoft.Logging.LocalConnection
{
    public class LocalConnectionMessageService : IDisposable
    {
        ChannelManager channelManager;
        bool isMaster;

        public bool Connected
        {
            get
            {
                if (channelManager != null)
                {
                    return channelManager.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        string channelName;
        public string ChannelName
        {
            get
            {
                return channelName;
            }
        }

        public event EventHandler<LogReceivedEventArgs> MessageReceived;

        public LocalConnectionMessageService(bool IsMaster, string ChannelName)
        {
            channelManager = new ChannelManager();
            isMaster = IsMaster;
            if (!string.IsNullOrEmpty(ChannelName))
            {
                channelName = ChannelName;
            }
            else
            {
                channelName = "{EE361AA7-369C-40b3-9F40-72D6FF0D2B1F}";
            }
            try
            {
                Connect();
                channelManager.MessageReceived += new EventHandler<MyMessageReceivedEventArgs>(channelManager_MessageReceived);
            }
            catch { /* ignore */ }
        }

        public void Connect()
        {
            channelManager.CreateChannel(isMaster, channelName);
        }

        public void SendMessage(string messageType, string message)
        {
            SendMessage(messageType + "|" + message);
        }

        public void SendMessage(Log log)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            MemoryStream memstream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(memstream, settings))
            {
                log.Serialize(writer);
                writer.Flush();
                memstream.Position = 0;

                string data = "LogCreated|" + new StreamReader(memstream).ReadToEnd();
                SendMessage(data);
            }
        }

        public void SendMessage(string message)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
                channelManager.SendAsync(message);
            else
                Deployment.Current.Dispatcher.BeginInvoke(() => channelManager.SendAsync(message));
        }

        void channelManager_MessageReceived(object sender, MyMessageReceivedEventArgs e)
        {
            int idx = e.Message.IndexOf('|');
            if (idx < 0) return;
            string s1 = e.Message.Substring(0, idx);
            string s2 = e.Message.Substring(idx + 1);
            if (s1 == "LogCreated")
            {
                using (XmlReader reader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(s2))))
                {
                    reader.Read();
                    Log log = Log.Deserialize(reader);
                    if (log != null)
                        if (MessageReceived != null)
                            MessageReceived(this, new LogReceivedEventArgs(log));
                }
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (channelManager != null)
            {
                channelManager.MessageReceived -= new EventHandler<MyMessageReceivedEventArgs>(channelManager_MessageReceived);
                channelManager.Dispose();
                channelManager = null;
            }
        }

        #endregion
    }
}
