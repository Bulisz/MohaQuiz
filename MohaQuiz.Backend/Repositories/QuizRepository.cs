using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;
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

    public async Task<Round> GetRoundDetailsAsync(int roundnumber)
    {
        Round round = (await _context.Rounds.Include(r => r.RoundType)
                                           .Include(r => r.Questions)
                                           .ThenInclude(q => q.CorrectAnswers)
                                           .FirstOrDefaultAsync(r => r.RoundNumber == roundnumber))!;
        return round;
    }

    public async Task ResetGameAsync()
    {
        var teamAnswers = await _context.TeamAnswers.ToListAsync();
        var teams = await _context.Teams.ToListAsync();
        _context.TeamAnswers.RemoveRange(teamAnswers);
        _context.Teams.RemoveRange(teams);
        await _context.SaveChangesAsync();
    }
}
