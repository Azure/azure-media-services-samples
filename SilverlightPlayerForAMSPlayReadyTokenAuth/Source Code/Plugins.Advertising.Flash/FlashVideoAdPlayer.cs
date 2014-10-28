using System;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    [ScriptableType]
    public class FlashVideoAdPlayer : Canvas, IVpaid, IDisposable
    {
        PendingInit pendingInit = null;
        bool isReady = false;

        const double FlashPluginWidth = 500;
        const double FlashPluginHeight = 375;

        public FlashVideoAdPlayer(ICreativeSource Source, IAdTarget Target)
        {
            AdTarget = Target;
            AdSource = Source;

            this.SizeChanged += new SizeChangedEventHandler(FlashVideoAdPlayer_SizeChanged);

            StringBuilder script = new StringBuilder();

            script.Append("function flashReady(){");
            script.Append("if (adPlayer) adPlayer.OnReady();");
            script.AppendLine("}");

            script.AppendLine("var adPlayer;");
            script.Append("function setPlayer(player){");
            script.Append("adPlayer = player;");
            script.AppendLine("}");

            script.Append("function VPAIDAdLoaded(){");
            script.Append("adPlayer.OnAdLoaded();");
            script.AppendLine("}");

            script.Append("function VPAIDAdStarted(){");
            script.Append("adPlayer.OnAdStarted();");
            script.AppendLine("}");

            script.Append("function VPAIDAdStopped(){");
            script.Append("adPlayer.OnAdStopped();");
            script.AppendLine("}");

            script.Append("function VPAIDAdVideoComplete(){");
            script.Append("if (adPlayer) adPlayer.OnAdVideoComplete();");
            script.AppendLine("}");

            InjectScript(script.ToString());

            HtmlPage.Window.Invoke("setPlayer", this);
        }

        void InjectScript(string script)
        {
            HtmlElement Script = HtmlPage.Document.CreateElement("script");
            Script.SetAttribute("type", "text/javascript");
            Script.SetProperty("text", script);
            HtmlPage.Document.DocumentElement.AppendChild(Script);
        }

        public IAdTarget AdTarget { get; private set; }
        public ICreativeSource AdSource { get; private set; }

        [ScriptableMember]
        public void OnReady()
        {
            isReady = true;
            if (pendingInit != null)
            {
                FlashHost.SetStyleAttribute("width", pendingInit.width.ToString() + "px");
                FlashHost.SetStyleAttribute("height", pendingInit.height.ToString() + "px");
                FlashPlayer.Invoke("initAd", FlashPluginWidth, FlashPluginHeight, pendingInit.viewMode, pendingInit.desiredBitrate, pendingInit.creativeData, pendingInit.environmentVariables);
                pendingInit = null;
            }
        }

        [ScriptableMember]
        public void OnAdLoaded()
        {
            if (AdLoaded != null) AdLoaded(this, EventArgs.Empty);
        }

        [ScriptableMember]
        public void OnAdStarted()
        {
            if (AdStarted != null) AdStarted(this, EventArgs.Empty);
        }

        [ScriptableMember]
        public void OnAdStopped()
        {
            if (AdStopped != null) AdStopped(this, EventArgs.Empty);
        }

        [ScriptableMember]
        public void OnAdVideoComplete()
        {
            if (AdVideoComplete != null) AdVideoComplete(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            FlashHost.SetStyleAttribute("visibility", "hidden");
        }

        private class PendingInit
        {
            public double width { get; set; }
            public double height { get; set; }
            public string viewMode { get; set; }
            public int desiredBitrate { get; set; }
            public string creativeData { get; set; }
            public string environmentVariables { get; set; }
        }

        Point GetAppPosition()
        {
            double curx = 0;
            double cury = 0;
            var obj = HtmlPage.Plugin;
            while (obj != null)
            {
                curx += (double)obj.GetProperty("offsetLeft");
                cury += (double)obj.GetProperty("offsetLeft");
                obj = obj.GetProperty("offsetParent") as HtmlElement;
            }
            return new Point(curx, cury);
        }

        void PositionFlashPlayer()
        {
            Point appPos = GetAppPosition();
            Point localPos = this.TransformToVisual(Application.Current.RootVisual).Transform(new Point());
            Point position = new Point(appPos.X + localPos.X, appPos.Y + localPos.Y);
            FlashHost.SetStyleAttribute("position", "absolute");
            FlashHost.SetStyleAttribute("left", position.X.ToString() + "px");
            FlashHost.SetStyleAttribute("top", position.Y.ToString() + "px");
        }

        void FlashVideoAdPlayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FlashHost.SetStyleAttribute("width", e.NewSize.Width.ToString() + "px");
            FlashHost.SetStyleAttribute("height", e.NewSize.Height.ToString() + "px");
        }

        HtmlElement FlashHost
        {
            get
            {
                return HtmlPage.Document.GetElementById("FlashHost");
            }
        }

        HtmlElement FlashPlayer
        {
            get
            {
                dynamic doc = HtmlPage.Document;
                var result = doc.getElementsByName("FlashPlayer")[0];
                return result;
                // IE only
                //return HtmlPage.Document.GetElementById("FlashPlayer");
            }
        }

        public string HandshakeVersion(string version)
        {
            if (isReady)
            {
                return (string)FlashPlayer.Invoke("handshakeVersion", version);
            }
            else
            {
                return "1.1";
            }
        }

        public void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            FlashHost.SetStyleAttribute("visibility", "visible");
            PositionFlashPlayer();
            
            if (isReady)
            {
                FlashHost.SetStyleAttribute("width", width.ToString() + "px");
                FlashHost.SetStyleAttribute("height", height.ToString() + "px");
                FlashPlayer.Invoke("initAd", FlashPluginWidth, FlashPluginHeight, viewMode, desiredBitrate, creativeData, environmentVariables);
            }
            else
            {
                pendingInit = new PendingInit()
                {
                    width = width,
                    height = height,
                    viewMode = viewMode,
                    desiredBitrate = desiredBitrate,
                    creativeData = creativeData,
                    environmentVariables = environmentVariables
                };
            }
        }

        public void StartAd()
        {
            FlashPlayer.Invoke("startAd");
        }

        public void StopAd()
        {
            FlashPlayer.Invoke("stopAd");
        }

        public void ResizeAd(double width, double height, string viewMode)
        {
            FlashHost.SetStyleAttribute("width", width.ToString() + "px");
            FlashHost.SetStyleAttribute("height", height.ToString() + "px");
            FlashPlayer.Invoke("resizeAd", FlashPluginWidth, FlashPluginHeight, viewMode);
        }

        public void PauseAd()
        {
            FlashPlayer.Invoke("pauseAd");
        }

        public void ResumeAd()
        {
            FlashPlayer.Invoke("resumeAd");
        }

        public void ExpandAd()
        {
            FlashPlayer.Invoke("expandAd");
        }

        public void CollapseAd()
        {
            FlashPlayer.Invoke("collapseAd");
        }

        public bool AdLinear
        {
            get
            {
                return AdSource.Type == CreativeSourceType.Linear;
                //(bool)FlashPlayer.Invoke("getVPAIDProperty", "adLinear"); 
            }
        }

        public bool AdExpanded
        {
            get { return (bool)FlashPlayer.Invoke("getVPAIDProperty", "adExpanded"); }
        }

        public TimeSpan AdRemainingTime
        {
            get { return TimeSpan.FromSeconds((double)FlashPlayer.Invoke("getVPAIDProperty", "adRemainingTime")); }
        }

        public double AdVolume
        {
            get { return (double)FlashPlayer.Invoke("getVPAIDProperty", "adVolume"); }
            set { FlashPlayer.Invoke("setVPAIDProperty", "adVolume", value); }
        }

        public event EventHandler AdLoaded;

        public event EventHandler AdStarted;

        public event EventHandler AdStopped;

        public event EventHandler AdPaused;

        public event EventHandler AdResumed;

        public event EventHandler AdExpandedChanged;

        public event EventHandler AdLinearChanged;

        public event EventHandler AdVolumeChanged;

        public event EventHandler AdVideoStart;

        public event EventHandler AdVideoFirstQuartile;

        public event EventHandler AdVideoMidpoint;

        public event EventHandler AdVideoThirdQuartile;

        public event EventHandler AdVideoComplete;

        public event EventHandler AdUserAcceptInvitation;

        public event EventHandler AdUserClose;

        public event EventHandler AdUserMinimize;

        public event EventHandler<ClickThroughEventArgs> AdClickThru;

        public event EventHandler<VpaidMessageEventArgs> AdError;

        public event EventHandler<VpaidMessageEventArgs> AdLog;

        public event EventHandler AdRemainingTimeChange;

        public event EventHandler AdImpression;
    }
}
