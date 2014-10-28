using Microsoft.Web.Media.Diagnostics;

namespace Microsoft.HealthMonitorPlayer.ViewModels
{
    public class TraceAreaViewModel
    {
        readonly TraceArea traceArea;

        public TraceAreaViewModel(TraceArea TraceArea)
        {
            traceArea = TraceArea;
        }

        public string Description { get { return traceArea.ToString(); } }

        public bool IsEnabled
        {
            get { 
                return Tracing.IsTraceAreaEnabled(traceArea); 
            }
            set {
                if (value)
                    Tracing.EnableTraceArea(traceArea);
                else
                    Tracing.DisableTraceArea(traceArea);            
            }
        }
    }
}
