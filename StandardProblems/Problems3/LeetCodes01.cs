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

        public static bool Palindrome(string s)
        {
            int left = 0;int right = s.Length - 1;

            while (left < right)
            {
                while (left < right && char.IsLetterOrDigit(s[left])) left++;
                while (left < right && char.IsLetterOrDigit(s[right])) right--;

                if (char.ToLower(s[left]) != char.ToLower(s[right])) return false;

                left++;
                right--;
            }

            return true;
        }
    }
}