using System;

namespace Microsoft.SilverlightMediaFramework.Samples.Framework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SampleAttribute : Attribute
    {
        public SampleAttribute(string groupName, string name)
        {
            GroupName = groupName;
            Name = name;
        }

        public string GroupName { get; set; }
        public string Name { get; set; }
    }
}