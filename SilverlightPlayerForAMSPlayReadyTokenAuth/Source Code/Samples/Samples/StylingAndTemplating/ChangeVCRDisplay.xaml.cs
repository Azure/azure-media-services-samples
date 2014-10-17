using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Samples.Framework;

namespace Microsoft.SilverlightMediaFramework.Samples.Samples.StylingAndTemplating
{
    [Sample(GroupNames.StylingAndTemplating, "Style Button Groups")]
    public partial class ChangeVCRDisplay : UserControl, ISupportCodeDisplay, ISupportBlendInstructions
    {
        public ChangeVCRDisplay()
        {
            InitializeComponent();
        }

        public string CSharpCode
        {
            get { return @""; }
        }

        public string XamlCode
        {
            get
            {
                return @"<Grid x:Name=""playercontrols"" Grid.ColumnSpan=""1"" Margin=""19,0,16,0"" RenderTransformOrigin=""0.5,0.5"">
    <Grid.RenderTransform>
        <CompositeTransform ScaleX=""-1""/>
    </Grid.RenderTransform>

<Grid x:Name=""functioncontrols"" Grid.ColumnSpan=""1"" Margin=""16,0"" Grid.Column=""4"" Opacity=""0.5"">";
            }
        }

        public string BlendInstructions
        {
            get
            {
                return @"1. In Expression Blend, Right click on a Player Control and select ""Edit Template"" --> ""Edit Copy"".
2. Select the ""functioncontrols"" visual element and set it's Opacity to 50%. That will make all buttons on the right side opaque. 
3. Select the ""playcontrols"" visual element and set it's RenderTransform to a ScaleTransform with X=-1. That will create a mirror image of the left side buttons. ";
            }
        }
    }
}
