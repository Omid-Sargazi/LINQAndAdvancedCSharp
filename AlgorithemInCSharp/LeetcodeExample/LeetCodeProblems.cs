using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Formats.Tar;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

namespace AlgorithemInCSharp.LeetcodeExample
{
    public class SolvingLeetCode
    {
        public static int[] TwoSum1(int[] nums, int target)
        {
            var seen = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int needed = target - nums[i];
                if (seen.TryGetValue(needed, out int value))
                {
                    return new int[] { i, value };
                }

                seen[nums[i]] = i;
            }

            return new int[] { 0 };
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            var pairs = nums.Select((v, i) => (val: v, idx: i)).ToArray();

            Array.Sort(pairs, (a, b) => a.val.CompareTo(b.val));

            int l = 0, r = nums.Length - 1;
            while (l < r)
            {
                int sum = pairs[l].val + pairs[r].val;
                if (sum == target)
                {
                    return new int[] { pairs[l].idx, pairs[r].idx };
                }
                else if (sum < target)
                {
                    l++;
                }
                else
                {
                    r--;
                }
            }

            return new int[0];
        }


        public static int MaximumSubarray(int[] nums)
        {
            int maxCurrent = nums[0];
            int maxSum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                maxCurrent = MaxTwoNum(maxCurrent, nums[i] + maxCurrent);
                maxSum = MaxTwoNum(maxSum, maxCurrent);
            }

            return maxSum;
        }

        private static int MaxTwoNum(int a, int b)
        {
            if (a < b)
            {
                return b;
            }
            return a;
        }
        private static int MinTwoNum(int a, int b)
        {
            if (a > b)
            {
                return b;
            }
            return a;
        }


        public static int MaximumProductSubarray(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int maxSofar = nums[0];
            int minSofar = nums[0];
            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];

                if (num < 0)
                {
                    (minSofar, maxSofar) = (maxSofar, minSofar);
                }

                maxSofar = MaxTwoNum(maxSofar, maxSofar * num);
                minSofar = MinTwoNum(minSofar, minSofar * num);

                result = MaxTwoNum(result, maxSofar);
            }

            return result;
        }

        public static int[] MaxSubArrayWithIndices(int[] nums, out int value)
        {
            int maxCurrent = nums[0];
            int maxSum = nums[0];
            int start = 0, end = 0, tempStart = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i] + maxCurrent)
                {
                    maxCurrent = nums[i];
                    tempStart = i;
                }
                else
                {
                    maxCurrent += nums[i];
                }
                if (maxCurrent > maxSum)
                {
                    maxSum = maxCurrent;
                    start = tempStart;
                    end = i;
                }

            }
            value = maxSum;
            int[] subArray = new int[end - start + 1];
            Array.Copy(nums, start, subArray, 0, subArray.Length);
            return subArray;
        }

        public static int BestTimeBuySellStock(int[] prices)
        {
            if (prices == null || prices.Length < 2) return 0;

            int minPrice = prices[0];
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
                else
                {
                    int profit = prices[i] - minPrice;
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                    }
                }
            }

            return maxProfit;
        }

        public int MaxProfit_Kadane(int[] prices)
        {
            int maxEndingHere = 0;
            int maxSofar = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                int diff = prices[i] - prices[i - 1];
                maxEndingHere = MaxTwoNum(maxEndingHere, diff + maxEndingHere);
                maxSofar = MaxTwoNum(maxSofar, maxEndingHere);
            }

            return maxSofar;
        }
    }

    public class SolvingProblems
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            var seen = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int completed = target - nums[i];
                if (seen.TryGetValue(completed, out int value))
                {
                    return new int[] { i, value };
                }

                seen[nums[i]] = i;
            }

            return new int[] { 0 };
        }

        public static int[] TwoSum2(int[] nums, int target)
        {
            var pairs = nums.Select((v, i) => (val: v, idx: i)).ToArray();

            Array.Sort(pairs, (a, b) => a.CompareTo(b));

            int l = 0;
            int r = nums.Length - 1;
            while (l < r)
            {
                int sum = pairs[l].val + pairs[r].val;
                if (sum == target)
                {
                    return new int[] { pairs[l].idx, pairs[r].idx };
                }
                else if (sum < target)
                {
                    l++;
                }
                else
                {
                    r++;
                }
            }

            return new int[] { 0 };
        }

        public static int BestTimeBuySellStock(int[] prices)
        {
            if (prices == null || prices.Length < 2) return 0;
            int maxSofar = 0;
            int result = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                int diff = prices[i] - prices[i - 1];
                maxSofar = Math.Max(0, maxSofar + diff);
                result = Math.Max(result, maxSofar);
            }

            return result;
        }

        public static int MaxProfit_Kadane(int[] prices)
        {
            if (prices == null || prices.Length < 2) return 0;
            int maxSofar = 0;
            int result = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                int diff = prices[i] - prices[i - 1];
                maxSofar = Math.Max(0, maxSofar + diff);
                result = Math.Max(result, maxSofar);
            }

            return result;
        }

        public static int BestTimeBuySellStock2(int[] prices)
        {
            if (prices == null || prices.Length < 2) return 0;
            int minPrice = prices[0];
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (minPrice > prices[i])
                {
                    minPrice = prices[i];
                }
                else
                {
                    maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
                }
            }

            return maxProfit;
        }

        public static int MaxProfit(int[] prices)
        {
            if (prices.Length < 2) return 0;

            int minPrice = int.MaxValue;
            int maxProfit = 0;

            foreach (var price in prices)
            {
                if (price < minPrice)
                {
                    minPrice = price;
                }
                else if (price - minPrice > maxProfit)
                {
                    maxProfit = price - minPrice;
                }
            }

            return maxProfit;
        }
    }

    public delegate int Mydelegate(int a, int b);

    public class DelegatProblem
    {
        static Func<int, int, int> d11 = Add;
        static Func<int, int, int> d22 = Mul;
        static Mydelegate d2 = Mul;
        static Mydelegate d1 = Add;
        public static void Run()
        {
            int a = 10;
            int b = 10;
            int res = d1(a, b);
            Console.WriteLine(res);

            Console.WriteLine(d11(a, b));
            Console.WriteLine(d22(a, b));
        }


        public static int Add(int a, int b) => a + b;
        public static int Mul(int a, int b) => a * b;


        public static void DefaultDelegate()
        {
            Mydelegate d = delegate (int a, int b) { return a * b; };
            Mydelegate d1 = (x, y) => x * y;
            Predicate<int> isEven = n => n % 2 == 0;

            Action notify = () => Console.WriteLine("Hello");
            notify += () => Console.WriteLine("Omid");
            notify += () => Console.WriteLine("From");
        }
    }


    public class SpanAndMemory
    {
        public static void Run()
        {
            int[] nums = { 1, 2, 3, 4 };
            Span<int> span = nums.AsSpan();

            

            Console.WriteLine($"{string.Join(",", span.ToArray())}");
        }
    }
}