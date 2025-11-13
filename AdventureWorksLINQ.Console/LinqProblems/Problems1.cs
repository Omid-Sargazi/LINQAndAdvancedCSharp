using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using AdventureWorksLINQ.Console.EFCore;
using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.LinqProblems
{
    public class LinqExamples2
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static async Task Execute()
        {

            System.Console.WriteLine("Adventure Works2019");

            var query = from p in db.Products
                        join
            plph in db.ProductListPriceHistories on
            p.ProductId equals plph.ProductId
                        where plph.EndDate == null
                        select new
                        {
                            ProductName = p.Name,
                            ListPrice = plph.ListPrice
                        };
            var res = query.AsNoTracking().ToList();
            foreach (var item in res)
            {
                System.Console.WriteLine($"{item.ProductName}+{item.ListPrice}");
            }

            var query2 = from p in db.Products
                         join plplh in db.ProductListPriceHistories
                         on p.ProductId equals plplh.ProductId
                         where plplh.EndDate == null
                         select new
                         {
                             ProductName = p.Name,
                             ListPrice = plplh.ListPrice
                         };

            var startDate = new DateTime(2014, 01, 01);
            var endDate = new DateTime(2015, 01, 01);

            var query3 = from p in db.People
                         join soh in db.SalesOrderHeaders
                         on p.BusinessEntityId equals soh.CustomerId
                         where soh.OrderDate >= startDate && soh.OrderDate <= endDate
                         group soh by new { p.LastName, p.FirstName } into g
                         select new
                         {
                             FullName = g.Key.LastName + "" + g.Key.FirstName,
                             OrderCount = g.Count()
                         };

            var startDate2 = new DateTime(2013, 1, 1);
            var endDate2 = new DateTime(2014, 1, 1);

            var query4 = from p in db.Products
                         join
            sod in db.SalesOrderDetails on p.ProductId equals sod.ProductId
                         join soh in db.SalesOrderHeaders on sod.SalesOrderId equals soh.SalesOrderId
                         where soh.OrderDate >= startDate2 && soh.OrderDate <= endDate
                         group sod by new { p.Name, p.ProductNumber } into g
                         select new
                         {
                             Name = g.Key.Name,
                             ProductNumber = g.Key.ProductNumber,
                             TotalCount = g.Sum(s => s.OrderQty)
                         };
            var result = query4.OrderByDescending(s => s.TotalCount).ToList();

            var page = 1;     // صفحه مورد نظر
            var pageSize = 50;
            var q1 = from p in db.Products
                     join sc in db.ProductSubcategories on
            p.ProductSubcategoryId equals sc.ProductSubcategoryId into scg
                     from sc in scg.DefaultIfEmpty()
                     join pc in db.ProductCategories on sc.ProductCategoryId equals pc.ProductCategoryId into cg
                     from pc in cg.DefaultIfEmpty()
                     where p.SellStartDate != null
                     orderby pc.Name, sc.Name, p.Name
                     select new
                     {
                         p.ProductId,
                         ProductName = p.Name,
                         SubcategoryName = sc != null ? sc.Name : null,
                         CategoryName = pc != null ? pc.Name : null

                     };
            var res2 = await q1.AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var q11 = db.Products.Where(p => p.SellStartDate != null)
            .OrderBy(p => p.ProductSubcategory.ProductCategory.Name)
            .ThenBy(p => p.ProductSubcategory.Name)
            .ThenBy(p => p.Name)
            .Select(p => new
            {
                p.ProductId,
                ProductName = p.Name,
                SubcategoryName = p.ProductSubcategory != null ? p.ProductSubcategory.Name : null,
                CategoryName = p.ProductSubcategory != null ? p.ProductSubcategory.ProductCategory.Name : null,
            }).AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize).ToList();


            var q13 = from c in db.ProductCategories
                      from sc in db.ProductSubcategories.DefaultIfEmpty()
                      select new
                      {
                          Category = c.Name,
                          SubCategory = sc != null ? sc.Name : null,
                          ProductCount = sc.Products.Where(p => p.SellStartDate != null).Count()
                      };

            var q14 = from c in db.ProductCategories
                      join sc in db.ProductSubcategories
                      on c.ProductCategoryId equals sc.ProductSubcategoryId into scg
                      from sc in scg.DefaultIfEmpty()
                      join p in db.Products on sc.ProductSubcategoryId equals p.ProductSubcategoryId into pg
                      from p in pg.DefaultIfEmpty()
                      where p == null || p.SellStartDate != null
                      group p by new { c.Name, SubCategory = sc != null ? sc.Name : null }
                         into g
                      select new
                      {
                          Category = g.Key.Name,
                          Subcategory = g.Key.SubCategory,
                          ProductCount = g.Count(x => x != null)
                      };

           
        }
    }
}