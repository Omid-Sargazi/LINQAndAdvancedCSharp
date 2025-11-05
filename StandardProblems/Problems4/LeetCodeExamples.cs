using System.Globalization;

namespace StandardProblems.Problems4
{
    public class LeetCodeExamples
    {
        public static void MaximumSubarray(int[] arr)
        {
            int current = arr[0];
            int best = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                current = Math.Max(arr[i], current + arr[i]);
                best = Math.Max(best, current);
            }

            Console.WriteLine($"MaxSubArray:{best}");
        }

        public static void MaximumProfit(int[] prices)
        {
            int minPrice = prices[0];
            int maxProfit = prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                minPrice = Math.Min(prices[i], minPrice);
                maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
            }

            Console.WriteLine($"MaximumProfit: {maxProfit}");
        }

        public static void FindMaxNumber(int[] arr, int k)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                max = Math.Max(arr[i], max);
            }

            Console.WriteLine($"Max Number in an array:{max}");
        }

        public static void MaxSumAllPositive(int[] arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
        }

        public static void MaxSubarraySum(int[] arr)
        {
            int maxSofar = arr[0];
            int currentMax = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                currentMax = Math.Max(arr[i], arr[i] + currentMax);
                maxSofar = Math.Max(maxSofar, currentMax);
            }

            Console.WriteLine("Maximum Subarray Sum = " + maxSofar);
        }

        public static void MaxPositiveSubarrayLength(int[] arr)
        {
            int currentSum = 0;
            int maxLength = 0;
            int start = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                currentSum += arr[i];
                if (currentSum > 0)
                {
                    maxLength = Math.Max(maxLength, i - start + 1);
                }
                else
                {
                    currentSum = 0;
                    start = i + 1;
                }
            }

            Console.WriteLine("Longest positive subarray length = " + maxLength);
        }

        public static void MajorityElement(int[] nums)
        {
            int candidate = 0;
            int count = 0;

            foreach (var num in nums)
            {
                if (count == 0)
                {
                    candidate = num;
                }

                count += (num == candidate) ? 1 : -1;
            }

            Console.WriteLine($"candidate: {candidate}");
        }

        public static void FindSubArrayIncreasing()
        {
            var sales = new[]
            {
                new { Date = new DateTime(2024, 1, 15), Amount = 1000 },
                new { Date = new DateTime(2024, 1, 20), Amount = 1500 },
                new { Date = new DateTime(2024, 2, 5), Amount = 800 },
                new { Date = new DateTime(2024, 2, 18), Amount = 1200 },
                new { Date = new DateTime(2024, 3, 10), Amount = 2000 },
                new { Date = new DateTime(2024, 3, 25), Amount = 1800 },
                new { Date = new DateTime(2024, 1, 30), Amount = 900 }
            };

            var monthlySales = sales.GroupBy(s => new { s.Date.Year, s.Date.Month })
            .Select(g => new { YearMonth = $"{g.Key.Year}--{g.Key.Month:D2}", TotalAmount = g.Sum(s => s.Amount) }).OrderBy(x => x.YearMonth);
        }
        
        
    }
}