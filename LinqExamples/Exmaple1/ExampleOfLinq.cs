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
                new{Name="Saeed",Age=40},
                new{Name="Vahid",Age=36}
            };
            var sortedThenBy = people.OrderBy(p => p.Name)
            .ThenBy(p => p.Age);
        }
    }
}