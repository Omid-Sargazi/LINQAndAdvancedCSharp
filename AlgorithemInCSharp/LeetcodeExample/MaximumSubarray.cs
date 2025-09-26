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
    }
}