using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizService
{
    Task<TeamNameDTO> CreateTeamAsync(TeamNameDTO teamNameDTO);
    Task<TeamNameDTO?> IsTeamCreatedAsync(string teamName);
    Task<IEnumerable<string>> GetAllTeamNamesAsync();
    Task ResetGameAsync();
    Task<RoundDetailsDTO> GetRoundDetailsAsync(int roundnumber);
    Task SendAnswerAsync(TeamAnswerDTO answerDTO);
    Task<TeamScoreSummaryDTO> GetSummaryOfTeamAsync(string teamName);
    Task<RoundAnswersOfTeamDTO> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam);
    Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO);
    TeamNameDTO GetRandomTeam(string myTeamName);
    Task RandomizeTeamNamesAsync();
}
