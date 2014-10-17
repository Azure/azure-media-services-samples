using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines an AdTarget for an HTMLElement object.
    /// </summary>
    public class HtmlElementAdTarget : AdTargetBase
    {
        readonly HtmlElement target;
        public HtmlElement Target { get { return target; } }

        /// <summary>
        /// Creates a new Target for an HtmlElement
        /// </summary>
        /// <param name="Target">The HtmlElement to be used as a target</param>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public HtmlElementAdTarget(HtmlElement Target, IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
            : base(TargetSource, TargetDependencies)
        {
            target = Target;
        }

        /// <inheritdoc />
        public override bool AddChild(IVpaid adPlayer)
        {
            // nothing to do
            return true;
        }

        /// <inheritdoc />
        public override void RemoveChild(IVpaid adPlayer)
        {
            // nothing to do
        }

        /// <inheritdoc />
        public override Size Size
        {
            get
            {
                // TODO: Should return actual size available
                var sWidth = (Target.GetStyleAttribute("width") ?? string.Empty).Trim();
                var sHeight = (Target.GetStyleAttribute("height") ?? string.Empty).Trim();

                double width = 0;
                double height = 0;
                if (sWidth.EndsWith("px"))
                {
                    width = sWidth.Substring(0, sWidth.Length - 2).Trim().ToDouble().GetValueOrDefault(0);
                }
                if (sHeight.EndsWith("px"))
                {
                    height = sHeight.Substring(0, sHeight.Length - 2).Trim().ToDouble().GetValueOrDefault(0);
                }

                return new Size(width, height);
            }
        }
    }
}
