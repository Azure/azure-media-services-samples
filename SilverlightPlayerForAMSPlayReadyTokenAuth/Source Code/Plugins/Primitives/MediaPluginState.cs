namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    /// <summary>
    /// Indicates the state of an IMediaPlugin.
    /// </summary>
    public enum MediaPluginState
    {
        Closed = 0,
        Opening = 1,
        Buffering = 2,
        Playing = 3,
        Paused = 4,
        Stopped = 5,
        Individualizing = 6,
        AcquiringLicense = 7,
        ClipPlaying = 100,
    }
}