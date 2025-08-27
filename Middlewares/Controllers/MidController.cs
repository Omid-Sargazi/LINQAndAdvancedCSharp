using Microsoft.AspNetCore.Mvc;

namespace Middlewares.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class MidController
    {

        [HttpGet("boom")]
        public IActionResult Boom()
        {
            throw new Exception("this is a test exception");
        }
    }
}