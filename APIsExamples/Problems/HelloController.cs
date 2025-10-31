using Microsoft.AspNetCore.Mvc;

namespace APIsExamples.Problems
{

    [ApiController]
    [Route("api/[controller]")]
    public class HelloController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello,World" });
        }
    }
}