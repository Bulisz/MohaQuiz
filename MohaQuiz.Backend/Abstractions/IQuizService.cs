using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizService
{
    Task<TeamNameDTO> CreateTeamAsync(TeamNameDTO teamNameDTO);
    Task<TeamNameDTO?> IsTeamCreatedAsync(string teamName);
    Task<IEnumerable<string>> GetAllTeamNamesAsync();
    Task DeleteTeamAsync(string teamName);
}
