using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Hubs;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Services;

public class GameProcessService : IGameProcessService
{
    private static int _actualRoundNumber;
    private static int _actualQuestionNumber;
    private static int[]? _gameProcessArray;
    private static bool _isGameStarted;
    private static bool _isScoring;

    private static readonly int[] CURRENT_GAME_PROCESS = new int[] { 5, 5, 5, 5, 5, 5 };
    private readonly IHubContext<GameControlHub> _hubContext;

    public GameProcessService(IHubContext<GameControlHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task StartGame()
    {
        _gameProcessArray = CURRENT_GAME_PROCESS;
        _actualRoundNumber = 1;
        _actualQuestionNumber = 0;
        _isGameStarted = true;

        await _hubContext.Clients.All.SendAsync("StartGame", "StartGame");
        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public bool IsGameStarted()
    {
        return _isGameStarted;
    }

    public async Task NextQuestion()
    {
        _actualQuestionNumber++;

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public async Task StartScoring()
    {
        _isScoring = true;

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public async Task NextRound()
    {
        _actualRoundNumber++;
        _actualQuestionNumber = 0;
        _isScoring = false;

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public async Task ResetGame()
    {
        _gameProcessArray = Array.Empty<int>();
        _actualRoundNumber = 0;
        _actualQuestionNumber = 0;
        _isGameStarted = false;
        _isScoring = false;

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public static GameProcessStateDTO GetActualGameProcess()
    {
        return new GameProcessStateDTO() { RoundNumber = _actualRoundNumber, QuestionNumber = _actualQuestionNumber, IsGameStarted = _isGameStarted, IsScoring = _isScoring };
    }
}
