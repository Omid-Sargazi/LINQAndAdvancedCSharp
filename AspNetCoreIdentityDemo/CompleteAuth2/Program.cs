using System.Net;
using System.Text.Json;
using CompleteAuth2.Data;
using CompleteAuth2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlite("Data sourde=auth.db"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
   options.Password.RequireDigit = true;
   options.Password.RequiredLength = 8;
   options.Password.RequireNonAlphanumeric = true;
   options.Password.RequireLowercase = true;
   options.Password.RequireUppercase = true;

   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
   options.Lockout.MaxFailedAccessAttempts = 5;

   options.User.RequireUniqueEmail = true; 
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
    
})
.AddCookie(
    options =>
    {
        options.Cookie.HttpOnly = true;
         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(14); // مدت اعتبار کوکی
        options.SlidingExpiration = true; // رولینگ!
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    }
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://identity.mycompany.com",
        ValidAudience = "my-api",
        ClockSkew = TimeSpan.FromMinutes(5)
    };

    // برای API: چالش = 401
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { error = "Unauthorized" });
            return context.Response.WriteAsync(result);
        }
    };
});
builder.Services.AddAuthorization(options =>
{
    // سیاست پیش‌فرض: فقط کاربر لاگین کرده
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    // سیاست‌های کاستوم (بعداً توضیح می‌دم)
    options.AddPolicy("Over18", policy =>
        policy.RequireAuthenticatedUser()
              .AddRequirements(new MinimumAgeRequirement(18)));

    options.AddPolicy("EuropeOnly", policy =>
        policy.RequireAuthenticatedUser()
              .AddRequirements(new RegionRequirement("Europe")));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
