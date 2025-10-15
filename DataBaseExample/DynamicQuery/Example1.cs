using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataBaseExample.DynamicQuery
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class ClientExpresion
    {
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Name = "Ali", Age = 25 },
                new Person { Name = "Sara", Age = 35 },
                new Person { Name = "Reza", Age = 40 }
            };
            
            // 2) ساخت Expression: x => x.Age > 30

            var param = Expression.Parameter(typeof(Person), "x");
            var prop = Expression.Property(param, "Age");
            var constant = Expression.Constant(30, typeof(int));
            var body = Expression.GreaterThan(prop, constant);
            var lambda = Expression.Lambda<Func<Person, bool>>(body, param);

            Console.WriteLine("Expression tree:");
            Console.WriteLine(lambda);

            var func = lambda.Compile();
            Console.WriteLine("\nCompile + invoke on single item:");
            Console.WriteLine($"Sara -> {func(new Person { Age = 35 })}"); // true

            var queryable = people.AsQueryable().Where(lambda);

            Console.WriteLine("\nResult of queryable.Where(lambda).ToList():");
            
            foreach (var p in queryable.ToList())
                Console.WriteLine($"{p.Name} ({p.Age})");
        }
    }

}