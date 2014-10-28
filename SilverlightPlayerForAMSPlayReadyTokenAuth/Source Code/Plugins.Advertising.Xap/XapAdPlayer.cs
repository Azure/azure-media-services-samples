using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;
using System.Xml.Linq;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// A VPAID Ad player that can play a Silverlight ad creative from a .xap file
    /// </summary>
    public class XapAdPlayer : AdPlayerHost, IVpaid, IDisposable
    {
        /// <summary>
        /// Indicates whether or not the ad is linear
        /// </summary>
        public bool Linear { get; private set; }

        /// <summary>
        /// Returns the dimensions of the ad
        /// </summary>
        public Size Dimensions { get; private set; }

        /// <summary>
        /// Returns the dimensions of the ad
        /// </summary>
        public Size ExpandedDimensions { get; private set; }

        /// <summary>
        /// Indicates that the ad is scalable.
        /// </summary>
        public bool Scalable { get; private set; }

        private ICreativeSource AdSource;

        public XapAdPlayer(ICreativeSource AdSource, IAdTarget AdTarget)
        {
            this.AdSource = AdSource;
            this.Dimensions = AdSource.Dimensions;
            this.ExpandedDimensions = AdSource.ExpandedDimensions;
            this.Scalable = AdSource.IsScalable;
            this.Linear = (AdSource.Type == CreativeSourceType.Linear);
        }

        public string HandshakeVersion(string version)
        {
            return "1.1";
        }

        double newwidth, newheight;
        string newviewMode;
        public void InitAd(double width, double height, string viewMode, int desiredBitrate, string creativeData, string environmentVariables)
        {
            newwidth = width; newheight = height; newviewMode = viewMode;
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    if (AdError != null)
                        AdError(this, new AdMessageEventArgs(e.Error.Message));
                }
                else
                {
                    StreamResourceInfo xapPackageSri = new StreamResourceInfo(e.Result, null);
                    IEnumerable<Assembly> assemblies = 
                        from part in GetAssemblyParts(xapPackageSri)
                        let stream = Application.GetResourceStream(xapPackageSri, new Uri(part.Source, UriKind.Relative)).Stream
                        select part.Load(stream);

                    foreach (var assembly in assemblies.ToList())
                    {
                        try
                        {
                            var types = assembly.GetTypes();
                            foreach (var moduleType in types.Where(t => t.GetCustomAttributes(typeof(ExportAttribute), true).OfType<ExportAttribute>().Any(a => a.ContractName == "IVpaid")))
                            {
                                try
                                {
                                    var result = Activator.CreateInstance(moduleType);
                                    XapPlugin = result;
                                    break;
                                }
                                catch { /* swallow, just keep looking for something that will work */ }
                            }
                            if (XapPlugin != null) break;
                        }
                        catch (ReflectionTypeLoadException) { /* swallow */ }
                    }

                    //foreach (var part in GetAssemblyParts(xapPackageSri))
                    //{
                    //    Stream assemblyStream = Application.GetResourceStream(xapPackageSri, new Uri(part.Source, UriKind.Relative)).Stream;
                    //    var assembly = part.Load(assemblyStream);
                    //    try
                    //    {
                    //        var types = assembly.GetTypes();
                    //        foreach (var moduleType in types.Where(t => t.GetCustomAttributes(typeof(ExportAttribute), true).OfType<ExportAttribute>().Any(a => a.ContractName == "IVpaid")))
                    //        {
                    //            try
                    //            {
                    //                var result = Activator.CreateInstance(moduleType);
                    //                XapPlugin = result;
                    //                break;
                    //            }
                    //            catch { /* swallow, just keep looking for something that will work */ }
                    //        }
                    //        if (XapPlugin != null) break;
                    //    }
                    //    catch (ReflectionTypeLoadException) { /* swallow */ }
                    //}

                    if (XapPlugin == null)
                    {
                        // failure to find an IVpaid implementation
                        if (AdError != null)
                            AdError(this, new AdMessageEventArgs("Could not find IVpaid implementation in xap creative."));
                    }
                    else
                    {
                        // position on the screen
                        IsHitTestVisible = true;
                        this.Cursor = Cursors.Hand;

                        // make linear ads opaque
                        if (Linear && ContentsElement != null)
                        {
                            Background = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            Background = new SolidColorBrush(Colors.Transparent);
                        }

                        Content = ContentsElement;

                        xapPlugin.InitAd(newwidth, newheight, newviewMode, desiredBitrate, creativeData, environmentVariables);

                        ResizeAd(newwidth, newheight, newviewMode);
                    }
                }
            };
            wc.OpenReadAsync(new Uri(creativeData, UriKind.RelativeOrAbsolute));
        }

        private static Assembly LoadAssembly(StreamResourceInfo xapPackageSri, AssemblyPart assemblyPart)
        {
            Stream assemblyStream = Application.GetResourceStream(xapPackageSri, new Uri(assemblyPart.Source, UriKind.Relative)).Stream;
            return assemblyPart.Load(assemblyStream);
        }

        private Assembly LoadAssemblyFromXap(string relativeUri, Stream xapPackageStream)
        {
            StreamResourceInfo xapPackageSri = new StreamResourceInfo(xapPackageStream, null);
            StreamResourceInfo assemblySri = Application.GetResourceStream(xapPackageSri, new Uri(relativeUri, UriKind.Relative));
            AssemblyPart assemblyPart = new AssemblyPart();

            return assemblyPart.Load(assemblySri.Stream);
        }

        private static IEnumerable<AssemblyPart> GetAssemblyParts(StreamResourceInfo xapPackageSri)
        {
            XName apName = XName.Get("AssemblyPart", "http://schemas.microsoft.com/client/2007/deployment");

            using (var resourceStream = Application.GetResourceStream(xapPackageSri, new Uri("AppManifest.xaml", UriKind.Relative)).Stream)
            {
                var root = XDocument.Load(resourceStream);
                var parts = root.Descendants(apName).Select(x => new AssemblyPart { Source = x.Attribute("Source").Value });
                return parts;
            }
        }

        private dynamic xapPlugin;
        [Import("IVpaid", AllowRecomposition = true)]
        public dynamic XapPlugin
        {
            get { return xapPlugin; }
            set
            {
                if (xapPlugin != null)
                {
                    if (xapPlugin is IVpaid)
                    {
                        xapPlugin.AdClickThru -= new EventHandler<ClickThroughEventArgs>(xapPlugin_AdClickThru);
                        xapPlugin.AdError -= new EventHandler<VpaidMessageEventArgs>(xapPlugin_AdError);
                        xapPlugin.AdLog -= new EventHandler<VpaidMessageEventArgs>(xapPlugin_AdLog);
                    }
                    else
                    {
                        UnwireHandler(xapPlugin, "AdClickThru", "xapPlugin_AdClickThruDynamic");
                        UnwireHandler(xapPlugin, "AdError", "xapPlugin_AdErrorDynamic");
                        UnwireHandler(xapPlugin, "AdLog", "xapPlugin_AdLogDynamic");
                    }
                    xapPlugin.AdExpandedChanged -= new EventHandler(xapPlugin_AdExpandedChanged);
                    xapPlugin.AdImpression -= new EventHandler(xapPlugin_AdImpression);
                    xapPlugin.AdLinearChanged -= new EventHandler(xapPlugin_AdLinearChanged);
                    xapPlugin.AdLoaded -= new EventHandler(xapPlugin_AdLoaded);
                    xapPlugin.AdPaused -= new EventHandler(xapPlugin_AdPaused);
                    xapPlugin.AdRemainingTimeChange -= new EventHandler(xapPlugin_AdRemainingTimeChange);
                    xapPlugin.AdResumed -= new EventHandler(xapPlugin_AdResumed);
                    xapPlugin.AdStarted -= new EventHandler(xapPlugin_AdStarted);
                    xapPlugin.AdStopped -= new EventHandler(xapPlugin_AdStopped);
                    xapPlugin.AdUserAcceptInvitation -= new EventHandler(xapPlugin_AdUserAcceptInvitation);
                    xapPlugin.AdUserClose -= new EventHandler(xapPlugin_AdUserClose);
                    xapPlugin.AdUserMinimize -= new EventHandler(xapPlugin_AdUserMinimize);
                    xapPlugin.AdVideoComplete -= new EventHandler(xapPlugin_AdVideoComplete);
                    xapPlugin.AdVideoFirstQuartile -= new EventHandler(xapPlugin_AdVideoFirstQuartile);
                    xapPlugin.AdVideoMidpoint -= new EventHandler(xapPlugin_AdVideoMidpoint);
                    xapPlugin.AdVideoStart -= new EventHandler(xapPlugin_AdVideoStart);
                    xapPlugin.AdVideoThirdQuartile -= new EventHandler(xapPlugin_AdVideoThirdQuartile);
                    xapPlugin.AdVolumeChanged -= new EventHandler(xapPlugin_AdVolumeChanged);
                }

                xapPlugin = value;

                if (xapPlugin != null)
                {
                    if (xapPlugin is IVpaid)
                    {
                        xapPlugin.AdClickThru += new EventHandler<ClickThroughEventArgs>(xapPlugin_AdClickThru);
                        xapPlugin.AdError += new EventHandler<VpaidMessageEventArgs>(xapPlugin_AdError);
                        xapPlugin.AdLog += new EventHandler<VpaidMessageEventArgs>(xapPlugin_AdLog);
                    }
                    else
                    {
                        // need to use reflection to hook up the event handler since it uses a dynamic delegate
                        WireUpHandler(xapPlugin, "AdClickThru", "xapPlugin_AdClickThruDynamic");
                        WireUpHandler(xapPlugin, "AdError", "xapPlugin_AdErrorDynamic");
                        WireUpHandler(xapPlugin, "AdLog", "xapPlugin_AdLogDynamic");
                    }
                    xapPlugin.AdExpandedChanged += new EventHandler(xapPlugin_AdExpandedChanged);
                    xapPlugin.AdImpression += new EventHandler(xapPlugin_AdImpression);
                    xapPlugin.AdLinearChanged += new EventHandler(xapPlugin_AdLinearChanged);
                    xapPlugin.AdLoaded += new EventHandler(xapPlugin_AdLoaded);
                    xapPlugin.AdPaused += new EventHandler(xapPlugin_AdPaused);
                    xapPlugin.AdRemainingTimeChange += new EventHandler(xapPlugin_AdRemainingTimeChange);
                    xapPlugin.AdResumed += new EventHandler(xapPlugin_AdResumed);
                    xapPlugin.AdStarted += new EventHandler(xapPlugin_AdStarted);
                    xapPlugin.AdStopped += new EventHandler(xapPlugin_AdStopped);
                    xapPlugin.AdUserAcceptInvitation += new EventHandler(xapPlugin_AdUserAcceptInvitation);
                    xapPlugin.AdUserClose += new EventHandler(xapPlugin_AdUserClose);
                    xapPlugin.AdUserMinimize += new EventHandler(xapPlugin_AdUserMinimize);
                    xapPlugin.AdVideoComplete += new EventHandler(xapPlugin_AdVideoComplete);
                    xapPlugin.AdVideoFirstQuartile += new EventHandler(xapPlugin_AdVideoFirstQuartile);
                    xapPlugin.AdVideoMidpoint += new EventHandler(xapPlugin_AdVideoMidpoint);
                    xapPlugin.AdVideoStart += new EventHandler(xapPlugin_AdVideoStart);
                    xapPlugin.AdVideoThirdQuartile += new EventHandler(xapPlugin_AdVideoThirdQuartile);
                    xapPlugin.AdVolumeChanged += new EventHandler(xapPlugin_AdVolumeChanged);
                }
            }
        }

        void UnwireHandler(object o, string eventname, string methodname)
        {
            EventInfo ei = o.GetType().GetEvent(eventname);
            MethodInfo mi = this.GetType().GetMethod(methodname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Delegate del = Delegate.CreateDelegate(ei.EventHandlerType, this, mi);
            ei.RemoveEventHandler(o, del);
        }

        void WireUpHandler(object o, string eventname, string methodname)
        {
            EventInfo ei = o.GetType().GetEvent(eventname);
            MethodInfo mi = this.GetType().GetMethod(methodname, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Delegate del = Delegate.CreateDelegate(ei.EventHandlerType, this, mi);
            ei.AddEventHandler(o, del);
        }

        void xapPlugin_AdVolumeChanged(object sender, EventArgs e)
        {
            if (AdVolumeChanged != null) AdVolumeChanged(this, e);
        }

        void xapPlugin_AdVideoThirdQuartile(object sender, EventArgs e)
        {
            if (AdVideoThirdQuartile != null) AdVideoThirdQuartile(this, e);
        }

        void xapPlugin_AdVideoStart(object sender, EventArgs e)
        {
            if (AdVideoStart != null) AdVideoStart(this, e);
        }

        void xapPlugin_AdVideoMidpoint(object sender, EventArgs e)
        {
            if (AdVideoMidpoint != null) AdVideoMidpoint(this, e);
        }

        void xapPlugin_AdVideoFirstQuartile(object sender, EventArgs e)
        {
            if (AdVideoFirstQuartile != null) AdVideoFirstQuartile(this, e);
        }

        void xapPlugin_AdVideoComplete(object sender, EventArgs e)
        {
            if (AdVideoComplete != null) AdVideoComplete(this, e);
        }

        void xapPlugin_AdUserMinimize(object sender, EventArgs e)
        {
            if (AdUserMinimize != null) AdUserMinimize(this, e);
        }

        void xapPlugin_AdUserClose(object sender, EventArgs e)
        {
            if (AdUserClose != null) AdUserClose(this, e);
        }

        void xapPlugin_AdUserAcceptInvitation(object sender, EventArgs e)
        {
            if (AdUserAcceptInvitation != null) AdUserAcceptInvitation(this, e);
        }

        void xapPlugin_AdStopped(object sender, EventArgs e)
        {
            if (AdStopped != null) AdStopped(this, e);
        }

        void xapPlugin_AdStarted(object sender, EventArgs e)
        {
            if (AdStarted != null) AdStarted(this, e);
        }

        void xapPlugin_AdResumed(object sender, EventArgs e)
        {
            if (AdResumed != null) AdResumed(this, e);
        }

        void xapPlugin_AdRemainingTimeChange(object sender, EventArgs e)
        {
            if (AdRemainingTimeChange != null) AdRemainingTimeChange(this, e);
        }

        void xapPlugin_AdPaused(object sender, EventArgs e)
        {
            if (AdPaused != null) AdPaused(this, e);
        }

        void xapPlugin_AdLogDynamic(object sender, EventArgs e)
        {
            dynamic d = e;
            xapPlugin_AdLog(this, new AdMessageEventArgs(d.Message));
        }

        void xapPlugin_AdLog(object sender, VpaidMessageEventArgs e)
        {
            if (AdLog != null) AdLog(this, e);
        }

        void xapPlugin_AdLoaded(object sender, EventArgs e)
        {
            if (AdLoaded != null) AdLoaded(this, e);
        }

        void xapPlugin_AdLinearChanged(object sender, EventArgs e)
        {
            if (AdLinear)
                Scalable = true;
            else
                Scalable = AdSource.IsScalable;

            ResizeContents();
            if (AdLinearChanged != null) AdLinearChanged(this, e);
        }

        void xapPlugin_AdImpression(object sender, EventArgs e)
        {
            if (AdImpression != null) AdImpression(this, e);
        }

        void xapPlugin_AdExpandedChanged(object sender, EventArgs e)
        {
            ResizeContents();
            if (AdExpandedChanged != null) AdExpandedChanged(this, e);
        }

        void xapPlugin_AdErrorDynamic(object sender, EventArgs e)
        {
            dynamic d = e;
            xapPlugin_AdError(this, new AdMessageEventArgs(d.Message));
        }

        void xapPlugin_AdError(object sender, VpaidMessageEventArgs e)
        {
            if (AdError != null) AdError(this, e);
        }

        void xapPlugin_AdClickThruDynamic(object sender, EventArgs e)
        {
            dynamic d = e;
            xapPlugin_AdClickThru(this, new AdClickThruEventArgs(d.Url, d.Id, d.PlayerHandles));
        }

        void xapPlugin_AdClickThru(object sender, ClickThroughEventArgs e)
        {
            if (AdClickThru != null) AdClickThru(this, e);
        }

        FrameworkElement ContentsElement
        {
            get { return XapPlugin as FrameworkElement; }
        }

        public void StartAd()
        {
            XapPlugin.StartAd();
        }

        public void StopAd()
        {
            XapPlugin.StopAd();
        }

        public void ResizeAd(double width, double height, string viewMode)
        {
            this.MaxWidth = width;
            this.MaxHeight = height;

            ResizeContents();

            if (XapPlugin != null)
            {
                XapPlugin.ResizeAd(width, height, viewMode);
            }
            else
            {
                newwidth = width; newheight = height; newviewMode = viewMode;
            }
        }

        private void ResizeContents()
        {
            if (ContentsElement != null)    // null is legit... and used in the case of html based ads
            {
                if (AdExpanded)
                {
                    if (Scalable || ExpandedDimensions.IsEmpty)
                    {
                        ContentsElement.Width = MaxWidth;
                        ContentsElement.Height = MaxHeight;
                    }
                    else
                    {
                        ContentsElement.Width = ExpandedDimensions.Width;
                        ContentsElement.Height = ExpandedDimensions.Height;
                    }
                }
                else
                {
                    if (Scalable || Dimensions.IsEmpty)
                    {
                        ContentsElement.Width = MaxWidth;
                        ContentsElement.Height = MaxHeight;
                    }
                    else
                    {
                        ContentsElement.Width = Dimensions.Width;
                        ContentsElement.Height = Dimensions.Height;
                    }
                }
            }
        }

        public void PauseAd()
        {
            XapPlugin.PauseAd();
        }

        public void ResumeAd()
        {
            XapPlugin.ResumeAd();
        }

        public void ExpandAd()
        {
            XapPlugin.ExpandAd();
        }

        public void CollapseAd()
        {
            XapPlugin.CollapseAd();
        }

        public bool AdLinear
        {
            get
            {
                if (xapPlugin != null)
                    return XapPlugin.AdLinear;
                else
                    return Linear;
            }
        }

        public bool AdExpanded
        {
            get { return XapPlugin.AdExpanded; }
        }

        public TimeSpan AdRemainingTime
        {
            get { return XapPlugin.AdRemainingTime; }
        }

        public double AdVolume
        {
            get
            {
                return XapPlugin.AdVolume;
            }
            set
            {
                XapPlugin.AdVolume = value;
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

        public void Dispose()
        {
            if (XapPlugin != null)
            {
                if (XapPlugin is IDisposable)
                    ((IDisposable)XapPlugin).Dispose();
                XapPlugin = null;
            }
        }
    }
}
