namespace MohaQuiz.Backend.Abstractions;

public interface IGameProcessService
{
    void NextRound();
    void NextQuestion();
}
