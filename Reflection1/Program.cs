// See https://aka.ms/new-console-template for more information
using Reflection1.Problems;
using Reflection1.Problems2;

Console.WriteLine("Hello, World!");
// var u1 = new User();

// var p1 = new Person2();
// var serialize = new RecursiveJsonSerializer();

// string json = serialize.ToJson(p1);
// Console.WriteLine(json);

// var res = SimpleJsonSerializer.ToJson(u1);
// Console.WriteLine(res);

BB b = new BB();
// ReflectionProblem1.Run(b);
ClientReflection.Run(b);