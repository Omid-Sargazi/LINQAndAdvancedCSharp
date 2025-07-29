using System.Threading.Tasks;
using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.Product
{
    public class TopSellingProducts
    {
        private static readonly AdventureWorks2019Context _context = new AdventureWorks2019Context();
        public static void Run()
        {
            var topProducts = (from detail in _context.SalesOrderDetails
                               join product in _context.Products on detail.ProductId equals product.ProductId
                               group detail by new { product.ProductId, product.Name, product.ProductNumber } into g
                               select new
                               {
                                   g.Key.Name,
                                   g.Key.ProductNumber,
                                   TotalQuantity = g.Sum(d => d.OrderQty)

                               }
            ).OrderByDescending(p => p.TotalQuantity)
            .Take(10).ToList();
        }

        public static async Task RunAsync()
        {
            var topProducts = await (from detail in _context.SalesOrderDetails
                                     join product in _context.Products on detail.ProductId equals product.ProductId
                                     group detail by new { product.ProductId, product.Name, product.ProductNumber } into g
                                     select new
                                     {
                                         g.Key.Name,
                                         g.Key.ProductNumber,
                                         TotalQuantity = g.Sum(d => d.OrderQty)
                                     }
                            ).OrderByDescending(p => p.TotalQuantity)
                            .Take(10)
                            .ToListAsync();
        }
    }
}