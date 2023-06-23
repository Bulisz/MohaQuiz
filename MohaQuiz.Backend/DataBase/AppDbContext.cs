using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.Models;

namespace MohaQuiz.Backend.DataBase;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
    public DbSet<TeamAnswer> TeamAnswers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
