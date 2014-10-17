using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <remarks>
    /// The VolumeControl has visual states defined that allow a designer to customize the appearance of the control in various states
    /// (such as MouseOver, Muted, and Disabled)
    /// using an application
    /// such as Expression Blend.
    /// </remarks>
    //VisualStates
    [TemplateVisualState(Name = VolumeControlVisualStates.CommonStates.Normal, GroupName = VolumeControlVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.CommonStates.MouseOver, GroupName = VolumeControlVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.CommonStates.Pressed, GroupName = VolumeControlVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.CommonStates.Disabled, GroupName = VolumeControlVisualStates.GroupNames.CommonStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.MutedStates.VolumeOn, GroupName = VolumeControlVisualStates.GroupNames.MuteStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.MutedStates.Muted, GroupName = VolumeControlVisualStates.GroupNames.MuteStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.ExpandedStates.Expanded, GroupName = VolumeControlVisualStates.GroupNames.ExpandedStates)]
    [TemplateVisualState(Name = VolumeControlVisualStates.ExpandedStates.Collapsed, GroupName = VolumeControlVisualStates.GroupNames.ExpandedStates)]

    //Parts
    [TemplatePart(Name = VolumeControlTemplateParts.SliderElement, Type = typeof(Slider))]
    [TemplatePart(Name = VolumeControlTemplateParts.BaseElement, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = VolumeControlTemplateParts.ExpandingElement, Type = typeof(FrameworkElement))]
    public partial class VolumeControl
    {
            
    }
}
