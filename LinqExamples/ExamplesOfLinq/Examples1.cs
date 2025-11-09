namespace LinqExamples.ExamplesOfLinq
{
    public class ExamplesOfLinq
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

            ExamplesOfLinq.EmployeeWithPage(employees, 2, 1);
        }

        private static void EmployeeWithPage(List<Employee> employees, int pageNumber, int pageSize)
        {
            var ordered = employees.OrderBy(e => e.Department)
                .ThenByDescending(e => e.Salary);

            int totalCount = ordered.Count();
            int totalPages = (totalCount + pageSize - 1) / pageSize;

            int skip = (pageNumber - 1) * pageSize;
            var pageItems = ordered.Skip(skip).Take(pageSize).ToList();

            var result = new PagedResult<Employee>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = pageItems,
            };

            var employees2 = new List<Employee>
            {
                new Employee { Id = 1, Name = "Saeed", Department = "Sell", Salary = 50000 },
                new Employee { Id = 2, Name = "Omid", Department = "Develop", Salary = 70000 },
                new Employee { Id = 3, Name = "Sara", Department = "Sell", Salary = 55000 },
                new Employee { Id = 4, Name = "Boby", Department = "Develop", Salary = 80000 },
                new Employee { Id = 5, Name = "Sali", Department = "Backup", Salary = 45000 },
                new Employee { Id = 6, Name = "Maki", Department = "Develop", Salary = 75000 }
            };

            Console.WriteLine($"****************");

            var AllAverage = employees2.Average(e => e.Salary);
            var levelForEachEmployee = employees2.Select(e => new { 
                SalaryLevel = e.Salary < AllAverage ? "Below" : e.Salary > AllAverage ? "Above" : "Average", 
                Name = e.Name, 
                Salary = e.Salary 
            })
            .GroupBy(e => e.SalaryLevel)
            .Select(g => new { 
                Level = g.Key, 
                Count = g.Count(), 
                Employee = g.Select(e => e.Name) 
            }).ToList();

            foreach (var group in levelForEachEmployee)
            {
                Console.WriteLine($"Level:{group.Level},Count:{group.Count}");
                Console.WriteLine($"Employees:{string.Join(",", group.Employee)}");
                Console.WriteLine("----------------------------");
            }

            var students = new List<Student>
            {
                new Student { Name = "Ali", Score = 45 },
                new Student { Name = "Sara", Score = 90 },
                new Student { Name = "Reza", Score = 75 },
                new Student { Name = "Neda", Score = 82 },
                new Student { Name = "Hamed", Score = 60 }
            };

            var groupedStudents = students.Select(s => new { 
                Level = s.Score < 60 ? "Weak" : s.Score > 60 && s.Score < 80 ? "Average" : "Strong", 
                Name = s.Name, 
                Score = s.Score 
            })
            .GroupBy(s => s.Level)
            .Select(g => new { 
                g.Key, 
                Name = g.Select(s => s.Name), 
                Score = g.Select(s => s.Score) 
            }).ToList();

            var salaryBands = employees2.GroupBy(e => e.Salary switch
            {
                < 55000 => "Low",
                >= 55000 and <= 75000 => "Average",
                > 75000 => "High",
            })
            .Select(g => new { 
                Brand = g.Key, 
                Count = g.Count(), 
                Employee = g 
            })
            .OrderBy(x => x.Brand);

            var departmentEquity = employees2.GroupBy(e => e.Department)
            .Select(g => new
            {
                Dep = g.Key,
                Min = g.Min(e => e.Salary),
                Max = g.Max(e => e.Salary),
                SalaryGap = g.Max(e => e.Salary) - g.Min(e => e.Salary),
                EmployeeCount = g.Count(),
                IsBalanced = (g.Max(e => e.Salary) - g.Min(e => e.Salary)) <= 15000,
            })
            .OrderBy(d => d.Dep);

            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "Sell System", Department = "Sell", Budget = 100000, LeadEmployeeId = 1 },
                new Project { Id = 2, Name = "Mobile App", Department = "Develop", Budget = 200000, LeadEmployeeId = 4 },
                new Project { Id = 3, Name = "Backup Customer", Department = "Backup", Budget = 50000, LeadEmployeeId = 5 }
            };

            var employeeProjects = new List<EmployeeProject>
            {
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Lead" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 2, Role = "Developer" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 1, Role = "Tester" },
                new EmployeeProject { EmployeeId = 4, ProjectId = 2, Role = "Lead" },
                new EmployeeProject { EmployeeId = 5, ProjectId = 3, Role = "Lead" },
                new EmployeeProject { EmployeeId = 6, ProjectId = 2, Role = "Developer" }
            };

            // مشکل اصلی اینجا بود - پرانتز و کاما رو درست کردم
            var res1 = employeeProjects.Join(projects,
                e => e.ProjectId,
                p => p.Id,
                (e, p) => new { e.EmployeeId, e.Role, p.Name, p.Department, p.Budget }
            )
            .GroupBy(x => x.EmployeeId)
            .Join(employees2,
                g => g.Key,
                e => e.Id,
                (g, e) => new { 
                    EmployeeId = g.Key, 
                    EmployeeName = e.Name, 
                    Projects = g.Select(x => new { 
                        ProjectName = x.Name, 
                        Role = x.Role, 
                        Budget = x.Budget 
                    }).ToList() 
                }
            );

            var students2 = new List<Student>
            {
                new Student { Id = 1, Name = "Ali" },
                new Student { Id = 2, Name = "Sara" },
                new Student { Id = 3, Name = "Omid" }
            };

            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Physics" },
                new Course { Id = 3, Name = "Chemistry" }
            };

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1, Grade = 90 },
                new Enrollment { StudentId = 1, CourseId = 2, Grade = 85 },
                new Enrollment { StudentId = 2, CourseId = 2, Grade = 95 },
                new Enrollment { StudentId = 3, CourseId = 1, Grade = 70 },
                new Enrollment { StudentId = 3, CourseId = 3, Grade = 80 }
            };

            // اینجا هم مشکل syntax داشت
            var result2 = enrollments.Join(courses,
                e => e.CourseId,
                c => c.Id,
                (e, c) => new { e.Grade, e.StudentId, CourseName = c.Name }
            )
            .GroupBy(g => g.StudentId)
            .Join(students2,
                g => g.Key,
                s => s.Id,
                (g, s) => new { 
                    StudentName = s.Name, 
                    CourseName = g.Select(x => x.CourseName), 
                    Grade = g.Select(x => x.Grade) 
                }
            );
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
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

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Budget { get; set; }
        public int LeadEmployeeId { get; set; }
    }

    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string Role { get; set; } // "Lead", "Developer", "Tester"
    }
}