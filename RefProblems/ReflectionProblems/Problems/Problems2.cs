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

            var data = new Dictionary<string, object>
            {
                {"Name","Saeed"},
                {"Age",39 }
            };


            Type personType = typeof(Person);
            object personInsatnce2 = Activator.CreateInstance(personType);

            foreach (var item in data)
            {
                PropertyInfo propertyInfo = personType.GetProperty(item.Key);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(personInsatnce2, item.Value);
                }
            }


            object personInsatnce = Activator.CreateInstance(personType);

            PropertyInfo nameProperty = personType.GetProperty("Name");
            PropertyInfo ageProperty = personType.GetProperty("Age");

            Console.WriteLine($"Name:{nameProperty.GetValue(personInsatnce2)},Age:{ageProperty.GetValue(personInsatnce2)}");

            nameProperty.SetValue(personInsatnce, "Omid");
            ageProperty.SetValue(personInsatnce, 43);

            string name = (string)nameProperty.GetValue(personInsatnce);
            int age = (int)ageProperty.GetValue(personInsatnce);

            Console.WriteLine($"Name:{name}, Age:{age}");
        }
    }
}