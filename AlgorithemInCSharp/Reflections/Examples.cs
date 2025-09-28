using System.Drawing;

namespace AlgorithemInCSharp.Reflections
{
    public class Example
    {
        public static void Run()
        {
            var person = new Person { Name = "Omid", Age = 42 };
            //================
            Type t = person.GetType();
            Type tt = typeof(Person);
            //================
            foreach (var prop in tt.GetProperties())
            {
                Console.WriteLine($"{prop.Name}");
            }

            foreach (var meth in tt.GetMethods())
            {
                Console.WriteLine($"{meth.Name}");
            }

            var sayHello = tt.GetMethod("SayHello");
            sayHello.Invoke(person,null);

            var obj = Activator.CreateInstance(tt);



            //================


            Console.WriteLine($"{t.Name}");
            Console.WriteLine($"{t.FullName}");
            //================
        }
    }




    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void SetAge(int age)
        {
            Age = age;
        }

        public static void SayHello()
        {
            Console.WriteLine($"Hello World!!!");
        }


        public static void PrintProperties(object obj)
        {
            var type = obj.GetType();

            foreach (var pro in type.GetProperties())
            {
                Console.WriteLine($"{pro.Name} = {pro.GetValue(obj)}");
            }
        }
    }
}