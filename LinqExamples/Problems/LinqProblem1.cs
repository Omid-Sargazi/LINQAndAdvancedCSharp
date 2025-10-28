namespace LinqExamples.Problems
{
    public class LinqProblem1
    {
        public static void Run()
        {
            int[] nums = { 5, 5, 4, 7, 8, 9, 4, 6, 2, 37, 4 };
            var res1 = nums.Where(n => n % 2 == 0).OrderByDescending(n => n);
            Console.WriteLine($"{string.Join(",", res1)}");


            List<string> students = new List<string>
            {
                "Ali", "Mohammad", "Ahmad", "Sara", "Amir", "Fatemeh", "Armin"
            };

            var res2 = students.Where(s => s.StartsWith("A")).OrderBy(s => s);


            var products = new[]

            {
                new{Name="Laptop",Price=2500},
                new{Name="Mouse",Price=3500},
                new{Name="Mouse",Price=3500},
                new{Name="Keyboard",Price=300},
                new{Name="Monitor",Price=2500},
                new{Name="Headphones",Price=3100},
                new{Name="Mouse2",Price=123},
            };

            var res3 = products.Where(p => p.Price > 1000).OrderByDescending(p => p.Name).Select(p => new { Name = p.Name, Price = p.Price });
            Console.WriteLine($"{string.Join(", ", res3)}");


            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var sumOdds = numbers.Where(n => n % 2 != 0).Count();
            var sumOdds2 = numbers.Where(n => n % 2 != 0).Sum();
            Console.WriteLine($"{string.Join(", ", sumOdds2)}");


            var students2 = new[]

            {
                new{Name="omid",Grade=100},
                new{Name="saeed",Grade=70},
                new{Name="vahid",Grade=60},
                new{Name="milad",Grade=40},
                new{Name="saleh",Grade=85},
                new{Name="sami",Grade=80},
            };

            var res4 = students2.Where(s => s.Grade > 85).Select(s => s.Name.ToUpper());



        }
    }
}