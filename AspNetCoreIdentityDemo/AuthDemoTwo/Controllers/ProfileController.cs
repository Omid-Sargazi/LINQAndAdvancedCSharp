using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoTwo.Controllers
{
    [ApiController]
   [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController:ControllerBase
    {
        [HttpGet()]
        public IActionResult Index() => Content("Profile – only JWT Bearer is allowed.");
    }
}