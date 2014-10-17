using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines an AdTarget for a Silverlight Border control.
    /// </summary>
    public class BorderAdTarget : FrameworkElementAdTarget
    {
        /// <summary>
        /// Creates a new Target for a Silverlight Border control
        /// </summary>
        /// <param name="Target">The Border control to be used as a target</param>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public BorderAdTarget(Border Target, IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
            : base(Target, TargetSource, TargetDependencies)
        {
        }

        /// <inheritdoc />
        public override bool AddChild(IVpaid AdPlayer)
        {
            if (((Border)target).Child == AdPlayer) return false;
            if (AdPlayer is UIElement && base.AddChild(AdPlayer))
            {
                ((Border)target).Child = (UIElement)AdPlayer;
                return true;
            }
            else return false;
        }

        /// <inheritdoc />
        public override void RemoveChild(IVpaid AdPlayer)
        {
            base.RemoveChild(AdPlayer);
            ((Border)target).Child = null;
        }
    }
}
