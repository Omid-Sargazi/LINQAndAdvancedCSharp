using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using APIFiftyProblems.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "local-auth",
        ValidAudience = "local-api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_VERY_LONG_SECRET_KEY_123!!"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();


app.MapPost("login", (UserLoginDto login) =>
{
    var user = FakeDatabase.FakeUsers.FirstOrDefault(u =>
        u.UserName == login.Username &&
        u.PasswordHash == login.Password);
    if (user == null)
        return Results.Unauthorized();

    var claims = new[]

    {
            new Claim("userId", user.Id.ToString()),
            new Claim("region", user.Region),
            new Claim(ClaimTypes.Role, user.Role)

    };
    var identity = new ClaimsIdentity(claims, "Bearer");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_VERY_LONG_SECRET_KEY_123!!"));


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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/secure", () => "Secure Data").RequireAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();



app.Run();

