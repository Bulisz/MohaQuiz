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

    private static readonly int[] CURRENT_GAME_PROCESS = new int[] { 5, 5, 5, 5, 5, 5 };
    private readonly IHubContext<GameControlHub> _hubContext;

    public GameProcessService(IHubContext<GameControlHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void NextRound()
    {
        if (_actualRoundNumber == 0)
        {
            _gameProcessArray = CURRENT_GAME_PROCESS;
            _actualRoundNumber++;
            _actualQuestionNumber = 0;
            //game begins
        }
        else
        {
            _actualRoundNumber++;
            _actualQuestionNumber = 0;
        }

        if (_actualRoundNumber == _gameProcessArray!.Length)
        {
            //last round
        }

        if (_actualRoundNumber > _gameProcessArray!.Length)
        {
            //game ends
        }

        _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    public void NextQuestion()
    {
        _actualQuestionNumber++;

        if (_gameProcessArray![_actualRoundNumber - 1] == _actualQuestionNumber)
        {
            //last question
        }

        if (_gameProcessArray![_actualRoundNumber - 1] > _actualQuestionNumber)
        {
            //round ends
        }

        _hubContext.Clients.All.SendAsync("GetGameProcessState", GetActualGameProcess());
    }

    private static GameProcessStateDTO GetActualGameProcess()
    {
        return new GameProcessStateDTO() { RoundNumber = _actualRoundNumber, QuestionNumber = _actualQuestionNumber };
    }
}
