using Microsoft.AspNetCore.Mvc;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Helpers;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Controllers;

[Route("mohaquiz/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost(nameof(CreateTeam))]
    public async Task<ActionResult<TeamNameDTO>> CreateTeam(TeamNameDTO teamNameDTO)
    {
        try
        {
            TeamNameDTO createdTeamNameDTO = await _quizService.CreateTeamAsync(teamNameDTO);
            return Ok(createdTeamNameDTO);
        }
        catch (QuizException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }

    [HttpGet("isteamcreated/{teamname}")]
    public async Task<ActionResult<TeamNameDTO>> IsTeamCreated(string teamName)
    {
        TeamNameDTO? teamNameDTO = await _quizService.IsTeamCreatedAsync(teamName);
        return Ok(teamNameDTO);
    }

    [HttpGet(nameof(GetAllTeamNames))]
    public async Task<ActionResult<IEnumerable<string>>> GetAllTeamNames()
    {
        IEnumerable<string> allTeamNames = await _quizService.GetAllTeamNamesAsync();
        return Ok(allTeamNames);
    }

    [HttpGet("getrounddetails/{roundnumber}")]
    public async Task<ActionResult<RoundDetailsDTO>> GetRoundDetails(int roundnumber)
    {
        RoundDetailsDTO roundDetailsDTO = await _quizService.GetRoundDetailsAsync(roundnumber);
        return Ok(roundDetailsDTO);
    }

    [HttpPost(nameof(SendAnswer))]
    public async Task<ActionResult> SendAnswer(TeamAnswerDTO answerDTO)
    {
        await _quizService.SendAnswerAsync(answerDTO);
        return Ok();
    }

    [HttpPost(nameof(GetRoundAnswersOfTeam))]
    public async Task<ActionResult<RoundAnswersOfTeamDTO>> GetRoundAnswersOfTeam(RoundAndTeamDTO roundAndTeam)
    {
        RoundAnswersOfTeamDTO summary = await _quizService.GetRoundAnswersOfTeamAsync(roundAndTeam);
        return Ok(summary);
    }


    [HttpPost(nameof(ScoringOfAQuestion))]
    public async Task<ActionResult> ScoringOfAQuestion(ScoringDTO scoringDTO)
    {
        await _quizService.ScoringOfAQuestionAsync(scoringDTO);
        return Ok();
    }

    [HttpGet("getsummaryofteam/{teamName}")]
    public async Task<ActionResult<TeamScoreSummaryDTO>> GetSummaryOfTeam(string teamName)
    {
        TeamScoreSummaryDTO summary = await _quizService.GetSummaryOfTeamAsync(teamName);
        return Ok(summary);
    }

    [HttpGet(nameof(GetSummaryOfGame))]
    public async Task<ActionResult<IEnumerable<GameSummaryDTO>>> GetSummaryOfGame()
    {
        IEnumerable<GameSummaryDTO> summary = await _quizService.GetSummaryOfGameAsync();
        return Ok(summary);
    }

    [HttpGet("getrandomteam/{myTeamName}")]
    public ActionResult<TeamNameDTO> GetRandomTeam(string myTeamName)
    {
        TeamNameDTO randomTeam = _quizService.GetRandomTeam(myTeamName);
        return Ok(randomTeam);
    }

    [HttpGet(nameof(RandomizeTeamNames))]
    public async Task<ActionResult> RandomizeTeamNames()
    {
        await _quizService.RandomizeTeamNamesAsync();
        return Ok();
    }

    [HttpGet(nameof(ResetGame))]
    public async Task<ActionResult> ResetGame()
    {
        await _quizService.ResetGameAsync();
        return Ok();
    }
}
