using Microsoft.AspNetCore.Mvc;
using Middlewares.Middleware1;

namespace Middlewares.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            throw new NotFoundException("user", 123);
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            throw new UnauthorizedException();
        }

        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            throw new ForbiddenException();
        }

        [HttpGet("conflict")]
        public IActionResult GetConflict()
        {
            throw new ConflictException("User already exist");
        }

        [HttpGet("business")]
        public IActionResult GetBusinessRule()
        {
            throw new BusinessRuleException("Password must be strong", "At least 8 characters");
        }

        [HttpGet("validation")]
        public IActionResult GetValidation()
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Email", new[] { "Email is required", "Email format is invalid" } },
                { "Password", new[] { "Password is too weak" } }
            };

            throw new ValidationException(errors);
        }


        [HttpGet("unhandled")]
        public IActionResult GetUnhandled()
        {
            throw new Exception("Unhandled error in code.");
        }


    }
}