using apbd_test_2.Data;
using apbd_test_2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_test_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        string? connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<DatabaseContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        builder.Services.AddScoped<IDbService, DbService>();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseAuthorization();
        
        app.Run();
    }
}