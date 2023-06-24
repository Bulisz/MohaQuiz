using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions
{
    public interface IGameControlHub
    {
        Task SendTeamNamesToAdmin(IQuizService quizService);
        Task SendGameProcessState(GameProcessStateDTO actualGameProcess);
    }
}