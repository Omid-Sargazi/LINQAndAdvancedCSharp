using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BimeDotCom.Models;
using BimeDotCom.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();



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


builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = "local-auth",
        ValidAudience = "local-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_VERY_LONG_SECRET_KEY_123!!"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SameRegion", policy => policy.AddRequirements(new SameRegionRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, SameRegionHandler>();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();




var app = builder.Build();


app.MapPost("login", (LoginDto login) =>
{
    var user = FakeDb.Users.FirstOrDefault(u => u.Username == login.UserName && u.PasswordHash == login.Password);
    if (user == null)
        return Results.Unauthorized();

    var claims = new[]

    {
        new Claim("userId",user.Id.ToString()),
new Claim("region",user.Region),
new Claim(ClaimTypes.Role,user.Role),
    };

    var identity = new ClaimsIdentity(claims, "Bearer");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_VERY_LONG_SECRET_KEY_123!!"));

    var token = new JwtSecurityToken(issuer: "local-auth", audience: "local-api", claims: identity.Claims, expires: DateTime.UtcNow.AddHours(1),
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

    return Results.Ok(new
    {
        token = new JwtSecurityTokenHandler().WriteToken(token)
    });
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.Run();


