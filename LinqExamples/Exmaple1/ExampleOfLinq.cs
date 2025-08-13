namespace LinqExamples.Exmaple1
{
    public class ExampleOfLinq
    {
        public static void Run()
        {
            var numbers = new List<int> { 1, 22, 3, 4, 5, 6, 7, 8, 99 };
            var doubled = numbers.Select(n => n * 2);
            Console.WriteLine(string.Join(",", doubled));


            var listOfLists = new List<List<int>>
            {
                new List<int>{1,2,3},
                new List<int>{4,5,6},
                new List<int>{7,8,9}
            };

            var flattened = listOfLists.SelectMany(x => x);
            Console.WriteLine(string.Join(',', flattened));
        }
    }
}