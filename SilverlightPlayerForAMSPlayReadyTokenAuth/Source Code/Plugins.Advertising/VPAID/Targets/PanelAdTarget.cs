using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines an AdTarget for a Silverlight Panel control such as a Grid or StackPanel.
    /// </summary>
    public class PanelAdTarget : FrameworkElementAdTarget
    {
        /// <summary>
        /// Creates a new Target for a Silverlight Panel control. This includes Grids and StackPanels
        /// </summary>
        /// <param name="Target">The Panel control to be used as a target</param>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public PanelAdTarget(Panel Target, IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
            : base(Target, TargetSource, TargetDependencies)
        {
        }

        /// <inheritdoc />
        public override bool AddChild(IVpaid AdPlayer)
        {
            if (((Panel)target).Children.Contains(AdPlayer as UIElement)) return false;
            if (base.AddChild(AdPlayer))
            {
                ((Panel)target).Children.Add(AdPlayer as UIElement);
                return true;
            }
            else return false;
        }

        /// <inheritdoc />
        public override void RemoveChild(IVpaid AdPlayer)
        {
            base.RemoveChild(AdPlayer);
            ((Panel)target).Children.Remove(AdPlayer as UIElement);
        }
    }
}
