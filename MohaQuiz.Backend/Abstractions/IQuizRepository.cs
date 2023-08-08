using MohaQuiz.Backend.Models;
using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizRepository
{
    Task<Team> CreateTeamAsync(string teamNameToCreate);
    Task<Team?> IsTeamCreatedAsync(string teamName);
    Task<IEnumerable<Team>> GetAllTeamNamesAsync();
    Task ResetGameAsync();
    Task<Round> GetRoundDetailsAsync(RoundOfGameDTO roundOfGame);
    Task SendAnswerAsync(TeamAnswerDTO answerDTO);
    Task<Team?> GetSummaryTeamByNameAsync(TeamAndGameDTO teamAndGame);
    Task<int> GetRoundAmountAsync(string gameName);
    Task<List<TeamAnswer>> GetRoundAnswersOfTeamAsync(RoundAndTeamDTO roundAndTeam);
    Task ScoringOfAQuestionAsync(ScoringDTO scoringDTO);
    Task<IEnumerable<GameSummaryDTO>> GetSummaryOfGameAsync(GameNameDTO gameName);
    Task RecordRoundAsync(Round newRound);
    Task<RoundType?> GetRoundTypeByName(string roundTypeName);
    Task<IEnumerable<string>> GetRoundTypesAsync();
    Task CreateGameAsync(string gameName);
    Task<Game?> GetGameByName(string gameName);
    Task<IEnumerable<Game>> GetAllGameNamesAsync();
}
