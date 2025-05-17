using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

       
        builder.Services.AddServices(builder.Configuration);
        builder.Services.AddCors();

        builder.Services.AddControllers();

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));


        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}