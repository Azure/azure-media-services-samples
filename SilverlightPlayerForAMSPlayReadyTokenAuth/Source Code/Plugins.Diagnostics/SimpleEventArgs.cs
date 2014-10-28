using System;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Wraps a single object in an event args.
    /// </summary>
    /// <typeparam name="T">The type of object that the event args will wrap</typeparam>
    public class SimpleEventArgs<T> : EventArgs
    {
        public T Result { get; set; }

        public SimpleEventArgs(T result)
        {
            Result = result;
        }
    }
}
