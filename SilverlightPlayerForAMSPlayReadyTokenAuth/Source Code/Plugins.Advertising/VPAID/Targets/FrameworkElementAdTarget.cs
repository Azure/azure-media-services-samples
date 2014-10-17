using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines the base class for a Silverlight FrameworkElement based AdTarget
    /// </summary>
    public abstract class FrameworkElementAdTarget : AdTargetBase
    {
        IVpaid adPlayer;

        protected FrameworkElement target { get; private set; }

        /// <summary>
        /// Creates a new Target for a Silverlight FrameworkElement
        /// </summary>
        /// <param name="Target">The FrameworkElement for this target</param>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public FrameworkElementAdTarget(FrameworkElement Target, IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
            : base(TargetSource, TargetDependencies)
        {
            target = Target;
            target.SizeChanged += new SizeChangedEventHandler(target_SizeChanged);
#if !WINDOWS_PHONE && !FULLSCREEN
            Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);
#endif
            size = target.GetEffectiveSize();
        }

#if !WINDOWS_PHONE && !FULLSCREEN
        void Content_FullScreenChanged(object sender, EventArgs e)
        {
            if (adPlayer != null)
            {
                adPlayer.ResizeAd(size.Width, size.Height, Application.Current.Host.Content.IsFullScreen ? "fullscreen" : "normal");
            }
        }
#endif

        Size size;
        void target_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            size = e.NewSize;
            if (adPlayer != null)
            {
#if !WINDOWS_PHONE && !FULLSCREEN
                adPlayer.ResizeAd(e.NewSize.Width, e.NewSize.Height, Application.Current.Host.Content.IsFullScreen ? "fullscreen" : "normal");
#else
                adPlayer.ResizeAd(e.NewSize.Width, e.NewSize.Height, "normal");
#endif
            }
        }

        /// <inheritdoc />
        public override bool AddChild(IVpaid AdPlayer)
        {
            if (!(AdPlayer is FrameworkElement)) throw new ArgumentException();
            ((FrameworkElement)AdPlayer).SetValue(UserControl.NameProperty, target.Name + ((FrameworkElement)AdPlayer).Name + AdPlayer.GetHashCode());
            adPlayer = AdPlayer;
            return true;
        }

        /// <inheritdoc />
        public override void RemoveChild(IVpaid AdPlayer)
        {
            if (adPlayer != AdPlayer) throw new ArgumentException();
            adPlayer = null;
        }

        /// <inheritdoc />
        public override Size Size
        {
            get { return size; }
        }
    }
}
