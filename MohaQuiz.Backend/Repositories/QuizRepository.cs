using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models;
using System.Net;

namespace MohaQuiz.Backend.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;
    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Team> CreateTeamAsync(string teamNameToCreate)
    {
        Team newTeam;

        await _semaphoreSlim.WaitAsync();
        try
        {
            bool isTeamNameExists = await _context.Teams.AnyAsync(t => t.TeamName == teamNameToCreate);

            if (isTeamNameExists)
                throw new QuizException(HttpStatusCode.BadRequest, "The teamName already exist");

            newTeam = new Team() { TeamName = teamNameToCreate };
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();
        }
        finally
        {
            _semaphoreSlim.Release();
        }

        return newTeam;
    }

    public async Task<Team?> IsTeamCreatedAsync(string teamName)
    {
        Team? team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamName == teamName);
        return team;
    }

    public async Task<IEnumerable<Team>> GetAllTeamNamesAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task DeleteTeamAsync(string teamName)
    {
        Team? teamToDelete = await _context.Teams.FirstOrDefaultAsync(t => t.TeamName == teamName);

        if (teamToDelete is not null)
        {
            _context.Teams.Remove(teamToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
