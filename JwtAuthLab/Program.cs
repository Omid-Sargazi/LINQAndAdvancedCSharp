using System.Security.Claims;
using JwtAuthLab.Models;
using JwtAuthLab.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
public class Program

{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // 1) Bind options
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

        // 2) Token service
        builder.Services.AddSingleton<TokenService>();

        // 3) Authentication + Authorization
        //    پارامترهای اعتبارسنجی را از TokenService می‌گیریم (یکبار Resolve می‌کنیم)
        var tmpProvider = builder.Services.BuildServiceProvider();
        var tokenSvc = tmpProvider.GetRequiredService<TokenService>();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenSvc.GetValidationParameters();
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();


        // --- کاربر نمایشی (قدم 5 را حرفه‌ای‌تر می‌کنیم) ---
        var users = new List<(string Id, string Username, string Password, string[] Roles)>
{
    ("123", "omid", "P@ssw0rd!", new[] {"Admin"})
};

        // --- Login: چک ساده و صدور توکن ---
        app.MapPost("/login", (LoginRequest req, TokenService tokens) =>
        {
            var user = users.SingleOrDefault(u => u.Username == req.Username && u.Password == req.Password);
            if (user == default)
                return Results.Unauthorized();

            var jwt = tokens.IssueAccessToken(user.Id, user.Username, user.Roles);
            return Results.Ok(new AuthResponse(jwt, 60 * app.Configuration.GetValue<int>("Jwt:AccessTokenMinutes")));
        });

        // --- Protected Endpoint: نیاز به توکن ---
        app.MapGet("/me", (ClaimsPrincipal user) =>
        {
            var name = user.Identity?.Name ?? "(unknown)";
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub") ?? "?";
            var roles = string.Join(",", user.FindAll(ClaimTypes.Role).Select(r => r.Value));
            return new
            {
                userId = id,
                userName = name,
                roles
            };
        }).RequireAuthorization();

        app.MapGet("/", () => "OK");
        app.Run();

    }
    
    // --- مدل‌ها (برای سادگی همینجا) ---
record LoginRequest(string Username, string Password);
record AuthResponse(string AccessToken, int ExpiresInSeconds);

}