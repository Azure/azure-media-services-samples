using System.Collections.Generic;
using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives.Advertising
{
    public interface IAdSequencingTrigger
    {
        string Id { get; }
        string Description { get; }
        IEnumerable<IAdSequencingSource> Sources { get; }

        TimeSpan? Duration { get; }
    }

    public interface IAdSequencingSource : IAdSource
    {
        IEnumerable<IAdSequencingSource> Sources { get; }
        string Format { get; }
    }

    public interface IAdSource
    {
        string Uri { get; }
        string AltReference { get; }

        IEnumerable<IAdSequencingTarget> Targets { get; }
    }

    public interface IAdSequencingTarget
    {
        string Region { get; }
        string Type { get; }
        IEnumerable<IAdSequencingTarget> Targets { get; }
    }
}
