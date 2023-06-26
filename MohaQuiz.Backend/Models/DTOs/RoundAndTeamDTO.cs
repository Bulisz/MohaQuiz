namespace MohaQuiz.Backend.Models.DTOs;

public record RoundAndTeamDTO
{
    public string TeamName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
}
