using System.Reflection;

namespace ReflectionProblems.Problems
{
    public class Problem2
    {
        public static void Run()
        {
            Type stringType = typeof(string);
            Console.WriteLine($"Full Name: {stringType.FullName}");
            Console.WriteLine("--------------------");

            MethodInfo[] methods = stringType.GetMethods()
            .Where(m => m.IsPublic).OrderBy(m => m.Name).ToArray();

            Console.WriteLine("Public Methods");

            foreach (var method in methods)
            {
                Console.WriteLine($"--{method.ReturnType.Name} {method.Name}");
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }



    public class ClassGetTypes
    {
        public static void Run()
        {
            Type personType = typeof(Person);

            object personInsatnce = Activator.CreateInstance(personType);

            PropertyInfo nameProperty = personType.GetProperty("Name");
            PropertyInfo ageProperty = personType.GetProperty("Age");

            nameProperty.SetValue(personInsatnce, "Omid");
            ageProperty.SetValue(personInsatnce, 43);

            string name = (string)nameProperty.GetValue(personInsatnce);
            int age = (int)ageProperty.GetValue(personInsatnce);

            Console.WriteLine($"Name:{name}, Age:{age}");
        }
    }
}