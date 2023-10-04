using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Repositories;

namespace MohaQuiz.Test.Repositories;

public class QuizRepositoryTests : IDisposable
{
    private AppDbContext _context = null!;
    private QuizRepository _repository = null!;
    public QuizRepositoryTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source = :memory:");
        _context = new AppDbContext(optionsBuilder.Options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
        _repository = new QuizRepository(_context);
    }


    [Fact]
    public async Task CreateTeamAsync_Valid_Test()
    {
        //Arrange
        string teamName = "Rágcsálók";

        //Act
        var result = await _repository.CreateTeamAsync(teamName);

        //Assert
        result.TeamName.Should().Be("Rágcsálók");
        _context.Teams.Count().Should().Be(1);
    }

    [Fact]
    public async Task CreateTeamAsync_AlreadyExist_Test()
    {
        //Arrange
        _context.Teams.Add(new Team() { TeamName = "Rágcsálók" });
        await _context.SaveChangesAsync();

        //Act
        var result = () => _repository.CreateTeamAsync("Rágcsálók");

        //Assert
        await result.Should().ThrowAsync<QuizException>().WithMessage("The teamName already exist");
    }

    [Fact]
    public async Task IsTeamCreatedAsync_Valid_Test()
    {
        //Arrange
        _context.Teams.Add(new Team() { TeamName = "Rágcsálók" });
        await _context.SaveChangesAsync();

        //Act
        var result = await _repository.IsTeamCreatedAsync("Rágcsálók");

        //Assert
        result!.Should().BeAssignableTo<Team>();
        result!.TeamName.Should().Be("Rágcsálók");
    }

    [Fact]
    public async Task IsTeamCreatedAsync_Null_Test()
    {
        //Arrange

        //Act
        var result = await _repository.IsTeamCreatedAsync("Rágcsálók");

        //Assert
        result!.Should().Be(null);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}