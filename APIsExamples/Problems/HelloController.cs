using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APIsExamples.Problems
{

    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello,World" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product{Id=100,Name="Laptop",Price=1200m},
            new Product{Id=101,Name="SmartWatch",Price=100m},
            new Product{Id=102,Name="IPad",Price=1300m},
            new Product{Id=103,Name="Lamp",Price=11m},
            new Product{Id=104,Name="Mouse",Price=10m},
        };
        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var product = _products.Where(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }
    }


    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}