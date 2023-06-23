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
    public async Task<ActionResult> CreateTeam(TeamNameDTO teamNameDTO)
    {
        try
        {
            await _quizService.CreateTeam(teamNameDTO);
            return Ok();
        }
        catch (QuizException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }
}
