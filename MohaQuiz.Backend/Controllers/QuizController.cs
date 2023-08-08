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

    [HttpPost(nameof(GetRoundDetails))]
    public async Task<ActionResult<RoundDetailsDTO>> GetRoundDetails(RoundOfGameDTO roundOfGame)
    {
        RoundDetailsDTO roundDetailsDTO = await _quizService.GetRoundDetailsAsync(roundOfGame);
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

    [HttpPost(nameof(GetSummaryOfTeam))]
    public async Task<ActionResult<TeamScoreSummaryDTO>> GetSummaryOfTeam(TeamAndGameDTO teamAndGame)
    {
        TeamScoreSummaryDTO summary = await _quizService.GetSummaryOfTeamAsync(teamAndGame);
        return Ok(summary);
    }

    [HttpPost(nameof(GetSummaryOfGame))]
    public async Task<ActionResult<IEnumerable<GameSummaryDTO>>> GetSummaryOfGame(GameNameDTO gameName)
    {
        IEnumerable<GameSummaryDTO> summary = await _quizService.GetSummaryOfGameAsync(gameName);
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

    [HttpPost(nameof(CreateGame))]
    public async Task<IActionResult> CreateGame(GameNameDTO gameName)
    {
        await _quizService.CreateGameAsync(gameName.GameName);
        return Ok();
    }

    [HttpGet(nameof(GetAllGameNames))]
    public async Task<ActionResult<IEnumerable<string>>> GetAllGameNames()
    {
        IEnumerable<string> allGameNames = await _quizService.GetAllGameNamesAsync();
        return Ok(allGameNames);
    }

    [HttpPost(nameof(RecordRound))]
    public async Task<IActionResult> RecordRound(RoundRecordDTO roundRecordDTO)
    {
        await _quizService.RecordRoundAsync(roundRecordDTO);
        return Ok();
    }

    [HttpGet(nameof(GetRoundTypes))]
    public async Task<ActionResult<IEnumerable<string>>> GetRoundTypes()
    {
        IEnumerable<string> roundTypes = await _quizService.GetRoundTypesAsync();
        return Ok(roundTypes);
    }
}
