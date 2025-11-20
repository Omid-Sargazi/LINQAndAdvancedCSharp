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

    options.AddPolicy("ManagerOrAdmin",policy=>policy.RequireRole("manager","admin"));
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





var app = builder.Build();


app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        var errorResponse = new
        {
            Success = false,
            Message = "An internal server error occurred",
            ErrorCode = "INTERNAL_ERROR", 
            TimeStamp = DateTime.UtcNow,
            RequestId = context.TraceIdentifier
        };
        
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
});


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
            new Claim(ClaimTypes.Role, "manager"), // نقش user
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

app.MapGet("/books",async (AppDbContext db) =>
{
    return await db.Books.ToListAsync();
});

app.MapPost("/books",async(Book book,AppDbContext db) =>
{
   db.Books.Add(book);
   await db.SaveChangesAsync();
   return Results.Created($"/books/{book.Id}",book); 
}).RequireAuthorization("ManagerOrAdmin");



app.MapPut("/books/{id}",async (int id,Book updatedBook,AppDbContext db) =>
{
    var bookDb = db.Books.FirstOrDefault(b=>b.Id==id);

    if(bookDb==null)
        return Results.NotFound($"book with ID{id} not found");

    bookDb.Title = updatedBook.Title;
    bookDb.Author=updatedBook.Author;
    bookDb.ISBN = updatedBook.ISBN;

    await db.SaveChangesAsync();

    return Results.Created($"book{bookDb.Id}",bookDb);
   

}).RequireAuthorization("AdminOnly");

app.MapPost("/users/register",async (AppDbContext db, User user) =>
{
   await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"{user.Id}",user); 
});

app.MapGet("/users",async (AppDbContext db) =>
{   
    var users = await db.Users.ToListAsync();
    return Results.Created($"Users",users);
}).RequireAuthorization("AdminOrManager");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseExceptionHandler(async exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType="application/json";

        var error = new
        {
            Success=false,
            Message="An Errro occurred while processing your request",
            ErrorCode="Internal_Error",
            TimeStamp=DateTime.UtcNow,
            RequestId=context.TraceIdentifier,
        };

        await context.Response.WriteAsJsonAsync(error);
    });
});

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    response.ContentType = "application/json";
    
    var error = response.StatusCode switch
    {
        404 => new { Success = false, Message = "Resource not found", ErrorCode = "NOT_FOUND" },
        401 => new { Success = false, Message = "Unauthorized access", ErrorCode = "UNAUTHORIZED" },
        403 => new { Success = false, Message = "Access forbidden", ErrorCode = "FORBIDDEN" },
        _ => new { Success = false, Message = "An error occurred", ErrorCode = "UNKNOWN_ERROR" }
    };
    
    await response.WriteAsJsonAsync(error);
});




app.Run();

