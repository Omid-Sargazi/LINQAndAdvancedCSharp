using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryAuthentication.Data;
using LibraryAuthentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlite("Data Source=LibraryDb"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer="local-auth",
        ValidAudience="local-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345")),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };

    jwtOptions.Events = new JwtBearerEvents
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

builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("AdminOnly",policy=>policy.RequireRole("admin"));
    options.AddPolicy("UserOnly",policy=>policy.RequireRole("user"));
    options.AddPolicy("ManagerOnly",policy=>policy.RequireRole("manager"));
}
);


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





builder.Services.AddAuthentication();
builder.Services.AddAuthorization();




var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapPost("/login",(LoginRequest request) =>
{
   if(request.UserName=="admin" && request.Password=="admin1234")
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name , request.UserName),
            new Claim(ClaimTypes.Role,"admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }; 

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "local-auth",
            Audience = "local-api",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
         var jwtToken = tokenHandler.WriteToken(token);

          Console.WriteLine($"🎫 Token generated for: {request.UserName} with role: admin");
        
        return Results.Ok(new { 
            Token = jwtToken,
            Username = request.UserName,
            Role = "admin",
            ExpiresIn = "1 hour"
        });
    }

    else if (request.UserName == "user" && request.Password == "user123")
    {
        // کاربر عادی - فقط برای نمایش
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.UserName),
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
            Username = request.UserName,
            Role = "user",
            Message = "این کاربر فقط می‌تواند کتابها را مشاهده کند"
        });
    }

    else if (request.UserName == "manager" && request.Password == "manager1234")
    {
        // کاربر عادی - فقط برای نمایش
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKeyForTestingOnly12345");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.UserName),
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
            Username = request.UserName,
            Role = "user",
            Message = "این کاربر فقط می‌تواند محصولات را مشاهده کند"
        });
    }

    return Results.Unauthorized();
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

