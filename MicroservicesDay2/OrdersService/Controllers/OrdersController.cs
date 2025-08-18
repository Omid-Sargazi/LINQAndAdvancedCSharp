using Microsoft.AspNetCore.Mvc;
using OrdersService.Models;
namespace OrdersService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<Order> items = new();
        private static int id = 0;
        [HttpGet] public IEnumerable<Order> GetAll() => items;
        
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = items.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        [HttpPost] public Order Create(Order dto) { dto.Id = ++id; items.Add(dto); return dto; }
    }
    


}