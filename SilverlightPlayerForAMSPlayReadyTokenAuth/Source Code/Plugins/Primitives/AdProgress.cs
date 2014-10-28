namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Indicates the progress of an ad as it is being played.
    /// </summary>
    public enum AdProgress
    {
        Start = 0,
        FirstQuartile,
        Midpoint,
        ThirdQuartile,
        Complete,
        Unknown
    }
}