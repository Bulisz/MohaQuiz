using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizRepository
{
    Task<Team> CreateTeamAsync(string teamNameToCreate);
    Task<Team?> IsTeamCreatedAsync(string teamName);
    Task<IEnumerable<Team>> GetAllTeamNamesAsync();
    Task ResetGameAsync();
    Task<Round> GetRoundDetailsAsync(int roundnumber);
    Task SendAnswerAsync(TeamAnswerDTO answerDTO);
    Task<Team?> GetTeamByNameAsync(string teamName);
    Task<int> GetRoundAmountAsync();
    Task<List<TeamAnswer>> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam);
    Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO);
}
