using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    public class AdSequencingTrigger : IAdSequencingTrigger
    {
        public AdSequencingTrigger()
        {
            Sources = new List<IAdSequencingSource>();
        }

        public System.TimeSpan? Duration { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public List<IAdSequencingSource> Sources { get; private set; }
        IEnumerable<IAdSequencingSource> IAdSequencingTrigger.Sources { get { return Sources; } }
    }

    public class AdSequencingSource : IAdSequencingSource
    {
        public AdSequencingSource()
        {
            Sources = new List<IAdSequencingSource>();
            Targets = new List<IAdSequencingTarget>();
        }

        public string Uri { get; set; }
        public string Format { get; set; }

        public List<IAdSequencingSource> Sources { get; private set; }
        IEnumerable<IAdSequencingSource> IAdSequencingSource.Sources { get { return Sources; } }
        public List<IAdSequencingTarget> Targets { get; private set; }
        IEnumerable<IAdSequencingTarget> IAdSource.Targets { get { return Targets; } }
        public string AltReference { get; set; }
    }

    public class AdSequencingTarget : IAdSequencingTarget
    {
        public AdSequencingTarget()
        {
            Targets = new List<IAdSequencingTarget>();
        }

        public string Region { get; set; }
        public string Type { get; set; }
        public List<IAdSequencingTarget> Targets { get; private set; }
        IEnumerable<IAdSequencingTarget> IAdSequencingTarget.Targets { get { return Targets; } }
    }
}
