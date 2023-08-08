using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizService
{
    Task<TeamNameDTO> CreateTeamAsync(TeamNameDTO teamNameDTO);
    Task<TeamNameDTO?> IsTeamCreatedAsync(string teamName);
    Task<IEnumerable<string>> GetAllTeamNamesAsync();
    Task ResetGameAsync();
    Task<RoundDetailsDTO> GetRoundDetailsAsync(RoundOfGameDTO roundOfGame);
    Task SendAnswerAsync(TeamAnswerDTO answerDTO);
    Task<TeamScoreSummaryDTO> GetSummaryOfTeamAsync(TeamAndGameDTO teamAndGame);
    Task<RoundAnswersOfTeamDTO> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam);
    Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO);
    TeamNameDTO GetRandomTeam(string myTeamName);
    Task RandomizeTeamNamesAsync();
    Task<IEnumerable<GameSummaryDTO>> GetSummaryOfGameAsync(GameNameDTO gameName);
    Task RecordRoundAsync(RoundRecordDTO roundRecordDTO);
    Task<IEnumerable<string>> GetRoundTypesAsync();
    Task CreateGameAsync(string gameName);
    Task<IEnumerable<string>> GetAllGameNamesAsync();
}
