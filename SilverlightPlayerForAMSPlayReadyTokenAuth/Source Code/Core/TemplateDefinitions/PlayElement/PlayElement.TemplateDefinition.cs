using System.Windows;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.AcquiringLicense, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Buffering, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Closed, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Individualizing, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Opening, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Paused, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Playing, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    [TemplateVisualState(Name = PlayElementVisualStates.PlayStates.Stopped, GroupName = PlayElementVisualStates.GroupNames.PlayStates)]
    public partial class PlayElement
    {
        
    }
}
