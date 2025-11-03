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


            var people = new[]
            {
                new { Name = "Ali", BirthDate = new DateTime(1990, 5, 15) },
                new { Name = "Sara", BirthDate = new DateTime(1985, 8, 22) },
                new { Name = "Reza", BirthDate = new DateTime(1995, 3, 10) },
                new { Name = "Fatemeh", BirthDate = new DateTime(1988, 12, 5) },
                new { Name = "Mohammad", BirthDate = new DateTime(1992, 7, 30) },
                new { Name = "Zahra", BirthDate = new DateTime(1982, 1, 18) },
                new { Name = "Hossein", BirthDate = new DateTime(1998, 9, 25) }
            };

            var today = DateTime.Today;

            var res2 = people.Select(p => new { Name = p.Name, Age = today.Year - p.BirthDate.Year - (today.DayOfYear < p.BirthDate.DayOfYear ? 1 : 0) })
            .GroupBy(p => (p.Age / 10) * 10).Select(g => new
            {
                AgeGroup = $"{g.Key}-{g.Key - 9}",
                Names = g.Select(p => p.Name).OrderBy(n => n)
            }).OrderBy(g => g.AgeGroup);




        }
    }
}