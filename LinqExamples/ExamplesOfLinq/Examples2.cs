namespace LinqExamples.ExamplesOfLinq
{
    public class Examples2
    {
        public static void Execute()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, Customer = "Alice" },
                new Order { Id = 2, Customer = "Bob" }
            };

            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, ProductId = 101, Qty = 2 },
                new OrderItem { OrderId = 1, ProductId = 102, Qty = 1 },
                new OrderItem { OrderId = 2, ProductId = 103, Qty = 3 }
            };

            var products = new List<Product>
            {
                new Product { Id = 101, Name = "Laptop", Price = 1000 },
                new Product { Id = 102, Name = "Mouse", Price = 25 },
                new Product { Id = 103, Name = "Keyboard", Price = 50 }
            };

            var result = orders.Join(orderItems,
            o => o.Id,

        oi => oi.OrderId,
        (o, oi) => new { CusName = o.Customer, orderId = o.Id, oi.Qty, oi.ProductId }

            ).Join(products, x => x.ProductId, p => p.Id, (x, p) => new { x.CusName, x.orderId, ProductName = p.Name, p.Price, x.Qty }).GroupBy(x => new { x.orderId, x.CusName }).Select(g => new
            {
                orderId = g.Key.orderId,
                Customer = g.Key.CusName,
                Items = g.Select(i => new { i.ProductName, i.Qty, LineTotal = i.Price * i.Qty }).ToList(),
                OrderQty = g.Sum(i => i.Qty * i.Price)
            }).ToList();


            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Ali" },
                new Student { Id = 2, Name = "Sara" },
                new Student { Id = 3, Name = "Omid" },
                 new Student { Id = 4, Name = "Sale" },
                new Student { Id = 5, Name = "Smi" },
                new Student { Id = 6, Name = "vahid" }
            };

            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Physics" },
                new Course { Id = 3, Name = "Chemistry" },
                 new Course { Id = 4, Name = "C#" },
                new Course { Id = 5, Name = "C++" },
                new Course { Id = 6, Name = "PHP" }
            };

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1, Grade = 90 },
                new Enrollment { StudentId = 1, CourseId = 2, Grade = 85 },
                new Enrollment { StudentId = 2, CourseId = 3, Grade = 95 },
                new Enrollment { StudentId = 3, CourseId = 1, Grade = 70 },
                new Enrollment { StudentId = 5, CourseId = 4, Grade = 80 },
                new Enrollment { StudentId = 5, CourseId = 1, Grade = 90 },
                new Enrollment { StudentId = 2, CourseId = 5, Grade = 85 },
                new Enrollment { StudentId = 1, CourseId = 2, Grade = 95 },
                new Enrollment { StudentId = 4, CourseId = 4, Grade = 70 },
                new Enrollment { StudentId = 6, CourseId = 6, Grade = 80 }
            };


            var res1 = students
            .Join(enrollments, s => s.Id, en => en.StudentId, (s, en) => new { StudentId = s.Id, StudentName = s.Name, en.CourseId, en.Grade })
            .Join(courses, x => x.CourseId, c => c.Id, (x, c) => new { x.StudentName, CourseName = c.Name, x.Grade })
            .GroupBy(x => x.StudentName)
            .Select(g => new { StudentName = g.Key, Courses = g.Select(c => new { c.CourseName, c.Grade }).ToList() })
            .ToList();

            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "سیستم فروش", Department = "فروش", Budget = 100000, LeadEmployeeId = 1 },
                new Project { Id = 2, Name = "اپ موبایل", Department = "توسعه", Budget = 200000, LeadEmployeeId = 4 },
                new Project { Id = 3, Name = "پشتیبانی مشتریان", Department = "پشتیبانی", Budget = 50000, LeadEmployeeId = 5 }
            };

            var employeeProjects = new List<EmployeeProject>
            {
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Lead" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 2, Role = "Developer" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 1, Role = "Tester" },
                new EmployeeProject { EmployeeId = 1, ProjectId = 2, Role = "Lead" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 3, Role = "Lead" },
                new EmployeeProject { EmployeeId = 6, ProjectId = 2, Role = "Developer" }
            };

            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "علی", Department = "فروش", Salary = 50000 },
                new Employee { Id = 2, Name = "رضا", Department = "توسعه", Salary = 70000 },
                new Employee { Id = 3, Name = "سارا", Department = "فروش", Salary = 55000 },
                new Employee { Id = 4, Name = "نازنین", Department = "توسعه", Salary = 80000 },
                new Employee { Id = 5, Name = "محمد", Department = "پشتیبانی", Salary = 45000 }
            };

            var employeyWithProjects = employees.Join(employeeProjects, e => e.Id, em => em.EmployeeId, (e, em) => new
            {
                Employee = e,
                EmployeeProject = em
            }).Join(projects, combine => combine.EmployeeProject.ProjectId, p => p.Id, (combine, p) => new
            {
                EmployeeId = combine.Employee.Id,
                EmployeeName = combine.Employee.Name,
                ProjectName = p.Name,
                Role = combine.EmployeeProject.Role,
                Department = p.Department
            }).GroupBy(x => new { x.EmployeeId, x.EmployeeName }).Select(g => new
            {
                EmployeeName = g.Key.EmployeeName,
                projects = g.Select(x => x.ProjectName)
            });

            var projectsRoles = projects.Join(employeeProjects, p => p.Id, e => e.ProjectId, (p, e) => new
            {
                p.Name,
                e.EmployeeId,
                e.ProjectId
            }).GroupBy(e => e.EmployeeId).Select(g => new { g.Key, Projects = g.Select(x => x.ProjectId).ToList() }).ToList();


            var projectTeams = projects
            .Join(employeeProjects, p => p.Id, emp => emp.ProjectId, (p, emp) => new { Project = p, EmployeeProject = emp })
            .Join(employees, combine => combine.EmployeeProject.EmployeeId, e => e.Id, (combine, e) => new
            {
                ProjectName = combine.Project.Name,
                EmployeeName = e.Name,
                Role = combine.EmployeeProject.Role,
                Department = combine.Project.Department
            }).GroupBy(x => x.ProjectName).Select(g => new
            {
                ProjectName = g.Key,
                TeamMembers = g.Select(m => $"{m.EmployeeName}({m.Role})").ToList()
            }).OrderBy(x => x.ProjectName);
            
        }
    }



    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
    }

    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    
    

}