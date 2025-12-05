using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.LinqProblems
{
    public class Problems05
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static void Execute()
        {
            
        }
        

        public static void Problem01_EmployeesInDepartment()
        {
            
                System.Console.WriteLine("\n=== کارمندان دپارتمان Engineering (فعلی) ===");
            
            // روش 1: با Join مستقیم
            var employees = db.Employees
                .Join(db.EmployeeDepartmentHistories.Where(edh => edh.EndDate == null), // کارمندان فعلی
                    e => e.BusinessEntityId,
                    edh => edh.BusinessEntityId,
                    (e, edh) => new { e, edh })
                .Join(db.Departments.Where(d => d.Name == "Engineering"),
                    combined => combined.edh.DepartmentId,
                    d => d.DepartmentId,
                    (combined, d) => new
                    {
                        combined.e,
                        Department = d
                    })
                .Select(x => new
                {
                    x.e.BusinessEntityId,
                    FirstName = x.e.BusinessEntity.FirstName,
                    LastName = x.e.BusinessEntity.LastName,
                    x.e.JobTitle,
                    x.e.HireDate,
                    DepartmentName = x.Department.Name,
                    DepartmentGroup = x.Department.GroupName
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(20)
                .ToList();
            
            System.Console.WriteLine($"تعداد کارمندان Engineering: {employees.Count}");
            foreach (var emp in employees)
            {
                System.Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                System.Console.WriteLine($"  دپارتمان: {emp.DepartmentName} ({emp.DepartmentGroup})");
                System.Console.WriteLine($"  تاریخ استخدام: {emp.HireDate:yyyy-MM-dd}");
                System.Console.WriteLine();
            }
        }

        public static void Problem02_ProductsByPriceRange()
        {
            var products = db.Products
            .Where(p=>p.ListPrice>=100&&p.ListPrice<=500)
            .Select(p => new
            {
                p.Name,
                p.ListPrice,
                p.ProductNumber,
                p.Color
            })
            .OrderByDescending(p=>p.ListPrice)
            .Take(15)
            .ToList();

            foreach (var p in products)
            {
                System.Console.WriteLine($"{p.Name} - ${p.ListPrice:F2} - {p.Color}");
            }
        }


        public static void Problem03_RecentOrders()
        {
            System.Console.WriteLine("\n=== آخرین 10 سفارش ===");
            
            var orders = db.SalesOrderHeaders
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new
                {
                    o.OrderDate,
                    o.CustomerId,
                    o.SalesOrderNumber,
                    o.TotalDue,
                    o.Status
                })
                .Take(10)
                .ToList();
            
            foreach (var order in orders)
            {
                System.Console.WriteLine($"{order.OrderDate:yyyy-MM-dd} - {order.SalesOrderNumber} - ${order.TotalDue:F2}");
            }
        }
    }
}