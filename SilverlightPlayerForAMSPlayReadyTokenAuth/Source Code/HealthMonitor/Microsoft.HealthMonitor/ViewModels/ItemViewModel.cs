using System.Collections.Generic;

namespace Microsoft.HealthMonitor.ViewModels
{
    public class ItemViewModel
    {
        string name;
        public ItemViewModel(string Name) {
            name = Name;
        }

        public override string ToString()
        {
            return name;
        }

        public IEnumerable<object> Items { get; set; }
    }
}
