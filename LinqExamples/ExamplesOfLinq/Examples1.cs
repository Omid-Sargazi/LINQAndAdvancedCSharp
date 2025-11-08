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

        }

        private static void EmployeeWithPage(List<Employee> employees,int pageNumber, int pageSize)
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