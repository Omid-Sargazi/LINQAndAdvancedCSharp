using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthDemoTwo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthDemoTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration  =configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username != "saeid" || request.Password != "123456")
            {
                return Unauthorized("Invalid credentials");
            }

              var jwtSection = _configuration.GetSection("JwtSettings");
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var signingKey = jwtSection["SigningKey"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,request.Username),
                new Claim("username",request.Username),

                new Claim(ClaimTypes.DateOfBirth, new DateTime(1990, 1, 1).ToString("yyyy-MM-dd")),
                new Claim("dateofbirth", "1990-01-01"),

                new Claim(ClaimTypes.Role,"User")  
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

              var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

             var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

              return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                expires_in = 1800
            });
        }
    }
}