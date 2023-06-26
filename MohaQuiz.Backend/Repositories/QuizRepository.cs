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

    public async Task SendAnswerAsync(TeamAnswerDTO answerDTO)
    {
        bool aswerExists = await _context.TeamAnswers
                                    .Include(a => a.Question)
                                    .ThenInclude(q => q.Round)
                                    .Include(a => a.Team)
                                    .AnyAsync(a => a.Question.Round.RoundNumber == answerDTO.RoundNumber && a.Question.QuestionNumber == answerDTO.QuestionNumber && a.Team.TeamName == answerDTO.TeamName);

        if (!aswerExists)
        {
            Question question = (await _context.Questions
                                    .Include(q => q.Round)
                                    .FirstOrDefaultAsync(q => q.Round.RoundNumber == answerDTO.RoundNumber && q.QuestionNumber == answerDTO.QuestionNumber))!;

            Team team = (await _context.Teams.FirstOrDefaultAsync(t => t.TeamName == answerDTO.TeamName))!;

            TeamAnswer answer = new()
            {
                Question = question,
                Team = team,
                TeamAnswerText = answerDTO.TeamAnswerText
            };

            _context.TeamAnswers.Add(answer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TeamAnswer>> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam)
    {
        List<TeamAnswer> answers = await _context.TeamAnswers
                                        .Include(a => a.Question)
                                        .ThenInclude(q => q.Round)
                                        .Include(a => a.Team)
                                        .Where(a => a.Team.TeamName == roundAndTeam.TeamName && a.Question.Round.RoundNumber == roundAndTeam.RoundNumber)
                                        .ToListAsync();                                        
        return answers;
    }

    public async Task<Team?> GetTeamByNameAsync(string teamName)
    {
        Team? team = await _context.Teams.Include(t => t.TeamAnswers)
                                  .ThenInclude(a => a.Question)
                                  .ThenInclude(q => q.Round)
                                  .FirstOrDefaultAsync(t => t.TeamName == teamName);
        return team;
    }

    public async Task<int> GetRoundAmountAsync()
    {
        return await _context.Rounds.CountAsync();
    }

    public async Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO)
    {
        TeamAnswer? answer = await _context.TeamAnswers.Include(a => a.Question)
                                                .ThenInclude(q => q.Round)
                                                .Include(a => a.Team)
                                                .FirstOrDefaultAsync(a => a.Team.TeamName == scoringDTO.TeamName
                                                                  && a.Question.Round.RoundNumber == scoringDTO.RoundNumber
                                                                  && a.Question.QuestionNumber == scoringDTO.QuestionNumber
                                                                  && a.GivenScore == 0);
        if (answer != null)
        {
            answer.GivenScore = scoringDTO.Score;
            _context.Update(answer);
            await _context.SaveChangesAsync();
        }
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
