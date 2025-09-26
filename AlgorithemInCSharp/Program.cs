// See https://aka.ms/new-console-template for more information
using AlgorithemInCSharp.LeetcodeExample;
using AlgorithemInCSharp.Sorting;
using AlgorithemInCSharp.Sorting2;

Console.WriteLine("Hello, World!");
int[] arr1 = new int[] { 1 };
int[] arr2 = new int[] { -1 };
int[] arr3 = new int[] { 0, 180, -80, 70, -70, 100, -100, -500, -1000 };
int[] arr4 = new int[] { 70, 60, 50, 40, 30, 20, 40, 45 };
int[] arr5 = new int[] { 1, 2, 3 };

// Sorting.Bubble(arr3);

// Sorting.Selection(arr3);
// Sorting.Insertion(arr3);

// Sorting.MergeSorting(arr3);
// Sorting.LomutoPartition(arr4,0,arr4.Length-1);
// QuickSort.Run(arr3,0,arr3.Length-1);    
// Tree.Run(arr5);
// HeapifySorting.Run(arr3);
// SortingHeapify.Run(arr4);
// SortArrays.Bubble(arr4);
// SortArrays.Selection(arr3);
// SortArrays.Insertion(arr4);

// SortArrays.MergeSort(arr4);
// SortArrays.QickSorting(arr4,0,arr4.Length-1);
// HeapExample.Run(arr3);
// HeapArraySorting.Run(arr3);
// KindOfSortArray.BubleSorting(arr3);
// KindOfSortArray.InsertionSorting(arr3);
int[] arr = {5, 2, 8, 1, 9, 3};
// var result = KindOfSortArray.MergeSorting(arr);

int[] arr6 = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
var result = Problem.MaxSubArray(arr6);
Console.WriteLine(result);

Console.WriteLine($"{Problem.MaxTwoNum(10, 10)}");

int[] arr7 = new int[] { 1, 2,3,-1,2 };

Console.WriteLine($"{LeetCodeProblem.MaxProSubArray(arr7)}");
Console.WriteLine($"{LeetCodeProblems.MaxSubArray(arr7)}");

// Console.WriteLine($"{string.Join(",",result)}");
// KindOfSortArray.QuickSorting(arr,0,arr.Length-1);

