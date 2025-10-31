using System.ComponentModel.DataAnnotations;
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
    public class ProductController : ControllerBase
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

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsTwoController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product{Id=100,Name="Laptop",Price=1200m},
            new Product{Id=101,Name="SmartWatch",Price=100m},
            new Product{Id=102,Name="IPad",Price=1300m},
            new Product{Id=103,Name="Lamp",Price=11m},
            new Product{Id=104,Name="Mouse",Price=10m},
        };


        [HttpGet]
        public IActionResult GetProducts([FromQuery] string? Name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            IEnumerable<Product> filteredProducts = _products;

            if (!string.IsNullOrEmpty(Name))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice);
            }

            return Ok(filteredProducts.ToList());
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

            _products.Add(product);

            return CreatedAtAction(nameof(GetProducts), new { Id = product.Id }, product);
        }
    }


    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is Required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        [Range(0.01,double.MaxValue,ErrorMessage ="Price must be greater than 0")]
        public decimal Price { get; set; }
    }

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}