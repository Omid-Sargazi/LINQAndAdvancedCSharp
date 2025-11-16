using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// تنظیمات Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "local-auth",
        ValidAudience = "local-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_YOUR_SECRET_KEY_123"))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("admin_greetings", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("scope", "greetings_api"));

var app = builder.Build();

// تنظیمات Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// endpoints
app.MapGet("/", () => "Hello World");

// اصلاح شده: استفاده از پارامترهای ساده
app.MapGet("/login", (string userName, string password) =>
{
    if (userName != "Omid" || password != "1234") 
        return Results.Unauthorized();

    var claims = new[]
    {
        new Claim("UserId", "1"),
        new Claim("region", "north"),
        new Claim(ClaimTypes.Role, "admin"),
        new Claim("scope", "greetings_api")
    };

    var identity = new ClaimsIdentity(claims);
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_YOUR_SECRET_KEY_123"));

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = identity,
        Expires = DateTime.UtcNow.AddHours(1),
        Issuer = "local-auth",
        Audience = "local-api",
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
    };

    var handler = new JwtSecurityTokenHandler();
    var token = handler.CreateToken(tokenDescriptor);
    var jwt = handler.WriteToken(token);

    return Results.Ok(new { token = jwt });
});

app.MapGet("/secure", () => "This is secure context").RequireAuthorization();
app.MapGet("/admin", () => "Admin area").RequireAuthorization(new AuthorizeAttribute { Roles = "admin" });
app.MapGet("/hello", () => "Hello world!").RequireAuthorization("admin_greetings");

app.Run();

// مدل برای login (اگر نیاز دارید)
public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}