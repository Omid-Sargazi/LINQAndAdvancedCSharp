using System.Security.Claims;
using System.Text;
using AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetTopOrders;
using AdventureWorksLINQ.AuthDemo.Application.Services;
using AdventureWorksLINQ.AuthDemo.Infrastructure.Auth;
using AdventureWorksLINQ.AuthDemo.Infrastructure.Services;
using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTopOrdersQuery).Assembly));

// افزودن سرویس‌ها
builder.Services.AddDbContext<AdventureWorks2019Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies();
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "AdventureWorksRedis";
});

// تنظیمات احراز هویت (فقط یک بار فراخوانی شود)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// تنظیمات Swagger (فقط از یکی استفاده کنید)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "XCorp API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Paste your JWT token below:",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ترتیب میدلورها (مهم!)
app.UseHttpsRedirection();
app.UseAuthentication(); // باید قبل از UseAuthorization و Swagger باشد
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/public", () => "this is a public API");
app.MapGet("/profile", (ClaimsPrincipal user) => $"hi {user.Identity?.Name ?? "user"}, welcome to profile")
    .RequireAuthorization();
app.MapGet("/admin", (ClaimsPrincipal user) => $"you enter as a manager:{user.Identity?.Name}")
    .RequireAuthorization(policy => policy.RequireRole("Admin")); // مشخص کردن نقش

app.Run();