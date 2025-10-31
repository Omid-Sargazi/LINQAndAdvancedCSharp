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


            var students3 = new List<Student>
            {
            new Student{Id=1,Name="Bob"},
            new Student{Id=2,Name="Mik"},
            new Student{Id=3,Name="Gorg"},
            };

            var enrollments = new List<Enrollment>
            {
                new Enrollment{CourseName="PHP",StudentId=1},
                new Enrollment{CourseName="C#",StudentId=2},
                new Enrollment{CourseName="C++",StudentId=3},
                new Enrollment{CourseName="C",StudentId=2},
                new Enrollment{CourseName="JAVA",StudentId=2},
                new Enrollment{CourseName="Golang",StudentId=1},
            };

            var leftJoin3 = students3.GroupJoin(enrollments,
             s => s.Id,

 e => e.StudentId,
 (s, es) => new { Student = s, Enrollements = es }

            ).SelectMany(x => x.Enrollements.DefaultIfEmpty(new Enrollment { CourseName = "Not Course" }),

             (a, b) => new { a.Student.Name, Course = b.CourseName }
            ).ToList();


            var products2 = new[]
            {
                new { Name = "Laptop", Category = "Electronics", Price = 1000 },
                new { Name = "Mouse", Category = "Electronics", Price = 50 },
                new { Name = "Shirt", Category = "Clothing", Price = 30 },
                new { Name = "Pants", Category = "Clothing", Price = 40 },
                new { Name = "Phone", Category = "Electronics", Price = 800 },
                new { Name = "Shoes", Category = "Clothing", Price = 60 }
            };

            var res16 = products2.GroupBy(g => g.Category).Select(g => new
            {
                Category = g.Key,
                Avg = g.Average(x => x.Price)
            }).OrderByDescending(p => p.Avg);

            int[] numbers5 = { 10, 5, 8, 20, 15, 25, 18 };

            var res18 = numbers5.OrderByDescending(x => x).Distinct().Skip(1).FirstOrDefault();


            string[] senetnces2 =
            {
                "LINQ is powerful",
                "C# programming language",
                "Entity Framework is an ORM",
                "ASP.NET Core is a web framework"
            };

            var res19 = senetnces2.SelectMany(s => s.Split(','))
            .OrderByDescending(word => word.Length)
            .FirstOrDefault();
            Console.WriteLine($"{string.Join(", ", res19)}");

            DateTime[] dates =
            {
                new DateTime(2024, 1, 1),  // دوشنبه
                new DateTime(2024, 1, 5),  // جمعه
                new DateTime(2024, 1, 6),  // شنبه
                new DateTime(2024, 1, 7),  // یکشنبه
                new DateTime(2024, 1, 12), // جمعه
                new DateTime(2024, 1, 13)  // شنبه
            };

            var res20 = dates.Where(d => d.DayOfWeek == DayOfWeek.Friday || d.DayOfWeek == DayOfWeek.Saturday)
            .OrderBy(d => d);
            Console.WriteLine($"{string.Join(", ", res20)}");

            var students4 = new[]
            {
                new { Name = "Ali", Grade = 18 },
                new { Name = "Sara", Grade = 16 },
                new { Name = "Reza", Grade = 19 },
                new { Name = "Fatemeh", Grade = 15 },
                new { Name = "Mohammad", Grade = 17 },
                new { Name = "Zahra", Grade = 20 }
            };
            double averageGrade = students4.Average(s => s.Grade);

            var countOfStudents = students4.Count();
            var sumOfGrades = students4.Sum(s => s.Grade);
            var midOfGrades = sumOfGrades / countOfStudents;
            var res21 = students4.Where(s => s.Grade > midOfGrades);
            var res22 = students4.Where(s => s.Grade > averageGrade).OrderByDescending(g => g);

            int[] numbers6 = { 5, 2, 9, 8, 3, 6, 1, 4, 7, 10 };
            var res24 = numbers.GroupBy(n => n % 2 == 0 ? "Even" : "Odd").OrderByDescending(g => g.Key);
            Console.WriteLine($"Even and odd grouping");
            foreach (var group in res24)
            {
                // Console.WriteLine($"{string.Join(",",res24)}"); 
                Console.WriteLine($"{group.Key}: {string.Join(",", group.OrderBy(x => x))}");
            }
            
            string[] words2 = { "radar", "hello", "level", "world", "madam", "civic", "test" };
            var res23 = words2.Where(w => w.Length > 0 && char.ToLower(w[0]) == char.ToLower(w[w.Length - 1])).OrderBy(w => w);

            var transactions = new[]

            {
                new { Description = "Salary", Amount = 2000 },
                new { Description = "Rent", Amount = -800 },
                new { Description = "Groceries", Amount = -150 },
                new { Description = "Bonus", Amount = 500 },
                new { Description = "Utilities", Amount = -120 },
                new { Description = "Freelance", Amount = 300 }
            };

            Console.WriteLine("Positive and negative transaction");

            var res25 = transactions.GroupBy(t => t.Amount > 0 ? "Posotive" : "Negative").OrderByDescending(t => t.Key);
            foreach (var group in res25)
            {
                Console.WriteLine($"{group.Key},{group.Sum(g => g.Amount)}");
            }

            int[] numbers7 = { 1, 2, 3, 2, 4, 1, 5, 3, 6, 2, 7, 1, 8, 3, 9 };
            // var res27 = numbers7.Distinct();
            var res27 = numbers7.GroupBy(n => n).Where(g => g.Count() > 1).OrderByDescending(g => g.Count()).ThenBy(g => g.Key);
            var res26 = numbers7.GroupBy(n => n);
            foreach(var group in res26)
            {
                Console.WriteLine($"{group.Key},Values:[{string.Join(",",group)}]");
            }
            Console.WriteLine($"{string.Join(",", res26)}");


            var products1 = new[]
            {
                new { Name = "Laptop", Category = "Electronics", Price = 1000 },
                new { Name = "Mouse", Category = "Electronics", Price = 50 },
                new { Name = "Keyboard", Category = "Electronics", Price = 120 },
                new { Name = "Shirt", Category = "Clothing", Price = 30 },
                new { Name = "Pants", Category = "Clothing", Price = 45 },
                new { Name = "Shoes", Category = "Clothing", Price = 80 },
                new { Name = "Phone", Category = "Electronics", Price = 800 }
            };


            var res28 = products1.GroupBy(p => p.Category).Select(g => new
            {
                Category = g.Key,
                Products = g.OrderByDescending(g => g.Price)
            }).OrderBy(g => g.Category);


            int[] numbers4 = { 1, 2, 3, 4, 5, 9, 16, 17, 25, 36, 49, 50, 64, 81, 100 };

            var res29 = numbers4.Where(n => Math.Sqrt(n) % 1 == 0).OrderBy(n => n); 









        }
    }


    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class Enrollment
    {
        public int StudentId { get; set; }
        public string CourseName { get; set; }
    }



}
