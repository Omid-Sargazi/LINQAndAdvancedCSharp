namespace StandardProblems.Problems3
{
    public class LeetCodes01
    {
        public static int MaxProfit(int[] prices)
        {
            int minPrice = prices[0];
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (minPrice > prices[i])
                {
                    minPrice = prices[i];
                }

                int profit = prices[i] - minPrice;
                maxProfit = Math.Max(profit, maxProfit);
            }
            return maxProfit;
        }
    }
}