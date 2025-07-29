using AdventureWorksLINQ.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;
        public ProductsController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        [HttpGet("active")]
        public IActionResult GetActiveProducts()
        {
            var result = _context.Products
            .Where(p => p.SellEndDate == null)
            .Select(p => new
            {
                p.Name,
                p.ProductNumber
            }).Take(10)
            .ToList();
            return Ok(result);

        }

        [HttpGet("by-subcategory/{subcategoryId:int}")]
        public IActionResult GetProductsBySubcategory(int subcategoryId)
        {
            var products = _context.Products
            .Where(p => p.ProductSubcategoryId == subcategoryId)
            .Include(p => p.ProductSubcategory)
            .ThenInclude(sc => sc.ProductCategory)
            .Select(p => new
            {
                p.Name,
                p.ProductNumber,
                Subcategory = p.ProductSubcategory!.Name,
                Category = p.ProductSubcategory.ProductCategory!.Name
            }).OrderBy(p => p.Name)
            .ToList();
            return Ok(products);
        }

        [HttpGet("orders/by-year/{year:int}")]
        public IActionResult GetOrdersByYear(int year)
        {
            var orders = _context.SalesOrderHeaders
            .Where(o => o.OrderDate.Year == year)
            .Include(o => o.Customer)
            .ThenInclude(c => c.Person)
            .Select(o => new
            {
                o.SalesOrderId,
                o.OrderDate,
                CustomerName = o.Customer.Person != null ? o.Customer.Person.FirstName + " " + o.Customer.Person.LastName : "N/A",
            })
            .OrderByDescending(o => o.OrderDate)
            .Take(20)
            .ToList();

            return Ok(orders);
        }

        [HttpGet("topselling/join/sync")]
        public IActionResult GetTopSellingProductsSync()
        {
            var topProducts = (from detail in _context.SalesOrderDetails
                               join product in _context.Products on detail.ProductId equals product.ProductId
                               group detail by new { product.ProductId, product.Name, product.ProductNumber } into g
                               select new
                               {
                                   g.Key.ProductId,
                                   g.Key.ProductNumber,
                                   TotalQuantity = g.Sum(g => g.OrderQty)
                               }
            ).OrderByDescending(p => p.TotalQuantity)
            .Take(10)
            .ToList();
            return Ok(topProducts);
        }
        [HttpGet("topselling/join/async")]
        public async Task<IActionResult> GetTopSellingProductsAsync()
        {
            var topProducts = await (from detail in _context.SalesOrderDetails
                                     join product in _context.Products on detail.ProductId equals product.ProductId
                                     group detail by new { product.ProductId, product.Name, product.ProductNumber } into g
                                     select new
                                     {
                                         g.Key.Name,
                                         g.Key.ProductNumber,
                                         TotalQuantity = g.Sum(d => d.OrderQty)
                                     })
                                     .OrderByDescending(p => p.TotalQuantity)
                                     .Take(10)
                                     .ToListAsync();

            return Ok(topProducts);
        }

    }

}