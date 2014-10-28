using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Microsoft.SilverlightMediaFramework.Samples.Framework
{
    public static class MetadataStore
    {
        public static List<Group> GetGroupsBasedOnReflectionData()
        {
            IEnumerable<Type> sampleTypes =
                typeof (MetadataStore).Assembly.GetTypes().Where(
                    t => typeof (UserControl).IsAssignableFrom(t)
                         && t.GetCustomAttributes(typeof (SampleAttribute), false).Any());

            IEnumerable<SampleAttribute> sampleTypeAttributes =
                sampleTypes
                    .SelectMany(t => t.GetCustomAttributes(typeof (SampleAttribute), false))
                    .OfType<SampleAttribute>();

            IEnumerable<string> groupNames =
                sampleTypeAttributes.Select(a => a.GroupName).Distinct();

            var returnValue = new List<Group>();
            foreach (string groupName in groupNames)
            {
                var newGroup = new Group(groupName);

                IEnumerable<string> sampleNameInGroup = sampleTypeAttributes.Where(a => a.GroupName == groupName)
                    .Select(a => a.Name);

                foreach (string sampleName in sampleNameInGroup)
                    newGroup.Samples.Add(new Sample(sampleName, GetSampleTypeByName(sampleName)));

                returnValue.Add(newGroup);
            }

            return returnValue;
        }

        private static Type GetSampleTypeByName(string sampleName)
        {
            return typeof (MetadataStore).Assembly.GetTypes()
                .Single(t => t.GetCustomAttributes(typeof (SampleAttribute), false).Any()
                             && t.GetCustomAttributes(typeof (SampleAttribute), false)
                                    .Select(a => (SampleAttribute) a)
                                    .First().Name == sampleName);
        }
    }
}