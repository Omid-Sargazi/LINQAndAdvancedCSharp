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
            int left = 0; int right = s.Length - 1;

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

        public static void RemoveDuplicateFromSortedArray(int[] arr)
        {
            int current = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != arr[current])
                {
                    current++;
                    arr[current] = arr[i];
                }
            }

            Console.WriteLine($"{string.Join(",", arr.Take(current + 1))}");
            Console.WriteLine($"{string.Join(",", arr)}");

        }
        
        public static int MaximumSubarray(int[] arr)
        {
            int currentSum = arr[0];
            int maxSum = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                currentSum = Math.Max(arr[i], currentSum + arr[i]);
                maxSum = Math.Max(maxSum, currentSum);
            }

            return maxSum;
        }
    }
}