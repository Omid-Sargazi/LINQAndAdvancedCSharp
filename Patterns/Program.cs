using System.Reflection.Metadata.Ecma335;
using Patterns.CompineAPIProject;
using Patterns.Mediator;
using Patterns.SimpelApis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<API1>();
builder.Services.AddHttpClient<API2>();
builder.Services.AddHttpClient<FirstApi>();
builder.Services.AddHttpClient<SecondApi>();

builder.Services.AddScoped<ExecuteTwoSyncMethod>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// اجرای کد مدیاتور قبل از app.Run()
// ClientMediator.Run();
// TypeProblem.TestMakeGeneric();
// ClientCalculator.TestSimpleInvoke();
var arr = new int[] { 1, 2, 3, 4 };

// app.MapGet("/combine", () => ins.Run());
// LeetcodeProblem.MaximumProductSubarray(arr);

app.MapGet("/", async (ExecuteTwoSyncMethod executor) =>
{
    return await executor.Run();
});

app.MapGet("/next", () => "Hello Omid");


app.Run();