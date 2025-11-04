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

            string[] words = { "apple", "banana", "cat", "dog", "almost", "biopsy", "hello", "access", "cell", "door" };

            // IsAlphabetical("omid");
            // Console.WriteLine(IsAlphabetical("abc"));

            var res3 = words.Where(IsAlphabetical).OrderBy(o => o);
            Console.WriteLine($"{string.Join(",", res3)}");


            var products = new[]
            {
                new { Name = "Laptop", Price = 1200, Stock = 5 },
                new { Name = "Mouse", Price = 50, Stock = 20 },
                new { Name = "Keyboard", Price = 300, Stock = 8 },
                new { Name = "Monitor", Price = 800, Stock = 3 },
                new { Name = "Headphones", Price = 150, Stock = 15 },
                new { Name = "Tablet", Price = 600, Stock = 12 },
                new { Name = "Printer", Price = 400, Stock = 2 }
            };

            var res4 = products.Where(p => p.Price > 500 && p.Stock < 10)
            .Select(p => new { Name = p.Name, Price = p.Price }).OrderByDescending(p => p.Price);

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var res5 = numbers.Where(Fact);
            Console.WriteLine($"{string.Join(",", res5)}");


        }

        public static bool IsAlphabetical(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            for (int i = 1; i < word.Length; i++)
            {
                Console.WriteLine(word[i]);
                if (word[i] < word[i - 1])
                    return false;
            }

            return true;
        }

        public static bool Fact(int num)
        {
            int fact = 1;
            for (int i = 1; i <= num; i++)
            {
                fact *= i;
            }

            return fact<1000000;
        }
    }


    public class Personn
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Personn(string name, int age)
        {
            Age = age;
            Name = name;
        }
    }

   public struct Point
        {
            public int X;
            public int Y;
            public Point(int x, int y) => (X, Y) = (x, y);
        }
}