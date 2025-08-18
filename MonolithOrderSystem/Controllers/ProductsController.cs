namespace MonolithOrderSystem.Controllers

{

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly InMemoryStore _store;
        public ProductsController(InMemoryStore store) => _store = store;

        [HttpPost]
        public ActionResult<Product> Create([FromBody] CreateProductRequest request)
        {
            var product = _store.AddProduct(request.Name, request.Price);
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
            => Ok(_store.GetProducts());
    }


}
