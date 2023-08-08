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
        return await _context.Teams.OrderBy(t => t.Id).ToListAsync();
    }

    public async Task<Round> GetRoundDetailsAsync(RoundOfGameDTO rounfOfGame)
    {
        Round round = (await _context.Rounds.Include(r => r.Game)
                                           .Include(r => r.RoundType)
                                           .Include(r => r.Questions.OrderBy(q => q.QuestionNumber))
                                           .ThenInclude(q => q.CorrectAnswers)
                                           .FirstOrDefaultAsync(r => r.RoundNumber == rounfOfGame.RoundNumber && r.Game.GameName == rounfOfGame.GameName))!;

        return round;
    }

    public async Task SendAnswerAsync(TeamAnswerDTO answerDTO)
    {
        bool aswerExists = await _context.TeamAnswers
                                    .Include(a => a.Question)
                                    .ThenInclude(q => q.Round)
                                    .ThenInclude(r => r.Game)
                                    .Include(a => a.Team)
                                    .AnyAsync(a => a.Question.Round.Game.GameName == answerDTO.GameName &&
                                                   a.Question.Round.RoundNumber == answerDTO.RoundNumber &&
                                                   a.Question.QuestionNumber == answerDTO.QuestionNumber &&
                                                   a.Team.TeamName == answerDTO.TeamName);

        if (!aswerExists)
        {
            Question question = (await _context.Questions
                                    .Include(q => q.Round)
                                    .ThenInclude(r => r.Game)
                                    .FirstOrDefaultAsync(q => q.Round.Game.GameName == answerDTO.GameName &&
                                                              q.Round.RoundNumber == answerDTO.RoundNumber &&
                                                              q.QuestionNumber == answerDTO.QuestionNumber))!;

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
                                        .ThenInclude(r => r.Game)
                                        .Include(a => a.Team)
                                        .Where(a => a.Team.TeamName == roundAndTeam.TeamName &&
                                                    a.Question.Round.Game.GameName == roundAndTeam.GameName &&
                                                    a.Question.Round.RoundNumber == roundAndTeam.RoundNumber &&
                                                    a.Question.QuestionNumber > 0)
                                        .OrderBy(a => a.Question.QuestionNumber)
                                        .ToListAsync();

        return answers;
    }

    public async Task<Team?> GetSummaryTeamByNameAsync(TeamAndGameDTO teamAndGame)
    {
        Team? team = await _context.Teams.Include(t => t.TeamAnswers)
                                  .ThenInclude(a => a.Question)
                                  .ThenInclude(q => q.Round)
                                  .ThenInclude(r => r.Game)
                                  .FirstOrDefaultAsync(t => t.TeamName == teamAndGame.TeamName);
        if (team is not null)
            team.TeamAnswers = team.TeamAnswers.Where(ta => ta.Question.Round.Game.GameName == teamAndGame.GameName).ToList();

        return team;
    }

    public async Task<int> GetRoundAmountAsync(string gameName)
    {
        return await _context.Rounds.Where(r => r.Game.GameName == gameName).CountAsync();
    }

    public async Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO)
    {
        TeamAnswer? answer = await _context.TeamAnswers
                                                .Include(a => a.Question)
                                                .ThenInclude(q => q.Round)
                                                .ThenInclude(r => r.Game)
                                                .Include(a => a.Team)
                                                .FirstOrDefaultAsync(a => a.Team.TeamName == scoringDTO.TeamName
                                                                  && a.Question.Round.Game.GameName == scoringDTO.GameName
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

    public async Task<IEnumerable<GameSummaryDTO>> GetSummaryOfGameAsync(GameNameDTO gameName)
    {
        IEnumerable<Team> teams = await _context.Teams
                                                    .Include(t => t.TeamAnswers)
                                                    .ThenInclude(ta => ta.Question)
                                                    .ThenInclude(q => q.Round)
                                                    .ThenInclude(r => r.Game)
                                                    .OrderByDescending(t => t.TeamAnswers.Sum(a => a.GivenScore))
                                                    .ToListAsync();

        return teams.Select(t => new GameSummaryDTO { TeamName = t.TeamName, TeamScore = t.TeamAnswers.Where(ta => ta.Question.Round.Game.GameName == gameName.GameName).Sum(a => a.GivenScore) });
    }

    public async Task ResetGameAsync()
    {
        var teamAnswers = await _context.TeamAnswers.ToListAsync();
        var teams = await _context.Teams.ToListAsync();
        _context.TeamAnswers.RemoveRange(teamAnswers);
        _context.Teams.RemoveRange(teams);
        await _context.SaveChangesAsync();
    }

    public async Task RecordRoundAsync(Round newRound)
    {
        newRound.RoundNumber = _context.Rounds.Count() + 1;
        _context.Rounds.Add(newRound);
        await _context.SaveChangesAsync();
    }

    public async Task<RoundType?> GetRoundTypeByName(string roundTypeName)
    {
        return await _context.RoundTypes.FirstOrDefaultAsync(rt => rt.RoundTypeName == roundTypeName);
    }

    public async Task<IEnumerable<string>> GetRoundTypesAsync()
    {
        return await _context.RoundTypes.Select(rt => rt.RoundTypeName).ToListAsync();
    }

    public async Task CreateGameAsync(string gameName)
    {
        Game game = new() { GameName = gameName };
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task<Game?> GetGameByName(string gameName)
    {
        return await _context.Games.FirstOrDefaultAsync(g => g.GameName == gameName);
    }

    public async Task<IEnumerable<Game>> GetAllGameNamesAsync()
    {
        return await _context.Games.OrderBy(t => t.Id).ToListAsync();
    }
}
