using System.Collections.Generic;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// Defines the base class for all AdTargets, HTML or Silverlight
    /// </summary>
    public abstract class AdTargetBase : IAdTarget
    {
        readonly IAdSequencingTarget targetSource;
        readonly IEnumerable<IAdSequencingTarget> targetDependencies;

        /// <summary>
        /// Constructor for AdTargetBase base class
        /// </summary>
        /// <param name="TargetSource">The sequencing target associated with this target</param>
        /// <param name="TargetDependencies">The sequencing target this is dependent on</param>
        public AdTargetBase(IAdSequencingTarget TargetSource, IEnumerable<IAdSequencingTarget> TargetDependencies)
        {
            targetDependencies = TargetDependencies;
            targetSource = TargetSource;
        }

        /// <inheritdoc />
        public abstract bool AddChild(IVpaid adPlayer);

        /// <inheritdoc />
        public abstract void RemoveChild(IVpaid adPlayer);

        /// <inheritdoc />
        public abstract Size Size { get; }

        /// <inheritdoc />
        public IAdSequencingTarget TargetSource
        {
            get { return targetSource; }
        }

        /// <inheritdoc />
        public IEnumerable<IAdSequencingTarget> TargetDependencies
        {
            get { return targetDependencies; }
        }
    }
}
