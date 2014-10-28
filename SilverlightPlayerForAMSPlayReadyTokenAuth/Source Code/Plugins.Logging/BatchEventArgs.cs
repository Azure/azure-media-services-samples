using System;

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// EventArgs used to return a batch object.
    /// </summary>
    public class BatchEventArgs : EventArgs
    {
        public BatchEventArgs(Batch Batch)
        {
            this.Batch = Batch;
        }

        public Batch Batch { get; private set; }
    }
}
