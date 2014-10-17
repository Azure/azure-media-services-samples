namespace Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions
{
    internal static class VolumeControlVisualStates
    {
        internal static class GroupNames
        {
            internal const string CommonStates = "CommonStates";
            internal const string MuteStates = "MuteStates";
            internal const string ExpandedStates = "ExpandedStates";
        }

        internal static class CommonStates
        {
            public const string Normal = "Normal";
            public const string Disabled = "Disabled";
            public const string Pressed = "Pressed";
            public const string MouseOver = "MouseOver";
        }

        internal static class MutedStates
        {
            public const string Muted = "Muted";
            public const string VolumeOn = "VolumeOn";
        }

        internal static class ExpandedStates
        {
            public const string Expanded = "Expanded";
            public const string Collapsed = "Collapsed";
        }
    }
}
