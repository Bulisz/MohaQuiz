namespace MohaQuiz.Backend.Abstractions;

public interface IGameProcessService
{
    Task NextRound();
    Task NextQuestion();
    Task StartGame(string gameName);
    bool IsGameStarted();
    Task ResetGame();
    Task StartScoring();
    Task StopScoring();
}
