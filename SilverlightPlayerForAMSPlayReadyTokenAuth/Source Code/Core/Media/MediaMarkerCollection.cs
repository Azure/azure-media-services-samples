using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SilverlightMediaFramework.Utilities;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.Diagnostics;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;

namespace Microsoft.SilverlightMediaFramework.Core.Media
{
    public class MediaMarkerCollection<TMediaMarker> : OrderedScriptableObservableCollection<TMediaMarker> where TMediaMarker : MediaMarker
    {
        private readonly IDictionary<string, TMediaMarker> _itemHash;

        public event Action<TMediaMarker> MarkerPositionChanged;

        bool _suspendCollectionChangedEvents = false;

        public MediaMarkerCollection()
        {
            _itemHash = new Dictionary<string, TMediaMarker>();
        }

        public new void Add(TMediaMarker mediaMarker)
        {
            if (!_itemHash.ContainsKey(mediaMarker.Id))
            {
                if (mediaMarker is CaptionElement)
                {
                    var comparable = new CaptionMarkerComparable(mediaMarker.Begin, (mediaMarker as CaptionElement).Index, true);
                    //var comparable = new MediaMarkerComparable(mediaMarker.End);
                    Add(mediaMarker, comparable);
                }
                else
                {
                    var comparable = new MediaMarkerComparable(mediaMarker.Begin);
                    //var comparable = new MediaMarkerComparable(mediaMarker.End);
                    Add(mediaMarker, comparable);
                }
            }
        }

        public bool ContainsId(string id)
        {
            return _itemHash.ContainsKey(id);
        }

        public TMediaMarker GetById(string id)
        {
            return _itemHash[id];
        }

        public IEnumerable<TMediaMarker> WhereActiveAtPosition(TimeSpan mediaPosition, TimeSpan? searchAfter = null)
        {
            var startIndex = 0;

            if (searchAfter.HasValue)
            {
                var comparer = new MediaMarkerComparable(searchAfter.Value);
                startIndex = SearchForInsertIndex(comparer);
            }

            return this.Skip(startIndex)
                .TakeWhile(i => i.Begin <= mediaPosition)
                .Where(i => i.IsActiveAtPosition(mediaPosition))
                .ToList();
        }

        public IEnumerable<TMediaMarker> WhereActiveInRange(TimeSpan rangeStart, TimeSpan rangeEnd, TimeSpan? searchAfter = null)
        {
            var startIndex = 0;

            if (searchAfter.HasValue)
            {
                var comparer = new MediaMarkerComparable(searchAfter.Value);
                startIndex = SearchForInsertIndex(comparer);
            }

            return this.Skip(startIndex)
                .TakeWhile(i => i.Begin <= rangeEnd)
                .Where(i => i.IsActiveInRange(rangeStart, rangeEnd))
                .ToList();
        }

        public IEnumerable<TMediaMarker> WhereContainedByRange(TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            var comparer = new MediaMarkerComparable(rangeStart);
            var startIndex = SearchForInsertIndex(comparer);

            return this.Skip(startIndex).TakeWhile(i => i.IsContainedByRange(rangeStart, rangeEnd));
        }

        public new void Clear()
        {
            _itemHash.Clear();
            base.Clear();
        }


        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                e.NewItems.Cast<TMediaMarker>()
                          .ForEach(OnItemAdded);
            }

            if (e.OldItems != null)
            {
                e.OldItems.Cast<TMediaMarker>()
                          .ForEach(OnItemRemoved);
            }

            if (!_suspendCollectionChangedEvents)
            {
                base.OnCollectionChanged(e);
            }
        }

        private void OnItemAdded(TMediaMarker item)
        {
            _itemHash.Add(item.Id, item);
            item.PositionChanged += Item_PositionChanged;
        }

        private void OnItemRemoved(TMediaMarker item)
        {
            item.PositionChanged -= Item_PositionChanged;
            _itemHash.Remove(item.Id);
        }

        private void Item_PositionChanged(MediaMarker item)
        {
            var tItem = item as TMediaMarker;

            if (tItem != null)
            {
                _suspendCollectionChangedEvents = true;
                Remove(tItem);
                Add(tItem);
                _suspendCollectionChangedEvents = false;
                MarkerPositionChanged.IfNotNull(i => i(tItem));
            }
        }

        private class CaptionMarkerComparable : IComparable<TMediaMarker>
        {
            private TimeSpan _searchTime;
            int _index;
            bool _compareBeginTime;

            public CaptionMarkerComparable(TimeSpan searchTime, int index, bool compareBeginTime)
            {
                _searchTime = searchTime;
                _index = index;
                _compareBeginTime = compareBeginTime;
            }

            public int CompareTo(TMediaMarker other)
            {
                int ret = 0;
                if (_compareBeginTime)
                {
                    ret = _searchTime.CompareTo(other.Begin);
                }
                else
                {
                    ret = _searchTime.CompareTo(other.End);
                }

                if (ret == 0)
                {
                    if (other is CaptionElement && _index != int.MinValue)
                    {
                        var i = (other as CaptionElement).Index;
                        if (i != _index)
                        {
                            return _index.CompareTo(i);
                        }
                    }
                }
                return ret;
            }
        }

        
        private class MediaMarkerComparable : IComparable<TMediaMarker>
        {
            private TimeSpan _searchTime;

            public MediaMarkerComparable(TimeSpan searchTime)
            {
                _searchTime = searchTime;
            }

            public int CompareTo(TMediaMarker other)
            {
                return _searchTime.CompareTo(other.Begin);
            }
        }
    }
}
