using System;
using System.Windows;
using System.Windows.Browser;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A VPAID ad player that can play html based ads.
    /// </summary>
    public class HtmlAdPlayer : QuartileAdPlayerBase
    {
        public HtmlAdPlayer(ICreativeSource Source, HtmlElementAdTarget Target)
            : base(Source.Dimensions, Source.IsScalable, Source.Type == CreativeSourceType.Linear)
        {
            AdTarget = Target;
            AdSource = Source;
        }

        public HtmlElementAdTarget AdTarget { get; private set; }
        public ICreativeSource AdSource { get; private set; }
        private HtmlElement element;

        protected override FrameworkElement CreateContentsElement()
        {
            return null;
        }

        public void OnClick(object sender, EventArgs e)
        {
            OnAdClickThru(new AdClickThruEventArgs(AdSource.ClickUrl, AdSource.Id, true));
        }

        public override void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            base.InitAd(width, height, viewMode, desiredBitrate, creativeData, environmentVariables);
        }

        public override void StartAd()
        {
            string url = AdSource.MediaSource;
            string click = AdSource.ClickUrl;
            string alt = AdSource.AltText;

            switch (AdSource.MediaSourceType)
            {
                case MediaSourceEnum.Static:
                    if (AdTarget.Target.TagName == "img")
                    {
                        element = AdTarget.Target;
                    }
                    else
                    {
                        element = HtmlPage.Document.CreateElement("img");
                        AdTarget.Target.AppendChild(element);
                    }

                    element.SetAttribute("src", url);
                    element.SetAttribute("alt", alt);
                    element.AttachEvent("onclick", new EventHandler(OnClick));
                    break;
                case MediaSourceEnum.HTML:
                    element = AdTarget.Target;
                    element.SetProperty("innerHTML", url);
                    element.AttachEvent("onclick", new EventHandler(OnClick));
                    break;
                case MediaSourceEnum.IFrame:
                    if (AdTarget.Target.TagName == "iframe")
                    {
                        element = AdTarget.Target;
                    }
                    else
                    {
                        element = HtmlPage.Document.CreateElement("iframe");
                        AdTarget.Target.AppendChild(element);
                    }

                    element.SetAttribute("src", url);
                    element.AttachEvent("onclick", new EventHandler(OnClick));
                    break;
            }

            if (!AdSource.Dimensions.IsEmpty)
            {
                element.SetStyleAttribute("width", AdSource.Dimensions.Width.ToString() + "px");
                element.SetStyleAttribute("height", AdSource.Dimensions.Height.ToString() + "px");
            }
            AdTarget.Target.SetStyleAttribute("visibility", "visible");

            base.StartAd();
            if (AdSource.Duration.HasValue)
            {
                StartVideo();
            }
        }

        public override void ResizeAd(double width, double height, string viewMode)
        {
            base.ResizeAd(width, height, viewMode);
        }

        public override void StopAd()
        {
            AdTarget.Target.SetStyleAttribute("visibility", "hidden");
            base.StopAd();
        }

        protected override TimeSpan? Duration
        {
            get
            {
                if (AdSource.Duration.HasValue)
                    return AdSource.Duration;
                else
                    return null;
            }
        }

        public override void Dispose()
        {
            if (element != null)
            {
                if (element != AdTarget.Target)
                {
                    AdTarget.Target.RemoveChild(element);
                }
            }
            element.DetachEvent("onclick", new EventHandler(OnClick));
            
            if (AdTarget != null)
            {
                AdTarget.Target.SetStyleAttribute("visibility", "hidden");
            }
            base.Dispose();
        }
    }
}
