using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions
{
    public interface IGameControlHub
    {
        //Task SendTeamNamesToAdminAsync(IEnumerable<string> teamNames);
        Task SendGameProcessStateAsync(GameProcessStateDTO actualGameProcess);
        Task SendTeamNamesToAdminAsync(IQuizService quizService);
    }
}