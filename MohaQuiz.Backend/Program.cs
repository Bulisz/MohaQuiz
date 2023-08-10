using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.Hubs;
using MohaQuiz.Backend.MiddleWareConfigs;
using MohaQuiz.Backend.Repositories;
using MohaQuiz.Backend.Services;

namespace MohaQuiz.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No connectionString");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


        builder.Services.AddCorsRules();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddSignalR();

        builder.Services.AddScoped<IGameProcessService,GameProcessService>();
        builder.Services.AddScoped<IQuizRepository,QuizRepository>();
        builder.Services.AddScoped<IQuizService,QuizService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors();

        app.UseAuthorization();
        app.MapControllers();

        app.MapFallbackToFile("index.html");
        app.MapHub<GameControlHub>("/gamecontrolhub");

        app.Run();
    }
}