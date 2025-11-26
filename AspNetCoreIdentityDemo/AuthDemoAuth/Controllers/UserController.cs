using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Policy="userPolicy")]
    [Authorize(Roles ="User, Admin")]
    public class UserController:ControllerBase
    {
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("User Profile - Accessible only to Users");
        }
    }
}

// اگر دقت کنی بیشتر سایتهها بیشتر از یکی احراز هویت دارند مثلا کوکی گوگل و بقیشو خودت بگو؟ ایا میشه اونها رو هم پیاده سازی کنیم.