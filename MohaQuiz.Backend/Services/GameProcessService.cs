using Microsoft.AspNetCore.SignalR;
using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Hubs;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Services;

public class GameProcessService : IGameProcessService
{
    private static string _gameName = string.Empty;
    private static int _actualRoundNumber;
    private static int _actualQuestionNumber;
    private static bool _isGameStarted;
    private static bool _isScoring;
    private static bool _isGameFinished;
    private static readonly int _mAX_ROUND_NUMBER = 6;

    private readonly IHubContext<GameControlHub> _hubContext;

    public GameProcessService(IHubContext<GameControlHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task StartGame(string gameName)
    {
        _gameName = gameName;
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


    public async Task StopScoring()
    {
        _isScoring = false;
        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public async Task NextRound()
    {
        _actualRoundNumber++;
        _actualQuestionNumber = 0;
        _isScoring = false;

        if(_actualRoundNumber > _mAX_ROUND_NUMBER)
        {
            _isGameFinished = true;
        }

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public async Task ResetGame()
    {
        _gameName = string.Empty;
        _actualRoundNumber = 0;
        _actualQuestionNumber = 0;
        _isGameStarted = false;
        _isScoring = false;
        _isGameFinished = false;

        await _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public static GameProcessStateDTO GetActualGameProcess()
    {
        return new GameProcessStateDTO() { GameName = _gameName,
                                           RoundNumber = _actualRoundNumber,
                                           QuestionNumber = _actualQuestionNumber,
                                           IsGameStarted = _isGameStarted,
                                           IsScoring = _isScoring,
                                           IsGameFinished = _isGameFinished};
    }

}
