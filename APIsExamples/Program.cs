var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();



// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     { 
//         Title = "My API", 
//         Version = "v1",
//         Description = "ASP.NET Core Web API",
//         Contact = new Microsoft.OpenApi.Models.OpenApiContact
//         {
//             Name = "Your Name",
//             Email = "your.email@example.com"
//         }
//     });
// });



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.UseRouting();
app.MapControllers();

app.Run();
