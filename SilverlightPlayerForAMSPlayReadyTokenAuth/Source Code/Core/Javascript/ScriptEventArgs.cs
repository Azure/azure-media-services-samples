using System;
using System.Windows.Browser;

namespace Microsoft.SilverlightMediaFramework.Core.Javascript
{
    /// <summary>
    /// Represents an argument for a scriptable event.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of this event argument.</typeparam>
    [ScriptableType]
    public class ScriptEventArgs<TResult> : EventArgs
    {
        public ScriptEventArgs(TResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Gets or sets the result of this event argument.
        /// </summary>
        [ScriptableMember]
        public TResult Result { get; set; }
    }
}