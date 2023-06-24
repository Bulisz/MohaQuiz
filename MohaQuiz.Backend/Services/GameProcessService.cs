using MohaQuiz.Backend.Abstractions;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Services;

public class GameProcessService : IGameProcessService
{
    private static int _actualRoundNumber;
    private static int _actualQuestionNumber;
    private static int[]? _gameProcessArray;

    private static readonly int[] CURRENT_GAME_PROCESS = new int[] { 5, 5, 5, 5, 5, 5 };
    private readonly IGameControlHub _gameControlHub;

    public GameProcessService(IGameControlHub gameControlHub)
    {
        _gameControlHub = gameControlHub;
    }

    public void NextRound()
    {
        if (_actualRoundNumber == 0)
        {
            _gameProcessArray = CURRENT_GAME_PROCESS;
            _actualRoundNumber++;
            _actualQuestionNumber = 0;
            _gameControlHub.SendGameProcessStateAsync(GetActualGameProcess());
            //game begins
        }
        else
        {
            _actualRoundNumber++;
            _actualQuestionNumber = 0;
            _gameControlHub.SendGameProcessStateAsync(GetActualGameProcess());
        }

        if (_actualRoundNumber == _gameProcessArray!.Length)
        {
            //last round
        }

        if (_actualRoundNumber > _gameProcessArray!.Length)
        {
            //game ends
        }
    }

    public void NextQuestion()
    {
        _actualQuestionNumber++;
        _gameControlHub.SendGameProcessStateAsync(GetActualGameProcess());

        if (_gameProcessArray![_actualRoundNumber - 1] == _actualQuestionNumber)
        {
            //last question
        }

        if (_gameProcessArray![_actualRoundNumber - 1] > _actualQuestionNumber)
        {
            //round ends
        }
    }

    private static GameProcessStateDTO GetActualGameProcess()
    {
        return new GameProcessStateDTO() { RoundNumber = _actualRoundNumber, QuestionNumber = _actualQuestionNumber };
    }
}
