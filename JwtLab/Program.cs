using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Ok");

app.MapGet("/health", () => new { status="healthy" });







app.MapPost("/login", (LoginRequest req) =>
{
    if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
    {
        return Results.BadRequest(new { messame = "username/password required" });
    }

    return Results.Ok(new { message = "login received.", user = req.Username });
});




app.Run();

public record LoginRequest(string Username, string Password);



