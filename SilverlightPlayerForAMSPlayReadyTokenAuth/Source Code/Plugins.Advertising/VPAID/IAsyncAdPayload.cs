using System;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    public interface IAsyncAdPayload : IAdPayload
    {
        StateEnum State { get; }
        event EventHandler StateChanged;
    }

    public enum StateEnum
    { 
        Loading,
        Ready,
        Failed
    }
}
