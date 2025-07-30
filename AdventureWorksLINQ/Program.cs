using AdventureWorksLINQ.AuthDemo.Application.Services;
using AdventureWorksLINQ.AuthDemo.Infrastructure.Auth;
using AdventureWorksLINQ.AuthDemo.Infrastructure.Services;
using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AdventureWorks2019Context>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.UseLazyLoadingProxies();
    }
);

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



builder.Services.AddMemoryCache();  

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();


