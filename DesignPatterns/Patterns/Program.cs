// See https://aka.ms/new-console-template for more information
using Patterns.StructuralPatterns;
using Patterns.StructuralPatterns.DivideAndConqure;

Console.WriteLine("Hello, World!!!!!!!");
// ClientAdaptee.Run();

int[] arr = new int[] { 70, 80, 1, 2, 3, 5, -5, -6, -7 };
Console.WriteLine(Problem1.Run(arr, 0, arr.Length - 1));
ClientComponent.Run();
