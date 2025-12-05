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

         public static void Problem04_ProductsPerCategory()
        {
            System.Console.WriteLine("\n=== تعداد محصولات هر دسته‌بندی ===");
            
            var categories = db.ProductCategories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    ProductCount = c.ProductSubcategories
                        .SelectMany(sc => sc.Products)
                        .Count()
                })
                .Where(c => c.ProductCount > 0)
                .OrderByDescending(c => c.ProductCount)
                .ToList();
            
            foreach (var cat in categories)
            {
                System.Console.WriteLine($"{cat.CategoryName}: {cat.ProductCount} محصول");
            }
        }

        // public static void Problem05_CustomersInCalifornia()
        // {
        //     System.Console.WriteLine("\n=== مشتریان کالیفرنیا ===");
            
        //     var customers = db.Customers
        //         .Where(c => c.Person != null && c.Person.EmailAddresses != null && 
        //                     c.Person.EmailAddresses.Any(e => e.EmailAddress1.EndsWith("@ca.com")))
        //         .Select(c => new
        //         {
        //             c.X,
        //             c.LastName,
        //             c.EmailAddress,
        //             Phone = c.Phone,
        //             City = c.Address.City
        //         })
        //         .OrderBy(c => c.LastName)
        //         .Take(15)
        //         .ToList();
            
        //     foreach (var cust in customers)
        //     {
        //         System.Console.WriteLine($"{cust.FirstName} {cust.LastName} - {cust.City} - {cust.EmailAddress}");
        //     }
        // }


        public static void Problem06_OrderDetailsWithProducts()
        {
            System.Console.WriteLine("\n=== جزئیات سفارشات با اطلاعات محصول ===");
            
            var orderDetails = db.SalesOrderDetails
                .Include(od => od.SalesOrder)
                .Include(od => od.ProductId)
                .Where(od => od.SalesOrder.OrderDate.Year == 2013)
                .Select(od => new
                {
                    OrderNumber = od.SalesOrder.SalesOrderNumber,
                    ProductName = od.ProductId,
                    od.OrderQty,
                    od.UnitPrice,
                    LineTotal = od.LineTotal,
                    OrderDate = od.SalesOrder.OrderDate
                })
                .OrderByDescending(od => od.OrderDate)
                .Take(20)
                .ToList();
            
            foreach (var detail in orderDetails)
            {
                System.Console.WriteLine($"{detail.OrderNumber} - {detail.ProductName} - {detail.OrderQty} x ${detail.UnitPrice:F2}");
            }
        }
    }
}