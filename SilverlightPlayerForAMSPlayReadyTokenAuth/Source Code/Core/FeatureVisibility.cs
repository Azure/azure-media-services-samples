namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Indicates the visibility of a feature to a user.
    /// </summary>
    public enum FeatureVisibility
    {
        /// <summary>
        /// Feature will be accessible but not visible.
        /// </summary>
        Hidden = 0,
        /// <summary>
        /// Feature will be visible.
        /// </summary>
        Visible,
        /// <summary>
        /// Feature will not be visible and will not be accessible.
        /// </summary>
        Disabled
    }
}