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


            string[] sentences =
            {
                "The quick brown fox jumps over the lazy dog",
                "LINQ is a powerful tool for C# developers",
                "Programming is fun and challenging",
                "Hello world from C# LINQ"
            };

            var res6 = sentences.Select(s=> new
            {
                Sentence=s,VowelCount=VowelsCount(s)
            }).OrderByDescending(p => p.VowelCount);
            // Console.WriteLine($"{string.Join(",", res6)}CountVowel");




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
            // Console.WriteLine($"{string.Join(",", res5)}");

            int[] numbers2 = { 1, 2, 3, 4, 5 };

            var res7 = numbers2.SelectMany(a => numbers2, (a, b) => new { A = a, B = b, Product = a * b }).
            Where(pair => pair.Product % 2 == 0).OrderBy(pair => pair.A).ThenBy(pair => pair.B);

            Console.WriteLine($"{string.Join(",", res7)}");

            string[] names = {"علی", "مریم", "رضا"};
            string[] families = { "محمدی", "احمدی", "کریمی" };

            var fullName = names.SelectMany(name => families, (name, family) => $"{name} {family}");


            string[] colors = {"قرمز", "آبی", "سبز"};
            string[] sizes = {"Small", "Medium", "Large"};
            decimal[] prices = { 29.99m, 39.99m, 49.99m };

            var productss = colors.SelectMany(c => sizes, (c, s) => new { Color = c, Size = s })
            .SelectMany(combo => prices, (combo, price) => new { Combo = combo, Price = price });


            string[] students = {"آرش", "نازنین", "کامیاب"};
            string[] courses = {"ریاضی", "فیزیک", "برنامه‌نویسی"};
            string[] grades = { "A", "B", "C" };

            var studentRecords = students.SelectMany(s => courses, (student, course) => new { Student = student, Course = course })
            .SelectMany(record => grades, (record, grade) => new
            {
                Student = record.Student,
                Course = record.Course,
                Grade = grade
            });

            var students2 = new[]
        {
                new { Name = "Ali", Grades = new[] { 18, 17, 19, 16 } },
                new { Name = "Sara", Grades = new[] { 15, 16, 14, 18 } },
                new { Name = "Reza", Grades = new[] { 19, 18, 20, 17 } },
                new { Name = "Fatemeh", Grades = new[] { 17, 16, 18, 15 } },
                new { Name = "Mohammad", Grades = new[] { 20, 19, 18, 17 } }
            };

            var res8 = students2.Where(s => s.Grades.Average() > 17 && s.Grades.All(g => g > 15)).Select(s => new { Name = s.Name, Aveg = s.Grades.Average() }).OrderBy(s => s.Name);


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

            return fact < 1000000;
        }
        
        public static int VowelsCount(string s)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(s))
            {
                foreach (char c in s)
                {
                    if (c == 'a' || c == 'e' || c == 'i' || c == 'u' || c == 'o')
                    {
                        count++;
                    }
                }
            }

            return count;
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