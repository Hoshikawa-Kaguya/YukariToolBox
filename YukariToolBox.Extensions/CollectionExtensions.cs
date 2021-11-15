// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace YukariToolBox.Extensions;

/// <summary>
/// 
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 将某个变量与另一个变量组成一个 List
    /// </summary>
    /// <param name="self">自己这个变量</param>
    /// <param name="another">另一个变量</param>
    /// <typeparam name="T">变量的数据类型</typeparam>
    /// <returns>由输入的 2 个变量构成的新 List</returns>
    public static List<T> With<T>(this T self, T another)
    {
        var retList = new List<T>
        {
            self,
            another
        };
        return retList;
    }

    /// <summary>
    /// 将某个变量添加到源 List 中来
    /// </summary>
    /// <param name="parentList">源 List</param>
    /// <param name="another">另一个变量</param>
    /// <typeparam name="T">变量的数据类型</typeparam>
    /// <returns>加入新成员后的 List</returns>
    public static List<T> With<T>(this List<T> parentList, T another)
    {
        parentList.Add(another);
        return parentList;
    }

    /// <summary>
    /// 将某个列表合并到源 List 中来
    /// </summary>
    /// <param name="parentList">源 List</param>
    /// <param name="anotherList">另一个列表</param>
    /// <typeparam name="T">变量的数据类型</typeparam>
    /// <returns>合并后的 List</returns>
    public static List<T> With<T>(this List<T> parentList, IEnumerable<T> anotherList)
    {
        parentList.AddRange(anotherList);
        return parentList;
    }

#nullable enable
    /// <summary>
    /// 数组元素完全相等判断（引用类型）
    /// </summary>
    /// <param name="arr1">要判断的数组1</param>
    /// <param name="arr2">要判断的数组2</param>
    /// <typeparam name="T">数组中的元素类型</typeparam>
    /// <returns>2个数组是否全等</returns>
    public static bool ArrayEquals<T>(this T[]? arr1, T[]? arr2) where T : class
    {
        if (arr1?.Length != arr2?.Length
         || arr1 is null     && arr2 is not null
         || arr1 is not null && arr2 is null)
            return false;

        if (arr1 is null && arr2 is null) return true;

        for (var i = 0; i < arr1?.Length; i++)
            if (arr2 != null && !(arr1[i] is null && arr2[i] is null))
            {
                if (arr1[i] is null || arr2[i] is null) return false;

                if (!arr1[i].Equals(arr2[i])) return false;
            }

        return true;
    }

    /// <summary>
    /// 数组元素完全相等判断（值类型）
    /// </summary>
    /// <param name="arr1">要判断的数组1</param>
    /// <param name="arr2">要判断的数组2</param>
    /// <typeparam name="T">数组中的元素类型</typeparam>
    /// <returns>2个数组是否全等</returns>
    public static bool ArrayValueEquals<T>(this T[] arr1, T[] arr2) where T : struct
    {
        if (arr1.Length != arr2.Length) return false;

        for (var i = 0; i < arr1.Length; i++)
            if (!arr1[i].Equals(arr2[i]))
                return false;

        return true;
    }

    /// <summary>
    /// 使用指定操作更新某个列表
    /// </summary>
    /// <param name="source">源列表</param>
    /// <param name="updateAction">对整个列表中元素进行的更新操作</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static bool Update<TSource>
        (this IEnumerable<TSource> source, Action<TSource> updateAction)
        where TSource : class
    {
        foreach (var item in source) updateAction(item);

        return true;
    }

    /// <summary>
    /// 更新列表的预操作，构造满足的查询条件
    /// </summary>
    /// <param name="source">源列表</param>
    /// <param name="whereAction">查询条件</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static (IEnumerable<TSource>, ICollection<TSource>) UpdateWhen<TSource>
        (this ICollection<TSource> source, Func<TSource, bool> whereAction)
        where TSource : struct
    {
        return (source.Where(whereAction), source);
    }

    /// <summary>
    /// 更新列表的预操作，构造满足的查询条件
    /// </summary>
    /// <param name="source">源列表</param>
    /// <param name="whereAction">查询条件</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static (IEnumerable<TSource>, IList<TSource>) UpdateWhen<TSource>
        (this IList<TSource> source, Func<TSource, bool> whereAction)
        where TSource : struct
    {
        return (source.Where(whereAction), source);
    }

    /// <summary>
    /// 在执行更新列表的预操作筛选出需要更新的内容后，更新源列表
    /// </summary>
    /// <param name="source">源列表</param>
    /// <param name="newValue">满足预操作的值，要更新成的新的值</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static (IEnumerable<TSource>, IList<TSource>) ExecuteUpdate<TSource>
        (this (IEnumerable<TSource>, IList<TSource>) source, TSource newValue)
        where TSource : struct
    {
        if (!source.Item2.IsReadOnly)
        {
            var searchResult = source.Item1.ToList();
            var sourceList   = source.Item2.ToList();
            foreach (var t in searchResult)
            {
                //查找源列表中有哪些匹配搜索到的内容，将其更新为新值
                var index = sourceList.FindIndex(j => j.Equals(t));
                source.Item2[index] = newValue;
            }
        }

        //source.Item2 = temp;
        return (source.Item1, source.Item2);
    }

    /// <summary>
    /// 在执行更新列表的预操作筛选出需要更新的内容后，更新源列表
    /// </summary>
    /// <param name="source">源列表</param>
    /// <param name="newValue">满足预操作的值，要更新成的新的值</param>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public static (IEnumerable<TSource>, ICollection<TSource>) ExecuteUpdate<TSource>
        (this (IEnumerable<TSource>, ICollection<TSource>) source, TSource newValue)
        where TSource : struct
    {
        if (!source.Item2.IsReadOnly)
        {
            var hasUpdate    = false;
            var searchResult = source.Item1.ToList();
            var sourceList   = source.Item2.ToList();
            foreach (var t in searchResult)
            {
                var index = sourceList.FindIndex(j => j.Equals(t));
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