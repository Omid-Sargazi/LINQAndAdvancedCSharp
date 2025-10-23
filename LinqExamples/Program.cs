// // See https://aka.ms/new-console-template for more information
// using LinqExamples.Exmaple1;
// using LinqExamples.LeetCodeProblems;
// using LinqExamples.SortingWithCSharp;

// Console.WriteLine("Hello, World! and Sorting..");
// // ExampleOfLinq.Run();

// int[] arr = new int[] { 40, -40, 50, -50, 41, 10, 1, 2, 11 };
// // BubbleSorting.Run(arr);
// // SelectionSort.Run(arr);

// // InsertionSort.Run(arr);

// TwoSumProblem.RunType();


using LinqExamples.DelegateProblems;

Console.WriteLine("Hello, World! and Sorting..");
Problem1 p1 = new Problem1();
int val = 0;
var res = p1.Run(3, 2, ref val);
Console.WriteLine($"Mul: {res},Add: {val}");
Console.WriteLine("Program");
// var res = p1.Calculator(2, 3);
// p1.Run();

Problems2 p2 = new Problems2();
p2.Run(100);

Problem3 p3 = new Problem3();
p3.PassDelegate(p3.Add);

Problem4 p4 = new Problem4();
p4.Execute(3, 4, (x, y) => x + y);

Problem5 p5 = new Problem5();
p5.Execute(5, 3, (x, y) => Console.WriteLine($"Sum{x+y}"));
p5.Execute(5, 3, (x, y) => Console.WriteLine($"Mul{x*y}"));