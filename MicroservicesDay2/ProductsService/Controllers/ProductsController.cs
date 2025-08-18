namespace ProductsService.Controllers
{
   
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private static List<Product> items = new();
    private static int id=0;
    [HttpGet] public IEnumerable<Product> GetAll()=>items;
    [HttpGet("{id}")] public ActionResult<Product> Get(int id)=>items.FirstOrDefault(x=>x.Id==id)??NotFound();
    [HttpPost] public Product Create(Product dto){ dto.Id=++id; items.Add(dto); return dto; }
}
}