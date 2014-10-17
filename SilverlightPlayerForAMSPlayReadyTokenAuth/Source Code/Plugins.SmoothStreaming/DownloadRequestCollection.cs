using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Utilities;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.SmoothStreaming
{
    internal class DownloadRequestCollection : OrderedScriptableObservableCollection<DownloadRequest>
    {
        public new void Add(DownloadRequest item)
        {
            var comparable = new DownloadRequestComparable(item.ChunkTimestamp);
            Add(item, comparable);
        }

        public IEnumerable<DownloadRequest> WhereAfterPosition(TimeSpan position, int? count = null)
        {
            var comparer = new DownloadRequestComparable(position);
            var startIndex = SearchForInsertIndex(comparer);

            return count.HasValue
                    ? this.Skip(startIndex).Take(count.Value).ToList()
                    : this.Skip(startIndex).ToList();
        }

        private class DownloadRequestComparable : IComparable<DownloadRequest>
        {
            private TimeSpan _searchTime;

            public DownloadRequestComparable(TimeSpan beginTime)
            {
                _searchTime = beginTime;
            }

            public int CompareTo(DownloadRequest other)
            {
                return _searchTime.CompareTo(other.ChunkTimestamp);
            }
        }
    }
}
