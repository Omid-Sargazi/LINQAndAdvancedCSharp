// See https://aka.ms/new-console-template for more information
using SortingInCSharp;
using SortingInCSharp.Reflections;

Console.WriteLine("Hello, World!");
Reflection.Run();

int[] arr = new int[] { 10, 20, 30, 70, -70, -10, 1, 2 };
// BubbleSorting.Run(arr);
// SelectioSort.Run(arr);
int[] arr1 = new int[] { 1, 2, 3 };
// HeapSorting.Run(arr);
MaxPriorityQueue maxP = new MaxPriorityQueue();
maxP.Insert(1);
maxP.Insert(10);
// maxP.Insert(111);
// maxP.Insert(-12);
// maxP.Insert(-12);
// maxP.Insert(10000);
Console.WriteLine(maxP.ToString());