using System;
using System.Collections.Generic;
using System.Linq;

namespace YukariToolBox.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool Update<TSource>(this IEnumerable<TSource> source, Action<TSource> updateAction)
            where TSource : class
        {
            foreach (TSource item in source)
            {
                updateAction(item);
            }

            return true;
        }

        public static Tuple<IEnumerable<TSource>, IList<TSource>> WhereUpdate<TSource>(
            this IList<TSource> source, Func<TSource, bool> whereAction) where TSource : struct
        {
            return new(source.Where(whereAction), source);
        }

        public static Tuple<IEnumerable<TSource>, IList<TSource>> Update<TSource>(
            this Tuple<IEnumerable<TSource>, IList<TSource>> source,
            TSource newValue)
        {
            var temp = source.Item1.ToList();
            for (var i = 0; i < temp.Count; i++)
            {
                int index = source.Item2.ToList().FindIndex(j => j.Equals(temp[i]));
                source.Item2[index] = newValue;
            }

            //source.Item2 = temp;
            return new (source.Item1, source.Item2);
        }
    }
}