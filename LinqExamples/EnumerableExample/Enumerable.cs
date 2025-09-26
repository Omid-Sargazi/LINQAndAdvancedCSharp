using System.Diagnostics;

namespace LinqExamples.EnumerableExample
{
    public static class Enumerable
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector, Comparer<TKey>.Default);
        }
    }


    public class UseLinq
    {
        public static void Run()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6 };
            var evenNumbers = nums.Where(x => x % 2 == 0);
        }
    }
}