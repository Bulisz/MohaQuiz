using Microsoft.AspNetCore.Mvc;
using MohaQuiz.Backend.Abstractions;

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
}
