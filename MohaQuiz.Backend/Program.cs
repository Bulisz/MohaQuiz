using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.MiddleWareConfigs;
using System;

namespace MohaQuiz.Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No connectionString");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddCorsRules();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors();

        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}