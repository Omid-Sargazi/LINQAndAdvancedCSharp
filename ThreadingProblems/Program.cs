using System.Threading.Tasks;
using ThreadingProblems.Problems;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddOpenApi();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        //  SyncConnection.Execute();
         await SyncConnection.Execute2();

        app.Run();


    }
}