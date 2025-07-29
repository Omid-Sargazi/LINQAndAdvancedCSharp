using AdventureWorksLINQ.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksLINQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
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
    }

}