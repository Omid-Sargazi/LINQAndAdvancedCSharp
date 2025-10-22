using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareProblem.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController:ControllerBase
    {
        public async Task<IActionResult> GetProducts()
        {
            await Task.Delay(200);
            return Ok (new { name = "Laptop" });
        }
    }
}