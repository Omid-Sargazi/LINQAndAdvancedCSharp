using Microsoft.AspNetCore.Mvc;
using ProductsService.Models;

namespace ProductsService.Controllers
{
   
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private static List<Product> items = new();
    private static int id=0;
    [HttpGet] public IEnumerable<Product> GetAll()=>items;

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = items.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost] public Product Create(Product dto){ dto.Id=++id; items.Add(dto); return dto; }
}
}