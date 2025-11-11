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

            // Console.WriteLine($"{string.Join(",", res2.Select(e => e.DepName))}");

            var departmentStatistics = employees.GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AverageSalary = Math.Round(g.Average(e => e.Salary), 2),
                MaxSalary = g.Max(e => e.Salary),
                MinSalary = g.Min(e => e.Salary),
                TotalSalary = g.Sum(e => e.Salary)
            }).OrderByDescending(dep => dep.AverageSalary);

            var employeesWithDuplicates = new List<Employee>
            {
                new Employee { Id = 1, Name = "Omid", Department = "Sell", Salary = 50000 },
                new Employee { Id = 2, Name = "Saeed", Department = "Dev", Salary = 70000 },
                new Employee { Id = 3, Name = "Saeed", Department = "Marke", Salary = 60000 },
                new Employee { Id = 4, Name = "Omid", Department = "dev", Salary = 75000 },
                new Employee { Id = 5, Name = "Vahid", Department = "Backup", Salary = 48000 }
            };

            var duplicateEmployees = employeesWithDuplicates.GroupBy(e => e.Name)
            .Select(g => new { RepeatName = g.Key, Name = g.Key, Dept = g.Select(e => e.Department), Salary = g.Select(e => e.Salary) });

            var duplicateEmployees2 = employeesWithDuplicates.GroupBy(e => e.Name).Where(g => g.Count() > 1).SelectMany(e=>e);
            foreach (var item in duplicateEmployees2)
            {
                Console.WriteLine($"{item.Department}+{item.Name}+{item.Name}+{item.Salary}");

            }

            var employeesByName3 = employeesWithDuplicates.GroupBy(e => e.Name)
            .Where(g => g.Count() > 1)
            .Select(g => new
            {
                Name = g.Key,
                Employees = g.Select(e => $"Id:{e.Id}, Dept{e.Department},Salary:{e.Salary}"),
                Count = g.Count(),
            });

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