using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.DataBase;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models;
using System.Net;

namespace MohaQuiz.Backend.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateTeam(string teamNameToCreate)
    {
        Team newTeam;

        bool isTeamNameExists = _context.Teams.Any(t => t.TeamName == teamNameToCreate);

        if (isTeamNameExists)
            throw new QuizException(HttpStatusCode.BadRequest, "The teamName already exist");
        else
        {
            newTeam = new Team() { TeamName = teamNameToCreate };
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();
        }
    }
}
