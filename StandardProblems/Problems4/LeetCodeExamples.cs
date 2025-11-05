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
    }
}