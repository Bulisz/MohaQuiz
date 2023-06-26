namespace MohaQuiz.Backend.Models.DTOs;

public record RoundAnswersOfTeamDTO
{
    public string TeamName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
    public List<TeamAnswerDTO> Answers { get; set; } = null!;
}
