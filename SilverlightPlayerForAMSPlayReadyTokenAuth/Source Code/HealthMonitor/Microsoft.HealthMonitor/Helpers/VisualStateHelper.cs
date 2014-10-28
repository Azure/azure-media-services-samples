using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.HealthMonitor.Helpers
{
    public static class VisualStateHelper
    {
        // This is the definition of the extension method FindStoryboard() shown above (which is where the real work happens) 
        public static Storyboard FindStoryboard(this FrameworkElement parent, string groupName, string stateName)
        {
            VisualState visualState = VisualStateManager.GetVisualStateGroups(parent)
                .Cast<VisualStateGroup>().Where(group => group.Name == groupName)
                .SingleOrDefault()
                .States.Cast<VisualState>()
                .Where(state => state.Name == stateName)
                .SingleOrDefault();

            if (visualState != null)
                return visualState.Storyboard;

            return null;
        }
    }
}
