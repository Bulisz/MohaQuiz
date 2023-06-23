using AutoMapper;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models.DTOs;
using System.Net;

namespace MohaQuiz.Backend.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;

    public QuizService(IQuizRepository quizRepository, IMapper mapper)
    {
        _quizRepository = quizRepository;
        _mapper = mapper;
    }

    public async Task CreateTeam(TeamNameDTO teamNameDTO)
    {
        string teamNameToCreate;

        if (teamNameDTO.TeamName.Length < 2)
            throw new QuizException(HttpStatusCode.BadRequest, "Too short teamName");
        else
            teamNameToCreate = teamNameDTO.TeamName[..1].ToUpper() + teamNameDTO.TeamName[1..];

        await _quizRepository.CreateTeam(teamNameToCreate);
    }
}
