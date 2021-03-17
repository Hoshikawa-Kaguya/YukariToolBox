using System;
using System.Collections.Generic;
using System.Linq;

namespace YukariToolBox.Extensions
{
    public static class CollectionExtensions
    {
#nullable enable
        public static bool ArrayEquals<T>(this T[]? arr1, T[]? arr2)
        {
            if (arr1?.Length != arr2?.Length
             || arr1 is null    && !(arr2 is null)
             || !(arr1 is null) && arr2 is null)
            {
                return false;
            }

            if (arr1 is null && arr2 is null)
            {
                return true;
            }

            for (int i = 0; i < arr1?.Length; i++)
            {
                if (arr2 != null && !(arr1[i] is null && arr2[i] is null))
                {
                    if (arr1[i] is null || arr2[i] is null)
                    {
                        return false;
                    }

                    if (!(arr1[i]?.Equals(arr2[i]) ?? true))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool Update<TSource>
            (this IEnumerable<TSource> source, Action<TSource> updateAction)
            where TSource : class
        {
            foreach (TSource item in source)
            {
                updateAction(item);
            }

            return true;
        }

        public static (IEnumerable<TSource>, ICollection<TSource>) UpdateWhen<TSource>
            (this ICollection<TSource> source, Func<TSource, bool> whereAction)
            where TSource : struct
        {
            return (source.Where(whereAction), source);
        }

        public static (IEnumerable<TSource>, IList<TSource>) UpdateWhen<TSource>
            (this IList<TSource> source, Func<TSource, bool> whereAction)
            where TSource : struct
        {
            return (source.Where(whereAction), source);
        }

        public static (IEnumerable<TSource>, IList<TSource>) ExecuteUpdate<TSource>
            (this (IEnumerable<TSource>, IList<TSource>) source, TSource newValue)
            where TSource : struct
        {
            if (!source.Item2.IsReadOnly)
            {
                var searchResult = source.Item1.ToList();
                var sourceList   = source.Item2.ToList();
                for (var i = 0; i < searchResult.Count; i++)
                {
                    //查找源列表中有哪些匹配搜索到的内容，将其更新为新值
                    int index = sourceList.FindIndex(j => j.Equals(searchResult[i]));
                    source.Item2[index] = newValue;
                }
            }

            //source.Item2 = temp;
            return (source.Item1, source.Item2);
        }

        public static (IEnumerable<TSource>, ICollection<TSource>) ExecuteUpdate<TSource>
            (this (IEnumerable<TSource>, ICollection<TSource>) source, TSource newValue)
            where TSource : struct
        {
            if (!source.Item2.IsReadOnly)
            {
                bool hasUpdate    = false;
                var  searchResult = source.Item1.ToList();
                var  sourceList   = source.Item2.ToList();
                for (var i = 0; i < searchResult.Count; i++)
                {
                    int index = sourceList.FindIndex(j => j.Equals(searchResult[i]));
                    if (index != -1)
                    {
                        sourceList[index] = newValue;
                        hasUpdate         = true;
                    }
                }

                if (hasUpdate)
                {
                    source.Item2.Clear();
                    sourceList.ForEach(i => { source.Item2.Add(i); });
                }
            }

            //source.Item2 = temp;
            return (source.Item1, source.Item2);
        }
    }
}