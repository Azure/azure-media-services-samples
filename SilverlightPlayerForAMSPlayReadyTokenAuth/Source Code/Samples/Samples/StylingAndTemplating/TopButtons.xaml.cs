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
    [Sample(GroupNames.StylingAndTemplating, "Buttons on Top")]
    public partial class TopButtons : UserControl, ISupportBlendInstructions, ISupportCodeDisplay
    {
        public TopButtons()
        {
            InitializeComponent();
        }

        public string BlendInstructions
        {
            get
            {
                return @"1. In Expression Blend, right click on the Player control and select ""Edit Template"" --> ""Edit Copy"".
2. Select the ""ContainerController"" visual element and change it's Grid.Row from ""4"" to ""0"". ";
            }
        }

        public string CSharpCode
        {
            get { return @""; }
        }

        public string XamlCode
        {
            get { return @"<Grid Grid.Row=""0"" x:Name=""ControllerContainer"" Height=""40"" VerticalAlignment=""Top"">"; }
        }
    }
}
