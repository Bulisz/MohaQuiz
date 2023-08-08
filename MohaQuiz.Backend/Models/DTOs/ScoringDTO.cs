namespace MohaQuiz.Backend.Models.DTOs;

public record ScoringDTO
{
    public string TeamName { get; set; } = string.Empty;
    public string GameName { get; set; } = string.Empty;
    public int RoundNumber { get; set; }
    public int QuestionNumber { get; set; }
    public double Score { get; set; }
}
