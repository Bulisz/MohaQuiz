using AutoMapper;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;
using System.Net;

namespace MohaQuiz.Backend.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;
    private readonly IGameControlHub _gameControlHub;

    public QuizService(IQuizRepository quizRepository, IMapper mapper, IGameControlHub gameControlHub)
    {
        _quizRepository = quizRepository;
        _mapper = mapper;
        _gameControlHub = gameControlHub;
    }

    public async Task<TeamNameDTO> CreateTeamAsync(TeamNameDTO teamNameDTO)
    {
        string teamNameToCreate;

        if (teamNameDTO.TeamName.Length < 2)
            throw new QuizException(HttpStatusCode.BadRequest, "Too short teamName");
        else
            teamNameToCreate = teamNameDTO.TeamName[..1].ToUpper() + teamNameDTO.TeamName[1..];

        TeamNameDTO newTeamDTO = _mapper.Map<TeamNameDTO>(await _quizRepository.CreateTeamAsync(teamNameToCreate));

        //IEnumerable<string> teamNames = await GetAllTeamNamesAsync();
        //await _gameControlHub.SendTeamNamesToAdminAsync(teamNames);

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

    public async Task DeleteTeamAsync(string teamName)
    {
        string teamNameToDelete = teamName[..1].ToUpper() + teamName[1..];
        await _quizRepository.DeleteTeamAsync(teamNameToDelete);
    }
}
