namespace LinqProblems.Problems01
{
    public class LinqProblems
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

            var res1 = employees.Where(e => e.Salary > employees.Average(e => e.Salary)).Select(e=>new{e.Name,e.Salary});

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