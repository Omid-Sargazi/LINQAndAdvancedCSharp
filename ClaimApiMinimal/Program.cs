using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClaimApiMinimal.Data;
using ClaimApiMinimal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer="local-auth",
        ValidAudience="local-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345")),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            return Task.CompletedTask;
        }
    };
});





builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",policy=>policy.RequireRole("admin"));
});



builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insert Bearer Token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement{
    {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
            Reference = new Microsoft.OpenApi.Models.OpenApiReference{
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }});
});


builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlite("Data Source=products.db"));


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/products", async (AppDbContext db) =>
{
    return await db.Products.ToListAsync();
});

app.MapPost("/products",async(Product product,AppDbContext db) =>
{
   db.Products.Add(product);
   await db.SaveChangesAsync();
   return Results.Created($"/products/{product.Id}",product); 
}).RequireAuthorization("AdminOnly");


app.MapPost("/login", (ClaimApiMinimal.Models.LoginRequest request) =>
{
    
    if (request.Username == "admin" && request.Password == "admin123")
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "admin"), // نقش admin
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "local-auth",   
            Audience = "local-api",   
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        
        Console.WriteLine($"🎫 Token generated for: {request.Username} with role: admin");
        
        return Results.Ok(new { 
            Token = jwtToken,
            Username = request.Username,
            Role = "admin",
            ExpiresIn = "1 hour"
        });
    }
    else if (request.Username == "user" && request.Password == "user123")
    {
        // کاربر عادی - فقط برای نمایش
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "user"), // نقش user
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "local-auth",
            Audience = "local-api",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        
        return Results.Ok(new { 
            Token = jwtToken,
            Username = request.Username,
            Role = "user",
            Message = "این کاربر فقط می‌تواند محصولات را مشاهده کند"
        });
    }
    
    return Results.Unauthorized();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();

