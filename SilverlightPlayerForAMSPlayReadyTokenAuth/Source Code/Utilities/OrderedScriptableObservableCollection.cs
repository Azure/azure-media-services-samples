using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public class OrderedScriptableObservableCollection<TItem> : ScriptableObservableCollection<TItem>
    {

        public new void Add(TItem item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add item
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <param name="comparable">the object to use for comparisons</param>
        [ScriptableMember]
        public void Add(TItem item, IComparable<TItem> comparable)
        {
            var index = SearchForInsertIndex(comparable);
            base.InsertItem(index, item);
        }

        protected int SearchForInsertIndex(IComparable<TItem> item)
        {
            int insertIndex = 0;

            if (this.Items.Any())
            {
                int searchIndex = this.Items.Count / 2;
                insertIndex = SearchForInsertIndex(item, 0, this.Items.Count-1);

                while (insertIndex < this.Count && item.CompareTo(this[insertIndex]) == 0)
                {
                    insertIndex++;
                }
            }

            return insertIndex;
        }

        private int SearchForInsertIndex(IComparable<TItem> item, int startIndex, int endIndex)
        {
            int insertIndex;
            
            if (endIndex - startIndex > 1)
            {
                int searchIndex = startIndex + ((endIndex - startIndex) / 2);
                var comparisonResult = item.CompareTo(Items[searchIndex]);

                if (comparisonResult > 0)
                {
                    insertIndex = SearchForInsertIndex(item, searchIndex, endIndex);
                }
                else if (comparisonResult < 0)
                {
                    insertIndex = SearchForInsertIndex(item, startIndex, searchIndex);
                }
                else
                {
                    insertIndex = searchIndex;
                }
            }
            else
            {
                var comparisonResult = item.CompareTo(Items[startIndex]);

                if (comparisonResult > 0)
                {
                    comparisonResult = item.CompareTo(Items[endIndex]);

                    if (comparisonResult > 0)
                    {
                        insertIndex = endIndex + 1;
                    }
                    else
                    {
                        insertIndex = endIndex;
                    }
                }
                else
                {
                    insertIndex = startIndex;
                }
            }

            return insertIndex;
        }


        /// <summary>
        /// insert item into collection
        /// </summary>
        /// <param name="index">index to insert at</param>
        /// <param name="item">item to insert</param>
        public new void Insert(int index, TItem item)
        {
            throw new NotImplementedException();
        }
    }
}
