// // // See https://aka.ms/new-console-template for more information
// // using LinqExamples.Exmaple1;
// // using LinqExamples.LeetCodeProblems;
// // using LinqExamples.SortingWithCSharp;

// // Console.WriteLine("Hello, World! and Sorting..");
// // // ExampleOfLinq.Run();

// // int[] arr = new int[] { 40, -40, 50, -50, 41, 10, 1, 2, 11 };
// // // BubbleSorting.Run(arr);
// // // SelectionSort.Run(arr);

// // // InsertionSort.Run(arr);

// // TwoSumProblem.RunType();


// using LinqExamples.DelegateProblems;

// Console.WriteLine("Hello, World! and Sorting..");
// Problem1 p1 = new Problem1();
// int val = 0;
// var res = p1.Run(3, 2, ref val);
// Console.WriteLine($"Mul: {res},Add: {val}");
// Console.WriteLine("Program");
// // var res = p1.Calculator(2, 3);
// // p1.Run();

// Problems2 p2 = new Problems2();
// p2.Run(100);

// Problem3 p3 = new Problem3();
// p3.PassDelegate(p3.Add);

// Problem4 p4 = new Problem4();
// p4.Execute(3, 4, (x, y) => x + y);

// Problem5 p5 = new Problem5();
// p5.Execute(5, 3, (x, y) => Console.WriteLine($"Sum{x+y}"));
// p5.Execute(5, 3, (x, y) => Console.WriteLine($"Mul{x * y}"));


// void AlertConsole(int temp) => Console.WriteLine($"Alert temp{temp}");
// void AlerLog(int temp) => Console.WriteLine($"[Log] High temp:{temp}");



// Thermometer t1 = new Thermometer();
// t1.OnTemperatureTooHigh += AlertConsole;
// t1.OnTemperatureTooHigh += AlerLog;

// t1.SetTemp(35);
// t1.SetTemp(29);

// Console.WriteLine("=============//=============");
// ClientTemrmometer client = new ClientTemrmometer();
// client.Run(40);

using LinqExamples.DelegateProblems;
using LinqExamples.Problem1;
using LinqExamples.Problems;
using LinqExamples.ReadAsyncParalel;

// ClientDelegate1.Run();
// FuncClient.Execute();
// ClientTemp clientTemp = new ClientTemp();
// clientTemp.Run(52);

// CreateFiles createFiles = new CreateFiles();
// createFiles.WriteFile();

// ClientParallelAsync clientParallelAsync = new ClientParallelAsync();
// await clientParallelAsync.Run();

// var admin = new AdminHandler();
// var manager = new ManagerHandler();
// var ceo = new CEOHandler();
// admin.SetNext(manager).SetNext(ceo);

// var req1 = new UserRequest { Title = "Manager", Context = "Some context" };
// var req2 = new UserRequest { Title = "Admin", Context = "Another content" };
// var req3 = new UserRequest { Title = "CEO", Context = "Important context" };
// var req4 = new UserRequest { Title = "Admin", Context = "Unrecognized context" };
// var req5 = new UserRequest { Title = "Guest", Context = "Some context" };

// admin.HandleRequest(req1);
// admin.HandleRequest(req2);
// admin.HandleRequest(req3);
// admin.HandleRequest(req4);
// admin.HandleRequest(req5);


LinqProblem1.Run();