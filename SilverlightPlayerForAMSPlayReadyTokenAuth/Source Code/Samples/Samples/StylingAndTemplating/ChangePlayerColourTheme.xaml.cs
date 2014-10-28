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
    [Sample(GroupNames.StylingAndTemplating, "Change Player Color Theme")]
    public partial class ChangePlayerColourTheme : UserControl, ISupportBlendInstructions
    {
        public ChangePlayerColourTheme()
        {
            InitializeComponent();
        }

        public string BlendInstructions
        {
            get
            {
                return @"1. In Expression Blend, Right Click on the Control and choose: ""Edit Template"" --> ""Edit Copy""
2. Choose a name for the new Style, for example ""playerStyle1"".

3. To change the ""Playlist"" button background:
3.1. Go to the ""Resources"" pane and change the ""blueGradient"" brush to a diffrent color brush, like Light Green.

4. To change the majority of Buttons display style:
4.1. Go to the ""Resources"" pane and change the ""btnBasePressed"" and the ""btnBaseGradient"" brushes to change the background gradients.

5. To change the background of the Button Panel:
5.1. In the ""Objects and Timeline"" pane select the ""ControllerContainer"" Grid and change it's background property.

6. To change the background of the Slow Motion button: 
6.1. In the ""Objects and Timeline"" pane select ""SlowMotionElement"" visual element, right click and select ""Edit Template"" --> ""Edit Current"". 
6.2. Select the ""background"" visual element and set the Background property.

7. To change the background of the Timeline Slider:
7.1. Right click on the ""TimelineElement"" and select ""Edit Template"" --> ""Edit Copy"".
7.2. Select the ""HorizontalTrackLargeChangeIncreaseRepeatButton"" Visual element and Right Click --> ""Edit Template"" --> ""Edit Current"". 
7.3. Select the Rectangle and change it's Background property. 

8. To change the background of the Volume Button: 
8.1. Right Click on the ""VolumeElement"" and select  ""Edit Template"" --> ""Edit Copy"".
8.2. When asked about conflicting resources choose ""Add the copied resources alongside the existing resources"". 
8.3. Assuming step 4.1 has happened, the Volume Button should now be Green. 

9. To change the ""Full Screen"" Button background:
9.1. Right click on the ""FullScreenToggleElement"" visual element and select ""Edit Template --> Edit Copy"".
9.2. Select the ""background"" visual element and change it's background. ";
            }
        }
    }
}
