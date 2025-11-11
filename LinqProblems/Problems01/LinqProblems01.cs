namespace LinqProblems.Problems01
{
    public class LinqProblems01
    {
        public static void Execute()
        {
            var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "علی", Department = "فروش", Salary = 50000 },
            new Employee { Id = 2, Name = "رضا", Department = "توسعه", Salary = 70000 },
            new Employee { Id = 3, Name = "سارا", Department = "فروش", Salary = 55000 },
            new Employee { Id = 4, Name = "نازنین", Department = "توسعه", Salary = 80000 },
            new Employee { Id = 5, Name = "محمد", Department = "پشتیبانی", Salary = 45000 }
        };
            var average = employees.Average(e => e.Salary);

            var res1 = employees.Where(e => e.Salary > employees.Average(e => e.Salary)).Select(e => new { e.Name, e.Salary });

            var res2 = employees.GroupBy(e => e.Department).Select(g => new
            {
                DepName = g.Key,
                NumsOfEmployee = g.Count(),
                AveSalary = g.Average(e => e.Salary),
                highSalary = g.Max(e=>e.Salary)
            });

            Console.WriteLine($"{string.Join(",", res2.Select(e => e.DepName))}");

            var departmentStatistics = employees.GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AverageSalary = Math.Round(g.Average(e => e.Salary), 2),
                MaxSalary = g.Max(e => e.Salary),
                MinSalary = g.Min(e => e.Salary),
                TotalSalary = g.Sum(e => e.Salary)
            }).OrderByDescending(dep => dep.AverageSalary);

        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }

}