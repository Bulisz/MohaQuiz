using Microsoft.AspNetCore.Mvc;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Models.DTOs;
using MohaQuiz.Backend.Services;

namespace MohaQuiz.Backend.Controllers;

[Route("mohaquiz/[controller]")]
[ApiController]
public class GameProcessController : ControllerBase
{
    private readonly IGameProcessService _gameProcessService;

    public GameProcessController(IGameProcessService gameProcessService)
    {
        _gameProcessService = gameProcessService;
    }

    [HttpGet(nameof(NextRound))]
    public async Task<IActionResult> NextRound()
    {
        await _gameProcessService.NextRound();
        return Ok();
    }

    [HttpGet(nameof(NextQuestion))]
    public async Task<IActionResult> NextQuestion()
    {
        await _gameProcessService.NextQuestion();
        return Ok();
    }

    [HttpGet(nameof(StartGame))]
    public async Task<IActionResult> StartGame()
    {
        await _gameProcessService.StartGame();
        return Ok();
    }

    [HttpGet(nameof(IsGameStarted))]
    public ActionResult<bool> IsGameStarted()
    {
        bool isGameStarted = _gameProcessService.IsGameStarted();
        return Ok(isGameStarted);
    }

    [HttpGet(nameof(GetActualGameProcess))]
    public ActionResult<GameProcessStateDTO> GetActualGameProcess()
    {
        GameProcessStateDTO actualGameProcess = GameProcessService.GetActualGameProcess();
        return Ok(actualGameProcess);
    }

    [HttpGet(nameof(ResetGame))]
    public ActionResult ResetGame()
    {
        _gameProcessService.ResetGame();
        return Ok();
    }
}
