using System.Collections.ObjectModel;

namespace Microsoft.SilverlightMediaFramework.Samples.Framework
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
            Samples = new ObservableCollection<Sample>();
        }

        public string Name { get; set; }

        public ObservableCollection<Sample> Samples { get; private set; }
    }
}