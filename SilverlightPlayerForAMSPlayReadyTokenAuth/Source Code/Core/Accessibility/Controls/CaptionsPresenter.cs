using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Controls
{
    public class CaptionsPresenter : ItemsControl
    {
        public void UpdateCaptions(TimeSpan mediaPosition, bool isSeeking)
        {
            var children = this.GetVisualChildren<CaptionBlockRegion>();
            children.ForEach(i => i.UpdateCaptions(mediaPosition, isSeeking));
        }
    }
}
