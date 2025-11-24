using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy="userPolicy")]
    public class UserController:ControllerBase
    {
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("User Profile - Accessible only to Users");
        }
    }
}