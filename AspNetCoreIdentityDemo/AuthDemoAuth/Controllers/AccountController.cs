using System.Threading.Tasks;
using AuthDemoAuth.Models;
using AuthDemoAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoAuth.Controllers
{
    [ApiController]
     [Route("api/account")]
    public class AccountController:ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Message="Invalid Data",
                    StatusCode=StatusCodes.Status400BadRequest,
                });
            }


            if(await _userService.UserExistsAsync(model.Email))
            {
                return Conflict(new Response
                {
                    Message="User Already existes",
                    StatusCode=StatusCodes.Status409Conflict,
                });
            }

            var result = await _userService.RegisterUserAsync(model);
            if(result)
            {
                return Ok(new Response
                {
                    Message="User Register Successfully",
                    StatusCode = StatusCodes.Status200OK
                });
            }

            return StatusCode(500, new Response
            {
                Message = "Registeration Failed",
                StatusCode = StatusCodes.Status500InternalServerError
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Message="Invalid Data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);
            if(user==null)
            {
                 return Unauthorized(new Response
            {
                Message="Invalid Credentials",
                StatusCode = StatusCodes.Status401Unauthorized
            });
            }

            var isValidUser = await _userService.ValidateUserAsync(model);
             if (!isValidUser)
            {
                return Unauthorized(new Response
                {
                    Message = "Invalid credentials", 
                    StatusCode = StatusCodes.Status401Unauthorized
                });
            }
            return Ok(new LoginResponse
            {
                Message = "Login seccessfull",
                Email=user.Email,
                Role=user.Role
            });

           
        }
    }

    public class Response
    {
        public string Message {get;set;}
        public int StatusCode {get;set;}
    }

    public class LoginResponse
    {
        public string Message {get;set;}
        public string Role {get;set;}
        public string Email {get;set;}
    }
}