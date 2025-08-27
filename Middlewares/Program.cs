using Middlewares.Middleware1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// app.UseMiddleware<MyCustomMiddleware>();
app.UseMyCustomMiddleware();

app.Use(async (context, next) =>
{
    Console.WriteLine("use 1");
    await next();
    Console.WriteLine("use 2");
});

app.UseLoggingMiddleware();

app.UseExceptionHandlingMiddleware();

// app.Run(async (context) =>
// {
//     await context.Response.WriteAsync("hello from run");
// });


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


