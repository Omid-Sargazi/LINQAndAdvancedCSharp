using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoAuth.Controllers
{
    [ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "ManagerPolicy")]
public class ManagerController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult GetDashboard()
    {
        return Ok("Admin Dashboard - Accessible only to Admins");
    }
}

}