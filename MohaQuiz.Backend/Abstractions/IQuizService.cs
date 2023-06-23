using MohaQuiz.Backend.Models.DTOs;

namespace MohaQuiz.Backend.Abstractions;

public interface IQuizService
{
    Task CreateTeam(TeamNameDTO teamNameDTO);
}
