using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Advertising.VPAID;

namespace Microsoft.SilverlightMediaFramework.Samples.SilverlightAd
{
    [Export("IVpaid")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainPage : UserControl, IVpaid
    {
        DispatcherTimer timer;
        public MainPage()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            tickCount = 6;
            counter.Text = tickCount.ToString() + " seconds remain";
            timer.Start();

            this.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainPage_MouseLeftButtonDown);
        }

        void MainPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AdClickThru != null) AdClickThru(this, new ClickThroughEventArgs("http://microsoft.com", "test", true));
        }

        int tickCount = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            tickCount--;
            counter.Text = tickCount.ToString() + " seconds remain";
            if (tickCount == 0)
            {
                timer.Tick -= new EventHandler(timer_Tick);
                timer.Stop();
                if (AdStopped != null)
                    AdStopped(this, EventArgs.Empty);
            }
        }

        public string HandshakeVersion(string version)
        {
            return "1.1";
        }

        public void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;
        }

        public void StartAd()
        {
            if (AdStarted != null)
                AdStarted(this, EventArgs.Empty);
        }

        public void StopAd()
        {
            if (AdStopped != null)
                AdStopped(this, EventArgs.Empty);
        }

        public void ResizeAd(double width, double height, string viewMode)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;
        }

        public void PauseAd()
        {
            if (AdPaused != null)
                AdPaused(this, EventArgs.Empty);
        }

        public void ResumeAd()
        {
            if (AdResumed != null)
                AdResumed(this, EventArgs.Empty);
        }

        public void ExpandAd()
        {
            adExpanded = true;
        }

        public void CollapseAd()
        {
            adExpanded = false;
        }

        bool adLinear;
        public bool AdLinear
        {
            get { return adLinear; }
        }

        bool adExpanded;
        public bool AdExpanded
        {
            get { return adExpanded; }
        }

        public TimeSpan AdRemainingTime
        {
            get { return TimeSpan.Zero; }
        }

        double adVolume;
        public double AdVolume
        {
            get
            {
                return adVolume;
            }
            set
            {
                adVolume = value;
            }
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

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    //adLinear = !adLinear;
        //    //if (AdLinearChanged != null) AdLinearChanged(this, EventArgs.Empty);

        //    //adExpanded = !adExpanded;
        //    //if (AdExpandedChanged != null) AdExpandedChanged(this, EventArgs.Empty);

        //    if (AdUserClose != null) AdUserClose(this, EventArgs.Empty);
        //    StopAd();
        //}
    }
}
