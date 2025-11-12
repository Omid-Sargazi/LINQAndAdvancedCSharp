using System.Security.Cryptography.X509Certificates;

namespace LinqProblems.Problems01
{
    public class LinqProblems01
    {
        public static void Execute()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Omid", Department = "sell", Salary = 50000 },
                new Employee { Id = 2, Name = "Saeed", Department = "Dev", Salary = 70000 },
                new Employee { Id = 3, Name = "Sara", Department = "sell", Salary = 55000 },
                new Employee { Id = 4, Name = "vahid", Department = "Extend", Salary = 80000 },
                new Employee { Id = 5, Name = "Miki", Department = "Backup", Salary = 45000 },
                new Employee { Id = 1, Name = "Didi", Department = "sell", Salary = 50000 },
                new Employee { Id = 2, Name = "TOM", Department = "Extend", Salary = 70000 },
                new Employee { Id = 3, Name = "Com", Department = "sell", Salary = 55000 },
                new Employee { Id = 4, Name = "POM", Department = "Extend", Salary = 80000 },
                new Employee { Id = 5, Name = "PID", Department = "Backup", Salary = 45000 },
                new Employee { Id = 6, Name = "Tok", Department = "Extend", Salary = 75000 }
            };
            var average = employees.Average(e => e.Salary);

            var res1 = employees.Where(e => e.Salary > employees.Average(e => e.Salary)).Select(e => new { e.Name, e.Salary });

            var res2 = employees.GroupBy(e => e.Department).Select(g => new
            {
                DepName = g.Key,
                NumsOfEmployee = g.Count(),
                AveSalary = g.Average(e => e.Salary),
                highSalary = g.Max(e => e.Salary)
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

            var duplicateEmployees2 = employeesWithDuplicates.GroupBy(e => e.Name).Where(g => g.Count() > 1).SelectMany(e => e);
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

            var result = GetPagedEmployees(employees, pageNumber: 1, pageSize: 2);

// Display results
            Console.WriteLine($"Page {result.PageNumber} of {result.TotalPages} (Total: {result.TotalCount} employees)");
            foreach (var emp in result.Data)
            {
                Console.WriteLine($"- {emp.Name} - {emp.Department} - {emp.Salary}");
            }


        }

        public static PagedResult<Employee> GetPagedEmployees(List<Employee> employees, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 1;

            var sortedEmployees = employees
            .OrderBy(emp => emp.Department)
            .ThenByDescending(emp => emp.Salary)
            .ToList();

            int totalCount = sortedEmployees.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Ensure pageNumber is within valid range
            pageNumber = Math.Min(pageNumber, totalPages);

            // Apply pagination using Skip and Take
            var pagedData = sortedEmployees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Employee>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = pagedData
            };




        }

        public class SalaryAnalysis
        {
            public static void AnalyzeSalaries(List<Employee> employees)
            {
                var salaryBand = employees
                .GroupBy(e => e.Salary switch
                {
                    < 55000 => "Low",
                    >= 55000 and <= 75000 => "Mid",
                    >= 75000 => "High",
                    _ => "Unknown"
                }).Select(g => new { Band = g.Key.Count = g.Count(), Employees = g }).OrderBy(e => e.Band);

                var departmentEquity = employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    MinSalary = g.Min(e => e.Salary),
                    MaxSalary = g.Max(e => e.Salary),
                    SalaryGap = g.Max(e => e.Salary) - g.Min(e => e.Salary),
                    EmployeeCount = g.Count(),
                    IsBalanced = (g.Max(e => e.Salary) - g.Min(e => e.Salary)) <= 15000,

                }).OrderBy(dep => dep.Department);

                var departmentRankings = employees
                .GroupBy(emp => emp.Department)
                .SelectMany(g => g.OrderByDescending(emp => emp.Salary).Select((emp, index) => new
                {
                    Department = g.Key,
                    Employee = emp,
                    Rank = index + 1
                })).OrderBy(x => x.Department).ThenBy(x => x.Rank);
                

            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }

    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}