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

    [HttpGet("startgame/{gameName}")]
    public async Task<IActionResult> StartGame(string gameName)
    {
        await _gameProcessService.StartGame(gameName);
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

    [HttpGet(nameof(StartScoring))]
    public async Task<IActionResult> StartScoring()
    {
        await _gameProcessService.StartScoring();
        return Ok();
    }

    [HttpGet(nameof(StopScoring))]
    public async Task<IActionResult> StopScoring()
    {
        await _gameProcessService.StopScoring();
        return Ok();
    }

    [HttpGet(nameof(ResetGame))]
    public ActionResult ResetGame()
    {
        _gameProcessService.ResetGame();
        return Ok();
    }
}
