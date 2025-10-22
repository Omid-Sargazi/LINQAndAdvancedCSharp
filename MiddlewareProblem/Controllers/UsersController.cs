using Microsoft.AspNetCore.Mvc;

namespace MiddlewareProblem.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            await Task.Delay(500);
            return Ok(new { message = "Users retrieved successfully" });
        }
        
        [HttpGet("error")]
        public IActionResult GetError()
        {
            throw new Exception("Test Exception from UsersController");
        }
    }
}