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

            string[] words = { "apple", "cat", "banana", "elephant", "dog", "zebra" };

            var res5 = words.Select(w => w.Length).OrderBy(w => w);
            var res6 = words.OrderBy(w => w.Length).ThenBy(w => w);
            // Console.WriteLine($"{string.Join(", ", res6)}");

            int[] numbers3 = { -5, 2, 0, -8, 3, -1, 0, 7, -2, 4 };

            var res7 = numbers3.GroupBy(n => n > 0 ? "Positive" : n < 0 ? "negative" : "Zero").
                Select(g => new { Type = g.Key, Count = g.Count() });
            Console.WriteLine($"{string.Join(", ", res7)}");

            string[] sentences =
            {
                "Hello world",
                "LINQ is awesome",
                "Hello LINQ",
                "C# programming"
            };

            var res8 = sentences.SelectMany(w => w.Split(','))
            .Select(w => w.Trim()).Distinct();
            var res9 = sentences.Select(s => s.Split(' '))
            .Aggregate((a, b) => a.Union(b).ToArray());

            var people = new[]
{
                new { Name = "Ali", Age = 25 },
                new { Name = "Sara", Age = 30 },
                new { Name = "Reza", Age = 25 },
                new { Name = "Fatemeh", Age = 35 },
                new { Name = "Mohammad", Age = 30 },
                new { Name = "Zahra", Age = 25 }
            };

            var res10 = people.GroupBy(p => p.Age).Select(p => new { Age = p.Key, Count = p.Count() });
            Console.WriteLine($"{string.Join(", ", res10)}");

            int[] list1 = { 1, 3, 5, 7, 9 };
            int[] list2 = { 2, 3, 5, 8, 10 };

            var res11 = list1.Intersect(list2).OrderByDescending(l => l);
            var res12 = list1.Join(list2,
            n1 => n1,
            n2 => n2,
            (n1, n2) => n1
            ).OrderBy(n => n);

            var customers = new[]

            {
                new {Id=1,Name="Bob"},
                new {Id=2,Name="Mik"},
                new {Id=3,Name="Sara"},
                new {Id=4,Name="Jira"},
            };

            var orders = new[]

            {
                new{Id=101,CustomerId=1,Amount=250m},
                new{Id=102,CustomerId=3,Amount=250m},
                new{Id=103,CustomerId=2,Amount=250m},
                new{Id=104,CustomerId=2,Amount=250m},
                new{Id=104,CustomerId=5,Amount=250m},
            };

            var res13 = customers.Join(orders,

                c => c.Id,
                o => o.CustomerId,
                (c, o) => new { CustomerName = c.Name, OrderId = o.Id, o.Amount }
            );

            Console.WriteLine($"{string.Join(", ", res13)}");

            var leftJoin = customers.GroupJoin(orders,
                c => c.Id,

o => o.CustomerId,
(c, ords) => new { Customer = c, Orders = ords }

            ).SelectMany(x => x.Orders.DefaultIfEmpty(new { Id = 0, CustomerId = 0, Amount = 0m }),
            (x, o) => new { CustomerName = x.Customer.Name, OrderId = o.Id, o.Amount });
            
            Console.WriteLine("Left join");
            Console.WriteLine($"{string.Join(", ", leftJoin)}");


            var leftJoin2 = from c in customers
                            join o in orders on c.Id equals o.CustomerId into ords
                            from o in ords.DefaultIfEmpty()
                            select new { CustomerName = c.Name, OrderId = o == null ? 0 : o.Id, Amount = o == null ? 0m : o.Amount };



            







        }
    }
}