namespace LinqExamples.Exmaple1
{
    public class ExampleOfLinq
    {
        public static void Run()
        {
            var numbers = new List<int> { 1, 22, 3, 4, 5, 6, 7, 8, 99, 99, 101, -99, 88, 0, 0 };
            var doubled = numbers.Select(n => n * 2);
            Console.WriteLine(string.Join(",", doubled));


            var listOfLists = new List<List<int>>
            {
                new (){1,2,3},
                new List<int>{4,5,6},
                new List<int>{7,8,9}
            };

            var flattened = listOfLists.SelectMany(x => x);
            Console.WriteLine(string.Join(',', flattened));

            bool hasEven = numbers.Any(x => x % 2 == 0);
            Console.WriteLine($"Is There Even In Numbers:{hasEven}");


            var events = numbers.Where(n => n % 2 == 0).OrderDescending();
            Console.WriteLine(string.Join(",", events));

            var unique = numbers.Distinct();
            Console.WriteLine(string.Join(",", unique));//==============Distinct

            var firstTwo = numbers.Take(2);
            Console.WriteLine(string.Join(", ", firstTwo));

            var skipTwo = numbers.Skip(2);
            Console.WriteLine(string.Join(", ", skipTwo));

            var sorted = numbers.OrderBy(n => n);
            Console.WriteLine(string.Join(",", sorted));

            var sortedDesc = numbers.OrderByDescending(n => n);

            var people = new[]
            {
                new{Name="Omid",Age=43},
                new{Name="Omid",Age=41},
                new{Name="Omid",Age=40},
                new{Name="Saeed",Age=40},
                new{Name="Saeed",Age=40},
                new{Name="Vahid",Age=36}
            };
            var sortedThenBy = people.OrderByDescending(p => p.Name)
            .ThenBy(p => p.Age);
            Console.WriteLine(string.Join(",", sortedThenBy));

            var groups = people.GroupBy(p => p.Name);
            foreach (var group in groups)
            {
                Console.WriteLine($"Name,{group.Key}");
                foreach (var name in group)
                {
                    Console.WriteLine($"-  {name.Name}");
                }
            }

            var lookup = people.ToLookup(p => p.Age);
            foreach (var per in lookup[40])
            {
                Console.WriteLine(per);
            }


            var students = new[]
            {
                new {Id=1,Name="Omid"},
                new {Id=2,Name="Saeed"},
                new {Id=3,Name="Vahid"},
                new {Id=4,Name="Sara"},
                new {Id=5,Name="Kobi"},
            };

            var scores = new[]
            {
                new {StudentId=1,Score=20},
                new {StudentId=2,Score=30},
                new {StudentId=3,Score=40},
            };

            var joinResult = students.Join(scores,
                s => s.Id,
                sc => sc.StudentId,
                (s, sc) => new { s.Name, sc.Score }

            );

            foreach (var item in joinResult)
            {
                Console.WriteLine($"{item.Name},{item.Score}");
            }


            var leftJoinGroupJoin = students.GroupJoin(scores,
                s => s.Id,
                sc => sc.StudentId,
                (s, sc) => new { s.Name, Score = sc }
            );

            foreach (var item in leftJoinGroupJoin)
            {
                Console.WriteLine(item.Name + "," + string.Join(",", item.Score.Select(s => s.Score)));
            }

            var nums = new[] { 1, 2, 3 };
            var words = new[] { "one", "two", "three" };
            var zipped = nums.Zip(words, (n, w) => $"{n}-{w}");
            

        }
    }
}