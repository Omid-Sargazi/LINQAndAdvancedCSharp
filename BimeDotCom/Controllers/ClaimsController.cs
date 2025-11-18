using System.Security.Claims;
using BimeDotCom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimeDotCom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController: ControllerBase
    {
        [HttpPost]
        [Authorize]
        public IActionResult Create(ClaimRequest claim)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var region = User.FindFirst("region")?.Value ?? string.Empty;

            claim.UserId = userId;
            claim.Region = region;

            FakeDb.Claims.Add(claim);
            return Ok(claim);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var claim = FakeDb.Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null) return NotFound();

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            var region = User.FindFirst("region")?.Value ?? string.Empty;

        if (userRole == "admin") return Ok(claim);
        if (claim.UserId == userId) return Ok(claim);
        if (claim.Region == region) return Ok(claim);

        return Forbid();
        }
    }
}