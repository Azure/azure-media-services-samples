using System;
using System.Windows.Messaging;
using System.Windows.Threading;

namespace Microsoft.Logging.LocalConnection
{
	public class ChannelManager : IDisposable
	{
		LocalMessageReceiver _receiver;
		LocalMessageSender _sender; 

		public event EventHandler<SendCompletedEventArgs> SendCompleted;
		public event EventHandler<MyMessageReceivedEventArgs> MessageReceived;
		string incomingMessage;
		bool receivingChunked = false;
		DispatcherTimer timer = new DispatcherTimer();
		bool connected = false;

		public ChannelManager()
		{
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += new EventHandler(timer_Tick);
			timer.Start();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (_sender != null)
			{
				_sender.SendAsync("heartbeat");
			}
			else
			{
				Connected = false;
			}
		}

		public bool Connected
		{
			get
			{
				return connected;
			}
			private set
			{
				connected = value;
			}
		}

		public void CreateChannel(bool isMaster, string channelName)
		{
			if (isMaster)
			{
				_receiver = new LocalMessageReceiver(channelName, ReceiverNameScope.Global, LocalMessageReceiver.AnyDomain);
				_receiver.DisableSenderTrustCheck = true;
				_receiver.MessageReceived += new EventHandler<MessageReceivedEventArgs>(_receiver_MessageReceived);
				_receiver.Listen();
			}
			else
			{
				string guid = Guid.NewGuid().ToString();
				if (_receiver != null)
				{
					guid = _receiver.ReceiverName;
				}
				_sender = new LocalMessageSender(channelName, LocalMessageSender.Global);
				_sender.SendCompleted += new EventHandler<SendCompletedEventArgs>(_sender_SendCompleted);
				_sender.SendAsync(string.Format("createchannel|{0}", guid));
				if (_receiver == null)
				{
					_receiver = new LocalMessageReceiver(guid, ReceiverNameScope.Global, LocalMessageReceiver.AnyDomain);
					_receiver.DisableSenderTrustCheck = true;
					_receiver.MessageReceived += new EventHandler<MessageReceivedEventArgs>(_receiver_MessageReceived);
					_receiver.Listen();
				}
			}
		}

		void _sender_SendCompleted(object sender, SendCompletedEventArgs e)
		{
			if (e.Message == "heartbeat")
			{
				if (e.Error != null)
				{
					Connected = false;
					_sender.SendCompleted -= new EventHandler<SendCompletedEventArgs>(_sender_SendCompleted);
					_sender = null;
				}
				else
				{
					Connected = true;
				}
			}
			if (SendCompleted != null)
			{
				SendCompleted(this, e);
			}
		}

		void _receiver_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			string msg = e.Message;
			if (e.Message == "heartbeat")
			{
				e.Response = "heartbeat";
				return;
			}
			if (e.Message == "beginchunked")
			{
				receivingChunked = true;
				incomingMessage = "";
				return;
			}
			else if (e.Message == "endchunked")
			{
				receivingChunked = false;
				msg = incomingMessage;
			}
			else if (receivingChunked)
			{
				incomingMessage += msg;
				return;
			}
			string[] s = msg.Split('|');
			switch (s[0])
			{
				case "createchannel":
					if (_sender != null)
					{
						_sender.SendCompleted -= new EventHandler<SendCompletedEventArgs>(_sender_SendCompleted);
					}
					_sender = new LocalMessageSender(s[1], LocalMessageSender.Global);
					_sender.SendCompleted += new EventHandler<SendCompletedEventArgs>(_sender_SendCompleted);
					_sender.SendAsync("heartbeat");
					break;
				default:
					if (MessageReceived != null)
					{
						MessageReceived(this, new MyMessageReceivedEventArgs(e, msg));
					}
					break;
			}
		}

		public void SendAsync(string message)
		{
			if (_sender != null)
			{
				if (message.Length > 10000)
				{
					_sender.SendAsync("beginchunked");
					for (int i = 0; i < message.Length; i += 10000)
					{
						if (i + 10000 >= message.Length)
						{
							_sender.SendAsync(message.Substring(i));
						}
						else
						{
							_sender.SendAsync(message.Substring(i, 10000));
						}
					}
					_sender.SendAsync("endchunked");
				}
				else
				{
					_sender.SendAsync(message);
				}
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			timer.Stop();
			timer.Tick -= new EventHandler(timer_Tick);
			if (_sender != null)
			{
				_sender.SendCompleted -= new EventHandler<SendCompletedEventArgs>(_sender_SendCompleted);
				_sender = null;
			}
			if (_receiver != null)
			{
				_receiver.MessageReceived -= new EventHandler<MessageReceivedEventArgs>(_receiver_MessageReceived);
				_receiver = null;
			}
		}

		#endregion
	}
}
