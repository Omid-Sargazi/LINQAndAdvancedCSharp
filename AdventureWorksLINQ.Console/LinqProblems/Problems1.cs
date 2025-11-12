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
            
        }
    }
}