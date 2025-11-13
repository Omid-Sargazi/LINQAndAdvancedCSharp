using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.LinqProblems
{
    public class LinqExamples2
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static void Execute()
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
           
        }
    }
}