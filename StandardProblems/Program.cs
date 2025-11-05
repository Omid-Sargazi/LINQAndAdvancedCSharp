// See https://aka.ms/new-console-template for more information
using StandardProblems.LINQProblems;
using StandardProblems.Problems;
using StandardProblems.Problems2;
using StandardProblems.Problems3;
using StandardProblems.Problems4;

Console.WriteLine("Hello, World!");
// Problem1.Run();
// ClientList.Run();

// LinqExamples.Run();

var nums = new int[] { 7, 6, 4, 3, 1 };
var nums2 = new int[] { 7,1,5,3,6,4 };
// Console.WriteLine(LeetCodes01.MaxProfit(nums2));
var nums3 = new int[] { 1, 1, 2, 2, -3, 4, 4, 5, 6, 7, 8 };
// LeetCodes01.RemoveDuplicateFromSortedArray(nums3);

LeetCodeExamples.MaximumSubarray(nums3);