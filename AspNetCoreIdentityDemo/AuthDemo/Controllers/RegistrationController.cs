using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Over18")]
    public class RegistrationController:ControllerBase
    {
         [HttpGet()]
         public IActionResult Index()
    {
        return Content("Registration – Over18 policy applied.");
    }
    }
}