namespace StandardProblems.LINQProblems
{
    public class LinqExamples
    {
        public static void Run()
        {
            Console.WriteLine("LINQ==========");

            var orders = new[]

            {
                new {Customer="Omid",Amount=100},
                new {Customer="Omid",Amount=1000},
                new {Customer="Sa",Amount=110},
                new {Customer="Va",Amount=1100},
                new {Customer="Sa",Amount=1200},
                new {Customer="Sa",Amount=10},
                new {Customer="Va",Amount=12},
                new {Customer="Jin",Amount=40},
                new {Customer="Jin",Amount=140},
            };

            var res1 = orders.GroupBy(c => c.Customer).
            Select(g => new { Name = g.Key, Amount = g.Sum(o => o.Amount) }).
            Where(a => a.Amount > 1000).OrderByDescending(o => o);

            


        }
    }
}