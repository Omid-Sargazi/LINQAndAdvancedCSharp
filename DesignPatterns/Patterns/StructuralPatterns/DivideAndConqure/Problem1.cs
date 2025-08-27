namespace Patterns.StructuralPatterns.DivideAndConqure
{
    public class Problem1
    {
        public static int Run(int[] arr,int start,int end)
        {
            if (start == end) return arr[start];

            int mid = (end + start) / 2;

            int maxLeft = Run(arr, start, mid);
            int maxRight = Run(arr, mid+1, end);

            return Math.Max(maxLeft, maxRight);
            
            Console.WriteLine(string.Join(",", arr));
        }
    }
}