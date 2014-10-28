#if WINDOWS_PHONE_70
using System.Collections.Generic;
namespace System.Linq
{
    public interface IQueryable<T> : IEnumerable<T>
    {
        
    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> AsQueryable<T>(this IEnumerable<T> enumerable)
        {
            return new Queryable<T>(enumerable);
        }

        public static IEnumerable<T> AsEnumerable<T>(this IQueryable<T> queryable)
        {
            return (IEnumerable<T>) queryable;
        }

        public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Func<TSource, IEnumerable<TResult>> selector) 
        {
            return source.AsEnumerable().SelectMany(selector).AsQueryable();
        }

        public static IQueryable<TSource> Concat<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            return source1.AsEnumerable().Concat(source2).AsQueryable();
        }

        public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
        {
            return source1.AsEnumerable().Except(source2).AsQueryable();
        }

        
    }

    public class Queryable<T> : IQueryable<T>
    {
        private IEnumerable<T> _collection;

        public Queryable(IEnumerable<T> collection)
        {
            _collection = collection;    
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
#endif