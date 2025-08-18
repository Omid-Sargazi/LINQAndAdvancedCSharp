namespace MonolithOrderSystem.Controllers
{
    [ApiController]
    [Route("api/orders")]

    public class OrdersController
    {
        private readonly InMemoryStore _store;
        public OrdersController(InMemoryStore store) => _store = store;

        [HttpPost]
        public ActionResult<Order> Create([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = _store.AddOrder(request.UserId, request.ProductId, request.Quantity);
                return CreatedAtAction(nameof(GetAll), new { id = order.Id }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
            => Ok(_store.GetOrders());
    }
    
}