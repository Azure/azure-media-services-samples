using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.HealthMonitorPlayer.ViewModels;
using Microsoft.Web.Media.Diagnostics;
using Microsoft.SilverlightMediaFramework.Diagnostics;
using System.Xml;

namespace Microsoft.HealthMonitorPlayer.Views
{
    public partial class TraceConfig : UserControl
    {
        public TraceConfig()
        {
            InitializeComponent();
            this.Loaded += TraceConfig_Loaded;
        }

        bool loaded;
        void TraceConfig_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= TraceConfig_Loaded;
            loaded = true;
        }

        private void Detailed_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            Tracing.EnableTraceArea(TraceArea.All);
        }

        private void Default_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (loaded) // no need to run this just because the radio button is checked by default
            {
                // go get the config file that was used to initialize tracing and reapply it.
                var defaultConfigFile = TraceMonitor.Config.TracingConfigFile;
                using (XmlReader reader = XmlReader.Create(defaultConfigFile))
                {
                    Tracing.ReadTraceConfig(reader);
                }
            }
        }

        private void Custom_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            TraceAreaList.ItemsSource = GetTraceAreaViewModels();
        }

        static IEnumerable<TraceAreaViewModel> GetTraceAreaViewModels()
        {
            foreach (TraceArea item in GetEnumValues<TraceArea>().Where(t => t != TraceArea.All && t != TraceArea.None))
            {
                yield return new TraceAreaViewModel(item);
            }
        }

        static IEnumerable<T> GetEnumValues<T>()
        {
            foreach (FieldInfo fi in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                yield return (T)Enum.Parse(typeof(T), fi.Name, false);
            }
        }
    }
}
