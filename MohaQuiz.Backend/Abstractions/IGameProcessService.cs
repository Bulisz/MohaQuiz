namespace MohaQuiz.Backend.Abstractions;

public interface IGameProcessService
{
    Task NextRound();
    Task NextQuestion();
    Task StartGame();
    bool IsGameStarted();
    Task ResetGame();
    Task StartScoring();
    Task StopScoring();
}
