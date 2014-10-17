using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines an AdTarget for a Silverlight ContentControl.
    /// </summary>
    public class ControlAdTarget : FrameworkElementAdTarget
    {        
        /// <summary>
        /// Creates a new Target for a Silverlight ContentControl control
        /// </summary>
        /// <param name="Target">The ContentControl control to be used as a target</param>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public ControlAdTarget(ContentControl Target, IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
            : base(Target, TargetSource, TargetDependencies)
        {
        }

        /// <inheritdoc />
        public override bool AddChild(IVpaid AdPlayer)
        {
            if (((ContentControl)target).Content == AdPlayer) return false;
            if (base.AddChild(AdPlayer))
            {
                ((ContentControl)target).Content = AdPlayer;
                return true;
            }
            else return false;
        }

        /// <inheritdoc />
        public override void RemoveChild(IVpaid AdPlayer)
        {
            base.RemoveChild(AdPlayer);
            ((ContentControl)target).Content = null;
        }
    }
}
