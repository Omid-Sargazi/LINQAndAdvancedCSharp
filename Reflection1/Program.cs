// See https://aka.ms/new-console-template for more information
using Reflection1.Problems;

Console.WriteLine("Hello, World!");
var u1 = new User();

var res = SimpleJsonSerializer.ToJson(u1);
Console.WriteLine(res);