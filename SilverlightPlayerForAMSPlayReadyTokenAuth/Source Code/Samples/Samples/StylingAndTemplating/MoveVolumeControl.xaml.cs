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
    [Sample(GroupNames.StylingAndTemplating, "Volume in Buttons Bar")]
    public partial class MoveVolumeControl : UserControl, ISupportBlendInstructions, ISupportCodeDisplay
    {
        public MoveVolumeControl()
        {
            InitializeComponent();
        }

        public string BlendInstructions
        {
            get
            {
                return @"1. In Expression Blend right click on the Player control and select ""Edit Template"" --> ""Edit Copy"".
2. Select the ""VolumeElement"" visual element, Right click and select ""Edit Template"" --> ""Edit Copy"". 
3. In the ""Objects and Timeline"" pane delete the entire visual tree.
4. Add the following element into the Volume Control Template:
<Slider x:Name=""SliderElement"" Minimum=""0"" Maximum=""1"" LargeChange=""0.1"" SmallChange=""0.01"" />";
            }
        }

        public string CSharpCode
        {
            get { return @""; }
        }

        public string XamlCode
        {
            get
            {
                return @"<Style x:Key=""VolumeControlStyle1"" TargetType=""smf:VolumeControl"" >
	<Setter Property=""Template"">
		<Setter.Value>
			<ControlTemplate TargetType=""smf:VolumeControl"">
				<Grid VerticalAlignment=""Bottom"" Cursor=""Hand"" Background=""Transparent"" Margin=""0,0,0,0"" Width=""36"" Height=""28"" >
					<VisualStateManager.VisualStateGroups>
						<VisualStateGroup x:Name=""CommonStates"">
							<VisualState x:Name=""Normal""/>
							<VisualState x:Name=""MouseOver""/>
							<VisualState x:Name=""Pressed""/>
							<VisualState x:Name=""Disabled""/>
						</VisualStateGroup>
						<VisualStateGroup x:Name=""ExpandedStates"">
							<VisualStateGroup.Transitions>
								<VisualTransition GeneratedDuration=""0:0:0.3""/>
							</VisualStateGroup.Transitions>
							<VisualState x:Name=""Expanded""/>
							<VisualState x:Name=""Collapsed""/>
						</VisualStateGroup>
						<VisualStateGroup x:Name=""MutedStates"">
							<VisualState x:Name=""Muted""/>
							<VisualState x:Name=""VolumeOn""/>
						</VisualStateGroup>
					</VisualStateManager.VisualStateGroups>

					<Slider x:Name=""SliderElement"" Minimum=""0"" Maximum=""1"" LargeChange=""0.1"" SmallChange=""0.01"" />

				</Grid>
			</ControlTemplate>
		</Setter.Value>
	</Setter>
</Style>";
            }
        }
    }
}
