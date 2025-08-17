// See https://aka.ms/new-console-template for more information
using SortingInCSharp;
using SortingInCSharp.Reflections;

Console.WriteLine("Hello, World!");
Reflection.Run();

int[] arr = new int[] { 10, 20, 30, 70, -70, -10, 1, 2 };
BubbleSorting.Run(arr);