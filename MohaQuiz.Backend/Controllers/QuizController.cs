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

    [HttpGet(nameof(ResetGame))]
    public async Task<ActionResult> ResetGame()
    {
        await _quizService.ResetGameAsync();
        return Ok();
    }
}
