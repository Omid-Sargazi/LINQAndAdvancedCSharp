using MiddlewareProblem.Problems;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

// app.UseMiddleware<Logging>();


// app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<RequestTimingMiddleware2>();
app.MapControllers();

app.Run();


