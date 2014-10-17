using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers
{
    internal static class Extensions
    {
        public static int? ToInt(this string source) 
        {
            int result;
            if (int.TryParse(source, out result))
                return result;
            else
                return null;
        }

        public static long? ToInt64(this string source)
        {
            long result;
            if (long.TryParse(source, out result))
                return result;
            else
                return null;
        }

        public static double? ToDouble(this string source)
        {
            double result;
            if (double.TryParse(source, out result))
                return result;
            else
                return null;
        }

        public static short? ToSingle(this string source)
        {
            short result;
            if (short.TryParse(source, out result))
                return result;
            else
                return null;
        }

        /// <summary>
        /// Raises the event safely by checking for null and avoiding race conditions.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="http://stackoverflow.com/questions/248072/evil-use-of-extension-methods" />
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        /// Raises the event safely by checking for null and avoiding race conditions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="http://stackoverflow.com/questions/248072/evil-use-of-extension-methods" />
        public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
        {
            if (handler != null)
                handler(sender, e);
        }

        public static IEnumerable<TSource> SkipPast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool found = false;
            foreach (var item in source)
            {
                if (found)
                    yield return item;
                else if (predicate(item))
                    found = true;
            }
        }

        public static void Remove<T>(this Queue<T> source, T itemToRemove)
        {
            var items = source.ToArray();
            source.Clear();
            foreach (T item in items)
            {
                if (!object.ReferenceEquals(item, itemToRemove))
                    source.Enqueue(item);
            }
        }

        /// <summary>
        /// Calculates the sum of a size's width and height.  Goos for quick comparisons.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double Sum(this Size s)
        {
            return s.Height * s.Width;
        }
        
        public static string Section(this string source, string start, string end)
        {
            int s = source.IndexOf(start);
            if (s >= 0)
            {
                s += start.Length;
                int e = source.IndexOf(end, s);
                if (e > s)
                {
                    return source.Substring(s, e - s);
                }
            }
            return "";
        }

        public static string SectionsReplace(this string source, string start, string end, string replace)
        {
            StringBuilder sb = new StringBuilder(512);
            int startIndex = 0;
            while (true)
            {
                int s = source.IndexOf(start, startIndex);
                if (s >= 0)
                {
                    int e = source.IndexOf(end, s + start.Length);
                    if (e >= 0)
                    {
                        sb.Append(source.Substring(startIndex, s - startIndex));
                        sb.Append(replace);
                        startIndex = e + end.Length;
                    }
                    else break;
                }
                else break;
            }
            if (startIndex == 0) return source; // optimization
            sb.Append(source.Substring(startIndex));
            return sb.ToString();
        }

        public static string SectionReplace(this string source, string start, string end, string replace)
        {
            StringBuilder sb = new StringBuilder(512);
            int s = source.IndexOf(start);
            if (s >= 0)
            {
                s += start.Length;
                int e = source.IndexOf(end, s);
                if (e > s)
                {
                    sb.Append(source.Substring(0, s));
                    sb.Append(replace);
                    sb.Append(source.Substring(e));
                    return sb.ToString();
                }
            }
            return source;
        }

        /// <summary>
        /// Scales a size based on another provided size
        /// </summary>
        /// <param name="s"></param>
        /// <param name="scale">A size used to scale this size</param>
        /// <returns>a new instance of Size</returns>
        public static Size Scale(this Size s, Size scale)
        {
            return new Size(s.Width + scale.Width, s.Height + scale.Height);
        }

        public static void Combine(this IDictionary d, object key, object val)
        {
            if (d.Contains(key))
            {
                d[key] = val;
            }
            else
            {
                d.Add(key, val);
            }
        }

        public static void Combine(this IList l, object val)
        {
            if (!l.Contains(val))
            {
                l.Add(val);
            }
        }

        public static void Combine<T, U>(this IDictionary<T, U> d, T key, U val)
        {
            if (d.ContainsKey(key))
            {
                d[key] = val;
            }
            else
            {
                d.Add(key, val);
            }
        }

        public static void Randomize<T>(this IList<T> l)
        {
            Randomize<T>(l, 0);
        }

        public static void Randomize<T>(this IList<T> l, int startIndex)
        {
            lock (l)
            {
                List<T> temp = new List<T>(); //temp shallow copy
                for (int t = startIndex; t < l.Count; t++) temp.Add(l[t]);

                Random random = new Random();
                List<int> rList = new List<int>(); // a list of randomized indicies
                while (rList.Count < temp.Count)
                {
                    int r = (int)Math.Round(random.NextDouble() * (temp.Count - 1));
                    if (!rList.Contains(r)) rList.Add(r);
                }

                while (l.Count > startIndex) l.RemoveAt(startIndex);
                foreach (int i in rList) l.Add(temp[i]);
            }
        }

        public static Size FullSize(this FrameworkElement c)
        {
            return new Size(c.ActualWidth + c.Margin.Left + c.Margin.Right, c.ActualHeight + c.Margin.Top + c.Margin.Bottom);
        }
    }
}
