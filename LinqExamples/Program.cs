// See https://aka.ms/new-console-template for more information
using LinqExamples.Exmaple1;
using LinqExamples.SortingWithCSharp;

Console.WriteLine("Hello, World! and Sorting..");
// ExampleOfLinq.Run();

int[] arr = new int[] { 40, -40, 50, -50, 41, 10, 1, 2, 11 };
// BubbleSorting.Run(arr);
SelectionSort.Run(arr);