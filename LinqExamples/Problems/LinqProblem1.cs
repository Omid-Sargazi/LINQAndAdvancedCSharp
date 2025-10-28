namespace LinqExamples.Problems
{
    public class LinqProblem1
    {
        public static void Run()
        {
            int[] nums = { 5, 5, 4, 7, 8, 9, 4, 6, 2, 37, 4 };
            var res1 = nums.Where(n => n % 2 == 0).OrderByDescending(n => n);
            Console.WriteLine($"{string.Join(",", res1)}");
        }
    }
}