using System.Reflection;
using Middlewares.MediatorPattern;
using Middlewares.Middleware1;
using Middlewares.ReflectionProblem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMediatore, Mediator>();


// builder.Services.AddTransient<IRequestHandler<UserComamnd, bool>, UserCommandHandler>();
// builder.Services.AddTransient<IRequestHandler<UserQuery, User>, UserQueryHandler>();

builder.Services.AddRequestHandler(Assembly.GetExecutingAssembly());

builder.Services.AddRepositories(Assembly.GetExecutingAssembly());



var app = builder.Build();

app.MapGet("/reflection", (IMediatore mediatore) =>
{
    var result = mediatore.Send<UserComamnd, bool>(new UserComamnd { Id = 1 });
    // var query = mediatore.Send<UserQuery, User>(new UserQuery { Id = 10 });
    Console.WriteLine("I`m In Reflection");
    return new
    {
        CommandResult = result,
        // QueryResult = query
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseExceptionsHandlingMiddleware();

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


