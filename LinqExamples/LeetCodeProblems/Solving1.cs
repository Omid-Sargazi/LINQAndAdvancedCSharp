using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace LinqExamples.LeetCodeProblems
{
    public class TwoSumProblem
    {
        public static int[] TwoSumRun(int[] arr, int target)
        {
            //[2,7,11,12],target=9;
            Dictionary<int, int> seen = [];

            for (int i = 0; i < arr.Length; i++)
            {
                int completed = target - arr[i];

                if (seen.TryGetValue(completed, out int value))
                {
                    return new int[] { value, i };
                }
                seen[arr[i]] = i;
            }

            return new int[] { 0 };
        }

        public static int[] TwoSum2(int[] arr, int target)
        {
            var pairs = arr.Select((a, b) => (val: a, idx: b)).ToArray();
            Array.Sort(pairs, (a, b) => a.CompareTo(b));

            int left = 0;
            int right = arr.Length - 1;

            while (left < right)
            {
                int sum = arr[left] + arr[right];
                if (sum == target)
                {
                    return new int[] { left, right };
                }
                else if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return new int[] { 0 };
        }

        public static int BestTimeBuySellStock(int[] prices)
        {
            int minPrice = prices[0];
            int Maxprofit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (minPrice > prices[i])
                {
                    minPrice = prices[i];
                }
                else
                {
                    int profit = prices[i] - minPrice;
                    if (profit > Maxprofit)
                    {
                        Maxprofit = profit;
                    }
                }
            }

            return Maxprofit;
        }

        public static bool ContainsDuplicate(int[] arr)
        {
            Dictionary<int, int> seen = [];
            for (int i = 0; i < arr.Length; i++)
            {
                if (seen.TryGetValue(arr[i], out int value))
                {
                    return true;
                }
                seen[arr[i]] = i;
            }

            return false;
        }

        public static bool ContainsDuplicate2(int[] arr)
        {
            HashSet<int> seen = [];
            for (int i = 0; i < arr.Length; i++)
            {
                if (!seen.Contains(arr[i]))
                {
                    seen.Add(arr[i]);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsDuplicate3(int[] arr)
        {
            HashSet<int> seen = [];
            foreach (var item in arr)
            {
                if (!seen.Add(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static int[] ProductArrayExceptSelf(int[] arr)
        {
            int n = arr.Length;
            int[] answer = new int[n];

            int[] prefix = new int[n];
            prefix[0] = 1;
            for (int i = 1; i < n; i++)
            {
                prefix[i] = prefix[i - 1] * arr[i - 1];
            }
            int[] suffix = new int[n];
            suffix[n - 1] = 1;
            for (int i = n - 2; i >= 0; i--)
            {
                suffix[i] = suffix[i + 1] * arr[i + 1];
            }

            for (int i = 0; i < n; i++)
            {
                answer[i] = prefix[i] * suffix[i];
            }



            return answer;

        }

        public static void RunType()
        {
            var p1 = new Person("omid", 42);
            var p2 = new Person("omid", 42);
            var p4 = p2 with { Age = 41 };
            var p3 = new Person("Saeed", 40);

            Console.WriteLine(p1 == p2);
            Console.WriteLine($"p1: {p1}");
            Console.WriteLine($"P4: {p4}");
        }
    }
    public record Person(string Name, int Age);


    public static class LinqMethods
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException();
            if (predicate == null) throw new ArgumentNullException();

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> source, Func<T, TResult> seelctor)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }

            if (seelctor == null) throw new ArgumentNullException();

            foreach (T item in source)
            {
                yield return seelctor(item);
            }
        }


    }
        
}