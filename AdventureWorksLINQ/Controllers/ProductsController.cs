using System.Diagnostics;
using AdventureWorksLINQ.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AdventureWorksLINQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;
        private readonly IMemoryCache _cache;
        private readonly IDistributedCache _distributedCache;
        public ProductsController(AdventureWorks2019Context context, IMemoryCache cache, IDistributedCache distributedCache)
        {
            _context = context;
            _cache = cache;
            _distributedCache = distributedCache;
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
            var stopwatch = Stopwatch.StartNew();
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
            stopwatch.Stop();
            return Ok(new
            {
                Data = topProducts,
                DurationMilliseconds = stopwatch.ElapsedMilliseconds,
            }

            );
        }
        [HttpGet("topselling/join/async")]
        public async Task<IActionResult> GetTopSellingProductsAsync()
        {
            var stopwatch = Stopwatch.StartNew();
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
            stopwatch.Stop();

            return Ok(new
            {
                Data = topProducts,
                DurationMilliseconds = stopwatch.ElapsedMilliseconds,
            });
        }

        [HttpGet("topselling/join/async/cached")]
        public async Task<IActionResult> GetTopSellingProductsCached()
        {
            var stopwatch = Stopwatch.StartNew();
            const string cacheKey = "TopSellingProducts";
            List<object>? result;

            if (!_cache.TryGetValue(cacheKey, out result))
            {
                result = await (from detail in _context.SalesOrderDetails
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
                          .ToListAsync<object>();
                _cache.Set(cacheKey, result, TimeSpan.FromMilliseconds(30));

            }

            stopwatch.Stop();

            return Ok(new
            {
                Source = result == null ? "DATABASE" : "CACHE",
                DurationMilliseconds = stopwatch.ElapsedMilliseconds,
                Data = result
            });
        }

        [HttpGet("topselling/redis")]
        public async Task<IActionResult> GetTopSellingProductsWithRedis()
        {
            const string cacheKey = "TopSellingProductsRedis";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                var resultFromCache = JsonSerializer.Deserialize<List<object>>(cachedData)!;
                return Ok(new
                {
                    Source = "REDIS_CACHE",
                    Data = resultFromCache
                });

            }
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
                             .ToListAsync<object>();

            var serializedData = JsonSerializer.Serialize(topProducts);

            await _distributedCache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMicroseconds(30)
            });

            return Ok(new
            {
                Source = "DATABASE",
                Data = topProducts
            });
            

        }

    }

}