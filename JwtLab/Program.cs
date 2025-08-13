using System.Security.Claims;
using JwtLab.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddSingleton<TokenService>();

var tmp = builder.Services.BuildServiceProvider();
var tokenSvc = tmp.GetRequiredService<TokenService>();





builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = tokenSvc.GetValidationParameters();
});

builder.Services.AddAuthorization();



var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();


var users = new List<(string Id, string userName, string Password, string[] Roles)>
{
    ("123", "omid", "P@ssw0rd!", new[] {"Admin"})
};

app.MapGet("/", () => "Ok");

app.MapGet("/health", () => new { status = "healthy" });








app.MapPost("/login", (LoginRequest req, TokenService tokens, IConfiguration cfg) =>
{
    var user = users.SingleOrDefault(u => u.userName == req.Username && u.Password == req.Password);
    if (user == default)
        return Results.Unauthorized();
    var jwt = tokens.IssueAccessToken(user.Id, user.userName, user.Roles);
    var mins = cfg.GetValue<int>("Jwt:AccessTokenMinutes");
    return Results.Ok(new AuthResponse(jwt, mins));


    // if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
    // {
    //     return Results.BadRequest(new { messame = "username/password required" });
    // }

    // return Results.Ok(new { message = "login received.", user = req.Username });
});

app.MapGet("/me", (ClaimsPrincipal user) =>
{
    var id = user.FindFirst("sub")?.Value ?? "?";
    var name = user.Identity?.Name ?? "(unknown)";
    var roles = string.Join(", ", user.FindAll(ClaimTypes.Role).Select(r => r.Value));
    return new { id, name, roles };
}).RequireAuthorization();






app.Run();

public record LoginRequest(string Username, string Password);
public record AuthResponse(string AccessToken, int ExpiresInSeconds);



