using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Hubs;
using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;
using System.Net;

namespace MohaQuiz.Backend.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;
    private readonly IHubContext<GameControlHub> _hubContext;

    private static Dictionary<string, string> _randomTeamNames = new();
    private static readonly Random _rand = new();

    public QuizService(IQuizRepository quizRepository, IMapper mapper, IHubContext<GameControlHub> hubContext)
    {
        _quizRepository = quizRepository;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task<TeamNameDTO> CreateTeamAsync(TeamNameDTO teamNameDTO)
    {
        string teamNameToCreate;

        if (teamNameDTO.TeamName.Length < 2)
            throw new QuizException(HttpStatusCode.BadRequest, "Too short teamName");
        else
            teamNameToCreate = teamNameDTO.TeamName[..1].ToUpper() + teamNameDTO.TeamName[1..];

        TeamNameDTO newTeamDTO = _mapper.Map<TeamNameDTO>(await _quizRepository.CreateTeamAsync(teamNameToCreate));

        IEnumerable<string> teamNames = await GetAllTeamNamesAsync();
        await _hubContext.Clients.All.SendAsync("GetTeamNames", teamNames);
        await _hubContext.Clients.All.SendAsync("GetSummaryOfTeam");

        return newTeamDTO;
    }

    public async Task<TeamNameDTO?> IsTeamCreatedAsync(string teamName)
    {
        string teamNameToCheck = teamName[..1].ToUpper() + teamName[1..];
        return _mapper.Map<TeamNameDTO?>(await _quizRepository.IsTeamCreatedAsync(teamNameToCheck));
    }

    public async Task<IEnumerable<string>> GetAllTeamNamesAsync()
    {
        IEnumerable<Team> teams = await _quizRepository.GetAllTeamNamesAsync();
        return teams.Select(t => t.TeamName);
    }

    public async Task<RoundDetailsDTO> GetRoundDetailsAsync(RoundOfGameDTO roundOfGame)
    {
        return _mapper.Map<RoundDetailsDTO>(await _quizRepository.GetRoundDetailsAsync(roundOfGame));
    }

    public async Task SendAnswerAsync(TeamAnswerDTO answerDTO)
    {
        await _quizRepository.SendAnswerAsync(answerDTO);
        if(answerDTO.TeamAnswerText.Length > 0)
        {
            await _hubContext.Clients.All.SendAsync("GetTeamAnswer", answerDTO);
        }
    }

    public async Task<RoundAnswersOfTeamDTO> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam)
    {
        roundAndTeam.TeamName = _randomTeamNames[roundAndTeam.TeamName];
        List<TeamAnswer> answers = await _quizRepository.GetRoundAnswersOfTeamAsync(roundAndTeam);
        List<TeamAnswerDTO> answerList = _mapper.Map<List<TeamAnswerDTO>>(answers);

        return new RoundAnswersOfTeamDTO() { TeamName = roundAndTeam.TeamName, RoundNumber = roundAndTeam.RoundNumber, Answers = answerList };
    }

    public async Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO)
    {
        await _quizRepository.ScoringOfAQuestionAsync(scoringDTO);
    }

    public async Task<TeamScoreSummaryDTO> GetSummaryOfTeamAsync(TeamAndGameDTO teamAndGame)
    {
        Team? team = await _quizRepository.GetSummaryTeamByNameAsync(teamAndGame);
        int roundAmount = await _quizRepository.GetRoundAmountAsync(teamAndGame.GameName);

        List<double> summary = new();
        if (team is not null)
        {
            for (int i = 1; i <= roundAmount; i++)
            {
                summary.Add(team.TeamAnswers.Where(a => a.Question.Round.RoundNumber == i).Sum(a => a.GivenScore));
            }
        }

        TeamScoreSummaryDTO summaryDTO = new () { TeamScoresPerRound = summary };
        return summaryDTO;
    }

    public async Task<IEnumerable<GameSummaryDTO>> GetSummaryOfGameAsync(GameNameDTO gameName)
    {
        return await _quizRepository.GetSummaryOfGameAsync(gameName);
    }

    public TeamNameDTO GetRandomTeam(string myTeamName)
    {
        return new TeamNameDTO() { TeamName = _randomTeamNames[myTeamName] };
    }

    public async Task ResetGameAsync()
    {
        await _quizRepository.ResetGameAsync();
    }

    public async Task RandomizeTeamNamesAsync()
    {
        List<string> originalteamNames = (await _quizRepository.GetAllTeamNamesAsync()).Select(t => t.TeamName).ToList();
        List<string> shuffledTeamNames = originalteamNames.ToList();

        if (originalteamNames.Count == 1)
        {
            _randomTeamNames = new Dictionary<string, string>() { { originalteamNames[0], originalteamNames[0] } };
        }
        else if (originalteamNames.Count > 1)
        {
            bool success;
            do
            {
                success = true;
                _randomTeamNames = new Dictionary<string, string>();
                Shuffle(shuffledTeamNames);
                for (int i = 0; i < originalteamNames.Count; i++)
                {
                    if (originalteamNames[i] == shuffledTeamNames[i])
                    {
                        success = false;
                        break;
                    }
                    _randomTeamNames.Add(originalteamNames[i], shuffledTeamNames[i]);
                }
            }
            while (!success);
        }
    }

    private static List<string> Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int k = _rand.Next(i + 1);
            (list[i], list[k]) = (list[k], list[i]);
        }
        return list;
    }

    public async Task RecordRoundAsync(RoundRecordDTO roundRecordDTO)
    {
        Round newRound = _mapper.Map<Round>(roundRecordDTO);
        newRound.RoundType = (await _quizRepository.GetRoundTypeByName(roundRecordDTO.RoundTypeName))!;
        newRound.Game = (await _quizRepository.GetGameByName(roundRecordDTO.GameName))!;
        await _quizRepository.RecordRoundAsync(newRound);
    }

    public async Task<IEnumerable<string>> GetRoundTypesAsync()
    {
        return await _quizRepository.GetRoundTypesAsync();
    }

    public async Task CreateGameAsync(string gameName)
    {
        await _quizRepository.CreateGameAsync(gameName);
    }

    public async Task<IEnumerable<string>> GetAllGameNamesAsync()
    {
        IEnumerable<Game> games = await _quizRepository.GetAllGameNamesAsync();
        return games.Select(g => g.GameName);
    }
}
