using System.Linq.Expressions;
using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace AdventureWorksLINQ.Console.IqueryableProblem
{
    public class Iqueryable1
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static void Run()
        {
            int page = 1;
            int pageSize = 10;
            bool desc = false;
            Execute(page, pageSize, false);
        }

        private static void Execute(int page, int pageSize, bool desc)
        {
            if (page < 1) page = 1;

            if (pageSize <= 0) pageSize = 10;

            var source = db.Products.AsNoTracking();

            var ordered = desc ? source.OrderByDescending(p => p.Name) : source.OrderBy(p => p.Name);

            var totalCount = ordered.Count();

            var pagedQuery = ordered.Skip((page - 1) * pageSize)
            .Take(pageSize);

            string sql = pagedQuery.ToQueryString();
            System.Console.WriteLine("===SQL===");
            System.Console.WriteLine(sql);

            var items = pagedQuery.Select(p => new { p.ProductId, p.Name }).ToList();
            System.Console.WriteLine($"=== Page {page} / Size {pageSize} | Total {totalCount} ===");
            foreach (var it in items)
            {
                System.Console.WriteLine($"{it.ProductId}--{it.Name}");
            }


            var q1 = Executee(
                source: db.Products.AsNoTracking(),
                keySelector: (AdventureWorksLINQ.Console.Models.Product p) => p.Name,
                descending: desc,
                page: page,
                pageSize: pageSize
            );

            System.Console.WriteLine("=== SQL(Product/Name) ===");
            System.Console.WriteLine(q1.ToString());

            var items1 = q1.Select(p => new { p.ProductId, p.Name }).ToList();
            System.Console.WriteLine("=== Results ===");
            foreach (var it in items1) System.Console.WriteLine($"{it.ProductId} - {it.Name}");




        }

        private static IQueryable<T> Executee<T, TKey>(IQueryable<T> source, Expression<Func<T, TKey>> keySelector, int page, int pageSize, bool descending)
        {
            if (page < 1) page = 1;

            if (pageSize <= 0) pageSize = 10;
            if (pageSize >= 100) pageSize = 100;

            var ordered = descending ? source.OrderByDescending(keySelector) :
            source.OrderBy(keySelector);

            var paged = ordered.Skip((page - 1) * pageSize).Take(pageSize);

            return paged;


        }


        public static void MaximumProductSubarray(int[] arr)
        {
            int n = arr.Length;
            int[] res = new int[n];

            int left = 1;
            res[0] = 1;
            for (int i = 1; i < n; i++)
            {
                left = left * arr[i - 1];
                res[i] = left;
            }

            int right = 1;
            for (int end = n - 1; end >= 0; end--)
            {
                res[end] = right * res[end];
                right = right * arr[end];
            }

            System.Console.Write($"{string.Join(",", res)}");
        }


        public static void Query1()
        {
            var query = db.People.Where(p => p.FirstName == "Admin");

            var query2 = db.People.Where(p => p.FirstName.StartsWith("A")).Select(p => p.LastName.StartsWith("B"));

            var query3 = db.People.Where(p => p.FirstName.StartsWith("A"))
            .Select(p => p.Customers).ToList();

            var highlyPaidEmployees = db.Employees.Where(e => e.Gender == "Man");

            var redProducts = db.Products.Where(p => p.Color == "Red").ToList();


            // foreach(var i in query3)
            // {
            //     foreach(var ii in i)
            //     {

            //     }
            // }

            var order2014 = db.SalesOrderHeaders.Where(o => o.OrderDate.Year == 214).Select(o => o.CustomerId == 1).ToList();

            var customersByName = db.People.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);

            //  foreach(var i in order2014)
            // {
            //     System.Console.WriteLine(i);
            // }

            var productsByPriceDesc = db.Products.OrderByDescending(p => p.ListPrice);
            // var employeesByHireDate = db.Employees.ToList().OrderBy(e => e.HireDate).ToList();

            // var ordersByTotalDue = db.SalesOrderHeaders.ToList().OrderByDescending(s => s.TotalDue).Select(s => s.CustomerId == 1).ToList();

            var productByColor = db.Products
            .GroupBy(p => p.Color)
            .Select(g => new { g.Key, Count = g.Count() });

            IQueryable<ColorGroup> productByColor2 = db.Products
            .GroupBy(p => p.Color)
            .Select(g => new ColorGroup { Color = g.Key, Count = g.Count() });

            // foreach (var i in productByColor2)
            // {
            //     System.Console.WriteLine(i.Color + " " + i.Count);
            // }

            var avgPriceBySubcategory = db.Products
            .Where(p => p.ProductSubcategoryId != null)
            .GroupBy(p => p.ProductSubcategoryId)
            .Select(g => new { g.Key, orderCount = g.Count() });

            // foreach (var i in avgPriceBySubcategory)
            // {
            //     System.Console.WriteLine(i.Key + " " + i.orderCount);
            // }

            var ordersByYear = db.SalesOrderHeaders
            .GroupBy(soh => soh.OrderDate.Year)
            .Select(g => new { g.Key, orderCount = g.Count() });


            var employeesByDepartment = db.EmployeeDepartmentHistories
            .Where(edh => edh.EndDate == null)
            .GroupBy(edh => edh.Department.Name)
            .Select(g => new { g.Key, EmployeeCount = g.Count() }).ToList();


            var productsWithCategory = db.Products
            .Join(db.ProductSubcategories,
                p => p.ProductSubcategoryId,
                ps => ps.ProductCategoryId,
                (p, ps) => new { productname = p.Name, subCategory = ps.Name }
            ).Select(s => s.subCategory).ToList();

            var ordersWithCustomer = db.SalesOrderHeaders
            .Join(db.Customers,
            o => o.CustomerId,
            c => c.CustomerId,
            (o, c) => new { OrderId = o.SalesOrderId, Customername = c.Person.FirstName + " " + c.Person.LastName }
            );


            var productsWithVendor = db.Products
            .Join(db.ProductVendors,
            p => p.ProductId,
            pv => pv.ProductId,
            (p, pv) => new { ProductName = p.Name, VendorName = pv.Product }
            );

            var totalProducts = db.Products.ToList().Count();

            var averagePrice = db.Products.Average(p => p.ListPrice);

            var uniqueCustomers = db.SalesOrderHeaders.Select(soh => soh.CustomerId).Distinct().Count();

            var q1 = db.Products.AsNoTracking()
            .Where(p => p.Name.StartsWith("A"))
            .OrderBy(p => p.Name)
            .Select(p => new { p.Name, p.ProductId })
            .Take(5).ToList();





        }
    }

    public class ColorGroup
    {
        public string Color { get; set; }
        public int Count { get; set; }
    }
}