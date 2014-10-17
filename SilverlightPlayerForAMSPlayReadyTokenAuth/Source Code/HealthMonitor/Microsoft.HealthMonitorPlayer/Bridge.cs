using System;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Core.Javascript;

namespace Microsoft.HealthMonitorPlayer
{
	public class Bridge
	{
        public event EventHandler<ScriptEventArgs<string>> PlayVideo;
        public event EventHandler StopVideo;
		static Bridge instance;

		public static Bridge Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Bridge();
				}
				return instance;
			}
		}

		protected Bridge()
		{
			HtmlPage.RegisterScriptableObject("Bridge", this);
		}

		public void PluginReady()
		{
			HtmlPage.Window.Invoke("onPluginReady");
		}

		[ScriptableMember]
		public void LoadVideo(string source)
		{
			if (PlayVideo != null)
			{
				PlayVideo(this, new ScriptEventArgs<string>(source));
			}
		}

        [ScriptableMember]
        public void UnloadVideo()
        {
            if (StopVideo != null)
            {
                StopVideo(this, EventArgs.Empty);
            }
        }

	}
}
