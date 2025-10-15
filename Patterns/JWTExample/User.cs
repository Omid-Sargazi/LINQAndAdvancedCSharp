using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Patterns.JWTExample
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "User"; // برای مثال

    }

    public class JWTExample
    {
        public static void Run()
        {
            var users = new List<User>();
            var passwordHasher = new PasswordHasher<User>();

            var builder = WebApplication.CreateBuilder();

            var jwtSecret = builder.Configuration["Jwt:Key"] ?? "very_long_secret_key_here_change_it";
            var issuer = builder.Configuration["Jwt:Issuer"] ?? "MyApp";
            var audience = builder.Configuration["Jwt:Audience"] ?? "MyApiClients";
            var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(60)

                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            var app = builder.Build();
            app.MapGet("/start", () => "Hello! JWT demo");

            app.MapPost("/register", (UserDto dto) =>
{
    if (users.Any(u => u.Email == dto.Email))
        return Results.Conflict("Email already exists.");

    var user = new User
    {
        Id = users.Count + 1,
        Name = dto.Name,
        Email = dto.Email,
        Role = dto.Role ?? "User"
    };
    user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
    users.Add(user);
    return Results.Created($"/users/{user.Id}", new { user.Id, user.Email });
});
        }
    }

    public record UserDto(string Name, string Email, string Password, string? Role);

}