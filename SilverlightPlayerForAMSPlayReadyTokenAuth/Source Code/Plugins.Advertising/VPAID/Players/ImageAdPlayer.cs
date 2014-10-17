using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A VPAID ad player that can show static image ads
    /// </summary>
    public class ImageAdPlayer : QuartileAdPlayerBase
    {
        public ImageAdPlayer(ICreativeSource Source, IAdTarget Target, IMediaPlugin ActiveMediaPlugin)
            : base(Source.Dimensions, Source.IsScalable, Source.Type == CreativeSourceType.Linear)
        {
            this.ActiveMediaPlugin = ActiveMediaPlugin;
            AdTarget = Target;
            AdSource = Source;
            if (AdSource.ClickUrl != null)
            {
                this.NavigateUri = new Uri(AdSource.ClickUrl, UriKind.RelativeOrAbsolute);
            }
        }

        protected IMediaPlugin ActiveMediaPlugin;
        public IAdTarget AdTarget { get; private set; }
        public ICreativeSource AdSource { get; private set; }

        protected override void OnClick()
        {
            OnAdClickThru(new AdClickThruEventArgs(AdSource.ClickUrl, AdSource.Id, false));
            base.OnClick();
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

        protected override FrameworkElement CreateContentsElement()
        {
            Image image = new Image() { Visibility = System.Windows.Visibility.Collapsed };
            if (AdSource.IsScalable)
            {
                if (AdSource.MaintainAspectRatio)
                {
                    image.Stretch = Stretch.Uniform;
                }
                else
                {
                    image.Stretch = Stretch.Fill;
                }
            }
            else
            {
                image.Stretch = Stretch.None;
            }

            image.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(image_ImageFailed);
            image.ImageOpened += new EventHandler<RoutedEventArgs>(image_ImageOpened);
            
            return image;
        }

        void image_ImageOpened(object sender, RoutedEventArgs e)
        {
            OnAdLoaded();
        }

        void image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Shutdown();
            OnAdError(new AdMessageEventArgs(e.ErrorException.Message));
        }

        protected Image Image
        {
            get
            {
                return ContentsElement as Image;
            }
        }

        public override void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            base.Init(width, height, viewMode, desiredBitrate, creativeData, environmentVariables);
            Image.Source = new BitmapImage(new Uri(creativeData));

            if (AdLinear)
            {
                LoadLinear();
            }
        }

        public override void StartAd()
        {
            Image.Visibility = System.Windows.Visibility.Visible;
            base.StartAd();
            if (AdSource.Duration.HasValue)
            {
                StartVideo();
            }
        }

        public override void StopAd()
        {
            Image.Visibility = System.Windows.Visibility.Collapsed;
            base.StopAd();
        }

        protected override void OnAdLinearChanged()
        {
            base.OnAdLinearChanged();
            if (AdLinear)
            {
                LoadLinear();
            }
            else
            {
                UnloadLinear();
            }
        }

        public override void Dispose()
        {
            if (AdLinear)
            {
                UnloadLinear();
            }

            if (Image != null)
            {
                Image.ImageFailed -= new EventHandler<ExceptionRoutedEventArgs>(image_ImageFailed);
                Image.ImageOpened -= new EventHandler<RoutedEventArgs>(image_ImageOpened);
            }

            ActiveMediaPlugin = null;

            base.Dispose();
        }

        //double previousVolume = .5;
        protected virtual void UnloadLinear()
        {
            //if (ActiveMediaPlugin != null)
            //{
            //    ActiveMediaPlugin.CurrentStateChanged -= MediaPlugin_CurrentStateChanged;
            //    ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Visible);
            //    //ActiveMediaPlugin.Volume = previousVolume;
            //    ActiveMediaPlugin.Play();
            //}
        }

        protected virtual void LoadLinear()
        {
            //if (ActiveMediaPlugin != null)
            //{
            //    PausePlayback(ActiveMediaPlugin);
            //    ActiveMediaPlugin.VisualElement.IfNotNull(v => v.Visibility = System.Windows.Visibility.Collapsed); 
            //    //previousVolume = ActiveMediaPlugin.Volume;
            //    //ActiveMediaPlugin.Volume = 0;
            //}
        }

        private void PausePlayback(IMediaPlugin mediaPlugin)
        {
            if (mediaPlugin.CurrentState == MediaPluginState.Buffering || mediaPlugin.CurrentState == MediaPluginState.Playing)
            {
                mediaPlugin.Pause();
            }
            else
            {
                mediaPlugin.CurrentStateChanged += MediaPlugin_CurrentStateChanged;
            }
        }

        void MediaPlugin_CurrentStateChanged(IMediaPlugin mediaPlugin, MediaPluginState state)
        {
            ActiveMediaPlugin.CurrentStateChanged -= MediaPlugin_CurrentStateChanged;
            PausePlayback(mediaPlugin);
        }
    }
}
