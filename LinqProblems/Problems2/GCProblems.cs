using System.Diagnostics;

namespace LinqProblems.Problems2
{
    public class GCProblems
    {
        public static void Execute()
        {
             const int N = 1_000_000;
        var sw = Stopwatch.StartNew();
            int before = GC.CollectionCount(0);
            Console.WriteLine($"Before{before}");

        for (int i = 0; i < N; i++)
        {
            var b = new byte[256]; // baseline allocation
            b[0] = 1;
        }

            int after = GC.CollectionCount(0);
            Console.WriteLine($"After:{after}");
        sw.Stop();
        Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms, Gen0: {after - before}");
        }
    }
}