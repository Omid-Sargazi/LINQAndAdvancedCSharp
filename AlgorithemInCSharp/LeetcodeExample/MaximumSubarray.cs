namespace AlgorithemInCSharp.LeetcodeExample
{
    public class Problem
    {
        //nums = [-2,1,-3,4,-1,2,1,-5,4]
        public static int MaximumSubarray(int[] nums)
        {
            int currentSum = 0;
            int maxSum = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                currentSum = Math.Max(nums[i], currentSum + nums[i]);
                maxSum = Math.Max(maxSum, currentSum);
            }

            return maxSum;
        }

        public static int MaxProSubArray(int[] nums)
        {
            int minSofar = 0;
            int maxSofar = 0;
            int result = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                int current = nums[i];
                int temMax = Math.Max(current, Math.Max(maxSofar * current, minSofar * current));

                minSofar = Math.Max(current, Math.Min(maxSofar * current, minSofar * current));

                result = Math.Max(result, maxSofar);
            }

            return result;


        }

        public static int MaxProSubArray2(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;

            int maxProduct = nums[0];
            int currentMax = nums[0];
            int currentMin = nums[0];

            for (int i = 0; i < nums.Length; i++)
            {
                int num = nums[i];

                if (num < 0)
                {
                    int temp = currentMax;
                    currentMax = currentMin;
                    currentMin = temp;
                }

                currentMax = Math.Max(num, currentMax * num);
                currentMin = Math.Min(num, currentMin * num);

                maxProduct = Math.Max(maxProduct, currentMax);
            }

            return maxProduct;
        }

        public static int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;

            int currentSum = nums[0];
            int maxSum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                currentSum = MaxTwoNum(nums[i], nums[i] + currentSum);

                maxSum = MaxTwoNum(maxSum, currentSum);
            }

            return maxSum;
        }

        public static int MaxTwoNum(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
                return b;
        }
    }
}