using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Indicates how the media is delivered.
    /// </summary>
    [Flags]
    public enum DeliveryMethods
    {
        NotSpecified = 0,
        ProgressiveDownload = 0x1,
        Streaming = 0x2,
        AdaptiveStreaming = 0x4,
    }
}