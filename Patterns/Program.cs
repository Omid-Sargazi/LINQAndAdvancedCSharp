using Patterns.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

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
LeetcodeProblem.MaximumProductSubarray(arr);

app.Run();