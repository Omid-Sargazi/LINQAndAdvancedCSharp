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
    }
}