using System.Reflection;

namespace SortingInCSharp.Reflections
{
    public class Car
    {
        public Car(){}
        public Car(string model){}
        public string Brand;
        public string Model { get; set; }
        public int Year { get; set; }
        public void Drive() { }
        public void Stop(){}
    }

    public class Reflection
    {
        public static void Run()
        {
            Type t = typeof(Car);
            MethodInfo[] methods = t.GetMethods();

            Console.WriteLine($"Type car is:{t.FullName}");
            foreach (var mth in methods)
            {
                Console.WriteLine(mth.Name);
            }

            PropertyInfo[] properties = t.GetProperties();
            foreach (var item in properties)
            {
                Console.WriteLine(item.Name);
            }

            foreach (var item in t.GetFields())
            {
                Console.WriteLine(item.Name);
            }

            foreach (var item in t.GetConstructors())
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}