using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Mixed2Controller:ControllerBase
    {
        [HttpGet()]
        public IActionResult Index()
    {
        return Content("Mixed2Controller – only JWT Bearer is used.");
    }
    }
}